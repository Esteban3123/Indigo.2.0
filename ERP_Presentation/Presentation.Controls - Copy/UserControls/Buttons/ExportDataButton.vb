#Region "Imports"

Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid
Imports Infrastructure.CrossCutting.Resources

#End Region

''' <summary>
''' Representa un botón que exporta los datos
''' de un GridControl a Microsoft Excel
''' </summary>
<System.ComponentModel.ToolboxItem(False)>
Public Class ExportDataButton
    Inherits DevExpress.XtraEditors.SimpleButton

#Region "Consts"

    ''' <summary>
    ''' Icono de Microsoft Excel
    ''' </summary>
    Private Const IMAGE_EXCEL As String = "iVBORw0KGgoAAAANSUhEUgAAABQAAAAUCAYAAACNiR0NAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8" & _
                                          "YQUAAAAZdEVYdFNvZnR3YXJlAEFkb2JlIEltYWdlUmVhZHlxyWU8AAAA1klEQVQ4T72VDQ3CMBCFKwEJ" & _
                                          "SEDCJCABCUiYAyQgAQmTgAQkIKG8b7SkNGt6XBq+5GV/13fXu2wLFmKMO2mS9umWnbTwJM3SIj2kzJzC" & _
                                          "2iiIxTfpzooOF4mELR0wpAorvdjlV8Oywqt0Lq7Z4f8N677Sa+IRw1pNkp4ctwwZUg5k0iWfKXMuTeky" & _
                                          "+2wakpmtILKWuCqEbFjjqhBo8vF9+oVrKAQielk/cw+Fhywgc4l7yy3cQ2kxvMLhr14vdjUc+/mq4aZE" & _
                                          "EnpERTQ/0//AWpCR8RcQwgti1cgwRt1v3QAAAABJRU5ErkJggg=="

#End Region

#Region "Fields"

    ''' <summary>
    ''' GridControl del cual se va a exportar
    ''' los datos a Microsoft Excel
    ''' </summary>
    Private _gridControl As GridControl

    ''' <summary>
    ''' SearchLookUp que contiene el GridView
    ''' de donde se van a exportar los datos
    ''' </summary>
    Private _searchLookUp As SearchLookUpEdit

    ''' <summary>
    ''' GridView del cual se va exportar
    ''' los datos a Microsoft Excel
    ''' </summary>
    Private _gridView As GridView

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene o asigna un valor que indica si el control es visible
    ''' </summary>
    ''' <value>Valor que indica si el control se hace visible</value>
    ''' <returns>Un valor que indica si el control es visible</returns>
    Public Overloads Property Visible As Boolean
        Get
            Return MyBase.Visible
        End Get
        Set(value As Boolean)
            If value Then
                If Me._gridView IsNot Nothing AndAlso Me._gridView.IsFindPanelVisible Then
                    MyBase.Visible = value
                End If
            Else
                MyBase.Visible = value
            End If
        End Set
    End Property

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="grid">GridControl que se va a exportar</param>
    Public Sub New(ByVal grid As GridControl)
        MyBase.New()
        Me.Inicializate()
        Me._gridControl = grid
        Me._gridView = grid.MainView
        Me._searchLookUp = Nothing
        Me.AddButtonOnGrid()
    End Sub

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="search">SearchLookUp que se va a exportar</param>
    Public Sub New(ByVal search As SearchLookUpEdit)
        MyBase.New()
        Me.Inicializate()
        Me._searchLookUp = search
        Me._gridView = search.Properties.View
        Me._gridControl = Nothing
        'Esta lógica se mueve al extendido del SearchLookUp
        'If Me._searchLookUp IsNot Nothing Then
        '    AddHandler Me._searchLookUp.Popup, AddressOf SearchLookUp_Popup
        'End If
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Inicializa los controles comunes
    ''' </summary>
    Private Sub Inicializate()
        Me.Name = Me.GetType().Name & "_" & DateTime.Now.Ticks.ToString()

        Me.Image = New Bitmap(New System.IO.MemoryStream(Convert.FromBase64String(IMAGE_EXCEL)))
        Me.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.Size = New System.Drawing.Size(34, 28)

        Me.ToolTip = ResourceManager.GetString("ToolTip", Me.GetType())
    End Sub

    ''' <summary>
    ''' Agrega y posiciona el botón en el GridControl
    ''' </summary>
    Private Sub AddButtonOnGrid()
        If Me._gridControl IsNot Nothing Then
            Me._gridControl.Controls.Add(Me)
            Me.Location = New Point((Me._gridControl.Width - Me.Size.Width - 20), 24)
            Me.Anchor = AnchorStyles.Top Or AnchorStyles.Right
            Me.BringToFront()
        End If
    End Sub

    ''' <summary>
    ''' Agrega y posiciona el botón en el SearchLookUpEdit
    ''' </summary>
    Private Sub AddButtonOnSearchLookUp(ByVal popup As DevExpress.Utils.Win.IPopupControl)
        If Not popup.PopupWindow.Controls.Contains(Me) Then
            If Me.IsDisposed Then

            End If
            popup.PopupWindow.Controls.Add(Me)
            Me.Location = New Point((popup.PopupWindow.Size.Width - Me.Size.Width - 25), 13)
            Me.BringToFront()
        End If
    End Sub

    ''' <summary>
    ''' Ejecuta el exportado de los datos
    ''' </summary>
    Protected Overrides Sub OnClick(e As EventArgs)
        If Me._gridView IsNot Nothing AndAlso Me._gridView.VisibleColumns.Count > 0 Then
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

#Region "Handlers"

    ''' <summary>
    ''' Aqui se agrega el botón si aun no se ha hecho
    ''' </summary>
    Private Sub SearchLookUp_Popup(sender As Object, e As EventArgs)
        Me.AddButtonOnSearchLookUp(sender)
    End Sub

#End Region

End Class
