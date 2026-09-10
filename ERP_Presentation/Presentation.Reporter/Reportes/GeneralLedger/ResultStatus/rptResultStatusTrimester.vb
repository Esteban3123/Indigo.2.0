#Region "Librerias Improtadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.AccountingRepository
Imports Domain.Entities
Imports DevExpress.XtraReports.UI
Imports System.Drawing.Printing
#End Region

Public Class rptResultStatusTrimester
    Implements IReport

    Public Sub CargarDataSource() Implements IReport.CargarDataSource

    End Sub

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    ''' <summary>
    ''' Evento que se dispara al pintar el reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub rptResultStatusTrimester_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        Dim isVisibleNumberAccount As Boolean = Me.Parameters("IncludedAccountNumber").Value
        If isVisibleNumberAccount = False Then
            Dim xrTableRow As XRTableRow = XrTable1.Rows(0)
            If xrTableRow.Cells(XrTableCell1.Name) IsNot Nothing Then
                Dim control = Me.FindControl(XrTableCell1.Name, True)
                xrTableRow.Cells.Remove(control)
            End If

            xrTableRow = XrTable10.Rows(0)
            If xrTableRow.Cells(XrTableCell28.Name) IsNot Nothing Then
                Dim control = Me.FindControl(XrTableCell28.Name, True)
                xrTableRow.Cells.Remove(control)
            End If
        End If
    End Sub

End Class