using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamReg.WebApp.Common
{
  public class ListResult<T>
  {
    public IEnumerable<T> result { set; get; }
    public int totalRow { set; get; }
  }
}
