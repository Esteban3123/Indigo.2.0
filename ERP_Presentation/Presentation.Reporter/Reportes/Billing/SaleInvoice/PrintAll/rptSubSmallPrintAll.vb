#Region "Librerias Improtadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.BillingRepository
#End Region

Public Class rptSubSmallPrintAll
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).BillingService.ListViewPrintAllInvoiceDetailByInvoiceId(ParametrosReporte(0))
    End Sub

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte
End Class