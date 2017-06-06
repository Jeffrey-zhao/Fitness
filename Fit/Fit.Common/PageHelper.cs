using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Common
{
  public class PageHelper
  {
    public const int PAGE_COUNT = 5;

    public static int CurrentPage { get; set; }
    public static int TotalPage { get; set; }
    public static long TotalCount { get; set; }

    private static int startPage, endPage;
    private static string hrefStr = "/AdminUser/List?pageIndex=";
    private static string firstPageAttr = string.Empty, lastPageAttr = string.Empty
                                      , prevPageAttr = string.Empty, nextPageAttr = string.Empty
                                      , currPageAttr = string.Empty;

    internal static void CalcTotalPage()
    {
      TotalPage = (int)Math.Ceiling(TotalCount * 1.0 / Consts.PAGE_SIZE_NUM);
    }

    private static void CoreAlgorithm()
    {
      if (CurrentPage <= PAGE_COUNT / 2)
      {
        startPage = 1;
        endPage = startPage + PAGE_COUNT - 1;
      }
      else if (CurrentPage >= TotalPage - PAGE_COUNT / 2)
      {
        endPage = TotalPage;
        startPage = endPage - PAGE_COUNT + 1;
      }
      else
      {
        startPage = CurrentPage - PAGE_COUNT / 2;
        endPage = startPage + PAGE_COUNT - 1;
      }
      if (startPage <= 1)
      {
        startPage = 1;
        firstPageAttr = "disabled";
        prevPageAttr = "disabled";
      }
      if (endPage >= TotalPage)
      {
        endPage = TotalPage;
        lastPageAttr = "disabled";
        nextPageAttr = "disabled";
      }
    }

    public static string GetHtmlPager()
    {
      CoreAlgorithm();

      StringBuilder strB = new StringBuilder();
      strB.Append(" <ul class='pagination pull-right'>")
        .AppendFormat("<li class='footable-page-arrow {0}'><a href='{1}'>«</a></li>", firstPageAttr, hrefStr + 1)
        .AppendFormat("<li class='footable-page-arrow {0}'><a href='{1}'>‹</a></li>", prevPageAttr, hrefStr + (CurrentPage - 1));
      for (int i = startPage; i <= endPage; i++)
      {
        if (i == CurrentPage) currPageAttr = "active";
        strB.AppendFormat("<li class='footable-page {0}'><a href='{1}'>{2}</a></li>", currPageAttr, hrefStr + i, i);
      }
      strB.AppendFormat("<li class='footable-page-arrow {0}'><a href='{1}'>›</a></li>", nextPageAttr, hrefStr + (CurrentPage + 1))
       .AppendFormat("<li class='footable-page-arrow {0}'><a href='{1}'>»</a></li>", lastPageAttr, hrefStr + TotalPage)
       .Append("</ul>");
      return strB.ToString();
    }

    public static void Reset()
    {
      startPage = 0;
      endPage = 0;
      CurrentPage = 0;
      TotalCount = 0;
      TotalPage = 0;
      firstPageAttr = string.Empty;
      lastPageAttr = string.Empty;
      prevPageAttr = string.Empty;
      nextPageAttr = string.Empty;
      currPageAttr = string.Empty;
    }

    /// <summary>
    /// 用于测试页面范围的计算
    /// </summary>
    /// <returns></returns>
    internal static string ForTestPageRange()
    {
      CoreAlgorithm();
      string temp = string.Empty;
      for (var i = startPage; i <= endPage; i++)
      {
        temp += i.ToString();
      }
      return temp;
    }
    internal static string FotTestAttr()
    {
      return string.Format("firstPageAttr:{0},prevPageAttr:{1},nextPageAttr:{2},lastPageAttr:{3}"
                                            , firstPageAttr, prevPageAttr, nextPageAttr, lastPageAttr);
    }
  }
}
