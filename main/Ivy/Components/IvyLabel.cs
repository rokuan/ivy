using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Media.Imaging;
using System;

namespace Ivy.Components
{
    public class IvyLabel : IvyTextComponent
    {
        private TextBlock label;

        public IvyLabel()
        {
            label = new TextBlock();
        }

        public override void draw(DrawingContext g) { }

        public override void draw(DrawingContext g, int x, int y, int allowedWidth, int allowedHeight)
        {
            if (!isVisible())
            {
                return;
            }

        }

        public override void setText(string s)
        {
            if (s != null)
            {
                label.Text = s;
            }
        }

        public override string getText()
        {
            return label.Text;
        }

        public override UIElement getComponent()
        {
            return label;
        }

        public override void setTextAlignment(Alignment alignment)
        {
            switch (alignment)
            {
                case Alignment.LEFT:
                    label.TextAlignment = TextAlignment.Left;
                    break;

                case Alignment.CENTER:
                    label.TextAlignment = TextAlignment.Center;
                    break;

                case Alignment.RIGHT:
                    label.TextAlignment = TextAlignment.Right;
                    break;
            }
        }

        public override void setVerticalTextAlignment(Alignment alignment)
        {
            switch (alignment)
            {
                case Alignment.TOP:
                    label.VerticalAlignment = VerticalAlignment.Top;
                    break;

                case Alignment.CENTER:
                    label.VerticalAlignment = VerticalAlignment.Center;
                    break;

                case Alignment.BOTTOM:
                    label.VerticalAlignment = VerticalAlignment.Bottom;
                    break;
            }
        }

        public override void setTextColor(Brush c)
        {
            label.Foreground = c;
        }

        public override void setBackgroundColor(Brush c)
        {
            label.Background = c;
        }

        public override void setBackgroundImage(string path)
        {
            label.Background = new ImageBrush(new BitmapImage(new Uri(path, UriKind.Relative)));
        }

        public override void setPadding(int margin)
        {
            marginLeft = margin;
            marginTop = margin;
            marginRight = margin;
            marginBottom = margin;

            label.Padding = new Thickness(margin);
        }

        public override void setPadding(int marginLeftRight, int marginTopBottom)
        {
            marginLeft = marginLeftRight;
            marginRight = marginLeftRight;
            marginTop = marginTopBottom;
            marginBottom = marginTopBottom;

            label.Padding = new Thickness(marginLeftRight, marginTopBottom, marginLeftRight, marginTopBottom);
        }

        public override void setPadding(int margLeft, int margTop, int margRight, int margBottom)
        {
            marginLeft = margLeft;
            marginRight = margRight;
            marginTop = margTop;
            marginBottom = margBottom;

            label.Padding = new Thickness(margLeft, margTop, margRight, margBottom);
        }
    }
}