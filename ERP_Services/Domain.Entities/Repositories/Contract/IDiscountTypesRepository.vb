'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Angi Camila Duran Vargas
' Created          : 15/03/2023
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base

Public Interface IDiscountTypesRepository
    Inherits IRepository(Of DiscountTypes)

    ''' <summary>
    ''' Obtiene el registro por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDiscountTypes(code As String) As DiscountTypes

    ''' <summary>
    ''' Obtiene el registro por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDiscountTypesById(id As Integer) As DiscountTypes

    ''' <summary>
    ''' Obtiene una lista de tipos de descuento por codigos
    ''' </summary>
    ''' <param name="ListCode"></param>
    ''' <returns></returns>
    Function GetListDiscountTypes(ListCode As List(Of String)) As List(Of DiscountTypes)


End Interface
