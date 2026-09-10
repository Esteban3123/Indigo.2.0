#Region "Imports"

Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports System.Text

#End Region

Public Interface IReportAdminService
    Inherits IDisposable

#Region "Methods"

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Treasury].[SP_ReportReceiptsByRegime] realizado para cargar los datos del reporte listado de recibos de caja por regimen
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Function GetReportReceiptsByRegime(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Treasury].[SP_ReportBankReconciliation] realizado para cargar los datos del reporte conciliación bancaria
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Function GetReportBankReconciliation(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Treasury].[SP_ReportBankReconciliationAutomatic] realizado para cargar los datos del reporte conciliación bancaria automatica
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Function GetReportBankReconciliationAutomatic(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet

#End Region

End Interface