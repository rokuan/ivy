using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;

namespace Ivy.Activity
{
    class ActivityManager
    {
        private static List<KeyValuePair<ActivityPage, Bundle>> pages = new List<KeyValuePair<ActivityPage,Bundle>>();

        public static void previousPage()
        {
            if (pages.Count == 0)
            {
                return;
            }

            //pages.Last().loadActivity();
        }

        public static void loadActivityPage(ActivityPage page, Bundle bundle)
        {
            if (page != null)
            {
                KeyValuePair<ActivityPage, Bundle> pairActBund = new KeyValuePair<ActivityPage,Bundle>(page, bundle);
                
                pages.Add(pairActBund);
                page.onStart(bundle);
            }
            else
            {
                //TODO
            }
        }
    }
}
