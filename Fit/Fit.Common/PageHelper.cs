using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Common
{
  public class PageHelper
  {
    public const int PAGE_COUNT = 5;  //只能是奇数

    public static int CurrentPage { get; set; }
    public static int TotalPage { get; set; }
    public static long TotalCount { get; set; }
    public static string HrefStr { get; set; } = "/AdminUser/List?pageIndex=";
    private static int startPage, endPage;
    private static string firstPageAttr = string.Empty, lastPageAttr = string.Empty
                                      , prevPageAttr = string.Empty, nextPageAttr = string.Empty
                                      , firstPageHref = string.Empty, lastPageHref = string.Empty
                                      , prevPageHref = string.Empty, nextPageHref = string.Empty;

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
      }
      if (endPage >= TotalPage)
      {
        endPage = TotalPage;
      }

      SetAttrAndHref();
    }
    private static void SetAttrAndHref()
    {
      firstPageHref = HrefStr + 1;
      prevPageHref = HrefStr + (CurrentPage - 1);
      lastPageHref = HrefStr + TotalPage;
      nextPageHref = HrefStr + (CurrentPage + 1);
      if (startPage <= 1)
      {
        firstPageAttr = "disabled";
        firstPageHref = "#";
      }
      if (endPage >= TotalPage)
      {
        lastPageAttr = "disabled";
        lastPageHref = "#";
      }
      if (CurrentPage <= 1)
      {
        prevPageAttr = "disabled";
        prevPageHref = "#";
      }
      if (CurrentPage >= TotalPage)
      {
        nextPageAttr = "disabled";
        nextPageHref = "#";
      }
    }

    public static string GetHtmlPager()
    {
      CalcTotalPage();
      CoreAlgorithm();

      StringBuilder strB = new StringBuilder();
      strB.Append(" <ul class='pagination pull-right'>")
        .AppendFormat("<li class='footable-page-arrow {0}'><a href='{1}'>«</a></li>"
          , firstPageAttr, firstPageHref)
        .AppendFormat("<li class='footable-page-arrow {0}'><a href='{1}'>‹</a></li>"
          , prevPageAttr, prevPageHref);
      for (int i = startPage; i <= endPage; i++)
      {
        var currPageAttr = string.Empty;
        if (i == CurrentPage) currPageAttr = "active";
        strB.AppendFormat("<li class='footable-page {0}'><a href='{1}' >{2}</a></li>"
            , currPageAttr, HrefStr + i, i);
      }
      strB.AppendFormat("<li class='footable-page-arrow {0}'><a href='{1}'>›</a></li>"
            , nextPageAttr, nextPageHref)
       .AppendFormat("<li class='footable-page-arrow {0}'><a href='{1}'>»</a></li>"
            , lastPageAttr, lastPageHref)
       .Append("</ul>");

      return strB.ToString();
    }

    public static void Reset()
    {
      TotalCount = 0;
      startPage = endPage = CurrentPage = TotalPage = 0;
      firstPageAttr = lastPageAttr = prevPageAttr = nextPageAttr = string.Empty;
      firstPageHref = lastPageHref = prevPageHref = nextPageHref = string.Empty;
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
      return
        string.Format("firstPageAttr:{0},prevPageAttr:{1},nextPageAttr:{2},lastPageAttr:{3}"
                                            , firstPageAttr, prevPageAttr, nextPageAttr, lastPageAttr);
    }
  }
}
