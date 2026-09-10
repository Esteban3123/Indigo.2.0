'***********************************************************************
' Assembly         : Infrastructure.Data.BudgetRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/09/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure

Public Class ObligationModificationRepository
    Inherits GenericRepository(Of ObligationModification)
    Implements IObligationModificationRepository

    'Contexto 
    Private _context As IGlobalModelUnitOfWork

#Region "Builder"
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene una modificacion de obligacion por codigo
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetObligationModification(Code As String, validityId As Integer) As ObligationModification Implements IObligationModificationRepository.GetObligationModification
        If Code Is Nothing OrElse Code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As ObligationModification In Me._context.ObligationModification.Include("ObligationModificationDetail") Where d.Code.Equals(Code.Trim()) And d.BudgetaryValidityId = validityId Select d).FirstOrDefault
        If res IsNot Nothing Then
            'Se agrega la descripcion de la obligacion
            Dim obligation = (From o In _context.Obligation.AsNoTracking Where o.Id = res.ObligationId Select o).FirstOrDefault
            res.CodeObligation = obligation.Code
            res.ObligationType = obligation.ObligationType

            'Se recorre los detalles para agregar los campos que se muestran en la rejilla
            If res.ObligationModificationDetail IsNot Nothing AndAlso res.ObligationModificationDetail.Count > 0 Then
                For Each itemDetail As ObligationModificationDetail In res.ObligationModificationDetail
                    Dim obligationDetail = (From x In _context.ObligationDetail.AsNoTracking.Include("Category").AsNoTracking.Include("Category.FinancialSource").AsNoTracking.Include("RevenueType").AsNoTracking.Include("CommitmentDetail").AsNoTracking
                                            Where x.Id = itemDetail.ObligationDetailId
                                            Select x).FirstOrDefault
                    itemDetail.ExpiredDate = obligationDetail.ExpiredDate
                    itemDetail.BalanceObligation = obligationDetail.Balance
                    itemDetail.CategoryId = obligationDetail.Category.Id
                    itemDetail.CodeNameCategory = obligationDetail.Category.Code + " - " + obligationDetail.Category.Name
                    itemDetail.RevenueTypeId = obligationDetail.RevenueType.Id
                    itemDetail.CodeNameRevenueType = obligationDetail.RevenueType.Code + " - " + obligationDetail.RevenueType.Name
                    itemDetail.CodeNameFinancialSource = obligationDetail.Category.FinancialSource.Code + " - " + obligationDetail.Category.FinancialSource.Name
                    If obligationDetail.CommitmentDetail IsNot Nothing Then
                        itemDetail.CommitmentDetailId = obligationDetail.CommitmentDetail.Id
                        itemDetail.BalanceCommitment = obligationDetail.CommitmentDetail.Balance
                    End If
                Next
            End If

            res.OriginalValue = (From d As ObligationModification In Me._context.ObligationModification.AsNoTracking() Where d.Code.Equals(Code.Trim()) And d.BudgetaryValidityId = validityId Select d).FirstOrDefault
            Return res
        Else
            Return New ObligationModification()
        End If
    End Function

    ''' <summary>
    ''' Obtiene una modificacion de obligacion por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetObligationModificationById(Id As Integer) As ObligationModification Implements IObligationModificationRepository.GetObligationModificationById
        Dim res = (From d In Me._context.ObligationModification.Include("ObligationModificationDetail") Where d.Id = Id Select d).FirstOrDefault
        If res IsNot Nothing Then
            Dim obligation = (From o In _context.Obligation.AsNoTracking Where o.Id = res.ObligationId Select o).FirstOrDefault
            res.CodeObligation = obligation.Code
            res.ObligationType = obligation.ObligationType

            'Se recorre los detalles para agregar los campos que se muestran en la rejilla
            If res.ObligationModificationDetail IsNot Nothing AndAlso res.ObligationModificationDetail.Count > 0 Then
                For Each itemDetail As ObligationModificationDetail In res.ObligationModificationDetail
                    Dim obligationDetail = (From x In _context.ObligationDetail.AsNoTracking.Include("Category").AsNoTracking.Include("Category.FinancialSource").AsNoTracking.Include("RevenueType").AsNoTracking.Include("CommitmentDetail").AsNoTracking
                                            Where x.Id = itemDetail.ObligationDetailId
                                            Select x).FirstOrDefault
                    itemDetail.ExpiredDate = obligationDetail.ExpiredDate
                    itemDetail.BalanceObligation = obligationDetail.Balance
                    itemDetail.CategoryId = obligationDetail.Category.Id
                    itemDetail.CodeNameCategory = obligationDetail.Category.Code + " - " + obligationDetail.Category.Name
                    itemDetail.RevenueTypeId = obligationDetail.RevenueType.Id
                    itemDetail.CodeNameRevenueType = obligationDetail.RevenueType.Code + " - " + obligationDetail.RevenueType.Name
                    itemDetail.CodeNameFinancialSource = obligationDetail.Category.FinancialSource.Code + " - " + obligationDetail.Category.FinancialSource.Name
                    If obligationDetail.CommitmentDetail IsNot Nothing Then
                        itemDetail.CommitmentDetailId = obligationDetail.CommitmentDetail.Id
                        itemDetail.BalanceCommitment = obligationDetail.CommitmentDetail.Balance
                    End If
                Next
            End If
            res.OriginalValue = (From d As ObligationModification In Me._context.ObligationModification.AsNoTracking() Where d.Id = Id Select d).FirstOrDefault
            Return res
        Else
            Return New ObligationModification
        End If
    End Function

    Public Function SP_SaveObligationModification(ObligationModificationXml As String, ObligationModificationDetailForDeleteXml As String, CodeUser As String) As SP_SaveObligationModification_Result Implements IObligationModificationRepository.SP_SaveObligationModification
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveObligationModification(ObligationModificationXml, ObligationModificationDetailForDeleteXml, CodeUser).SingleOrDefault()
    End Function

#End Region

End Class
