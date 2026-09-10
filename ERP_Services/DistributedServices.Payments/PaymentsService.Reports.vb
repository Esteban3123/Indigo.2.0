#Region "Imports"

Imports Application.Payments
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

#End Region

Partial Public Class PaymentsService

    Public Function GetListReportExtractAccountPayable(InitialDate As Date?, EndDate As Date?, InitialNit As String, EndNit As String, InitialBillNumber As String, EndBillNumber As String, TypeReport As Byte, Session As SessionValues) As DataSet Implements IPaymentReports.GetListReportExtractAccountPayable
        Using service As IReportsAdminService = Container.Current.Resolve(Of ReportsAdminService)()
            Return service.GetListReportExtractAccountPayable(InitialDate, EndDate, InitialNit, EndNit, InitialBillNumber, EndBillNumber, TypeReport, Session)
        End Using
    End Function

    Public Function GetListReportPaymentsByAge(criterias As Dictionary(Of String, String), filters As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements IPaymentReports.GetListReportPaymentsByAge
        Using service As IReportsAdminService = Container.Current.Resolve(Of IReportsAdminService)()
            Return service.GetListReportPaymentsByAge(criterias, filters, Session)
        End Using
    End Function

End Class
