#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports DevExpress.XtraReports.Parameters
Imports Infrastructure.Data.Xpo.MedicalFeesRepository
Imports Infrastructure.Data.Xpo.CommonRepository

#End Region

Public Class rptGlosaMedicalFees
    Implements IReport
    ''' <summary>
    ''' Nombre del reporte
    ''' </summary>
    ''' <remarks></remarks>
    Const CNameReport = "MedicalFees.FrmGlosaMedicalFees"
    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance
    Private INDUser As Object

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Dim filtroConsulta As String = "GlosaMedicalFeesId = " & ParametrosReporte(0)
        Dim IndList = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).MedicalFeesService.GetCollection(Of GlosaMedicalFeesDetailXpo)(Nothing, filtroConsulta)
        Dim INDCodeUser = CType(IndList(0), GlosaMedicalFeesDetailXpo).GlosaMedicalFeesId.CreationUser.Trim()
        INDUser = XpoServiceEx.Instance(IndigoSessionValues.SecurityContainer).TreasuryService.GetCollection(Of Infrastructure.Data.Xpo.SecurityRepository.UserXpo)(Nothing, "UserCode = '" & INDCodeUser & "'")
        Me.DataSource = IndList
        Dim filter As String = "IdPerson = " & CType(IndList(0), GlosaMedicalFeesDetailXpo).AccountPayableId.IdThirdParty.PersonId
        Dim INDCommonAdress = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).CommonService.GetCollection(Of CommonAddressXpo)(Nothing, filter)
        INDAdress.Text = INDCommonAdress.FirstOrDefault.Address
    End Sub

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return rptGlosaMedicalFees.CNameReport
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Private Sub rptGlosaMedicalFeest_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        If Me.Parameters.Count > 0 And Me.Parameters(0).Value > 0 Then
            Dim ParametrosFilter As ParameterCollection = Me.Parameters
            ParametrosReporte = New Object() {ParametrosFilter("Id").Value}
            CargarDataSource()
        End If

        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        INDUserImp.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        INDUserCreate.Text = INDUser(0).CodeName
        '' INDlblTotal.Text = Utils.Num2Text(Convert.ToDouble(GetCurrentColumnValue("INDCfTotal"))).ToString & " PESOS M/Cte."
    End Sub
End Class