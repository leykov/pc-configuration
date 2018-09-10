using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebPCConfigTool.Pages
{
    public partial class Components : System.Web.UI.Page
    {
        private bool SendCompToLastPage = false;
        private long delId = -1;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Comps_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Insert data if the CommandName == "Insert" and the validation controls indicate valid data...
            try
            {
                GridView gridView = sender as GridView;
                ObjectDataSource ods = gridView.DataSourceObject as ObjectDataSource;

                if (e.CommandName == "Insert" && Page.IsValid)
                {
                    // Insert new record
                    ods.Insert();
                    // Indicate that the user needs to be sent to the last page
                    SendCompToLastPage = true;
                }
                if (e.CommandName == "Delete")
                {
                    // Insert new record
                    int idx = Convert.ToInt32(e.CommandArgument);
                    Label lblId = (Label)gridView.Rows[idx].FindControl("lblId");
                    delId = Convert.ToInt64(lblId.Text);
                    ods.Delete();
                    SendCompToLastPage = true;
                }

            }
            catch (Exception serviceEx)
            {
                SendCompToLastPage = false;
                Control grid = (Control)sender;
                ScriptManager.RegisterClientScriptBlock(this, grid.GetType(), "msg", $"alert('{serviceEx.InnerException.Message}');", true);
            }
        }

        protected void GridView_DataBound(object sender, EventArgs e)
        {
            // Send user to last page of data, if needed
            GridView gridView = sender as GridView;

            if (SendCompToLastPage && gridView.PageCount>0)
                gridView.PageIndex = gridView.PageCount - 1;
        }

        protected void odsRams_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            this.SetInputParams(this.gridRams, e);
        }

        protected void odsHdds_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            this.SetInputParams(this.gridHdds, e);
        }

        protected void odsOss_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            this.SetInputParams(this.gridOss, e);
        }

        /// <summary>
        /// Set input params for INSERT method.
        /// </summary>
        /// <param name="gridView"></param>
        /// <param name="e"></param>
        private void SetInputParams(GridView gridView, ObjectDataSourceMethodEventArgs e)
        {
            TextBox txtName = (TextBox)gridView.FooterRow.FindControl("NewName");
            TextBox txtPrice = (TextBox)gridView.FooterRow.FindControl("NewPrice");
            DropDownList type = (DropDownList)gridView.FooterRow.FindControl("NewType");

            // Set the ObjectDataSource's InsertParameters values...
            e.InputParameters["name"] = txtName.Text;
            e.InputParameters["price"] = Convert.ToDecimal(txtPrice.Text);
            e.InputParameters["compType"] = Convert.ToInt32(type.SelectedValue);
        }

        protected void Comps_Deleting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            e.InputParameters["id"] = delId;
            delId = -1;
        }

        protected string GetEnumDescription(object enumType)
        {

            var value = (Enum)enumType;

            if (value==null)
                throw new ArgumentException("Enumeration type is expected.");


            return value.GetDescription();
        }

    }
}