#Region "Librerias Importadas"

Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.InventoryRepository

#End Region

Public Class rptSubListPharmaceuticalDispensing
    Implements IReport
    Implements IReportAsync
    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Dim filtroConsulta As String = "FechaDispensacion >= #" & Format(ParametrosReporte(0), "yyyy-MM-dd HH:mm:ss") & "# AND FechaDispensacion <= #" & Format(ParametrosReporte(1), "yyyy-MM-dd HH:mm:ss") & "#"
        'si filtra por Almacén
        If Not String.IsNullOrEmpty(ParametrosReporte(2)) Then
            filtroConsulta &= String.Format(" And IdAlmacen In ({0})", ParametrosReporte(2))
        End If
        'si filtra por Producto
        If Not String.IsNullOrEmpty(ParametrosReporte(3)) Then
            filtroConsulta &= String.Format(" AND ProductId In ({0})", ParametrosReporte(3))
        End If
        'si filtra por documento
        If Not String.IsNullOrEmpty(ParametrosReporte(4)) Then
            filtroConsulta &= String.Format(" AND IdPharmaceuticalDispensing In ({0})", ParametrosReporte(4))
        End If
        'Filtro por tipo de reporte
        filtroConsulta &= String.Format(" AND Type = {0}", 2)
        Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).TreasuryService.GetCollection(Of InventoryViewReportPharmaceuticalDispensingWithINDIGO999Xpo)(Nothing, filtroConsulta)

    End Sub
    Public Function CargarDataSourceAsync() As Task Implements IReportAsync.CargarDataSourceAsync
        Return Task.Factory.StartNew(AddressOf CargarDataSource)
    End Function
    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte
End Class