using System;
using System.Globalization;

class Program
{
    static void Main(string[] args)
    {
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

        CovidConfig config = new CovidConfig();

        Console.WriteLine("Satuan suhu saat ini: " + config.satuan_suhu);
        Console.WriteLine("Apakah ingin mengubah satuan suhu? y/n");
        string pilihan = Console.ReadLine();

        if (pilihan.ToLower() == "y")
        {
            config.UbahSatuan();
            Console.WriteLine("Satuan suhu berhasil diubah menjadi: " + config.satuan_suhu);
        }

        Console.WriteLine();
        Console.WriteLine("Berapa suhu badan anda saat ini? Dalam nilai " + config.satuan_suhu);
        double suhu = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Berapa hari yang lalu (perkiraan) anda terakhir memiliki gejala demam?");
        int hariDemam = Convert.ToInt32(Console.ReadLine());

        bool suhuNormal;

        if (config.satuan_suhu.ToLower() == "celcius")
        {
            suhuNormal = suhu >= 36.5 && suhu <= 37.5;
        }
        else
        {
            suhuNormal = suhu >= 97.7 && suhu <= 99.5;
        }

        bool hariDemamNormal = hariDemam < config.batas_hari_deman;

        if (suhuNormal && hariDemamNormal)
        {
            Console.WriteLine(config.pesan_diterima);
        }
        else
        {
            Console.WriteLine(config.pesan_ditolak);
        }
    }
}