using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Luza_Project.Helper
{
    public static class AppUtilities
    {
        public enum ResponseCode
        {
            Success = 0,
            Failed = 1,
            Warning = 2,
            Exception = 9
        }
        public class CommonDbResponse
        {
            public ResponseCode Code { get; set; }

            public int ErrorCode { get; set; }
            public string Message { get; set; }
            public string Id { get; set; }
            public string Extra1 { get; set; }
            public string Extra2 { get; set; }
            public object Data { get; set; }
            public void SetMessage(int Code, string Message, string Extra1 = "", String Extra2 = "", string Extra3 = "", string Extra4 = "", string Extra5 = "", DataTable dt = null, DataRow row = null)
            {
                switch (Code)
                {
                    case 1:
                        this.Code = ResponseCode.Failed;
                        break;
                    case 2:
                        this.Code = ResponseCode.Warning;
                        break;
                    case 0:
                        this.Code = ResponseCode.Success;
                        break;
                    default:
                        this.Code = ResponseCode.Exception;
                        break;
                }

                this.ErrorCode = Code;
                this.Message = Message;
                this.Extra1 = Extra1 ?? "";
                this.Extra2 = Extra2 ?? "";
                if (dt != null)
                    this.Data = dt;
                else
                    this.Data = row;
            }
            public void SetMessage(string Code, string Message, string Extra1 = "", String Extra2 = "", string Extra3 = "", string Extra4 = "", string Extra5 = "", DataTable dt = null, DataRow row = null)
            {
                int _code = 0;
                if (int.TryParse(Code.Trim(), out _code))
                {
                    SetMessage(_code, Message, Extra1, Extra2, Extra3, Extra4, Extra5);
                }
                else
                    SetMessage(9, "Invalid Response Code", Extra1, Extra2, Extra3, Extra4, Extra5);
                if (dt != null)
                    this.Data = dt;
                else
                    this.Data = row;
            }
        }
        public static void ShowPopup(this System.Web.Mvc.Controller Cont, Int32 code, string Message, string Title = "", string Extra1 = "")
        {
            CommonDbResponse dbresp = new CommonDbResponse();
            if (code == 0)
            {
                dbresp.Code = ResponseCode.Success;
            }
            else if (code == 2)
            {
                dbresp.Code = ResponseCode.Warning;
            }
            else if (code == 9)
            {
                dbresp.Code = ResponseCode.Exception;
            }
            else
                dbresp.Code = ResponseCode.Failed;
            dbresp.Extra2 = Title;
            dbresp.Message = Message;
            dbresp.Extra1 = Extra1;
        }
    }
}