#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PaymentsRepository
Imports DevExpress.XtraReports.UI
#End Region

Public Class rptSubMassiveConfirmNotesDC
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).TreasuryService.GetCollection(Of PaymentsPaymentNotes)(Nothing, "Code In ('" & String.Join("','", CType(ParametrosReporte(0), List(Of String))) & "')")
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