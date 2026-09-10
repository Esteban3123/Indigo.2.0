
Imports Domain.Base.Entities
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base

'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 12-11-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Public Interface IVacationPeriodDomain
    Inherits IDisposable

    Delegate Function CalculateAverageLiquidation(contractId As Integer, initialDate As Date, endDate As Date) As Decimal

    ''' <summary>
    ''' carga los periodos de vacaciones que tenga el empleado
    ''' </summary>
    ''' <param name="employees">Lista de empleados</param>
    ''' <returns>Lista de empleados</returns>
    ''' <remarks></remarks>
    Function LoadVacationPeriod(employees As List(Of Employee), Optional ByVal audit As AuditMessage = Nothing, Optional endDate As Date? = Nothing) As List(Of Employee)

    ''' <summary>
    ''' Calcula la fecha fin de vacaciones y la fecha de ingreso de vacaciones
    ''' </summary>
    ''' <param name="EmployeeId"></param>
    ''' <param name="RequestDay"></param>
    ''' <param name="InitialDate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CalculateDatesResumption(EmployeeId As Integer, RequestDay As Integer, InitialDate As Date) As ActionResult(Of Tuple(Of Date, Date))

    ''' <summary>
    ''' Calcula el valor de las vacaciones de los empleados y consume los periodos que necesita para salir a vacaciones
    ''' </summary>
    ''' <param name="employees">Empleados</param>
    ''' <param name="requestDays">Dias solicitados</param>
    ''' <param name="typeCalculate">Tipo de calculo</param>
    ''' <param name="typeVacation">Tipo de vacaciones</param>
    ''' <param name="typePayment">Tipo de pago</param>
    ''' <param name="initialDateVacation">Fecha inicial vacaciones</param>
    ''' <param name="holidays">Dias Festivos</param>
    ''' <returns>ActionMessageResult</returns>
    ''' <remarks></remarks>
    Function CalculateValueVacation(employees As List(Of Employee), requestDays As Integer, typeCalculate As Byte, typeVacation As Byte, typePayment As Byte, initialDateVacation As Date, Optional HUN As Boolean = False) As ActionMessageResult(Of List(Of Employee))

    ''' <summary>
    ''' Verifica si un dias es valido para las vacaciones
    ''' </summary>
    ''' <param name="listHoliday">Lista de festivos</param>
    ''' <param name="saturdayBusinessDay">Trabaja sabado</param>
    ''' <param name="sundayBusinessDay">Trabaja Domingo</param>
    ''' <param name="dateValidation">Fecha</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function IsValidDay(listHoliday As List(Of Domain.Entities.Holiday), saturdayBusinessDay As Boolean, sundayBusinessDay As Boolean, dateValidation As Date) As Boolean

    ''' <summary>
    ''' Modifica las vacaciones de un emplado para hacerle el inreso forzoso
    ''' </summary>
    ''' <param name="employee">Empleado con sus agregados</param>
    ''' <param name="initialDate">Fecha Inicial Vacaciones</param>
    ''' <param name="endDate">Fecha Final Vacaciones</param>
    ''' <param name="dateEntry">Fecha Ingreso Forzoso</param>
    ''' <param name="daysDifference">Dias diferencia entre ingreso forzoso y fecha final vacaciones</param>
    ''' <returns>Empleado con sus vacaciones modificadas</returns>
    ''' <remarks></remarks>
    Function ForceEntryVacationEmployee(employee As Employee, initialDate As Date, endDate As Date, dateEntry As Date, daysDifference As Integer, Optional ForceIngressResolutionNumber As String = Nothing, Optional ForceIngressResolutionDate As Date = Nothing, Optional incomeType As Integer = 0, Optional forceEntryResolutionDate As Date = Nothing, Optional contract As Domain.Payroll.Entities.Contract = Nothing) As Employee

    ''' <summary>
    ''' Crea una lista de detalles de calendarios en un rango de fechas
    ''' </summary>
    ''' <param name="initialDate">Fecha Inicial</param>
    ''' <param name="endDate">Fecha Final</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CreateListScheduleDetailBetweenDate(employee As Employee, contractValid As Contract, initialDate As Date, endDate As Date) As List(Of ScheduleDetail)

    ''' <summary>
    ''' Calcula el valor de las vacaciones de los empleados y consume los periodos que necesita para salir a vacaciones
    ''' </summary>
    ''' <param name="employees">Empleados</param>
    ''' <param name="requestDays">Dias solicitados</param>
    ''' <param name="typeCalculate">Tipo de calculo</param>
    ''' <param name="typeVacation">Tipo de vacaciones</param>
    ''' <param name="typePayment">Tipo de pago</param>
    ''' <param name="initialDateVacation">Fecha inicial vacaciones</param>
    ''' <param name="holidays">Dias Festivos</param>
    ''' <returns>ActionMessageResult</returns>
    ''' <remarks></remarks>
    Function CalculateValueVacationByFormulates(employees As List(Of Employee), requestDays As Integer, typeCalculate As Byte, typeVacation As Byte, typePayment As Byte, initialDateVacation As Date, indigo As SessionValues, Optional HUN As Boolean = False) As ActionMessageResult(Of List(Of Employee))

    ''' <summary>
    ''' Genera del Comprobante Contable para Vacaciones
    ''' </summary>
    ''' <param name="ListVacationDetail"></param>
    ''' <param name="Employee"></param>
    ''' <param name="Contract"></param>
    ''' <param name="payrollSettings"></param>
    ''' <param name="group"></param>
    ''' <param name="indigo"></param>
    ''' <returns></returns>
    Function CreateVoucherTransaction(Vacation As Vacation, Employee As Employee, group As Group, Session As Infrastructure.CrossCutting.Base.SessionValues) As ActionResult(Of Domain.Entities.VoucherTransaction)

    Function CreateJournalVoucher(ListVacationDetail As List(Of VacationDetail), Employee As Employee, Contract As Contract, group As Group, audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionResult(Of Domain.Entities.JournalVouchers)
End Interface
