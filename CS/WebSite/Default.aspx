<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>How to use ASPxGridLookup in multiple selection mode as the ASPxGridView editor</title>
</head>
<body>
    <form id="form1" runat="server">
    <dx:ASPxGridView ID="Grid" runat="server" AutoGenerateColumns="False" DataSourceID="GridDataSource"
        KeyFieldName="ID" OnCustomColumnDisplayText="Grid_CustomColumnDisplayText" OnRowUpdating="Grid_RowUpdating"
        OnRowInserting="Grid_RowInserting">
        <Columns>
            <dx:GridViewCommandColumn ShowEditButton="true" ShowNewButton="True" ShowDeleteButton="True"/>
            <dx:GridViewDataColumn FieldName="ID">
                <EditFormSettings Visible="False" />
            </dx:GridViewDataColumn>
            <dx:GridViewDataColumn FieldName="TagIDs">
                <EditItemTemplate>
                    <dx:ASPxGridLookup ID="Lookup" runat="server" AutoGenerateColumns="false" DataSourceID="LookupDataSource"
                        KeyFieldName="ID" SelectionMode="Multiple" OnInit="Lookup_Init" TextFormatString="{1}">
                        <GridViewStyles>
                            <FocusedRow BackColor="#F16A39" />
                        </GridViewStyles>
                        <Columns>
                            <dx:GridViewCommandColumn ShowSelectCheckbox="true" VisibleIndex="0">
                            </dx:GridViewCommandColumn>
                            <dx:GridViewDataTextColumn FieldName="ID" VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Name" VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:ASPxGridLookup>
                </EditItemTemplate>
            </dx:GridViewDataColumn>
        </Columns>
        <SettingsEditing EditFormColumnCount="1" Mode="PopupEditForm" PopupEditFormWidth="200" />
    </dx:ASPxGridView>
    <asp:ObjectDataSource ID="GridDataSource" runat="server" TypeName="DataProvider"
        DataObjectTypeName="GridDataItem" SelectMethod="GetGridData" InsertMethod="InsertGrid"
        UpdateMethod="UpdateGrid" DeleteMethod="DeleteGrid" />
    <asp:ObjectDataSource ID="LookupDataSource" runat="server" TypeName="DataProvider"
        SelectMethod="GetTags" />
    </form>
</body>
</html>