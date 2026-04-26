using System;
using System.IO;
using System.Text.Json;

public class CovidConfig
{
    private readonly string configFilePath = "covid_config.json";

    public string satuan_suhu { get; set; } = "celcius";
    public int batas_hari_deman { get; set; } = 14;
    public string pesan_ditolak { get; set; } = "Anda tidak diperbolehkan masuk ke dalam gedung ini";
    public string pesan_diterima { get; set; } = "Anda dipersilahkan untuk masuk ke dalam gedung ini";

    public CovidConfig()
    {
        LoadConfig();
    }

    public void LoadConfig()
    {
        if (File.Exists(configFilePath))
        {
            string json = File.ReadAllText(configFilePath);
            ConfigData? config = JsonSerializer.Deserialize<ConfigData>(json);

            if (config != null)
            {
                satuan_suhu = config.satuan_suhu;
                batas_hari_deman = config.batas_hari_deman;
                pesan_ditolak = config.pesan_ditolak;
                pesan_diterima = config.pesan_diterima;
            }
        }
        else
        {
            WriteNewConfig();
        }
    }

    public void WriteNewConfig()
    {
        ConfigData config = new ConfigData
        {
            satuan_suhu = satuan_suhu,
            batas_hari_deman = batas_hari_deman,
            pesan_ditolak = pesan_ditolak,
            pesan_diterima = pesan_diterima
        };

        JsonSerializerOptions options = new JsonSerializerOptions()
        {
            WriteIndented = true
        };

        string json = JsonSerializer.Serialize(config, options);
        File.WriteAllText(configFilePath, json);
    }

    public void UbahSatuan()
    {
        if (satuan_suhu.ToLower() == "celcius")
        {
            satuan_suhu = "fahrenheit";
        }
        else
        {
            satuan_suhu = "celcius";
        }

        WriteNewConfig();
    }

    private class ConfigData
    {
        public string satuan_suhu { get; set; } = "celcius";
        public int batas_hari_deman { get; set; } = 14;
        public string pesan_ditolak { get; set; } = "Anda tidak diperbolehkan masuk ke dalam gedung ini";
        public string pesan_diterima { get; set; } = "Anda dipersilahkan untuk masuk ke dalam gedung ini";
    }
}