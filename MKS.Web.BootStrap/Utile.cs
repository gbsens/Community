using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace MKS.Web.BootStrap
{
    public static class GridView
    {
        public static void AssignColumName(DataControlFieldCollection col, string name, object value )
        {
            for (int i = 0; i < col.Count; i++)
            {
                if(col[i].HeaderText==name)
                {
                     col[i].HeaderText=value as string;
                }
            }
       
        }
        public static List<ListItem> BindPager(int totalRecordCount, int currentPageIndex, int pageSize)
        {
            double getPageCount = (double)((decimal)totalRecordCount / (decimal)pageSize);
            int pageCount = (int)Math.Ceiling(getPageCount);
            List<ListItem> pages = new List<ListItem>();
            if (pageCount > 1)
            {
                pages.Add(new ListItem("<<", "1", currentPageIndex > 1));
                for (int i = 1; i <= pageCount; i++)
                {
                    pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPageIndex + 1));
                }
                pages.Add(new ListItem(">>", pageCount.ToString(), currentPageIndex < pageCount - 1));
            }

            return pages;
        }
    }

}
