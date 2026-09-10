'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 18-08-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()> _
Public Interface IPayrollAuthorizationConcept

    ''' <summary>
    ''' Lista todos las autorizaciones de concepto
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function ListAllAuthorizationConcept(session As SessionValues) As List(Of AuthorizationConcept)

    ''' <summary>
    ''' Obtiene las autorizaciones de concepto que tenga un grupo
    ''' </summary>
    ''' <param name="groupId">Codigo del Grupo</param>
    ''' <returns>Lista de AuthorizationConcept</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetAuthorizationConceptByGroupId(ByVal groupId As Integer, session As SessionValues) As List(Of AuthorizationConcept)

    ''' <summary>
    ''' Obtiene las autorizaciones de concepto que tenga un empleado
    ''' </summary>
    ''' <param name="employeeId">Codigo del empleado</param>
    ''' <returns>Lista de AuthorizationConcept</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetAuthorizationConceptByEmployeeId(ByVal employeeId As Integer, session As SessionValues) As List(Of AuthorizationConcept)

    ''' <summary>
    ''' Graba o Actualiza una autorizacion de concepto
    ''' </summary>
    ''' <param name="authorizationConcept">authorizationConcept</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function SaveAuthorizationConcept(ByVal authorizationConcept As AuthorizationConcept, session As SessionValues) As Boolean

    ''' <summary>
    ''' Elimina una autorizacion de concepto
    ''' </summary>
    ''' <param name="authorizationConcept">Autorizacion del concepto</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    <OperationContract()> _
    Function DeleteAuthorizationConcept(ByVal authorizationConcept As AuthorizationConcept, session As SessionValues) As ActionMessageResult(Of AuthorizationConcept)

    ''' <summary>
    ''' Graba o Actualiza una lista de autorizacion de concepto
    ''' </summary>
    ''' <param name="authorizationConcept">Lista de autorizacion de concepto</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function SaveListAuthorizationConcept(ByVal authorizationConcept As List(Of AuthorizationConcept), session As SessionValues) As Boolean

End Interface
