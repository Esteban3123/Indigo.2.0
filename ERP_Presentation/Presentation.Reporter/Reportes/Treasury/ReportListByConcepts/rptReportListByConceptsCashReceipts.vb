#Region "Librerias Importadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.TreasuryRepository
Imports Infrastructure.Data.Xpo
#End Region

Public Class rptReportListByConceptsCashReceipts
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Public Sub CargarDataSource() Implements IReport.CargarDataSource

        Dim filtroConsulta As String = "GetDate(IdCashReceipt.DocumentDate) >= #" & Format(ParametrosReporte(0), "yyyy-MM-dd") & "# AND GetDate(IdCashReceipt.DocumentDate) <= #" & Format(ParametrosReporte(1), "yyyy-MM-dd") & "#"
        ' si filtra por terceros
        If ParametrosReporte(2) IsNot Nothing And ParametrosReporte(3) IsNot Nothing Then
            filtroConsulta &= "AND IdCashReceipt.IdThirdParty.Nit >= '" & ParametrosReporte(2) & "' AND IdCashReceipt.IdThirdParty.Nit <= '" & ParametrosReporte(3) & "'"
        End If
        'si filtra por conceptos
        If ParametrosReporte(4) IsNot Nothing And ParametrosReporte(5) IsNot Nothing Then
            filtroConsulta &= "AND IdCashReceiptConcept.Code >= '" & ParametrosReporte(4) & "' AND IdCashReceiptConcept.Code <= '" & ParametrosReporte(5) & "'"
        End If

        If ParametrosReporte(6) <> 4 Then
            filtroConsulta &= "AND IdCashReceipt.Status = " & ParametrosReporte(6)
        End If

        Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).TreasuryService.GetCollection(Of TreasuryCashReceiptDetailsXpo)(Nothing, filtroConsulta)
      
    End Sub

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Private Sub rptReportListByConceptsCashReceipts_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        INDLblUserPrint.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        INDLblDate.Text = "Informe comprendido entre " & CDate(Me.ParametrosReporte(0)).ToString("dd De MMMM Del yyyy") & " " & CDate(Me.ParametrosReporte(1)).ToString("A dd De MMMM Del yyyy")
        'informe resumido
        If ParametrosReporte(7) = 2 Then

            XrTable2.Visible = False
            Detail.Visible = False

        End If
    End Sub
End Class