'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 30-07-2013
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

Public Class PayrollParameterAdminService

    Implements IPayrollParameterAdminService

    'Repositorio de empleados
    Private _PayrollParameterRepository As IPayrollParameterRepository

    ''' <summary>
    ''' inicia el repositorio de Parametros de Nómina
    ''' </summary>
    ''' <param name="payrollParameterRepository">Repositorio de Parámetros de Nómina</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal payrollParameterRepository As IPayrollParameterRepository)
        If (payrollParameterRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de Parámetros de Nómina vacio")
        End If
        _PayrollParameterRepository = payrollParameterRepository
    End Sub

    ''' <summary>
    ''' Elimina un Parámetro de Nómina
    ''' </summary>
    ''' <param name="payrollParameter">Parámetro de Nómina</param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeletePayrollParameter(payrollParameter As PayrollParameter, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean Implements IPayrollParameterAdminService.DeletePayrollParameter
        If payrollParameter Is Nothing Then
            Throw New ArgumentNullException("payrollParameter vacio")
        End If
        Dim unitWork As IUnitWork = _PayrollParameterRepository.UnitWork
        Try
            _PayrollParameterRepository.DeleteEntity(payrollParameter)
            unitWork.Commit()
            'IndigoAuditSimpleEntity(Of PayrollParameter).Execute(payrollParameter, audit, Infrastructure.CrossCutting.Audit.Actions.Delete, payrollParameter)
            Return True
        Catch ex As Exception
            unitWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un Parámetro de Nómina
    ''' </summary>
    ''' <param name="groupId">Group Id</param>
    ''' <returns>Parámetro de Nómina</returns>
    ''' <remarks></remarks>
    Public Function GetPayrollParameter(groupId As String) As PayrollParameter Implements IPayrollParameterAdminService.GetPayrollParameter
        If String.IsNullOrEmpty(groupId) Then
            Throw New ArgumentNullException("groupId vacio")
        End If
        Try
            Return _PayrollParameterRepository.GetPayrollParameter(groupId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Lista todos los Parámetros de Nómina
    ''' </summary>
    ''' <returns>Parámetros de Nómina</returns>
    ''' <remarks></remarks>
    Public Function ListAllPayrollParameter() As List(Of PayrollParameter) Implements IPayrollParameterAdminService.ListAllPayrollParameter
        Try
            Return _PayrollParameterRepository.ListAllPayrollParameter()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Almacena un Parámetro de Nómina
    ''' </summary>
    ''' <param name="payrollParameter">Parámetro de Nómina</param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SavePayrollParameter(payrollParameter As PayrollParameter, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean Implements IPayrollParameterAdminService.SavePayrollParameter
        If payrollParameter Is Nothing Then
            Throw New ArgumentNullException("payrollParameter vacio")
        End If
        Dim unitWork As IUnitWork = _PayrollParameterRepository.UnitWork
        Try
            _PayrollParameterRepository.SaveEntity(payrollParameter)
            unitWork.Commit()
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
            _PayrollParameterRepository = Nothing
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
