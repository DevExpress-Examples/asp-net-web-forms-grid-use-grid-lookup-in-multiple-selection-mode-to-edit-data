Option Infer On

Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports DevExpress.Web
Imports DevExpress.Web.Data

Partial Public Class _Default
	Inherits System.Web.UI.Page
	Protected Sub Grid_CustomColumnDisplayText(ByVal sender As Object, ByVal e As ASPxGridViewColumnDisplayTextEventArgs)
		If e.Column.FieldName = "TagIDs" Then
			Dim tagIDs = CType(e.Value, Integer())

			Dim text = DataProvider.GetTags().Where(Function(t) tagIDs.Contains(t.ID)). Select(Function(t) t.Name).DefaultIfEmpty().Aggregate(Function(a, b) a & ", " & b)

			e.DisplayText = If(text, String.Empty)
		End If
	End Sub

	Protected Sub Grid_RowInserting(ByVal sender As Object, ByVal e As ASPxDataInsertingEventArgs)
		e.NewValues("TagIDs") = GetTags()
	End Sub

	Protected Sub Grid_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs)
		e.NewValues("TagIDs") = GetTags()
	End Sub

	Protected Sub Lookup_Init(ByVal sender As Object, ByVal e As EventArgs)
		Dim lookup = CType(sender, ASPxGridLookup)
		Dim container = CType(lookup.NamingContainer, GridViewEditItemTemplateContainer)

		If container.Grid.IsNewRowEditing Then
			Return
		End If

		Dim tagIDs = CType(container.Grid.GetRowValues(container.VisibleIndex, container.Column.FieldName), Integer())
		If tagIDs IsNot Nothing Then
			For Each tagID In tagIDs
				lookup.GridView.Selection.SelectRowByKey(tagID)
			Next tagID
		End If
	End Sub

	Private Function GetTags() As Integer()
		Dim column = CType(Grid.Columns("TagIDs"), GridViewDataColumn)
		Dim lookup = CType(Grid.FindEditRowCellTemplateControl(column, "Lookup"), ASPxGridLookup)
		Dim tags = TryCast(lookup.GridView.GetSelectedFieldValues(lookup.KeyFieldName), List(Of Object))

		Return tags.Select(Function(t) CInt(Fix(t))).ToArray()
	End Function
End Class