# ExtraDamage 打怪额外伤害插件

- 作者: 哨兵、羽学
- 出处: Tshock官方群816771079
- 这是一个Tshock服务器插件，主要用于：
- 玩家在冷却时间后攻击怪物可造成额外伤害并在怪物头顶显示额外伤害值

## 更新日志

```
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
| /ed 伤害 冷却秒数 | 无 |   ExtraDamage.admin    |    修改统一的额外伤害与冷却时间   |
| /reload  | 无 |   tshock.cfg.reload    |    重载配置文件    |

## 配置

```json
{
  "插件开关": true,
  "额外伤害": 0.1,
  "冷却时间": 1,
  "默认颜色": {
    "R": 255,
    "G": 255,
    "B": 255
  },
  "低于生命不增伤": 10,
  "额外弹幕": [
    {
      "弹幕ID": 118,
      "伤害": 10,
      "数量": 5,
      "速度": 10.0,
      "角度": 25.0,
      "弹幕AI": {
        "0":1.0,
        "1":1.0,
        "2":1.0
      },
      "持续帧数": 180
    },
    {
      "弹幕ID": 173,
      "伤害": 10,
      "数量": 30,
      "速度": 20.0,
      "角度": -360.0,
      "弹幕AI": {},
      "持续帧数": 120
    }
  ],
  "免疫额外伤害NPC表": [
    17,
    18,
    19,
    20,
    38,
    105,
    106,
    107,
    108,
    160,
    123,
    124,
    142,
    207,
    208,
    227,
    228,
    229,
    353,
    354,
    376,
    441,
    453,
    550,
    579,
    588,
    589,
    633,
    663,
    678,
    679,
    680,
    681,
    682,
    683,
    684,
    685,
    686,
    687,
    375,
    442,
    443,
    539,
    444,
    445,
    446,
    447,
    448,
    605,
    627,
    601,
    613
  ]
}
```
## 反馈
- 优先发issued -> 共同维护的插件库：https://github.com/Controllerdestiny/TShockPlugin
- 次优先：TShock官方群：816771079
- 大概率看不到但是也可以：国内社区trhub.cn ，bbstr.net , tr.monika.love