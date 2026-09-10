#Region "Librerias Improtadas"

Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.AccountingRepository
Imports Domain.Entities
Imports DevExpress.XtraReports.UI
Imports System.Drawing.Printing
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.PaymentsRepository

#End Region

Public Class rptMonthlyAmortization
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance
    Private INDUser As Object
    Public Sub CargarDataSource() Implements IReport.CargarDataSource

        Dim INDListId As String
        Dim filtroConsulta As String = Nothing

        If ParametrosReporte(0).Count > 0 Then
            For Each item As Integer In ParametrosReporte(0)
                If INDListId IsNot Nothing Then
                    INDListId &= ","
                End If
                INDListId &= item
            Next
        End If
        filtroConsulta = "Id in (" & INDListId & ")"

        Dim INDList = XpoServiceex.Instance(IndigoSessionValues.TransactionalContainer).PaymentsService.GetCollection(Of PaymentsDeferredCausationShareXpo)(Nothing, filtroConsulta)
        Dim INDCodeUser = CType(INDList(0), PaymentsDeferredCausationShareXpo).DeferredCausationId.CreationUser.Trim()
        INDUser = XpoServiceEx.Instance(IndigoSessionValues.SecurityContainer).PaymentsService.GetCollection(Of Infrastructure.Data.Xpo.SecurityRepository.UserXpo)(Nothing, "UserCode = '" & INDCodeUser & "'")
        Me.DataSource = INDList
        Me.INDLBlSubtitle.Text = "De " & MonthName(CType(INDList(0), PaymentsDeferredCausationShareXpo).PaymentMonth, True) & " de " & CType(INDList(0), PaymentsDeferredCausationShareXpo).PaymentYear

    End Sub

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Private Sub rptMonthlyAmortization_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        INDUserImp.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        If INDUser.count() > 0 Then
            INDLblCreationUser.Text = INDUser(0).CodeName
        End If
    End Sub
End Class