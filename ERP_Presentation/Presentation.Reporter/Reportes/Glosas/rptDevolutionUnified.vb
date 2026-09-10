#Region "Librerias Importadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Domain.Entities
#End Region

Public Class rptDevolutionUnified
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Dim filtroConsulta As String = "GlosaDevolutionsReceptionCId.Id = " & Me.ParametrosReporte(0) & "AND (InvoiceNumber) = '" & Me.ParametrosReporte(1) & "'"
        Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).GlosasService.GetCollection(Of GlosasRepository.GlosaDevolutionsReceptionDXpo)(Nothing, filtroConsulta)
    End Sub

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Private Sub rptDevolutionUnified_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        Me.INDPrCompanyName.Value = IndigoSessionValues.IndigoCompanyName.ToUpper()
        Me.INDPrCompanyNit.Value = IndigoSessionValues.IndigoCompanyNit
        If Me.ParametrosReporte.Count > 2 Then
            INDPrOperatingUnit.Value = Me.ParametrosReporte(2)
        End If
    End Sub
End Class