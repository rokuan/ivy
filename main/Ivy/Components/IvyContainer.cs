
using System.Collections.Generic;
namespace Ivy.Components
{
    public abstract class IvyContainer : IvyComponent
    {
        public abstract void enterContainer();
        public abstract void nextComponent(Movement m);

        public abstract List<IvyComponent> getChildren();
    }
}
