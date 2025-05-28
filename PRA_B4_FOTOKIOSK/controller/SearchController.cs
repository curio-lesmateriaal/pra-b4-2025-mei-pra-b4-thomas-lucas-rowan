using PRA_B4_FOTOKIOSK.magie;
using PRA_B4_FOTOKIOSK.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static System.Net.Mime.MediaTypeNames;

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

            var now = DateTime.Now;
            int today = (int)now.DayOfWeek;

            //if (Window.weekOfDay.SelectedItem is not ComboBoxItem selectedWeekOfDay)
            //{
            //    return;
            //}
            string time = SearchManager.GetSearchInput();
            //string time = Window.tbZoeken.Text;
            //Window.lbSearchInfo.Content = text;

            //var fotoDate = DateTime.Parse(photo.Source.Split("\\")[2].Split("_id")[0].Replace("_", ":"));

            string[] timeParts = time.Split(':');
            if (int.TryParse(timeParts[0], out int iHour) &&
                int.TryParse(timeParts[1], out int iMinute) &&
                int.TryParse(timeParts[2], out int iSecond))
            {
                if (timeParts == null || timeParts.Length != 3)
                {
                    MessageBox.Show("Geen geldige invoer");
                    return;
                }

                bool isFound = false;


                //string searchTerm = SearchManager.GetSearchInput()?.ToLower() ?? "";

                foreach (string dir in Directory.GetDirectories(@"../../../fotos"))
                {
                    string folderName = Path.GetFileName(dir);
                    if (!folderName.StartsWith(today.ToString() + "_")) continue;

                    foreach (string file in Directory.GetFiles(dir))
                    {
                        string fileName = Path.GetFileName(file);
                        string[] fileParts = fileName.Split('_');

                        if (fileParts.Length > 3)
                        {
                            if (int.TryParse(fileParts[0], out int hour) &&
                                int.TryParse(fileParts[1], out int minute) &&
                                int.TryParse(fileParts[2], out int second))
                            {
                                if (hour == iHour && minute == iMinute && second == iSecond)
                                {
                                    SearchManager.SetPicture(file);
                                    isFound = true;

                                    string displaySecond = second.ToString();
                                    string displayMinute = minute.ToString();

                                    if (minute.ToString().Length == 1)
                                    {
                                        displayMinute = "0" + minute;

                                    }

                                    if (second.ToString().Length == 1)
                                    {
                                        displaySecond = "0" + second;
                                    }

                                    // Haal alle afbeeldingsbestanden op uit de juiste map

                                    string text = "Foto gevonden:  - " + hour + ":" + displayMinute + ":" + displaySecond + " met id: " + fileParts[3].Substring(2, fileParts[3].Length - 6);
                                    //SearchManager.SetSearchImageInfo(text);
                                    Window.lbSearchInfo.Content = text;

                                }

                            }


                        }
                    }
                }
            }
        }
    }
}







