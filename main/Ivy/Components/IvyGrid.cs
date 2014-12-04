using Ivy.Components;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System;
using System.Windows.Media.Imaging;
using System.Collections.Generic;

namespace Ivy.Components
{
    public class IvyGrid : IvyContainer
    {
        private int rows = 1, cols = 1;
        private IvyComponent[,] grid;

        /*private JPanel gridPanel;
        private GridBagConstraints constr;*/

        /* TODO: mettre a 0, 0 ? */
        private int selectedRow = -1, selectedColumn = -1;

        private Grid gridComp;

        public IvyGrid(int rowNb, int colNb)
        {
            /*gridPanel = new JPanel(new GridBagLayout());
            constr = new GridBagConstraints();*/
            gridComp = new Grid();

            if (rows > 0)
            {
                rows = rowNb;
            }
            if (colNb > 0)
            {
                cols = colNb;
            }

            grid = new IvyComponent[rows, cols];

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    grid[r, c] = null;
                }
            }

            RowDefinition rowDef;
            ColumnDefinition colDef;

            gridComp.RowDefinitions.Clear();
            gridComp.ColumnDefinitions.Clear();

            for (int r = 0; r < rows; r++)
            {
                rowDef = new RowDefinition();
                gridComp.RowDefinitions.Add(rowDef);
            }

            for (int c = 0; c < cols; c++)
            {
                colDef = new ColumnDefinition();
                gridComp.ColumnDefinitions.Add(colDef);
            }
        }

        public void setRows(int r)
        {
            if (r > 0)
            {
                rows = r;

                grid = new IvyComponent[rows, cols];

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        grid[i, j] = null;
                    }
                }

                RowDefinition rowDef;

                gridComp.RowDefinitions.Clear();

                for (int row = 0; row < rows; row++)
                {
                    rowDef = new RowDefinition();
                    gridComp.RowDefinitions.Add(rowDef);
                }
            }
        }

        public void setCols(int c)
        {
            if (c > 0)
            {
                cols = c;

                grid = new IvyComponent[rows, cols];

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        grid[i, j] = null;
                    }
                }

                ColumnDefinition colDef;

                gridComp.ColumnDefinitions.Clear();

                for (int col = 0; col < cols; col++)
                {
                    colDef = new ColumnDefinition();
                    gridComp.ColumnDefinitions.Add(colDef);
                }
            }
        }

        public void addComponent(IvyComponent comp)
        {
            setComponentAt(comp, comp.getRow(), comp.getCol());
        }

        public void setComponentAt(IvyComponent comp, int r, int c)
        {
            if (r >= 0 && r < rows && c >= 0 && c < cols)
            {
                grid[r, c] = comp;


                Grid.SetColumn(comp.getComponent(), c);
                Grid.SetRow(comp.getComponent(), r);
                Grid.SetColumnSpan(comp.getComponent(), comp.getColSpan());
                Grid.SetRowSpan(comp.getComponent(), comp.getRowSpan());

                //TODO: mettre le bon parametre
                /*if(comp.getWidthType() == DimensionParameters.FILL && comp.getHeightType() == DimensionParameters.FILL){
                    constr.fill = GridBagConstraints.BOTH;
                }
                else if(comp.getWidthType() == DimensionParameters.FILL){
                    constr.fill = GridBagConstraints.HORIZONTAL;
                }
                else if(comp.getHeightType() == DimensionParameters.FILL){
                    constr.fill = GridBagConstraints.VERTICAL;
                }*/

                gridComp.Children.Add(comp.getComponent());
            }
        }

        public override void draw(DrawingContext g) { }

        public override void draw(DrawingContext g, int x, int y, int allowedWidth, int allowedHeight)
        {
            if (!isVisible())
            {
                return;
            }

            //TODO: OnRender(...)
        }

        public override UIElement getComponent()
        {
            return gridComp;
        }

        public override void setBackgroundColor(Brush c)
        {
            gridComp.Background = c;
        }

        public override void setBackgroundImage(string path)
        {
            gridComp.Background = new ImageBrush(new BitmapImage(new Uri(path, UriKind.Relative)));
        }

        public override void enterContainer()
        {
            //Select first element
        }

        public override void nextComponent(Movement m)
        {
            switch (m)
            {
                case Movement.LEFT:
                    previousColumn();
                    break;

                case Movement.RIGHT:
                    nextColumn();
                    break;

                case Movement.UP:
                    previousRow();
                    break;

                case Movement.DOWN:
                    nextRow();
                    break;
            }
        }

        public void previousColumn()
        {
            if (selectedColumn == 0)
            {
                return;
            }

            //TODO
        }

        public void nextColumn()
        {
            if (selectedColumn == cols - 1)
            {
                return;
            }

            //TODO
        }

        public void previousRow()
        {
            if (selectedRow == 0)
            {
                return;
            }

            //TODO
        }

        public void nextRow()
        {
            if (selectedRow == rows - 1)
            {
                return;
            }

            //TODO
        }

        public override List<IvyComponent> getChildren()
        {
            List<IvyComponent> components = new List<IvyComponent>();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (grid[i, j] != null)
                    {
                        components.Add(grid[i, j]);
                    }
                }
            }

            return components;
        }
    }
}