'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 14-01-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Text

<ServiceContract()> _
Public Interface IPayrollIncentivePayment

    ''' <summary>
    ''' Almacena una Prima
    ''' </summary>
    ''' <param name="listIncentivePayment">Lista de Primas</param>
    ''' <param name="session"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveIncentivePayment(listIncentivePayment As List(Of Domain.Payroll.Entities.IncentivePayment), session As SessionValues) As ActionResult(Of List(Of IncentivePayment))

    ''' <summary>
    ''' Elimina unas Primas Almacenadas
    ''' </summary>
    ''' <param name="incentivePayment">Primas</param>
    ''' <param name="session"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function DeleteIncentivePayment(ByVal incentivePayment As IncentivePayment, session As SessionValues) As Boolean

    ''' <summary>
    ''' Calcula las Primas de un Grupo o Grupos
    ''' </summary>
    ''' <param name="strGroupId">String de ID's de Grupos</param>
    ''' <param name="period">Periodo o Semestre de la Prima</param>
    ''' <param name="MaxPremiumByYear"># de Primas por Año del Grupo</param>
    ''' <param name="employeeNit">Nit del Empleado</param>
    ''' <param name="valueExtraIncentivePayment">Valor de Prima Extra</param>
    ''' <param name="offset">Índice inicial para paginación (-1 = modo legacy, >=0 = modo batch)</param>
    ''' <param name="pageSize">Tamaño del lote (50 por defecto)</param>
    ''' <returns>Action Result de Primas (Lista)</returns>
    <OperationContract()>
    Function CalculateIncentivePayment(strGroupId As String, period As Char, MaxPremiumByYear As Byte, VarYear As Integer, PaymentType As Char, session As SessionValues, Optional employeeNit As String = "", Optional valueExtraIncentivePayment As Double = 0, Optional offset As Integer = -1, Optional pageSize As Integer = 50) As ActionMessageResult(Of List(Of IncentivePayment))

    ''' <summary>
    ''' Obtiene una lista de Primas por Fecha Inicio, Fecha Fin y Id del Grupo
    ''' </summary>
    ''' <param name="StrGroupId">Id del Grupo</param>
    ''' <param name="MaxPremiumByYear">Máximo Primas por Año</param>
    ''' <param name="period">Periodo</param>
    ''' <param name="VarYear">Año</param>
    ''' <returns>Lista de Primas</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetIncentivePaymentByPeriodGroupId(StrGroupId As String, MaxPremiumByYear As Byte, period As Char, VarYear As Integer, session As SessionValues, Status As Byte) As ActionMessageResult(Of List(Of IncentivePayment))

    ''' <summary>
    ''' Función que selecciona el Banco y arma el archivo Plano para Bancos
    ''' </summary>
    ''' <param name="ListIncentivePayment">Lista de Primas</param>
    ''' <param name="session">Objeto Auditoria</param>
    ''' <returns>ActionMessageResult(StringBuilder)</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GenerateBankFileIncentivePayment(PeriodEndDate As Date, BankId As Integer, AccountNumber As String, AccountType As String, CompanyId As Integer, session As SessionValues) As ActionMessageResult(Of StringBuilder)

    ''' <summary>
    ''' Funciona que retorna la Lista de Fechas de Liquidaciones de Primas Confirmadas y que se pagan por Periodo Independiente
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract> _
    Function GetConfirmIncentivenDates(session As SessionValues) As List(Of Date)

    <OperationContract> _
    Function GetHeadIncentivePayment(Year As Integer, Period As Integer, session As SessionValues) As List(Of IncentivePayment)

    <OperationContract> _
    Function GetDetailIncentivePayment(GroupId As Integer, Year As Integer, Period As Integer, session As SessionValues) As List(Of IncentivePayment)

    ''' <summary>
    ''' Obtiene el conteo de empleados para liquidar primas en un grupo y periodo específico
    ''' </summary>
    ''' <param name="strGroupId">Id del Grupo</param>
    ''' <param name="period">Periodo (1=Primer semestre, 2=Segundo semestre)</param>
    ''' <param name="session">Sesión</param>
    ''' <returns>Cantidad de empleados a liquidar</returns>
    <OperationContract>
    Function GetEmployeeCountForIncentivePayment(strGroupId As String, period As Char, session As SessionValues) As Integer

End Interface
