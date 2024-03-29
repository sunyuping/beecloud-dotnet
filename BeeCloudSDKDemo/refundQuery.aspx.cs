﻿using BeeCloud;
using BeeCloud.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BeeCloudSDKDemo
{
    public partial class refundQuery : System.Web.UI.Page
    {
        private static List<BCRefund> refunds = new List<BCRefund>();
        private static string typeChannel;

        protected void Page_Load(object sender, EventArgs e)
        {
            string type = Request.Form["querytype"];

            if (type == "alirefundquery")
            {
                typeChannel = "Ali";
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + "Ali" + "</span><br/>");
                BCRefundQuerytResult result = BCPay.BCRefundQueryByCondition("ALI", null, null, null, null, null, 50);
                refunds = result.refunds;
            }
            if (type == "wxrefundquery")
            {
                typeChannel = "WX";
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + "WX" + "</span><br/>");
                BCRefundQuerytResult result = BCPay.BCRefundQueryByCondition("WX", null, null, null, null, null, 50);
                refunds = result.refunds;
            }
            if (type == "unionrefundquery")
            {
                typeChannel = "UN";
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + "UN" + "</span><br/>");
                BCRefundQuerytResult result = BCPay.BCRefundQueryByCondition("UN", null, null, null, null, null, 50);
                refunds = result.refunds;
            }
            this.bind();
        }

        protected void bind()
        {
            RefundQueryGridView.DataSource = refunds;
            RefundQueryGridView.DataBind();
        }

        protected void QueryGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (typeChannel != "WX" || e.Row.Cells[5].Text != "False")
            {
                e.Row.Cells[8].Visible = false;
            }
        }

        protected void RefundQueryGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "refundStatus")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                string refundNo = refunds[rowIndex].refundNo.ToString();
                BCRefundStatusQueryResult result = BCPay.BCRefundStatusQuery("WX", refundNo);
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.resultCode + "</span><br/>");
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.resultMsg + "</span><br/>");
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.errDetail + "</span><br/>");
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.refundStatus + "</span><br/>");
            }
        }
    }
}