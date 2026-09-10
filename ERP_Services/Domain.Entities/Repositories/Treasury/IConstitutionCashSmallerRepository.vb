'***********************************************************************
' Assembly         : Domain.Treasury
' Author           : Carlos Mario Arias Rubiano
' Created          : 17/01/2017
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface IConstitutionCashSmallerRepository
    Inherits IRepository(Of ConstitutionCashSmaller)

    ''' <summary>
    ''' Obtiene un registro de cambio de cheque por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetConstitutionCashSmaller(ByVal code As String) As ConstitutionCashSmaller

    ''' <summary>
    ''' Gets the cashing by identifier.
    ''' </summary>
    ''' Obtiene un registro de cambio de cheque por id
    ''' <returns></returns>
    Function GetConstitutionCashSmallerById(id As Integer, tracking As Boolean) As ConstitutionCashSmaller

    ''' <summary>
    ''' Realiza el proceso de fondo de caja menor
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_SaveConstitutionCashSmaller(xmlObject As String, codeUser As String) As SP_SaveConstitutionCashSmaller_Result

End Interface