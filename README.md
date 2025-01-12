# ExtraDamage 打怪额外伤害插件

- 作者: 哨兵、羽学
- 出处: Tshock官方群816771079
- 这是一个Tshock服务器插件，主要用于：
- 玩家在冷却时间后攻击怪物可造成额外伤害并在怪物头顶显示额外伤害值

<details>
  <summary>进度限制表</summary>

```事件与进度```
- None (无) - 0
- EyeOfCthulhu (克眼, 克苏鲁之眼) - 1
- SlimeKing (史莱姆王, 史莱姆之王, 史王) - 2
- EvilBoss (克脑, 克苏鲁之脑, 世界吞噬者, 世界吞噬怪, 世吞) - 3
- Skeletron (骷髅王) - 4
- QueenBee (蜂王) - 5
- Deerclops (鹿角怪, 独眼巨鹿) - 6
- WallOfFlesh (血肉墙, 肉山, 肉后, 困难模式) - 7
- MechBossAny (一王后) - 8
- TheTwins (双子魔眼) - 9
- TheDestroyer (毁灭者, 铁长直) - 10
- SkeletronPrime (机械骷髅王) - 11
- Plantera (世纪之花, 世花) - 12
- Golem (石巨人) - 13
- DukeFishron (猪鲨, 猪龙鱼公爵) - 14
- LunaticCultist (拜月教邪教徒, 拜月) - 15
- Moonlord (月球领主, 月亮领主, 月总) - 16
- EmpressOfLight (光之女皇, 光女) - 17
- QieenSlime (史莱姆皇后, 史后) - 18
- HalloweenTree (哀木) - 19
- HalloweenKing (南瓜王) - 20
- ChristmasTree (长绿尖叫怪) - 21
- ChristmasIceQueen (冰雪女皇) - 22
- ChristmasSantank (圣诞坦克) - 23
- Martians (火星飞碟, 飞碟) - 24
- Clown (小丑) - 25
- TowerSolar (日耀柱) - 26
- TowerVortex (星旋柱) - 27
- TowerNebula (星云柱) - 28
- TowerStardust (星尘柱) - 29
- Goblins (哥布林入侵, 哥布林) - 30
- Pirates (海盗入侵, 海盗) - 31
- Frost (霜月) - 32
- BloodMoon (血月) - 33
- DrakMageT1 (旧日一, 黑暗法师) - 34
- OrgeT2 (旧日二, 巨魔) - 35
- BetsyT3 (旧日三, 贝蒂斯, 双足翼龙) - 36
- Raining (雨天) - 37
- DyaTime (白天) - 38
- Night (夜晚) - 39
- WindyDay (大风天) - 40
- Halloween (万圣节) - 41
- Party (派对) - 42

```世界种子```
- DrunkWorld (2020, 醉酒世界) - 43
- tenthAnniversaryWorld (2021, 十周年) - 44
- ForTheWorthy (ftw) - 45
- RemixWorld (混乱世界, 颠倒世界) - 46
- NotTheBeesWorld (ntb, 蜂蜜世界) - 47
- DontStarveWorld (永恒领域, 饥荒) - 48
- zenithWorld (天顶世界, 天顶) - 49
- NoTrapsWorld (陷阱世界, 陷阱) - 50

```月相```
- FullMoon (满月) - 51
- WaningGibbous (亏凸月) - 52
- ThirdQuarter (下弦月) - 53
- WaningCrescen (残月) - 54
- NewMoon (新月) - 55
- WaxingCrescent (娥眉月) - 56
- FirstQuarter (上弦月) - 57
- WaxingGibbous (盈凸月) - 58

</details>

## 更新日志

```
v1.0.6
【额外弹幕】加入了【角度修正】属性，该属性为bool类型
 自动根据玩家朝向修正偏移角度、旋转弧度
 加入了造成额外伤害时【回血回蓝】配置项，当数值非0时触发
 范围伤害的悬浮文字不再根据【玩家自定义颜色】决定，固定为:灰白色
 移除了所有悬浮文字的【小数点显示】

v1.0.5
修复了【额外弹幕】其中一个的【进度限制】不达标，导致其他弹幕无法生成的问题

v1.0.4
简化了【额外弹幕】属性名，
【中心扩缩】改名为【半径】
【持续帧数】改名为【生命】
移除了【衰减比例】
【额外弹幕】加入了【进度限制】

v1.0.3
[fix]：修复中心扩缩不准的BUG，修复额外弹幕杀死的NPC不掉落物品的BUG
额外弹幕不支持使用敌对弹幕（暂时没找到更好的写法先搁浅）
冷却时间可精确到小数点,如0.01秒
根据其他服主需求伤害机制改回了自定义
【额外弹幕】加入了【以玩家为中心】，关闭时则以怪物为中心点发射弹幕
加入了【衰减速度】根据弹幕数量递减最大值不超过1，数值越小减速越少
（人话：以【速度】与【数量】进行百分比衰减）
加入了【中心扩缩】，根据弹幕数量偏移，正数为扩，负数为缩
加入了【旋转角度】，根据弹幕数量偏移旋转度数

v1.0.2
 弹幕伤害改为由【弹幕最低伤害】到【玩家手上武器伤害】随机计算（当产生暴击时最小伤害可翻倍）
【额外伤害】可参与暴击计算，当受伤NPC被暴击时额外伤害*2
 加入了【范围伤害】，根据击中的NPC查找附近【最大范围格数】内的其他NPC造成【额外伤害】（并显示悬浮伤害值）

v1.0.1
【额外伤害】将0.1 到 1视为百分比
加入了额外弹幕功能可自定义：
弹幕ID、伤害、数量、角度、速度、弹幕AI、持续帧数
根据弹幕生成的数量：逐渐对弹幕速度进行递减、偏移角度的递增

v1.0.0
帮哨兵改善的打怪额外伤害插件
纠正不正确的变量名与方法重构
【额外伤害】为击中怪物时的伤害额外10%（指令只需要输入整数即可）
【冷却时间】单位为秒
【低于生命不增伤】为怪物低于10%生命时不会受到额外伤害(避免虚值把NPC直接从服务器移除不掉落物品)
【免疫额外伤害NPC表】默认配置里的ID为城镇NPC
```

## 指令

| 语法                             | 别名  |       权限       |                   说明                   |
| -------------------------------- | :---: | :--------------: | :--------------------------------------: |
| /ed 255 255 255 | 无 |   ExtraDamage.use    |    修改自己的气泡颜色    |
| /ed 额外伤害 冷却秒数 | 无 |   ExtraDamage.admin    |    修改统一的额外伤害与冷却时间   |
| /reload  | 无 |   tshock.cfg.reload    |    重载配置文件    |

## 配置

```json
{
  "插件开关": true,
  "冷却时间": 5.01,
  "额外伤害": 0.5,
  "回血": 20,
  "回蓝": 100,
  "怪物低于生命不增伤": 0.1,
  "启用范围伤害": true,
  "最大范围格数": 10,
  "默认颜色": {
    "R": 255,
    "G": 255,
    "B": 255
  },
  "启用额外弹幕": true,
  "额外弹幕": [
    {
      "弹幕ID": 118,
      "伤害": 40,
      "数量": 5,
      "速度": 10.0,
      "半径": 75.0,
      "偏移": 25.0,
      "旋转": 0.0,
      "角度修正": false,
      "AI": {},
      "生命": 180,
      "进度限制": 0,
      "以玩家为中心": true
    },
    {
      "弹幕ID": 173,
      "伤害": 20,
      "数量": 30,
      "速度": 20.0,
      "半径": 50.0,
      "偏移": -360.0,
      "旋转": 5.0,
      "角度修正": true,
      "AI": {},
      "生命": 120,
      "进度限制": 0,
      "以玩家为中心": false
    },
    {
      "弹幕ID": 931,
      "伤害": 10,
      "数量": 10,
      "速度": 10.0,
      "半径": 15.0,
      "偏移": 7.0,
      "旋转": -45.0,
      "角度修正": true,
      "AI": {},
      "生命": 240,
      "进度限制": 0,
      "以玩家为中心": false
    },
    {
      "弹幕ID": 731,
      "伤害": 40,
      "数量": 15,
      "速度": 10.0,
      "半径": 0.0,
      "偏移": 15.0,
      "旋转": 30.0,
      "角度修正": true,
      "AI": {
        "0": 100.0
      },
      "生命": 240,
      "进度限制": 0,
      "以玩家为中心": false
    },
    {
      "弹幕ID": 173,
      "伤害": 30,
      "数量": 30,
      "速度": 10.0,
      "半径": 0.0,
      "偏移": -360.0,
      "旋转": 5.0,
      "角度修正": true,
      "AI": {},
      "生命": 120,
      "进度限制": 0,
      "以玩家为中心": true
    },
    {
      "弹幕ID": 931,
      "伤害": 15,
      "数量": 10,
      "速度": 10.0,
      "半径": 0.0,
      "偏移": 15.0,
      "旋转": -5.0,
      "角度修正": true,
      "AI": {},
      "生命": 240,
      "进度限制": 0,
      "以玩家为中心": true
    }
  ],
  "免疫额外伤害NPC表": []
}
```
## 反馈
- 优先发issued -> 共同维护的插件库：https://github.com/Controllerdestiny/TShockPlugin
- 次优先：TShock官方群：816771079
- 大概率看不到但是也可以：国内社区trhub.cn ，bbstr.net , tr.monika.love