'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Faiber Mora
' Created          : 07-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Payroll
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Public Class VacationRequestAdminService
    Implements IVacationRequestAdminService

    ''' <summary>
    ''' Repositorio de vacaciones
    ''' </summary>
    ''' <remarks></remarks>
    Private _vacationRequestRepository As IVacationRequestRepository

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(vacationRequestRepository As IVacationRequestRepository)
        If vacationRequestRepository Is Nothing Then
            Throw New ArgumentException("El repositorio de solicitudes de vacaciones no puede ser nulo")
        End If
        Me._vacationRequestRepository = vacationRequestRepository
    End Sub

    ''' <summary>
    ''' Funcion para obtener las solicitudes de vacaciones de un empleado
    ''' </summary>
    ''' <param name="employeeId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetVacationRequestByEmployee(employeeId As Integer) As List(Of VacationRequest) Implements IVacationRequestAdminService.GetVacationRequestByEmployee
        Return _vacationRequestRepository.GetVacationRequestByEmployee(employeeId)
    End Function

    ''' <summary>
    ''' Guarda la solicitud de vacaciones hecha por el usuario
    ''' </summary>
    ''' <param name="vacationRequest"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function PostVacationRequest(ByVal vacationRequest As VacationRequest) Implements IVacationRequestAdminService.PostVacationRequest
        If vacationRequest Is Nothing Then
            Throw New ArgumentException("La solicitud de vacaciones no puede estar vacia")
        End If
        Dim unitWork As IUnitWork = _vacationRequestRepository.UnitWork
        Try
            _vacationRequestRepository.SaveEntity(vacationRequest)
            unitWork.Commit()
            Return True
        Catch ex As Exception
            unitWork.RollbackChanges()
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Elimina la solicitud de vacaciones solo si esta en estado registrado
    ''' </summary>
    ''' <param name="vacationRequest"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DeleteVacationRequest(ByVal vacationRequest As VacationRequest) Implements IVacationRequestAdminService.DeleteVacationRequest
        If vacationRequest Is Nothing Then
            Throw New ArgumentException("El Id de la Solicitud no puede ser nulo")
        End If
        Dim unikWork As IUnitWork = _vacationRequestRepository.UnitWork
        Try
            _vacationRequestRepository.DeleteEntity(vacationRequest)
            unikWork.Commit()
            Return True
        Catch ex As Exception
            unikWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolice")
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
            _vacationRequestRepository = Nothing
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
