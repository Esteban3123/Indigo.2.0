Imports Domain.Common
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Transactions

Public Class DepartmentAdminService
    Implements IDepartmentAdminService

    Private _DepartmentRepository As IDepartmentRepository


    ''' <summary>
    ''' Initializa una nueva instancia de la clase <see cref="DetailedConceptAdminService" />.
    ''' </summary>
    ''' <param name="departmentRepository">el repositorio para el manejo de los Departamentos.</param>
    Public Sub New(ByVal departmentRepository As IDepartmentRepository)
        If departmentRepository Is Nothing Then
            Throw New ArgumentNullException("departmentRepository Vacio")
        End If
        _DepartmentRepository = departmentRepository
    End Sub

    Public Function GetDepartment(code As String, ByVal IdCountry As String) As Domain.Entities.Department Implements IDepartmentAdminService.GetDepartment
        If String.IsNullOrEmpty(code) = True Then
            Throw New ArgumentNullException("code vacío")
        End If
        Try
            Return _DepartmentRepository.GetDepartment(code, IdCountry)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Function GetDepartmentById(ByVal idDepartment As Integer, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Entities.Department Implements IDepartmentAdminService.GetDepartmentById
        If Not (idDepartment > 0) Then
            Throw New ArgumentNullException("Department Id vacío")
        End If
        Try
            Return _DepartmentRepository.GetDepartmentById(idDepartment)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function GetDepartments(ByVal IdCountry As String) As List(Of Domain.Entities.Department) Implements IDepartmentAdminService.GetDepartments
        If String.IsNullOrEmpty(IdCountry) = True Then
            Throw New ArgumentNullException("IdCountry vacío")
        End If
        Try
            Return _DepartmentRepository.GetDepartments(IdCountry)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function ListAllDepartment() As List(Of Domain.Entities.Department) Implements IDepartmentAdminService.ListAllDepartment
        Try
            Return _DepartmentRepository.ListAllDepartment()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function SaveDepartment(department As Domain.Entities.Department, audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionResult(Of Department) Implements IDepartmentAdminService.SaveDepartment
        If department Is Nothing Then
            Throw New ArgumentNullException("department vacío")
        End If
        Dim UnitOfWork As IUnitWork = _DepartmentRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim auditProcess As IndigoAuditSimpleEntity(Of Department)
                Dim auxDepartment As Domain.Entities.Department = Nothing
                Dim status As Integer

                If department.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                    department.ModificationUser = audit.CodeUser
                    department.ModificationDate = Date.Now()
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                    auxDepartment = _DepartmentRepository.GetDepartmentById(department.Id, False)
                Else
                    department.CreationUser = audit.CodeUser
                    department.CreationDate = Date.Now()
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                End If

                'Valido si se va a guardar o a eliminar
                _DepartmentRepository.SaveEntity(department)
                UnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of Department)(department, audit, status, auxDepartment)
                auditProcess.Execute()

                department.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of Department) With {.StatusCode = eStatusResult.SUCCESS, .StateResult = True, .ObjectEmbbeded = department}
            End Using

        Catch ex As Exception
            UnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Department) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Elimina un departamento
    ''' </summary>
    ''' <param name="department"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteDepartment(department As Domain.Entities.Department, audit As AuditMessage) As ActionMessageResult(Of Domain.Entities.Department) Implements IDepartmentAdminService.DeleteDepartment
        Dim result As New ActionMessageResult(Of Department)
        result.StateResult = True
        If _DepartmentRepository Is Nothing Then
            Throw New ArgumentNullException("departmentRepository vacío")
        End If
        Dim UnitOfWork As IUnitWork = _DepartmentRepository.UnitWork
        Try
            ' elimino el Departamento
            _DepartmentRepository.DeleteEntity(department)
            UnitOfWork.Commit()
            '/***** Auditoria Basica ********/
            IndigoAuditBasic.Execute(department.GetType.Name, audit.Functional, department.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
            '/*****Auditoria Avanzada ******/
            Dim auditObject As New IndigoAuditSimpleEntity(Of Domain.Entities.Department)(department, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            auditObject.Execute()
            Return result
        Catch ex As DbUpdateException
            UnitOfWork.RollbackChanges()
            result.StateResult = False
            result.MessageResult.Add(New MessageResult("c-0000", department.Code))
            Return result
        Catch ex As Exception
            UnitOfWork.RollbackChanges()
            result.StateResult = False
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return result
        End Try
    End Function

    ''' <summary>
    ''' Metodo que cambia el estado del registro
    ''' </summary>
    ''' <param name="code">Codigo Ciudad</param>
    ''' <returns>booleano</returns>
    ''' <remarks></remarks>
    Public Function ChangeStateDepartment(code As String, ByVal IdCountry As String, state As Boolean, audit As AuditMessage) As ActionResult(Of Department) Implements IDepartmentAdminService.ChangeStateDepartment
        Dim department As Department = _DepartmentRepository.GetDepartment(code, IdCountry)
        department.State = state
        Return SaveDepartment(department, audit)
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _DepartmentRepository = Nothing
            IndigoGC.Execute()
        End If
        disposedValue = True
    End Sub

    ' Visual Basic agrega este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
