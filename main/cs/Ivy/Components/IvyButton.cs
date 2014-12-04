using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System;

namespace Ivy.Components
{
    public class IvyButton : IvyTextComponent
    {
        private Button button;

        public IvyButton()
        {
            button = new Button();
            //setTextAlignment(textAlignment);
            borderType = Border.ROUNDED;
            borderColor = Brushes.DarkGray;
            backgroundColor = Brushes.LightGray;
        }

        public override void draw(DrawingContext g) { }

        public override void draw(DrawingContext g, int x, int y, int allowedWidth, int allowedHeight)
        {
            if (!isVisible())
            {
                return;
            }


        }

        public override UIElement getComponent()
        {
            return button;
        }

        public override void setText(string s)
        {
            if (s != null)
            {
                button.Content = s;
            }
        }

        public override string getText()
        {
            return (string)button.Content;
        }

        public override void setTextAlignment(Alignment alignment)
        {
            if (alignment == textAlignment)
            {
                return;
            }

            switch (alignment)
            {
                case Alignment.LEFT:
                    button.HorizontalContentAlignment = HorizontalAlignment.Left;
                    break;

                case Alignment.CENTER:
                    button.HorizontalContentAlignment = HorizontalAlignment.Center;
                    break;

                case Alignment.RIGHT:
                    button.HorizontalContentAlignment = HorizontalAlignment.Right;
                    break;
            }
        }

        public override void setVerticalTextAlignment(Alignment alignment)
        {
            switch (alignment)
            {
                case Alignment.TOP:
                    button.VerticalContentAlignment = VerticalAlignment.Top;
                    break;

                case Alignment.CENTER:
                    button.VerticalContentAlignment = VerticalAlignment.Center;
                    break;

                case Alignment.BOTTOM: 
                    button.VerticalContentAlignment = VerticalAlignment.Bottom;
                    break;
            }
        }

        public override void setTextColor(Brush c)
        {
            button.Foreground = c;
        }

        public override void setBackgroundColor(Brush c)
        {
            button.Background = c;
        }

        public override void setBackgroundImage(string path)
        {
            button.Background = new ImageBrush(new BitmapImage(new Uri(path, UriKind.Relative)));
        }

        public override void setPadding(int margin)
        {
            marginLeft = margin;
            marginTop = margin;
            marginRight = margin;
            marginBottom = margin;

            button.Padding = new Thickness(margin);
        }

        public override void setPadding(int marginLeftRight, int marginTopBottom)
        {
            marginLeft = marginLeftRight;
            marginRight = marginLeftRight;
            marginTop = marginTopBottom;
            marginBottom = marginTopBottom;

            button.Padding = new Thickness(marginLeftRight, marginTopBottom, marginLeftRight, marginTopBottom);
        }

        public override void setPadding(int margLeft, int margTop, int margRight, int margBottom)
        {
            marginLeft = margLeft;
            marginRight = margRight;
            marginTop = margTop;
            marginBottom = margBottom;

            button.Padding = new Thickness(margLeft, margTop, margRight, margBottom);
        }
    }
}