#Region "Librerias Improtadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Domain.Entities
Imports DevExpress.XtraReports.UI
Imports System.Drawing.Printing
#End Region

Public Class rptSubTransferOrderByCampaign
    Implements IReport
    Implements IReportAsync

#Region "Properties"
    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance
#End Region


#Region "Load Data"

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Try
            Dim EntityName As String = IIf(ParametrosReporte.Count > 1, ParametrosReporte(1), "TransferOrder")
            Dim filter As String = String.Format("CampaignDetailId = {0} AND EntityName = '{1}'", ParametrosReporte(0), EntityName)

            Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).BillingService.GetCollectionUnionXpo(Of Relation.MixingStationCampaignReportsXpo)(Nothing, filter)
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