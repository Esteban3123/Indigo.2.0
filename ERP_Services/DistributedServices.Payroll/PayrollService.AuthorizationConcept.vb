'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Daniel Eduardo Arévalo 
' Created          : 25-04-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.IOC
Imports Application.Payroll
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Partial Class PayrollService

    ''' <summary>
    ''' Elimina una autorizacion de concepto
    ''' </summary>
    ''' <param name="authorizationConcept">Autorizacion del concepto</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    Public Function DeleteAuthorizationConcept(authorizationConcept As AuthorizationConcept, session As SessionValues) As ActionMessageResult(Of AuthorizationConcept) Implements IPayrollAuthorizationConcept.DeleteAuthorizationConcept
        Using authorizationAdmin As IAuthorizationConceptAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IAuthorizationConceptAdminService)()
            Return authorizationAdmin.DeleteAuthorizationConcept(authorizationAdmin, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene las autorizaciones de concepto que tenga un empleado
    ''' </summary>
    ''' <param name="employeeId">Codigo del empleado</param>
    ''' <returns>Lista de AuthorizationConcept</returns>
    ''' <remarks></remarks>
    Public Function GetAuthorizationConceptByEmployeeId(employeeId As Integer, session As SessionValues) As List(Of AuthorizationConcept) Implements IPayrollAuthorizationConcept.GetAuthorizationConceptByEmployeeId
        Using authorizationAdmin As IAuthorizationConceptAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IAuthorizationConceptAdminService)()
            Return authorizationAdmin.GetAuthorizationConceptByEmployeeId(employeeId)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene las autorizaciones de concepto que tenga un grupo
    ''' </summary>
    ''' <param name="groupId">Codigo del Grupo</param>
    ''' <returns>Lista de AuthorizationConcept</returns>
    ''' <remarks></remarks>
    Public Function GetAuthorizationConceptByGroupId(groupId As Integer, session As SessionValues) As List(Of AuthorizationConcept) Implements IPayrollAuthorizationConcept.GetAuthorizationConceptByGroupId
        Using authorizationAdmin As IAuthorizationConceptAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IAuthorizationConceptAdminService)()
            Return authorizationAdmin.GetAuthorizationConceptByGroupId(groupId)
        End Using
    End Function

    ''' <summary>
    ''' Lista todos las autorizaciones de concepto
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllAuthorizationConcept(session As SessionValues) As List(Of AuthorizationConcept) Implements IPayrollAuthorizationConcept.ListAllAuthorizationConcept
        Using authorizationAdmin As IAuthorizationConceptAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IAuthorizationConceptAdminService)()
            Return authorizationAdmin.ListAllAuthorizationConcept()
        End Using
    End Function

    ''' <summary>
    ''' Graba o Actualiza una autorizacion de concepto
    ''' </summary>
    ''' <param name="authorizationConcept">authorizationConcept</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveAuthorizationConcept(authorizationConcept As AuthorizationConcept, session As SessionValues) As Boolean Implements IPayrollAuthorizationConcept.SaveAuthorizationConcept
        Using authorizationAdmin As IAuthorizationConceptAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IAuthorizationConceptAdminService)()
            Return authorizationAdmin.SaveAuthorizationConcept(authorizationConcept, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Graba o Actualiza una lista de autorizacion de concepto
    ''' </summary>
    ''' <param name="authorizationConcept">Lista de autorizacion de concepto</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveListAuthorizationConcept(authorizationConcept As List(Of AuthorizationConcept), session As SessionValues) As Boolean Implements IPayrollAuthorizationConcept.SaveListAuthorizationConcept
        Using authorizationAdmin As IAuthorizationConceptAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IAuthorizationConceptAdminService)()
            Return authorizationAdmin.SaveListAuthorizationConcept(authorizationConcept, session.AuditMessageWcf)
        End Using
    End Function
End Class
