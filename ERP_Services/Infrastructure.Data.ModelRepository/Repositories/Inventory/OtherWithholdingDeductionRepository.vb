'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class OtherWithholdingDeductionRepository
    Inherits GenericRepository(Of OtherWithholdingDeduction)
    Implements IOtherWithholdingDeductionRepository

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
    ''' Obtiene otras deducciones y retenciones por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetOtherWithholdingDeduction(code As String) As OtherWithholdingDeduction Implements IOtherWithholdingDeductionRepository.GetOtherWithholdingDeduction
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As OtherWithholdingDeduction In Me._context.OtherWithholdingDeduction Where d.Code.Equals(code.Trim()) Select d).FirstOrDefault

        If res IsNot Nothing Then
            Dim account = (From ap In _context.AccountPayableConcepts.AsNoTracking Where ap.Id = res.AccountPayableConceptId Select ap).FirstOrDefault
            res.AccountPayableConceptDescription = account.Code + " - " + account.Name

            res.OriginalValue = (From g In _context.OtherWithholdingDeduction.AsNoTracking
                                  Where g.Code.Equals(code.Trim())
                                  Select g).FirstOrDefault

            Return res
        Else
            Return New OtherWithholdingDeduction()
        End If
        Return New OtherWithholdingDeduction()
    End Function

    ''' <summary>
    ''' Obtiene otras deducciones y retenciones por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetOtherWithholdingDeductionById(id As Integer) As OtherWithholdingDeduction Implements IOtherWithholdingDeductionRepository.GetOtherWithholdingDeductionById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d In Me._context.OtherWithholdingDeduction Where d.Id = id Select d).ToList
        If res.Count > 0 Then
            res.SingleOrDefault.OriginalValue = (From d As OtherWithholdingDeduction In Me._context.OtherWithholdingDeduction.AsNoTracking Where d.Id = id Select d).SingleOrDefault()
            Return res.SingleOrDefault
        Else
            Return New OtherWithholdingDeduction()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un listado de otras retenciones y deducciones
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListOtherWithholdingDeduction() As List(Of OtherWithholdingDeduction) Implements IOtherWithholdingDeductionRepository.ListOtherWithholdingDeduction
        Dim res = (From d In Me._context.OtherWithholdingDeduction.AsNoTracking() Select d).ToList()
        If res.Count > 0 Then
            For Each item In res
                Dim accpay = (From apc In Me._context.AccountPayableConcepts.Include("MainAccounts").Include("RetentionConcepts").AsNoTracking() Where apc.Id = item.AccountPayableConceptId Select apc).FirstOrDefault()
                item.AccountPayableConcepts = accpay
                If item.Type = 1 And accpay.RetentionConcepts IsNot Nothing Then
                    item.CodeNameConcepts = accpay.RetentionConcepts.Code + " - " + accpay.RetentionConcepts.Name
                    item.RetentionPercentage = accpay.RetentionConcepts.Rate
                Else
                    item.CodeNameConcepts = accpay.Code + " - " + accpay.Name
                End If
            Next
        End If
        Return res
    End Function
End Class
