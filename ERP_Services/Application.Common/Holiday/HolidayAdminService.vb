'***********************************************************************
' Assembly         : Application.Common
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 26-04-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Common
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base
Imports Application.Base

Public Class HolidayAdminService
    Implements IHolidayAdminService

    ' Repositorio de Domingos y Festivos
    Private _holidayRepository As IHolidayRepository

    Public Sub New(ByVal holidayRepository As IHolidayRepository)
        If holidayRepository Is Nothing Then
            Throw New ArgumentNullException("holidayRepository vacio")
        End If
        _holidayRepository = holidayRepository
    End Sub

    ''' <summary>
    ''' Elimina un Domingo y/o Festivo
    ''' </summary>
    ''' <param name="holiday">Holiday</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeleteHoliday(holiday As Holiday, audit As AuditMessage) As Boolean Implements IHolidayAdminService.DeleteHoliday
        If holiday Is Nothing Then
            Throw New ArgumentNullException("Holiday vacio")
        End If
        Dim unitWork As IUnitWork = _holidayRepository.UnitWork
        Try
            _holidayRepository.DeleteEntity(holiday)
            unitWork.Commit()

            '/*****Auditoria Básica ******/
            IndigoAuditBasic.Execute(holiday.GetType.Name, audit.Functional, holiday.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Eliminar, audit.Company)
            '/*****Auditoria Avanzada ******/
            Dim auditObject As New IndigoAuditSimpleEntity(Of Holiday)(holiday, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            auditObject.Execute()
            Return True
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            unitWork.RollbackChanges()
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un Domingo y/o Festivo
    ''' </summary>
    ''' <param name="holiDate">Fecha</param>
    ''' <returns>Domingo y/o Festivo</returns>
    ''' <remarks></remarks>
    Public Function GetHoliday(holiDate As Date) As Holiday Implements IHolidayAdminService.GetHoliday
        If String.IsNullOrEmpty(holiDate) Then
            Throw New ArgumentNullException("holiDate Vacio")
        End If
        Try
            Return _holidayRepository.GetHoliday(holiDate)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New Domain.Entities.Holiday()
        End Try
    End Function

    ''' <summary>
    ''' Lista Todos los Domingos y/o Festivos
    ''' </summary>
    ''' <returns>Lista de Domingos y/o Festivos</returns>
    ''' <remarks></remarks>
    Public Function ListAllHolidays() As List(Of Holiday) Implements IHolidayAdminService.ListAllHolidays
        Try
            Return _holidayRepository.ListAllHolidays()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Almacena o Actuliza un Domingo y/o Festivo
    ''' </summary>
    ''' <param name="holiday">Holiday</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveHoliday(holiday As Holiday, audit As AuditMessage) As Boolean Implements IHolidayAdminService.SaveHoliday
        If holiday Is Nothing Then
            Throw New ArgumentNullException("holiday vacio")
        End If
        Dim unitWork As IUnitWork = _holidayRepository.UnitWork
        Try
            'Dim auxHoliday As Holiday
            'If holiday.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
            '    auxHoliday = _holidayRepository.GetHoliday(holiday.Holiday1).HolidayAux
            'End If

            If holiday.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                Dim resHoliday = GetHoliday(holiday.Holiday1)
                If resHoliday Is Nothing Then
                    'Valido si se va a guardar o a actualizar
                    _holidayRepository.SaveEntity(holiday)
                    unitWork.Commit()
                    '/*****Auditoria Básica ******/
                    IndigoAuditBasic.Execute(holiday.GetType.Name, audit.Functional, holiday.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Crear, audit.Company)
                    '/*****Auditoria Avanzada ******/
                    Dim auditObject As New IndigoAuditSimpleEntity(Of Holiday)(holiday, audit, Infrastructure.CrossCutting.Audit.Actions.Insert)
                    auditObject.Execute()
                End If
            ElseIf holiday.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                _holidayRepository.SaveEntity(holiday)
                unitWork.Commit()
                '/*****Auditoria Básica ******/
                IndigoAuditBasic.Execute(holiday.GetType.Name, audit.Functional, holiday.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Modificar, audit.Company)
                '/*****Auditoria Avanzada ******/
                Dim auditObject As New IndigoAuditSimpleEntity(Of Holiday)(holiday, audit, Infrastructure.CrossCutting.Audit.Actions.Update, holiday.OriginalValue)
                auditObject.Execute()
            End If

            Return True
        Catch ex As Exception
            unitWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function

    Public Function ListHolidaybyYear(year As Integer) As List(Of Holiday) Implements IHolidayAdminService.ListHolidaybyYear
        If String.IsNullOrEmpty(year) Then
            Throw New ArgumentNullException("year Vacio")
        End If
        Try
            Return _holidayRepository.ListHolidaybyYear(year)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene los festivos que estan dentro de un rango de fecha
    ''' </summary>
    ''' <param name="initialDate">Fecha Inicial</param>
    ''' <param name="endDate">Fecha Final</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListHolidayBetweenDate(initialDate As Date, endDate As Date) As List(Of Holiday) Implements IHolidayAdminService.ListHolidayBetweenDate
        Try
            Return _holidayRepository.ListHolidayBetweenDate(initialDate, endDate)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _holidayRepository = Nothing
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
