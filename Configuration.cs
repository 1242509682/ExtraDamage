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

    [JsonProperty("冷却时间", Order = 1)]
    public double Cooldown { get; set; } = 5.01;
    [JsonProperty("额外伤害", Order = 2)]
    public double ExtraDamage { get; set; } = 0.5;
    [JsonProperty("怪物低于生命不增伤", Order = 3)]
    public double Life { get; set; } = 0.1;

    [JsonProperty("启用范围伤害", Order = 4)]
    public bool RangeDamage { get; set; } = true;
    [JsonProperty("最大范围格数", Order = 5)]
    public int MaxRange { get; set; } = 10;

    [JsonConverter(typeof(ColorJsonConverter))]
    [JsonProperty("默认颜色", Order = 6)]
    public Color ColorRGB { get; set; } = new Color(255, 255, 255); // 预设颜色为白色

    [JsonProperty("启用额外弹幕", Order = 7)]
    public bool ProjEnabled { get; set; } = true;
    [JsonProperty("弹幕最低伤害", Order = 8)]
    public int projDamage { get; set; } = 10;
    [JsonProperty("额外弹幕", Order = 9)]
    public List<ProjData> ExtraProj { get; set; } = new List<ProjData>();

    [JsonProperty("免疫额外伤害NPC表", Order = 10)]
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
                TarCenter = true,
                ID = 118,
                damage = 40,
                Count = 5,
                Velocity = 10f,
                decay = 0.9f,
                CEC = 75f,
                Angle = 25f,
                Rotate = 0f,
                life = 180,
            },

            new ProjData()
            {
                TarCenter = false,
                ID = 173,
                damage = 20,
                Count = 30,
                Velocity = 20f,
                decay = 0.9f,
                CEC = -70f,
                Angle = -360f,
                Rotate = 5f,
                life = 120,
            },

            new ProjData()
            {
                TarCenter = false,
                ID = 931,
                damage = 10,
                Count = 10,
                Velocity = 10f,
                decay = 0.99f,
                CEC = 15,
                Angle = 7f,
                Rotate = -45f,
                life = 240,
            },

            new ProjData()
            {
                TarCenter = false,
                ID = 731,
                damage = 40,
                Count = 15,
                Velocity = 10f,
                decay = 0.99f,
                CEC = 0f,
                Angle = 15f,
                Rotate = 30f,
                ai = new Dictionary<int, float>
                {
                    {0, 100f},
                },
                life = 240,
            },

            new ProjData()
            {
                TarCenter = true,
                ID = 173,
                damage = 30,
                Count = 30,
                Velocity = 10f,
                decay = 0.7f,
                CEC = 0f,
                Angle = -360f,
                Rotate = 5f,
                life = 120,
            },

            new ProjData()
            {
                TarCenter = true,
                ID = 931,
                damage = 15,
                Count = 10,
                Velocity = 10f,
                decay = 0.99f,
                CEC = 0,
                Angle = 15f,
                Rotate = -5f,
                life = 240,
            },
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