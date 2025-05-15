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
            // Stel de prijslijst in aan de rechter kant.
            ShopManager.SetShopPriceList("Prijzen:\nFoto 10x15: €2.55\nFoto 15x20: €4,00\nSleutelhanger: €7,00\nMok: €9,33\nT-shirt: €12,69");

            // Stel de bon in onderaan het scherm
            ShopManager.SetShopReceipt("Eindbedrag\n€");

            // Vul de productlijst met producten
            ShopManager.Products.Add(new KioskProduct() { Name = "Foto 10x15", Price =  2.55 });
            ShopManager.Products.Add(new KioskProduct() { Name = "Foto 15x20", Price = 4.00 });
            ShopManager.Products.Add(new KioskProduct() { Name = "Sleutelhanger", Price = 7.00 });
            ShopManager.Products.Add(new KioskProduct() { Name = "Mok", Price = 9.33 });
            ShopManager.Products.Add(new KioskProduct() { Name = "T-shirt", Price = 12.69  });

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
