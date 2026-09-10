'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 11-12-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Text

Public Interface IIncentivePaymentAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene una lista de Primas por Fecha Inicio, Fecha Fin y Id del Grupo
    ''' </summary>
    ''' <param name="StrGroupId">Id del Grupo</param>
    ''' <param name="MaxPremiumByYear">Máximo Primas por Año</param>
    ''' <param name="period">Periodo</param>
    ''' <param name="VarYear">Año</param>
    ''' <returns>Lista de Primas</returns>
    ''' <remarks></remarks>
    Function GetIncentivePaymentByPeriodGroupId(StrGroupId As String, MaxPremiumByYear As Byte, period As Char, VarYear As Integer, SessionValues As SessionValues, Status As Byte) As ActionMessageResult(Of List(Of IncentivePayment))

    ''' <summary>
    ''' Elimina una Prima
    ''' </summary>
    ''' <param name="incentivePayment">Primas</param>
    ''' <param name="audit"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function DeleteIncentivePayment(ByVal incentivePayment As IncentivePayment, ByVal audit As AuditMessage) As Boolean

    ''' <summary>
    ''' Almacena las Primas
    ''' </summary>
    ''' <param name="listIncentivePayment">Lista de Primas</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function SaveIncentivePayment(listIncentivePayment As List(Of IncentivePayment), SessionValues As SessionValues) As ActionResult(Of List(Of IncentivePayment))

    ''' <summary>
    ''' Calcula las Primas de un Grupo o Grupos
    ''' </summary>
    ''' <param name="strGroupId">String de ID's de Grupos</param>
    ''' <param name="period">Periodo o Semestre de la Prima</param>
    ''' <param name="MaxPremiumByYear"># de Primas por Año del Grupo</param>
    ''' <param name="employeeNit">Nit del Empleado</param>
    ''' <param name="valueExtraIncentivePayment">Valor de Prima Extra</param>
    ''' <param name="employeeList">Lista opcional de empleados a liquidar (patrón nómina con lotes desde Presentation)</param>
    ''' <param name="offset">Índice de inicio del lote (-1 = modo legacy, 0 = primer lote, >0 = lotes siguientes)</param>
    ''' <returns>Action Result de Primas (Lista)</returns>
    ''' <remarks></remarks>
    Function CalculateIncentivePayment(strGroupId As String, period As Char, MaxPremiumByYear As Byte, VarYear As Integer, PaymentType As Char, SessionValues As SessionValues, Optional employeeNit As String = "", Optional valueExtraIncentivePayment As Double = 0, Optional employeeList As List(Of Employee) = Nothing, Optional offset As Integer = -1) As ActionMessageResult(Of List(Of IncentivePayment))

    ''' <summary>
    ''' Función que selecciona el Banco y arma el archivo Plano para Bancos
    ''' </summary>
    ''' <param name="ListIncentivePayment">Lista de Primas</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>ActionMessageResult(StringBuilder)</returns>
    ''' <remarks></remarks>
    Function GenerateBankFile(PeriodEndDate As Date, BankId As Integer, AccountNumber As String, AccountType As Integer, CompanyId As Integer) As ActionMessageResult(Of StringBuilder)

    ''' <summary>
    ''' Funciona que retorna la Lista de Fechas de Liquidaciones de Primas Confirmadas y que se pagan por Periodo Independiente
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetConfirmIncentivenDates() As List(Of Date)

    Function GetHeadIncentivePayment(Year As Integer, Period As Integer) As List(Of IncentivePayment)

    Function GetDetailIncentivePayment(GroupId As Integer, Year As Integer, Period As Integer) As List(Of IncentivePayment)
End Interface
