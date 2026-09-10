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

Public Interface IDeferredCausationAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda o Actualiza una causacion diferida
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveDeferredCausation(ByVal deferredCausation As DeferredCausation, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of DeferredCausation)

    ''' <summary>
    ''' Elimina una causacion diferida
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteDeferredCausation(ByVal deferredCausation As DeferredCausation, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene un listado de causaciones diferidas
    ''' </summary>
    ''' <returns></returns>
    Function GetDeferredCausationByIdAccountPayable(ByVal idAccountPayable As Integer, ByVal audit As AuditMessage) As List(Of DeferredCausation)

    ''' <summary>
    ''' Obtiene un listado de causaciones diferidas por el codigo de la cuenta por pagar
    ''' </summary>
    ''' <returns></returns>
    Function GetDeferredCausationByAccountPayableCode(Code As String, audit As AuditMessage) As List(Of Domain.Entities.DeferredCausation)

End Interface
