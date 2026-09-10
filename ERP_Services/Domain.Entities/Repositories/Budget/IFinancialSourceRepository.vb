'***********************************************************************
' Assembly         : Domain.Budget
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface IFinancialSourceRepository
    Inherits IRepository(Of FinancialSource)

    ''' <summary>
    ''' Obtiene una fuente de financiacion por su código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFinancialSource(code As String, validityId As Integer, Optional tracking As Boolean = True) As FinancialSource

    ''' <summary>
    ''' Obtiene todos los recursos para copiarlos y agregarlos a otra vigencia
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetListFinancialSourceForCopyBase(validityId As Integer) As List(Of FinancialSource)

    ''' <summary>
    ''' Obtiene la fuente de financiacio por codigo y la vigencia
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="validityId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFinancialSourceByCodeAndValidityForCopyBase(code As String, validityId As Integer) As FinancialSource

End Interface
