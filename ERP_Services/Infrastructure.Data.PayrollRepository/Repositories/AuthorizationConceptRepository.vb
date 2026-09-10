'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Cristhian Mauricio Salazar
' Created          : 09-08-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Payroll.Entities
Imports Domain.Payroll
Public Class AuthorizationConceptRepository
    Inherits GenericRepository(Of AuthorizationConcept)
    Implements IAuthorizationConceptRepository

    ''' <summary>
    ''' Contexto de payrrol
    ''' </summary>
    Private _context As IPayrollUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de payrrol
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IPayrollUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene las autorizaciones de concepto que tenga un empleado
    ''' </summary>
    ''' <param name="employeeId">Codigo del empleado</param>
    ''' <returns>Lista de AuthorizationConcept</returns>
    ''' <remarks></remarks>
    Public Function GetAuthorizationConceptByEmployeeId(employeeId As Integer) As List(Of AuthorizationConcept) Implements IAuthorizationConceptRepository.GetAuthorizationConceptByEmployeeId
        Dim authoConcept = From e In _context.AuthorizationConcept.AsNoTracking.Include("AuthorizationConceptGroup").AsNoTracking.Include("AuthorizationConceptEmployee").AsNoTracking.Include("Concept").AsNoTracking.Include("Concept.ConceptGroup").AsNoTracking
                           Where e.AuthorizationConceptEmployee.Any(Function(x) x.EmployeeId = employeeId)
                           Select e
        Return authoConcept.ToList()
    End Function

    ''' <summary>
    ''' Obtiene las autorizaciones de concepto que tenga un grupo
    ''' </summary>
    ''' <param name="groupId">Codigo del Grupo</param>
    ''' <returns>Lista de AuthorizationConcept</returns>
    ''' <remarks></remarks>
    Public Function GetAuthorizationConceptByGroupId(groupId As Integer) As List(Of AuthorizationConcept) Implements IAuthorizationConceptRepository.GetAuthorizationConceptByGroupId
        Dim authoConcept = (From e In _context.AuthorizationConcept.Include("AuthorizationConceptGroup") _
                            .Include("AuthorizationConceptEmployee").Include("Concept").Include("Concept.ConceptGroup")
                            Where e.AuthorizationConceptGroup.Any(Function(x) x.GroupId = groupId) Order By e.Concept.ConceptType
                            Select e).ToList()

        'Dim listEmployee = (From j In _context.Contract Where j.GroupId = groupId Select j.EmployeeId).AsEnumerable()

        ''Dim authoEmployee = (From e In _context.AuthorizationConcept.Include("AuthorizationConceptEmployee").Include("Concept").Include("Concept.ConceptGroup")
        ''                     Where e.AuthorizationConceptEmployee.Any(Function(x) listEmployee.Exists(Function(y) y.EmployeeId = x.EmployeeId))
        ''                     Order By e.Concept.Code
        ''                     Select e).ToList()
        'Dim xxxy = (From r In From e In _context.AuthorizationConceptEmployee.Include("AuthorizationConcept").Include("AuthorizationConcept.Concept").Include("AuthorizationConcept.Concept.ConceptGroup")
        '                      Where listEmployee.Contains(e.EmployeeId)
        '                      Select e).ToList()

        'Dim fff = (From t In xxxy Select t.AuthorizationConcept).ToList


        'Dim authoTodo = authoConcept.Union(fff).Distinct()

        Return authoConcept.ToList()
    End Function

    ''' <summary>
    ''' Lista todos las autorizaciones de concepto
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllAuthorizationConcept() As List(Of AuthorizationConcept) Implements IAuthorizationConceptRepository.ListAllAuthorizationConcept
        Dim authoConcept = From e In _context.AuthorizationConcept.Include("AuthorizationConceptGroup") _
                            .Include("AuthorizationConceptEmployee").Include("Concept")
                           Select e
        Return authoConcept.ToList()
    End Function

End Class
