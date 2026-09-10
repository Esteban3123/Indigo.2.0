'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Kevin Garay Rodriguez
' Created          : 06-08-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Domain.Payroll.Entities
Imports Presentation.CloudAgent
Imports Presentation.Base

#End Region

''' <summary>
''' Realiza la conexion con los servicios del cuadro de turnos
''' </summary>
Public Class MSchedule
    Inherits ModelBase
    Implements IDisposable

    Shared TAG As String = "554"

    Sub New()
        MyBase.New(TAG)
    End Sub

#Region "Methods"

    ''' <summary>
    ''' Obtiene los empleados con turnos en una unidad funcional y perdiodo
    ''' </summary>
    ''' <param name="FUnitId">Id de la unidad funcional</param>
    ''' <param name="period">Periodo (mes y año)</param>
    ''' <returns>Tipo de estudio</returns>
    Public Async Function ListAllScheduleByFunctionalUnitWithPeriodAsync(ByVal FUnitId As String, ByVal period As String) As Task(Of List(Of Schedule))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetScheduleAsync(FUnitId, period, Indigo)
    End Function


    ''' <summary>
    ''' Guarda los cambios de los turnos de un empleado asincrono
    ''' </summary>
    ''' <param name="Schedule">El registro del empleado y sus turnos</param>
    ''' <returns>Si se realizo o no el cambio</returns>
    Public Async Function SaveScheduleAsync(ByVal Schedule As Schedule) As Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveScheduleAsync(Schedule, Indigo)
    End Function


    ''' <summary>
    ''' Borra los turnos de un empleado
    ''' </summary>
    ''' <param name="Schedule">El registro del empleado y sus turnos</param>
    ''' <returns>Si se realizo o no el cambio</returns>
    Public Async Function DeleteScheduleAsync(ByVal Schedule As Schedule) As Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.DeleteScheduleAsync(Schedule, Indigo)
    End Function

    ''' <summary>
    ''' Obtiene el listado de tipos de estudio asincrono
    ''' </summary>
    ''' <returns>Listado de tipos de estudio</returns>
    Public Async Function ListAllScheduleAsync() As Task(Of List(Of Schedule))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ListAllScheduleAsync(Indigo)
    End Function


    ''' <summary>
    ''' Lista los campos nulos de la base de datos y permitir su customizacion
    ''' </summary>
    ''' <returns>Un conjuto de datos con los campos marcados como nulos en la base de datos</returns>
    Public Function GetFieldsNULL() As DataSet
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetFieldsNULL("Schedule", Indigo)
    End Function

    Public Async Function GetScheduleDetail(ByVal id As String) As Task(Of ScheduleDetail)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetScheduleDetailAsync(id, Indigo)
    End Function

    ''' <summary>
    ''' Guarda los cambios de los turnos de un empleado asincrono
    ''' </summary>
    ''' <param name="ScheduleDetail">El registro del empleado y sus turnos</param>
    ''' <returns>Si se realizo o no el cambio</returns>
    Public Async Function SaveScheduleDetailAsync(ByVal ScheduleDetail As ScheduleDetail) As Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveScheduleDetailAsync(ScheduleDetail, Indigo)
    End Function

    ''' <summary>
    ''' Borra los turnos de un empleado
    ''' </summary>
    ''' <param name="ScheduleDetail">El registro del empleado y sus turnos</param>
    ''' <returns>Si se realizo o no el cambio</returns>
    Public Async Function DeleteScheduleDetailAsync(ByVal ScheduleDetail As ScheduleDetail) As Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.DeleteScheduleDetailAsync(ScheduleDetail, Indigo)
    End Function

    ''' <summary>
    ''' Funcion para obtener todos los turno que hay en un periodo y que tenga plantilla en un dia especifico
    ''' </summary>
    ''' <param name="period">periodo en el que esta el turno ej "08/2012"</param>
    ''' <param name="day">dia en el periodo del turno</param>
    ''' <returns>listado con el total de coincidencias para esa fecha</returns>
    Public Function GetScheduleByPeriodDay(period As String, day As Integer, functionalUnitId As Integer) As List(Of Schedule)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetScheduleByPeriodDay(period, day, functionalUnitId, Indigo)
    End Function

    ''' <summary>
    ''' Metodo que devuelve los empleados con turnos en un determinado periodo
    ''' </summary>
    ''' <param name="idEmployee">id del empleado</param>
    ''' <param name="period">periodo del schedule</param>
    ''' <returns>listado de los schedule
    ''' </returns>
    ''' <remarks></remarks>
    Public Async Function GetScheduleByEmployeePeriodAsync(idEmployee As Integer, period As String) As Task(Of List(Of Schedule))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetScheduleByEmployeePeriodAsync(idEmployee, period, 31, 1, Indigo)
    End Function

    ''' <summary>
    ''' Metodo que devuelve el schedule de un empleado en periodo y unidad funcional especifica
    ''' </summary>
    ''' <param name="idEmployee">id del empleado</param>
    ''' <param name="period">periodo del schedule</param>
    ''' <param name="functionalUnitId">id de la unidad funcional</param>
    ''' <returns>schedule</returns>
    ''' <remarks></remarks>
    Public Async Function GetScheduleByEmployeePeriodFunctionalUnitAsync(idEmployee As Integer, period As String, functionalUnitId As Integer) As Task(Of Schedule)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetScheduleByEmployeePeriodFunctionalUnitAsync(idEmployee, period, functionalUnitId, Indigo)
    End Function

    ''' <summary>
    ''' Metodo que devuelve el schedule de un empleado en periodo y unidad funcional especifica
    ''' </summary>
    ''' <param name="idEmployee">id del empleado</param>
    ''' <param name="period">periodo del schedule</param>
    ''' <param name="functionalUnitId">id de la unidad funcional</param>
    ''' <returns>schedule</returns>
    ''' <remarks></remarks>
    Public Async Function GetScheduleByEmployeePeriodFunctionalUnitRangeDaysAsync(idEmployee As Integer, period As String, functionalUnitId As Integer, diaMin As Integer, diaMax As Integer) As Task(Of Schedule)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetScheduleByEmployeePeriodFunctionalUnitRangeDaysAsync(idEmployee, period, functionalUnitId, diaMin, diaMax, Indigo)
    End Function

    ''' <summary>
    ''' obtiene un listado de los detalles de un empleado que estan dentro de un rango de fechas de manera asincronica
    ''' </summary>
    ''' <param name="employeeId">Id del empleado</param>
    ''' <param name="dateInitial">Fecha Inicial</param>
    ''' <param name="dateEnd">Fecha Final</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetScheduleDetailByEmployeeBetweenDateAsync(employeeId As Integer, dateInitial As Date, dateEnd As Date) As Task(Of List(Of ScheduleDetail))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetScheduleDetailByEmployeeBetweenDateAsync(employeeId, dateInitial, dateEnd, Indigo)
    End Function

    ''' <summary>
    ''' Elimina una lista de schedule details
    ''' </summary>
    ''' <param name="listScheduleDetail">Lista de los schedule a eliminar</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteScheduleDetailMasiveAsync(listScheduleDetail As List(Of ScheduleDetail)) As Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.DeleteScheduleDetailMasiveAsync(listScheduleDetail, Indigo)
    End Function

    ''' <summary>
    ''' Obtiene los schedule detail que son eventos para poder aprovarlos
    ''' </summary>
    ''' <param name="functionalunitId">id de la u funcional</param>
    ''' <param name="dateInitial">fecha inicial</param>
    ''' <param name="dateEnding">fecha final</param>
    ''' <returns>el listado de los schedule detail con eventos</returns>
    ''' <remarks></remarks>
    Public Async Function GetScheduleDetailWithEventsAsync(functionalunitId As Integer, dateInitial As Date, dateEnding As Date) As Task(Of List(Of ScheduleDetail))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetScheduleDetailWithEventsAsync(functionalunitId, dateInitial, dateEnding, Indigo)
    End Function

    ''' <summary>
    ''' Guarda un listado de schedule details con los Eventos aprobados
    ''' </summary>
    ''' <param name="list_schedule_details">Listado de detalles con eventos aprobados</param>
    ''' <returns>si realizo o no la accion</returns>
    ''' <remarks></remarks>
    Public Async Function SaveScheduleDetailWithEventMasiveAsync(list_schedule_details As List(Of Domain.Payroll.Entities.ScheduleDetail)) As Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveScheduleDetailWithEventMasiveAsync(list_schedule_details, Indigo)
    End Function

    ''' <summary>
    ''' Metodo que busca los scheduleDetails por id del contrato
    ''' </summary>
    ''' <param name="contractId">Id del contrato a buscar</param>
    ''' <returns>Lista de los schedule details encontrados</returns>
    Public Async Function GetScheduleDetailByContractId(contractId As Integer) As Task(Of List(Of ScheduleDetail))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetScheduleDetailByContractIdAsync(contractId, Indigo)
    End Function

    ''' <summary>
    ''' Guarda varios schedule a varios empleados de diferentes dias
    ''' </summary>
    ''' <param name="_scheduleDetail">El schedule detail (turno) que se replicará</param>
    ''' <param name="_employeesCheked">Listado de Id's de los empleados que se les registrara el turno</param>
    ''' <param name="_dayToSave">Listado de días que se van a registrar</param>
    ''' <param name="newFunctionalUnit">Objeto tipo FunctionalUnit, para saber si se registrar en una unidad funcional diferente</param>
    ''' <param name="Include_Holiday">Indica si se registran los dias feriados</param>
    ''' <param name="currentFunctionalUnit">Objeto que contiene la unidad funcional actual</param>
    ''' <param name="ScheduleDatasource">Listado de schedule que tiene el datasource actual</param>
    ''' <param name="Period">Periodo del schedule inicial en el formulario</param>
    ''' <param name="_List_Holidays">Listado de festivos para ese periodo</param>
    ''' <returns>Si se realizo o no la operación</returns>
    ''' <remarks></remarks>
    Public Async Function SaveScheduleDetailMasiveAsync(_scheduleDetail As ScheduleDetail, _employeesCheked As List(Of Integer), _dayToSave As List(Of Date), newFunctionalUnit As FunctionalUnit, Include_Holiday As Boolean, currentFunctionalUnit As FunctionalUnit, ScheduleDatasource As List(Of Schedule), Period As String, _List_Holidays As List(Of Domain.Entities.Holiday), Optional EditFlag As Boolean = False) As Task(Of Domain.Base.Entities.ActionMessageResult(Of List(Of Schedule)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveScheduleDetailMasiveAsync(_scheduleDetail, _employeesCheked, _dayToSave, newFunctionalUnit, Include_Holiday, currentFunctionalUnit, ScheduleDatasource, Period, _List_Holidays, Indigo, EditFlag)
    End Function

    ''' <summary>
    ''' Obtiene el talento humano de acuerdo al codigo 
    ''' </summary>
    ''' <param name="id">Id</param>
    ''' <returns>Talento humano</returns>
    Public Async Function GetEmployeeByIdForContractLiquidation(ByVal id As Integer) As Task(Of Employee)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetEmployeeByIdForContractLiquidationAsync(id, Indigo)
    End Function

    Public Async Function GetPositionRoll(ByVal RollId As Integer, ByVal RollCode As String) As Task(Of List(Of PositionRoll))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetPermissionRollAsync(RollCode, RollId, Indigo)
    End Function

    Public Async Function GetPositionUser(ByVal UserId As Integer) As Task(Of List(Of PositionUser))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetPositionUserAsync(UserId, Indigo)
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
