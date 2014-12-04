using System.Windows.Media;

namespace Ivy.Components
{
    public abstract class IvyTextComponent : IvyComponent
    {
        protected Alignment textAlignment = Alignment.LEFT;
        protected Alignment verticalTextAlignment = Alignment.CENTER;
        protected Brush textColor = Brushes.Black;

        public abstract void setText(string s);
        public abstract string getText();

        public abstract void setTextAlignment(Alignment alignment);
        public Alignment getTextAlignment()
        {
            return textAlignment;
        }

        public abstract void setVerticalTextAlignment(Alignment alignment);
        public Alignment getVerticalTextAlignment()
        {
            return verticalTextAlignment;
        }

        public abstract void setTextColor(Brush c);
        public Brush getTextColor()
        {
            return textColor;
        }

        public abstract void setPadding(int padding);
        public abstract void setPadding(int paddingLeftRight, int paddingTopBottom);
        public abstract void setPadding(int paddingLeft, int paddingTop, int paddingRight, int paddingBottom);
    }
}