using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Ivy.Components
{
    public abstract class IvyComponent
    {
        public enum DimensionParameters
        {
            FILL = 0,
            AUTO = 1,
            FIXED = 2
        }

        public enum Orientation
        {
            HORIZONTAL = 0,
            VERTICAL = 1
        }

        public enum Alignment
        {
            LEFT = 0,
            CENTER = 1,
            RIGHT = 2,
            TOP = 3,
            BOTTOM = 4
        }

        public enum Border
        {
            SQUARED = 0,
            ROUNDED = 1,
            NONE = 2
        }

        public enum Movement
        {
            UP = 0,
            LEFT = 1,
            RIGHT = 2,
            DOWN = 3
        }

        protected int marginLeft = 0, marginTop = 0, marginRight = 0, marginBottom = 0;

        protected int rowSpan = 1, colSpan = 1;
        protected int row = 0, col = 0;

        protected int preferredWidth = -1, preferredHeight = -1, assignedWidth = -1, assignedHeight = -1;
        protected int x, y;

        protected DimensionParameters widthType = DimensionParameters.FILL, heightType = DimensionParameters.FILL;

        protected Border borderType = Border.NONE;
        protected Brush borderColor = Brushes.Black;
        protected Brush backgroundColor = null;

        private string id = null;

        public void setMargin(int margin)
        {
            marginLeft = margin;
            marginTop = margin;
            marginRight = margin;
            marginBottom = margin;

            ((FrameworkElement)
            this.getComponent()).Margin = new Thickness(margin);
        }

        public void setMargin(int marginLeftRight, int marginTopBottom)
        {
            marginLeft = marginLeftRight;
            marginRight = marginLeftRight;
            marginTop = marginTopBottom;
            marginBottom = marginTopBottom;

            ((FrameworkElement)
            this.getComponent()).Margin = new Thickness(marginLeftRight, marginTopBottom, marginLeftRight, marginTopBottom);
        }

        public void setMargin(int margLeft, int margTop, int margRight, int margBottom)
        {
            marginLeft = margLeft;
            marginRight = margRight;
            marginTop = margTop;
            marginBottom = margBottom;

            ((FrameworkElement)
            this.getComponent()).Margin = new Thickness(margLeft, margTop, margRight, marginBottom);
        }

        public void setWidth(int w)
        {
            if (w >= 0)
            {
                if (widthType == DimensionParameters.FILL)
                {
                    preferredWidth = w;
                }
                else
                {
                    preferredWidth = w;
                    widthType = DimensionParameters.FIXED;
                }
            }
        }

        public void setHeight(int h)
        {
            if (h >= 0)
            {
                if (heightType == DimensionParameters.FILL)
                {
                    preferredHeight = h;
                }
                else
                {
                    preferredHeight = h;
                    heightType = DimensionParameters.FIXED;
                }
            }
        }

        public void setWidthType(DimensionParameters wType)
        {
            /* TODO: modifier lorsque nouvelle valeur */
                widthType = wType;

                switch (widthType)
                {
                    case DimensionParameters.AUTO:
                        break;

                    case DimensionParameters.FILL:
                        /* TODO */
                        //((FrameworkElement)this.getComponent()).Width = double.NaN;
                        break;

                    case DimensionParameters.FIXED:
                        break;
                }
        }

        public void setHeightType(DimensionParameters hType)
        {
                heightType = hType;
        }

        public DimensionParameters getWidthType()
        {
            return widthType;
        }

        public DimensionParameters getHeightType()
        {
            return heightType;
        }

        public abstract void draw(DrawingContext g);
        public abstract void draw(DrawingContext g, int x, int y, int allowedWidth, int allowedHeight);

        public int getTotalWidth()
        {
            return marginLeft + preferredWidth + marginRight;
        }

        public int getTotalHeight()
        {
            return marginTop + preferredHeight + marginBottom;
        }

        public void setVisible(bool vsbl)
        {
            getComponent().Visibility = vsbl ? Visibility.Visible : Visibility.Hidden;
        }

        public bool isVisible()
        {
            return getComponent().Visibility == Visibility.Visible;
        }

        public void setEnabled(bool enab)
        {
            getComponent().IsEnabled = enab;
        }

        public bool isEnabled()
        {
            return getComponent().IsEnabled;
        }

        public abstract UIElement getComponent();

        public void setBorderType(Border bType)
        {
            /* TODO: mettre les bonnes bordures */

            /*switch (bType)
            {
                case Border.NONE:
                    getComponent(). = (null);
                    break;

                case Border.ROUNDED:
                    getComponent().setBorder(null);
                    break;

                case Border.SQUARED:
                    getComponent().setBorder(null);
                    break;
            }*/
        }

        /*public void setBackgroundColor(Brush c)
        {
            backgroundColor = c;
            //TODO: trouver une maniere de mettre le background pour les composants le supportant 
            //this.getComponent().Background = c;
        }*/

        public abstract void setBackgroundColor(Brush c);
        public abstract void setBackgroundImage(string path);

        public int getRow()
        {
            return row;
        }

        public int getCol()
        {
            return col;
        }

        public int getRowSpan()
        {
            return rowSpan;
        }

        public int getColSpan()
        {
            return colSpan;
        }

        public void setRow(int r)
        {
            if (r >= 0)
            {
                row = r;
            }
        }

        public void setCol(int c)
        {
            if (c >= 0)
            {
                col = c;
            }
        }

        public void setRowSpan(int rs)
        {
            if (rs > 0)
            {
                rowSpan = rs;
            }
        }

        public void setColSpan(int cs)
        {
            if (cs > 0)
            {
                colSpan = cs;
            }
        }

        public bool hasName()
        {
            return id != null;
        }

        public string getName()
        {
            return id;
        }

        public void setName(string name)
        {
            if (name != null)
            {
                id = name;
            }
        }
    }
}