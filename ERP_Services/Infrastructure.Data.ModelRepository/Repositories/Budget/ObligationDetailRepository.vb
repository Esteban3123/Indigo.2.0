'***********************************************************************
' Assembly         : Infrastructure.Data.ModelRepository
' Author           : Carlos Ernesto Cordoba
' Created          : 19-08-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base.Entities

#End Region

Public Class ObligationDetailRepository
    Inherits GenericRepository(Of ObligationDetail)
    Implements IObligationDetailRepository




    'Contexto de Budget
    Private _context As IGlobalModelUnitOfWork

#Region "Buider"
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub
#End Region

   

    ''' <summary>
    ''' obtiene un detalle por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetObligationDetailById(id As Integer) As ObligationDetail Implements IObligationDetailRepository.GetObligationDetailById
        Return (From cd In _context.ObligationDetail Where cd.Id = id Select cd).FirstOrDefault()
    End Function

    ''' <summary>
    ''' obtiene le detalle de la obligacion por id de la cabecera
    ''' </summary>
    ''' <param name="ObligationId"></param>
    ''' <returns></returns>
    Public Function GetObligationDetailByObligationId(ObligationId As Integer) As List(Of ObligationDetail) Implements IObligationDetailRepository.GetObligationDetailByObligationId
        Dim res = (From od In _context.ObligationDetail Where od.ObligationId = ObligationId Select od).ToList()
        For Each item In res
            Dim budget = (From b In _context.Budget.AsNoTracking() Where b.CategoryId = item.CategoryId And b.RevenueTypeId = item.RevenueTypeId Select b).FirstOrDefault()

            If item.CommitmentDetailId IsNot Nothing Then
                Dim commitmentDetail = (From cd In _context.CommitmentDetail.AsNoTracking() Where cd.Id = item.CommitmentDetailId Select cd).FirstOrDefault()
                Dim commitment = (From c In _context.Commitment.AsNoTracking() Where c.Id = commitmentDetail.CommitmentId Select c).FirstOrDefault()
                item.CommitmentCode = commitment.Code
                item.CommitmentDocument = commitment.Document
                item.BalanceCommitment = commitmentDetail.Balance
                item.CommitmentType = commitment.CommitmentType
            Else
                item.Balance = budget.Balance
            End If
            Dim category = (From c In _context.Category.AsNoTracking() Where c.Id = item.CategoryId Select c).FirstOrDefault()
            item.CodeNameBudget = String.Concat(category.Code, " - ", category.Name)
            item.CodeNameFinancialSource = (From fs In _context.FinancialSource.AsNoTracking() Where fs.Id = category.FinancialSourceId Select String.Concat(fs.Code, " - ", fs.Name)).FirstOrDefault()
        Next
        Return res
    End Function
End Class
