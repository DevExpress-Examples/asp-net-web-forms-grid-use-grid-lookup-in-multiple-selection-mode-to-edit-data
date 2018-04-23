Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports DevExpress.Web.ASPxGridLookup
Imports DevExpress.Web.ASPxGridView
Imports DevExpress.Web.Data

Partial Public Class _Default
	Inherits System.Web.UI.Page
	Protected Sub Grid_CustomColumnDisplayText(ByVal sender As Object, ByVal e As ASPxGridViewColumnDisplayTextEventArgs)
		If e.Column.FieldName = "TagIDs" Then
			Dim tagIDs = CType(e.Value, Integer())

			Dim text = DataProvider.GetTags().Where(Function(t) tagIDs.Contains(t.ID)). Select(Function(t) t.Name).DefaultIfEmpty().Aggregate(Function(a, b) a & ", " & b)

			e.DisplayText = If((text <> Nothing), text, String.Empty)
		End If
	End Sub

	Protected Sub Grid_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs)
		Dim column = CType(Grid.Columns("TagIDs"), GridViewDataColumn)
		Dim lookup = CType(Grid.FindEditRowCellTemplateControl(column, "Lookup"), ASPxGridLookup)
		Dim tags = TryCast(lookup.GridView.GetSelectedFieldValues(lookup.KeyFieldName), List(Of Object))

		e.NewValues("TagIDs") = tags.Select(Function(t) CInt(Fix(t))).ToArray()
	End Sub

	Protected Sub Lookup_Init(ByVal sender As Object, ByVal e As EventArgs)
		Dim lookup = CType(sender, ASPxGridLookup)
		Dim container = CType(lookup.NamingContainer, GridViewEditItemTemplateContainer)

		Dim tagIDs = CType(container.Grid.GetRowValues(container.VisibleIndex, container.Column.FieldName), Integer())
		For Each tagID In tagIDs
			lookup.GridView.Selection.SelectRowByKey(tagID)
		Next tagID
	End Sub
End Class