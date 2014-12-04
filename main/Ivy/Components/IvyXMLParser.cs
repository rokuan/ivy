using System.Xml;
using Ivy.Exceptions;
using System;
using System.Windows.Media;
using System.Collections.Generic;

namespace Ivy.Components
{
    class IvyXMLParser
    {
        private static List<string> names;

        public static IvyComponent parseXML(string fileName)
        {
            names = new List<string>();

            lock (names)
            {
                XmlDocument document = new XmlDocument();
                IvyComponent comp = null;

                try
                {
                    document.Load(fileName);

                    comp = parseXMLComponent(document.DocumentElement);
                }
                catch (Exception e)
                {
                    //TODO
                    System.Windows.MessageBox.Show(e.ToString());
                    System.Windows.MessageBox.Show(e.StackTrace);
                }

                return comp;
            }
        }

        public static IvyComponent parseXMLComponent(XmlNode e)
        {
            IvyComponent comp = null;

            if (e.Name.Equals("Grid"))
            {
                comp = parseXMLGrid(e);
            }
            else if (e.Name.Equals("Button"))
            {
                comp = parseXMLButton(e);
            }
            else if (e.Name.Equals("Label"))
            {
                comp = parseXMLLabel(e);
            }
            else if (e.Name.Equals("List"))
            {
                comp = parseXMLList(e);
            }

            return comp;
        }

        //--- TEXT COMPONENT

        private static IvyButton parseXMLButton(XmlNode e)
        {
            IvyButton button = new IvyButton();

            foreach (XmlAttribute attr in e.Attributes)
            {
                if (getComponentAttributes(button, attr))
                {
                    continue;
                }

                if (getTextComponentAttributes(button, attr))
                {
                    continue;
                }
            }

            //TODO
            return button;
        }

        private static IvyLabel parseXMLLabel(XmlNode e)
        {
            IvyLabel label = new IvyLabel();

            foreach (XmlAttribute attr in e.Attributes)
            {
                if (getComponentAttributes(label, attr))
                {
                    continue;
                }

                if (getTextComponentAttributes(label, attr))
                {
                    continue;
                }
            }

            return label;
        }

        private static IvyGrid parseXMLGrid(XmlNode e)
        {
            IvyGrid grid = new IvyGrid(1, 1);
            string value;
            IvyComponent compChild;


            foreach (XmlAttribute attr in e.Attributes)
            {
                /* Component Attributes */

                if (getComponentAttributes(grid, attr))
                {
                    continue;
                }

                /* Grid Attributes */

                if (attr.Name.Equals("rows"))
                {
                    value = attr.Value;

                    try
                    {
                        grid.setRows(int.Parse(value));
                    }
                    catch (Exception ex)
                    {
                        throw new InvalidAttributeTypeException();
                    }
                }
                else if (attr.Name.Equals("cols"))
                {
                    value = attr.Value;

                    try
                    {
                        grid.setCols(int.Parse(value));
                    }
                    catch (Exception ex)
                    {
                        throw new InvalidAttributeTypeException();
                    }
                }
            }

            if (e.HasChildNodes)
            {
                foreach (XmlNode child in e.ChildNodes)
                {
                    compChild = parseXMLComponent(child);
                    grid.setComponentAt(compChild, compChild.getRow(), compChild.getCol());
                }
            }

            return grid;
        }

        private static IvyList parseXMLList(XmlNode e)
        {
            IvyList list = new IvyList();

            foreach (XmlAttribute attr in e.Attributes)
            {
                if (getComponentAttributes(list, attr))
                {
                    continue;
                }
            }

            return list;
        }

        private static bool getTextComponentAttributes(IvyTextComponent comp, XmlAttribute attr)
        {
            String value = attr.Value;

            if (attr.Name.Equals("text"))
            {
                comp.setText(value);
            }
            else if (attr.Name.Equals("textAlignment"))
            {
                if (value == string.Empty)
                {
                    throw new EmptyAttributeValueException();
                }

                if (value.Equals("left"))
                {
                    comp.setTextAlignment(IvyComponent.Alignment.LEFT);
                }
                else if (value.Equals("center"))
                {
                    comp.setTextAlignment(IvyComponent.Alignment.CENTER);
                }
                else if (value.Equals("right"))
                {
                    comp.setTextAlignment(IvyComponent.Alignment.RIGHT);
                }
                else
                {
                    throw new InvalidAttributeException();
                }
            }
            else if (attr.Name.Equals("verticalTextAlignment"))
            {
                if (value == string.Empty)
                {
                    throw new EmptyAttributeValueException();
                }

                if (value.Equals("top"))
                {
                    comp.setVerticalTextAlignment(IvyComponent.Alignment.TOP);
                }
                else if (value.Equals("center"))
                {
                    comp.setVerticalTextAlignment(IvyComponent.Alignment.CENTER);
                }
                else if (value.Equals("right"))
                {
                    comp.setVerticalTextAlignment(IvyComponent.Alignment.BOTTOM);
                }
                else
                {
                    throw new InvalidAttributeException();
                }
            }
            else if (attr.Name.Equals("textColor"))
            {
                if (value == string.Empty)
                {
                    throw new EmptyAttributeValueException();
                }

                try
                {
                    comp.setTextColor((SolidColorBrush)new BrushConverter().ConvertFromString(value));
                }
                catch (Exception ex)
                {
                    throw new InvalidAttributeException();
                }
            }
            else if (attr.Name.Equals("padding"))
            {
                value = attr.Value;

                if (value == string.Empty)
                {
                    throw new EmptyAttributeValueException();
                }

                String[] paddingValues = value.Split(',');

                for (int i = 0; i < paddingValues.Length; i++)
                {
                    paddingValues[i] = paddingValues[i].Trim();
                }

                if (paddingValues.Length == 1)
                {
                    try
                    {
                        comp.setPadding(int.Parse(paddingValues[0]));
                    }
                    catch (Exception ex)
                    {
                        throw new InvalidAttributeTypeException();
                    }
                }
                else if (paddingValues.Length == 2)
                {
                    try
                    {
                        comp.setPadding(int.Parse(paddingValues[0]),
                                int.Parse(paddingValues[1]));
                    }
                    catch (Exception ex)
                    {
                        throw new InvalidAttributeTypeException();
                    }
                }
                else if (paddingValues.Length == 4)
                {
                    try
                    {
                        comp.setPadding(int.Parse(paddingValues[0]),
                                int.Parse(paddingValues[1]),
                                int.Parse(paddingValues[2]),
                                int.Parse(paddingValues[3]));
                    }
                    catch (Exception ex)
                    {
                        throw new InvalidAttributeTypeException();
                    }
                }
                else
                {
                    throw new InvalidAttributeValueException();
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        private static bool getComponentAttributes(IvyComponent comp, XmlAttribute attr)
        {
            String value = attr.Value;

            if (attr.Name.Equals("name"))
            {
                value = attr.Value;

                if (value == string.Empty)
                {
                    throw new EmptyAttributeValueException();
                }

                if (names.Contains(value))
                {
                    throw new DuplicateNameException();
                }

                comp.setName(value);
            }
            else if (attr.Name.Equals("width"))
            {
                value = attr.Value;

                if (value == string.Empty)
                {
                    throw new EmptyAttributeValueException();
                }

                if (value.Equals("*"))
                {
                    comp.setWidthType(IvyComponent.DimensionParameters.FILL);
                }
                else if (value.Equals("auto"))
                {
                    comp.setWidthType(IvyComponent.DimensionParameters.AUTO);
                }
                else
                {
                    comp.setWidthType(IvyComponent.DimensionParameters.FIXED);

                    try
                    {
                        comp.setWidth(int.Parse(value));
                    }
                    catch (Exception ex)
                    {
                        throw new InvalidAttributeTypeException();
                    }
                }
            }
            else if (attr.Name.Equals("height"))
            {
                value = attr.Value;

                if (value == string.Empty)
                {
                    throw new EmptyAttributeValueException();
                }

                if (value.Equals("*"))
                {
                    comp.setHeightType(IvyComponent.DimensionParameters.FILL);
                }
                else if (value.Equals("auto"))
                {
                    comp.setHeightType(IvyComponent.DimensionParameters.AUTO);
                }
                else
                {
                    comp.setHeightType(IvyComponent.DimensionParameters.FIXED);

                    try
                    {
                        comp.setHeight(int.Parse(value));
                    }
                    catch (Exception ex)
                    {
                        throw new InvalidAttributeTypeException();
                    }
                }
            }
            else if (attr.Name.Equals("margin"))
            {
                value = attr.Value;

                if (value == string.Empty)
                {
                    throw new EmptyAttributeValueException();
                }

                String[] marginValues = value.Split(',');

                for (int i = 0; i < marginValues.Length; i++)
                {
                    marginValues[i] = marginValues[i].Trim();
                }

                if (marginValues.Length == 1)
                {
                    try
                    {
                        comp.setMargin(int.Parse(marginValues[0]));
                    }
                    catch (Exception ex)
                    {
                        throw new InvalidAttributeTypeException();
                    }
                }
                else if (marginValues.Length == 2)
                {
                    try
                    {
                        comp.setMargin(int.Parse(marginValues[0]),
                                int.Parse(marginValues[1]));
                    }
                    catch (Exception ex)
                    {
                        throw new InvalidAttributeTypeException();
                    }
                }
                else if (marginValues.Length == 4)
                {
                    try
                    {
                        comp.setMargin(int.Parse(marginValues[0]),
                                int.Parse(marginValues[1]),
                                int.Parse(marginValues[2]),
                                int.Parse(marginValues[3]));
                    }
                    catch (Exception ex)
                    {
                        throw new InvalidAttributeTypeException();
                    }
                }
                else
                {
                    throw new InvalidAttributeValueException();
                }
            }
            else if (attr.Name.Equals("row"))
            {
                try
                {
                    comp.setRow(int.Parse(attr.Value));
                }
                catch (Exception e)
                {
                    throw new InvalidAttributeTypeException();
                }
            }
            else if (attr.Name.Equals("col"))
            {
                try
                {
                    comp.setCol(int.Parse(attr.Value));
                }
                catch (Exception ex)
                {
                    throw new InvalidAttributeTypeException();
                }
            }
            else if (attr.Name.Equals("rowSpan"))
            {
                try
                {
                    comp.setRowSpan(int.Parse(attr.Value));
                }
                catch (Exception ex)
                {
                    throw new InvalidAttributeTypeException();
                }
            }
            else if (attr.Name.Equals("colSpan"))
            {
                try
                {
                    comp.setColSpan(int.Parse(attr.Value));
                }
                catch (Exception ex)
                {
                    throw new InvalidAttributeTypeException();
                }
            }
            else if (attr.Name.Equals("border"))
            {
                value = attr.Value;

                if (value == string.Empty)
                {
                    throw new EmptyAttributeValueException();
                }

                if (value.Equals("none"))
                {
                    comp.setBorderType(IvyComponent.Border.NONE);
                }
                else if (value.Equals("squared"))
                {
                    comp.setBorderType(IvyComponent.Border.SQUARED);
                }
                else if (value.Equals("rounded"))
                {
                    comp.setBorderType(IvyComponent.Border.ROUNDED);
                }
                else
                {
                    throw new InvalidAttributeValueException();
                }
            }
            else if (attr.Name.Equals("borderColor"))
            {
                value = attr.Value;

                if (value == string.Empty)
                {
                    throw new EmptyAttributeValueException();
                }

                /* TODO */
            }
            else if (attr.Name.Equals("backgroundColor"))
            {
                value = attr.Value;

                if (value == string.Empty)
                {
                    throw new EmptyAttributeValueException();
                }

                try
                {
                    comp.setBackgroundColor((SolidColorBrush)new BrushConverter().ConvertFromString(value));
                }
                catch (Exception ex)
                {
                    throw new InvalidAttributeException();
                }
            }
            else if (attr.Name.Equals("backgroundImage"))
            {
                value = attr.Value;

                if (value == string.Empty)
                {
                    throw new EmptyAttributeValueException();
                }

                try
                {
                    comp.setBackgroundImage(value);
                }
                catch (Exception ex)
                {
                    throw new NoSuchResourceException();
                }
            }
            else if (attr.Name.Equals("visible"))
            {
                value = attr.Value;

                if (value == string.Empty)
                {
                    throw new EmptyAttributeValueException();
                }

                try
                {
                    comp.setVisible(bool.Parse(value));
                }
                catch (Exception ex)
                {
                    throw new InvalidAttributeTypeException();
                }
            }
            else
            {
                return false;
            }

            return true;
        }
    }
}
