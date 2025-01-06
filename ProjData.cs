using Newtonsoft.Json;

namespace ExtraDamage;

public class ProjData
{
    [JsonProperty("以玩家为中心", Order = 0)]
    public bool TarCenter = true;
    [JsonProperty("弹幕ID", Order = 0)]
    public int ID = 0;
    [JsonProperty("伤害", Order = 1)]
    public int damage = 20;
    [JsonProperty("数量", Order = 2)]
    public int Count = 5;
    [JsonProperty("速度", Order = 3)]
    public float Velocity = 10f;
    [JsonProperty("衰减比例", Order = 4)]
    public float decay = 0.9f;
    [JsonProperty("中心扩缩", Order = 5)]
    public float CEC = 10f;
    [JsonProperty("偏移角度", Order = 6)]
    public float Angle = 15f;
    [JsonProperty("旋转角度", Order = 7)]
    public float Rotate = 5f;
    [JsonProperty("弹幕AI", Order = 8)]
    public Dictionary<int, float> ai { get; set; } = new Dictionary<int, float>();
    [JsonProperty("持续帧数", Order = 9)]
    public int life = 60;
}