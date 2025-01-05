using TShockAPI;
using Microsoft.Xna.Framework;
using static ExtraDamage.ExtraDamage;

namespace ExtraDamage;

public class Command
{
    public static void CMD(CommandArgs args)
    {
        var plr = args.Player;
        if (args.Parameters.Count == 0)
        {
            plr.SendInfoMessage("指令菜单\n" +
                                "/ed 额外伤害 冷却秒数 ——管理权限\n" +
                                "/ed 255 255 255 ——设置伤害气泡颜色");
            return;
        }

        if (plr.HasPermission("ExtraDamage.admin"))
        {
            if (args.Parameters.Count == 2)
            {
                if (double.TryParse(args.Parameters[0], out var damage) && int.TryParse(args.Parameters[1], out var cooldown))
                {
                    ExtraDamage.Config.ExtraDamage = damage;
                    ExtraDamage.Config.Cooldown = cooldown;
                    ExtraDamage.Config.Write();

                    args.Player.SendSuccessMessage($"额外伤害设置为{damage}%，冷却时间设置为{cooldown}秒");
                    return;
                }
                else
                {
                    args.Player.SendErrorMessage("参数无效。用法：/ed 百分比 冷却");
                }
            }
        }
        else
        {
            args.Player.SendErrorMessage("格式错误,正确格式为:/ed 255 255 255");
        }

        if (args.Parameters.Count >= 3)
        {
            if (byte.TryParse(args.Parameters[0], out var r) &&
                byte.TryParse(args.Parameters[1], out var g) &&
                byte.TryParse(args.Parameters[2], out var b))
            {
                color[plr.Name] = new Color(r, g, b);
                args.Player.SendSuccessMessage($"您的气泡颜色已设置为: {r}, {g}, {b}\n" +
                                               $"ps:如果没生效或没气泡弹出请再输一次");
                return;
            }
        }
    }
}
