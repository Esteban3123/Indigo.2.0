#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel

#End Region

<ServiceContract()> _
Public Interface IPaymentReports

    ''' <summary>
    ''' Metodo que realiza el llamado al storeProcedure SP_ReportExtractAccountPayable
    ''' </summary>
    ''' <param name="InitialDate"></param>
    ''' <param name="EndDate"></param>
    ''' <param name="InitialNit"></param>
    ''' <param name="EndNit"></param>
    ''' <param name="InitialBillNumber"></param>
    ''' <param name="EndBillNumber"></param>
    ''' <param name="TypeReport"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetListReportExtractAccountPayable(InitialDate As Date?, EndDate As Date?, InitialNit As String, EndNit As String, InitialBillNumber As String, EndBillNumber As String, TypeReport As Byte, Session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Payments].[SP_ReportPaymentsByAge] realizado para cargar los datos de pagos por edades
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="filters"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetListReportPaymentsByAge(criterias As Dictionary(Of String, String), filters As Dictionary(Of String, String), Session As SessionValues) As DataSet

End Interface
