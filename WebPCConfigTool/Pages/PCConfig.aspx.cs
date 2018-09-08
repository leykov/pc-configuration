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
            this.btnConfirm.ForeColor = System.Drawing.Color.LightGray;
        }

        protected void Grid_SelectedIndexChanged(object sender, EventArgs e)
        {
            odsPcConfig.Select();
        }

        protected void odsPcConfig_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            var res = e.ReturnValue as List<Component>;
            if (res != null && res.Count() > 0)
            {
                this.gridConfiguration.DataSource = res;
                this.btnConfirm.Enabled = res.Count() >= 5;
                if (btnConfirm.Enabled)
                {
                    btnConfirm.ForeColor = System.Drawing.Color.Black;
                }
                this.totalPrice.Text = res.Sum(c => c.Price).ToString();
                this.gridConfiguration.DataBind();
            }
        }

        protected void odsPcConfig_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            this.gridConfiguration.DataSource = null;
            this.btnConfirm.Enabled = false;
            this.btnConfirm.ForeColor = System.Drawing.Color.LightGray;
            this.totalPrice.Text = "";
            // unselect all gridViews
            this.gridCards.SelectedIndex = -1;
            this.gridHDD.SelectedIndex = -1;
            this.gridCpus.SelectedIndex = -1;
            this.gridOS.SelectedIndex = -1;
            this.gridRAM.SelectedIndex = -1;
            this.gridConfiguration.DataBind();
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            //var comRepo = new ComponentRepository();
            //var rows = this.gridConfiguration.Rows;
            //foreach (var item in rows)
            //{
            //    GridViewRow comp = item as GridViewRow;
            //    TextBox txtQ = (TextBox)comp.FindControl("txtQuantity");
            //}
            odsPcConfig.Insert();
        }

        protected void odsPcConfig_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            SetParameters(e, true);
        }

        protected void odsPcConfig_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            SetParameters(e, false);
        }

        private void SetParameters(object eventArgs, bool isInsert)
        {
            var e = (isInsert)
                ? eventArgs as ObjectDataSourceMethodEventArgs
                : eventArgs as ObjectDataSourceSelectingEventArgs;

            string idHdd = (this.gridHDD.SelectedDataKey != null) ? this.gridHDD.SelectedDataKey.Value.ToString() : null;
            string idRam = (this.gridRAM.SelectedDataKey != null) ? this.gridRAM.SelectedDataKey.Value.ToString() : null;
            string idOs = (this.gridOS.SelectedDataKey != null) ? this.gridOS.SelectedDataKey.Value.ToString() : null;
            string idCpu = (this.gridCpus.SelectedDataKey != null) ? this.gridCpus.SelectedDataKey.Value.ToString() : null;
            string idVc = (this.gridCards.SelectedDataKey != null) ? this.gridCards.SelectedDataKey.Value.ToString() : null;
            e.InputParameters["idHDD"] = Convert.ToInt64(idHdd);
            e.InputParameters["idRAM"] = Convert.ToInt64(idRam);
            e.InputParameters["idOS"] = Convert.ToInt64(idOs);
            e.InputParameters["idCPU"] = Convert.ToInt64(idCpu);
            e.InputParameters["idVC"] = Convert.ToInt64(idVc);
        }
    }
}