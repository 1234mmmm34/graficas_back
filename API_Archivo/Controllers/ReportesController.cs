using API_Archivo.Clases;
using Microsoft.AspNetCore.Mvc;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Xml.Linq;

namespace API_Archivo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportesController : ControllerBase
    {
        [HttpGet]
        [Route("Reporte_Entradas")]
        public IActionResult GenerarReporteEntradas()
        {
            Entradas objEntradas = new Entradas();

            List<Entradas> entradas = objEntradas.Consultar_Entradas();

            // Crear un nuevo documento PDF
            PdfDocument document = new PdfDocument();

            // Añadir una página al documento
            PdfPage page = document.AddPage();

            // Obtener el objeto XGraphics para dibujar en la página
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Dibujar el encabezado
            XFont fontEncabezado = new XFont("Arial", 16);
            XSize textSize = gfx.MeasureString("Reporte de entradas", fontEncabezado);
            double xPos = (page.Width - textSize.Width) / 2; // Centrar horizontalmente
            double yPos = 40; // Posición fija en la parte superior de la página
            gfx.DrawString("Reporte de entradas", fontEncabezado, XBrushes.Black,
                new XRect(xPos, yPos, page.Width, page.Height),
                XStringFormats.TopLeft);

            // Dibujar la tabla de datos
            double tableWidth = 400;
            yPos += 30; // Ajuste de la posición Y
            xPos = (page.Width - tableWidth) / 2;
            double cellHeight = 30;

            // Encabezados de columna
            string[] headers = { "ID", "Fecha", "Nombre de la Persona" };
            XFont fontHeader = new XFont("Arial", 12);
            for (int i = 0; i < headers.Length; i++)
            {
                gfx.DrawRectangle(XBrushes.LightGray, xPos, yPos, tableWidth / 3, cellHeight);
                gfx.DrawString(headers[i], fontHeader, XBrushes.Black,
                    new XRect(xPos, yPos, tableWidth / 3, cellHeight),
                    XStringFormats.Center);
                xPos += tableWidth / 3;
            }
            yPos += cellHeight;

            // Datos de las entradas
            XFont fontData = new XFont("Arial", 12);
            foreach (var entrada in entradas)
            {
                xPos = (page.Width - tableWidth) / 2;

                gfx.DrawRectangle(XBrushes.White, xPos, yPos, tableWidth / 3, cellHeight);
                gfx.DrawString(entrada.id_entrada.ToString(), fontData, XBrushes.Black,
                    new XRect(xPos, yPos, tableWidth / 3, cellHeight),
                    XStringFormats.Center);
                xPos += tableWidth / 3;

                gfx.DrawRectangle(XBrushes.White, xPos, yPos, tableWidth / 3, cellHeight);
                gfx.DrawString(entrada.fecha.ToString("dd/MM/yyyy HH:mm"), fontData, XBrushes.Black,
                    new XRect(xPos, yPos, tableWidth / 3, cellHeight),
                    XStringFormats.Center);
                xPos += tableWidth / 3;

                gfx.DrawRectangle(XBrushes.White, xPos, yPos, tableWidth / 3, cellHeight);
                gfx.DrawString(entrada.nombre, fontData, XBrushes.Black,
                    new XRect(xPos, yPos, tableWidth / 3, cellHeight),
                    XStringFormats.Center);

                yPos += cellHeight;
            }

            // Guardar el documento en un MemoryStream
            MemoryStream stream = new MemoryStream();
            document.Save(stream, false);
            stream.Position = 0;

            // Devolver el PDF como un archivo para descargar
            return File(stream, "application/pdf", "reporte_SinFiltros.pdf");
        }

        [HttpGet]
        [Route("Reporte_EntradasPorPersona")]
        public IActionResult GenerarReporteEntradasPorPersona(string nombre_persona)
        {
            Entradas objEntradas = new Entradas();

            List<Entradas> entradas = objEntradas.Consultar_EntradasPorPersona(nombre_persona);

            // Crear un nuevo documento PDF
            PdfDocument document = new PdfDocument();

            // Añadir una página al documento
            PdfPage page = document.AddPage();

            // Obtener el objeto XGraphics para dibujar en la página
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Dibujar el encabezado
            XFont fontEncabezado = new XFont("Arial", 16);
            XSize textSize = gfx.MeasureString("Reporte de entradas", fontEncabezado);
            double xPos = (page.Width - textSize.Width) / 2; // Centrar horizontalmente
            double yPos = 40; // Posición fija en la parte superior de la página
            gfx.DrawString("Reporte de entradas", fontEncabezado, XBrushes.Black,
                new XRect(xPos, yPos, page.Width, page.Height),
                XStringFormats.TopLeft);

            // Dibujar la tabla de datos
            double tableWidth = 400;
            yPos += 30; // Ajuste de la posición Y
            xPos = (page.Width - tableWidth) / 2;
            double cellHeight = 30;

            // Encabezados de columna
            string[] headers = { "ID", "Fecha", "Nombre de la Persona" };
            XFont fontHeader = new XFont("Arial", 12);
            for (int i = 0; i < headers.Length; i++)
            {
                gfx.DrawRectangle(XBrushes.LightGray, xPos, yPos, tableWidth / 3, cellHeight);
                gfx.DrawString(headers[i], fontHeader, XBrushes.Black,
                    new XRect(xPos, yPos, tableWidth / 3, cellHeight),
                    XStringFormats.Center);
                xPos += tableWidth / 3;
            }
            yPos += cellHeight;

            // Datos de las entradas
            XFont fontData = new XFont("Arial", 12);
            foreach (var entrada in entradas)
            {
                xPos = (page.Width - tableWidth) / 2;

                gfx.DrawRectangle(XBrushes.White, xPos, yPos, tableWidth / 3, cellHeight);
                gfx.DrawString(entrada.id_entrada.ToString(), fontData, XBrushes.Black,
                    new XRect(xPos, yPos, tableWidth / 3, cellHeight),
                    XStringFormats.Center);
                xPos += tableWidth / 3;

                gfx.DrawRectangle(XBrushes.White, xPos, yPos, tableWidth / 3, cellHeight);
                gfx.DrawString(entrada.fecha.ToString("dd/MM/yyyy HH:mm"), fontData, XBrushes.Black,
                    new XRect(xPos, yPos, tableWidth / 3, cellHeight),
                    XStringFormats.Center);
                xPos += tableWidth / 3;

                gfx.DrawRectangle(XBrushes.White, xPos, yPos, tableWidth / 3, cellHeight);
                gfx.DrawString(entrada.nombre, fontData, XBrushes.Black,
                    new XRect(xPos, yPos, tableWidth / 3, cellHeight),
                    XStringFormats.Center);

                yPos += cellHeight;
            }

            // Guardar el documento en un MemoryStream
            MemoryStream stream = new MemoryStream();
            document.Save(stream, false);
            stream.Position = 0;

            // Devolver el PDF como un archivo para descargar
            return File(stream, "application/pdf", $"reporte_entradasPorPersona{nombre_persona}.pdf");
        }

        [HttpGet]
        [Route("Reporte_EntradasPorFecha")]
        public IActionResult GenerarReporteEntradasPorFecha(string fecha_inicio, string fecha_final)
        {
            Entradas objEntradas = new Entradas();

            List<Entradas> entradas = objEntradas.Consultar_EntradasPorFecha(fecha_inicio, fecha_final);

            // Crear un nuevo documento PDF
            PdfDocument document = new PdfDocument();

            // Añadir una página al documento
            PdfPage page = document.AddPage();

            // Obtener el objeto XGraphics para dibujar en la página
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Dibujar el encabezado
            XFont fontEncabezado = new XFont("Arial", 16);
            XSize textSize = gfx.MeasureString("Reporte de entradas", fontEncabezado);
            double xPos = (page.Width - textSize.Width) / 2; // Centrar horizontalmente
            double yPos = 40; // Posición fija en la parte superior de la página
            gfx.DrawString("Reporte de entradas", fontEncabezado, XBrushes.Black,
                new XRect(xPos, yPos, page.Width, page.Height),
                XStringFormats.TopLeft);

            // Dibujar la tabla de datos
            double tableWidth = 400;
            yPos += 30; // Ajuste de la posición Y
            xPos = (page.Width - tableWidth) / 2;
            double cellHeight = 30;

            // Encabezados de columna
            string[] headers = { "ID", "Fecha", "Nombre de la persona" };
            XFont fontHeader = new XFont("Arial", 12);
            for (int i = 0; i < headers.Length; i++)
            {
                gfx.DrawRectangle(XBrushes.LightGray, xPos, yPos, tableWidth / 3, cellHeight);
                gfx.DrawString(headers[i], fontHeader, XBrushes.Black,
                    new XRect(xPos, yPos, tableWidth / 3, cellHeight),
                    XStringFormats.Center);
                xPos += tableWidth / 3;
            }
            yPos += cellHeight;

            // Datos de las entradas
            XFont fontData = new XFont("Arial", 12);
            foreach (var entrada in entradas)
            {
                xPos = (page.Width - tableWidth) / 2;

                gfx.DrawRectangle(XBrushes.White, xPos, yPos, tableWidth / 3, cellHeight);
                gfx.DrawString(entrada.id_entrada.ToString(), fontData, XBrushes.Black,
                    new XRect(xPos, yPos, tableWidth / 3, cellHeight),
                    XStringFormats.Center);
                xPos += tableWidth / 3;

                gfx.DrawRectangle(XBrushes.White, xPos, yPos, tableWidth / 3, cellHeight);
                gfx.DrawString(entrada.fecha.ToString("dd/MM/yyyy HH:mm"), fontData, XBrushes.Black,
                    new XRect(xPos, yPos, tableWidth / 3, cellHeight),
                    XStringFormats.Center);
                xPos += tableWidth / 3;

                gfx.DrawRectangle(XBrushes.White, xPos, yPos, tableWidth / 3, cellHeight);
                gfx.DrawString(entrada.nombre, fontData, XBrushes.Black,
                    new XRect(xPos, yPos, tableWidth / 3, cellHeight),
                    XStringFormats.Center);

                yPos += cellHeight;
            }

            // Guardar el documento en un MemoryStream
            MemoryStream stream = new MemoryStream();
            document.Save(stream, false);
            stream.Position = 0;

            // Devolver el PDF como un archivo para descargar
            return File(stream, "application/pdf", $"reporte_entradasPorFecha{fecha_inicio}-{fecha_final}.pdf");
        }

        [HttpGet]
        [Route("Reporte_Deudas")]
        public IActionResult GenerarReporteDeudas(int id_tesorero, string tipo_deuda)
        {
            DeudasController objDeudas = new DeudasController();
            List<Deudas> deudas = new List<Deudas>();

            if (tipo_deuda == "Ordinaria" || tipo_deuda == "ordinaria")
            {
                deudas = objDeudas.Consultar_DeudasOrdinarias(id_tesorero);
            }
            else if (tipo_deuda == "Extraordinaria" || tipo_deuda == "extraordinaria")
            {
                deudas = objDeudas.Consultar_DeudaExtra(id_tesorero);
            }
            else
            {
                return BadRequest("Tipo de deuda no válido.");
            }



            // Crear un nuevo documento PDF
            PdfDocument document = new PdfDocument();

            // Añadir una página al documento
            PdfPage page = document.AddPage();

            // Obtener el objeto XGraphics para dibujar en la página
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Dibujar el encabezado
            XFont fontEncabezado = new XFont("Arial", 16);
            XSize textSize = gfx.MeasureString("Reporte de deudas", fontEncabezado);
            double xPos = (page.Width - textSize.Width) / 2; // Centrar horizontalmente
            double yPos = 40; // Posición fija en la parte superior de la página
            gfx.DrawString("Reporte de deudas", fontEncabezado, XBrushes.Black,
                new XRect(xPos, yPos, page.Width, page.Height),
                XStringFormats.TopLeft);

            // Dibujar la tabla de datos
            double tableWidth = 550; // Ancho total de la tabla
            yPos += 30; // Ajuste de la posición Y
            xPos = (page.Width - tableWidth) / 2; // Centrar la tabla horizontalmente

            // Ancho de cada columna
            double[] columnWidths = { 40, 60, 100, 150, 80, 80, 60, 100 }; // Ancho de cada columna en orden

            // Encabezados de columna
            string[] headers = { "ID", "Monto", "Nombre", "Descripción", "Días de gracia", "Periodicidad", "Recargo", "Próximo Pago" };
            XFont fontHeader = new XFont("Arial", 12);
            for (int i = 0; i < headers.Length; i++)
            {
                gfx.DrawRectangle(XBrushes.LightGray, xPos, yPos, columnWidths[i], 30); // Altura fija de celda
                gfx.DrawString(headers[i], fontHeader, XBrushes.Black,
                    new XRect(xPos, yPos, columnWidths[i], 30),
                    XStringFormats.Center);
                xPos += columnWidths[i];
            }
            yPos += 30; // Ajuste de la posición Y

            // Datos de las deudas
            XFont fontData = new XFont("Arial", 12);
            foreach (var deuda in deudas)
            {
                xPos = (page.Width - tableWidth) / 2; // Centrar horizontalmente

                // Dibujar cada celda de datos
                for (int i = 0; i < headers.Length; i++)
                {
                    gfx.DrawRectangle(XBrushes.White, xPos, yPos, columnWidths[i], 20); // Altura fija de celda
                    string data = "";
                    switch (i)
                    {
                        case 0: data = deuda.id_deudas.ToString(); break;
                        case 1: data = deuda.monto.ToString(); break;
                        case 2: data = deuda.nombre; break;
                        case 3: data = deuda.descripcion; break;
                        case 4: data = deuda.dias_gracia.ToString(); break;
                        case 5: data = deuda.periodicidad.ToString(); break;
                        case 6: data = deuda.recargo.ToString(); break;
                        case 7: data = deuda.proximo_pago.ToString("dd/MM/yyyy"); break;
                    }
                    gfx.DrawString(data, fontData, XBrushes.Black,
                        new XRect(xPos, yPos, columnWidths[i], 20),
                        XStringFormats.Center);
                    xPos += columnWidths[i];
                }
                yPos += 20; // Ajuste de la posición Y
            }

            // Guardar el documento en un MemoryStream
            MemoryStream stream = new MemoryStream();
            document.Save(stream, false);
            stream.Position = 0;

            // Devolver el PDF como un archivo para descargar
            return File(stream, "application/pdf", $"reporte_Deudas{tipo_deuda}.pdf");
        }

        [HttpGet]
        [Route("Reporte_Deudores")]
        public IActionResult GenerarReporteDeudores(int id_fraccionamiento)
        {
            DeudasController objDeudas = new DeudasController();
            List<Deudoress> deudores = objDeudas.Consultar_DeudoresOrdinarios(id_fraccionamiento);

            // Crear un nuevo documento PDF
            PdfDocument document = new PdfDocument();

            // Añadir una página al documento
            PdfPage page = document.AddPage();

            // Obtener el objeto XGraphics para dibujar en la página
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Dibujar el encabezado
            XFont fontEncabezado = new XFont("Arial", 16);
            XSize textSize = gfx.MeasureString("Reporte de deudores", fontEncabezado);
            double xPos = (page.Width - textSize.Width) / 2; // Centrar horizontalmente
            double yPos = 40; // Posición fija en la parte superior de la página
            gfx.DrawString("Reporte de deudores", fontEncabezado, XBrushes.Black,
                new XRect(xPos, yPos, page.Width, page.Height),
                XStringFormats.TopLeft);

            // Dibujar la tabla de datos
            double tableWidth = 550; // Ancho total de la tabla
            yPos += 30; // Ajuste de la posición Y
            xPos = (page.Width - tableWidth) / 2; // Centrar la tabla horizontalmente

            // Ancho de cada columna
            double[] columnWidths = { 40, 120, 100, 100, 80, 110 }; // Ancho de cada columna en orden

            // Encabezados de columna
            string[] headers = { "ID", "Persona", "Tipo de Deuda", "Nombre de Deuda", "Monto", "Próximo Pago" };
            XFont fontHeader = new XFont("Arial", 12);
            for (int i = 0; i < headers.Length; i++)
            {
                gfx.DrawRectangle(XBrushes.LightGray, xPos, yPos, columnWidths[i], 30); // Altura fija de celda
                gfx.DrawString(headers[i], fontHeader, XBrushes.Black,
                    new XRect(xPos, yPos, columnWidths[i], 30),
                    XStringFormats.Center);
                xPos += columnWidths[i];
            }
            yPos += 30; // Ajuste de la posición Y

            // Datos de las deudas
            XFont fontData = new XFont("Arial", 12);
            foreach (var deudor in deudores)
            {
                xPos = (page.Width - tableWidth) / 2; // Centrar horizontalmente

                // Dibujar cada celda de datos
                for (int i = 0; i < headers.Length; i++)
                {
                    gfx.DrawRectangle(XBrushes.White, xPos, yPos, columnWidths[i], 20); // Altura fija de celda
                    string data = "";
                    switch (i)
                    {
                        case 0: data = deudor.id_deudor.ToString(); break;
                        case 1: data = deudor.nombre_persona; break;
                        case 2: data = deudor.tipo_deuda; break;
                        case 3: data = deudor.nombre_deuda; break;
                        case 4: data = deudor.monto.ToString(); break;
                        case 5: data = DateTime.Parse(deudor.proximo_pago).ToString("yyyy-MM-dd"); break;
                    }
                    gfx.DrawString(data, fontData, XBrushes.Black,
                        new XRect(xPos, yPos, columnWidths[i], 20),
                        XStringFormats.Center);
                    xPos += columnWidths[i];
                }
                yPos += 20; // Ajuste de la posición Y
            }

            // Guardar el documento en un MemoryStream
            MemoryStream stream = new MemoryStream();
            document.Save(stream, false);
            stream.Position = 0;

            // Devolver el PDF como un archivo para descargar
            return File(stream, "application/pdf", $"reporte_DeudoresFraccionamiento_{id_fraccionamiento}.pdf");
        }

    }
}