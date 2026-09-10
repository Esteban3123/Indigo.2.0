#Region "Librerias Improtadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Domain.Entities
Imports DevExpress.XtraReports.UI
Imports System.Drawing.Printing
Imports DevExpress.XtraReports.Parameters
Imports Infrastructure.Data.Xpo.SecurityRepository
Imports Presentation.common.MVP
Imports Infrastructure.Data.Xpo.CommonRepository
Imports Presentation.CloudAgent
Imports System.Globalization
#End Region

Public Class rptConsignmentInventoryRemission
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance
    ''' <summary>
    ''' obtiene la moneda a la cual se realiza el reporte 
    ''' </summary>
    Dim CurrencyData As Currency

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Dim filtroConsulta As String = "ConsignmentInventoryRemissionId.Id = " & ParametrosReporte(0)

        Dim INDList As List(Of InventoryConsignmentInventoryRemissionDetailReportXpo) = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).BillingService.GetCollection(Of InventoryConsignmentInventoryRemissionDetailReportXpo)(Nothing, filtroConsulta)

        If INDList.Count > 0 Then
            Dim CurrencyId = CType(INDList(0), InventoryConsignmentInventoryRemissionDetailReportXpo).ConsignmentInventoryRemissionId.CurrencyId

            CurrencyData = IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetCurrencyById(INDList(0).ConsignmentInventoryRemissionId.CurrencyId, IndigoSessionValues)

            Dim INDNameUser = CType(INDList(0), InventoryConsignmentInventoryRemissionDetailReportXpo).ConsignmentInventoryRemissionId.CreationUser.Trim()
            Dim INDListUser = XpoServiceEx.Instance(IndigoSessionValues.SecurityContainer).SecurityService.GetCollection(Of UserXpo)(Nothing, "UserCode = '" & INDNameUser & "'")

            If INDListUser IsNot Nothing Then
                Dim INDCodName = CType(INDListUser(0), UserXpo).CodeName.Trim
                Me.INDUserCreate.Text = INDCodName
            End If
        End If
        Me.DataSource = INDList
    End Sub

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Private Sub rptConsignmentInventoryRemission_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        If Me.Parameters.Count > 0 And Me.Parameters(0).Value > 0 Then
            Dim ParametrosFilter As ParameterCollection = Me.Parameters
            ParametrosReporte = New Object() {ParametrosFilter("INDSubIdConsignmentInventoryRemission").Value}
            CargarDataSource()
        End If

        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        INDUserImp.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName

        If CurrencyData IsNot Nothing AndAlso Not String.IsNullOrEmpty(CurrencyData.Abbreviation) Then
            Dim _culture As CultureInfo = CultureInfo.CurrentCulture.Clone()
            Dim CurrencyAbbreviation As String = CurrencyData.Abbreviation
            _culture.NumberFormat = CurrencyAbbreviation.GetNumberFormat
            ApplyLocalization(_culture)
        End If

    End Sub
End Class