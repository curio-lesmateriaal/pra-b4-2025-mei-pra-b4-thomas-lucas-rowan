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
    public class KioskProduct
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; } // bewerking van de klas met naam prijs enz
    }

    public class ShopController
    {

        public static List<KioskProduct> Products = new List<KioskProduct>();

        public static Home Window { get; set; }

        public void Start()
        {
            // Voeg producten met beschrijving toe
            Products.Add(new KioskProduct() { Name = "Foto 10x15", Price = 2.55, Description = "Glanzende fotoafdruk 10x15 cm" });
            Products.Add(new KioskProduct() { Name = "Foto 15x20", Price = 4.00, Description = "Glanzende fotoafdruk 15x20 cm" });
            Products.Add(new KioskProduct() { Name = "Sleutelhanger", Price = 7.00, Description = "Sleutelhanger met eigen foto" });
            Products.Add(new KioskProduct() { Name = "Mok", Price = 9.33, Description = "Koffiemok met foto" });
            Products.Add(new KioskProduct() { Name = "T-shirt", Price = 12.69, Description = "T-shirt met gepersonaliseerde opdruk" });

            // Bouw prijslijst dynamisch op
            StringBuilder priceList = new StringBuilder();
            priceList.AppendLine("Prijzen:");
            foreach (KioskProduct product in Products)
            {
                priceList.AppendLine($"{product.Name}: €{product.Price} - {product.Description}");
            }

            // Stel prijslijst in aan de rechterkant
            ShopManager.SetShopPriceList(priceList.ToString());

            // Stel de bon in onderaan het scherm
            ShopManager.SetShopReceipt("Eindbedrag\n€");

            // Update dropdown met producten
            ShopManager.UpdateDropDownProducts();


        }


        // Wordt uitgevoerd wanneer er op de Toevoegen knop is geklikt
        public void AddButtonClick()
        {
            
        }

        // Wordt uitgevoerd wanneer er op de Resetten knop is geklikt
        public void ResetButtonClick()
        {

        }

        // Wordt uitgevoerd wanneer er op de Save knop is geklikt
        public void SaveButtonClick()
        {
        }

    }
}
