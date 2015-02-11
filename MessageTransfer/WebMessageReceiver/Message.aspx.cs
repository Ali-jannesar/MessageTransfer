using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;
using MessageReceiverWebService.classes;

namespace MessageReceiverWebService
{
    public partial class Message : System.Web.UI.Page
    {
        DataLayerClass DataLayer = new DataLayerClass();
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                FillGrid();
            }
        }


        private void FillGrid()
        {
            
            //Retrieve the messages from the database and show them in Gridview

            try
            {
                clear_error();
                DataTable Table = DataLayer.DataFill();
                BindData(Table);
            }
            catch (Exception Ex)
            {
                lbl_Error.Text = Ex.Message;
            }
        }


        private void BindData(DataTable Table)
        {
            //Binds a datatable to the Gridview
            
            if (Table != null && Table.Rows.Count > 0)
            {
                grid_Messages.DataSource = Table;
                grid_Messages.DataBind();
            }
            else
            {
                ShowEmptyGrid();
            }


        }

        private void ShowEmptyGrid()
        {
           //Create a Data table and binds it to the Gridview in case the database is empty

            DataTable dtempty = new DataTable();
            dtempty.Columns.Add("RowID", typeof(string));
            dtempty.Columns.Add("DateTime", typeof(string));
            dtempty.Columns.Add("Message", typeof(string));
            dtempty.Rows.Add(dtempty.NewRow());
            grid_Messages.DataSource = dtempty;
            grid_Messages.DataBind();
            int TotalColumns = grid_Messages.Rows[0].Cells.Count;
            grid_Messages.Rows[0].Cells.Clear();
            grid_Messages.Rows[0].Cells.Add(new TableCell());
            grid_Messages.Rows[0].Cells[0].ColumnSpan = TotalColumns;
            grid_Messages.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            grid_Messages.Rows[0].Cells[0].ForeColor = System.Drawing.Color.Orange;
            grid_Messages.Rows[0].Cells[0].Font.Size = 20;
            grid_Messages.Rows[0].Cells[0].Font.Bold = true;
            grid_Messages.Rows[0].Cells[0].Text = "No Message Found";
        }


        private void clear_error()
        {
            //Clears the error lable 

            lbl_Error.Text = "";
        }

        protected void grid_Messages_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //Deleting messages from Gridview
            
            try
            {
                DataLayerClass DataLayer = new DataLayerClass();
                clear_error();
                int id = int.Parse((grid_Messages.DataKeys[e.RowIndex].Value).ToString());
                DataLayer.DeleteMessage(id);
                FillGrid();
            }
            catch(Exception Ex)
            {
                lbl_Error.Text = Ex.Message;
            }

        }

        protected void button_Refresh_Click(object sender, EventArgs e)
        {
            //Refresh the Gridview by calling FillGrid Method

            FillGrid();
        }

   
    }
}