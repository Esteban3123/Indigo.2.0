Imports System.Data.Entity.Core.Objects
Imports System.Data.Entity.Infrastructure
Imports Domain.Entities
Imports Infrastructure.Data.Base

Public Class BankReconciliationRepository
    Inherits GenericRepository(Of BankReconciliation)
    Implements IBankReconciliationRepository

#Region "Fields"

    Private _context As IGlobalModelUnitOfWork

#End Region

#Region "Builders"

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        Me._context = context
    End Sub

#End Region

#Region "Methods"

    Public Function GetBankReconciliationById(id As Integer, Optional tracking As Boolean = True) As BankReconciliation Implements IBankReconciliationRepository.GetBankReconciliationById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d In Me._context.BankReconciliation.Include("BankReconciliationExtractDetail") Where d.Id = id Select d).ToList
        If res.Count > 0 Then
            res(0).OriginalValue = (From d In Me._context.BankReconciliation.AsNoTracking.Include("BankReconciliationExtractDetail").AsNoTracking Where d.Id = id Select d).SingleOrDefault()
            Return res.SingleOrDefault
        Else
            Return New BankReconciliation()
        End If
    End Function

    Public Function GetBankReconciliationByCode(code As String, Optional tracking As Boolean = True) As BankReconciliation Implements IBankReconciliationRepository.GetBankReconciliationByCode
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As BankReconciliation In Me._context.BankReconciliation.AsNoTracking.Include("BankReconciliationExtractDetail").AsNoTracking Where d.Code.Equals(code.Trim()) Select d).FirstOrDefault
        If res IsNot Nothing Then
            res.EntityBankAccountCodeName = (From p In _context.EntityBankAccounts.AsNoTracking.Include("Bank").AsNoTracking Where res.EntityBankAccountId = p.Id Select p.Code + " - " + p.Bank.Name).FirstOrDefault

            Return res
        Else
            Return New BankReconciliation()
        End If
    End Function

    Public Function SP_GetBankReconciliationDetails(XmlCriterias As String) As List(Of SP_GetBankReconciliationDetails_Result) Implements IBankReconciliationRepository.SP_GetBankReconciliationDetails
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_GetBankReconciliationDetails(XmlCriterias).ToList()
    End Function

    Public Function GenerateBankReconciliationSP(xml As String, UserCode As String) As ObjectResult(Of SP_SaveBankReconciliation_Result) Implements IBankReconciliationRepository.GenerateBankReconciliationSP
        Return _context.SP_SaveBankReconciliation(xml, UserCode)
    End Function

#End Region

End Class
