'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 09-08-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Payroll.Entities
Public Interface IAuthorizationConceptRepository
    Inherits IRepository(Of AuthorizationConcept)

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
    ''' Obtiene las autorizaciones de concepto que tenga un grupo filtradas por ConceptClass
    ''' </summary>
    ''' <param name="groupId">Codigo del Grupo</param>
    ''' <param name="ConceptClass">Clase del concepto a filtrar</param>
    ''' <returns>Lista de AuthorizationConcept filtrada por ConceptClass</returns>
    Function GetAuthorizationConceptByGroupId(ByVal groupId As Integer, ByVal ConceptClass As String) As List(Of AuthorizationConcept)

    ''' <summary>
    ''' Obtiene las autorizaciones de concepto que tenga un empleado
    ''' </summary>
    ''' <param name="employeeId">Codigo del empleado</param>
    ''' <returns>Lista de AuthorizationConcept</returns>
    ''' <remarks></remarks>
    Function GetAuthorizationConceptByEmployeeId(ByVal employeeId As Integer) As List(Of AuthorizationConcept)

End Interface
