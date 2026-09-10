'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 26/08/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IBillingItemsRestrictionAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda o Actualiza
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveBillingItemsRestriction(ByVal BillingItemsRestriction As BillingItemsRestriction, ListBillingItemsRestrictionDetail As List(Of BillingItemsRestrictionDetail), ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of BillingItemsRestriction)

    ''' <summary>
    ''' Elimina
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteBillingItemsRestriction(ByVal BillingItemsRestriction As BillingItemsRestriction, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene una entidad por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetBillingItemsRestriction(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of BillingItemsRestriction)

    ''' <summary>
    ''' Obtiene una entidad por id
    ''' </summary>
    ''' <returns></returns>
    Function GetBillingItemsRestrictionById(ByVal id As Integer, ByVal audit As AuditMessage) As ActionResult(Of BillingItemsRestriction)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeStateBillingItemsRestriction(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of BillingItemsRestriction)

    ''' <summary>
    ''' obtiene los detalles por id de cabecera
    ''' </summary>
    ''' <param name="idHeader"></param>
    ''' <returns></returns>
    Function GetItemsRestrictionDetailByIdHeader(idHeader As Integer) As ActionResult(Of List(Of BillingItemsRestrictionDetail))

End Interface
