#Region "Imports"

Imports System.Drawing.Printing
Imports DevExpress.XtraReports.Parameters
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.BillingRepository

#End Region

Public Class rptBillingNoteDetail
    Implements IReport

#Region "Properties"

    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    ''' <summary>
    ''' constante con el nombre del modulo de glosas
    ''' </summary>
    Private Const GLOSAS_MODULE = "Glosas"

#End Region

#Region "Load Data"

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Dim filtroConsulta As String = $"Id ={ParametrosReporte(0)} AND (NoteType = 6 OR PortfolioNoteEntityName = '{GLOSAS_MODULE}')  "
        Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).BillingService.GetCollection(Of ViewBillingNoteXpo)(Nothing, filtroConsulta)
    End Sub

#End Region

#Region "Methods"

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

#End Region

#Region "Events"

    Private Sub rptBillingNoteDetail_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        If Me.Parameters.Count > 0 And Me.Parameters(0).Value > 0 Then
            Dim ParametrosFilter As ParameterCollection = Me.Parameters
            ParametrosReporte = New Object() {ParametrosFilter("INDIdBillingNoteSubreport").Value}
            CargarDataSource()
        End If

        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
    End Sub

#End Region

End Class