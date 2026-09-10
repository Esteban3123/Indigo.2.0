'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/10/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities


Public Interface IBillingGroupAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda o Actualiza un grupo
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveBillingGroup(ByVal BillingGroup As BillingGroup, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of BillingGroup)

    ''' <summary>
    ''' Elimina un grupo
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteBillingGroup(ByVal BillingGroup As BillingGroup, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene un grupo por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetBillingGroup(ByVal code As String, ByVal audit As AuditMessage) As BillingGroup

    ''' <summary>
    ''' Obtiene un grupo uvr por id
    ''' </summary>
    ''' <returns></returns>
    Function GetBillingGroupById(ByVal id As Integer) As BillingGroup

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeStateBillingGroup(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of BillingGroup)
End Interface
