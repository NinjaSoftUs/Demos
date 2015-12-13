using System.Collections.Generic;
using System.Diagnostics;
using Prism.Regions;

namespace NinjaSoft.DirectoryStatusCore.Extenstions
{
    public static class RegionExtensions
    {
        public static void RemoveView(this IRegion region, object view)
        {
            List<object> viewList = new List<object>();
            viewList.AddRange(region.Views);
            foreach (var targetView in viewList)
            {
                if (targetView == view)
                {
                    //region.Deactivate(view);
                    Debug.WriteLine("Remove " + view.ToString() + " from " + region.Name + " region");
                    region.Remove(view);
                }
            }
        }

        public static void ClearRegion(this IRegion region)
        {
            List<object> views = new List<object>();
            views.AddRange(region.Views);
            foreach (var view in views)
            {
                //region.Deactivate(view);
                Debug.WriteLine("Remove " + view.ToString() + " from " + region.Name + " region");
                region.Remove(view);
            }
        }

        public static void AddAndActivateIfNotExists(this IRegion region, object view)
        {
            if (!region.Views.Contains(view))
            {
                region.Add(view);
                region.Activate(view);
            }
        }

    

        public static void ShowView(this IRegion region, object view)
        {
            ClearRegion(region);
            // RemoveView(region,view);
            //Debug.WriteLine("View Injection: " + view.ToString() + " into " + region.ProjectName + " region");
            region.Add(view);
            region.Activate(view);
        }
    }
}