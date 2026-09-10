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


Public Class AnnualizedCashFlowModificationRepository
    Inherits GenericRepository(Of AnnualizedCashFlowModification)
    Implements IAnnualizedCashFlowModificationRepository



    'Contexto de Budget
    Private _context As IGlobalModelUnitOfWork

#Region "Buider"
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub
#End Region

    ''' <summary>
    ''' obtiene una modificacion del pac por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetAnnualizedCashFlowModificationByCode(code As String, type As Integer) As AnnualizedCashFlowModification Implements IAnnualizedCashFlowModificationRepository.GetAnnualizedCashFlowModificationByCode
        Dim res = (From acfm In _context.AnnualizedCashFlowModification Where acfm.Code = code And acfm.DocumentSource = type Select acfm).FirstOrDefault()
        If res IsNot Nothing Then
            Dim validaty = (From v In _context.BudgetaryValidity.AsNoTracking() Where v.Id = res.BudgetaryValidityId Select v).FirstOrDefault()
            res.BudgetEntityId = validaty.BudgetaryEntityId
            res.OriginalValue = (From acfm In _context.AnnualizedCashFlowModification.AsNoTracking() Where acfm.Code = code And acfm.DocumentSource = type Select acfm).FirstOrDefault()
            Return res
        End If
        Return New AnnualizedCashFlowModification
    End Function

    ''' <summary>
    ''' obtiene una modificacion del pac por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetAnnualizedCashFlowModificationById(id As Integer) As AnnualizedCashFlowModification Implements IAnnualizedCashFlowModificationRepository.GetAnnualizedCashFlowModificationById
        Dim res = (From acfm In _context.AnnualizedCashFlowModification.Include("AnnualizedCashFlowModificationDetail") Where acfm.Id = id Select acfm).FirstOrDefault()
        If res IsNot Nothing Then
            Dim validaty = (From v In _context.BudgetaryValidity.AsNoTracking() Where v.Id = res.BudgetaryValidityId Select v).FirstOrDefault()
            res.BudgetEntityId = validaty.BudgetaryEntityId
            res.OriginalValue = (From acfm In _context.AnnualizedCashFlowModification.AsNoTracking() Where acfm.Id = id Select acfm).FirstOrDefault()
            Return res
        End If
        Return New AnnualizedCashFlowModification
    End Function
End Class
