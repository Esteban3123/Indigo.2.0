#Region "Librerias Importadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.PaymentsRepository
Imports Infrastructure.Data.Xpo
Imports DevExpress.XtraReports.UI

#End Region

Public Class rptNotesListP
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Dim filtroConsulta As String = "GetDate(PaymentNoteId.NoteDate) >= #" & Format(ParametrosReporte(0), "yyyy-MM-dd") & "# AND GetDate(PaymentNoteId.NoteDate) <= #" & Format(ParametrosReporte(1), "yyyy-MM-dd") & "#"

        'Filtro por Terceros
        If ParametrosReporte(4) IsNot Nothing And ParametrosReporte(5) IsNot Nothing Then
            filtroConsulta &= " AND PaymentNoteId.IdSupplier.IdThirdParty.Nit >= '" & ParametrosReporte(4) & "' AND PaymentNoteId.IdSupplier.IdThirdParty.Nit <= '" & ParametrosReporte(5) & "'"
        End If

        'Filtro por Notas
        If ParametrosReporte(6) IsNot Nothing And ParametrosReporte(7) IsNot Nothing Then
            filtroConsulta &= " AND PaymentNoteId.Code >= '" & ParametrosReporte(6) & "' AND PaymentNoteId.Code <= '" & ParametrosReporte(7) & "'"
        End If

        If ParametrosReporte(2) <> 4 Then
            filtroConsulta &= " AND PaymentNoteId.Status = " & ParametrosReporte(2)
        End If

        If ParametrosReporte(3) <> 3 Then
            filtroConsulta &= " AND PaymentNoteId.Nature = " & ParametrosReporte(3)
        End If

        Dim listReport As List(Of PaymentsPaymentNotesAccountPayableAdvance) = XpoServiceex.Instance(IndigoSessionValues.TransactionalContainer).PaymentsService.GetCollection(Of PaymentsPaymentNotesAccountPayableAdvance)(Nothing, filtroConsulta)
        Me.DataSource = listReport

    End Sub

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Private Sub rptNotesListP_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        Me.INDLblUserPrint.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        Me.INDLBlSubtitle.Text = "Informe comprendido entre " & CDate(ParametrosReporte(0)).ToString("dd De MMMM Del yyyy") & " " & CDate(ParametrosReporte(1)).ToString("A dd De MMMM Del yyyy")
    End Sub
End Class