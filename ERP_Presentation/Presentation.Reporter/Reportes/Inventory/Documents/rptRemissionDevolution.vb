#Region "Librerias Improtadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Domain.Entities
Imports DevExpress.XtraReports.UI
Imports System.Drawing.Printing
Imports DevExpress.XtraReports.Parameters
Imports Infrastructure.Data.Xpo.SecurityRepository
Imports System.Globalization

#End Region

Public Class rptRemissionDevolution
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Dim filtroConsulta As String = "RemissionDevolutionId.Id = " & ParametrosReporte(0) & " AND Quantity > 0"

        Dim INDList As List(Of InventoryRemissionDevolutionDetailReportXpo) = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).BillingService.GetCollection(Of InventoryRemissionDevolutionDetailReportXpo)(Nothing, filtroConsulta)
        If INDList.Count > 0 Then
            Dim INDNameUser = CType(INDList(0), InventoryRemissionDevolutionDetailReportXpo).RemissionDevolutionId.CreationUser.Trim()

            Dim INDListUser = XpoServiceEx.Instance(IndigoSessionValues.SecurityContainer).SecurityService.GetCollection(Of UserXpo)(Nothing, "UserCode = '" & INDNameUser & "'")

            If INDListUser IsNot Nothing AndAlso INDListUser.Any Then
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

    Private Sub rptRemissionDevolution_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        If Me.Parameters.Count > 0 And Me.Parameters(0).Value > 0 Then
            Dim ParametrosFilter As ParameterCollection = Me.Parameters
            ParametrosReporte = New Object() {ParametrosFilter("INDSubIdRemissionDevolution").Value}
            CargarDataSource()
        End If

        Dim currencyAbbreviation As String = TryCast(Me.DataSource, List(Of InventoryRemissionDevolutionDetailReportXpo))?.FirstOrDefault?.RemissionDevolutionId?.CurrencyAbbreviation
        If String.IsNullOrEmpty(currencyAbbreviation) Then currencyAbbreviation = IndigoSessionValues.CurrencyISO4217
        Dim culture As CultureInfo = CultureInfo.CurrentCulture.Clone()
        culture.NumberFormat = currencyAbbreviation?.GetNumberFormat
        Me.ApplyLocalization(culture)

        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        INDUserImp.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
    End Sub
End Class