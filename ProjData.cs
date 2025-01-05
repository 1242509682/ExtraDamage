using Newtonsoft.Json;

namespace ExtraDamage;

public class ProjData
{
    [JsonProperty("弹幕ID", Order = 0)]
    public int ID = 0;
    [JsonProperty("伤害", Order = 1)]
    public int Damage = 10;
    [JsonProperty("数量", Order = 2)]
    public int Count = 5;
    [JsonProperty("速度", Order = 3)]
    public float Velocity = 10f;
    [JsonProperty("角度", Order = 4)]
    public float Angle = 15f;
    [JsonProperty("弹幕AI", Order = 5)]
    public Dictionary<int, float> ai { get; set; } = new Dictionary<int, float>();
    [JsonProperty("持续帧数", Order = 6)]
    public int life = 60;
}