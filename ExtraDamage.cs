using Microsoft.Xna.Framework;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;
using TShockAPI.Hooks;
using Command = ExtraDamage.Command;

namespace ExtraDamage;

[ApiVersion(2, 1)]
public class ExtraDamage : TerrariaPlugin
{
    #region 插件信息
    public override string Name => "打怪额外伤害";
    public override string Author => "哨兵 羽学";
    public override Version Version => new(1, 0, 0);
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

        var life = (int)(npc.life / (float)npc.lifeMax * 100);
        if (life <= Config.Life)
        {
            return;
        }

        if (!cooldowns.ContainsKey(plr.name))
        {
            cooldowns[plr.name] = now;
            color[plr.name] = Config.ColorRGB;
        }

        //现在-玩家时间 >= 冷却时间 设置额外伤害
        if ((now - cooldowns[plr.name]).TotalSeconds >= Config.Cooldown)
        {
            var damage = args.Damage * (Config.ExtraDamage * 0.01);
            npc.life -= (int)damage;
            npc.netUpdate = true;

            TSPlayer.All.SendData(PacketTypes.CreateCombatTextExtended, $"{damage:F1}", (int)color[plr.name].PackedValue, npc.position.X, npc.position.Y - 3, 0f, 0);

            cooldowns[plr.name] = now;
        }
    } 
    #endregion
}