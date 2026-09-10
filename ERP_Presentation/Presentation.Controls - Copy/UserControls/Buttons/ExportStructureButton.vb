#Region "Imports"

Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid

#End Region

''' <summary>
''' Representa un botón que exporta una estructura
''' de columnas definidas a Microsoft Excel
''' </summary>
Public Class ExportStructureButton
    Inherits DevExpress.XtraEditors.SimpleButton

#Region "Consts"

    ''' <summary>
    ''' Icono de exportado de estructura
    ''' </summary>
    Private Const IMAGE_EXPORT As String = "iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAYAAABzenr0AAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8" & _
                                           "YQUAAAAZdEVYdFNvZnR3YXJlAEFkb2JlIEltYWdlUmVhZHlxyWU8AAABCElEQVRYR+2UgQ3CIBBFO4Kj" & _
                                           "dISO0hEcwU0cxREcwREcAf8nH0KD2GKPqJGXXODo9e7ncjB0Oh/HOTfBWnFSmTIIooCLXDOY86cE3Bhs" & _
                                           "bD6nypRB0FfMwB3Glq0Z42pibWdAiWtiNwu4al0zxtXE/vcM0GaVKYMgCjCfgc1YC1A+b/LPsBJT+GE3" & _
                                           "vjrANjxqR9ir4qSJgNgh7EM3KIaiOD8pUcDua6iaCwEpOH/WjSjAcgay7zhLi89aSRSw+xqqViYA/qK4" & _
                                           "zgLNZyArTuSTUUd2IKkXgDVt9eJBgs+urT9S78Dk2nLPDmSFcDbCDnJtSQWUQAyvZLw5piDxloHmY9VG" & _
                                           "QKdTxzA8AK8vcIANQsHjAAAAAElFTkSuQmCC"

#End Region

#Region "Fields"

    ''' <summary>
    ''' GridControl usado para realizar la estructura
    ''' y exportado a Microsoft Excel
    ''' </summary>
    Private _gridControl As GridControl

    ''' <summary>
    ''' GridView usado para realizar la estructura
    ''' y exportado a Microsoft Excel
    ''' </summary>
    Private _gridView As GridView

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New()
        MyBase.New()

        Me.Image = New Bitmap(New System.IO.MemoryStream(Convert.FromBase64String(IMAGE_EXPORT)))
        Me.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.Size = New System.Drawing.Size(40, 32)

        Me._gridControl = New GridControl()
        Me._gridView = New GridView()
        Me._gridControl.MainView = Me._gridView
        Me._gridControl.Name = "GridControl" & "_" & DateTime.Now.Ticks
        Me._gridControl.Size = New System.Drawing.Size(400, 200)
        Me._gridControl.TabIndex = 1
        Me._gridControl.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me._gridView})
        Me._gridView.Name = "GridView" & "_" & DateTime.Now.Ticks
        Me._gridView.GridControl = Me._gridControl
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Agrega una nueva columna a la estructura
    ''' </summary>
    ''' <param name="caption">Titulo de la columna</param>
    Public Sub AddColumn(ByVal caption As String)
        If caption Is Nothing OrElse caption.Trim().Equals(String.Empty) Then
            caption = "GridColumn" & (Me._gridView.Columns.Count + 1)
        End If
        Me._gridView.Columns.Add(New DevExpress.XtraGrid.Columns.GridColumn())
        Me._gridView.Columns(Me._gridView.Columns.Count - 1).Caption = caption.Trim()
        Me._gridView.Columns(Me._gridView.Columns.Count - 1).FieldName = caption.Replace(" ", "")
        Me._gridView.Columns(Me._gridView.Columns.Count - 1).Name = "GridColumn" & (Me._gridView.Columns.Count + 1)
        Me._gridView.Columns(Me._gridView.Columns.Count - 1).Visible = True
        Me._gridView.Columns(Me._gridView.Columns.Count - 1).VisibleIndex = Me._gridView.Columns.Count - 1
    End Sub

    ''' <summary>
    ''' Agrega un rango de columnas
    ''' </summary>
    ''' <param name="listNameColumns">Lista de columnas a agregar</param>
    Public Sub AddRangeColumns(ByVal ParamArray listNameColumns() As String)
        If listNameColumns IsNot Nothing AndAlso listNameColumns.Length > 0 Then
            For Each c As String In listNameColumns
                Me.AddColumn(c)
            Next
        End If
    End Sub

    ''' <summary>
    ''' Ejecuta el exportado de la estructura
    ''' </summary>
    Protected Overrides Sub OnClick(e As EventArgs)
        If Me._gridView.Columns.Count > 0 Then
            Dim fileName As String = System.IO.Path.GetTempFileName() & ".xlsx"
            Dim param As New DevExpress.XtraPrinting.XlsxExportOptions(DevExpress.XtraPrinting.TextExportMode.Value, True, False)
            Me._gridView.ExportToXlsx(fileName, param)
            If System.IO.File.Exists(fileName) Then
                System.Diagnostics.Process.Start(fileName)
            End If
        End If
        MyBase.OnClick(e)
    End Sub

#End Region

End Class
