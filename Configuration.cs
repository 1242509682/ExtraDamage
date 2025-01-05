using Newtonsoft.Json;
using TShockAPI;
using Microsoft.Xna.Framework;
using Newtonsoft.Json.Linq;

namespace ExtraDamage;

internal class Configuration
{
    #region 实例变量
    [JsonProperty("插件开关", Order = 0)]
    public bool Enabled { get; set; } = true;

    [JsonProperty("额外伤害", Order = 1)]
    public double ExtraDamage { get; set; } = 0.1;

    [JsonProperty("冷却时间", Order = 2)]
    public int Cooldown { get; set; } = 1;

    [JsonConverter(typeof(ColorJsonConverter))]
    [JsonProperty("默认颜色", Order = 3)]
    public Color ColorRGB { get; set; } = new Color(255, 255, 255); // 预设颜色为白色

    [JsonProperty("低于生命不增伤", Order = 4)]
    public int Life { get; set; } = 10;

    [JsonProperty("额外弹幕", Order = 5)]
    public List<ProjData> ExtraProj { get; set; } = new List<ProjData>();

    [JsonProperty("免疫额外伤害NPC表", Order = 6)]
    public List<int> NPClist { get; set; } = new List<int>();
    #endregion

    #region 预设配置参数方法
    private void Ints()
    {
        ColorRGB = new Color(255, 255, 255);

        ExtraProj = new List<ProjData>()
        {
            new ProjData()
            {
                ID = 118,
                Damage = 10,
                Count = 5,
                Velocity = 10f,
                Angle = 25f,
                life = 180,
            },

            new ProjData()
            {
                ID = 173,
                Damage = 10,
                Count = 30,
                Velocity = 20f,
                Angle = -360f,
                life = 120,
            }
        };

        //预设这些NPCID都是城镇NPC
        NPClist = new List<int>
        {
            17,18,19,20,38,
            105,106,107,108,
            160,123,124,142,
            207,208,227,228,
            229,353,354,376,
            441,453,550,579,
            588,589,633,663,
            678,679,680,681,
            682,683,684,685,
            686,687,375,442,
            443,539,444,445,
            446,447,448,605,
            627,601,613
        };
    } 
    #endregion

    #region 读取与创建配置文件方法
    public static readonly string FilePath = Path.Combine(TShock.SavePath, "打怪额外伤害.json");
    public void Write()
    {
        string json = JsonConvert.SerializeObject(this, Formatting.Indented);
        File.WriteAllText(FilePath, json);
    }
    public static Configuration Read()
    {
        if (!File.Exists(FilePath))
        {
            var NewConfig = new Configuration();
            NewConfig.Ints();
            new Configuration().Write();
            return NewConfig;
        }
        else
        {
            string jsonContent = File.ReadAllText(FilePath);
            return JsonConvert.DeserializeObject<Configuration>(jsonContent)!;
        }
    }
    #endregion

    #region 用于反序列化的JsonConverter 使Config看得简洁(转换器
    internal class ColorJsonConverter : JsonConverter<Color>
    {
        public override void WriteJson(JsonWriter writer, Color value, JsonSerializer serializer)
        {
            var colorObject = new JObject
            {
                ["R"] = value.R,
                ["G"] = value.G,
                ["B"] = value.B,
            };

            colorObject.WriteTo(writer);
        }

        public override Color ReadJson(JsonReader reader, Type objectType, Color existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var colorObject = JObject.Load(reader);

            var r = colorObject["R"]!.Value<byte>();
            var g = colorObject["G"]!.Value<byte>();
            var b = colorObject["B"]!.Value<byte>();

            return new Color(r, g, b);
        }
    }
    #endregion
}