'***********************************************************************
' Assembly         : Domain.Treasury
' Author           : Johan Sebastian Cuellar Esquivel
' Created          : 13-04-2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface IUploadBankStatementsRepository
    Inherits IRepository(Of UploadBankStatements)

    Function GetUploadBankStatementsDetailByUploadBankStatementsId(UploadBankStatementsId As Integer) As List(Of UploadBankStatementsDetail)

    Function GetUploadBankStatementsById(id As Integer) As UploadBankStatements

    Function GenerateUploadBankStatementsSP(xml As String, UserCode As String) As Entity.Core.Objects.ObjectResult(Of SP_SaveUploadBankStatements_Result)

    Function GetUploadBankStatementsByCode(code As String) As UploadBankStatements

    Function SetUploadBankStatementsDetail(xml As String) As Entity.Core.Objects.ObjectResult(Of SP_SetUploadBankStatementsDetail_Result)
    ''' <summary>
    ''' Función encargada de obtener los Conceptos de Conciliación por el Id de la entidad Bancaria
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Function GetBankConciliationConceptsByEntityBankAccounts(id As Integer) As List(Of BankConciliationConcepts)

End Interface
