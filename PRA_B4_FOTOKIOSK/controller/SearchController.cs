using PRA_B4_FOTOKIOSK.magie;
using PRA_B4_FOTOKIOSK.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PRA_B4_FOTOKIOSK.controller
{
    public class SearchController
    {
        // De window die we laten zien op het scherm
        public static Home Window { get; set; }


        // Start methode die wordt aangeroepen wanneer de zoek pagina opent.
        public void Start()
        {

        }

        // Wordt uitgevoerd wanneer er op de Zoeken knop is geklikt
        public void SearchButtonClick()
        {



            if (Window.weekOfDay.SelectedItem is not ComboBoxItem selectedWeekOfDay)
            {
                return;
            }

            string selectedWeekOfDayString = selectedWeekOfDay.Content.ToString();
            var now = DateTime.Now;
            int today = (int)now.DayOfWeek;


            // Initializeer de lijst met fotos
            if (selectedWeekOfDayString == "zondag")
            {
                today = 0;
            }
            else if (selectedWeekOfDayString == "maandag")
            {
                today = 1;
            }
            else if (selectedWeekOfDayString == "dinsdag")
            {
                today = 2;
            }
            else if (selectedWeekOfDayString == "woensdag")
            {
                today = 3;
            }
            else if (selectedWeekOfDayString == "donderdag")
            {
                today = 4;
            }
            else if (selectedWeekOfDayString == "vrijdag")
            {
                today = 5;
            }
            else if (selectedWeekOfDayString == "zaterdag")
            {
                today = 6;
            }

            foreach (string dir in Directory.GetDirectories(@"../../../fotos"))
            {
                string folderName = Path.GetFileName(dir);
                if (!folderName.StartsWith(today.ToString() + "_")) continue;

                // Haal alle afbeeldingsbestanden op uit de juiste map
                string[] imageFiles = Directory.GetFiles(dir, "*.*")
                    .Where(file => file.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                                   file.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                                   file.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
                    .ToArray();

                foreach (string imagePath in imageFiles)
                {
                    // Hier kun je iets doen met het pad van het beeld
                    Console.WriteLine($"Gevonden foto: {imagePath}");

                    // Als je metadata wil uitlezen:
                    // var image = new FileInfo(imagePath);
                    // var created = image.CreationTime;
                }
            }

        }


    }
}

    

