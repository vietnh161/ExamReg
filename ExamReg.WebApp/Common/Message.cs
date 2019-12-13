using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamReg.WebApp.Common
{
  public class Message
  {
    public string message { set; get; }
    public int successCount { set; get; }
    public int notSuccessCount { set; get; }
    public Message()
    {
      message = "";
      successCount = 0;
      notSuccessCount = 0;
    }
  }
}
