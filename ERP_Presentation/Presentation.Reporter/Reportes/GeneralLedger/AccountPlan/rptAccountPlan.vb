#Region "Librerias Improtadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.AccountingRepository
Imports Domain.Entities
Imports DevExpress.XtraReports.UI
Imports System.Drawing.Printing
#End Region

Public Class rptAccountPlan
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Dim filtroConsulta As String = "Number >= '" & ParametrosReporte(0) & "' And Number <= '" & ParametrosReporte(1) & "'" & " AND LegalBookId.Id = " & ParametrosReporte(3)

        If ParametrosReporte(2) <> 6 Then
            filtroConsulta &= " AND IdAccountLevel.Level = " & ParametrosReporte(2)
        End If

        Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).AccountingService.GetCollection(Of MainAccountsXpo)(Nothing, filtroConsulta)
    End Sub

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Private Sub rptAccountPlan_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        Me.INDLblUserPrint.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        INDLblSubtitle.Text = "Desde la Cuenta N° " & ParametrosReporte(0) & " hasta la Cuenta N° " & ParametrosReporte(1)
    End Sub
End Class