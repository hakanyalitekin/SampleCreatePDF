using SampleCreatePDF.Managers;
using SampleCreatePDF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SampleCreatePDF.Controllers
{
    public class HomeController : ApiController
    {

        [HttpGet]
        [Route("api/Home/CreatePDF")]
        public HttpResponseMessage CreatePDF()
        {
            PdfManager _htmlToPdf = new PdfManager();
            /*
             Örnek HTML şablonumuza erişiyoruz.
             Gerçek hayatta veritabanından erişilir.
            */
            string temp = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "PDF_Template.html");


            // Örnek sipariş oluşturan metodumuzu çağırıyoruz.
            var order = GetOrder();

            //Handlebars'tan faydalanarak modelimiz ile template'imizi eşitliyoruz. (Öncesinde bir kaç format ayarı yapıyoruz.)
            var html = _htmlToPdf.CombineModelToHtml(temp, order);

            //Ve pdf'imizi oluşturuyoruz.
            byte[] pdf = _htmlToPdf.HtmlToPdf(html);


            /*
             Bu kısmı sonucu görebilmek adına ekledim. 
             Gerçek hatta örneğin Azure Storage'a yüklenip linki istenebilir
             ya da herhangi bir yere kaydedip DB'ye yolunun yazılması istenebilir.
             */
            #region PDF'ile alakası olmayan kısım.
            HttpResponseMessage result = null;
            result = Request.CreateResponse(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(pdf);
            result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            result.Content.Headers.ContentDisposition.FileName = "Invoice" + ".pdf";
            #endregion

            return result;

        }

        //Örnek bir sipariş oluşturuyoruz.
        public Order GetOrder()
        {
            List<OrderItem> orderItems = new List<OrderItem>()
                {
                   new OrderItem(){ ProductNo ="P-111", Description="Laptop", Quantity =3, UnitName ="Asus ZenBook", TotalPriceTl =15000, UnitPriceTl = 5000 },
                   new OrderItem(){ ProductNo ="P-112", Description="Ultrabook Laptop Serisi", Quantity =1, UnitName ="Apple MacBook", TotalPriceTl =8000, UnitPriceTl = 8000 },
                   new OrderItem(){ ProductNo ="P-113", Description="Akıllı Telefon ", Quantity =5, UnitName ="Iphone", TotalPriceTl =25000, UnitPriceTl = 5000 },
                   new OrderItem(){ ProductNo ="P-114", Description="Akıllı Telefon", Quantity =3, UnitName ="Samsung S20", TotalPriceTl =12000, UnitPriceTl = 4000 }

                };

            Order order = new Order()
            {
                OrderNo = "Order-1512",
                TotalPriceTl = orderItems.Sum(x => x.TotalPriceTl),
                CreateTs = DateTime.Now.AddDays(-10),
                DateDelivered = DateTime.Now,
                Note = "Bu sipariş ile ilgili not bulunmamaktadır.",
                OrderItems = orderItems,
            };
            return order;
        }
    }
}

