'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 13-08-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Payroll
Imports Infrastructure.CrossCutting.IOC
Imports Infrastructure.CrossCutting.Base
Imports Domain.Payroll.Entities
Imports Domain.Payroll
Imports Domain.Base.Entities
Imports System.Text

Partial Class PayrollService

    Implements IPayrollIncentivePayment

    ''' <summary>
    ''' Elimina unas Primas Almacenadas
    ''' </summary>
    ''' <param name="incentivePayment">Primas</param>
    ''' <param name="session"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeleteIncentivePayment(incentivePayment As Domain.Payroll.Entities.IncentivePayment, session As Infrastructure.CrossCutting.Base.SessionValues) As Boolean Implements IPayrollIncentivePayment.DeleteIncentivePayment
        Using incentivePaymentPayroll As IIncentivePaymentAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IIncentivePaymentAdminService)()
            Return incentivePaymentPayroll.DeleteIncentivePayment(incentivePayment, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Almacena una Prima
    ''' </summary>
    ''' <param name="listIncentivePayment">Lista de Primas</param>
    ''' <param name="session"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveIncentivePayment(listIncentivePayment As List(Of Domain.Payroll.Entities.IncentivePayment), session As SessionValues) As ActionResult(Of List(Of IncentivePayment)) Implements IPayrollIncentivePayment.SaveIncentivePayment
        Using incentivePaymentPayroll As IIncentivePaymentAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IIncentivePaymentAdminService)()
            Return incentivePaymentPayroll.SaveIncentivePayment(listIncentivePayment, session)
        End Using
    End Function

    ''' <summary>
    ''' Calcula las Primas de un Grupo o Grupos
    ''' </summary>
    ''' <param name="strGroupId">String de ID's de Grupos</param>
    ''' <param name="period">Periodo o Semestre de la Prima</param>
    ''' <param name="MaxPremiumByYear"># de Primas por Año del Grupo</param>
    ''' <param name="employeeNit">Nit del Empleado</param>
    ''' <param name="offset">Índice inicial para paginación (-1 = modo legacy, >=0 = modo batch)</param>
    ''' <param name="pageSize">Tamaño del lote (50 por defecto)</param>
    ''' <returns>Action Result de Primas (Lista)</returns>
    Public Function CalculateIncentivePayment(strGroupId As String, period As Char, MaxPremiumByYear As Byte, VarYear As Integer, PaymentType As Char, session As SessionValues, Optional employeeNit As String = "", Optional valueExtraIncentivePayment As Double = 0.0, Optional offset As Integer = -1, Optional pageSize As Integer = 50) As Domain.Base.Entities.ActionMessageResult(Of List(Of IncentivePayment)) Implements IPayrollIncentivePayment.CalculateIncentivePayment
        Dim incentivePaymentPayroll As IIncentivePaymentAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IIncentivePaymentAdminService)()

        If offset >= 0 Then ' Modo con paginación
            Dim liquidationRepository As Domain.Payroll.IPayrollLiquidationRepository = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of Domain.Payroll.IPayrollLiquidationRepository)()
            Dim groupRepository As Domain.Payroll.IGroupRepository = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of Domain.Payroll.IGroupRepository)()
            Dim settingsRepository As Domain.Payroll.IPayrollSettingsRepository = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of Domain.Payroll.IPayrollSettingsRepository)()

            Dim dates = GetIncentivePaymentDates(strGroupId, period, groupRepository, settingsRepository)

            If dates Is Nothing OrElse dates.Item1 Is Nothing OrElse dates.Item2 Is Nothing Then
                Return New ActionMessageResult(Of List(Of IncentivePayment)) With {
                    .StateResult = False,
                    .Message = "No se pudieron determinar las fechas de liquidación."
                }
            End If

            Dim allEmployees = liquidationRepository.GetEmployesIncentivePayment(strGroupId, dates.Item2.Value, dates.Item1.Value, "")

            If allEmployees Is Nothing OrElse allEmployees.Count = 0 Then
                Return New ActionMessageResult(Of List(Of IncentivePayment)) With {
                    .StateResult = False,
                    .Message = "No existen empleados para liquidar en el grupo seleccionado."
                }
            End If

            Dim employeeBatch = allEmployees.Skip(offset).Take(pageSize).ToList()

            System.Diagnostics.Debug.WriteLine($"[PRIMAS-WCF] Lote: offset={offset}, pageSize={pageSize}, obtenidos={employeeBatch.Count}")

            Return incentivePaymentPayroll.CalculateIncentivePayment(strGroupId, period, MaxPremiumByYear, VarYear, PaymentType, session, employeeNit, valueExtraIncentivePayment, employeeBatch, offset)
        Else ' Modo legacy
            Return incentivePaymentPayroll.CalculateIncentivePayment(strGroupId, period, MaxPremiumByYear, VarYear, PaymentType, session, employeeNit, valueExtraIncentivePayment, Nothing, -1)
        End If
    End Function

    ''' <summary>
    ''' Obtiene una lista de Primas por Fecha Inicio, Fecha Fin y Id del Grupo
    ''' </summary>
    ''' <param name="StrGroupId">Id del Grupo</param>
    ''' <param name="MaxPremiumByYear">Máximo Primas por Año</param>
    ''' <param name="period">Periodo</param>
    ''' <param name="VarYear">Año</param>
    ''' <returns>Lista de Primas</returns>
    ''' <remarks></remarks>
    Public Function GetIncentivePaymentByPeriodGroupId(StrGroupId As String, MaxPremiumByYear As Byte, period As Char, VarYear As Integer, session As SessionValues, Status As Byte) As ActionMessageResult(Of List(Of IncentivePayment)) Implements IPayrollIncentivePayment.GetIncentivePaymentByPeriodGroupId
        Using incentivePaymentPayroll As IIncentivePaymentAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IIncentivePaymentAdminService)()
            Return incentivePaymentPayroll.GetIncentivePaymentByPeriodGroupId(StrGroupId, MaxPremiumByYear, period, VarYear, session, Status)
        End Using
    End Function

    ''' <summary>
    ''' Función que selecciona el Banco y arma el archivo Plano para Bancos
    ''' </summary>
    ''' <param name="ListIncentivePayment">Lista de Primas</param>
    ''' <param name="session">Objeto Auditoria</param>
    ''' <returns>ActionMessageResult(StringBuilder)</returns>
    ''' <remarks></remarks>
    Public Function GenerateBankFileIncentivePayment(PeriodEndDate As Date, BankId As Integer, AccountNumber As String, AccountType As String, CompanyId As Integer, session As SessionValues) As ActionMessageResult(Of StringBuilder) Implements IPayrollIncentivePayment.GenerateBankFileIncentivePayment
        Using incentivePaymentPayroll As IIncentivePaymentAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IIncentivePaymentAdminService)()
            Return incentivePaymentPayroll.GenerateBankFile(PeriodEndDate, BankId, AccountNumber, AccountType, CompanyId)
        End Using
    End Function

    ''' <summary>
    ''' Funciona que retorna la Lista de Fechas de Liquidaciones de Primas Confirmadas y que se pagan por Periodo Independiente
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetConfirmIncentivenDates(session As SessionValues) As List(Of Date) Implements IPayrollIncentivePayment.GetConfirmIncentivenDates
        Using incentivePaymentPayroll As IIncentivePaymentAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IIncentivePaymentAdminService)()
            Return incentivePaymentPayroll.GetConfirmIncentivenDates()
        End Using
    End Function

    Public Function GetHeadIncentivePayment(Year As Integer, Period As Integer, session As SessionValues) As List(Of IncentivePayment) Implements IPayrollIncentivePayment.GetHeadIncentivePayment
        Using incentivePaymentPayroll As IIncentivePaymentAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IIncentivePaymentAdminService)()
            Return incentivePaymentPayroll.GetHeadIncentivePayment(Year, Period)
        End Using
    End Function

    Public Function GetDetailIncentivePayment(GroupId As Integer, Year As Integer, Period As Integer, session As SessionValues) As List(Of IncentivePayment) Implements IPayrollIncentivePayment.GetDetailIncentivePayment
        Using incentivePaymentPayroll As IIncentivePaymentAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IIncentivePaymentAdminService)()
            Return incentivePaymentPayroll.GetDetailIncentivePayment(GroupId, Year, Period)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene el conteo de empleados para liquidar primas en un grupo y periodo específico
    ''' </summary>
    Public Function GetEmployeeCountForIncentivePayment(strGroupId As String, period As Char, session As SessionValues) As Integer Implements IPayrollIncentivePayment.GetEmployeeCountForIncentivePayment
        ' Resolver repositorios necesarios
        Dim liquidationRepository As IPayrollLiquidationRepository = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPayrollLiquidationRepository)()
        Dim groupRepository As IGroupRepository = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IGroupRepository)()
        Dim settingsRepository As IPayrollSettingsRepository = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPayrollSettingsRepository)()

        ' Obtener fechas de liquidación
        Dim dates = GetIncentivePaymentDates(strGroupId, period, groupRepository, settingsRepository)

        If dates Is Nothing OrElse dates.Item1 Is Nothing OrElse dates.Item2 Is Nothing Then
            Return 0
        End If
        Dim count = liquidationRepository.GetEmployesIncentivePaymentCount(strGroupId, dates.Item2.Value, dates.Item1.Value)
        Return count
    End Function

    ''' <summary>
    ''' Método helper para obtener las fechas de liquidación de primas
    ''' </summary>
    Private Function GetIncentivePaymentDates(strGroupId As String, period As Char, groupRepository As Domain.Payroll.IGroupRepository, settingsRepository As Domain.Payroll.IPayrollSettingsRepository) As Tuple(Of Date?, Date?)
        Dim groupId As Integer = CInt(strGroupId)
        Dim group = groupRepository.GetGroupById(groupId)

        If group Is Nothing Then
            Return Nothing
        End If

        ' Determinar fechas según el periodo
        Dim startDate As Date?
        Dim endDate As Date?

        If period = "1" Then
            startDate = group.PayrollParameter.StartDateIncentivePayment1
            endDate = group.PayrollParameter.EndDateIncentivePayment1
        Else
            startDate = group.PayrollParameter.StartDateIncentivePayment2
            endDate = group.PayrollParameter.EndDateIncentivePayment2
        End If

        ' Si no hay fechas en PayrollParameter, usar PayrollSettings
        If startDate Is Nothing OrElse endDate Is Nothing Then
            Dim settings = settingsRepository.GetSettingPayroll()

            If settings IsNot Nothing Then
                If period = "1" Then
                    startDate = settings.InitialDateServicesIncentivePayment
                    endDate = settings.EndDateServicesIncentivePayment
                Else
                    startDate = settings.InitialDateChristmasIncentivePayment
                    endDate = settings.EndDateChristmasIncentivePayment
                End If
            End If
        End If

        Return New Tuple(Of Date?, Date?)(startDate, endDate)
    End Function


End Class
