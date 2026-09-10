'***********************************************************************
' Assembly         : DistributedServices.Treasury
' Author           : Johan Sebastian Cuellar Esquivel
' Created          : 13-04-2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
#End Region

<ServiceContract()>
Public Interface ITreasuryServiceUploadBankStatements
    <OperationContract()>
    Function GetUploadBankStatementsByCode(code As String, audit As AuditMessage) As UploadBankStatements
    <OperationContract()>
    Function SaveUploadBankStatements(UploadBankStatements As UploadBankStatements, audit As AuditMessage) As ActionResult(Of UploadBankStatements)
    <OperationContract()>
    Function GetUploadBankStatementsDetailByUploadBankStatementsId(UploadBankStatementsId As Integer) As List(Of UploadBankStatementsDetail)
    <OperationContract()>
    Function SetCopyPasteOrImportFileSetBankStatementsDetail(session As SessionValues, dataImportFile As List(Of ImportFileRow), dataCopyPaste As List(Of List(Of String))) As ActionResult(Of List(Of UploadBankStatementsDetail))
    ''' <summary>
    ''' Función encargada de obtener los Conceptos de Conciliación acorde con la entidad Bancaria
    ''' </summary>
    ''' <param name="EntityBankAccountId"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetBankConciliationConceptsByEntityBankAccountId(EntityBankAccountId As Integer) As ActionResult(Of List(Of BankConciliationConcepts))
    ''' <summary>
    ''' Función encargada de asignar el DocumentType a los Detalles del extracto acorde con sus conceptos
    ''' </summary>
    ''' <param name="ConciliationConcepts"></param>
    ''' <param name="UploadBankStatementDetails"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function MatchConciliationConceptsWithDescriptionTransaction(ConciliationConcepts As List(Of BankConciliationConcepts), UploadBankStatementDetails As List(Of UploadBankStatementsDetail)) As ActionResult(Of List(Of UploadBankStatementsDetail))
    ''' <summary>
    ''' Función que envía el archivo del extracto bancario a la API de extracción y retorna el resultado.
    ''' La URL de la API se resuelve server-side desde la BD; el cliente nunca la recibe.
    ''' </summary>
    <OperationContract()>
    Function ExtractBankStatementDetail(fileBytes As Byte(), fileName As String, year As String, audit As AuditMessage) As ActionResult(Of String)

End Interface
