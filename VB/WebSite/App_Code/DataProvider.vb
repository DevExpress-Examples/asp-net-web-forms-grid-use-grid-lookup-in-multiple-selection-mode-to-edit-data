Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Collections
Imports System.Web.SessionState

Public Module DataProvider
	Private ReadOnly Property Session() As HttpSessionState
		Get
			Return HttpContext.Current.Session
		End Get
	End Property

	Private ReadOnly Property GridData() As IList(Of GridDataItem)
		Get
			Const key As String = "6B95C3EB-8FB0-4BF6-8E10-BFA51D18CE72"
			If Session(key) Is Nothing Then
				Session(key) = CreateGridData()
			End If
			Return DirectCast(Session(key), IList(Of GridDataItem))
		End Get
	End Property
	Private ReadOnly Property Tags() As IList(Of Tag)
		Get
			Const key As String = "{70DDA114-EBF1-4990-B3D5-20E1C60CEBF8}"
			If Session(key) Is Nothing Then
				Session(key) = CreateTags()
			End If
			Return DirectCast(Session(key), IList(Of Tag))
		End Get
	End Property

	Private Function CreateGridData() As IList(Of GridDataItem)
		Dim result = New List(Of GridDataItem)()
		For i As Integer = 0 To 99
			result.Add(New GridDataItem() With {
				.ID = i,
				.TagIDs = New Integer() {
					If(i Mod 2 = 0, 0, 1),
					If(i Mod 3 = 0, 2, 3)
				}
			})
		Next i
		Return result
	End Function

	Private Function CreateTags() As IList(Of Tag)
		Dim result = New List(Of Tag)()
		For i As Integer = 0 To 4
			result.Add(New Tag() With {
				.ID = i,
				.Name = "#Tag" & i
			})
		Next i
		Return result
	End Function

	Public Function GetGridData() As IList(Of GridDataItem)
		Return GridData
	End Function

	Public Function GetTags() As IList(Of Tag)
		Return Tags
	End Function

	Public Sub InsertGrid(ByVal item As GridDataItem)
		item.ID = GridData.Max(Function(i) i.ID)
		item.ID += 1
		GridData.Add(item)
	End Sub

	Public Sub UpdateGrid(ByVal item As GridDataItem)
		GridData.First(Function(i) i.ID = item.ID).TagIDs = item.TagIDs
	End Sub

	Public Sub DeleteGrid(ByVal item As GridDataItem)
		GridData.Remove(GridData.First(Function(i) i.ID = item.ID))
	End Sub
End Module

Public Class GridDataItem
	Public Property ID() As Integer
	Public Property TagIDs() As Integer()
End Class

Public Class Tag
	Public Property ID() As Integer
	Public Property Name() As String
End Class