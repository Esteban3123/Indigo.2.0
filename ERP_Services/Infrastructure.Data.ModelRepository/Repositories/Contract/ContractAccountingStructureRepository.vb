'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 04/10/2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Dynamic
Imports System

Public Class ContractAccountingStructureRepository
    Inherits GenericRepository(Of ContractAccountingStructure)
    Implements IContractAccountingStructureRepository

    ''' <summary>
    ''' Contexto de payments
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de payments
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    Public Function GetContractAccountingStructure(code As String) As ContractAccountingStructure Implements IContractAccountingStructureRepository.GetContractAccountingStructure
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d In Me._context.ContractAccountingStructure
        Where d.Code.Equals(code.Trim())
                   Select d).FirstOrDefault
        If res IsNot Nothing Then

            Dim account As MainAccounts

            If res.AccountRecoveryFeeId IsNot Nothing Then
                account = (From ma In _context.MainAccounts.AsNoTracking Where ma.Id = res.AccountRecoveryFeeId Select ma).FirstOrDefault
                res.AccountRecoveryFeeDescription = account.Number + " - " + account.Name
            End If
            If res.AccountParticularId IsNot Nothing Then
                account = (From ma In _context.MainAccounts.AsNoTracking Where ma.Id = res.AccountParticularId Select ma).FirstOrDefault
                res.AccountParticularDescription = account.Number + " - " + account.Name
            End If
            If res.AccountWithoutRadicateId IsNot Nothing Then
                account = (From ma In _context.MainAccounts.AsNoTracking Where ma.Id = res.AccountWithoutRadicateId Select ma).FirstOrDefault
                res.AccountWithoutRadicateDescription = account.Number + " - " + account.Name
            End If
            If res.AccountRadicateId IsNot Nothing Then
                account = (From ma In _context.MainAccounts.AsNoTracking Where ma.Id = res.AccountRadicateId Select ma).FirstOrDefault
                res.AccountRadicateDescription = account.Number + " - " + account.Name
            End If
            If res.AccountObjectionRemediedId IsNot Nothing Then
                account = (From ma In _context.MainAccounts.AsNoTracking Where ma.Id = res.AccountObjectionRemediedId Select ma).FirstOrDefault
                res.AccountObjectionRemediedDescription = account.Number + " - " + account.Name
            End If
            If res.AccountConciliationId IsNot Nothing Then
                account = (From ma In _context.MainAccounts.AsNoTracking Where ma.Id = res.AccountConciliationId Select ma).FirstOrDefault
                res.AccountConciliationDescription = account.Number + " - " + account.Name
            End If
            If res.AccountLegalCollectionId IsNot Nothing Then
                account = (From ma In _context.MainAccounts.AsNoTracking Where ma.Id = res.AccountLegalCollectionId Select ma).FirstOrDefault
                res.AccountLegalCollectionDescription = account.Number + " - " + account.Name
            End If
            If res.AccountHardCollectionId IsNot Nothing Then
                account = (From ma In _context.MainAccounts.AsNoTracking Where ma.Id = res.AccountHardCollectionId Select ma).FirstOrDefault
                res.AccountHardCollectionDescription = account.Number + " - " + account.Name
            End If
            If res.AccountDebitOrderId IsNot Nothing Then
                account = (From ma In _context.MainAccounts.AsNoTracking Where ma.Id = res.AccountDebitOrderId Select ma).FirstOrDefault
                res.AccountDebitOrderDescription = account.Number + " - " + account.Name
            End If
            If res.AccountCreditOrderId IsNot Nothing Then
                account = (From ma In _context.MainAccounts.AsNoTracking Where ma.Id = res.AccountCreditOrderId Select ma).FirstOrDefault
                res.AccountCreditOrderDescription = account.Number + " - " + account.Name
            End If

            account = (From ma In _context.MainAccounts.AsNoTracking Where ma.Id = res.DebitAccountDeteriorationId Select ma).FirstOrDefault
            res.DebitAccountDeteriorationDescription = account.Number + " - " + account.Name

            account = (From ma In _context.MainAccounts.AsNoTracking Where ma.Id = res.CreditAccountDeteriorationId Select ma).FirstOrDefault
            res.CreditAccountDeteriorationDescription = account.Number + " - " + account.Name

            account = (From ma In _context.MainAccounts.AsNoTracking Where ma.Id = res.ReversalAccountDeteriorationId Select ma).FirstOrDefault
            res.ReversalAccountDeteriorationDescription = account.Number + " - " + account.Name

            account = (From ma In _context.MainAccounts.AsNoTracking Where ma.Id = res.PreviousPeriodReversalAccountDeteriorationId Select ma).FirstOrDefault
            res.PreviousPeriodReversalAccountDeteriorationDescription = account.Number + " - " + account.Name

            account = (From ma In _context.MainAccounts.AsNoTracking Where ma.Id = res.CreditProvisionAccountId Select ma).FirstOrDefault
            res.CreditProvisionAccountDescription = account.Number + " - " + account.Name

            account = (From ma In _context.MainAccounts.AsNoTracking Where ma.Id = res.DebitProvisionAccountId Select ma).FirstOrDefault
            res.DebitProvisionAccountDescription = account.Number + " - " + account.Name

            res.ServicesPendingBillingMainAccountDescription = (From x In _context.MainAccounts.AsNoTracking Where x.Id = res.ServicesPendingBillingMainAccountId Select String.Concat(x.Number, " - ", x.Name)).FirstOrDefault()

            res.OriginalValue = (From g In _context.ContractAccountingStructure.AsNoTracking
                                  Where g.Code.Equals(code.Trim())
                                  Select g).FirstOrDefault

            Return res
        Else
            Return New ContractAccountingStructure()
        End If
    End Function

    Public Function GetActiveList() As List(Of ContractAccountingStructure) Implements IContractAccountingStructureRepository.GetActiveList
        Return (From d In Me._context.ContractAccountingStructure.AsNoTracking()
                Where d.Status = True
                Order By d.Code
                Select d).ToList()
    End Function

    Public Function GetListByCodes(codes As List(Of String)) As List(Of ContractAccountingStructure) Implements IContractAccountingStructureRepository.GetListByCodes
        If codes Is Nothing OrElse codes.Count = 0 Then
            Return New List(Of ContractAccountingStructure)()
        End If
        Dim trimmed = codes.Where(Function(c) Not String.IsNullOrWhiteSpace(c)).Select(Function(c) c.Trim()).Distinct().ToList()
        Return (From d In Me._context.ContractAccountingStructure.AsNoTracking()
                Where trimmed.Contains(d.Code) AndAlso d.Status = True
                Select d).ToList()
    End Function

    Public Function GetContractAccountingStructureById(id As Integer) As ContractAccountingStructure Implements IContractAccountingStructureRepository.GetContractAccountingStructureById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d In Me._context.ContractAccountingStructure.AsNoTracking Where d.Id = id Select d).FirstOrDefault
        If res IsNot Nothing AndAlso res.Id > 0 Then
            Return res
        Else
            Return New ContractAccountingStructure()
        End If
    End Function

End Class
