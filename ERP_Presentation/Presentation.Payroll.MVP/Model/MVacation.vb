'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Cristhian Mauricio Salazar
' Created          : 24-10-2013
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.Payroll.Entities
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo
Imports DevExpress.Data.Linq
Imports Domain.Base.Entities
Imports Presentation.Base

Public Class MVacation
    Inherits ModelBase
    Implements IDisposable

    Public Shared TAG As String = "587"

    Sub New(Tag As String)
        MyBase.New(Tag)
    End Sub

#Region "Methods"

    ''' <summary>
    ''' Obtiene las vacaciones que tenga solicitadas o pagas un empleado
    ''' </summary>
    ''' <param name="employeeId">Id del empleado</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Async Function GetVacationByEmployeeAsync(employeeId As Integer) As Task(Of List(Of VacationPeriod))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetVacationPeriodByEmployeeAsync(employeeId, Indigo)
    End Function

    ''' <summary>
    ''' Obtiene las vacaciones que tenga solicitadas o pagas un empleado
    ''' </summary>
    ''' <param name="employeeId">Id del empleado</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Async Function CalculateDatesResumption(EmployeeId As Integer, RequestDay As Integer, InitialDate As Date) As Task(Of ActionResult(Of Tuple(Of Date, Date)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.CalculateDatesResumptionAsync(EmployeeId, RequestDay, InitialDate, Indigo)
    End Function

    ''' <summary>
    ''' Elimina una vacacion
    ''' </summary>
    ''' <param name="vacation">Vacacion</param>
    ''' <returns></returns>
    Async Function DeleteVacationAsync(ByVal vacation As VacationPeriod) As Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.DeleteVacationPeriodAsync(vacation, Indigo)
    End Function

    ''' <summary>
    ''' Guarda o edita una vacacion
    ''' </summary>
    ''' <param name="vacation">vacacion</param>
    ''' <returns></returns>
    Async Function SaveVacationAsync(ByVal vacation As VacationPeriod) As Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveVacationPeriodAsync(vacation, Indigo)
    End Function

    ''' <summary>
    ''' Guarda o edita una vacacion por el agregado es el empleado 
    ''' </summary>
    ''' <param name="listEmployee">lista de empleados</param>
    ''' <param name="typeVacation">Tipo de vacacion 1 liquidar 2 Disfrutar</param>
    ''' <returns></returns>
    Async Function SaveVacationEmployeeAsync(ByVal listEmployee As List(Of Employee), typeVacation As Byte, initialDate As Nullable(Of Date), incorporationDate As Nullable(Of Date)) As Task(Of ActionMessageResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveVacationEmployeeAsync(listEmployee, typeVacation, initialDate, incorporationDate, Indigo)
    End Function

    ''' <summary>
    ''' Lista todas la vacaciones que tiene el empleado y le agrega los periodos que se le debe
    ''' </summary>
    ''' <param name="choice">opcion de filtro 1 - Empleado 2 - Grupo</param>
    ''' <param name="value">Valor del filtro</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ListVacationByFilterAsync(choice As Byte, value As String) As Task(Of List(Of Employee))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ListVacationPeriodByFilterAsync(choice, value, Indigo)
    End Function

    ''' <summary>
    ''' Funcion para solicitar las vacaciones de uno o varios empleados
    ''' </summary>
    ''' <param name="employees">Lista de empleados</param>
    ''' <param name="requestDays">Dias Solicitados</param>
    ''' <param name="typeCalculate">Tipo de calculo 1 - Promedio 2 - Sueldo Basico</param>
    ''' <param name="typeVacation">Tipo de vacaciones 1 - Liquuidar 2 - Disfrutar</param>
    ''' <param name="typePayment">Tipo de pago  1 - Inmedito 2 - Proxima Nomina</param>
    ''' <param name="initialDateVacation">Fecha Inicial Vacaciones</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function RequestVacationEmployees(employees As List(Of Employee), requestDays As Integer, typeCalculate As Byte, typeVacation As Byte, typePayment As Nullable(Of Byte), initialDateVacation As Date) As Task(Of ActionMessageResult(Of List(Of Employee)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.RequestVacationEmployeesAsync(employees, requestDays, typeCalculate, typeVacation, typePayment, initialDateVacation, Indigo)
    End Function

    ''' <summary>
    ''' Cancelar la solicitud de vacaciones que aun no esta pagada
    ''' </summary>
    ''' <param name="vacation"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function CancelRequestAsync(vacation As Vacation) As Task(Of Domain.Base.Entities.ActionMessageResult(Of Domain.Payroll.Entities.Vacation))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.CancelRequestAsync(vacation, Indigo)
    End Function

    ''' <summary>
    ''' Funcion para el realizar el ingreso forzoso
    ''' </summary>
    ''' <param name="vacation">Solicitud de vacacion</param>
    ''' <param name="dateEntry">Fecha ingreso</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Async Function ForceEntryAsync(vacation As Vacation, dateEntry As Date) As Task(Of Domain.Base.Entities.ActionMessageResult(Of Domain.Payroll.Entities.Vacation))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ForceEntryAsync(vacation, dateEntry, Indigo)
    End Function

    ''' <summary>
    ''' Obtiene todos los empleados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAllEmployee() As LinqInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.GetEmployee()
    End Function

    ''' <summary>
    ''' Lista todos los grupos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAllGroup() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.GetGroup()
    End Function

    ''' <summary>
    ''' Obtiene los detalles de calendario de varios empleados en un rango de fechas
    ''' </summary>
    ''' <param name="listIdEmpleoyee">Lista de ids de empleados</param>
    ''' <param name="dateInitial">Fecha Inicial</param>
    ''' <param name="dateEnd">Fecha Final</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetScheduleDetailByListEmployeeBetweenDateAsync(listIdEmpleoyee As List(Of Integer), dateInitial As Date, dateEnd As Date) As Task(Of List(Of ScheduleDetail))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetScheduleDetailByListEmployeeBetweenDateAsync(listIdEmpleoyee, dateInitial, dateEnd, Indigo)
    End Function

    ''' <summary>
    ''' Obtiene la lista de novedades que estan dentro de un rango de fechas a excepcion de la que se envia como parametro
    ''' </summary>
    ''' <param name="listEmployeeId">Lista de id de empleados</param>
    ''' <param name="initialDate">Fecha de inicio</param>
    ''' <param name="endDate">Fecha Fin</param>
    ''' <returns>Lista de novedades</returns>
    ''' <remarks></remarks>
    Public Async Function GetNoveltyByListIdEmployeeBetweenDateAsync(listEmployeeId As List(Of Integer), initialDate As Date, endDate As Date) As Task(Of List(Of Novelty))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetNoveltyByListIdEmployeeBetweenDateAsync(listEmployeeId, initialDate, endDate, Indigo)
    End Function

    ''' <summary>
    ''' Funcion para listar las vaciones que estan en cierto rango de fechas
    ''' </summary>
    ''' <param name="initialDate">Fecha Inicial</param>
    ''' <param name="endDate">Fecha Final</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetVacationBetweenDate(employeeId As Integer, initialDate As Date, endDate As Date) As Task(Of List(Of Vacation))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetVacationBetweenDateAsync(employeeId, initialDate, endDate, Indigo)
    End Function

    Public Async Function getResumptionHolidayByEmployeeId(EmployeeId As Integer) As Task(Of ActionResult(Of List(Of ResumptionHoliday)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetResumptionHolidayAsync(EmployeeId, Indigo)
    End Function

    ''' <summary>
    ''' Funcion Confirmar las Vacaciones Compensadas
    ''' </summary>
    ''' <param name="vacation">Solicitud de vacacion</param>
    ''' <param name="dateEntry">Fecha ingreso</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Async Function ConfirmVacationAsync(listVacation As List(Of Vacation)) As Task(Of Domain.Base.Entities.ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ConfirmVacationAsync(listVacation, Indigo)
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
