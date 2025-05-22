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


    public class ShopController
    {

        public static Home Window { get; set; }

        public void Start()
        {
            // Voeg producten met beschrijving toe
            ShopManager.Products.Add(new KioskProduct() { Name = "Foto 10x15", Price = 2.55, Description = "Glanzende fotoafdruk 10x15 cm" });
            ShopManager.Products.Add(new KioskProduct() { Name = "Foto 15x20", Price = 4.00, Description = "Glanzende fotoafdruk 15x20 cm" });
            ShopManager.Products.Add(new KioskProduct() { Name = "Sleutelhanger", Price = 7.00, Description = "Sleutelhanger met eigen foto" });
            ShopManager.Products.Add(new KioskProduct() { Name = "Mok", Price = 9.33, Description = "Koffiemok met foto" });
            ShopManager.Products.Add(new KioskProduct() { Name = "T-shirt", Price = 12.69, Description = "T-shirt met gepersonaliseerde opdruk" });

            // Bouw prijslijst dynamisch op
            foreach (KioskProduct product in ShopManager.Products)
            {
                ShopManager.AddShopPriceList(product.Name + ": €" + product.Price + " " + product.Description + "\n");
            }


            // Stel prijslijst in aan de rechterkant

            // Stel de bon in onderaan het scherm
            ShopManager.SetShopReceipt("Eindbedrag\n€");

            // Update dropdown met producten
            ShopManager.UpdateDropDownProducts();


        }

        public class OrderedProduct
        {
            public string id { get; set; }
            public string productName { get; set; }
            public int amount { get; set; }
            public double totalPrice { get; set; }

        }

        private List<OrderedProduct> orderedProducts = new List<OrderedProduct>();

        // Wordt uitgevoerd wanneer er op de Toevoegen knop is geklikt
        public void AddButtonClick()
        {
            KioskProduct product = ShopManager.GetSelectedProduct();
            int? id = ShopManager.GetFotoId();
            int? amount = ShopManager.GetAmount();
            if (amount == null || id == null || product == null)
            {
                //verkeerde input
                return;
            }

            OrderedProduct newProduct = new OrderedProduct
            {
                id = id.ToString(),
                productName = product.Name,
                amount = amount.Value,
                totalPrice = product.Price * amount.Value
            };

            orderedProducts.Add(newProduct);

            // Genereer kassabon
            double totaalBedrag = orderedProducts.Sum(product => product.totalPrice);
            StringBuilder receiptBuilder = new StringBuilder();
            receiptBuilder.AppendLine($"EindBedrag\n€ {totaalBedrag:F2}\n");

            foreach (var endProduct in orderedProducts)
            {
                receiptBuilder.AppendLine($"{endProduct.productName} (x{endProduct.amount}) - € {endProduct.totalPrice:F2}");
            }

            ShopManager.SetShopReceipt(receiptBuilder.ToString());

        }




        // Wordt uitgevoerd wanneer er op de Resetten knop is geklikt
        public void ResetButtonClick()
        {
        }

        // Wordt uitgevoerd wanneer er op de Save knop is geklikt
        public void SaveButtonClick()
        {
            // Haal de inhoud van de bon op
            string receipt = ShopManager.GetShopReceipt();

            // Huidige map van het programma
            string currentFolder = AppDomain.CurrentDomain.BaseDirectory;

            // Submap "Bonnen" binnen de huidige folder
            string subFolderName = "Bonnen";
            string folderPath = Path.Combine(currentFolder, subFolderName);

            // Zorg dat de map bestaat (maakt aan als hij er nog niet is)
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Unieke bestandsnaam op basis van datum/tijd
            string fileName = $"bon_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
            string fullPath = Path.Combine(folderPath, fileName);

            try
            {
                // Schrijf de bon naar een tekstbestand
                File.WriteAllText(fullPath, receipt);

                // Toon bevestiging
                System.Windows.MessageBox.Show($"Bon succesvol opgeslagen:\n{fullPath}");
            }
            catch (Exception ex)
            {
                // Toon foutmelding
                System.Windows.MessageBox.Show("Fout bij opslaan van de bon: " + ex.Message);
            }
        }

    }
}
