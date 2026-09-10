'***********************************************************************
' Assembly         : Domain.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities

Public Interface IDeferredCausationShareRepository
    Inherits IRepository(Of DeferredCausationShare)

    ''' <summary>
    ''' Obtiene las cuotas de la causacion diferida
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDeferredCausationShareById(id As Integer) As DeferredCausationShare

    ''' <summary>
    ''' Obtiene el listado de cuotas de causacion que tiene asociado la cxp
    ''' </summary>
    ''' <param name="accountPayableId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDeferredCausationShareByAccountPayableId(accountPayableId As Integer) As List(Of DeferredCausationShare)

End Interface
