using System.Windows;
using System.Windows.Media;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System;

namespace Ivy.Components
{
    public class IvyList : IvyContainer
    {
        private List<IvyComponent> components;
        private Orientation orientation = Orientation.HORIZONTAL;
        private bool scrollable = true;
        private int visibleComponents = 0;
        private int selectedIndex = 0;
        private bool centerSelected = false;

        private ListBox list;

        public IvyList()
        {
            list = new ListBox();
            components = new List<IvyComponent>();
        }

        public override void draw(DrawingContext g) { }

        public void addComponent(IvyComponent comp)
        {
            components.Add(comp);
            list.Items.Add(comp.getComponent());
        }

        public override void draw(DrawingContext g, int x, int y, int allowedWidth, int allowedHeight)
        {
            if (!isVisible())
            {
                return;
            }
        }

        /* Quand la longueur de la liste est en AUTO */
        public int getMaximumChildWidth()
        {
            int maxWidth = 0;

            foreach (IvyComponent comp in components)
            {
                if (comp.getTotalWidth() > maxWidth)
                {
                    maxWidth = comp.getTotalWidth();
                }
            }

            return maxWidth;
        }

        /* Quand la hauteur de la liste est en AUTO */
        public int getMaximumChildHeight()
        {
            int maxHeight = 0;

            foreach (IvyComponent comp in components)
            {
                if (comp.getTotalHeight() > maxHeight)
                {
                    maxHeight = comp.getTotalWidth();
                }
            }

            return maxHeight;
        }

        public int getVisibleComponentsNb()
        {
            return visibleComponents;
        }

        public int getSelectedIndex()
        {
            return selectedIndex;
        }

        public bool isSelectedCentered()
        {
            return centerSelected;
        }

        public void setSelectedCentered(bool centered)
        {
            centerSelected = centered;
        }

        public void setVisibleComponentsNb(int nb)
        {
            if (nb > 0)
            {
                visibleComponents = nb;
            }
        }

        public override UIElement getComponent()
        {
            return list;
        }

        public override void setBackgroundColor(Brush c)
        {
            list.Background = c;
        }

        public override void setBackgroundImage(string path)
        {
            list.Background = new ImageBrush(new BitmapImage(new Uri(path, UriKind.Relative)));
        }

        public override void enterContainer()
        {
            //Select first element
        }

        public override void nextComponent(Movement m)
        {
            switch (orientation)
            {
                case Orientation.HORIZONTAL:
                    switch (m)
                    {
                        case Movement.LEFT:
                            previousElement();
                            break;

                        case Movement.RIGHT:
                            nextElement();
                            break;
                    }
                    break;

                case Orientation.VERTICAL:
                    switch (m)
                    {
                        case Movement.UP:
                            previousElement();
                            break;

                        case Movement.DOWN:
                            nextElement();
                            break;
                    }
                    break;
            }
        }

        public void previousElement()
        {
            //TODO
        }

        public void nextElement()
        {
            //TODO
        }

        public override List<IvyComponent> getChildren()
        {
            return components;
        }
    }
}