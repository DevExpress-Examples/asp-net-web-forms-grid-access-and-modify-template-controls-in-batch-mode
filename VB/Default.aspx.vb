Imports DevExpress.Data.Filtering
Imports DevExpress.Export
Imports DevExpress.Web
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraPrintingLinks
Imports DevExpress.XtraRichEdit
Imports DevExpress.XtraRichEdit.API.Native
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Configuration
Imports System.Data
Imports System.Diagnostics
Imports System.IO
Imports System.Linq
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls

Partial Public Class _Default
	Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
		AddHandler gridView.CellEditorInitialize, Sub(s, ea)
			ea.Editor.ReadOnly = False
		End Sub
		gridView.SettingsEditing.BatchEditSettings.AllowRegularDataItemTemplate = supportDataItemTemplate.Checked
	End Sub
	Protected Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs)
		Dim data = Enumerable.Range(1, 6).Select(Function(i) New With {
			Key .ID = i,
			Key .rate = i,
			Key .num = i,
			Key .progress = i*10
		}).ToList()

		gridView.DataSource = data
		gridView.DataBind()

		gridView.DataColumns("rate1").DataItemTemplate = New RatingControlTemplate()
		gridView.DataColumns("progress").DataItemTemplate = New ProgressControlTemplate()
	End Sub

	Private Class RatingControlTemplate
		Implements ITemplate

		Public Sub InstantiateIn(ByVal container As Control)
			Dim gridContainer As GridViewDataItemTemplateContainer = TryCast(container, GridViewDataItemTemplateContainer)
			If gridContainer IsNot Nothing Then
				Dim column As GridViewDataColumn = gridContainer.Column
				Dim divContainer As New System.Web.UI.HtmlControls.HtmlGenericControl()
				gridContainer.Controls.Add(divContainer)
				Dim rating As New ASPxRatingControl()
				divContainer.Controls.Add(rating)
				rating.ItemCount = 10
				rating.ReadOnly = True
				divContainer.Attributes.Add("onclick", String.Format("grid.batchEditApi.StartEdit({0}, 0)", gridContainer.VisibleIndex))
			End If
		End Sub
	End Class

	Private Class ProgressControlTemplate
		Implements ITemplate

		Public Sub InstantiateIn(ByVal container As Control)
			Dim gridContainer As GridViewDataItemTemplateContainer = TryCast(container, GridViewDataItemTemplateContainer)
			If gridContainer IsNot Nothing Then
				Dim column As GridViewDataColumn = gridContainer.Column
				Dim bar As New ASPxProgressBar()
				bar.BackColor = System.Drawing.Color.Azure
				bar.Maximum = 100
				bar.Width = 100
				bar.ReadOnly = True
				gridContainer.Controls.Add(bar)
			End If
		End Sub
	End Class
End Class
