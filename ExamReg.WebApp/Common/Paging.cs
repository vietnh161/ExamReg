using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamReg.WebApp.Common
{
  public class Paging
  {
    public int currentPage { set; get; }
    public int pageSize { set; get; }
    public string sort { set; get; }
  }
}
