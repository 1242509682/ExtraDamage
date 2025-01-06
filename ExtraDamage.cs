using Microsoft.Xna.Framework;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;
using TShockAPI.Hooks;

namespace ExtraDamage;

[ApiVersion(2, 1)]
public class ExtraDamage : TerrariaPlugin
{
    #region 插件信息
    public override string Name => "打怪额外伤害";
    public override string Author => "哨兵 羽学";
    public override Version Version => new(1, 0, 3);
    public override string Description => "玩家在冷却时间后攻击怪物造成额外伤害并提示自己的额外伤害值气泡";
    #endregion

    #region 注册与释放
    public ExtraDamage(Main game) : base(game) { }
    public override void Initialize()
    {
        LoadConfig();
        GeneralHooks.ReloadEvent += ReloadConfig;
        ServerApi.Hooks.NpcStrike.Register(this, this.OnNpcStrike);
        TShockAPI.Commands.ChatCommands.Add(new TShockAPI.Command("ExtraDamage.use", Command.CMD, "ed"));
    }
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            GeneralHooks.ReloadEvent -= ReloadConfig;
            ServerApi.Hooks.NpcStrike.Deregister(this, this.OnNpcStrike);
            TShockAPI.Commands.ChatCommands.RemoveAll(x => x.CommandDelegate == Command.CMD);
        }
        base.Dispose(disposing);
    }
    #endregion

    #region 配置重载读取与写入方法
    internal static Configuration Config = new();
    private static void ReloadConfig(ReloadEventArgs args = null!)
    {
        LoadConfig();
        args.Player.SendInfoMessage("[打怪额外伤害]重新加载配置完毕。");
    }
    private static void LoadConfig()
    {
        Config = Configuration.Read();
        Config.Write();
    }
    #endregion

    #region 打怪额外伤害核心方法 + 头顶悬浮文字
    private readonly Dictionary<string, DateTime> cooldowns = new Dictionary<string, DateTime>();
    public static Dictionary<string, Color> color = new Dictionary<string, Color>();
    private void OnNpcStrike(NpcStrikeEventArgs args)
    {
        var now = DateTime.Now;
        var plr = args.Player;
        var npc = args.Npc;
        if (plr == null || npc == null || !Config.Enabled || Config.NPClist.Contains(npc.type))
        {
            return;
        }

        //低于生命不增伤
        var life = npc.life / (float)npc.lifeMax;
        if (life <= Config.Life)
        {
            return;
        }

        //没有冷却时间的玩家则设置冷却时间和颜色
        if (!cooldowns.ContainsKey(plr.name))
        {
            cooldowns[plr.name] = now;
            color[plr.name] = Config.ColorRGB;
        }

        //现在-玩家时间 >= 冷却时间 设置额外伤害
        if ((now - cooldowns[plr.name]).TotalSeconds >= Config.Cooldown)
        {
            //计算额外伤害并涵盖暴击
            var damage = args.Damage * Config.ExtraDamage * (args.Critical ? 2 : 1);
            npc.life -= (int)damage;
            npc.netUpdate = true;

            //发送悬浮文字
            TSPlayer.All.SendData(PacketTypes.CreateCombatTextExtended, $"{damage:F1}",
                                 (int)color[plr.name].PackedValue, npc.position.X, npc.position.Y - 3, 0f, 0);

            //范围伤害
            if (Config.RangeDamage)
            {
                var inRange = Main.npc.Where(n => n.active && !n.friendly &&
                                             !Config.NPClist.Contains(n.type) &&
                                             n.Distance(npc.position) <= Config.MaxRange * 16);

                //循环查找符合范围内的NPC并造成同等伤害
                foreach (var npc2 in inRange)
                {
                    npc2.StrikeNPC((int)damage, args.KnockBack, args.HitDirection, args.Critical, args.NoEffect, args.FromNet);
                    npc2.netUpdate = true;

                    //发送悬浮文字
                    TSPlayer.All.SendData(PacketTypes.CreateCombatTextExtended, $"{damage:F1}",
                                         (int)color[plr.name].PackedValue, npc2.position.X, npc2.position.Y - 3, 0f, 0);
                }
            }

            // 额外弹幕
            if (Config.ProjEnabled && Config.ExtraProj != null && Config.ExtraProj.Count > 0)
            {
                var Source = plr.GetProjectileSource_Item(plr.HeldItem);
                MyProjectile.SpawnProjectile(Source, Config.ExtraProj, npc, args.KnockBack);
            }

            cooldowns[plr.name] = now;
        }
    }
    #endregion
}