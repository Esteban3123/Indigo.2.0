'***********************************************************************
' Assembly         : Infrastructure.Data.TreasuryRepository
' Author           : Johan Sebastian Cuellar Esquivel
' Created          : 13-04-2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Core.Objects


Public Class UploadBankStatementsRepository
    Inherits GenericRepository(Of UploadBankStatements)
    Implements IUploadBankStatementsRepository

    'Contexto de payroll
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    Public Function GetUploadBankStatementsByCode(code As String) As UploadBankStatements Implements IUploadBankStatementsRepository.GetUploadBankStatementsByCode
        Dim res = (From hc In _context.UploadBankStatements Where hc.Code = code Select hc).FirstOrDefault()
        If res IsNot Nothing Then
            res.OriginalValue = (From hc In _context.UploadBankStatements.AsNoTracking() Where hc.Code = code Select hc).FirstOrDefault()
            Return res
        End If
        Return New UploadBankStatements
    End Function

    Public Function GetUploadBankStatementsById(id As Integer) As UploadBankStatements Implements IUploadBankStatementsRepository.GetUploadBankStatementsById
        Dim res = (From hc In _context.UploadBankStatements Where hc.Id = id Select hc).FirstOrDefault()
        If res IsNot Nothing Then
            Return res
        End If
        Return New UploadBankStatements
    End Function

    Public Function GenerateUploadBankStatementsSP(xml As String, UserCode As String) As ObjectResult(Of SP_SaveUploadBankStatements_Result) Implements IUploadBankStatementsRepository.GenerateUploadBankStatementsSP
        Return _context.SP_SaveUploadBankStatements(xml, UserCode)
    End Function

    Public Function GetUploadBankStatementsDetailByUploadBankStatementsId(UploadBankStatementsId As Integer) As List(Of UploadBankStatementsDetail) Implements IUploadBankStatementsRepository.GetUploadBankStatementsDetailByUploadBankStatementsId
        Dim res = (From hcd In _context.UploadBankStatementsDetail Where hcd.UploadBankStatementsId = UploadBankStatementsId Select hcd).ToList()
        Return res
    End Function

    Public Function SetUploadBankStatements(xml As String) As ObjectResult(Of SP_SetUploadBankStatementsDetail_Result) Implements IUploadBankStatementsRepository.SetUploadBankStatementsDetail
        Return _context.SP_SetUploadBankStatementsDetail(xml)
    End Function

    ''' <summary>
    ''' Función que trae los Conceptos de Concialiación relacionados con le Entidad Bancaria
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetBankConciliationConceptsByEntityBankAccounts(id As Integer) As List(Of BankConciliationConcepts) Implements IUploadBankStatementsRepository.GetBankConciliationConceptsByEntityBankAccounts
        Dim res = (From bcc In _context.BankConciliationConcepts
                   Join bd In _context.BankDetail On bcc.Id Equals bd.BankConciliationConceptsId
                   Join b In _context.Bank On bd.BankId Equals b.Id
                   Join eba In _context.EntityBankAccounts On b.Id Equals eba.IdBank
                   Where eba.Id = id
                   Select bcc).ToList()

        Return res
    End Function
End Class
