using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using WebPCConfigTool.Model;

namespace WebPCConfigTool.Pages
{
    public partial class PCConfig : System.Web.UI.Page
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
            string idOs = (this.gridOS.SelectedDataKey != null) ? this.gridOS.SelectedDataKey.Value.ToString() : null;
            string idCpu = (this.gridCpus.SelectedDataKey != null) ? this.gridCpus.SelectedDataKey.Value.ToString() : null;
            string idVc= (this.gridCards.SelectedDataKey != null) ? this.gridCards.SelectedDataKey.Value.ToString() : null;
            e.InputParameters["idHDD"] = Convert.ToInt64(idHdd);
            e.InputParameters["idRAM"] = Convert.ToInt64(idRam);
            e.InputParameters["idOS"] = Convert.ToInt64(idOs);
            e.InputParameters["idCPU"] = Convert.ToInt64(idCpu);
            e.InputParameters["idVC"] = Convert.ToInt64(idVc);
        }

        protected void odsPcConfig_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            var res = e.ReturnValue as List<Component>;
            if (res != null && res.Count() > 0)
            {
                this.gridConfiguration.DataSource = res;
                this.totalPrice.Text = res.Sum(c => c.Price).ToString();
                this.gridConfiguration.DataBind();
            }
        }
    }
}