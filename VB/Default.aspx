<%@ Page Language="vb" AutoEventWireup="true" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head runat="server">
    <title>Sample</title>
	<script src="AppClientCode.js"></script>
</head>
<body>
	<form id="form1" runat="server">
        <div>

            <dx:ASPxCheckBox ID="supportDataItemTemplate" runat="server" AutoPostBack="true" Text="Support data item template" Checked="true" />
            <dx:ASPxGridView ID="gridView" runat="server" KeyFieldName="ID" AutoGenerateColumns="False" ClientInstanceName="grid">
                <Columns>
                    <dx:GridViewDataColumn Name="rate1" FieldName="rate" Width="100px"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Name="num" FieldName="num">
                        <DataItemTemplate>
							<div style="background-color: aquamarine; color:darkgreen; min-width: 50px; text-align: center; height: 100%" id="tmpl<%#Container.VisibleIndex%>"><%#Eval("num")%> </div>
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Name="num2" FieldName="num" />
                    <dx:GridViewDataColumn Name="progress" FieldName="progress" Width="100px"></dx:GridViewDataColumn>
                    <dx:GridViewCommandColumn ShowEditButton="true" />
                </Columns>
                <SettingsEditing Mode="Batch" />
                <ClientSideEvents BatchEditEndEditing="batchEndEdit" Init="gridInit" BatchEditChangesCanceling="onBatchEditChangesCanceling" />
            </dx:ASPxGridView>

            <asp:AccessDataSource ID="AccessDataSource1" runat="server" DataFile="~/App_Data/nwind.mdb"
                SelectCommand="SELECT [ProductID], [ProductName], [SupplierID], [CategoryID], [UnitPrice], [UnitsInStock], [UnitsOnOrder] FROM [Products]"></asp:AccessDataSource>
        </div>
    </form>
</body>
</html>