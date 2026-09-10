Imports System.ComponentModel
Imports System.Windows.Forms
Imports DevExpress.Data
Imports DevExpress.Xpo
Imports DevExpress.XtraGrid.Views.Grid

Public Class SummaryCtrl

    Private _view As GridView
    Private _selectedGroupRowsCount As Integer = 0
    Private refreshTimer As Timer ' Timer para verificar la carga de datos
    Private initialTotalRecords As Integer?

    Public Sub New()
        InitializeComponent()
        refreshTimer = New Timer()
        refreshTimer.Interval = 1000 ' Intervalo de 1 segundo
        AddHandler refreshTimer.Tick, AddressOf RefreshTimer_Tick
    End Sub

    <Browsable(True)>
    Public Property View() As GridView
        Get
            Return _view
        End Get
        Set(ByVal value As GridView)
            If _view IsNot Nothing Then
                ' Eliminar los manejadores de eventos del GridView anterior
                RemoveHandler _view.SelectionChanged, AddressOf SelectionChanged
                RemoveHandler _view.GridControl.DataSourceChanged, AddressOf DataSourceChanged
                RemoveHandler _view.ColumnFilterChanged, AddressOf ColumnFilterChanged
            End If

            _view = value

            If _view IsNot Nothing AndAlso _view.GridControl IsNot Nothing Then
                ' Agregar los manejadores de eventos al nuevo GridView
                AddHandler _view.SelectionChanged, AddressOf SelectionChanged
                AddHandler _view.GridControl.DataSourceChanged, AddressOf DataSourceChanged
                AddHandler _view.ColumnFilterChanged, AddressOf ColumnFilterChanged
            End If

            ' Llamar a RefreshTotals para asegurar que los totales estén actualizados
            RefreshTotals()
        End Set
    End Property

    Private Sub ColumnFilterChanged(sender As Object, e As EventArgs)
        RefreshTotals()
    End Sub

    Private Sub DataSourceChanged(sender As Object, e As EventArgs)
        ' Reinicializar el total inicial de registros cuando se cambie la fuente de datos
        initialTotalRecords = Nothing
        RefreshTotals()
    End Sub

    Private Sub SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        Dim view As GridView = CType(sender, GridView)
        Dim r = DirectCast(sender, DevExpress.XtraGrid.Views.Base.ColumnView).GetRow(e.ControllerRow)

        UpdateSelectedGroups(e.ControllerRow)
        RefreshTotals()
    End Sub

    Private Sub UpdateSelectedGroups(controllerRow As Integer)
        If _view.IsGroupRow(controllerRow) Then
            If _view.IsRowSelected(controllerRow) Then
                _selectedGroupRowsCount += 1
            Else
                _selectedGroupRowsCount -= 1
            End If

            If _selectedGroupRowsCount < 0 Then
                _selectedGroupRowsCount = 0
            End If
        End If

        _view.InvalidateFooter()
    End Sub

    Public Sub RefreshTotals()
        If _view Is Nothing OrElse _view.GridControl Is Nothing OrElse _view.GridControl.DataSource Is Nothing Then Return

        ' Verificar si la fuente de datos es del tipo XPInstantFeedbackSource
        If TypeOf _view.GridControl.DataSource Is XPInstantFeedbackSource Then
            ' Iniciar el timer para verificar la carga de datos
            refreshTimer.Start()
        Else
            ' Si no es XPInstantFeedbackSource, trabajar como estaba antes
            UpdateTotals()
        End If
    End Sub

    Private Sub RefreshTimer_Tick(sender As Object, e As EventArgs)
        ' Comprobar si los datos han terminado de cargarse
        If Not _view.IsAsyncInProgress Then
            ' Detener el timer
            refreshTimer.Stop()
            ' Refrescar los totales
            UpdateTotals()
        End If
    End Sub

    Private Sub UpdateTotals()
        If _view Is Nothing OrElse _view.GridControl Is Nothing OrElse _view.GridControl.DataSource Is Nothing Then Return

        If Not initialTotalRecords.HasValue Then
            initialTotalRecords = _view.DataRowCount
        End If
        ' Mostrar el total de registros en el control de etiqueta
        INDLblTotal.Text = _view.SelectedRowsCount - (From x In _view.GetSelectedRows() Where _view.IsGroupRow(x) Select x).Count()
        INDLblTotalVisualizations.Text = _view.DataRowCount
        INDLblTotalAdecuations.Text = initialTotalRecords 'CType(_view.DataSource, IList).Count.ToString()
    End Sub

    Private Sub SummaryCtrl_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not DesignMode AndAlso _view IsNot Nothing AndAlso _view.GridControl IsNot Nothing Then
            AddHandler _view.SelectionChanged, AddressOf SelectionChanged
            AddHandler _view.GridControl.DataSourceChanged, AddressOf DataSourceChanged
            AddHandler _view.ColumnFilterChanged, AddressOf ColumnFilterChanged
        End If
    End Sub

End Class
