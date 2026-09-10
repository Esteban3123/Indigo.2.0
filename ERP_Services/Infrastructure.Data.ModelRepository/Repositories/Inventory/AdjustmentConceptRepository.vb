'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 16/09/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class AdjustmentConceptRepository
    Inherits GenericRepository(Of AdjustmentConcept)
    Implements IAdjustmentConceptRepository

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
    ''' Obtiene un concepto de ajuste por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAdjustmentConcept(code As String) As AdjustmentConcept Implements IAdjustmentConceptRepository.GetAdjustmentConcept
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As AdjustmentConcept In Me._context.AdjustmentConcept.Include("AdjustmentConceptUser")
                   Where d.Code.Equals(code.Trim())
                   Select d).FirstOrDefault
        If res IsNot Nothing Then

            Dim account = (From ac In _context.MainAccounts.AsNoTracking Where ac.Id = res.AdjustmentAccountId Select ac).FirstOrDefault
            res.MainAccountAdjustmentDescription = account.Number + " - " + account.Name

            If res.IvaAccountId IsNot Nothing Then
                account = (From ai In _context.MainAccounts.AsNoTracking Where ai.Id = res.IvaAccountId Select ai).FirstOrDefault
                res.MainAccountIvaDescription = account.Number + " - " + account.Name
            Else
                res.MainAccountIvaDescription = String.Empty
            End If

            If res.CostCenterId IsNot Nothing Then
                Dim costcenter = (From cc In _context.CostCenter.AsNoTracking Where cc.Id = res.CostCenterId Select cc).FirstOrDefault
                res.CostCenterDescription = costcenter.Code + " - " + costcenter.Name
                res.CostCenterCodeName = costcenter.Code + " - " + costcenter.Name
            Else
                res.CostCenterDescription = String.Empty
                res.CostCenterCodeName = String.Empty
            End If

            res.OriginalValue = (From g In _context.AdjustmentConcept.AsNoTracking
                                 Where g.Code.Equals(code.Trim())
                                 Select g).FirstOrDefault

            Return res
        Else
            Return New AdjustmentConcept()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un concepto de ajuste por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAdjustmentConceptById(id As Integer) As AdjustmentConcept Implements IAdjustmentConceptRepository.GetAdjustmentConceptById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d In Me._context.AdjustmentConcept.Include("MainAccounts") Where d.Id = id Select d).FirstOrDefault()
        If res IsNot Nothing Then
            Dim cc = (From c In Me._context.CostCenter.AsNoTracking() Where res.CostCenterId = c.Id Select c).FirstOrDefault()
            If cc IsNot Nothing Then
                res.CostCenterCodeName = cc.Code + " - " + cc.Name
            Else
                res.CostCenterCodeName = String.Empty 
            End If

            res.OriginalValue = (From d As AdjustmentConcept In Me._context.AdjustmentConcept.AsNoTracking() Where d.Id = id Select d).FirstOrDefault()
            Return res
        Else
            Return New AdjustmentConcept()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un concepto de ajuste por cuenta contable y centro de costo.
    ''' </summary>
    ''' <param name="conceptType"></param>
    ''' <param name="adjustmentAccountId"></param>
    ''' <param name="costCenterId"></param>
    Public Function GetListByAdjustmentAccountCostCenterId(conceptType As Byte, adjustmentAccountId As Integer, costCenterId As Integer) As List(Of AdjustmentConcept) Implements IAdjustmentConceptRepository.GetListByAdjustmentAccountCostCenterId
        If conceptType = 0 Then
            Throw New ArgumentNullException("conceptType")
        End If
        If adjustmentAccountId = 0 Then
            Throw New ArgumentNullException("adjustmentAccountId")
        End If
        If costCenterId = 0 Then
            Throw New ArgumentNullException("costCenterId")
        End If
        Dim res = (From d As AdjustmentConcept In Me._context.AdjustmentConcept.AsNoTracking().Include("AdjustmentConceptUser")
                   Where d.Status = True And d.ConceptType = conceptType And d.AdjustmentAccountId = adjustmentAccountId And d.CostCenterId = costCenterId
                   Select d).ToList()

        For Each resItem As AdjustmentConcept In res
            Dim cc = (From c In Me._context.CostCenter.AsNoTracking() Where resItem.CostCenterId = c.Id Select c).FirstOrDefault()
            If cc IsNot Nothing Then
                resItem.CostCenterCodeName = cc.Code + " - " + cc.Name
            Else
                resItem.CostCenterCodeName = String.Empty
            End If

            resItem.OriginalValue = (From d As AdjustmentConcept In Me._context.AdjustmentConcept.AsNoTracking() Where d.Status = 1 AndAlso d.ConceptType = conceptType AndAlso d.AdjustmentAccountId = adjustmentAccountId AndAlso d.CostCenterId = costCenterId Select d).FirstOrDefault()
        Next

        Return res

    End Function

End Class
