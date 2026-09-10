'***********************************************************************
' Assembly         : Domain.Budget
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 09-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface IEarningsTypeRepository
    Inherits IRepository(Of RevenueType)

    ''' <summary>
    ''' Obtiene un tipo de ingreso
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetEarningsType(code As String, validityId As Integer, type As Integer, Optional tracking As Boolean = True) As RevenueType

    ''' <summary>
    ''' Obtiene un tipo de ingreso by validity
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetEarningsTypeByValidity(code As String, ValidityId As String, Optional tracking As Boolean = True) As RevenueType

    ''' <summary>
    ''' Lista los tipos de ingreso por vigencia
    ''' </summary>
    ''' <param name="ValidityId">The validity identifier.</param>
    ''' <returns></returns>
    Function ListEarningsTypeByValidity(ValidityId As Integer) As List(Of RevenueType)

    ''' <summary>
    ''' Obtiene todas los tipos de ingreso=1 o tipos de gasto=2 para copiarlos y agregarlos a otra vigencia
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetListRevenueTypeForCopyBase(validityId As Integer, type As Integer) As List(Of RevenueType)

    ''' <summary>
    ''' Obtiene el tipo de ingreso=1 o tipo de gasto=2 por codigo, vigencia y tipo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="validityId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetRevenueTypeByCodeAndValidityForCopyBase(code As String, validityId As Integer, type As Integer) As RevenueType

End Interface
