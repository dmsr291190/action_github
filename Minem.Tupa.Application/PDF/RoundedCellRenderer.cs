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
    public class RoundedCellRenderer : iText.Layout.Renderer.CellRenderer
    {
        private float radius;
        private Color fillColor;

        public RoundedCellRenderer(Cell modelElement, float radius, Color fillColor)
            : base(modelElement)
        {
            this.radius = radius;
            this.fillColor = fillColor!=null? fillColor: new DeviceRgb(255, 255, 255);
        }

        public override void DrawBackground(DrawContext drawContext)
        {
            Rectangle rect = GetOccupiedAreaBBox();
            PdfCanvas canvas = drawContext.GetCanvas();

            canvas.SaveState();
            if (fillColor != null)
            {
                canvas.SetFillColor(fillColor);
            }
            canvas.SetStrokeColor(ColorConstants.BLACK);
            canvas.SetLineWidth(0.5f);
            canvas.RoundRectangle(rect.GetX() + 1, rect.GetY() + 1, rect.GetWidth() - 2, rect.GetHeight() - 2, radius);
            canvas.FillStroke();
            canvas.RestoreState();

            base.DrawBackground(drawContext);
        }
    }
}
