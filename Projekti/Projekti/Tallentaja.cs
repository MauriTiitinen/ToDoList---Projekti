using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Projekti.MainWindow;
using System.IO;

namespace Projekti
{
    internal static class VTallentaja
    {
        private static string tiedostonimi = "valmiit.json";
        public static bool Tallenna(ObservableCollection<Tehtävä> vlista)
        {
            try
            {
                var json_muoto = JsonSerializer.Serialize(vlista);
                File.WriteAllText(tiedostonimi, json_muoto);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static ObservableCollection<Tehtävä> LueValmiit()
        {
            try
            {
                var json_muoto = File.ReadAllText(tiedostonimi);
                var vlista = JsonSerializer.Deserialize<ObservableCollection<Tehtävä>>(json_muoto);
                if (vlista == null)
                    return new ObservableCollection<Tehtävä>();
                return vlista;
            }
            catch
            {
                return new ObservableCollection<Tehtävä>();
            }

        }
    }
    internal static class KTallentaja
    {
        private static string tiedostonimi = "kesken.json";
        public static bool Tallenna(ObservableCollection<Tehtävä> klista)
        {
            try
            {
                var json_muoto1 = JsonSerializer.Serialize(klista);
                File.WriteAllText(tiedostonimi, json_muoto1);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static ObservableCollection<Tehtävä> LueKesken()
        {
            try
            {
                var json_muoto1 = File.ReadAllText(tiedostonimi);
                var klista = JsonSerializer.Deserialize<ObservableCollection<Tehtävä>>(json_muoto1);
                if (klista == null)
                    return new ObservableCollection<Tehtävä>();
                return klista;
            }
            catch
            {
                return new ObservableCollection<Tehtävä>();
            }

        }
    }
}
        


