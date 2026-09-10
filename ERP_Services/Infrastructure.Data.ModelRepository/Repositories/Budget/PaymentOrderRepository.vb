'***********************************************************************
' Assembly         : Infrastructure.Data.ModelRepository
' Author           : Juan Carlos Bermudez
' Created          : 09-09-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure

#End Region

Public Class PaymentOrderRepository
    Inherits GenericRepository(Of PaymentOrder)
    Implements IPaymentOrderRepository

#Region "Properties"

    'Contexto de Budget
    Private _context As IGlobalModelUnitOfWork

#End Region

#Region "Buider"
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' obtiene una orden de pago por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPaymentOrderByCode(code As String, BudgetaryValidityId As Integer) As PaymentOrder Implements IPaymentOrderRepository.GetPaymentOrderByCode
        Dim result = (From e In _context.PaymentOrder.Include("PaymentOrderDetail")
                      Where e.Code = code AndAlso e.BudgetaryValidityId = BudgetaryValidityId Select e).FirstOrDefault
        If result IsNot Nothing Then
            Dim thirdParty = (From tp In _context.ThirdParty Where tp.Id = result.ThirdPartyId Select tp).FirstOrDefault()
            result.NameThirdParty = thirdParty.Nit + " - " + thirdParty.Name

            If result.PaymentOrderDetail IsNot Nothing AndAlso result.PaymentOrderDetail.Count > 0 Then
                For Each item In result.PaymentOrderDetail
                    Dim obligationDetail = (From od In _context.ObligationDetail.AsNoTracking() Where od.Id = item.ObligationDetailId Select od).FirstOrDefault
                    Dim category = (From c In _context.Category.AsNoTracking() Where c.Id = obligationDetail.CategoryId Select c).FirstOrDefault
                    item.CodeNameCategory = category.Code + " - " + category.Name
                    item.BalanceAffects = obligationDetail.Balance

                    If category.FinancialSourceId IsNot Nothing Then
                        Dim financialSource = (From fs In _context.FinancialSource.AsNoTracking() Where fs.Id = category.FinancialSourceId Select fs).FirstOrDefault
                        item.CodeNameFinancialSource = financialSource.Code + " - " + financialSource.Name
                    End If

                    Dim obligation = (From ob In _context.Obligation.AsNoTracking() Where ob.Id = obligationDetail.ObligationId Select ob).FirstOrDefault
                    item.CodeObligation = obligation.Code
                    item.ObligationDate = obligation.DocumentDate

                    If obligationDetail.CommitmentDetailId IsNot Nothing Then
                        Dim commitment = (From c In _context.Commitment.AsNoTracking() Join cd In _context.CommitmentDetail On c.Id Equals cd.CommitmentId Where cd.Id = obligationDetail.CommitmentDetailId Select c).FirstOrDefault
                        item.CodeCommitment = commitment.Code
                    End If

                    Dim revueneType = (From rt In _context.RevenueType.AsNoTracking() Where rt.Id = obligationDetail.RevenueTypeId Select rt).FirstOrDefault
                    item.CodeNameRevenueType = revueneType.Code + " - " + revueneType.Name

                Next
            End If
            result.OriginalValue = (From e In _context.PaymentOrder.AsNoTracking() Where e.Code = code AndAlso e.BudgetaryValidityId = BudgetaryValidityId Select e).FirstOrDefault()
            Return result
        Else
            Return New PaymentOrder
        End If
    End Function

    ''' <summary>
    ''' Obtiene una orden de pago por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPaymentOrderById(id As Integer) As PaymentOrder Implements IPaymentOrderRepository.GetPaymentOrderById
        Dim result = (From rm In _context.PaymentOrder.Include("PaymentOrderDetail") Where rm.Id = id Select rm).FirstOrDefault()
        If result IsNot Nothing Then
            Dim thirdParty = (From tp In _context.ThirdParty Where tp.Id = result.ThirdPartyId Select tp).FirstOrDefault()
            result.NameThirdParty = thirdParty.Nit + " - " + thirdParty.Name

            If result.PaymentOrderDetail IsNot Nothing AndAlso result.PaymentOrderDetail.Count > 0 Then
                For Each item In result.PaymentOrderDetail
                    Dim obligationDetail = (From od In _context.ObligationDetail.AsNoTracking() Where od.Id = item.ObligationDetailId Select od).FirstOrDefault
                    Dim category = (From c In _context.Category.AsNoTracking() Where c.Id = obligationDetail.CategoryId Select c).FirstOrDefault
                    item.CodeNameCategory = category.Code + " - " + category.Name
                    item.BalanceAffects = obligationDetail.Balance

                    If category.FinancialSourceId IsNot Nothing Then
                        Dim financialSource = (From fs In _context.FinancialSource.AsNoTracking() Where fs.Id = category.FinancialSourceId Select fs).FirstOrDefault
                        item.CodeNameFinancialSource = financialSource.Code + " - " + financialSource.Name
                    End If

                    Dim obligation = (From ob In _context.Obligation.AsNoTracking() Where ob.Id = obligationDetail.ObligationId Select ob).FirstOrDefault
                    item.CodeObligation = obligation.Code
                    item.ObligationDate = obligation.DocumentDate

                    If obligationDetail.CommitmentDetailId IsNot Nothing Then
                        Dim commitment = (From c In _context.Commitment.AsNoTracking() Join cd In _context.CommitmentDetail On c.Id Equals cd.CommitmentId Where cd.Id = obligationDetail.CommitmentDetailId Select c).FirstOrDefault
                        item.CodeCommitment = commitment.Code
                    End If

                    Dim revueneType = (From rt In _context.RevenueType.AsNoTracking() Where rt.Id = obligationDetail.RevenueTypeId Select rt).FirstOrDefault
                    item.CodeNameRevenueType = revueneType.Code + " - " + revueneType.Name

                Next
            End If
            result.OriginalValue = (From po In _context.PaymentOrder.AsNoTracking() Where po.Id = id Select po).FirstOrDefault()
            Return result
        Else
            Return New PaymentOrder
        End If
    End Function

    Public Function SP_SavePaymentOrder(PaymentOrderXml As String, PaymentOrderDetailForDeleteXml As String, CodeUser As String) As SP_SavePaymentOrder_Result Implements IPaymentOrderRepository.SP_SavePaymentOrder
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SavePaymentOrder(PaymentOrderXml, PaymentOrderDetailForDeleteXml, CodeUser).SingleOrDefault()
    End Function

#End Region

End Class
