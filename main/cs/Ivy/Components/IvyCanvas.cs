using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;

namespace Ivy.Components
{
    class IvyCanvas : IvyComponent
    {
        private Brush currentColor;
        private Canvas canvas;
        private WriteableBitmap bmp;

        public IvyCanvas()
        {
            canvas = new Canvas();
        }

        public void drawImage(BitmapImage img)
        {
            //canvas.Children.Add(new img);
        }

        public void drawLine(int x1, int y1, int x2, int y2)
        {

        }

        public void drawRect(int x, int y, int w, int h)
        {

        }

        public void drawRect(Int32Rect r)
        {
            drawRect(r.X, r.Y, r.Width, r.Height);
        }

        public void setColor(Brush color)
        {
            currentColor = color;
        }

        public override void draw(DrawingContext g)
        {
            
        }

        public override void draw(DrawingContext g, int x, int y, int allowedWidth, int allowedHeight)
        {
            
        }

        public override void setBackgroundColor(Brush c)
        {
            
        }

        public override void setBackgroundImage(string path)
        {
            
        }

        public override UIElement getComponent()
        {
            return canvas;
        }
    }
}
