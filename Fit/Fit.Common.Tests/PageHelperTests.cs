using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Common.Tests
{
  [TestFixture]
  public class PageHelperTests
  {
    [Test]
    [Ignore("")]
    public void GetPagerHtml()
    {
      string result, attr;
      PageHelper.CurrentPage = 3;
      PageHelper.TotalPage = 8;
      result = PageHelper.ForTestPageRange();
      attr = PageHelper.FotTestAttr();
      Assert.AreEqual("12345", result, "1");
      Assert.AreEqual(attr, string.Format("firstPageAttr:{0},prevPageAttr:{1},nextPageAttr:{2},lastPageAttr:{3}"
            , "disabled", "disabled", string.Empty, string.Empty), "11");
      PageHelper.Reset();

      PageHelper.CurrentPage = 3;
      PageHelper.TotalPage = 4;
      result = PageHelper.ForTestPageRange();
      attr = PageHelper.FotTestAttr();
      Assert.AreEqual("1234", result, "2");
      Assert.AreEqual(attr, string.Format("firstPageAttr:{0},prevPageAttr:{1},nextPageAttr:{2},lastPageAttr:{3}"
            , "disabled", "disabled", "disabled", "disabled"), "22");
      PageHelper.Reset();

      PageHelper.CurrentPage = 2;
      PageHelper.TotalPage = 6;
      result = PageHelper.ForTestPageRange();
      attr = PageHelper.FotTestAttr();
      Assert.AreEqual("12345", result, "3");
      Assert.AreEqual(attr, string.Format("firstPageAttr:{0},prevPageAttr:{1},nextPageAttr:{2},lastPageAttr:{3}"
            , "disabled", "disabled", string.Empty, string.Empty), "33");
      PageHelper.Reset();

      PageHelper.CurrentPage = 1;
      PageHelper.TotalPage = 6;
      result = PageHelper.ForTestPageRange();
      attr = PageHelper.FotTestAttr();
      Assert.AreEqual("12345", result, "4");
      Assert.AreEqual(attr, string.Format("firstPageAttr:{0},prevPageAttr:{1},nextPageAttr:{2},lastPageAttr:{3}"
            , "disabled", "disabled", string.Empty, string.Empty), "44");
      PageHelper.Reset();

      PageHelper.CurrentPage = 1;
      PageHelper.TotalPage = 3;
      result = PageHelper.ForTestPageRange();
      attr = PageHelper.FotTestAttr();
      Assert.AreEqual("123", result, "5");
      Assert.AreEqual(attr, string.Format("firstPageAttr:{0},prevPageAttr:{1},nextPageAttr:{2},lastPageAttr:{3}"
            , "disabled", "disabled", "disabled", "disabled"), "55");
      PageHelper.Reset();

      PageHelper.CurrentPage = 6;
      PageHelper.TotalPage = 6;
      result = PageHelper.ForTestPageRange();
      attr = PageHelper.FotTestAttr();
      Assert.AreEqual("23456", result, "6");
      Assert.AreEqual(attr, string.Format("firstPageAttr:{0},prevPageAttr:{1},nextPageAttr:{2},lastPageAttr:{3}"
            , string.Empty, string.Empty, "disabled", "disabled"), "66");
      PageHelper.Reset();

      PageHelper.CurrentPage = 1;
      PageHelper.TotalPage = 1;
      result = PageHelper.ForTestPageRange();
      attr = PageHelper.FotTestAttr();
      Assert.AreEqual("1", result, "7");
      Assert.AreEqual(attr, string.Format("firstPageAttr:{0},prevPageAttr:{1},nextPageAttr:{2},lastPageAttr:{3}"
            , "disabled", "disabled", "disabled", "disabled"), "77");
      var html = PageHelper.GetHtmlPager();
      PageHelper.Reset();
    }
    [Test]
    public void CalcTotalPage()
    {
      PageHelper.TotalCount = 21;
      PageHelper.CurrentPage = 3;
      PageHelper.CalcTotalPage();
      Assert.AreEqual(CalcExpect(PageHelper.TotalCount), PageHelper.TotalPage, "1");
      PageHelper.Reset();

      PageHelper.TotalCount = 11;
      PageHelper.CurrentPage = 3;
      PageHelper.CalcTotalPage();
      Assert.AreEqual(CalcExpect(PageHelper.TotalCount), PageHelper.TotalPage, "2");
      PageHelper.Reset();

      PageHelper.TotalCount = 7;
      PageHelper.CurrentPage = 3;
      PageHelper.CalcTotalPage();
      Assert.AreEqual(CalcExpect(PageHelper.TotalCount), PageHelper.TotalPage, "3");
      PageHelper.Reset();

      PageHelper.TotalCount = 2;
      PageHelper.CurrentPage = 1;
      PageHelper.CalcTotalPage();
      Assert.AreEqual(CalcExpect(PageHelper.TotalCount), PageHelper.TotalPage, "4");
      PageHelper.Reset();
    }

    private int CalcExpect(long count)
    {
      return (int)Math.Ceiling(count * 1.0 / Consts.PAGE_SIZE_NUM);
    }
  }
}
