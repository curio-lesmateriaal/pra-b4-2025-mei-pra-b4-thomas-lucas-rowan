using PRA_B4_FOTOKIOSK.magie;
using PRA_B4_FOTOKIOSK.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRA_B4_FOTOKIOSK.controller
{
    public class PictureController
    {

        // De window die we laten zien op het scherm
        public static Home Window { get; set; }

        // De lijst met fotos die we laten zien
        public List<KioskPhoto> PicturesToDisplay = new List<KioskPhoto>();

        // Start methode die wordt aangeroepen wanneer de foto pagina opent.
        public void Start()
        {
            var now = DateTime.Now;
            int currentDay = (int)now.DayOfWeek;

            // WAARSCHUWING. ZONDER FILTER LAADT DIT ALLES!
            // foreach is een for-loop die door een array loopt
            foreach (string dir in Directory.GetDirectories(@"../../../fotos"))
            {

                string folderName = Path.GetFileName(dir);
                if (!folderName.StartsWith(currentDay.ToString() + "_")) continue;


                foreach (string file in Directory.GetFiles(dir))
                {
                    // Splits pad in delen
                    string[] pathParts = file.Split("\\");

                    // Splits bestandsnaam in datumtijd en ID
                    string[] nameParts = pathParts[2].Split("_id");

                    // Zet datumtijd om naar DateTime
                    DateTime fileDate = DateTime.Parse(nameParts[0].Replace("_", ":"));

                    // Haal foto-ID uit bestandsnaam
                    int photoId = int.Parse(nameParts[1].Split(".")[0]);

                    //int fotoId = int.Parse(file.Split("_id")[1].Split(".")[0]);

                    if (fileDate >= now.AddMinutes(-30) && fileDate <= now.AddMinutes(-2)) 
                    {
                        
                        bool add = false;

                        foreach (KioskPhoto photo in PicturesToDisplay)
                        {
                            //Foto wordt DateTime
                            var fotoDate = DateTime.Parse(photo.Source.Split("\\")[2].Split("_id")[0].Replace("_", ":")); 
                            if (fotoDate.AddSeconds(60) != fileDate)
                                continue; 

                            int index = PicturesToDisplay.IndexOf(photo);
                            PicturesToDisplay.Insert(index, new KioskPhoto() { Id = photoId, Source = file });
                            add = true;
                            break;
                        }

                        if (!add)
                        {
                            PicturesToDisplay.Add(new KioskPhoto() { Id = photoId, Source = file });

                        }

                    }

                }



                // Update de fotos
                PictureManager.UpdatePictures(PicturesToDisplay);
            }
        }

        // Wordt uitgevoerd wanneer er op de Refresh knop is geklikt
        public void RefreshButtonClick()
        {
            Start();
        }

    }
}

