#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.TreasuryRepository
Imports DevExpress.XtraReports.UI
Imports Domain.Entities
Imports System.Drawing.Printing
#End Region

Public Class rptCashReceiptResume
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion.
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance
    Dim indList
    Private INDUser As Object
    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Dim filtroConsulta As String = "Id = " & ParametrosReporte(0)
        Dim indList = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).TreasuryService.GetCollection(Of TreasuryCashReceiptsXpo)(Nothing, filtroConsulta)
        Dim INDCodeUser = CType(indList(0), TreasuryCashReceiptsXpo).CreationUser.Trim()
        INDUser = XpoServiceEx.Instance(IndigoSessionValues.SecurityContainer).TreasuryService.GetCollection(Of Infrastructure.Data.Xpo.SecurityRepository.UserXpo)(Nothing, "UserCode = '" & INDCodeUser & "'")
        Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).TreasuryService.GetCollection(Of TreasuryCashReceiptsXpo)(Nothing, filtroConsulta)
    End Sub

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Private Sub rptCashReceiptResume_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        Me.INDLblUserPrint.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        Me.INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        Me.INDLblNitCompany.Text = "Nit : " & IndigoSessionValues.IndigoCompanyNit
        INDLblNumLetters.Text = Utils.Num2Text(Convert.ToDouble(GetCurrentColumnValue("Value"))).ToString & " PESOS M/CTE."
        If INDUser.count > 0 Then
            INDLblCreationUser.Text = INDUser(0).CodeName
        End If
    End Sub

    Private Sub XrTable14_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTable14.BeforePrint
        Dim table As XRTable = CType(sender, XRTable)
        Dim row As XRTableRow = table.Rows(0)

        Dim filtroConsulta2 As String = "IdCashReceipt = " & ParametrosReporte(0)
        indList = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).TreasuryService.GetCollection(Of TreasuryPaymentMethodsXpo)(Nothing, filtroConsulta2)

        If CType(indList(0), TreasuryPaymentMethodsXpo).PaymentMethodTypes <> 1 Then
            Dim aux = XrTable14.WidthF
            If row.Cells("XrTableCell17") IsNot Nothing Then
                row.Cells.Remove(XrTableCell17)
                XrTable14.WidthF = aux
            End If
        End If
    End Sub
End Class