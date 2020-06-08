using HandlebarsDotNet;
using SelectPdf;
using System;
using System.Web;

namespace SampleCreatePDF.Managers
{
    public class PdfManager
    {
        HtmlToPdf _htmlToPdf = new HtmlToPdf();
        public byte[] HtmlToPdf(string html)
        {
            //Kağıdımızı oluşturuyoruz. Duruma göre farklı kağıt ölçüleri çağırılabilir.
            PageA4();

            //PDF'i oluşturuyoruz.
            var doc = _htmlToPdf.ConvertHtmlString(html);

            return doc.Save();
        }

        private void PageA4()
        {
            _htmlToPdf.Options.PdfPageSize = PdfPageSize.A4;
            _htmlToPdf.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
            _htmlToPdf.Options.MarginLeft = 10;
            _htmlToPdf.Options.MarginRight = 10;
            _htmlToPdf.Options.MarginTop = 20;
            _htmlToPdf.Options.MarginBottom = 20;
        }

        public string CombineModelToHtml(string html, object model)
        {
            //Tarih Formatı
            Handlebars.RegisterHelper("dateFormat", (writer, context, parameters) =>
            {
                writer.WriteSafeString(DateTime.Parse(parameters[0].ToString()).ToString(parameters[1].ToString()));

                /* 
                 Bu kısım biraz kafa karıştırıcı olabilir. Bu yüzden biraz detaylandırıyorum
                 
                 * * 1.Parametre -> parameters[0].ToString() -> örn. 30.01.2020
                 * 2.Parametre -> parameters[1].ToString() -> "dd/MM/yyyy"
                 
                  Neden tarih formatını parametik yaptık da elimizle yazmadık.
                  Bunun sebebi birden fazla tarih foramtı istenmesi durumunda bu isteği karşılayabilmek için.

                 */
            });

            // Para Formatı
            Handlebars.RegisterHelper("moneyFormat", (writer, context, parameters) =>
            {
                writer.WriteSafeString($"{parameters[0]:N2}");
            });

            return HttpUtility.HtmlDecode(Handlebars.Compile(html)(model));
        }

    }
}