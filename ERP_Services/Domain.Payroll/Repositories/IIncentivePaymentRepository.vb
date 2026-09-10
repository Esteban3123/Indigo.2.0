'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 11-12-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Payroll.Entities

Public Interface IIncentivePaymentRepository

    Inherits IRepository(Of IncentivePayment)

    ''' <summary>
    ''' Obtiene una lista de Primas por Fecha Inicio, Fecha Fin y Id del Grupo
    ''' </summary>
    ''' <param name="groupId">Id del Grupo</param>
    ''' <param name="InitialDate">Fecha Inicio Periodo</param>
    ''' <param name="EndDate">Fecha Fin Periodo</param>
    ''' <param name="EmployeeId">Id del Empleado (opcional, para filtrar por empleado específico)</param>
    ''' <returns>Lista de Primas</returns>
    ''' <remarks></remarks>
    Function GetIncentivePaymentByPayrollDateGroupId(groupId As String, InitialDate As Date, EndDate As Date, Statuas As Byte, Optional tracking As Boolean = True, Optional EmployeeId As Integer = 0) As List(Of IncentivePayment)

    ''' <summary>
    ''' Obtiene la lista de Primas por Id del Contrato y Fecha Próxima Nómina
    ''' </summary>
    ''' <param name="contractId">Id del Contrato</param>
    ''' <param name="payrollNextDate">Fecha Próxima Nómina</param>
    ''' <returns>Lista de Primas</returns>
    ''' <remarks></remarks>
    Function GetIncentivePaymentByContractIdPayrollNextDate(contractId As String, PayrollNextDate As Date) As IncentivePayment

    ''' <summary>
    ''' Funciona que retorna la Lista de Fechas de Liquidaciones de Primas Confirmadas y que se pagan por Periodo Independiente
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetConfirmIncentivenDates() As List(Of Date)

    ''' <summary>
    ''' Función para traer el listado de Primas para Pago por Archivo de Bancos
    ''' </summary>
    ''' <param name="PeriodEndDate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetListIncentivePaymentBankFile(PeriodEndDate As Date) As List(Of IncentivePayment)

    ''' <summary>
    ''' Función para la Liquidación de Nómina de un empleado en vacaciones, para que me cargue la prima de servicios
    ''' </summary>
    ''' <param name="ContractId">Id del Contrato</param>
    ''' <returns>IncentivePayment</returns>
    ''' <remarks></remarks>
    Function GetIncentivePaymentByEmployeeIdLastLiquidation(EmployeeId As Integer) As List(Of IncentivePayment)

    Function GetIncentivePaymentByEmployeeIdRetefuente(EmployeeId As Integer, Year As Integer) As List(Of IncentivePayment)

    Function GetHeadIncentivePayment(GroupId As Integer, ByVal PeriodEndDate As Date, Period As Integer) As List(Of IncentivePayment)

    Function GetDetailIncentivePaymentDetail(GroupId As Integer, ByVal PeriodEndDate As Date, Period As Integer) As List(Of IncentivePayment)

    Function GetIncentivePaymentByContractId(ContractId As Integer) As List(Of IncentivePayment)

    Function SP_GenerateJournalVouchersIncentivePayment(GroupId As Integer, PeriodInitialDate As Date, PeriodEndDate As Date, Period As Integer, codeUser As String) As List(Of SP_GenerateJournalVouchersIncentivePayment_Result)
    ''' <summary>
    ''' Obtencion de primas para archivo plano para bancos
    ''' </summary>
    ''' <param name="BankFile"></param>
    ''' <returns></returns>
    Function GetIncentivePaymentBankFileProcess(BankFile As BankFile) As List(Of IncentivePayment)



    ''' <summary>
    ''' Obtiene el conteo de liquidaciones de primas (optimizado, sin cargar relaciones)
    ''' </summary>
    ''' <param name="groupId">Id del Grupo</param>
    ''' <param name="initialDate">Fecha inicial</param>
    ''' <param name="endDate">Fecha final</param>
    ''' <param name="status">Estado (1=En liquidación, 2=Confirmado)</param>
    ''' <param name="period">Periodo (1=Junio, 2=Diciembre)</param>
    ''' <returns>Cantidad de registros</returns>
    Function GetIncentivePaymentCountByDateGroup(groupId As String, initialDate As Date, endDate As Date, status As Byte, period As Char) As Integer
End Interface
