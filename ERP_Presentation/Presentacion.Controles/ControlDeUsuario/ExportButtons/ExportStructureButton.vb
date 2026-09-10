#Region "Imports"

Imports DevExpress.Spreadsheet

#End Region

Public Class ExportStructureButton
    Inherits DevExpress.XtraEditors.SimpleButton

#Region "Consts"

    Private Const IMAGE_EXPORT As String = "iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAYAAABzenr0AAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8" &
                                           "YQUAAAAZdEVYdFNvZnR3YXJlAEFkb2JlIEltYWdlUmVhZHlxyWU8AAABCElEQVRYR+2UgQ3CIBBFO4Kj" &
                                           "dISO0hEcwU0cxREcwREcAf8nH0KD2GKPqJGXXODo9e7ncjB0Oh/HOTfBWnFSmTIIooCLXDOY86cE3Bhs" &
                                           "bD6nypRB0FfMwB3Glq0Z42pibWdAiWtiNwu4al0zxtXE/vcM0GaVKYMgCjCfgc1YC1A+b/LPsBJT+GE3" &
                                           "vjrANjxqR9ir4qSJgNgh7EM3KIaiOD8pUcDua6iaCwEpOH/WjSjAcgay7zhLi89aSRSw+xqqViYA/qK4" &
                                           "zgLNZyArTuSTUUd2IKkXgDVt9eJBgs+urT9S78Dk2nLPDmSFcDbCDnJtSQWUQAyvZLw5piDxloHmY9VG" &
                                           "QKdTxzA8AK8vcIANQsHjAAAAAElFTkSuQmCC"

#End Region

#Region "Fields"

    Private _sheets As List(Of ExcelSheet)

#End Region

#Region "Builders"

    Public Sub New()
        MyBase.New()

        Me.Image = New Bitmap(New System.IO.MemoryStream(Convert.FromBase64String(IMAGE_EXPORT)))
        Me.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.Size = New System.Drawing.Size(40, 32)
    End Sub

#End Region

#Region "Methods"

    Public Sub AddRangeColumns(ByVal ParamArray listNameColumns() As String)
        Dim columns = New List(Of ExcelColumn)
        For Each nameColumn In listNameColumns
            columns.Add(New ExcelColumn With {.Name = nameColumn})

        Next
        Me._sheets = New List(Of ExcelSheet) From {New ExcelSheet With {.Columns = columns}}
    End Sub

    Public Sub AddExcelSheets(sheet As ExcelSheet)
        Me._sheets = New List(Of ExcelSheet) From {sheet}
    End Sub

    Public Sub AddExcelSheets(sheets As List(Of ExcelSheet))
        Me._sheets = sheets
    End Sub

    Public Sub ExecuteOnClick()
        OnClick(Nothing)
    End Sub

#End Region

#Region "No Public Methods"

    Protected Overrides Sub OnClick(e As EventArgs)
        If Me._sheets IsNot Nothing AndAlso Me._sheets.Count > 0 Then
            Dim workbook As New Workbook()
            workbook.Unit = DevExpress.Office.DocumentUnit.Point
            Dim author As String = workbook.CurrentAuthor
            workbook.BeginUpdate()
            Dim sheetIndex = 0
            For Each sheet In _sheets
                If sheetIndex > 0 Then
                    workbook.Worksheets.Add()
                End If

                Dim worksheet As Worksheet = workbook.Worksheets(sheetIndex)
                If Not String.IsNullOrEmpty(sheet.Name) Then
                    worksheet.Name = sheet.Name
                End If

                Dim columnIndex = 0
                For Each column In sheet.Columns
                    Dim cell As Cell = worksheet.Cells(0, columnIndex)
                    cell.Value = column.Name
                    cell.FillColor = Color.LightGray
                    If Not String.IsNullOrEmpty(column.Comment) Then
                        worksheet.Comments.Add(cell, author, column.Comment)
                    End If

                    Select Case column.Type
                        Case ExcelColumnType.Text
                            worksheet.Columns(columnIndex).NumberFormat = "@"
                    End Select
                    columnIndex = columnIndex + 1
                Next

                worksheet.Cells.AutoFitColumns()
                sheetIndex = sheetIndex + 1
            Next
            workbook.Worksheets.ActiveWorksheet = workbook.Worksheets(0)
            workbook.EndUpdate()
            workbook.Calculate()
            Dim fileName As String = System.IO.Path.GetTempFileName() & ".xlsx"
            workbook.SaveDocument(fileName, DocumentFormat.OpenXml)
            System.Diagnostics.Process.Start(fileName)
        End If

        MyBase.OnClick(e)
    End Sub

#End Region

End Class

Public Class ExcelSheet

    Property Name As String

    Property Columns As List(Of ExcelColumn)

End Class

Public Class ExcelColumn

    Property Name As String

    Property Comment As String

    Property Type As ExcelColumnType = ExcelColumnType.IsDefault

End Class

Public Enum ExcelColumnType
    IsDefault
    Text
    Number
End Enum
