#Region "Imports"

Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports System.Text

#End Region

<ServiceContract()>
Public Interface ITreasuryServiceReports

#Region "Methods"

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Treasury].[SP_ReportReceiptsByRegime] realizado para cargar los datos del reporte listado de recibos de caja por regimen
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetReportReceiptsByRegime(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Treasury].[SP_ReportBankReconciliation] realizado para cargar los datos del reporte conciliación bancaria
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetReportBankReconciliation(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Treasury].[SP_ReportBankReconciliationAutomatic] realizado para cargar los datos del reporte conciliación bancaria automatica
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetReportBankReconciliationAutomatic(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet

#End Region

End Interface