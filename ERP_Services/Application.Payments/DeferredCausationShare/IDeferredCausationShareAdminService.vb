'***********************************************************************
' Assembly         : Application.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 02/04/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IDeferredCausationShareAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda o Actualiza una cuota de causacion diferida
    ''' </summary>
    ''' <returns></returns>
    Function SaveDeferredCausationShare(ByVal deferredCausationShare As DeferredCausationShare) As ActionResult(Of DeferredCausationShare)

    ''' <summary>
    ''' Actualiza el valor de las cuotas de causacion diferida
    ''' </summary>
    ''' <param name="ListDeferredCausationShare"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveListDeferredCausationShare(ByVal ListDeferredCausationShare As List(Of DeferredCausationShare), ByVal audit As AuditMessage) As ActionResult(Of List(Of DeferredCausationShare))

    ''' <summary>
    ''' Elimina una cuota de causacion diferida
    ''' </summary>
    ''' <returns></returns>
    Function DeleteDeferredCausationShare(ByVal deferredCausationShare As DeferredCausationShare) As ActionResult

    ''' <summary>
    ''' Obtiene una cuota de la causacion diferida por id
    ''' </summary>
    ''' <returns></returns>
    Function GetDeferredCausationShareById(ByVal id As Integer) As ActionResult(Of DeferredCausationShare)

    ''' <summary>
    ''' Obtiene el listado de las cuotas de la causacion que tiene asociado una cxp
    ''' </summary>
    ''' <param name="accountPayableId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDeferredCausationShareByAccountPayableId(accountPayableId As Integer) As ActionResult(Of List(Of DeferredCausationShare))

End Interface
