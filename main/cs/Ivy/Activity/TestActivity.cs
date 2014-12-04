using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ivy.Activity
{
    public class TestActivity : BaseActivity
    {
        public TestActivity(int maxPlayers) : base(maxPlayers)
        {

        }

        public override bool onAcceleratorEvent(Event.AccelerometerEvent ae)
        {
            System.Windows.MessageBox.Show(ae.ToString());
            return true;
        }

        public override bool onPadEvent(Event.PadEvent pe)
        {
            System.Windows.MessageBox.Show(pe.ToString());
            return true;
        }

        public override bool onTouchEvent(Event.TouchEvent te)
        {
            System.Windows.MessageBox.Show(te.ToString());
            return true;
        }
    }
}
