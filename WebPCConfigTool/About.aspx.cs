﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebPCConfigTool.Model;

namespace WebPCConfigTool
{
    public partial class About : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Grid_SelectedIndexChanged(object sender, EventArgs e)
        {
            odsPcConfig.Select();
        }

        protected void odsPcConfig_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            string idHdd = (this.gridHDD.SelectedDataKey != null) ? this.gridHDD.SelectedDataKey.Value.ToString() : null;
            string idRam = (this.gridRAM.SelectedDataKey != null) ? this.gridRAM.SelectedDataKey.Value.ToString() : null;
            e.InputParameters["idHDD"] = Convert.ToInt64(idHdd);
            e.InputParameters["idRAM"] = Convert.ToInt64(idRam);
        }

        protected void odsPcConfig_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            var res = e.ReturnValue as List<Component>;
            if (res!=null && res.Count()>0)
            {
                this.gridConfiguration.DataSource = res;
                this.totalPrice.Text = res.Sum(c => c.Price).ToString();
                this.gridConfiguration.DataBind();
            }

        }
    }
}