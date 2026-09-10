#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.TreasuryRepository
Imports DevExpress.Xpo
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraReports.Parameters

#End Region

Public Class rptCrossingAccountReduced
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Private ValueLetters As String

    Private Value As Decimal

    Private INDUser As Object

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Dim filtroConsulta As String = "Id = " & ParametrosReporte(0)
        Dim list = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).TreasuryService.GetCollection(Of TreasuryCrossingAccountXpo)(Nothing, filtroConsulta)

        If list.Count > 0 Then
            Dim INDCodeUser = CType(list(0), TreasuryCrossingAccountXpo).CreationUser.Trim()
            INDUser = XpoServiceEx.Instance(IndigoSessionValues.SecurityContainer).TreasuryService.GetCollection(Of Infrastructure.Data.Xpo.SecurityRepository.UserXpo)(Nothing, "UserCode = '" & INDCodeUser & "'")
            Me.DataSource = list
            If INDUser.count() > 0 Then
                INDLblCreationUser.Text = INDUser(0).CodeName
            End If
            ValueLetters = Utils.Num2Text(Convert.ToDouble(Value)).ToString & " PESOS M/CTE."
        End If
    End Sub

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Private Sub rptCrossingAccount_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        If Me.Parameters.Count > 0 And Me.Parameters(0).Value > 0 Then
            Dim ParametrosFilter As ParameterCollection = Me.Parameters
            ParametrosReporte = New Object() {ParametrosFilter("INDSubIdCrossing").Value}
            CargarDataSource()
        End If

        Me.INDLblUserPrint.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        Me.INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        Me.INDLblNitCompany.Text = "Nit : " & IndigoSessionValues.IndigoCompanyNit
        Me.INDLblNumLetters.Text = Me.ValueLetters
        Me.INDLblValue.Text = CStr(Microsoft.VisualBasic.Format(Me.Value, "c0"))
        Me.PamINDReportType.Value = ParametrosReporte(1)
    End Sub
End Class