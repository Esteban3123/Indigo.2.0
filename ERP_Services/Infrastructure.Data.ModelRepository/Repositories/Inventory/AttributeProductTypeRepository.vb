'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class AttributeProductTypeRepository
    Inherits GenericRepository(Of AttributeProductType)
    Implements IAttributeProductTypeRepository

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
    ''' Obtiene un atributo por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAttributeProductType(code As String) As AttributeProductType Implements IAttributeProductTypeRepository.GetAttributeProductType
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As AttributeProductType In Me._context.AttributeProductType.Include("AttributeProductTypeOptionList")
                   Where d.Code.Equals(code.Trim())
                   Select d).FirstOrDefault
        If res IsNot Nothing Then

            Dim productType = (From pt In _context.ProductType.AsNoTracking Where pt.Id = res.ProductTypeId Select pt).FirstOrDefault
            res.ProductTypeDescription = productType.Code + " - " + productType.Name

            res.OriginalValue = (From g In _context.AttributeProductType.AsNoTracking.Include("AttributeProductTypeOptionList").AsNoTracking
                                  Where g.Code.Equals(code.Trim())
                                  Select g).FirstOrDefault

            Return res
        Else
            Return New AttributeProductType()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un atributo por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAttributeProductTypeById(id As Integer) As AttributeProductType Implements IAttributeProductTypeRepository.GetAttributeProductTypeById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d In Me._context.AttributeProductType Where d.Id = id Select d).ToList
        If res.Count > 0 Then
            res.SingleOrDefault.OriginalValue = (From d As AttributeProductType In Me._context.AttributeProductType.AsNoTracking() Where d.Id = id Select d).SingleOrDefault()
            Return res.SingleOrDefault
        Else
            Return New AttributeProductType()
        End If
    End Function
End Class
