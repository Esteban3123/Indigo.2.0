#Region "Librerias Improtadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Domain.Entities
Imports DevExpress.XtraReports.UI
Imports System.Drawing.Printing
#End Region

Public Class rptSubTransferOrder
    Implements IReport
    Implements IReportAsync

#Region "Properties"
    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Private a As List(Of InventoryTransferOrderDetailReportXpo)
    Private b As List(Of InventoryTransferOrderDetailReportXpo)
#End Region


#Region "Load Data"

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Try
            Dim filter As String = "GetDate(TransferOrderId.DocumentDate) >= #" & Format(ParametrosReporte(0), "yyyy-MM-dd") & "# AND GetDate(TransferOrderId.DocumentDate) <= #" & Format(ParametrosReporte(1), "yyyy-MM-dd") & "#"

            ' filtra por Estado
            If ParametrosReporte(3) <> 4 Then
                filter &= " And TransferOrderId.Status = " & ParametrosReporte(3)
            End If

            ' filtra por Detalle
            If ParametrosReporte(5) IsNot Nothing Then
                filter &= " And TransferOrderId.Description LIKE '" & ParametrosReporte(5) & "'"
            End If

            'filtro por producto
            If Not String.IsNullOrEmpty(ParametrosReporte(6)) Then
                filter &= String.Format(" AND ProductId.Id IN ({0})", ParametrosReporte(6))
            End If

            'filtro por grupo
            If Not String.IsNullOrEmpty(ParametrosReporte(7)) Then
                filter &= String.Format(" AND ProductId.ProductGroupId.Id IN ({0})", ParametrosReporte(7))
            End If

            'filtro por subgrupo
            If Not String.IsNullOrEmpty(ParametrosReporte(8)) Then
                filter &= String.Format(" AND ProductId.ProductSubGroupId.Id IN ({0})", ParametrosReporte(8))
            End If

            'filtro por tercero
            If Not String.IsNullOrEmpty(ParametrosReporte(9)) Then
                filter &= String.Format(" AND TransferOrderId.ThirdPartyId.Id In ({0})", ParametrosReporte(9))
            End If

            'filtro por unidad funcional
            If Not String.IsNullOrEmpty(ParametrosReporte(10)) Then
                filter &= String.Format(" AND TransferOrderId.TargetFunctionalUnitId.Id IN ({0})", ParametrosReporte(10))
            End If

            'filtro por almacen origen
            If Not String.IsNullOrEmpty(ParametrosReporte(11)) Then
                filter &= String.Format(" AND TransferOrderId.SourceWarehouseId.Id IN ({0})", ParametrosReporte(11))
            End If

            'filtro por almacen destino 
            If Not String.IsNullOrEmpty(ParametrosReporte(12)) Then
                filter &= String.Format(" AND TransferOrderId.TargetWarehouseId.Id IN ({0})", ParametrosReporte(12))

            End If

            'filtro por documento
            If Not String.IsNullOrEmpty(ParametrosReporte(13)) Then
                filter &= String.Format(" AND TransferOrderId.Id In ({0})", ParametrosReporte(13))
            End If

            Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).BillingService.GetCollectionUnionXpo(Of InventoryTransferOrderDetailReportXpo)(Nothing, filter)

        Catch ex As Exception
            Base.MessageIndigo.Show(MessageType.Errores, Me.Text, Botones.Aceptar, "")
        End Try

    End Sub


#End Region

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