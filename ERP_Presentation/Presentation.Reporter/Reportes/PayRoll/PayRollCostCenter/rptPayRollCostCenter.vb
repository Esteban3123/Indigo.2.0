#Region "Librerias Improtadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Domain.Entities
Imports DevExpress.XtraReports.UI
Imports System.Drawing.Printing
#End Region

Public Class rptPayRollCostCenter
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Public Sub CargarDataSource() Implements IReport.CargarDataSource

        Dim filtro As String = "CreationDate >= '" & ParametrosReporte(0) & "' AND CreationDate <= '" & ParametrosReporte(1) & "' AND State = '" & ParametrosReporte(2) & "'"

        If ParametrosReporte(3) IsNot Nothing And ParametrosReporte(4) IsNot Nothing Then
            filtro &= "AND Code >= '" & ParametrosReporte(3) & "' AND Code <= '" & ParametrosReporte(4) & "'"

        End If


        Me.DataSource = XpoService.ListCollectionAccountingReport(Of PayrollCostCenterReportXpo)(IndigoSessionValues.TransactionalContainer, Nothing, filtro)

    End Sub

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Private Sub rptPayRollCostCenter_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint

        'Cargar los valores del titulo (nombre de la empresa y Nit)
        Me.INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        Me.INDLblNitCompany.Text = "Nit : " & IndigoSessionValues.IndigoCompanyNit
    End Sub
End Class