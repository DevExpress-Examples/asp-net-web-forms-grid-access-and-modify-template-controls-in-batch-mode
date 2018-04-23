using DevExpress.Data.Filtering;
using DevExpress.Export;
using DevExpress.Web;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        gridView.CellEditorInitialize += (s, ea) => { ea.Editor.ReadOnly = false; };
        gridView.SettingsEditing.BatchEditSettings.AllowRegularDataItemTemplate = supportDataItemTemplate.Checked;
    }
    protected void Page_Init(object sender, EventArgs e)
    {
        var data = Enumerable.Range(1, 6).Select(i => new { ID = i, rate = i, num = i, progress = i*10 }).ToList();

        gridView.DataSource = data;
        gridView.DataBind();

        gridView.DataColumns["rate1"].DataItemTemplate = new RatingControlTemplate();
        gridView.DataColumns["progress"].DataItemTemplate = new ProgressControlTemplate();
    }

    private class RatingControlTemplate : ITemplate
    {
        public void InstantiateIn(Control container)
        {
            GridViewDataItemTemplateContainer gridContainer = container as GridViewDataItemTemplateContainer;
            if (gridContainer != null)
            {
                GridViewDataColumn column = gridContainer.Column;
                ASPxRatingControl rating = new ASPxRatingControl();
                rating.ItemCount = 10;
                rating.ReadOnly = true;
                gridContainer.Controls.Add(rating);
            }
        }
    }

    private class ProgressControlTemplate : ITemplate
    {
        public void InstantiateIn(Control container)
        {
            GridViewDataItemTemplateContainer gridContainer = container as GridViewDataItemTemplateContainer;
            if (gridContainer != null)
            {
                GridViewDataColumn column = gridContainer.Column;
                ASPxProgressBar bar = new ASPxProgressBar();
                bar.BackColor = System.Drawing.Color.Azure;
                bar.Maximum = 100;
                bar.Width = 100;
                bar.ReadOnly = true;
                gridContainer.Controls.Add(bar);
            }
        }
    }
}