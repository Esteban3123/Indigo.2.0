'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 26/08/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class BillingItemsRestrictionRepository
    Inherits GenericRepository(Of BillingItemsRestriction)
    Implements IBillingItemsRestrictionRepository

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
    ''' Obtiene una definicion de tarifa por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBillingItemsRestriction(code As String) As BillingItemsRestriction Implements IBillingItemsRestrictionRepository.GetBillingItemsRestriction
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As BillingItemsRestriction In Me._context.BillingItemsRestriction
                   Where d.Code.Equals(code.Trim())
                   Select d).FirstOrDefault

        If res Is Nothing Then
            Return New BillingItemsRestriction()
        Else
            Return res
        End If
    End Function

    ''' <summary>
    ''' Obtiene una definicion de tarifa por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBillingItemsRestrictionById(id As Integer) As BillingItemsRestriction Implements IBillingItemsRestrictionRepository.GetBillingItemsRestrictionById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d In Me._context.BillingItemsRestriction Where d.Id = id Select d).ToList
        If res.Count > 0 Then
            res.SingleOrDefault.OriginalValue = (From d As BillingItemsRestriction In Me._context.BillingItemsRestriction.AsNoTracking() Where d.Id = id Select d).SingleOrDefault()
            Return res.SingleOrDefault
        Else
            Return New BillingItemsRestriction()
        End If
    End Function

End Class
