#Region "Librerias Improtadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.AccountingRepository
Imports Domain.Entities
Imports DevExpress.XtraReports.UI
Imports System.Drawing.Printing
#End Region

Public Class rptDiaryVouchers
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Public Sub CargarDataSource() Implements IReport.CargarDataSource

        Dim filtroConsulta As String = Nothing
        filtroConsulta = "IdAccounting.VoucherDate >= #" & Format(ParametrosReporte(10), "yyyy-MM-dd") & "# AND IdAccounting.VoucherDate <= #" & Format(ParametrosReporte(11), "yyyy-MM-dd") & "# AND IdAccounting.LegalBookId = " & ParametrosReporte(13)

        'filtro por estado
        If (ParametrosReporte(12) <> 4) Then
            filtroConsulta &= " AND IdAccounting.Status = " & ParametrosReporte(12)
        End If

        'filtro por tipo de comprobante
        If ParametrosReporte(2) IsNot Nothing And ParametrosReporte(3) IsNot Nothing Then
            filtroConsulta &= " AND IdAccounting.IdJournalVoucher.Code >= '" & ParametrosReporte(2) & "' AND IdAccounting.IdJournalVoucher.Code <= '" & ParametrosReporte(3) & "'"
        End If

        'filtro por comprobante
        If ParametrosReporte(4) IsNot Nothing And ParametrosReporte(5) IsNot Nothing Then
            filtroConsulta &= " AND IdAccounting.Consecutive >= " & ParametrosReporte(4) & " AND IdAccounting.Consecutive <= " & ParametrosReporte(5)
        End If

        'filtro por Tercero
        If ParametrosReporte(6) IsNot Nothing And ParametrosReporte(7) IsNot Nothing Then
            filtroConsulta &= " AND IdThirdParty.Nit >= '" & ParametrosReporte(6) & "' AND IdThirdParty.Nit <= '" & ParametrosReporte(7) & "'"
        End If

        'filtro por Centro De Costo
        If ParametrosReporte(8) IsNot Nothing And ParametrosReporte(9) IsNot Nothing Then
            filtroConsulta &= " AND IdCostCenter.Code >= '" & ParametrosReporte(8) & "' AND IdCostCenter.Code <= '" & ParametrosReporte(9) & "'"
        End If
        Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).AccountingService.GetCollection(Of JournalVoucherDetailsXpo)(Nothing, filtroConsulta)

    End Sub

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Private Sub rptDiaryVouchers_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint

        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        INDLblDates.Text = "Informe comprendido entre " & CDate(ParametrosReporte(10)).ToString("dd De MMMM Del yyyy") & " " & CDate(ParametrosReporte(11)).ToString("A dd De MMMM Del yyyy")
        Me.INDLblUserPrint.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        'imprimir reporte segun el nivel de la cuenta
        If ParametrosReporte(1) = 2 Then
            INDGhAux.Visible = False
            INDGhSubAuxiliar.Visible = False

        ElseIf ParametrosReporte(1) = 1 Then
            INDGhSubAccount.Visible = False
            INDGhAux.Visible = False
            INDGhSubAuxiliar.Visible = False
        End If
    End Sub

    Private Sub XrTable4_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTable4.BeforePrint
        Dim table As XRTable = CType(sender, XRTable)
        Dim row As XRTableRow = table.Rows(0)

        If ParametrosReporte(0) = 2 Then
            If row.Cells("XrTableCell28") IsNot Nothing And row.Cells("XrTableCell29") IsNot Nothing Then
                row.Cells.Remove(XrTableCell28)
                row.Cells.Remove(XrTableCell29)
                XrTableCell26.WidthF = 75.71
                XrTableCell27.WidthF = 375.3
                XrTableCell30.WidthF = 84.97
                XrTableCell31.WidthF = 89.65
                XrTableCell32.WidthF = 86.22
                XrTableCell33.WidthF = 87.15
            End If
        End If
    End Sub

    Private Sub XrTable5_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTable5.BeforePrint
        Dim table As XRTable = CType(sender, XRTable)
        Dim row As XRTableRow = table.Rows(0)

        If ParametrosReporte(0) = 2 Then
            If row.Cells("XrTableCell36") IsNot Nothing And row.Cells("XrTableCell37") IsNot Nothing Then
                row.Cells.Remove(XrTableCell36)
                row.Cells.Remove(XrTableCell37)
                XrTableCell34.WidthF = 75.71
                XrTableCell35.WidthF = 375.3
                XrTableCell38.WidthF = 84.97
                XrTableCell39.WidthF = 89.65
                XrTableCell40.WidthF = 86.22
                XrTableCell41.WidthF = 87.15
            End If
        End If
    End Sub

    Private Sub XrTable6_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTable6.BeforePrint
        Dim table As XRTable = CType(sender, XRTable)
        Dim row As XRTableRow = table.Rows(0)

        If ParametrosReporte(0) = 2 Then
            If row.Cells("XrTableCell44") IsNot Nothing And row.Cells("XrTableCell45") IsNot Nothing Then
                row.Cells.Remove(XrTableCell44)
                row.Cells.Remove(XrTableCell45)
                XrTableCell42.WidthF = 75.71
                XrTableCell43.WidthF = 375.3
                XrTableCell46.WidthF = 84.97
                XrTableCell47.WidthF = 89.65
                XrTableCell48.WidthF = 86.22
                XrTableCell49.WidthF = 87.15
            End If
        End If
    End Sub

    Private Sub XrTable8_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTable8.BeforePrint
        Dim table As XRTable = CType(sender, XRTable)
        Dim row As XRTableRow = table.Rows(0)
        If ParametrosReporte(0) = 2 Then
            If row.Cells("XrTableCell52") IsNot Nothing And row.Cells("XrTableCell56") IsNot Nothing Then
                row.Cells.Remove(XrTableCell52)
                row.Cells.Remove(XrTableCell56)
                XrTableCell51.WidthF = 75.71
                XrTableCell55.WidthF = 375.3
                XrTableCell59.WidthF = 84.97
                XrTableCell54.WidthF = 89.65
                XrTableCell60.WidthF = 86.22
                XrTableCell58.WidthF = 87.15
            End If
        End If
    End Sub

    Private Sub XrTable9_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTable9.BeforePrint
        Dim table As XRTable = CType(sender, XRTable)
        Dim row As XRTableRow = table.Rows(0)
        If ParametrosReporte(0) = 2 Then
            If row.Cells("XrTableCell12") IsNot Nothing And row.Cells("XrTableCell13") IsNot Nothing Then
                row.Cells.Remove(XrTableCell12)
                row.Cells.Remove(XrTableCell13)
                XrTableCell10.WidthF = 75.71
                XrTableCell11.WidthF = 375.3
                XrTableCell14.WidthF = 84.97
                XrTableCell15.WidthF = 89.65
                XrTableCell17.WidthF = 86.22
                XrTableCell18.WidthF = 87.15
            End If
        End If
    End Sub
End Class