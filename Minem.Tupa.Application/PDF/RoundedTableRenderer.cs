using iText.Kernel.Colors;
using iText.Kernel.Geom;
using iText.Kernel.Pdf.Canvas;
using iText.Layout.Element;
using iText.Layout.Renderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Application.PDF
{
    public class RoundedTableRenderer : TableRenderer
    {
        private float radioEsquina;
        private Color colorFondo;

        public RoundedTableRenderer(Table modelElement, float radio, Color fondo)
            : base(modelElement)
        {
            this.radioEsquina = radio;
            this.colorFondo = fondo != null ? fondo : new DeviceRgb(255, 255, 255);
        }

        public override void Draw(DrawContext drawContext)
        {
            // Obtener el área que ocupa toda la tabla
            Rectangle rect = GetOccupiedAreaBBox();

            PdfCanvas canvas = drawContext.GetCanvas();

            canvas.SaveState();
            if (colorFondo != null)
            {
                canvas.SetFillColor(colorFondo);
            }
            canvas.SetStrokeColor(ColorConstants.BLACK);
            canvas.SetLineWidth(1.2f);

            // Dibujar rectángulo redondeado que envuelve la tabla
            //canvas.RoundRectangle(rect.GetX() - 5, rect.GetY() - 5, rect.GetWidth() + 10, rect.GetHeight() + 10, radioEsquina);
            canvas.RoundRectangle(rect.GetX(), rect.GetY(), rect.GetWidth(), rect.GetHeight(), radioEsquina);
            canvas.FillStroke();
            canvas.RestoreState();

            // Dibujar el resto (contenido de la tabla)
            base.Draw(drawContext);
        }
    }
}
