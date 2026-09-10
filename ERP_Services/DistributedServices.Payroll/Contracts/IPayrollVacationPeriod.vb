'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 24-10-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()> _
Public Interface IPayrollVacationPeriod

    ''' <summary>
    ''' Calcula la fecha fin de vacaciones y la fecha de ingreso de vacaciones
    ''' </summary>
    ''' <param name="EmployeeId"></param>
    ''' <param name="RequestDay"></param>
    ''' <param name="InitialDate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function CalculateDatesResumption(EmployeeId As Integer, RequestDay As Integer, InitialDate As Date, session As SessionValues) As ActionResult(Of Tuple(Of Date, Date))

    ''' <summary>
    ''' Obtiene las vacaciones que tenga solicitadas o pagas un empleado
    ''' </summary>
    ''' <param name="employeeId">Id del empleado</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetVacationPeriodByEmployee(employeeId As Integer, session As SessionValues) As List(Of VacationPeriod)

    ''' <summary>
    ''' Elimina una vacacion
    ''' </summary>
    ''' <param name="vacation">Vacacion</param>
    ''' <returns></returns>
    <OperationContract()> _
    Function DeleteVacationPeriod(ByVal vacation As VacationPeriod, session As SessionValues) As Boolean

    ''' <summary>
    ''' Guarda o edita una vacacion
    ''' </summary>
    ''' <param name="vacation">vacacion</param>
    ''' <returns></returns>
    <OperationContract()> _
    Function SaveVacationPeriod(ByVal vacation As VacationPeriod, session As SessionValues) As Boolean

    ''' <summary>
    ''' Lista todas la vacaciones que tiene el empleado y le agrega los periodos que se le debe
    ''' </summary>
    ''' <param name="choice">opcion de filtro 1 - Empleado 2 - Grupo</param>
    ''' <param name="value">Valor del filtro</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function ListVacationPeriodByFilter(choice As Byte, value As String, session As SessionValues) As List(Of Employee)

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
    <OperationContract()> _
    Function RequestVacationEmployees(employees As List(Of Employee), requestDays As Integer, typeCalculate As Byte, typeVacation As Byte, typePayment As Nullable(Of Byte), initialDateVacation As Date, session As SessionValues) As ActionMessageResult(Of List(Of Employee))

    ''' <summary>
    ''' Guarda o edita una vacacion desde el agregado de empleado
    ''' </summary>
    ''' <param name="listEmployee">Lista de empleados</param>
    ''' <param name="typeVacation">Tipo de vacacion 1 liquidar 2 Disfrutar</param>
    ''' <param name="session">Auditoria</param>
    ''' <returns>boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveVacationEmployee(ByVal listEmployee As List(Of Employee), typeVacation As Byte, initialDate As Nullable(Of Date), endDate As Nullable(Of Date), session As SessionValues) As ActionMessageResult


    ''' <summary>
    ''' Cancelar la solicitud de vacaciones que aun no esta pagada
    ''' </summary>
    ''' <param name="vacation"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function CancelRequest(vacation As Vacation, session As SessionValues) As ActionMessageResult(Of Vacation)

    ''' <summary>
    ''' Funcion para el realizar el ingreso forzoso
    ''' </summary>
    ''' <param name="vacation">Solicitud de vacacion</param>
    ''' <param name="dateEntry">Fecha ingreso</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function ForceEntry(vacation As Vacation, dateEntry As Date, session As SessionValues) As ActionMessageResult(Of Vacation)

    ''' <summary>
    ''' Funcion la cual obtiene las vacaciones de una fecha de liquidacion especifica
    ''' </summary>
    ''' <param name="dateLiquidation">Fecha de liquidacion de nomina</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetVacationLiquidationDate(dateLiquidation As Date, state As Byte, session As SessionValues) As List(Of Vacation)

    ''' <summary>
    ''' Funcion para listar todas las vacaciones de tengan cierto estado de incorporacion
    ''' </summary>
    ''' <param name="stateIncorporation">Estado de incorporacion</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetVacationStateIncorporation(stateIncorporation As Byte, session As SessionValues) As List(Of Vacation)

    ''' <summary>
    ''' Obtiene los detalles de calendario de varios empleados en un rango de fechas
    ''' </summary>
    ''' <param name="listIdEmpleoyee">Lista de ids de empleados</param>
    ''' <param name="dateInitial">Fecha Inicial</param>
    ''' <param name="dateEnd">Fecha Final</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetScheduleDetailByListEmployeeBetweenDate(listIdEmpleoyee As List(Of Integer), dateInitial As Date, dateEnd As Date, session As SessionValues) As List(Of ScheduleDetail)

    ''' <summary>
    ''' Obtiene la lista de novedades que estan dentro de un rango de fechas a excepcion de la que se envia como parametro
    ''' </summary>
    ''' <param name="listEmployeeId">Lista de id de empleados</param>
    ''' <param name="initialDate">Fecha de inicio</param>
    ''' <param name="endDate">Fecha Fin</param>
    ''' <returns>Lista de novedades</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetNoveltyByListIdEmployeeBetweenDate(listEmployeeId As List(Of Integer), initialDate As Date, endDate As Date, session As SessionValues) As List(Of Novelty)

    ''' <summary>
    ''' Funcion para listar las vaciones que estan en cierto rango de fechas
    ''' </summary>
    ''' <param name="initialDate">Fecha Inicial</param>
    ''' <param name="endDate">Fecha Final</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetVacationBetweenDate(employeeId As Integer, initialDate As Date, endDate As Date, session As SessionValues) As List(Of Vacation)

    ''' <summary>
    ''' Lista los periodos de vacaciones del empleado con detalle (vacaiones)
    ''' </summary>
    ''' <param name="employeeId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetVacationPeriodWithDetailByEmployee(employeeId As Integer, session As SessionValues) As List(Of VacationPeriod)

    <OperationContract()>
    Function IsValidDay(listHoliday As List(Of Domain.Entities.Holiday), saturdayBusinessDay As Boolean, sundayBusinessDay As Boolean, dateValidation As Date, session As Infrastructure.CrossCutting.Base.SessionValues) As Boolean

    ''' <summary>
    ''' Función para Confirmar las Vacaciones de Tipo Pago Inmediato. Genera Comprobante Contable y Comprobante de Egreso
    ''' </summary>
    ''' <param name="listVacation"> Lista de Vacaciones</param>
    ''' <param name="audit"></param>
    ''' <returns>ActionResult</returns>
    <OperationContract()>
    Function ConfirmVacation(listVacation As List(Of Domain.Payroll.Entities.Vacation), session As Infrastructure.CrossCutting.Base.SessionValues) As ActionResult
End Interface
