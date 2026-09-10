'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Angi Camila Durán Vargas
' Created          : 15-03-2023
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class DiscountTypesRepository
    Inherits GenericRepository(Of DiscountTypes)
    Implements IDiscountTypesRepository

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
    ''' Obtiene un tipo de descuento por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDiscountTypes(code As String) As DiscountTypes Implements IDiscountTypesRepository.GetDiscountTypes
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As DiscountTypes In Me._context.DiscountTypes
                   Where d.Code.Equals(code.Trim())
                   Select d).FirstOrDefault
        If res IsNot Nothing Then

            res.OriginalValue = (From g In _context.DiscountTypes.AsNoTracking
                                 Where g.Code.Equals(code.Trim())
                                 Select g).FirstOrDefault

            Return res
        Else
            Return New DiscountTypes()
        End If
    End Function

    ''' <summary>
    ''' Obtiene una lista de tipo de descuento  por una lista de codigos
    ''' </summary>
    ''' <param name="Listcode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListDiscountTypes(Listcode As List(Of String)) As List(Of DiscountTypes) Implements IDiscountTypesRepository.GetListDiscountTypes
        If Listcode Is Nothing OrElse Listcode.Count = 0 Then
            Throw New ArgumentNullException("code")
        End If

        Dim res = (From d As DiscountTypes In Me._context.DiscountTypes.AsNoTracking()
                   Where Listcode.Contains(d.Code)
                   Select d).ToList()
        If res IsNot Nothing Then

            Return res
        Else
            Return New List(Of DiscountTypes)
        End If
    End Function

    ''' <summary>
    ''' Obtiene un tipo de descuento por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDiscountTypesById(id As Integer) As DiscountTypes Implements IDiscountTypesRepository.GetDiscountTypesById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d In Me._context.DiscountTypes Where d.Id = id Select d).ToList
        If res.Count > 0 Then
            res.SingleOrDefault.OriginalValue = (From d As DiscountTypes In Me._context.DiscountTypes.AsNoTracking() Where d.Id = id Select d).SingleOrDefault()
            Return res.SingleOrDefault
        Else
            Return New DiscountTypes()
        End If
    End Function

End Class
