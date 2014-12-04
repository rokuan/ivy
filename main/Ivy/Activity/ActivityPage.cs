using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ivy.Components;
using Ivy.Exceptions;

namespace Ivy.Activity
{
    public abstract class ActivityPage
    {
        private Dictionary<string, IvyComponent> components;

        public ActivityPage()
        {
            components = new Dictionary<string, IvyComponent>();
        }

        public abstract void onStart(Bundle bundle);

        protected void loadContent(string xmlFile)
        {
            IvyComponent comp = IvyXMLParser.parseXML(xmlFile);

            if (comp != null)
            {
                addComponent(comp);
            }
        }

        private void addComponent(IvyComponent comp)
        {
            if (comp is IvyContainer)
            {
                IvyContainer cont = (IvyContainer)comp;
                List<IvyComponent> children = cont.getChildren();

                if (children != null)
                {
                    foreach (IvyComponent child in children)
                    {
                        addComponent(child);
                    }
                }
            }

            if (comp.hasName())
            {
                if (components.ContainsKey(comp.getName()))
                {
                    //Ne devrait pas arriver
                    throw new DuplicateNameException();
                }

                components.Add(comp.getName(), comp);
            }
        }

        protected IvyComponent findComponentByName(string name)
        {
            return components[name];
        }

        protected void toPage(ActivityPage page, Bundle bundle)
        {
            ActivityManager.loadActivityPage(page, bundle);
        }
    }
}
