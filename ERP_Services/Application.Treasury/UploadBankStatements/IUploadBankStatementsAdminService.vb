'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Johan Sebastian Cuellar Esquivel
' Created          : 13-04-2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IUploadBankStatementsAdminService
    Inherits IDisposable
    Function GetUploadBankStatementsByCode(code As String, ByVal audit As AuditMessage) As UploadBankStatements

    Function SaveUploadBankStatements(UploadBankStatements As UploadBankStatements, ByVal audit As AuditMessage) As ActionResult(Of UploadBankStatements)

    Function GetUploadBankStatementsDetailByUploadBankStatementsId(UploadBankStatementsId As Integer) As List(Of UploadBankStatementsDetail)

    Function SetCopyPasteOrImportFileSetBankStatementsDetail(dataImportFile As List(Of ImportFileRow), dataCopyPaste As List(Of List(Of String))) As ActionResult(Of List(Of UploadBankStatementsDetail))
    ''' <summary>
    ''' Función encargada de obtener los Conceptos de Conciliación acorde con la entidad Bancaria
    ''' </summary>
    ''' <param name="EntityBankAccountId"></param>
    ''' <returns></returns>
    Function GetBankConciliationConceptsByEntityBankAccountId(EntityBankAccountId As Integer) As ActionResult(Of List(Of BankConciliationConcepts))
    ''' <summary>
    ''' Función encargada de asignar el DocumentType a los Detalles del extracto acorde con sus conceptos
    ''' </summary>
    ''' <param name="ConciliationConcepts"></param>
    ''' <param name="UploadBankStatementDetails"></param>
    ''' <returns></returns>
    Function MatchConciliationConceptsWithDescriptionTransaction(ConciliationConcepts As List(Of BankConciliationConcepts), UploadBankStatementDetails As List(Of UploadBankStatementsDetail)) As ActionResult(Of List(Of UploadBankStatementsDetail))
    ''' <summary>
    ''' Función para invocar la API de Cargue de Extracto Bancario
    ''' </summary>
    ''' <param name="fileBytes"></param>
    ''' <param name="fileName"></param>
    ''' <param name="year"></param>
    ''' <param name="endpointUrl"></param>
    ''' <returns></returns>
    Function ExtractBankStatementDetail(fileBytes As Byte(), fileName As String, year As String, endpointUrl As String) As ActionResult(Of String)

End Interface
