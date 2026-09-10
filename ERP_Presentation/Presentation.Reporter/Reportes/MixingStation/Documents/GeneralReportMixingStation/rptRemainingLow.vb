Imports System.Data.SqlClient
Imports DevExpress.XtraReports.UI
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.MixingStationRepository
Imports Presentation.Base
Imports Presentation.CloudAgent

Public Class rptRemainingLow
	Implements IReport
	Implements IReportAsync

	Private INDUser As SecurityRepository.UserXpo
	Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

	Public ReadOnly Property NameReport As String Implements IReport.NameReport
		Get
			Return Nothing
		End Get
	End Property

	''' <summary>
	''' Variable para inicializar los valores de sesion
	''' </summary>
	Dim IndigoSessionValues As SessionValues = SessionValues.Instance

	Public Sub CargarDatasource() Implements IReport.CargarDataSource
		Try
			Dim xpoService = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer)
			Dim campaingDetailInfo = GetCampaignDetailInfo(xpoService)
			INDFecha.Text = DateTime.Now.ToString("dd/MM/yyyy")

			If campaingDetailInfo IsNot Nothing AndAlso campaingDetailInfo.Count > 0 Then
				Dim infoMixingstation = campaingDetailInfo.First()
				INDAlmacen.Text = infoMixingstation.WareHouseRemaining
				INDCentral.Text = infoMixingstation.MixingStation

				Dim data = GetDataViewQuantityRemainingLow(xpoService, infoMixingstation)
				If data IsNot Nothing AndAlso data.Count > 0 Then
					AssignValues(data)
					Me.DataSource = data
				End If
			End If

			SetCompanyName()
		Catch ex As Exception
			ShowMessageError(ex)
		End Try
	End Sub

	Private Function GetCampaignDetailInfo(xpoService As XpoServiceEx) As List(Of ViewCampaignDetailInfoXpo)
		Dim filter As String = $"Id = {ParametrosReporte(0)}"
		Return xpoService.MixingStationService.GetCollection(Of ViewCampaignDetailInfoXpo)(Nothing, filter).ToList()
	End Function

	Private Function GetDataViewQuantityRemainingLow(xpoService As XpoServiceEx, infoMixingstation As ViewCampaignDetailInfoXpo) As List(Of ViewQuantityRemainingLowXpo)
		Dim filter As String = $"IdMixingStation = {infoMixingstation.IdMixingStation} AND IdWarehouse = {infoMixingstation.IdWarehouse}"
		Return xpoService.MixingStationService.GetCollection(Of ViewQuantityRemainingLowXpo)(Nothing, filter).ToList()
	End Function

	Private Sub AssignValues(data As List(Of ViewQuantityRemainingLowXpo))
		Dim firstRecord = data.First()
		INDNombreQP.Text = firstRecord.QP
		INDNombreQC.Text = firstRecord.QC
		INDDetalles.Text = GetGroupedDetails(data)
	End Sub

	Private Function GetGroupedDetails(data As List(Of ViewQuantityRemainingLowXpo)) As String
		Dim groupedData = From item In data
						  Group By item.CauseReprocessingRejectionId Into Group
						  Select New With {
							  .CauseReprocessingRejectionId = CauseReprocessingRejectionId,
							  .CauseReprocessingRejectionName = Group.First().CauseReprocessingRejectionName,
							  .RowNumbers = String.Join(",", Group.Select(Function(x) x.RowNumber))
						  }

		Return String.Join(", ", groupedData.Select(Function(g) $"{g.RowNumbers} - {g.CauseReprocessingRejectionName}"))
	End Function

	Private Sub XrTableRow1_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableRow1.BeforePrint
		INDUserImp.Text = $"Usuario Impresión: {IndigoSessionValues.UserIndigo} - {IndigoSessionValues.UserIndigoName}"
	End Sub

	Private Sub SetCompanyName()
		INDLblCompanyName.Text = IndigoSessionValues.IndigoCompanyName
		INDLblCompanyNit.Text = $"Nit: {IndigoSessionValues.IndigoCompanyNit}"
	End Sub

	Public Function CargarDataSourceAsync() As Task Implements IReportAsync.CargarDataSourceAsync
		Return Task.Factory.StartNew(AddressOf CargarDatasource)
	End Function

	Public Sub CargarImagenes() Implements IReport.CargarImagenes
	End Sub

	Private Sub ShowMessageError(ex As Exception)
		MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
	End Sub

	Private Function GetExceptionDetails(exception As Exception) As String
		Dim properties = exception.GetType().GetProperties()
		Dim fields = properties.Select(Function(prop) $"{prop.Name} : {If(prop.GetValue(exception, Nothing) IsNot Nothing, prop.GetValue(exception, Nothing).ToString(), String.Empty)}")
		Return String.Join(vbLf, fields)
	End Function
End Class
