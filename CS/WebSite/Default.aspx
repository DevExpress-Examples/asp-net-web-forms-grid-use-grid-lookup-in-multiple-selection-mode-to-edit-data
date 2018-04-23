<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.2, Version=11.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridLookup" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.2, Version=11.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <dx:ASPxGridView ID="Grid" runat="server" AutoGenerateColumns="False" DataSourceID="GridDataSource"
        KeyFieldName="ID" OnCustomColumnDisplayText="Grid_CustomColumnDisplayText" 
        OnRowUpdating="Grid_RowUpdating">
        <Columns>
            <dx:GridViewCommandColumn>
                <EditButton Visible="true" />
            </dx:GridViewCommandColumn>
            <dx:GridViewDataColumn FieldName="ID" ReadOnly="true" />
            <dx:GridViewDataColumn FieldName="TagIDs">
                <EditItemTemplate>
                    <dx:ASPxGridLookup ID="Lookup" runat="server" AutoGenerateColumns="false" DataSourceID="LookupDataSource" KeyFieldName="ID"
                        SelectionMode="Multiple" OnInit="Lookup_Init" TextFormatString="{1}">
                        <GridViewStyles>
                            <FocusedRow BackColor="#F16A39" />
                        </GridViewStyles>
                        <Columns>
                            <dx:GridViewCommandColumn ShowSelectCheckbox="true" VisibleIndex="0"></dx:GridViewCommandColumn>
                            <dx:GridViewDataTextColumn FieldName="ID" VisibleIndex="1"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Name" VisibleIndex="2"></dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:ASPxGridLookup>
                </EditItemTemplate>
            </dx:GridViewDataColumn>
        </Columns>
        <SettingsEditing Mode="PopupEditForm" PopupEditFormWidth="500" />
    </dx:ASPxGridView>
    <asp:ObjectDataSource ID="GridDataSource" runat="server" TypeName="DataProvider"
        DataObjectTypeName="GridDataItem" SelectMethod="GetGridData" UpdateMethod="UpdateGrid" />
    <asp:ObjectDataSource ID="LookupDataSource" runat="server" TypeName="DataProvider"
        SelectMethod="GetTags" />
    </form>
</body>
</html>