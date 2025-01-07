using ExtraDamage.Progress;
using Newtonsoft.Json;

namespace ExtraDamage;

public class ProjData
{
    [JsonProperty("弹幕ID", Order = 0)]
    public int ID = 0;
    [JsonProperty("伤害", Order = 1)]
    public int damage = 20;
    [JsonProperty("数量", Order = 2)]
    public int Count = 5;
    [JsonProperty("速度", Order = 3)]
    public float Velocity = 10f;
    [JsonProperty("半径", Order = 4)]
    public float Radius = 10f;
    [JsonProperty("偏移", Order = 5)]
    public float Angle = 15f;
    [JsonProperty("旋转", Order = 6)]
    public float Rotate = 5f;
    [JsonProperty("AI", Order = 7)]
    public Dictionary<int, float> ai { get; set; } = new Dictionary<int, float>();
    [JsonProperty("生命", Order = 8)]
    public int life = 60;
    [JsonProperty("进度限制", Order = 9)]
    public ProgressType isProgress { get; set; } = ProgressType.None;
    [JsonProperty("以玩家为中心", Order = 10)]
    public bool TarCenter = true;
}