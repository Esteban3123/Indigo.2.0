'***********************************************************************
' Assembly         : Infrastructure.Data.BudgetRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 29/01/2018
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base
Imports System.Linq.Expressions

Public Class PrivateBudgetItemsStructureRepository
    Inherits GenericRepository(Of PrivateBudgetItemsStructure)
    Implements IPrivateBudgetItemsStructureRepository

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

    ''' <summary>
    ''' Obtiene el registro por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    Public Function GetPrivateBudgetItemsStructure(code As String, Optional tracking As Boolean = True) As Domain.Entities.PrivateBudgetItemsStructure Implements IPrivateBudgetItemsStructureRepository.GetPrivateBudgetItemsStructure
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As PrivateBudgetItemsStructure In Me._context.PrivateBudgetItemsStructure.Include("PrivateBudgetItemsStructureDetail") Where d.Code.Equals(code.Trim()) Select d).FirstOrDefault
        If res IsNot Nothing Then

            If res.ParentId IsNot Nothing Then
                res.PrivateBudgetItemsStructureDescription = (From p In _context.PrivateBudgetItemsStructure.AsNoTracking Where p.Id = res.ParentId Select String.Concat(p.Code, " - ", p.Description)).FirstOrDefault
            End If

            res.CountChild = (From p In _context.PrivateBudgetItemsStructure.AsNoTracking Where p.ParentId = res.Id Select p).Count()

            If res.PrivateBudgetItemsStructureDetail IsNot Nothing AndAlso res.PrivateBudgetItemsStructureDetail.Count > 0 Then
                For Each item In res.PrivateBudgetItemsStructureDetail
                    item.MainAccountDescription = (From x In _context.MainAccounts.AsNoTracking Where x.Id = item.MainAccountId Select String.Concat(x.Number, " - ", x.Name)).FirstOrDefault()

                    If item.ThirdPartyId IsNot Nothing Then
                        item.ThirdPartyDescription = (From x In _context.ThirdParty.AsNoTracking Where x.Id = item.ThirdPartyId Select String.Concat(x.Nit, " - ", x.Name)).FirstOrDefault()
                    End If

                    If item.CostCenterId IsNot Nothing Then
                        item.CostCenterDescription = (From x In _context.CostCenter.AsNoTracking Where x.Id = item.CostCenterId Select String.Concat(x.Code, " - ", x.Name)).FirstOrDefault()
                    End If
                Next
            End If

            res.OriginalValue = (From d As PrivateBudgetItemsStructure In Me._context.PrivateBudgetItemsStructure.AsNoTracking() Where d.Code.Equals(code.Trim()) Select d).FirstOrDefault()
            Return res
        Else
            Return New PrivateBudgetItemsStructure()
        End If
    End Function

    ''' <summary>
    ''' Obtiene el registro por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    Public Function GetPrivateBudgetItemsStructureById(id As String, Optional tracking As Boolean = True) As Domain.Entities.PrivateBudgetItemsStructure Implements IPrivateBudgetItemsStructureRepository.GetPrivateBudgetItemsStructureById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d In Me._context.PrivateBudgetItemsStructure Where d.Id = id Select d).FirstOrDefault()
        If res IsNot Nothing Then
            res.OriginalValue = (From d As PrivateBudgetItemsStructure In Me._context.PrivateBudgetItemsStructure.AsNoTracking() Where d.Id = id Select d).FirstOrDefault()
            Return res
        Else
            Return New PrivateBudgetItemsStructure()
        End If
    End Function

End Class
