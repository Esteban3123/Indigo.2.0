#Region "Imports"

Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.Spreadsheet
Imports DevExpress.XtraPrinting
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
    Private Const IMAGE_EXCEL As String = "iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAABmJLR0QA/wD/AP+gvaeTAAAB
X0lEQVQ4jcWSP0ubURjFf/f9E6EpEcwiSpFAxo5SIiiCX8BB6GB33Ry6FLcOGUoLpZ/ADl2Kk46uGRQc/ARBBwcFaaBtgnmjz3M6vOCfxMZMeuAuP+5
znnMuF55boR9UfqwdA5W7rFgePxgrvZy7yyR9Plr4+CF6wLTyABvcHIUKQAIwVV9+5bFqZkbvvAUSEoQ0oVAuDTVKACzyTzJfxZys/QeZIzMwMTM2QZ
tOaP363TeqAJBXcE9lzsrrRaoT02zMvyVWwN1Aw6tEAG5CbjSaR3xbfs/fyw5X11d5ikeUAEiGzCmEmG4vIwCynDUbh9h0sWbF5P7mrtVuK5ghM5aqs
6z/rFMqvCAhQj5qgmvryZyt/R1kxpe978jzBKMZZLbpse/iRnzW3ZYEEsEgpCnpRUZ6kfWNhoMbg9bXxilwCjD57s3tnRQIA5/1/lsMkBBOCIGb8z+J
k0f7PYn+Aac9nla6oWUDAAAAAElFTkSuQmCC"

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
                If Me._gridView IsNot Nothing AndAlso Me._gridView.OptionsView.ShowFooter Then
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
        AddHandler DirectCast(Me._gridView, GridView).CustomDrawFooter, AddressOf GridView_CustomDrawFooter
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
        AddHandler DirectCast(Me._gridView, GridView).CustomDrawFooter, AddressOf GridView_CustomDrawFooter
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
        'Me.Size = New System.Drawing.Size(34, 28)
        Me.ToolTip = ResourceManager.GetString("ToolTip", Me.GetType())
    End Sub

    Private _lastLocationX As Integer = 0
    Private _lastLocationY As Integer = 0

    ''' <summary>
    ''' Agrega y posiciona el botón en el GridControl
    ''' </summary>
    Private Sub AddButtonOnGrid(ByVal e As DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs)
        If e.Bounds.Location.X = _lastLocationX AndAlso e.Bounds.Location.Y = _lastLocationY Then
            Return
        End If

        If Me._gridControl IsNot Nothing Then
            Me._gridControl.Controls.Add(Me)
        End If
        If Me._gridView IsNot Nothing Then
            Dim pInfo = Me._gridView.GetType().GetField("fViewInfo", Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance)
            Dim xWidth As Integer = 0
            If pInfo IsNot Nothing Then
                Dim col = DirectCast(pInfo.GetValue(Me._gridView), GridViewInfo)
                xWidth = col.ViewRects.IndicatorWidth
            End If
            If Me._gridControl IsNot Nothing Then
                Dim distance = (e.Bounds.Height - Me.Height) / 2

                Me.Anchor = AnchorStyles.Top Or AnchorStyles.Left
                Me.Location = New Point(e.Bounds.Location.X + distance, e.Bounds.Location.Y + 1)
            Else
                Dim popup As DevExpress.Utils.Win.IPopupControl = TryCast(Me._searchLookUp, DevExpress.Utils.Win.IPopupControl)
                Me.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
                Me.Location = New Point(e.Bounds.Location.X + xWidth + popup.PopupWindow.Margin.Left + popup.PopupWindow.Padding.Left + 4, popup.PopupWindow.Size.Height - Me.Size.Height - popup.PopupWindow.Margin.Bottom - popup.PopupWindow.Padding.Bottom - 2)
            End If

            Me.Size = New System.Drawing.Size(34, e.Bounds.Height - 2)

            _lastLocationX = e.Bounds.Location.X
            _lastLocationY = e.Bounds.Location.Y

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

            Dim options As New XlsxExportOptionsEx()
            ' Set the currency format for the summary cells
            options.ExportType = DevExpress.Export.ExportType.WYSIWYG
            options.TextExportMode = TextExportMode.Value
            options.ShowTotalSummaries = DevExpress.Utils.DefaultBoolean.False
            options.ApplyFormattingToEntireColumn = True
            Me._gridView.ExportToXlsx(fileName, options)
            If System.IO.File.Exists(fileName) Then
                System.Diagnostics.Process.Start(fileName)
            End If
        End If
        MyBase.OnClick(e)
    End Sub

#End Region

#Region "Handlers"

    ''' <summary>
    ''' Aqui se muestra el botón
    ''' </summary>
    Private Sub GridView_CustomDrawFooter(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs)
        Me.AddButtonOnGrid(e)
    End Sub

    ''' <summary>
    ''' Aqui se agrega el botón si aun no se ha hecho
    ''' </summary>
    Private Sub SearchLookUp_Popup(sender As Object, e As EventArgs)
        Me.AddButtonOnSearchLookUp(sender)
    End Sub

#End Region

End Class
