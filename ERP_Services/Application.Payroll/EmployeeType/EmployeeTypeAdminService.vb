'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Cristhian Mauricio Salazar Narvaez
' Created          : 08-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll
Imports Domain.Payroll.Entities
Imports Domain.Base
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports System.Data.Entity.Infrastructure

Public Class EmployeeTypeAdminService
    Implements IEmployeeTypeAdminService

    'Repositorio de empleados
    Private _EmployeeTypeRepository As IEmployeeTypeRepository

    ''' <summary>
    ''' inicia el repositorio de empleados
    ''' </summary>
    ''' <param name="employeeRepository">Repositorio de empleados</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal employeeRepository As IEmployeeTypeRepository)
        If (employeeRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de empleados vacio")
        End If
        _EmployeeTypeRepository = employeeRepository
    End Sub

    ''' <summary>
    ''' Elimina un tipo de empleado
    ''' </summary>
    ''' <param name="employeeType">Tipo de empleado</param>
    ''' <returns></returns>
    Public Function DeleteEmployeeType(employeeType As Domain.Payroll.Entities.EmployeeType, audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionMessageResult(Of Domain.Payroll.Entities.EmployeeType) Implements IEmployeeTypeAdminService.DeleteEmployeeType
        Dim result As New ActionMessageResult(Of Domain.Payroll.Entities.EmployeeType)
        result.StateResult = True

        If employeeType Is Nothing Then
            Throw New ArgumentNullException("Empleado es vacio")
        End If
        Dim unitWork As IUnitWork = _EmployeeTypeRepository.UnitWork
        Try
            _EmployeeTypeRepository.DeleteEntity(employeeType)
            unitWork.Commit()

            '/***** Auditoria Basica ********/
            IndigoAuditBasic.Execute("EmployeeType", audit.Functional, employeeType.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
            '/*****Auditoria Avanzada ******/
            Dim auditObject As New IndigoAuditSimpleEntity(Of EmployeeType)(employeeType, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            auditObject.Execute()

            Return result

        Catch ex As DbUpdateException
            unitWork.RollbackChanges()
            result.StateResult = False
            result.MessageResult.Add(New MessageResult("c-0000", employeeType.Code))
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            unitWork.RollbackChanges()
            result.StateResult = False
            Return result
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un tipo de empleado por codigo
    ''' </summary>
    ''' <param name="code">codigo tipo empleado</param>
    ''' <returns> Employee</returns>
    Public Function GetEmployeeType(code As String) As Domain.Payroll.Entities.EmployeeType Implements IEmployeeTypeAdminService.GetEmployeeType
        Try

            Return _EmployeeTypeRepository.GetEmployeeType(code)

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guarda o edita un Empleado y todos sus agregados
    ''' </summary>
    ''' <param name="employeeType">Empleado</param>
    ''' <returns></returns>
    Public Function SaveEmployeeType(employeeType As Domain.Payroll.Entities.EmployeeType, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean Implements IEmployeeTypeAdminService.SaveEmployeeType
        If employeeType Is Nothing Then
            Throw New ArgumentNullException("Tipo Empleado Vacio")
        End If
        Dim unitWork As IUnitWork = _EmployeeTypeRepository.UnitWork
        Try

            Dim auditProcess As IndigoAuditSimpleEntity(Of EmployeeType)
            Dim AuxEmployeeType As EmployeeType = Nothing
            Dim status As Integer

            If employeeType.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                employeeType.ModificationUser = audit.CodeUser
                employeeType.ModificationDate = Date.Now()
                status = Infrastructure.CrossCutting.Audit.Actions.Update
                AuxEmployeeType = _EmployeeTypeRepository.GetEmployeeType(employeeType.Code, False)
            Else
                employeeType.CreationUser = audit.CodeUser
                employeeType.CreationDate = Date.Now()
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            End If

            'Valido si se va a guardar o a eliminar
            _EmployeeTypeRepository.SaveEntity(employeeType)
            unitWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of EmployeeType)(employeeType, audit, status, AuxEmployeeType)
            auditProcess.Execute()
            Return True

        Catch ex As Exception
            unitWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _EmployeeTypeRepository = Nothing
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
