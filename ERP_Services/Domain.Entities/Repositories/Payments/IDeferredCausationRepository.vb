'***********************************************************************
' Assembly         : Domain.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities

Public Interface IDeferredCausationRepository
    Inherits IRepository(Of DeferredCausation)

    ''' <summary>
    ''' Obtiene las causaciones diferidas de las facturas
    ''' </summary>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDeferredCausationByIdAccountPayable(idAccountPayable As Integer, Optional tracking As Boolean = True) As List(Of DeferredCausation)

    ''' <summary>
    ''' Obtiene las causaciones diferidas de las facturas
    ''' </summary>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDeferredCausationByAccountPayableCode(code As String) As List(Of DeferredCausation)

End Interface
