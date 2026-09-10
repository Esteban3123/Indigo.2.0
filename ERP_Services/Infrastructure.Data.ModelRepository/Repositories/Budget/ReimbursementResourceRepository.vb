'***********************************************************************
' Assembly         : Infrastructure.Data.ModelRepository
' Author           : Juan Carlos Bermudez
' Created          : 14-09-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure

#End Region

Public Class ReimbursementResourceRepository
    Inherits GenericRepository(Of ReimbursementResource)
    Implements IReimbursementResourceRepository

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
    ''' obtiene un compromiso por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetReimbursementResourceByCode(code As String, BudgetaryValidityId As Integer) As ReimbursementResource Implements IReimbursementResourceRepository.GetReimbursementResourceByCode
        Dim result = (From e In _context.ReimbursementResource.Include("ReimbursementResourceDetaill").Include("ReimbursementResourceDetaill.PaymentOrderDetail")
                      Where e.Code = code AndAlso e.BudgetaryValidityId = BudgetaryValidityId Select e).FirstOrDefault
        If result IsNot Nothing Then
            Dim paymentOrder = (From po In _context.PaymentOrder Where po.Id = result.PaymentOrderId Select po).FirstOrDefault()
            result.CodePaymentOrder = paymentOrder.Code

            If result.ReimbursementResourceDetaill IsNot Nothing AndAlso result.ReimbursementResourceDetaill.Count > 0 Then
                For Each item In result.ReimbursementResourceDetaill
                    Dim obligationDetail = (From od In _context.ObligationDetail Where od.Id = item.PaymentOrderDetail.ObligationDetailId Select od).FirstOrDefault
                    item.BalanceAffects = item.PaymentOrderDetail.Balance

                    Dim category = (From c In _context.Category Where c.Id = obligationDetail.CategoryId Select c).FirstOrDefault
                    item.CategoryId = category.Id
                    item.CodeNameCategory = category.Code + " - " + category.Name

                    Dim revueneType = (From rt In _context.RevenueType Where rt.Id = obligationDetail.RevenueTypeId Select rt).FirstOrDefault
                    item.RevenueTypeId = revueneType.Id
                    item.CodeNameRevenueType = revueneType.Code + " - " + revueneType.Name

                    If category.FinancialSourceId IsNot Nothing Then
                        Dim financialSource = (From fs In _context.FinancialSource Where fs.Id = category.FinancialSourceId Select fs).FirstOrDefault
                        item.CodeNameFinancialSource = financialSource.Code + " - " + financialSource.Name
                    End If
                Next
            End If
            result.OriginalValue = (From e In _context.ReimbursementResource.AsNoTracking() Where e.Code = code AndAlso e.BudgetaryValidityId = BudgetaryValidityId Select e).FirstOrDefault()
            Return result
        Else
            Return New ReimbursementResource
        End If
    End Function

    ''' <summary>
    ''' Obtiene un compromiso por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetReimbursementResourceById(id As Integer) As ReimbursementResource Implements IReimbursementResourceRepository.GetReimbursementResourceById
        Dim resul = (From rm In _context.ReimbursementResource.AsNoTracking.Include("ReimbursementResourceDetaill").AsNoTracking Where rm.Id = id Select rm).FirstOrDefault
        If resul IsNot Nothing Then
            Return resul
        Else
            Return New ReimbursementResource
        End If
    End Function

    ''' <summary>
    ''' Metodo para consultar hasta que punto se puede hacer el reintegro
    ''' </summary>
    ''' <param name="paymentOrderId"></param>
    ''' <returns>1 = Obligación, 2 = Compromiso / Reserva, 3 = Presupuesto</returns>
    ''' <remarks></remarks>
    Public Function GetReimbursementUntilByPaymentOrderId(paymentOrderId As Integer) As Integer Implements IReimbursementResourceRepository.GetReimbursementUntilByPaymentOrderId
        Dim resul = (From pmd In _context.PaymentOrderDetail.AsNoTracking Where pmd.PaymentOrderId = paymentOrderId Select pmd).ToList
        For Each item In resul
            Dim obligationDetail = (From od In _context.ObligationDetail.AsNoTracking Where od.Id = item.ObligationDetailId Select od).FirstOrDefault
            If obligationDetail.CommitmentDetailId Is Nothing Then
                Return 1
            End If
            Dim commmitmentDetail = (From cd In _context.CommitmentDetail.AsNoTracking Where cd.Id = obligationDetail.CommitmentDetailId Select cd).FirstOrDefault
            If commmitmentDetail.AvailabilityDetailId Is Nothing Then
                Return 2
            End If
        Next
        Return 3
    End Function

    Public Function SP_SaveReimbursementResource(ReimbursementResourceXml As String, ReimbursementResourceDetailForDeleteXml As String, CodeUser As String) As SP_SaveReimbursementResource_Result Implements IReimbursementResourceRepository.SP_SaveReimbursementResource
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveReimbursementResource(ReimbursementResourceXml, ReimbursementResourceDetailForDeleteXml, CodeUser).SingleOrDefault()
    End Function

#End Region

End Class
