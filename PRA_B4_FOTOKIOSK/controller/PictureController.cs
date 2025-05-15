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
            // Initializeer de lijst met fotos
            PicturesToDisplay.Clear();

            // Lees foto's in en filter op juiste dag + tijd
            var allPhotos = new List<KioskPhoto>();

            // WAARSCHUWING. ZONDER FILTER LAADT DIT ALLES!
            // foreach is een for-loop die door een array loopt
            foreach (string dir in Directory.GetDirectories(@"../../../fotos"))
            {
                string folderName = Path.GetFileName(dir);
                if (!folderName.StartsWith(((int)now.DayOfWeek).ToString() + "_"))
                    continue;

                foreach (string file in Directory.GetFiles(dir))
                {
                    DateTime? fotoTime = GetPhotoTime(file, now);
                    if (fotoTime.HasValue)
                    {
                        double minutenVerschil = (now - fotoTime.Value).TotalMinutes;
                        if (minutenVerschil >= 2 && minutenVerschil <= 30)
                        {
                            allPhotos.Add(new KioskPhoto() { Id = 0, Source = file });
                        }
                    }
                }
            }

            // Sorteer op tijd
            allPhotos = allPhotos.OrderBy(p => GetPhotoTime(p.Source, now)).ToList();
            
            // Koppel foto's die 60 seconden verschillen
            var ordered = new List<KioskPhoto>();
            var used = new HashSet<string>();

            //chatgpt Heeft dit voor de helft gemaakt
            for (int i = 0; i < allPhotos.Count; i++)
            {
                var foto1 = allPhotos[i];
                if (used.Contains(foto1.Source)) continue;

                ordered.Add(foto1);
                used.Add(foto1.Source);

                var tijd1 = GetPhotoTime(foto1.Source, now);

                //chatgpt Heeft dit voor de helft gemaakt
                for (int j = i + 1; j < allPhotos.Count; j++)
                {
                    var foto2 = allPhotos[j];
                    if (used.Contains(foto2.Source)) continue;

                    var tijd2 = GetPhotoTime(foto2.Source, now);
                    if ((tijd2 - tijd1)?.TotalSeconds == 60)
                    {
                        ordered.Add(foto2);
                        used.Add(foto2.Source);
                    }
                }
            }

            PicturesToDisplay = ordered;

            // Update de fotos
            PictureManager.UpdatePictures(PicturesToDisplay);
        }

        // Wordt uitgevoerd wanneer er op de Refresh knop is geklikt
        public void RefreshButtonClick()
        {
            Start();
        }

        // Combineer parse + tijdafleiding in compacte 
        private DateTime? GetPhotoTime(string file, DateTime now)
        {
            string[] parts = Path.GetFileNameWithoutExtension(file).Split('_');
            if (parts.Length >= 3 &&
                int.TryParse(parts[0], out int uur) &&
                int.TryParse(parts[1], out int minuut) &&
                int.TryParse(parts[2], out int seconde))
            {
                return new DateTime(now.Year, now.Month, now.Day, uur, minuut, seconde);
            }
            return null;
        }
    }
}

