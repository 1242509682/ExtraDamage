using Microsoft.Xna.Framework;
using Terraria;


namespace ExtraDamage;

internal class MyProjectile
{
    #region 弹幕生成方法
    private static int index = 0;
    public static void SpawnProjectile(Terraria.DataStructures.IEntitySource Source, List<ProjData> data, NPC npc, float knockBack)
    {
        if (data == null || data.Count <= 0) return;
        var proj = data[index];

        // 获取距离和方向向量
        var tar = npc.GetTargetData(true);
        var dict = npc.Center - tar.Center;
        if (tar.Invalid || proj.ID <= 0) // 目标无效则跳过
        {
            Next(data);
            return;
        }

        // 弧度：定义总角度范围的一半（从中心线两侧各偏移） 
        var radian = proj.Angle * (float)Math.PI / 180;
        // 计算每次发射的弧度增量
        var addRadian = radian * 2 / (proj.Count - 1);

        // 初始化默认AI值
        var ai0 = proj.ai != null && proj.ai.ContainsKey(0) ? proj.ai[0] : 0f;
        var ai1 = proj.ai != null && proj.ai.ContainsKey(1) ? proj.ai[1] : 0f;
        var ai2 = proj.ai != null && proj.ai.ContainsKey(2) ? proj.ai[2] : 0f;

        //以“玩家为中心”为true 以玩家为中心,否则以被击中的npc为中心
        var pos = proj.TarCenter
                ? new Vector2(tar.Center.X, tar.Center.Y)
                : new Vector2(npc.Center.X, npc.Center.Y);

        // 根据弹幕数量属性发射多个弹幕，每次发射都进行速度衰减
        for (var i = 0; i < proj.Count; i++)
        {
            // 计算衰减值，随着弹幕数量的增加而减慢
            var decay = 1.0f - i / (float)proj.Count * proj.decay;

            // 应用发射速度
            var speed = proj.Velocity * decay;
            var vel = dict.SafeNormalize(Vector2.Zero) * speed;

            // 应用角度偏移
            var Angle = (i - (proj.Count - 1) / 2.0f) * addRadian;
            vel = vel.RotatedBy(Angle);

            // 如果旋转角度不为0，则设置旋转角度
            if (proj.Rotate != 0)
            {
                vel = vel.RotatedBy(Angle + proj.Rotate * i);
            }

            // 如果中心扩展不为0，则应用中心外扩或内缩
            var NewPos = pos;
            if (proj.CEC != 0)
            {
                // 计算相对于中心点的偏移量
                var ExAngle = i / (float)(proj.Count - 1) * MathHelper.TwoPi; // 均匀分布的角度
                var absEx = Math.Abs(proj.CEC); // 使用绝对值以确保正确的扩展距离
                var offset = new Vector2((float)Math.Cos(ExAngle), (float)Math.Sin(ExAngle)) * absEx;

                // 如果 CEC 是负数，则反向偏移量
                if (proj.CEC < 0)
                {
                    offset *= -1;
                }
                NewPos += offset;
            }

            //创建并发射弹幕
            var newProj = Projectile.NewProjectile(Source, NewPos.X, NewPos.Y,vel.X, vel.Y,
                                                   proj.ID, proj.damage, knockBack, 
                                                   Main.myPlayer, ai0, ai1, ai2);
            // 弹幕生命
            Main.projectile[newProj].timeLeft = proj.life > 0 ? proj.life : 0;
            if (proj.life == 0)
            {
                Main.projectile[newProj].Kill();
            }
        }

        Next(data); // 移动到下一个要发射的弹幕
    }
    #endregion

    #region 移动到下一个要发射的弹幕方法
    private static void Next(List<ProjData> data)
    {
        index++;
        if (index >= data.Count)
        {
            index = 0;
        }
    }
    #endregion

}
