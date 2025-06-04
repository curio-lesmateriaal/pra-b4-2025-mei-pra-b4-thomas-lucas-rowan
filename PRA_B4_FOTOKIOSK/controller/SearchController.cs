using PRA_B4_FOTOKIOSK.magie;
using PRA_B4_FOTOKIOSK.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PRA_B4_FOTOKIOSK.controller
{
    public class SearchController
    {
        // De window die we laten zien op het scherm
        public static Home Window { get; set; }

        // Start methode die wordt aangeroepen wanneer de zoekpagina opent
        public void Start()
        {

        }
        //10_08_45_id7700
        //10:08:45
        // Wordt uitgevoerd wanneer er op de Zoeken-knop is geklikt
        public void SearchButtonClick()
        {
            var now = DateTime.Now;
            int today = (int)now.DayOfWeek;

            string time = SearchManager.GetSearchInput();

            if (string.IsNullOrWhiteSpace(time))
            {
                MessageBox.Show("Voer een tijd in.");
                return;
            }

            string[] timeParts = time.Split(':');
            if (timeParts.Length != 3 ||
                !int.TryParse(timeParts[0], out int iHour) ||
                !int.TryParse(timeParts[1], out int iMinute) ||
                !int.TryParse(timeParts[2], out int iSecond))
            {
                MessageBox.Show("Ongeldige invoer. Voer een geldige tijd in als hh:mm:ss.");
                return;
            }

            bool isFound = false;

            foreach (string dir in Directory.GetDirectories(@"../../../fotos"))
            {
                string folderName = Path.GetFileName(dir);
                if (!folderName.StartsWith(today.ToString() + "_")) continue;

                foreach (string file in Directory.GetFiles(dir))
                {
                    string fileName = Path.GetFileName(file);
                    string[] fileParts = fileName.Split('_');

                    if (fileParts.Length > 3 &&
                        int.TryParse(fileParts[0], out int hour) &&
                        int.TryParse(fileParts[1], out int minute) &&
                        int.TryParse(fileParts[2], out int second))
                    {
                        if (hour == iHour && minute == iMinute && second == iSecond)
                        {
                            SearchManager.SetPicture(file);
                            isFound = true;

                            string displayHour = hour.ToString("D2");
                            string displayMinute = minute.ToString("D2");
                            string displaySecond = second.ToString("D2");
                            string date = now.ToString("dd-MM-yyyy");

                            // Haal ID uit het vierde deel van de bestandsnaam, bv. id123456.jpg → 123456
                            string idRaw = fileParts[3]; // bv. id123456.jpg
                            string id = idRaw.Substring(2, idRaw.Length - 6); // strip 'id' en '.jpg'

                            string infoText = $" ID: {id} | Tijd: {displayHour}:{displayMinute}:{displaySecond} | Datum: {date}";
                            SearchManager.SetSearchImageInfo(infoText);

                            return;
                        }
                    }
                }
            }

            if (!isFound)
            {
                MessageBox.Show("Geen foto gevonden op dit tijdstip.");
            }
        }
    }
}