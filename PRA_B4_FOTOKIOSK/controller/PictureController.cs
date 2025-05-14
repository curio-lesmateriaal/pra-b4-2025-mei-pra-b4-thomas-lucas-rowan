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
            int today = (int)now.DayOfWeek;


            // Initializeer de lijst met fotos
            PicturesToDisplay.Clear();

            // WAARSCHUWING. ZONDER FILTER LAADT DIT ALLES!
            // foreach is een for-loop die door een array loopt
            foreach (string dir in Directory.GetDirectories(@"../../../fotos"))
            {
                /**
                 * dir string is de map waar de fotos in staan. Bijvoorbeeld:
                 * \fotos\0_Zondag
                 */

                string folderName = Path.GetFileName(dir);

                if (folderName.StartsWith(((int)DateTime.Now.DayOfWeek).ToString() + "_"))
                {
                    foreach (string file in Directory.GetFiles(dir))
                    {
                        string name = Path.GetFileName(file);
                        string[] id = name.Split('_');

                        if (id.Length >= 3 &&
                            int.TryParse(id[0], out int uur) &&
                            int.TryParse(id[1], out int minuut) &&
                            int.TryParse(id[2], out int seconde)
                            )
                        {
                            DateTime fotoTime = new DateTime(now.Year, now.Month, now.Day, uur, minuut, seconde);
                            TimeSpan difference = now - fotoTime;

                            if (difference.TotalMinutes >= 2 && difference.TotalMinutes <= 30)
                            {
                                PicturesToDisplay.Add(new KioskPhoto() { Id = 0, Source = file });

                            }

                        }


                        /**
                         * file string is de file van de foto. Bijvoorbeeld:
                         * \fotos\0_Zondag\10_05_30_id8824.jpg
                         */


                        //PicturesToDisplay.Add(new KioskPhoto() { Id = 0, Source = file });
                    }
                }
        

            }

            // Update de fotos
            PictureManager.UpdatePictures(PicturesToDisplay);
        }

        // Wordt uitgevoerd wanneer er op de Refresh knop is geklikt
        public void RefreshButtonClick()
        {
            Start();
        }

    }
}
