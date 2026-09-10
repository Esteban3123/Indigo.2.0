'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Cristhian Mauricio Salazar Narvaez
' Created          : 09-08-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IAuthorizationConceptAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista todos las autorizaciones de concepto
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllAuthorizationConcept() As List(Of AuthorizationConcept)

    ''' <summary>
    ''' Obtiene las autorizaciones de concepto que tenga un grupo
    ''' </summary>
    ''' <param name="groupId">Codigo del Grupo</param>
    ''' <returns>Lista de AuthorizationConcept</returns>
    ''' <remarks></remarks>
    Function GetAuthorizationConceptByGroupId(ByVal groupId As Integer) As List(Of AuthorizationConcept)

    ''' <summary>
    ''' Obtiene las autorizaciones de concepto que tenga un empleado
    ''' </summary>
    ''' <param name="employeeId">Codigo del empleado</param>
    ''' <returns>Lista de AuthorizationConcept</returns>
    ''' <remarks></remarks>
    Function GetAuthorizationConceptByEmployeeId(ByVal employeeId As Integer) As List(Of AuthorizationConcept)

    ''' <summary>
    ''' Graba o Actualiza una autorizacion de concepto
    ''' </summary>
    ''' <param name="authorizationConcept">authorizationConcept</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function SaveAuthorizationConcept(ByVal authorizationConcept As AuthorizationConcept, ByVal audit As AuditMessage) As Boolean

    ''' <summary>
    ''' Elimina una autorizacion de concepto
    ''' </summary>
    ''' <param name="authorizationConcept">Autorizacion del concepto</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    Function DeleteAuthorizationConcept(ByVal authorizationConcept As AuthorizationConcept, ByVal audit As AuditMessage) As ActionMessageResult(Of AuthorizationConcept)

    ''' <summary>
    ''' Graba o Actualiza una lista de autorizacion de concepto
    ''' </summary>
    ''' <param name="authorizationConcept">Lista de autorizacion de concepto</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function SaveListAuthorizationConcept(ByVal authorizationConcept As List(Of AuthorizationConcept), ByVal audit As AuditMessage) As Boolean

End Interface
