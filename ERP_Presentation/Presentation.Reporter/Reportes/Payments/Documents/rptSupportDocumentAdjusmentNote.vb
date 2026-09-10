Imports System.Drawing.Printing
Imports DevExpress.XtraReports.Parameters
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PaymentsRepository

Public Class rptSupportDocumentAdjusmentNote
    Implements IReport
    Implements IReportAsync

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Public ReadOnly Property NameReport As String Implements IReport.NameReport

    Private DataList As List(Of ViewElectronicDocumentSupportRptXpo)

    Public Sub CargarImagenes() Implements IReport.CargarImagenes
    End Sub

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        DataList = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PaymentsService.GetCollection(Of ViewElectronicDocumentSupportRptXpo)(Nothing, $"Id = {ParametrosReporte(0)}")
        If DataList?.Any() Then
            DataSource = DataList
        Else
            Me.Visible = False
        End If
    End Sub

    Public Function CargarDataSourceAsync() As Task Implements IReportAsync.CargarDataSourceAsync
        Return Task.Factory.StartNew(AddressOf CargarDataSource)
    End Function

    Private Sub rpt_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        CargarDataSource()
        LoadCompanyInfo()
    End Sub

    Private Sub LoadCompanyInfo()
        Dim firstRow As ViewElectronicDocumentSupportRptXpo = DataList?.FirstOrDefault

        If firstRow IsNot Nothing Then
            Me.INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
            Me.INDLblNitCompany.Text = $"Nit: {IndigoSessionValues.IndigoCompanyNit} - Dirección: {firstRow.AddresssCustomer} - Teléfono: {firstRow.PhoneCustomer}"
            Me.INDLblDocumentNumber.Text = $"N° {firstRow.DocumentNumber}"

            If String.IsNullOrEmpty(firstRow.QR) Then
                XrBarCode2.Visible = False
            End If
        End If
    End Sub
End Class