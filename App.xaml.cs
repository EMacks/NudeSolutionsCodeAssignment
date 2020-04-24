using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace NSApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
           // Init();
        }

        private void Init()
        {
            var l = new List<Product>()
            {
                new Product(new Guid("b3b8dc14-c1c0-4ae7-aa46-e5d2b6705ef6"),"Electronics", "TV", 2000),
                new Product(new Guid("61769512-02ef-48d8-8268-bec70fa94e18"),"Electronics", "Playstation", 400),
                new Product(new Guid("9ead2a03-1e92-4079-a577-fe1e4096fa2e"),"Electronics", "Stereo", 1600),
                new Product(new Guid("e6b48fc0-98b8-4432-b74c-b885ac49bcbc"),"Clothing", "Shirts", 1100),
                new Product(new Guid("67981ccd-ed21-4e0c-aa86-77b575d1b41c"),"Clothing", "Jeans", 1100),
                new Product(new Guid("4cdd4ca6-6d5b-4131-a1b6-36a00fdf13a6"),"Kitchen", "Pots and Pans", 3000),
                new Product(new Guid("c671b528-e988-4b09-bdad-bc85653fa62f"),"Kitchen", "Flatware", 500),
                new Product(new Guid("15112a63-97a0-480d-9dc2-41adcf16e11b"),"Kitchen", "Knife Set", 500),
                new Product(new Guid("fcb83244-abf7-439a-b471-c5fe86af9ebc"),"Kitchen", "Misc", 1000)
            };
            foreach(var i in l)
            {
                i.Save();
            }
        }
    }
    
    
}
