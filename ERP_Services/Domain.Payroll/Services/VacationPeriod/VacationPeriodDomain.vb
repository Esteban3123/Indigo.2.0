'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 12-11-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Diagnostics
Imports Domain.Base.Entities
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base

Public Class VacationPeriodDomain
    Implements IVacationPeriodDomain

    ''' <summary>
    ''' Repositorio de liquidacion
    ''' </summary>
    ''' <remarks></remarks>
    Private _liquidationRepository As IPayrollLiquidationRepository

    ''' <summary>
    ''' Repositorio del libro fiscal
    ''' </summary>
    Private _bookRepository As Domain.Entities.IBookRepository

    ''' <summary>
    ''' Repositorio de liquidacion detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private _liquidationDetailRepository As ILiquidationDetailRepository

    ''' <summary>
    ''' Repositorio de festivos
    ''' </summary>
    ''' <remarks></remarks>
    Private _holidayRepository As Domain.Entities.IHolidayRepository

    ''' <summary>
    ''' Repositorio de Parámetros de Nómina
    ''' </summary>
    ''' <remarks></remarks>
    Private _PayrollSettings As IPayrollSettingsRepository

    ''' <summary>
    ''' Repositorio de Primas
    ''' </summary>
    ''' <remarks></remarks>
    Private _incentivePaymentRepository As IIncentivePaymentRepository

    ''' <summary>
    ''' Repositosio de Retroactivo
    ''' </summary>
    Private _retroactiveRepository As IRetroactiveCRepository

    ''' <summary>
    ''' Repositorio de Conceptos
    ''' </summary>
    Private _conceptRepository As IConceptRepository

    ''' <summary>
    ''' Repositorio de Cargos
    ''' </summary>
    Private _PositionRepository As IPositionRepository

    ''' <summary>
    ''' Repositorio de Novedades
    ''' </summary>
    Private _noveltyDomain As INoveltyDomain

    ''' <summary>
    ''' Repositorio para empleado
    ''' </summary>
    ''' <remarks></remarks>
    Private _employeeRepository As IEmployeeRepository

    ''' <summary>
    ''' Repositorio de Embargos
    ''' </summary>
    Private _ForeclousureRepository As IForeclousureRepository

    ''' <summary>
    ''' Repositorio de Convenios
    ''' </summary>
    Private _AgreementsCRepository As IAgreementsCRepository

    ''' <summary>
    ''' Repositorio de Conceptos Autorizados
    ''' </summary>
    ''' <remarks></remarks>
    Private _autorizationConceptRepository As IAuthorizationConceptRepository

    ''' <summary>
    ''' Repositorio de Conceptos de Nómina en parámetros contables
    ''' </summary>
    Private _IConceptAccountingStructureRepository As IConceptAccountingStructureRepository

    ''' <summary>
    ''' Repositorio de Distribuciones de Gastos de Nómina
    ''' </summary>
    Private _CostDistributionRepository As ICostDistributionsRepository

    ''' <summary>
    ''' Repositorio de Fondos
    ''' </summary>
    Private _FundsRepository As IFundsLevelRepository

    Private _entityBankAccount As Domain.Entities.IEntityBankAccountRepository

    Private _cashRegisterRepository As Domain.Entities.ICashRegisterRepository

    Private _expenseConceptRepository As Domain.Entities.IExpenseConceptRepository

    Private _manualConceptRepository As IManualConcepts

    Private _liquidationDomain As ILiquidationDomain

    Public Sub New(liquidationRepository As IPayrollLiquidationRepository, bookRepository As Domain.Entities.IBookRepository, holidayRepository As Domain.Entities.IHolidayRepository, PayrollSettings As IPayrollSettingsRepository,
                   incentivePaymentRepository As IIncentivePaymentRepository, retroactiveRepository As IRetroactiveCRepository, conceptRepository As IConceptRepository,
                   PositionRepository As IPositionRepository, employeeRepository As IEmployeeRepository, liquidationDetailRepository As ILiquidationDetailRepository, noveltyDomain As INoveltyDomain,
                   ForeclousureRepository As IForeclousureRepository, AgreementsCRepository As IAgreementsCRepository, autorizationConceptRepository As IAuthorizationConceptRepository,
                   IConceptAccountingStructureRepository As IConceptAccountingStructureRepository, CostDistributionRepository As ICostDistributionsRepository, FundsRepository As IFundsLevelRepository,
                   entityBankAccount As Domain.Entities.IEntityBankAccountRepository, cashRegisterRepository As Domain.Entities.ICashRegisterRepository, expenseConceptRepository As Domain.Entities.IExpenseConceptRepository,
                   manualConceptRepository As IManualConcepts, liquidationDomain As ILiquidationDomain)
        _liquidationRepository = liquidationRepository
        _bookRepository = bookRepository
        _holidayRepository = holidayRepository
        _PayrollSettings = PayrollSettings
        _incentivePaymentRepository = incentivePaymentRepository
        _retroactiveRepository = retroactiveRepository
        _conceptRepository = conceptRepository
        _PositionRepository = PositionRepository
        _employeeRepository = employeeRepository
        _liquidationDetailRepository = liquidationDetailRepository
        _noveltyDomain = noveltyDomain
        _ForeclousureRepository = ForeclousureRepository
        _AgreementsCRepository = AgreementsCRepository
        _autorizationConceptRepository = autorizationConceptRepository
        _IConceptAccountingStructureRepository = IConceptAccountingStructureRepository
        _CostDistributionRepository = CostDistributionRepository
        _FundsRepository = FundsRepository
        _entityBankAccount = entityBankAccount
        _cashRegisterRepository = cashRegisterRepository
        _expenseConceptRepository = expenseConceptRepository
        _manualConceptRepository = manualConceptRepository
        _liquidationDomain = liquidationDomain
    End Sub

    ''' <summary>
    ''' Método que calcula la fecha fin y la fecha de ingreso de vacaciones
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CalculateDatesResumption(EmployeeId As Integer, RequestDay As Integer, InitialDate As Date) As ActionResult(Of Tuple(Of Date, Date)) Implements IVacationPeriodDomain.CalculateDatesResumption
        Try
            'Fecha fin de vacaciones para validaciones
            Dim EndDateValidation As Date = Nothing
            'Fecha fin de vacaciones para retornar en la tupla
            Dim EndDateReturn As Date = Nothing
            'Fecha de ingreso de vacaciones
            Dim EntryDate As Date = Nothing

            'Se consulta el empleado por id
            Dim Employee = _employeeRepository.GetEmployeeById(EmployeeId, False)
            If Employee Is Nothing Then
                Return New ActionResult(Of Tuple(Of Date, Date)) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = "No existe el empleado"}
            End If

            'Se valida que la fecha inicial no sea menor a la fecha actual del grupo del empleado
            If InitialDate < Employee.Contract.ToList().Find(Function(x) x.Valid = True).Group.NextDateLiquidation Then
                Return New ActionResult(Of Tuple(Of Date, Date)) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = "La fecha inicial no puede ser menor a la fecha del grupo del empleado (" + Employee.Contract.ToList().Find(Function(x) x.Valid = True).Group.NextDateLiquidation + ")"}
            End If

            'Se obtiene los parametros del grupo
            Dim PayrollParameter = Employee.Contract.ToList().Find(Function(x) x.Valid = True).Group.PayrollParameter

            'Se obtiene los dias festivos desde la fecha inicial hasta seis meses despues
            Dim ListHolidays = _holidayRepository.ListHolidayBetweenDate(InitialDate, InitialDate.AddMonths(6))

            'Se valida que el día de inicio de vacaciones sea un día hábil
            Dim cont As Integer = 0
            While cont = 0
                If IsValidDay(ListHolidays, PayrollParameter.SaturdayBusinessDay, PayrollParameter.SundayBusinessDay, InitialDate) Then
                    cont = 1
                Else
                    InitialDate = DateAdd(DateInterval.Day, 1, InitialDate)
                End If
            End While

            'Se calcula la fecha fin de vacaciones
            EndDateValidation = InitialDate
            EndDateValidation = DateAdd(DateInterval.Day, RequestDay - 1, InitialDate)

            'Se asigna la fecha fin para retornar
            EndDateReturn = EndDateValidation

            'Se le aumenta un dia a la fecha fin de vacaciones y se valida que ese dia sea habil para la fecha de ingreso
            EndDateValidation = DateAdd(DateInterval.Day, 1, EndDateValidation)
            cont = 0
            While cont = 0
                If IsValidDay(ListHolidays, PayrollParameter.SaturdayBusinessDay, PayrollParameter.SundayBusinessDay, EndDateValidation) Then
                    cont = 1
                Else
                    EndDateValidation = DateAdd(DateInterval.Day, 1, EndDateValidation)
                End If
            End While

            'Se asigna la fecha de ingreso
            EntryDate = EndDateValidation

            Return New ActionResult(Of Tuple(Of Date, Date)) With {.ObjectEmbbeded = New Tuple(Of Date, Date)(EndDateReturn, EntryDate), .StateResult = True, .StatusCode = eStatusResult.SUCCESS}
        Catch ex As Exception
            Return New ActionResult(Of Tuple(Of Date, Date)) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' carga los periodos de vacaciones que tenga el empleado
    ''' </summary>
    ''' <param name="employees">Lista de empleados</param>
    ''' <returns>Lista de empleados</returns>
    ''' <remarks></remarks>
    Public Function LoadVacationPeriod(employees As List(Of Employee), Optional audit As AuditMessage = Nothing, Optional endWorkingDate As Date? = Nothing) As List(Of Employee) Implements IVacationPeriodDomain.LoadVacationPeriod
        For Each itemEmployee As Employee In employees
            Dim contractValid As Contract
            Dim queryContract = itemEmployee.Contract.Where(Function(x) x.Valid = True)

            If queryContract.Count = 1 Then
                contractValid = queryContract.SingleOrDefault()
            ElseIf queryContract.Count > 1 Then
                Throw New Exception("El empleado " & itemEmployee.ThirdParty.Name & " posee dos contratos activos")
            Else
                Continue For
            End If
            Dim endDate = Date.Now()
            If endWorkingDate IsNot Nothing Then
                endDate = endWorkingDate
            End If

            Dim addPeriod As Integer = 0
            Dim vacationAdd = contractValid.Group.AdditionalVacationDays 'Dias de vacaciones adicionales obtenidos desde grupos
            If endWorkingDate Is Nothing Then
                If contractValid.ContractType.Undefined = False Then ' Si el contrato NO es indefinido
                    If contractValid.Group.PayrollParameter.VacationAdvanced = True Then 'Si el grupo posee el parametro vacaciones avanzadas le permito mostrar todos los posibles periodos que tenga
                        endDate = contractValid.ContractEndingDate
                    End If
                Else ' Si el contrato es indefinido
                    If contractValid.Group.PayrollParameter.VacationAdvanced = True Then ' Y si el grupo posee el parametro vacaciones avanzadas le agrego un periodo mas
                        addPeriod = 1
                    End If
                End If
            End If
            Dim monthContract = DateDiff(DateInterval.Month, itemEmployee.VacationLastDateLiquidation, endDate)

            Dim vacationByMonth = 12 / contractValid.Group.PayrollParameter.MaxVacationByYear
            Dim numberPeriodsVacation As Integer = 0

            If endWorkingDate IsNot Nothing Then
                numberPeriodsVacation = CType(Math.Ceiling(monthContract / vacationByMonth), Integer) + addPeriod
            Else
                numberPeriodsVacation = CType(monthContract / vacationByMonth, Integer) + addPeriod
            End If

            Dim NumberVacationChanged As Boolean = False
            Dim QueryContractBefore = itemEmployee.Contract.Where(Function(x) x.Status = 4)
            For Each item In QueryContractBefore
                If DateDiff(DateInterval.Day, item.ContractEndingDate, contractValid.ContractInitialDate) = 1 And item.Group.PayrollParameter.MaxVacationByYear <> contractValid.Group.PayrollParameter.MaxVacationByYear Then
                    NumberVacationChanged = True
                End If
            Next

            ' Si el último período abierto quedó truncado al vencimiento de un contrato a
            ' término fijo que se renovó al día siguiente sin solución de continuidad (caso
            ' típico de Alto Riesgo), se extiende de vuelta al cierre natural del período en
            ' vez de quedar truncado para siempre, ya que la generación de períodos nunca
            ' revisita un período ya persistido.
            If itemEmployee.VacationPeriod.Count > 0 Then
                Dim lastPeriod = itemEmployee.VacationPeriod.OrderBy(Function(v) v.InitialDatePeriod).Last()
                If lastPeriod.TakenDays = 0 AndAlso lastPeriod.PendingDays > 0 AndAlso
                   DateDiff(DateInterval.Day, lastPeriod.EndDatePeriod, contractValid.ContractInitialDate) = 1 Then
                    Dim naturalEndDate = lastPeriod.InitialDatePeriod.AddMonths(vacationByMonth).AddDays(-1)
                    If lastPeriod.EndDatePeriod < naturalEndDate Then
                        Dim correctedEndDate = If(contractValid.ContractEndingDate < naturalEndDate, contractValid.ContractEndingDate, naturalEndDate)
                        If correctedEndDate > lastPeriod.EndDatePeriod Then
                            Dim baseVacationDaysCorrection As Byte = contractValid.Group.PayrollParameter.VacationDays
                            Dim newVacationDays As Byte
                            If correctedEndDate = naturalEndDate Then
                                newVacationDays = baseVacationDaysCorrection
                            Else
                                Dim periodDays = DateDiff(DateInterval.Day, lastPeriod.InitialDatePeriod, correctedEndDate)
                                newVacationDays = CByte(Math.Truncate(periodDays * baseVacationDaysCorrection / 365))
                            End If
                            Dim addedDays = newVacationDays - lastPeriod.VacationDays
                            lastPeriod.EndDatePeriod = correctedEndDate
                            lastPeriod.VacationDays = newVacationDays
                            lastPeriod.PendingDays += addedDays
                            lastPeriod.ContractId = contractValid.Id
                            lastPeriod.MarkAsModified()
                        End If
                    End If
                End If
            End If

            For index = 1 To numberPeriodsVacation Step 1
                Dim vacation As VacationPeriod = New VacationPeriod()
                'vacation.Employee = itemEmployee
                Dim _booleanVactationPeriod = IIf(itemEmployee.VacationPeriod.Count > 0, True, False)


                vacation.ContractId = contractValid.Id
                If index = 1 Then
                    If _booleanVactationPeriod Then
                        ' El corte por cambio de grupo solo aplica si el último período realmente
                        ' atraviesa la fecha de transición (empieza antes y termina después). Sin el
                        ' chequeo de InitialDatePeriod, un período ya generado por completo bajo el
                        ' grupo nuevo se recortaba y regeneraba cada vez que se volvía a consultar).
                        If itemEmployee.VacationPeriod.LastOrDefault.InitialDatePeriod < contractValid.ContractInitialDate AndAlso
                           itemEmployee.VacationPeriod.LastOrDefault.EndDatePeriod > contractValid.ContractInitialDate And itemEmployee.VacationPeriod.LastOrDefault.TakenDays = 0 And NumberVacationChanged Then
                            With itemEmployee.VacationPeriod.LastOrDefault
                                .EndDatePeriod = contractValid.ContractInitialDate.AddDays(-1)
                            End With
                        End If
                        vacation.InitialDatePeriod = itemEmployee.VacationPeriod.LastOrDefault.EndDatePeriod.AddDays(+1)
                    Else
                        vacation.InitialDatePeriod = itemEmployee.VacationLastDateLiquidation
                    End If
                Else
                    vacation.InitialDatePeriod = itemEmployee.VacationPeriod.LastOrDefault.EndDatePeriod.AddDays(+1)
                End If

                vacation.EndDatePeriod = vacation.InitialDatePeriod.AddMonths(vacationByMonth).AddDays(-1)
                If endWorkingDate IsNot Nothing AndAlso vacation.EndDatePeriod > endWorkingDate.Value Then
                    vacation.EndDatePeriod = endWorkingDate.Value
                End If

                'Fecha de finalizacion del contrato es menor a la fecha de finalizacion del periodo
                If DateDiff(DateInterval.Day, vacation.EndDatePeriod, contractValid.ContractEndingDate) < 0 Then
                    vacation.EndDatePeriod = contractValid.ContractEndingDate
                End If
                'La fecha de finalizacion del nuevo periodo es menor a la fecha inicial: el contrato
                'vigente no alcanza a cubrir ni un día de un período adicional (p.ej. termina el mismo
                'día en que ya cierra el último período existente). No hay período nuevo que generar;
                'se descarta el candidato sin tocar el último período ya persistido (antes este bloque
                'le pisaba PendingDays con un cálculo de DateDiff sin relación con los días reales).
                If DateDiff(DateInterval.Day, vacation.InitialDatePeriod, vacation.EndDatePeriod) < 0 Then
                    Exit For
                End If
                'La fecha de inicio es mayor a la fecha actual o 
                'La fecha de inicio es mayor a la fecha de finalizacion del contrato
                If DateDiff(DateInterval.Day, endDate, vacation.InitialDatePeriod) > 0 Then
                    Exit For
                End If
                Dim listExistPeriod = itemEmployee.VacationPeriod.Where(Function(x) x.InitialDatePeriod = vacation.InitialDatePeriod And x.EndDatePeriod = vacation.EndDatePeriod)
                If listExistPeriod.Count > 0 Then
                    Continue For
                End If
                'Si la fecha de finalizacion es menor a un año
                'o si la fecha de finalizacion es igual a la fecha de finalizacion del contrato
                'los dias de vacaciones se calcula proporcional
                Dim isDateMismatch As Boolean = DateDiff(DateInterval.Day, vacation.EndDatePeriod, vacation.InitialDatePeriod.AddMonths(vacationByMonth).AddDays(-1)) <> 0
                Dim baseVacationDays As Byte = contractValid.Group.PayrollParameter.VacationDays

                If isDateMismatch Then
                    Dim PeriodDays As Integer = DateDiff(DateInterval.Day, vacation.InitialDatePeriod, vacation.EndDatePeriod)

                    If endWorkingDate IsNot Nothing AndAlso endWorkingDate.Value.Day < vacation.InitialDatePeriod.Day Then
                        PeriodDays -= 1 ' Ajuste por no haber cumplido el día del ciclo
                    End If

                    vacation.VacationDays = Math.Truncate(PeriodDays * baseVacationDays / 365)
                Else 'Si el papametro de dias adicionales tiene dias
                    If vacationAdd > 0 AndAlso contractValid.Group.NextDateLiquidation >= vacation.EndDatePeriod Then
                        vacation.VacationDays = baseVacationDays + vacationAdd
                    Else
                        vacation.VacationDays = baseVacationDays
                    End If
                End If
                'Si los dias de vacaciones es menor o igual a cero
                If vacation.VacationDays <= 0 Then
                    Exit For
                End If
                vacation.PendingDays = vacation.VacationDays
                vacation.TakenDays = 0
                vacation.CreationDate = Date.Now()
                vacation.CreationUser = ""
                itemEmployee.VacationPeriod.Add(vacation)
            Next
        Next
        Return employees
    End Function

    ''' <summary>
    ''' Calcula el valor de las vacaciones de los empleados y consume los periodos que necesita para salir a vacaciones
    ''' </summary>
    ''' <param name="employees">Lista de empleados</param>
    ''' <param name="requestDays">Dias Solicitados</param>
    ''' <param name="typeCalculate">Tipo de calculo 1 - Promedio 2 - Sueldo Basico </param>
    ''' <param name="typeVacation">Tipo de vacaciones 1 - Liquuidar 2 - Disfrutar 3 - Permiso con cargo a vacaciones</param>
    ''' <param name="typePayment">Tipo de pago  1 - Inmedito 2 - Proxima Nomina</param>
    ''' <param name="initialDateVacation">Fecha Inicial Vacaciones</param>
    ''' <returns>ActionMessageResult</returns>
    ''' <remarks></remarks>
    Public Function CalculateValueVacation(employees As List(Of Employee), requestDays As Integer, typeCalculate As Byte, typeVacation As Byte, typePayment As Byte, initialDateVacation As Date, Optional HUN As Boolean = False) As ActionMessageResult(Of List(Of Employee)) Implements IVacationPeriodDomain.CalculateValueVacation

        Dim VacationPlusDays As Integer = 0

        If HUN = True Then
            Dim Contract = employees(0).Contract.ToList().Find(Function(x) x.Valid = True)

            If Contract.ContractType.JobBondingType.Code = "005" Then

                Dim JobBondingDate = Contract.JobBondingDate

                Dim YearsDifference = DateDiff(DateInterval.Year, JobBondingDate, Date.Now)

                If YearsDifference >= 5 And YearsDifference <= 10 Then
                    VacationPlusDays = 2
                End If

                If YearsDifference >= 11 And YearsDifference <= 15 Then
                    VacationPlusDays = 3
                End If

                If YearsDifference >= 16 And YearsDifference <= 20 Then
                    VacationPlusDays = 4
                End If

                If YearsDifference >= 21 Then
                    VacationPlusDays = 5
                End If
            End If
        End If


        Dim actionResult As ActionMessageResult(Of List(Of Employee)) = New ActionMessageResult(Of List(Of Employee))()
        actionResult.StateResult = True

        Dim endDate As Date
        Dim incorporationDate As Date
        Dim enjoyDays As Integer = 0
        requestDays = requestDays + VacationPlusDays
        Dim paidDays As Integer = 0
        Dim DayNotCount As Integer = 0
        employees(0).MarkAsAdded()
        Dim index As Integer = 1
        Dim payrollParameter = employees(0).Contract.ToList().Find(Function(x) x.Valid = True).Group.PayrollParameter
        If typeVacation = 2 Or typeVacation = 4 Then ' Es disfrutar
            Dim holidays As List(Of Domain.Entities.Holiday) = _holidayRepository.ListHolidayBetweenDate(initialDateVacation, initialDateVacation.AddMonths(6))
            endDate = initialDateVacation.AddDays(-1)
            While index <= requestDays 'Calculo la fecha final de vacaciones
                endDate = endDate.AddDays(1)

                If HUN = False Then
                    If IsValidDay(holidays, payrollParameter.SaturdayBusinessDay, payrollParameter.SundayBusinessDay, endDate) Then ' Si no es un festivo ni un domingo aumenta el indice
                        index += 1
                    End If
                Else
                    If IsValidDay(holidays, False, False, endDate) Then ' Si no es un festivo ni un domingo aumenta el indice
                        index += 1
                    End If
                End If
                enjoyDays += 1

                If payrollParameter.Day31 = False Then
                    If Day(endDate) = 31 Then
                        DayNotCount += 1
                    End If
                Else
                    DayNotCount = 0
                End If


            End While

            incorporationDate = endDate.AddDays(1)
            If HUN = True Then
                While IsValidDay(holidays, payrollParameter.SaturdayBusinessDay, payrollParameter.SundayBusinessDay, incorporationDate) = False 'Calculo la fecha de incorporacion
                    enjoyDays += 1
                    incorporationDate = incorporationDate.AddDays(1)
                End While
            Else
                While IsValidDay(holidays, payrollParameter.SaturdayBusinessDay, payrollParameter.SundayBusinessDay, incorporationDate) = False 'Calculo la fecha de incorporacion
                    'enjoyDays += 1
                    incorporationDate = incorporationDate.AddDays(1)
                End While
            End If


            paidDays = enjoyDays - DayNotCount

        Else ' Si eligio liquidar o permiso a vacaciones
            If typeVacation = 3 Then 'Permiso a vacaciones
                endDate = initialDateVacation.AddDays(requestDays - 1)
            End If
            enjoyDays = requestDays
        End If
        For Each employee As Employee In employees 'Recorro los empleados que hay que liquidar
            Dim pendingDays As Integer = 0
            employee.VacationPeriod.ToList().ForEach(Sub(x)
                                                         pendingDays += x.PendingDays
                                                     End Sub)
            If typeVacation = 2 Then 'Disfrutar
                Dim vacationConflict As Vacation = Nothing
                employee.VacationPeriod.ToList.ForEach(Sub(x)
                                                           Dim hit = x.Vacation.FirstOrDefault(Function(v) (initialDateVacation >= v.VacationStartDate And initialDateVacation <= v.VacationEndDate) _
                                                                                Or (endDate >= v.VacationStartDate And endDate <= v.VacationEndDate))
                                                           If hit IsNot Nothing AndAlso vacationConflict Is Nothing Then
                                                               vacationConflict = hit
                                                               Exit Sub
                                                           End If
                                                       End Sub)
                If vacationConflict IsNot Nothing Then 'El empleado esta en vacaciones
                    actionResult.StateResult = False
                    actionResult.MessageResult.Add(New MessageResult("V003", employee.ThirdParty.Name, vacationConflict.VacationStartDate, vacationConflict.VacationEndDate))
                    Continue For
                End If
            End If
            If pendingDays = 0 Then 'No posee dias pendientes
                actionResult.StateResult = False
                actionResult.MessageResult.Add(New MessageResult("V002", employee.ThirdParty.Name))
                Continue For
            ElseIf (requestDays - VacationPlusDays) > pendingDays Then 'La cantidad de dias solicitada es mayor a la disponible
                actionResult.StateResult = False
                actionResult.MessageResult.Add(New MessageResult("V001", employee.ThirdParty.Name, pendingDays))
                Continue For
            End If
            Dim contract As Contract = employee.Contract.Where(Function(x) x.Valid = True).SingleOrDefault()

            ' IBC del mes anterior para PreviousMonthIBC en vacaciones
            Dim IBCLastPeriod As Nullable(Of Decimal) = Nothing
            Dim listPrevLiqVacation = _liquidationRepository.LiquidationLastMonths(contract.InitialContractNumber, 1, contract.Group.NextDateLiquidation)
            If listPrevLiqVacation IsNot Nothing AndAlso listPrevLiqVacation.Count > 0 Then
                Dim prevLiqVacation = listPrevLiqVacation.Where(Function(x) x.RegisterStatus = "C").OrderByDescending(Function(x) x.PayrollDateLiquidated).FirstOrDefault()
                If prevLiqVacation IsNot Nothing Then
                    IBCLastPeriod = prevLiqVacation.PeriodJCB
                End If
            End If

            Dim valueBase As Decimal
            Dim valueVacation As Decimal
            Dim valueBaseDays As Decimal
            If typeCalculate = 1 Then 'Calculo el Promedio
                Dim dateIndex = Date.Now().AddDays(-Date.Now().Day + 1)
                Dim valueTotalLiquidation As Decimal = 0
                Dim monthsAverage As Integer = 0
                Dim valueSalary As Decimal = contract.BasicSalary
                For index = 1 To contract.Group.PayrollParameter.AverageMonthVacation Step 1

                    Dim listLiquidation = _liquidationDetailRepository.LiquidationDetailBaseVacationByEmployeeDate(contract.EmployeeId, dateIndex.AddMonths(-1), dateIndex.AddDays(-1))
                    Dim valueLiquidation As Decimal = 0
                    listLiquidation.ForEach(Sub(x)
                                                If x.AccruedValue IsNot Nothing = True Then
                                                    valueLiquidation += x.AccruedValue.Value
                                                End If
                                            End Sub)
                    If valueLiquidation > 0 Then
                        valueTotalLiquidation += valueLiquidation
                        monthsAverage += 1
                    End If
                    dateIndex = dateIndex.AddMonths(-1)
                Next
                If monthsAverage = 0 Then
                    valueBase = 0
                Else
                    valueBase = (valueTotalLiquidation / monthsAverage) + valueSalary
                End If
            Else 'Sueldo Basico
                valueBase = contract.BasicSalary
            End If
            valueBaseDays = valueBase / 30
            valueVacation = valueBaseDays * paidDays
            'valueVacation = (valueBase / 30) * paidDays
            If typeVacation = 1 Then
                valueVacation = valueBaseDays * requestDays
            End If
            Dim health, pension, SolidarityFund, total As Decimal
            health = (valueVacation * contract.Group.PayrollParameter.EmployeeHealthContributionPercentage) / 100
            pension = (valueVacation * contract.Group.PayrollParameter.EmployeePensionContributionPercentage) / 100

            If valueVacation > (contract.Group.PayrollParameter.LegalSalaryMinimum * 4) Then
                SolidarityFund = valueVacation * 0.01
            Else
                SolidarityFund = 0
            End If

            total = valueVacation - health - pension - SolidarityFund

            Dim requestDaysTmp = requestDays
            For Each vacationPeriod As VacationPeriod In employee.VacationPeriod.OrderBy(Function(x) x.InitialDatePeriod)
                If vacationPeriod.PendingDays > 0 Then
                    Dim takenDaysTmp As Integer
                    If requestDaysTmp > vacationPeriod.PendingDays Then
                        takenDaysTmp = vacationPeriod.PendingDays
                        vacationPeriod.TakenDays += takenDaysTmp
                        requestDaysTmp -= vacationPeriod.PendingDays
                        vacationPeriod.PendingDays = 0
                    Else
                        takenDaysTmp = requestDaysTmp
                        vacationPeriod.TakenDays += requestDaysTmp
                        vacationPeriod.PendingDays -= requestDaysTmp
                        requestDaysTmp = 0
                    End If
                    Dim vacation As Vacation = New Vacation()
                    vacation.VacationStartDate = initialDateVacation
                    vacation.VacationEndDate = endDate
                    vacation.TypeLiquidation = typeCalculate
                    vacation.TypeVacation = typeVacation
                    vacation.TypePayment = typePayment
                    If typePayment = 1 Then 'Inmediato
                        vacation.State = 2
                    ElseIf typePayment = 2 Then 'Proxima Nomina
                        vacation.State = 1
                        vacation.LiquidationDate = contract.Group.NextDateLiquidation
                    End If

                    vacation.TakenDays = takenDaysTmp
                    vacation.EnjoyDays = enjoyDays
                    vacation.IncorporationDate = incorporationDate
                    vacation.BaseLiquidation = valueBase
                    vacation.VacationValue = Utils.RoundedValuesByRate(valueVacation, contract.Group.PayrollParameter.AproximationValue)
                    vacation.HealthContribution = health
                    vacation.PensionContribution = pension
                    vacation.SolidarityFundValue = SolidarityFund
                    vacation.VacationValueNet = total
                    vacation.TakenDaysReal = takenDaysTmp
                    vacation.IncorporationDateReal = incorporationDate
                    vacation.StateIncorporation = 1 'Normal
                    vacation.PreviousMonthIBC = IBCLastPeriod
                    If vacationPeriod.ChangeTracker.State = ObjectState.Unchanged Then
                        vacationPeriod.MarkAsModified()
                    End If
                    vacationPeriod.Vacation.Add(vacation)
                    If requestDaysTmp = 0 Then
                        Exit For
                    End If
                End If
            Next
        Next
        actionResult.ObjectEmbbeded = employees
        Return actionResult

    End Function

    ''' <summary>
    ''' Verifica si un dias es valido para las vacaciones
    ''' </summary>
    ''' <param name="listHoliday">Lista de festivos</param>
    ''' <param name="saturdayBusinessDay">Trabaja sabado</param>
    ''' <param name="sundayBusinessDay">Trabaja Domingo</param>
    ''' <param name="dateValidation">Fecha</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function IsValidDay(listHoliday As List(Of Domain.Entities.Holiday), saturdayBusinessDay As Boolean, sundayBusinessDay As Boolean, dateValidation As Date) As Boolean Implements IVacationPeriodDomain.IsValidDay
        Dim isHoliday = listHoliday.FindAll(Function(x) x.Holiday1 = dateValidation).Count
        Dim isValid As Boolean = True
        If isHoliday = 1 Then ' Si es festivo
            isValid = False
        ElseIf saturdayBusinessDay = False And dateValidation.DayOfWeek = DayOfWeek.Saturday Then
            isValid = False
        ElseIf sundayBusinessDay = False And dateValidation.DayOfWeek = DayOfWeek.Sunday Then
            isValid = False
        End If

        Return isValid
    End Function


    ''' <summary>
    ''' Modifica las vacaciones de un emplado para hacerle el ingreso forzoso
    ''' </summary>
    ''' <param name="employee">Empleado con sus agregados</param>
    ''' <param name="initialDate">Fecha Inicial Vacaciones</param>
    ''' <param name="endDate">Fecha Final Vacaciones</param>
    ''' <param name="dateEntry">Fecha Ingreso Forzoso</param>
    ''' <param name="daysDifference">Dias diferencia entre ingreso forzoso y fecha final vacaciones</param>
    ''' <returns>Empleado con sus vacaciones modificadas</returns>
    ''' <remarks></remarks>
    Public Function ForceEntryVacationEmployee(employee As Employee, initialDate As Date, endDate As Date, dateEntry As Date, daysDifference As Integer, Optional ForceIngressResolutionNumber As String = Nothing,
                                               Optional ForceIngressResolutionDate As Date = Nothing, Optional incomeType As Integer = 0,
                                               Optional forceEntryResolutionDate As Date = Nothing, Optional contract As Domain.Payroll.Entities.Contract = Nothing) As Employee Implements IVacationPeriodDomain.ForceEntryVacationEmployee
        Dim index = employee.VacationPeriod.Count() - 1
        While index >= 0
            Dim itemVacationPeriod = employee.VacationPeriod(index)
            Dim listVacation = itemVacationPeriod.Vacation.Where(Function(x) x.VacationStartDate = initialDate And x.VacationEndDate = endDate)
            If listVacation.Count > 0 Then
                Dim ItemVacation = listVacation(0)
                If incomeType = 1 Then 'Si el tipo de ingreso es por interrupción
                    If daysDifference > ItemVacation.TakenDaysReal Then
                        itemVacationPeriod.PendingDays += ItemVacation.TakenDaysReal
                        itemVacationPeriod.TakenDays -= ItemVacation.TakenDaysReal
                        daysDifference -= ItemVacation.TakenDaysReal
                        ItemVacation.TakenDaysReal = 0
                    Else
                        itemVacationPeriod.PendingDays += daysDifference
                        itemVacationPeriod.TakenDays -= daysDifference
                        ItemVacation.TakenDaysReal -= daysDifference
                        daysDifference = 0
                        index = -1
                    End If
                End If
                ItemVacation.IncorporationDateReal = dateEntry

                If incomeType = 1 Then 'Si el tipo de ingreso es por interrupción
                    ItemVacation.StateIncorporation = 2
                    ItemVacation.State = 4
                ElseIf incomeType = 2 Then 'Si el tipo de ingreso es por aplazamiento
                    ItemVacation.State = 3
                    ItemVacation.StateIncorporation = 1
                    'Se disminuye un dia a la fecha de ingreso porque el dateDiff hace la diferencia en dias pero no cuenta apartir del mismo dia de ingreso si no que empieza a contar apartir del dia siguiente
                    forceEntryResolutionDate = DateAdd(DateInterval.Day, -1, forceEntryResolutionDate)
                    ItemVacation.DaysDeferredPending = DateDiff(DateInterval.Day, forceEntryResolutionDate, ItemVacation.VacationEndDate)
                End If
                ItemVacation.ForceEntryResolutionNumber = ForceIngressResolutionNumber
                ItemVacation.ForceEntryResolutionDate = ForceIngressResolutionDate
                itemVacationPeriod.MarkAsModified()
                ItemVacation.MarkAsModified()
            End If
            index -= 1
        End While
        employee.MarkAsModified()
        Return employee
    End Function

    ''' <summary>
    ''' Calcula los días habiles que haya entre dos fechas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CalculateSkillFullDays(IncomeDate As Date, VacationEndDate As Date, Contract As Domain.Payroll.Entities.Contract) As Integer
        'Días hábiles a retornar
        Dim skillFullDays As Integer = 0
        'Días festivos
        Dim holidays As List(Of Domain.Entities.Holiday) = _holidayRepository.ListHolidayBetweenDate(IncomeDate, VacationEndDate)

        If IncomeDate < VacationEndDate Then
            'Se recorre siempre y cuando las fechas sean diferentes
            While IncomeDate <= VacationEndDate
                If IsValidDay(holidays, Contract.Group.PayrollParameter.SaturdayBusinessDay, Contract.Group.PayrollParameter.SundayBusinessDay, IncomeDate) Then 'SI el día es válido
                    skillFullDays = skillFullDays + 1
                End If
                'Se aumenta un día a la fecha de ingreso
                IncomeDate = DateAdd(DateInterval.Day, 1, IncomeDate)
            End While
        End If
        'Retorno el valor
        Return skillFullDays
    End Function

    ''' <summary>
    ''' Crea una lista de detalles de calendario
    ''' </summary>
    ''' <param name="initialDate">Fecha Inicial</param>
    ''' <param name="endDate">Fecha Final</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CreateListScheduleDetailBetweenDate(employee As Employee, contractValid As Contract, initialDate As Date, endDate As Date) As List(Of ScheduleDetail) Implements IVacationPeriodDomain.CreateListScheduleDetailBetweenDate
        Dim runDate As Date = initialDate
        Dim listScheduleDetail As New List(Of ScheduleDetail)()
        While runDate <= endDate
            Dim detail As New ScheduleDetail()
            detail.GroupId = contractValid.GroupId
            detail.EmployeeId = contractValid.EmployeeId
            detail.ContractId = contractValid.Id
            detail.CompanyId = contractValid.FunctionalUnit.BranchOffice.CompanyId
            detail.BranchOfficeId = contractValid.FunctionalUnit.BranchOfficeId
            detail.FunctionalUnitId = contractValid.FunctionalUnitId
            detail.CenterCostId = employee.CostCenterId
            detail.ScheduleFunctionalUnitId = contractValid.FunctionalUnitId
            detail.Letter = "V"
            detail.DateDetail = runDate
            detail.TotalNumberHours = 0
            detail.ScheduleTemplateId = Nothing
            detail.Status = 0
            detail.State = True
            listScheduleDetail.Add(detail)
            runDate = runDate.AddDays(1)
        End While
        Return listScheduleDetail
    End Function

    ''' <summary>
    ''' Calcula el valor de las vacaciones de los empleados y consume los periodos que necesita para salir a vacaciones
    ''' </summary>
    ''' <param name="employees">Lista de empleados</param>
    ''' <param name="requestDays">Dias Solicitados</param>
    ''' <param name="typeCalculate">Tipo de calculo 1 - Promedio 2 - Sueldo Basico </param>
    ''' <param name="typeVacation">Tipo de vacaciones 1 - Liquuidar 2 - Disfrutar 3 - Permiso con cargo a vacaciones</param>
    ''' <param name="typePayment">Tipo de pago  1 - Inmedito 2 - Proxima Nomina</param>
    ''' <param name="initialDateVacation">Fecha Inicial Vacaciones</param>
    ''' <returns>ActionMessageResult</returns>
    ''' <remarks></remarks>
    Public Function CalculateValueVacationByFormulates(employees As List(Of Employee), requestDays As Integer, typeCalculate As Byte, typeVacation As Byte, typePayment As Byte, initialDateVacation As Date, Indigo As SessionValues, Optional HUN As Boolean = False) As ActionMessageResult(Of List(Of Employee)) Implements IVacationPeriodDomain.CalculateValueVacationByFormulates

        Dim IncentivePaymentVacationValue As Double = 0
        Dim BonificationValue As Double = 0
        Dim VacationValue As Double = 0
        Dim VacationIncrease As Double = 0
        Dim PaidValueAverageIncentiveServicesValue As Double
        Dim BonificationYearValue As Double = 0
        Dim VacationPlusDays As Integer = 0
        Dim enjoyDaysPensionHealth As Integer = 0

        Dim actionResult As ActionMessageResult(Of List(Of Employee)) = New ActionMessageResult(Of List(Of Employee))()
        actionResult.StateResult = True

        Try

            If employees Is Nothing OrElse employees.Count = 0 Then 'Se valida que al menos haya seleccionado un empleado
                actionResult.StateResult = False
                actionResult.MessageResult.Add(New MessageResult("DONTAPPLY", "Debe seleccionar al menos un empleado"))
                Return actionResult
            End If
            Dim Contract = employees(0).Contract.ToList().Find(Function(x) x.Valid = True)

            'Cargo los Parámetros de Nómina
            Dim PayrollSettings = _PayrollSettings.GetSettingPayroll()

            If PayrollSettings Is Nothing Then
                actionResult.StateResult = False
                actionResult.MessageResult.Add(New MessageResult("V003", "No hay Parámetros de Nómina Creados"))
                Return actionResult
            End If

            'Creamos las Variables de las Fórmulas
            Dim VacationFormula As String
            Dim BonificationFormula As String
            Dim VacationalIncreaseFormula As String
            Dim VacationalIncentiveFormula As String


            If PayrollSettings.VacationFormulates <> "0" Or PayrollSettings.VacationFormulates IsNot Nothing Then
                VacationFormula = PayrollSettings.VacationFormulates
            End If

            If PayrollSettings.BonificationVacationFormulates <> "0" Or PayrollSettings.BonificationVacationFormulates IsNot Nothing Then
                BonificationFormula = PayrollSettings.BonificationVacationFormulates
            End If

            If PayrollSettings.VacationalIncreaseFormulates <> "0" Or PayrollSettings.VacationalIncreaseFormulates IsNot Nothing Then
                VacationalIncreaseFormula = PayrollSettings.VacationalIncreaseFormulates
            End If

            If PayrollSettings.VacationIncentiveFormulates <> "0" Or PayrollSettings.VacationIncentiveFormulates IsNot Nothing Then
                VacationalIncentiveFormula = PayrollSettings.VacationIncentiveFormulates
            End If

            If HUN = True Then

                If Contract.ContractType.JobBondingType.Code = "005" Then

                    Dim YearsDifference As Integer = DateDiff(DateInterval.Month, Contract.JobBondingDate, initialDateVacation)

                    YearsDifference = YearsDifference / 12

                    If YearsDifference >= 5 And YearsDifference <= 10 Then
                        VacationPlusDays = 2
                    End If

                    If YearsDifference >= 11 And YearsDifference <= 15 Then
                        VacationPlusDays = 3
                    End If

                    If YearsDifference >= 16 And YearsDifference <= 20 Then
                        VacationPlusDays = 4
                    End If

                    If YearsDifference >= 21 Then
                        VacationPlusDays = 5
                    End If
                End If
            End If

            Dim PaidHealthDays As Integer = 0

            Dim endDate As Date
            Dim incorporationDate As Date
            Dim enjoyDays As Integer = 0
            requestDays = requestDays + VacationPlusDays
            Dim paidDays As Integer = 0
            Dim DayNotCount As Integer = 0
            employees(0).MarkAsAdded()
            Dim index As Integer = 1
            Dim payrollParameter = employees(0).Contract.ToList().Find(Function(x) x.Valid = True).Group.PayrollParameter
            If typeVacation = 2 Or typeVacation = 4 Then ' Es disfrutar
                Dim holidays As List(Of Domain.Entities.Holiday) = _holidayRepository.ListHolidayBetweenDate(initialDateVacation, initialDateVacation.AddMonths(6))
                endDate = initialDateVacation.AddDays(-1)
                While index <= requestDays 'Calculo la fecha final de vacaciones
                    endDate = endDate.AddDays(1)

                    If HUN = False Then
                        If IsValidDay(holidays, payrollParameter.SaturdayBusinessDay, payrollParameter.SundayBusinessDay, endDate) Then ' Si no es un festivo ni un domingo aumenta el indice
                            index += 1
                        End If
                    Else
                        If IsValidDay(holidays, False, False, endDate) Then ' Si no es un festivo ni un domingo aumenta el indice
                            index += 1
                        End If
                    End If
                    enjoyDays += 1
                    enjoyDaysPensionHealth = enjoyDays
                    PaidHealthDays = enjoyDays

                    If payrollParameter.Day31 = False Then
                        If Day(endDate) = 31 Then
                            DayNotCount += 1
                        End If
                    Else
                        DayNotCount = 0
                    End If

                    If Day(endDate) = 31 Then
                        enjoyDaysPensionHealth = enjoyDaysPensionHealth - 1
                        PaidHealthDays = PaidHealthDays - 1
                    End If


                End While

                incorporationDate = endDate.AddDays(1)
                If HUN = True Then
                    While IsValidDay(holidays, payrollParameter.SaturdayBusinessDay, payrollParameter.SundayBusinessDay, incorporationDate) = False 'Calculo la fecha de incorporacion
                        enjoyDays += 1
                        incorporationDate = incorporationDate.AddDays(1)
                    End While
                Else
                    If PayrollSettings.WeekendVacationPaid = 2 Then
                        While IsValidDay(holidays, payrollParameter.SaturdayBusinessDay, payrollParameter.SundayBusinessDay, incorporationDate) = False 'Calculo la fecha de incorporacion
                            enjoyDays += 1
                            PaidHealthDays = PaidHealthDays + 1

                            If incorporationDate.Day = 31 Then
                                PaidHealthDays = PaidHealthDays - 1
                            End If

                            incorporationDate = incorporationDate.AddDays(1)
                        End While
                    End If
                End If


                enjoyDays = enjoyDays - DayNotCount
                paidDays = enjoyDays

            Else ' Si eligio liquidar o permiso a vacaciones
                If typeVacation = 3 Then 'Permiso a vacaciones
                    endDate = initialDateVacation.AddDays(requestDays - 1)
                End If
                enjoyDays = requestDays
                paidDays = requestDays
            End If
            For Each employee As Employee In employees 'Recorro los empleados que hay que liquidar

                Contract = employee.Contract.ToList().Find(Function(x) x.Valid = True)

                ' IBC del mes anterior para PreviousMonthIBC en vacaciones
                Dim IBCLastPeriodVac As Nullable(Of Decimal) = Nothing
                Dim listPrevLiqVac = _liquidationRepository.LiquidationLastMonths(Contract.InitialContractNumber, 1, Contract.Group.NextDateLiquidation)
                If listPrevLiqVac IsNot Nothing AndAlso listPrevLiqVac.Count > 0 Then
                    Dim prevLiqVac = listPrevLiqVac.Where(Function(x) x.RegisterStatus = "C").OrderByDescending(Function(x) x.PayrollDateLiquidated).FirstOrDefault()
                    If prevLiqVac IsNot Nothing Then
                        IBCLastPeriodVac = prevLiqVac.PeriodJCB
                    End If
                End If

                'Variables para Fórmulas:
                Dim BasicSalary = Contract.BasicSalary
                Dim LegalSalaryMinimun = Contract.Group.PayrollParameter.LegalSalaryMinimum
                Dim TransportHealthValue = Contract.Group.PayrollParameter.TransportHelpValue
                Dim PayrollDays = 30
                Dim RepresentationCost As Double = 0
                Dim SalaryPromedio As Double = 0

                If BasicSalary > (2 * LegalSalaryMinimun) Then
                    TransportHealthValue = 0
                End If


                Dim ListPositionEmployee = _PositionRepository.ListAllPosition()

                'Busco si el Empleado se le paga Gastos de Representación
                Dim ObjPositionEmployee As Position

                If ListPositionEmployee IsNot Nothing And ListPositionEmployee.Count > 0 Then
                    ObjPositionEmployee = ListPositionEmployee.Where(Function(x) x.Id = Contract.PositionId).FirstOrDefault()
                End If

                If ObjPositionEmployee IsNot Nothing Then
                    If ObjPositionEmployee.RepresentationCost = True Then
                        RepresentationCost = Contract.BasicSalary * 0.135
                    End If
                End If

                Dim ObjRetroactiveEmployee = _retroactiveRepository.GetListRetroactiveByEmployeeId(initialDateVacation.Year, Contract.EmployeeId)

                If ObjRetroactiveEmployee Is Nothing Then
                    ObjRetroactiveEmployee = _retroactiveRepository.GetListRetroactiveByEmployeeId((initialDateVacation.Year) - 1, Contract.EmployeeId)
                End If

                Dim ListPaidValueAverageIncentiveServices = _incentivePaymentRepository.GetIncentivePaymentByEmployeeIdLastLiquidation(Contract.EmployeeId)
                Dim datePeriod As Date = New Date(Contract.Group.NextDateLiquidation.Year - 1, Contract.Group.NextDateLiquidation.Month, 1)
                Dim ObjPaidValueAverageIncentiveServices = ListPaidValueAverageIncentiveServices.Where(Function(x) x.Period = 1 And x.PeriodEndDate >= datePeriod).FirstOrDefault()

                'Buscamos el objeto de Primas de Servicios
                If ObjPaidValueAverageIncentiveServices IsNot Nothing Then
                    PaidValueAverageIncentiveServicesValue = ObjPaidValueAverageIncentiveServices.TotalAccrued

                    If ObjRetroactiveEmployee IsNot Nothing Then

                        'Si toca buscar el retroactivo, busco el dato del Concepto
                        Dim ListStringConceptClass As New List(Of String)
                        ListStringConceptClass.Add("002")
                        Dim Concept = _conceptRepository.GetConceptByConceptClass(ListStringConceptClass).FirstOrDefault()

                        If Concept IsNot Nothing Then
                            Dim ObjConceptRetroactive = ObjRetroactiveEmployee.RetroactiveD.Where(Function(x) x.IdConcept = Concept.Id).FirstOrDefault()
                            If ObjConceptRetroactive IsNot Nothing Then
                                PaidValueAverageIncentiveServicesValue = ObjConceptRetroactive.ValueConceptWithRetroactive + PaidValueAverageIncentiveServicesValue
                            End If
                        End If
                    End If

                End If


                'Buscamos el objeto de Bonificaciones
                Dim ObjConceptClassBonificationValue = _liquidationRepository.GetLastConceptClassListDate(IIf(Contract.InitialContractNumber = 0, Contract.Id, Contract.InitialContractNumber), "055", datePeriod).ToList()

                If ObjConceptClassBonificationValue Is Nothing Or ObjConceptClassBonificationValue.Count <= 0 Then
                    ObjConceptClassBonificationValue = _liquidationRepository.GetLastConceptClassListDate(IIf(Contract.InitialContractNumber = 0, Contract.Id, Contract.InitialContractNumber), "046", datePeriod).ToList()
                End If

                If ObjConceptClassBonificationValue IsNot Nothing AndAlso ObjConceptClassBonificationValue.Count > 0 Then

                    Dim ObjConceptBonification = ObjConceptClassBonificationValue.Where(Function(x) x.Liquidation.RegisterStatus = "C").FirstOrDefault()

                    If ObjConceptClassBonificationValue.Any(Function(x) x.Liquidation.RegisterStatus = " " And x.PayrollDate.Month = Contract.Group.NextDateLiquidation.Month And x.PayrollDate.Year = Contract.Group.NextDateLiquidation.Year) Then
                        ObjConceptBonification = ObjConceptClassBonificationValue.Where(Function(x) x.Liquidation.RegisterStatus = " " And x.PayrollDate.Month = Contract.Group.NextDateLiquidation.Month And x.PayrollDate.Year = Contract.Group.NextDateLiquidation.Year).FirstOrDefault()
                    End If

                    If ObjConceptBonification Is Nothing Then
                        ObjConceptBonification = ObjConceptClassBonificationValue.FirstOrDefault()
                    End If

                    Dim DatePaidBonification = ObjConceptBonification.PayrollDate

                    If Contract.JobBondingDate.Month = Contract.Group.NextDateLiquidation.Month And Indigo.IndigoCompanyType = 2 And HUN = False Then
                        Dim ListEmployee = New List(Of Employee)
                        ListEmployee.Add(Contract.Employee)
                        Dim objLiquidationTemp = _liquidationDomain.NewExecuteLiquitadion(ListEmployee, Contract.Group, False, Indigo, New Date(1, 1, 1), False)
                        If objLiquidationTemp.StateResult = True Then
                            If objLiquidationTemp.ObjectEmbbeded IsNot Nothing Then
                                Dim tmpLiquidation = objLiquidationTemp.ObjectEmbbeded.FirstOrDefault()
                                If tmpLiquidation.LiquidationDetail.Any(Function(x) x.ConceptClass = "046") Then
                                    ObjConceptBonification = tmpLiquidation.LiquidationDetail.Where(Function(x) x.ConceptClass = "046").FirstOrDefault()
                                End If
                            End If

                        End If

                    End If

                    Dim BonificationConceptId As Integer

                    BonificationYearValue = ObjConceptBonification.ConceptTotalValue
                    BonificationConceptId = ObjConceptBonification.ConceptId

                    If ObjRetroactiveEmployee IsNot Nothing AndAlso DatePaidBonification.Date < ObjRetroactiveEmployee.NextPayrollDate Then
                        Dim ObjConceptRetroactive = ObjRetroactiveEmployee.RetroactiveD.Where(Function(x) x.IdConcept = BonificationConceptId).FirstOrDefault()
                        If ObjConceptRetroactive IsNot Nothing Then
                            BonificationYearValue = ObjConceptRetroactive.ValueConceptWithRetroactive + BonificationYearValue
                        End If
                    End If

                    'End If
                End If


                Dim pendingDays As Integer = 0
                employee.VacationPeriod.ToList().ForEach(Sub(x)
                                                             pendingDays += x.PendingDays
                                                         End Sub)
                employee.RemainingVacationDays = pendingDays - requestDays
                If typeVacation = 2 Then 'Disfrutar
                    Dim vacationConflict As Vacation = Nothing
                    employee.VacationPeriod.ToList.ForEach(Sub(x)
                                                               Dim hit = x.Vacation.FirstOrDefault(Function(v) (initialDateVacation >= v.VacationStartDate And initialDateVacation < If(v.ForceEntryResolutionDate Is Nothing, v.VacationEndDate, v.ForceEntryResolutionDate.Value)) _
                                                                                Or (endDate >= v.VacationStartDate And endDate < If(v.ForceEntryResolutionDate Is Nothing, v.VacationEndDate, v.ForceEntryResolutionDate.Value)))
                                                               If hit IsNot Nothing AndAlso vacationConflict Is Nothing Then
                                                                   vacationConflict = hit
                                                                   Exit Sub
                                                               End If
                                                           End Sub)
                    If vacationConflict IsNot Nothing Then 'El empleado esta en vacaciones
                        actionResult.StateResult = False
                        actionResult.MessageResult.Add(New MessageResult("-003", employee.ThirdParty.Name, vacationConflict.VacationStartDate, vacationConflict.VacationEndDate))
                        Continue For
                    End If
                End If
                If pendingDays = 0 Then 'No posee dias pendientes
                    actionResult.StateResult = False
                    actionResult.MessageResult.Add(New MessageResult("-002", employee.ThirdParty.Name))
                    Continue For
                ElseIf (requestDays - VacationPlusDays) > pendingDays Then 'La cantidad de dias solicitada es mayor a la disponible
                    actionResult.StateResult = False
                    actionResult.MessageResult.Add(New MessageResult("-001", employee.ThirdParty.Name, pendingDays))
                    Continue For
                End If

                Dim valueBase As Decimal
                Dim valueVacation As Decimal
                Dim valueBaseDays As Decimal
                If typeCalculate = 1 Then 'Calculo el Promedio
                    Dim dateIndex = Date.Now().AddDays(-Date.Now().Day + 1)
                    Dim valueTotalLiquidation As Decimal = 0
                    Dim monthsAverage As Integer = 0
                    Dim valueSalary As Decimal = Contract.BasicSalary
                    For index = 1 To Contract.Group.PayrollParameter.AverageMonthVacation Step 1

                        Dim listLiquidation = _liquidationDetailRepository.LiquidationDetailBaseVacationByEmployeeDate(Contract.EmployeeId, dateIndex.AddMonths(-1), dateIndex.AddDays(-1))
                        Dim valueLiquidation As Decimal = 0
                        listLiquidation.ForEach(Sub(x)
                                                    If x.AccruedValue IsNot Nothing = True Then
                                                        valueLiquidation += x.AccruedValue.Value
                                                    End If
                                                End Sub)
                        If valueLiquidation > 0 Then
                            valueTotalLiquidation += valueLiquidation

                        End If
                        'Se coloca la linea que se comento anteriormente aqui, por la explicación de Guerra que siempre se debe de dividir el valor por 12 meses
                        monthsAverage += 1
                        dateIndex = dateIndex.AddMonths(-1)
                    Next
                    If monthsAverage = 0 Then
                        valueBase = 0
                    Else
                        valueBase = (valueTotalLiquidation / monthsAverage) + valueSalary
                        SalaryPromedio = valueBase
                    End If
                Else 'Sueldo Basico
                    valueBase = Contract.BasicSalary
                    SalaryPromedio = valueBase
                End If
                valueBaseDays = valueBase / 30

                Dim VartypeVacation As Boolean = False
                If typeVacation = 1 Then
                    VartypeVacation = True
                End If

                'Cargo las Variables Nuevas requeridas por Medilaser que son los Promedios de Sueldos, Horas Extras, Recargos Nocturnos Normales, Recargos Nocturnos Festivos y Recargos Dominicales

                Dim InitialMonth As Date = Contract.Group.NextDateLiquidation
                Dim CountMonth As Integer = 0

                If VartypeVacation Then
                    If Contract.Group.PayrollParameter.AverageMonthVacationCompensation Is Nothing Or Contract.Group.PayrollParameter.AverageMonthVacationCompensation <= 0 Then
                        actionResult.StateResult = False
                        actionResult.MessageResult.Add(New MessageResult("DONTAPPLY", "Las vacaciones son de Tipo Compensación, pero en el Formulario de Grupos, los Meses Promedio de Meses Compensadas están en cero"))
                        Return actionResult
                    End If
                    CountMonth = Contract.Group.PayrollParameter.AverageMonthVacationCompensation
                    InitialMonth = InitialMonth.AddMonths(Contract.Group.PayrollParameter.AverageMonthVacationCompensation * -1)
                Else

                    If Contract.Group.PayrollParameter.AverageMonthVacation <= 0 Then
                        actionResult.StateResult = False
                        actionResult.MessageResult.Add(New MessageResult("DONTAPPLY", "Las vacaciones son de Tipo Disfrute, pero en el Formulario de Grupos, los Meses Promedio de Disfrute están en cero"))
                        Return actionResult
                    End If
                    CountMonth = Contract.Group.PayrollParameter.AverageMonthVacation
                    InitialMonth = InitialMonth.AddMonths(Contract.Group.PayrollParameter.AverageMonthVacation * -1)
                End If


                Dim ContractSalaryPromedy As Decimal = 0
                Dim NormalEveningRechargePromedy As Decimal = 0
                Dim Normal_EveningRechargePromedy As Decimal = 0
                Dim FestiveEveningRechargePromedy As Decimal = 0
                Dim SundayRechargePromedy As Decimal = 0
                Dim ExtraHoursPromedy As Decimal = 0
                Dim BonificationSalaryPromedy As Decimal = 0
                Dim VacationSalaryPromedy As Decimal = 0

                Dim ContractSalaryList = _liquidationRepository.GetLastConceptClassListDateBetween(Contract.InitialContractNumber, "005", InitialMonth, Date.Now)
                ContractSalaryPromedy = CalcValueAcumulatedLiquidationDetail(ContractSalaryList, CountMonth)

                Dim ContractNormalEveningList = _liquidationRepository.GetLastConceptClassListDateBetween(Contract.InitialContractNumber, "042", InitialMonth, Date.Now)
                NormalEveningRechargePromedy = CalcValueAcumulatedLiquidationDetail(ContractNormalEveningList, CountMonth)

                Dim Contract_NormalEveningList = _liquidationRepository.GetLastConceptClassListDateBetween(Contract.InitialContractNumber, "042", InitialMonth.AddMonths(1), Date.Now)
                Normal_EveningRechargePromedy = CalcValueAcumulatedLiquidationDetail(Contract_NormalEveningList, CountMonth)

                Dim ContractFestiveEveningList = _liquidationRepository.GetLastConceptClassListDateBetween(Contract.InitialContractNumber, "043", InitialMonth, Date.Now)
                FestiveEveningRechargePromedy = CalcValueAcumulatedLiquidationDetail(ContractFestiveEveningList, CountMonth)

                Dim SundayRechargeList = _liquidationRepository.GetLastListConceptClassListDateBetween(Contract.InitialContractNumber, New List(Of String)({"051", "052"}), InitialMonth, Date.Now)
                SundayRechargePromedy = CalcValueAcumulatedLiquidationDetail(SundayRechargeList, CountMonth)

                Dim ExtraHoursList = _liquidationRepository.GetLastListConceptClassListDateBetween(Contract.InitialContractNumber, New List(Of String)({"001", "012", "013", "050"}), InitialMonth, Date.Now)
                ExtraHoursPromedy = CalcValueAcumulatedLiquidationDetail(ExtraHoursList, CountMonth)

                Dim SalaryBonification = _liquidationRepository.GetLastConceptClassListDateBetween(Contract.InitialContractNumber, "055", InitialMonth, Date.Now)
                BonificationSalaryPromedy = CalcValueAcumulatedLiquidationDetail(SalaryBonification, CountMonth)

                If VacationFormula.Contains("Sueldo Promedio Contrato") Then
                    valueBase = ContractSalaryPromedy
                End If

                If VacationFormula.Contains("Sueldo Contrato") Then
                    valueBase = BasicSalary
                End If

                If VacationFormula.Contains("Promedio Recargos Nocturnos Normales") Then
                    valueBase = valueBase + NormalEveningRechargePromedy
                End If

                If VacationFormula.Contains("Recargos Promedio Nocturnos N") Then
                    valueBase = valueBase + Normal_EveningRechargePromedy
                End If

                If VacationFormula.Contains("Promedio Recargos Nocturnos Festivos") Then
                    valueBase = valueBase + FestiveEveningRechargePromedy
                End If

                If VacationFormula.Contains("Promedio Recargos Dominicales") Then
                    valueBase = valueBase + SundayRechargePromedy
                End If

                If VacationFormula.Contains("Promedio Horas Extras") Then
                    valueBase = valueBase + ExtraHoursPromedy
                End If

                If VacationFormula.Contains("Promedio Bonificaciones Salarias") Then
                    valueBase = valueBase + BonificationSalaryPromedy
                End If

                If VacationFormula.Contains("Promedio Salario Variable Vacaciones") Then
                    Dim DateInitialMonth = Contract.Group.NextDateLiquidation.AddMonths(-Contract.Group.PayrollParameter.AverageMonthVacationCompensation + 1)
                    Dim DateEndMonth = Contract.Group.NextDateLiquidation
                    Dim VacationSalaryList = _liquidationDetailRepository.LiquidationDetailBaseVacation(Contract.InitialContractNumber, New List(Of String)({"001", "012", "013", "042", "043", "050", "051", "052"}), DateInitialMonth.AddMonths(1).AddDays(-1), DateEndMonth.AddMonths(1).AddDays(-1))
                    VacationSalaryPromedy = CalcValueAcumulatedLiquidationDetail(VacationSalaryList, CountMonth)
                    valueBase = valueBase + VacationSalaryPromedy
                End If

                Dim FormulaMessage As String = String.Empty
                Dim ErrorFormula As Boolean = False
                Dim ReplaceFormulateVacation As String = String.Empty

                'Reemplazo las Fórmulas con las Variables
                valueVacation = ReplaceDataFormulates(VacationFormula, BasicSalary, PayrollDays, enjoyDays, LegalSalaryMinimun, RepresentationCost, BonificationYearValue, TransportHealthValue, PaidValueAverageIncentiveServicesValue, paidDays, SalaryPromedio, VartypeVacation, ContractSalaryPromedy, NormalEveningRechargePromedy, FestiveEveningRechargePromedy, SundayRechargePromedy, BonificationSalaryPromedy, ExtraHoursPromedy, VacationSalaryPromedy, ErrorFormula, FormulaMessage, ReplaceFormulateVacation)

                If ErrorFormula = True Then
                    actionResult.StateResult = False
                    actionResult.MessageResult.Add(New MessageResult("DONTAPPLY", "VACACIONES: " + FormulaMessage))
                    Return actionResult
                End If


                Dim ReplaceFormulateIncrease As String = String.Empty
                VacationIncrease = ReplaceDataFormulates(VacationalIncreaseFormula, BasicSalary, PayrollDays, enjoyDays, LegalSalaryMinimun, RepresentationCost, BonificationYearValue, TransportHealthValue, PaidValueAverageIncentiveServicesValue, paidDays, SalaryPromedio, VartypeVacation, ContractSalaryPromedy, NormalEveningRechargePromedy, FestiveEveningRechargePromedy, SundayRechargePromedy, BonificationSalaryPromedy, ExtraHoursPromedy, VacationSalaryPromedy, ErrorFormula, FormulaMessage, ReplaceFormulateIncrease)

                If ErrorFormula = True Then
                    actionResult.StateResult = False
                    actionResult.MessageResult.Add(New MessageResult("DONTAPPLY", "INCREMENTO VACACIONAL: " + FormulaMessage))
                    Return actionResult
                End If

                Dim ReplaceFormulateBonification As String = String.Empty
                BonificationValue = ReplaceDataFormulates(BonificationFormula, BasicSalary, PayrollDays, enjoyDays, LegalSalaryMinimun, RepresentationCost, BonificationYearValue, TransportHealthValue, PaidValueAverageIncentiveServicesValue, paidDays, SalaryPromedio, VartypeVacation, ContractSalaryPromedy, NormalEveningRechargePromedy, FestiveEveningRechargePromedy, SundayRechargePromedy, BonificationSalaryPromedy, ExtraHoursPromedy, VacationSalaryPromedy, ErrorFormula, FormulaMessage, ReplaceFormulateBonification)

                If ErrorFormula = True Then
                    actionResult.StateResult = False
                    actionResult.MessageResult.Add(New MessageResult("DONTAPPLY", "BONIFICACIÓN ESPECIAL RECREACIÓN: " + FormulaMessage))
                    Return actionResult
                End If

                Dim ReplaceFormulateIncentive As String = String.Empty
                IncentivePaymentVacationValue = ReplaceDataFormulates(VacationalIncentiveFormula, BasicSalary, PayrollDays, enjoyDays, LegalSalaryMinimun, RepresentationCost, BonificationYearValue, TransportHealthValue, PaidValueAverageIncentiveServicesValue, paidDays, SalaryPromedio, VartypeVacation, ContractSalaryPromedy, NormalEveningRechargePromedy, FestiveEveningRechargePromedy, SundayRechargePromedy, BonificationSalaryPromedy, ExtraHoursPromedy, VacationSalaryPromedy, ErrorFormula, FormulaMessage, ReplaceFormulateIncentive)

                If ErrorFormula = True Then
                    actionResult.StateResult = False
                    actionResult.MessageResult.Add(New MessageResult("DONTAPPLY", "PRIMA DE VACACIONES: " + FormulaMessage))
                    Return actionResult
                End If


                If requestDays >= 30 And requestDays < 45 Then
                    IncentivePaymentVacationValue = IncentivePaymentVacationValue * 2
                    BonificationValue = BonificationValue * 2
                End If

                Dim health, pension, SolidarityFund, total As Decimal

                Dim HealthFormulate As String
                Dim PensionFormulate As String
                Dim PensionSolidarityFormulate As String

                Dim ReplaceHealthFormulate As String
                Dim ReplacePensionFormulate As String
                Dim ReplacePensionSolidarityFormulate As String

                Dim SalaryType = Contract.ContractType.SalaryType
                Dim BaseHealth As Decimal = 0
                Dim BaseSolidarityFund As Decimal = If(SalaryType = 2, valueVacation * 0.7D, valueVacation)

                If SalaryType <> 2 Then
                    health = (valueVacation * Contract.Group.PayrollParameter.EmployeeHealthContributionPercentage) / 100
                    HealthFormulate = "(valueVacation * Contract.Group.PayrollParameter.EmployeeHealthContributionPercentage) / 100"
                    ReplaceHealthFormulate = "(" + valueVacation.ToString() + " * " + Contract.Group.PayrollParameter.EmployeeHealthContributionPercentage.ToString() + ") / 100"

                    pension = (valueVacation * Contract.Group.PayrollParameter.EmployeePensionContributionPercentage) / 100
                    PensionFormulate = "(valueVacation * Contract.Group.PayrollParameter.EmployeePensionContributionPercentage) / 100"
                    ReplacePensionFormulate = "(" + valueVacation.ToString() + " * " + Contract.Group.PayrollParameter.EmployeePensionContributionPercentage.ToString() + ") / 100"
                Else
                    BaseHealth = valueVacation

                    If Contract.BasicSalary >= (Contract.Group.PayrollParameter.HealthContributionMaximunSalary * Contract.Group.PayrollParameter.LegalSalaryMinimum) Then
                        BaseHealth = Contract.Group.PayrollParameter.LegalSalaryMinimum * Contract.Group.PayrollParameter.HealthContributionMaximunSalary
                        BaseHealth = (BaseHealth * PaidHealthDays) / 30
                        health = ((BaseHealth) * Contract.Group.PayrollParameter.EmployeeHealthContributionPercentage) / 100
                        pension = ((BaseHealth) * Contract.Group.PayrollParameter.EmployeePensionContributionPercentage) / 100
                    Else
                        health = ((BaseHealth * 0.7) * Contract.Group.PayrollParameter.EmployeeHealthContributionPercentage) / 100
                        pension = ((BaseHealth * 0.7) * Contract.Group.PayrollParameter.EmployeePensionContributionPercentage) / 100
                    End If

                    HealthFormulate = "((valueVacation * 0.7) * Contract.Group.PayrollParameter.EmployeeHealthContributionPercentage) / 100"
                    ReplaceHealthFormulate = "((" + BaseHealth.ToString() + " * 0.7) * " + Contract.Group.PayrollParameter.EmployeeHealthContributionPercentage.ToString() + ") / 100"

                    PensionFormulate = "((valueVacation * 0.7) * Contract.Group.PayrollParameter.EmployeePensionContributionPercentage) / 100"
                    ReplacePensionFormulate = "((" + BaseHealth.ToString() + " * 0.7) * " + Contract.Group.PayrollParameter.EmployeePensionContributionPercentage.ToString() + ") / 100"
                End If

                Dim SMLMV As Decimal = Contract.Group.PayrollParameter.LegalSalaryMinimum

                If valueBase > SMLMV * 20 Then
                    SolidarityFund = Math.Ceiling(BaseSolidarityFund * 0.02 / 100) * 100
                    PensionSolidarityFormulate = "Ceiling(valueVacation * 0.02 / 100) * 100"
                    ReplacePensionSolidarityFormulate = "Ceiling(" + valueVacation.ToString() + " * 0.02 / 100) * 100"
                ElseIf valueBase > SMLMV * 19 Then
                    SolidarityFund = Math.Ceiling(BaseSolidarityFund * 0.018 / 100) * 100
                    PensionSolidarityFormulate = "Ceiling(valueVacation * 0.018 / 100) * 100"
                    ReplacePensionSolidarityFormulate = "Ceiling(" + valueVacation.ToString() + " * 0.018 / 100) * 100"
                ElseIf valueBase > SMLMV * 18 Then
                    SolidarityFund = Math.Ceiling(BaseSolidarityFund * 0.016 / 100) * 100
                    PensionSolidarityFormulate = "Ceiling(valueVacation * 0.016 / 100) * 100"
                    ReplacePensionSolidarityFormulate = "Ceiling(" + valueVacation.ToString() + " * 0.016 / 100) * 100"
                ElseIf valueBase > SMLMV * 17 Then
                    SolidarityFund = Math.Ceiling(BaseSolidarityFund * 0.014 / 100) * 100
                    PensionSolidarityFormulate = "Ceiling(valueVacation * 0.014 / 100) * 100"
                    ReplacePensionSolidarityFormulate = "Ceiling(" + valueVacation.ToString() + " * 0.014 / 100) * 100"
                ElseIf valueBase > SMLMV * 16 Then
                    SolidarityFund = Math.Ceiling(BaseSolidarityFund * 0.012 / 100) * 100
                    PensionSolidarityFormulate = "Ceiling(valueVacation * 0.012 / 100) * 100"
                    ReplacePensionSolidarityFormulate = "Ceiling(" + valueVacation.ToString() + " * 0.012 / 100) * 100"
                ElseIf valueBase > SMLMV * 4 Then
                    SolidarityFund = Math.Ceiling(BaseSolidarityFund * 0.01 / 100) * 100
                    PensionSolidarityFormulate = "Ceiling(valueVacation * 0.01 / 100) * 100"
                    ReplacePensionSolidarityFormulate = "Ceiling(" + valueVacation.ToString() + " * 0.01 / 100) * 100"
                Else
                    SolidarityFund = 0
                    PensionSolidarityFormulate = "0"
                    ReplacePensionSolidarityFormulate = "0"
                End If

                Dim TarifaAprox As Integer = If(Contract?.Group?.PayrollParameter?.AproximationValue Is Nothing, 0, Contract?.Group?.PayrollParameter?.AproximationValue)

                health = Math.Ceiling(health / 100) * 100
                pension = Math.Ceiling(pension / 100) * 100

                'Calcula la Salud y Pensión de Acuerdo al IBC del Mes Anterior
                If PayrollSettings.HealthPensionIBCVacation = 2 Then
                    Dim valorBase As Decimal = 0

                    Dim listLiquidation As List(Of Liquidation) = _liquidationRepository.LiquidationLastMonths(Contract.InitialContractNumber, 1, Contract.Group.NextDateLiquidation)
                    valorBase = _noveltyDomain.CalculateAverageIBCLiquidationLastMonth(listLiquidation, 1, SalaryType)

                    If valorBase = 0 Then
                        valorBase = Contract.Group.PayrollParameter.LegalSalaryMinimum
                    End If

                    Dim ValorBaseCalcularMesAnterior = 0
                    Dim ListTmpVacation As New List(Of Vacation)

                    Dim LastMonthInitialDate As Date
                    Dim LastMonthEndDate As Date

                    If typeVacation <> 1 AndAlso employee.VacationPeriod IsNot Nothing AndAlso employee.VacationPeriod.Count > 0 Then

                        For Each objVacationPeriod As VacationPeriod In employee.VacationPeriod

                            Dim ThisMonth = New Date(initialDateVacation.Year, initialDateVacation.Month, 1)
                            LastMonthInitialDate = ThisMonth.AddMonths(-1)
                            LastMonthEndDate = New Date(LastMonthInitialDate.Year, LastMonthInitialDate.Month, Date.DaysInMonth(LastMonthInitialDate.Year, LastMonthInitialDate.Month))

                            If objVacationPeriod.Vacation.Any(Function(x) x.VacationStartDate >= LastMonthInitialDate And x.VacationStartDate <= LastMonthEndDate) Then
                                'Vacaciones que iniciaron el mes anterior
                                ListTmpVacation.AddRange(objVacationPeriod.Vacation.Where(Function(x) x.VacationStartDate >= LastMonthInitialDate And x.VacationStartDate <= LastMonthEndDate).ToList())
                            End If


                            If objVacationPeriod.Vacation.Any(Function(x) x.IncorporationDate >= LastMonthInitialDate And x.IncorporationDate <= LastMonthEndDate) Then
                                'Vacaciones que finalizaron el mes anterior
                                Dim ListIncorporationVacation = objVacationPeriod.Vacation.Where(Function(x) x.IncorporationDate >= LastMonthInitialDate And x.IncorporationDate <= LastMonthEndDate).ToList()

                                For Each objIncoporationVacation As Vacation In ListIncorporationVacation

                                    If ListTmpVacation.Any(Function(x) x.Id = objIncoporationVacation.Id) = False Then
                                        ListTmpVacation.Add(objIncoporationVacation)
                                    End If

                                Next

                            End If

                            If objVacationPeriod.Vacation.Any(Function(x) x.IncorporationDate >= ThisMonth And x.VacationStartDate < ThisMonth) Then

                                Dim ListIncorporationVacation = objVacationPeriod.Vacation.Where(Function(x) x.IncorporationDate >= ThisMonth And x.VacationStartDate < ThisMonth).ToList()

                                For Each objIncoporationVacation As Vacation In ListIncorporationVacation

                                    If ListTmpVacation.Any(Function(x) x.Id = objIncoporationVacation.Id Or x.VacationStartDate = objIncoporationVacation.VacationStartDate) = False Then
                                        ListTmpVacation.Add(objIncoporationVacation)
                                    End If

                                Next

                            End If

                        Next

                        If ListTmpVacation.Count > 1 And valorBase = Contract.Group.PayrollParameter.LegalSalaryMinimum Then
                            valorBase = 0
                        End If

                        For Each objVacation As Vacation In ListTmpVacation

                            Dim HealthAport = objVacation.HealthContribution
                            Dim HealthIBC = 0

                            ' Sin aporte de salud o sin días disfrutados no hay IBC que prorratear
                            ' (e.g. vacaciones interrumpidas/aplazadas con TakenDays = 0)
                            If HealthAport = 0 OrElse objVacation.EnjoyDays <= 0 Then
                                Continue For
                            End If

                            If objVacation.VacationStartDate < LastMonthInitialDate And objVacation.IncorporationDateReal < LastMonthInitialDate Then
                                'Es de hace dos meses, se deben tomar los días del mes anterior

                                Dim tmpEnjoyDays = objVacation.EnjoyDays

                                If Date.DaysInMonth(objVacation.VacationStartDate.Year, objVacation.VacationStartDate.Month) = 31 Then
                                    tmpEnjoyDays = tmpEnjoyDays - 1
                                End If

                                If tmpEnjoyDays > 0 Then
                                    HealthIBC = HealthAport * 30 / tmpEnjoyDays
                                    Dim CalculationDays = _liquidationDomain.Days360(LastMonthInitialDate, DateAdd(DateInterval.Day, -1, objVacation.IncorporationDateReal))
                                    Dim BaseLastMonth = HealthIBC * 100 / 4
                                    ValorBaseCalcularMesAnterior = ValorBaseCalcularMesAnterior + (BaseLastMonth / 30 * CalculationDays)
                                End If

                            End If

                            If objVacation.VacationStartDate >= LastMonthInitialDate And objVacation.IncorporationDateReal <= LastMonthEndDate Then
                                'Es porque son todas del mes anterior

                                Dim tmpEnjoyDays = objVacation.EnjoyDays

                                If objVacation.IncorporationDateReal.AddDays(-1).Day = 31 Then
                                    tmpEnjoyDays = tmpEnjoyDays - 1
                                End If

                                If tmpEnjoyDays > 0 Then
                                    HealthIBC = HealthAport * 30 / tmpEnjoyDays
                                    Dim CalculationDays = tmpEnjoyDays
                                    Dim BaseLastMonth = HealthIBC * 100 / 4
                                    ValorBaseCalcularMesAnterior = ValorBaseCalcularMesAnterior + (BaseLastMonth / 30 * CalculationDays)
                                End If

                            End If

                            If objVacation.VacationStartDate >= LastMonthInitialDate And objVacation.IncorporationDateReal > LastMonthEndDate Then
                                'Es porque iniciaron el mes anterior pero finalizan en este

                                Dim tmpEnjoyDays = objVacation.EnjoyDays

                                If LastMonthEndDate.Day = 31 Then
                                    tmpEnjoyDays = tmpEnjoyDays - 1
                                End If

                                If tmpEnjoyDays > 0 Then
                                    HealthIBC = HealthAport * 30 / tmpEnjoyDays
                                    Dim CalculationDays = _liquidationDomain.Days360(objVacation.VacationStartDate, LastMonthEndDate)
                                    Dim BaseLastMonth = HealthIBC * 100 / 4
                                    ValorBaseCalcularMesAnterior = ValorBaseCalcularMesAnterior + (BaseLastMonth / 30 * CalculationDays)
                                End If

                            End If

                            If objVacation.VacationStartDate < LastMonthInitialDate And objVacation.IncorporationDateReal > LastMonthEndDate Then
                                'Es porque iniciaron hace dos meses pero finalizan en este

                                Dim tmpEnjoyDays = objVacation.EnjoyDays

                                If Date.DaysInMonth(objVacation.VacationStartDate.Year, objVacation.VacationStartDate.Month) = 31 Then
                                    tmpEnjoyDays = tmpEnjoyDays - 1
                                End If

                                If LastMonthEndDate.Day = 31 Then
                                    tmpEnjoyDays = tmpEnjoyDays - 1
                                End If

                                If tmpEnjoyDays > 0 Then
                                    HealthIBC = HealthAport * 30 / tmpEnjoyDays
                                    Dim CalculationDays = 30
                                    Dim BaseLastMonth = HealthIBC * 100 / 4
                                    ValorBaseCalcularMesAnterior = ValorBaseCalcularMesAnterior + (BaseLastMonth / 30 * CalculationDays)
                                    valorBase = 0
                                End If

                            End If

                        Next

                    End If

                    valorBase = valorBase + ValorBaseCalcularMesAnterior

                    If valorBase < LegalSalaryMinimun Then
                        valorBase = LegalSalaryMinimun
                    End If

                    Dim ValorBaseCalcular = (valorBase * PaidHealthDays) / 30

                    If SalaryType <> 2 Then
                        health = (ValorBaseCalcular * Contract.Group.PayrollParameter.EmployeeHealthContributionPercentage) / 100
                        HealthFormulate = "(ValorBaseCalcular * Contract.Group.PayrollParameter.EmployeeHealthContributionPercentage) / 100"
                        ReplaceHealthFormulate = "(" + ValorBaseCalcular.ToString() + " * " + Contract.Group.PayrollParameter.EmployeeHealthContributionPercentage.ToString() + ") / 100"

                        pension = (ValorBaseCalcular * Contract.Group.PayrollParameter.EmployeePensionContributionPercentage) / 100
                        PensionFormulate = "(ValorBaseCalcular * Contract.Group.PayrollParameter.EmployeePensionContributionPercentage) / 100"
                        ReplacePensionFormulate = "(" + ValorBaseCalcular.ToString() + " * " + Contract.Group.PayrollParameter.EmployeePensionContributionPercentage.ToString() + ") / 100"
                    Else
                        If Contract.BasicSalary >= (Contract.Group.PayrollParameter.HealthContributionMaximunSalary * Contract.Group.PayrollParameter.LegalSalaryMinimum) Then
                            ValorBaseCalcular = Contract.Group.PayrollParameter.LegalSalaryMinimum * Contract.Group.PayrollParameter.HealthContributionMaximunSalary
                            ValorBaseCalcular = (ValorBaseCalcular * PaidHealthDays) / 30
                        End If

                        If PayrollSettings.HealthPensionIBCVacation = 2 Then
                            health = ((ValorBaseCalcular) * Contract.Group.PayrollParameter.EmployeeHealthContributionPercentage) / 100
                            HealthFormulate = "((ValorBaseCalcular * 0.7) * Contract.Group.PayrollParameter.EmployeeHealthContributionPercentage) / 100"
                            ReplaceHealthFormulate = "((" + ValorBaseCalcular.ToString() + " * 0.7) * " + Contract.Group.PayrollParameter.EmployeeHealthContributionPercentage.ToString() + ") / 100"

                            pension = ((ValorBaseCalcular) * Contract.Group.PayrollParameter.EmployeePensionContributionPercentage) / 100
                            PensionFormulate = "((ValorBaseCalcular * 0.7) * Contract.Group.PayrollParameter.EmployeePensionContributionPercentage) / 100"
                            ReplacePensionFormulate = "((" + ValorBaseCalcular.ToString() + " * 0.7) * " + Contract.Group.PayrollParameter.EmployeePensionContributionPercentage.ToString() + ") / 100"
                        Else
                            health = ((ValorBaseCalcular * 0.7) * Contract.Group.PayrollParameter.EmployeeHealthContributionPercentage) / 100
                            HealthFormulate = "((ValorBaseCalcular * 0.7) * Contract.Group.PayrollParameter.EmployeeHealthContributionPercentage) / 100"
                            ReplaceHealthFormulate = "((" + ValorBaseCalcular.ToString() + " * 0.7) * " + Contract.Group.PayrollParameter.EmployeeHealthContributionPercentage.ToString() + ") / 100"

                            pension = ((ValorBaseCalcular * 0.7) * Contract.Group.PayrollParameter.EmployeePensionContributionPercentage) / 100
                            PensionFormulate = "((ValorBaseCalcular * 0.7) * Contract.Group.PayrollParameter.EmployeePensionContributionPercentage) / 100"
                            ReplacePensionFormulate = "((" + ValorBaseCalcular.ToString() + " * 0.7) * " + Contract.Group.PayrollParameter.EmployeePensionContributionPercentage.ToString() + ") / 100"
                        End If
                    End If

                    health = Math.Ceiling(health / 100) * 100
                    pension = Math.Ceiling(pension / 100) * 100

                    If valorBase > SMLMV * 20 Then
                        SolidarityFund = Math.Ceiling(BaseSolidarityFund * 0.02 / 100) * 100
                        PensionSolidarityFormulate = "Ceiling(valueVacation * 0.02 / 100) * 100"
                        ReplacePensionSolidarityFormulate = "Ceiling(" + valueVacation.ToString() + " * 0.02 / 100) * 100"
                    ElseIf valorBase > SMLMV * 19 Then
                        SolidarityFund = Math.Ceiling(BaseSolidarityFund * 0.018 / 100) * 100
                        PensionSolidarityFormulate = "Ceiling(valueVacation * 0.018 / 100) * 100"
                        ReplacePensionSolidarityFormulate = "Ceiling(" + valueVacation.ToString() + " * 0.018 / 100) * 100"
                    ElseIf valorBase > SMLMV * 18 Then
                        SolidarityFund = Math.Ceiling(BaseSolidarityFund * 0.016 / 100) * 100
                        PensionSolidarityFormulate = "Ceiling(valueVacation * 0.016 / 100) * 100"
                        ReplacePensionSolidarityFormulate = "Ceiling(" + valueVacation.ToString() + " * 0.016 / 100) * 100"
                    ElseIf valorBase > SMLMV * 17 Then
                        SolidarityFund = Math.Ceiling(BaseSolidarityFund * 0.014 / 100) * 100
                        PensionSolidarityFormulate = "Ceiling(valueVacation * 0.014 / 100) * 100"
                        ReplacePensionSolidarityFormulate = "Ceiling(" + valueVacation.ToString() + " * 0.014 / 100) * 100"
                    ElseIf valorBase > SMLMV * 16 Then
                        SolidarityFund = Math.Ceiling(BaseSolidarityFund * 0.012 / 100) * 100
                        PensionSolidarityFormulate = "Ceiling(valueVacation * 0.012 / 100) * 100"
                        ReplacePensionSolidarityFormulate = "Ceiling(" + valueVacation.ToString() + " * 0.012 / 100) * 100"
                    ElseIf valorBase > SMLMV * 4 Then
                        SolidarityFund = Math.Ceiling(BaseSolidarityFund * 0.01 / 100) * 100
                        PensionSolidarityFormulate = "Ceiling(valueVacation * 0.01 / 100) * 100"
                        ReplacePensionSolidarityFormulate = "Ceiling(" + valueVacation.ToString() + " * 0.01 / 100) * 100"
                    Else
                        SolidarityFund = 0
                        PensionSolidarityFormulate = "0"
                        ReplacePensionSolidarityFormulate = "0"
                    End If

                    If employee.Pensionary = True Then
                        pension = 0
                    End If
                End If

                ' De acuerdo a solicitud de Miocardio, cuando las vacaciones son compensadas, no se descuenta Salud, Pensión y Fondo de Solidaridad
                If typeVacation = 1 Or typeVacation = 4 Then
                    health = 0
                    pension = 0
                    SolidarityFund = 0
                End If

                'los siguientes son descuentos que se agregan a las Vacaciones cuando son ÚNICAMENTE VACACIONES POR PAGO INMEDIATO
                Dim ForeclousureValue As Decimal = 0
                Dim ValueTotalAgreements As Decimal = 0
                Dim ListDetailVacationAgreements As New List(Of VacationDetail)
                Dim ListDetailVacationForeclousure As New List(Of VacationDetail)
                Dim ListDetailVacationManualConcept As New List(Of VacationDetail)
                Dim ObjForeclousure As New List(Of Foreclousure)

                Dim AuthorizationConcept = _autorizationConceptRepository.GetAuthorizationConceptByGroupId(Contract.GroupId)

                Dim IdHealthConcept = 0
                If AuthorizationConcept.Any(Function(x) x.Concept.ConceptType = 2 And x.Concept.ConceptClass = "017") Then
                    IdHealthConcept = AuthorizationConcept.Where(Function(x) x.Concept.ConceptType = 2 And x.Concept.ConceptClass = "017").FirstOrDefault().ConceptId
                End If

                Dim IdPensionConcept = 0
                If AuthorizationConcept.Any(Function(x) x.Concept.ConceptType = 2 And x.Concept.ConceptClass = "014") Then
                    IdPensionConcept = AuthorizationConcept.Where(Function(x) x.Concept.ConceptType = 2 And x.Concept.ConceptClass = "014").FirstOrDefault().ConceptId
                End If

                Dim IdPensionSolidarityConcept = 0
                If AuthorizationConcept.Any(Function(x) x.Concept.ConceptType = 2 And x.Concept.ConceptClass = "038") Then
                    IdPensionSolidarityConcept = AuthorizationConcept.Where(Function(x) x.Concept.ConceptType = 2 And x.Concept.ConceptClass = "038").FirstOrDefault().ConceptId
                End If

                Dim IdIncentivePaymentVacationConcept = 0 'PRIMA DE VACACIONES
                If AuthorizationConcept.Any(Function(x) x.Concept.ConceptType = 1 And x.Concept.ConceptClass = "049") Then
                    IdIncentivePaymentVacationConcept = AuthorizationConcept.Where(Function(x) x.Concept.ConceptType = 1 And x.Concept.ConceptClass = "049").FirstOrDefault().ConceptId
                End If

                Dim IdBonificationConcept = 0 'BONIFICACIÓN ESPECIAL PARA LA RECREACIÓN
                If AuthorizationConcept.Any(Function(x) x.Concept.ConceptType = 1 And x.Concept.ConceptClass = "063") Then
                    IdBonificationConcept = AuthorizationConcept.Where(Function(x) x.Concept.ConceptType = 1 And x.Concept.ConceptClass = "063").FirstOrDefault().ConceptId
                End If

                Dim IdVacationIncreaseConcept = 0 'INCREMENTO VACACIONAL
                If AuthorizationConcept.Any(Function(x) x.Concept.ConceptType = 1 And x.Concept.ConceptClass = "064") Then
                    IdVacationIncreaseConcept = AuthorizationConcept.Where(Function(x) x.Concept.ConceptType = 1 And x.Concept.ConceptClass = "064").FirstOrDefault().ConceptId
                End If

                Dim TotalAccruedManualConcept As Decimal = 0
                Dim TotalDeductedManualConcept As Decimal = 0

                If typePayment = 1 Then

                    If Indigo.IndigoPayrollIntegration = 1 Then
                        If PayrollSettings.IdVacationConcept Is Nothing Then
                            actionResult.StateResult = False
                            actionResult.MessageResult.Add(New MessageResult("DONTAPPLY", "El Concepto de Vacaciones no está parametrizado. Se parametriza en el Formulario de Parámetros de Nómina"))
                            Return actionResult
                        End If
                    End If

                    'Creamos el listado de Convenios para que sean descontados por Vacaciones de Pago Inmediato que aplican para el proceso de vacaciones que afecte vacaciones
                    ObjForeclousure = _ForeclousureRepository.GetForeclousureByEmployeeStatusVacation(employee.Id, 2)

                    If ObjForeclousure IsNot Nothing AndAlso ObjForeclousure.Count > 0 Then
                        ListDetailVacationForeclousure = CreateVacationDetailForeclousure(ObjForeclousure, valueVacation)
                        If ListDetailVacationForeclousure.Count > 0 Then
                            ForeclousureValue = ListDetailVacationForeclousure.Sum(Function(x) x.Deducted)
                        End If
                    End If
                    'Obtemenos el listado de Convenios para que sean descontados por Vacaciones de Pago Inmediato que aplican para el proceso de vacaciones
                    Dim ListAgreements = _AgreementsCRepository.GetAgreementsByEmployeeStatusVacation(employee.Id, 2, initialDateVacation)

                    If ListAgreements IsNot Nothing AndAlso ListAgreements.Count > 0 Then
                        'Creamos el detallado de Vacaiones de los Convenios
                        ListDetailVacationAgreements = CreateVacationDetailAgreements(ListAgreements)
                        If ListDetailVacationAgreements.Count > 0 Then
                            ValueTotalAgreements = ListDetailVacationAgreements.Sum(Function(x) x.Deducted)
                        End If
                    End If

                End If

                Dim TmpListManualConcept = _manualConceptRepository.GetManualConceptsByContractNumberPayrollDate(Contract.InitialContractNumber, initialDateVacation, incorporationDate, 1, 6)

                If TmpListManualConcept IsNot Nothing AndAlso TmpListManualConcept.Count > 0 Then
                    'Creamos el detallado de los conceptos manuales de Vacaciones
                    ListDetailVacationManualConcept = CreateVacationDetailManualConcept(TmpListManualConcept)
                    If ListDetailVacationManualConcept.Count > 0 Then
                        TotalAccruedManualConcept = ListDetailVacationManualConcept.Sum(Function(x) x.Accrued)
                        TotalDeductedManualConcept = ListDetailVacationManualConcept.Sum(Function(x) x.Deducted)
                    End If
                End If

                If employee.Pensionary = True Then 'Si el empleado ya se pensiono, no se aporta a pension. 
                    pension = 0
                End If

                'Totalizamos el Valor de Vacaciones
                total = (valueVacation + TotalAccruedManualConcept + VacationIncrease + BonificationValue + IncentivePaymentVacationValue) - (health + pension + SolidarityFund + ForeclousureValue + ValueTotalAgreements + TotalDeductedManualConcept)

                'Agregamos el Detalle de Vacaciones
                Dim ListVacationDetail = CreateVacationDetail(valueVacation, VacationFormula, ReplaceFormulateVacation, BonificationValue, BonificationFormula, ReplaceFormulateBonification, IncentivePaymentVacationValue, VacationalIncentiveFormula, ReplaceFormulateIncentive,
                                                          VacationIncrease, VacationalIncreaseFormula, ReplaceFormulateIncrease, health, HealthFormulate, ReplaceHealthFormulate, pension, PensionFormulate, ReplacePensionFormulate, SolidarityFund, PensionSolidarityFormulate,
                                                          ReplacePensionSolidarityFormulate, ForeclousureValue, PayrollSettings.IdVacationConcept, IdHealthConcept, IdPensionConcept, IdPensionSolidarityConcept, IdIncentivePaymentVacationConcept, IdVacationIncreaseConcept, IdBonificationConcept)

                If ListVacationDetail.Count = 0 Then
                    actionResult.StateResult = False
                    actionResult.MessageResult.Add(New MessageResult("DONTAPPLY", "Se ha generado un error en las Vacaciones pues no tienen detalle. Por favor contacte con el Administrador del Sistema"))
                    Return actionResult
                End If

                Dim CountVacation As Integer = 0
                'Creo el objeto de Vacaciones
                Dim requestDaysTmp = requestDays - VacationPlusDays
                For Each vacationPeriod As VacationPeriod In employee.VacationPeriod.OrderBy(Function(x) x.InitialDatePeriod)
                    If vacationPeriod.PendingDays > 0 Then
                        CountVacation += 1
                        Dim takenDaysTmp As Integer
                        If requestDaysTmp > vacationPeriod.PendingDays Then
                            takenDaysTmp = vacationPeriod.PendingDays
                            vacationPeriod.TakenDays += takenDaysTmp
                            requestDaysTmp -= vacationPeriod.PendingDays
                            vacationPeriod.PendingDays = 0
                        Else
                            takenDaysTmp = requestDaysTmp
                            vacationPeriod.TakenDays += requestDaysTmp
                            vacationPeriod.PendingDays -= requestDaysTmp
                            requestDaysTmp = 0
                        End If
                        Dim vacation As Vacation = New Vacation()
                        vacation.VacationStartDate = initialDateVacation
                        vacation.VacationEndDate = endDate
                        vacation.TypeLiquidation = typeCalculate
                        vacation.TypeVacation = typeVacation
                        vacation.TypePayment = typePayment
                        If typePayment = 1 Then 'Inmediato
                            If Indigo.IndigoPayrollIntegration = 1 Then
                                vacation.State = 1
                            Else
                                vacation.State = 2
                            End If
                        ElseIf typePayment = 2 Then 'Proxima Nomina
                            vacation.State = 1
                            vacation.LiquidationDate = Contract.Group.NextDateLiquidation
                        End If

                        vacation.TakenDays = takenDaysTmp
                        vacation.EnjoyDays = enjoyDays
                        vacation.IncorporationDate = incorporationDate
                        vacation.BaseLiquidation = valueBase
                        vacation.VacationValue = Utils.RoundedValuesByRate(valueVacation, Contract.Group.PayrollParameter.AproximationValue)
                        vacation.VacationalIncreaseValue = VacationIncrease
                        vacation.VacationBonificationValue = BonificationValue
                        vacation.IncentivePaymentVacationValue = IncentivePaymentVacationValue
                        vacation.HealthContribution = health
                        vacation.PensionContribution = pension
                        vacation.SolidarityFundValue = SolidarityFund
                        vacation.VacationValueNet = total
                        vacation.TakenDaysReal = takenDaysTmp
                        vacation.IncorporationDateReal = incorporationDate
                        vacation.StateIncorporation = 1 'Normal
                        vacation.PreviousMonthIBC = IBCLastPeriodVac

                        'Agrego el detalle de Vacaciones
                        If ListVacationDetail IsNot Nothing AndAlso ListVacationDetail.Count > 0 Then
                            For Each ObjVacationDetail As VacationDetail In ListVacationDetail
                                vacation.VacationDetail.Add(ObjVacationDetail)
                            Next
                        End If

                        'Agrego el detalle de Convenios
                        If ListDetailVacationAgreements IsNot Nothing AndAlso ListDetailVacationAgreements.Count > 0 Then
                            For Each ObjVacationDetail As VacationDetail In ListDetailVacationAgreements
                                vacation.VacationDetail.Add(ObjVacationDetail)
                            Next
                        End If

                        'Es porque con este periodo de Vacaciones no se han pagado Convenios
                        If vacationPeriod.Vacation.Count = 0 Then

                            'Agregamos el detalle de Embargos
                            If ListDetailVacationForeclousure IsNot Nothing AndAlso ListDetailVacationForeclousure.Count > 0 Then
                                For Each ObjVacationDetail As VacationDetail In ListDetailVacationForeclousure
                                    vacation.VacationDetail.Add(ObjVacationDetail)
                                Next
                            End If

                            'Agregamos el detalle de Conceptos Manuales
                            If ListDetailVacationManualConcept IsNot Nothing AndAlso ListDetailVacationManualConcept.Count > 0 Then
                                For Each ObjVacationDetail As VacationDetail In ListDetailVacationManualConcept
                                    vacation.VacationDetail.Add(ObjVacationDetail)
                                Next
                            End If
                        End If

                        If vacationPeriod.ChangeTracker.State = ObjectState.Unchanged Then
                            vacationPeriod.MarkAsModified()
                        End If

                        For Each ObjVacation As Vacation In vacationPeriod.Vacation

                            If ObjVacation.ChangeTracker.State = ObjectState.Added Then
                                For Each ObjVacationDetail As VacationDetail In ObjVacation.VacationDetail
                                    ObjVacationDetail.MarkAsAdded()
                                Next
                            End If
                        Next

                        vacationPeriod.Vacation.Add(vacation)

                        If requestDaysTmp = 0 Then
                            Exit For
                        End If
                    End If
                Next

            Next

            actionResult.ObjectEmbbeded = employees

        Catch ex As Exception
            actionResult.StateResult = False
            actionResult.Message = ex.Message.ToString()
            actionResult.MessageResult.Add(New MessageResult("DONTAPPLY", ex.Message.ToString()))
            Return actionResult
        End Try

        Return actionResult

    End Function

    Public Function ReplaceDataFormulates(FormulaConcept As String, BasicSalary As Double, PayrollDays As Integer, VacationDays As Integer, LegalMinumunSalary As Double, RepresentationCost As Double,
                                           BonificationValue As Double, TransportHealthValue As Double, PaidValueAverageIncentiveServicesValue As Double, PaidVacationDays As Integer, SalaryPromedio As Double, varTypeVacation As Boolean,
                                          ContractSalaryPromedy As Decimal, NormalEveningRechargePromedy As Decimal, FestiveEveningRechargePromedy As Decimal, SundayRechargePromedy As Decimal, BonificationSalaryPromedy As Decimal, ExtraHoursPromedy As Decimal, VacationSalaryPromedy As Decimal,
                                          ByRef StatusFormula As Boolean, ByRef ErrorFormula As String, ByRef ReplaceFormulate As String) As Double

        Dim FormulaOriginal = FormulaConcept

        FormulaConcept = Replace(FormulaConcept, "[Salario Mínimo]", Format(LegalMinumunSalary, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Auxilio Transporte]", Format(TransportHealthValue, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Sueldo Contrato]", Format(BasicSalary, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Días Nómina]", PayrollDays)
        FormulaConcept = Replace(FormulaConcept, "[Días Vacaciones]", VacationDays)
        FormulaConcept = Replace(FormulaConcept, "[Gastos de Representación]", Format(RepresentationCost, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Bonificacion Año Servicio]", Format(BonificationValue, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Valor Prima Servicio]", Format(PaidValueAverageIncentiveServicesValue, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Dias Pagados Vacaciones]", PaidVacationDays)
        FormulaConcept = Replace(FormulaConcept, "[Sueldo Promedio]", Format(SalaryPromedio, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Vacaciones Compensadas]", varTypeVacation)
        FormulaConcept = Replace(FormulaConcept, "[Sueldo Promedio Contrato]", Format(ContractSalaryPromedy, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Promedio Recargos Nocturnos Normales]", Format(NormalEveningRechargePromedy, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Recargos Promedio Nocturnos N]", Format(NormalEveningRechargePromedy, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Promedio Recargos Nocturnos Festivos]", Format(FestiveEveningRechargePromedy, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Promedio Recargos Dominicales]", Format(SundayRechargePromedy, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Promedio Bonificaciones Salarias]", Format(BonificationSalaryPromedy, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Promedio Horas Extras]", Format(ExtraHoursPromedy, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Promedio Salario Variable Vacaciones]", Format(VacationSalaryPromedy, "0.00").Replace(",", "."))

        ReplaceFormulate = FormulaConcept
        Dim result = Utils.EvalExpression(FormulaConcept)
        Dim ConceptValue As Decimal = 0
        If result.StateResult = True Then
            StatusFormula = False
            ConceptValue = CType(result.ObjectEmbbeded, Decimal)
        Else
            StatusFormula = True
            ErrorFormula = "Se presentó error en la Fórmula: " + FormulaOriginal
            ConceptValue = 0
        End If

        Return ConceptValue


    End Function

    Public Function CalcValueAcumulatedLiquidationDetail(ListLiquidationDetail As List(Of LiquidationDetail), CountMonth As Integer) As Decimal
        If ListLiquidationDetail IsNot Nothing AndAlso ListLiquidationDetail.Count > 0 Then
            Dim Value As Decimal = 0
            Dim AccruedValue As Decimal = 0
            Dim DeductedValue As Decimal = 0
            Dim ObjListLiquidation As New List(Of Liquidation)

            AccruedValue = ListLiquidationDetail.Where(Function(x) x.ConceptType = 1).Sum(Function(x) x.ConceptTotalValue)
            DeductedValue = ListLiquidationDetail.Where(Function(x) x.ConceptType = 2).Sum(Function(x) x.ConceptTotalValue)

            Return (AccruedValue - DeductedValue) / CountMonth
        Else
            Return 0
        End If

    End Function

    Public Function CreateVacationDetail(VacationValue As Decimal, VacationFormulate As String, ReplaceVacationFormulate As String, BonificationValue As Decimal, BonificationFormulate As String, BonificationReplaceFormulate As String,
                                         IncentivePaymentVacationValue As Decimal, IncentiveFormulate As String, IncentiveReplaceFormulate As String, VacationIncrease As Decimal, VacationIncreaseFormulate As String, VacationIncreaseReplace As String,
                                         HealthValue As Decimal, HealthFormulate As String, HealthReplaceFormulate As String, PensionValue As Decimal, PensionFormulate As String, PensionReplaceFormulate As String,
                                         PensionSolidarityFund As Decimal, PensionSolidarityFormulate As String, PensionSolidarityReplaceFormulate As String, ForeclousureValue As Decimal, IdConceptVacation As Integer,
                                         IdHealthConcept As Integer, IdPensionConcept As Integer, IdPensionSolidarityConcept As Integer, IdIncentivePaymentVacationConcept As Integer, IdVacationIncreaseConcept As Integer, IdBonificationConcept As Integer) As List(Of VacationDetail)

        Dim ListVacationDetail As New List(Of VacationDetail)

        'Valor Vacaciones
        If VacationValue > 0 Then
            Dim ObjVacationDetail As New VacationDetail

            With ObjVacationDetail
                .IdConcept = IdConceptVacation
                .Description = "Vacaciones"
                .Accrued = CDec(VacationValue)
                .Deducted = 0
                .ConceptFormulate = VacationFormulate
                .ReplaceConceptFormulate = ReplaceVacationFormulate
            End With

            ListVacationDetail.Add(ObjVacationDetail)
        End If

        'Bonificación Especial para Recreación
        If BonificationValue > 0 Then
            Dim ObjVacationDetail As New VacationDetail

            With ObjVacationDetail
                .IdConcept = IdBonificationConcept
                .Description = "Bonificación Especial para Recreación"
                .Accrued = CDec(BonificationValue)
                .Deducted = 0
                .ConceptFormulate = BonificationFormulate
                .ReplaceConceptFormulate = BonificationReplaceFormulate
            End With

            ListVacationDetail.Add(ObjVacationDetail)
        End If

        'Prima de Vacaciones
        If IncentivePaymentVacationValue > 0 Then
            Dim ObjVacationDetail As New VacationDetail

            With ObjVacationDetail
                .IdConcept = IdIncentivePaymentVacationConcept
                .Description = "Prima de Vacaciones"
                .Accrued = CDec(IncentivePaymentVacationValue)
                .Deducted = 0
                .ConceptFormulate = IncentiveFormulate
                .ReplaceConceptFormulate = IncentiveReplaceFormulate
            End With

            ListVacationDetail.Add(ObjVacationDetail)
        End If

        'Incremento de Vacaciones
        If VacationIncrease > 0 Then
            Dim ObjVacationDetail As New VacationDetail

            With ObjVacationDetail
                .IdConcept = IdVacationIncreaseConcept
                .Description = "Incremento Vacaciones"
                .Accrued = CDec(VacationIncrease)
                .Deducted = 0
                .ConceptFormulate = VacationIncreaseFormulate
                .ReplaceConceptFormulate = VacationIncreaseReplace
            End With

            ListVacationDetail.Add(ObjVacationDetail)
        End If

        'Aporte Salud
        If HealthValue > 0 Then
            Dim ObjVacationDetail As New VacationDetail

            With ObjVacationDetail
                .IdConcept = IdHealthConcept
                .Description = "Aporte Salud"
                .Accrued = 0
                .Deducted = Math.Ceiling(CDec(HealthValue))
                .ConceptFormulate = HealthFormulate
                .ReplaceConceptFormulate = HealthReplaceFormulate
            End With

            ListVacationDetail.Add(ObjVacationDetail)
        End If

        'Aporte Pensión
        If PensionValue > 0 Then
            Dim ObjVacationDetail As New VacationDetail

            With ObjVacationDetail
                .IdConcept = IdPensionConcept
                .Description = "Aporte Pensión"
                .Accrued = 0
                .Deducted = Math.Ceiling(CDec(PensionValue))
                .ConceptFormulate = PensionFormulate
                .ReplaceConceptFormulate = PensionReplaceFormulate
            End With

            ListVacationDetail.Add(ObjVacationDetail)
        End If

        'Aporte Pensión
        If PensionSolidarityFund > 0 And IdPensionSolidarityConcept > 0 Then
            Dim ObjVacationDetail As New VacationDetail

            With ObjVacationDetail
                .IdConcept = IdPensionSolidarityConcept
                .Description = "Fondo de Solidaridad Pensional"
                .Accrued = 0
                .Deducted = Math.Ceiling(CDec(PensionSolidarityFund))
                .ConceptFormulate = PensionSolidarityFormulate
                .ReplaceConceptFormulate = PensionSolidarityReplaceFormulate
            End With

            ListVacationDetail.Add(ObjVacationDetail)
        End If

        Return ListVacationDetail
    End Function

    Public Function CreateVacationDetailAgreements(ListAgreements As List(Of AgreementsC)) As List(Of VacationDetail)

        Dim ObjListVacacionDetal As New List(Of VacationDetail)
        For Each ObjAgreements As AgreementsC In ListAgreements

            Dim ObjVacationDetail As New VacationDetail

            With ObjVacationDetail
                .IdConcept = ObjAgreements.ConceptId
                .Description = "Convenio # " + ObjAgreements.Consecutive.ToString() + " - " + ObjAgreements.Company.Name
                .Accrued = 0
                .Deducted = CalculateAgreementsValue(ObjAgreements)
                .ConceptFormulate = "AgreementsValueValue"
                .ReplaceConceptFormulate = .Deducted
            End With

            ObjListVacacionDetal.Add(ObjVacationDetail)

        Next

        Return ObjListVacacionDetal
    End Function

    Public Function CreateVacationDetailManualConcept(ListManualConcept As List(Of ManualConcepts)) As List(Of VacationDetail)

        Dim ObjListVacacionDetal As New List(Of VacationDetail)
        For Each ObjManualConcept As ManualConcepts In ListManualConcept

            Dim ObjVacationDetail As New VacationDetail

            Dim AccruedValue As Decimal = 0
            Dim DeductedValue As Decimal = 0

            If ObjManualConcept.Concept.ConceptType = 1 Then
                AccruedValue = ObjManualConcept.QuoteValue
            Else
                DeductedValue = ObjManualConcept.QuoteValue
            End If

            With ObjVacationDetail
                .IdConcept = ObjManualConcept.ConceptId
                .Description = "Concepto Manual # " + ObjManualConcept.Consecutive.ToString()
                .Accrued = AccruedValue
                .Deducted = DeductedValue
                .ConceptFormulate = "[Valor Concepto Manual]"
                .ReplaceConceptFormulate = IIf(AccruedValue > 0, AccruedValue, DeductedValue)
            End With

            ObjListVacacionDetal.Add(ObjVacationDetail)

        Next

        Return ObjListVacacionDetal
    End Function

    Public Function CreateVacationDetailForeclousure(ListForeclousure As List(Of Foreclousure), ValueVacation As Decimal) As List(Of VacationDetail)

        Dim ObjListVacacionDetal As New List(Of VacationDetail)
        For Each ObjForeclousure As Foreclousure In ListForeclousure

            Dim ObjVacationDetail As New VacationDetail

            With ObjVacationDetail
                .IdConcept = ObjForeclousure.IdConcept
                .Description = "Embargo # " + ObjForeclousure.Code.ToString()
                .Accrued = 0
                .Deducted = CalculateForeclousureValue(ObjForeclousure, ValueVacation)
                .ConceptFormulate = "ForeclousureValueValue"
                .ReplaceConceptFormulate = .Deducted
            End With

            ObjListVacacionDetal.Add(ObjVacationDetail)
        Next

        Return ObjListVacacionDetal
    End Function

    Public Function CalculateAgreementsValue(ObjAgrements As AgreementsC) As Decimal
        Dim AgreementValue = 0
        If ObjAgrements.LiquidationType = 1 And ObjAgrements.TermType = 1 Then 'Fijo - Fijo
            If ObjAgrements.CurrentBalance > 0 Then
                AgreementValue = ObjAgrements.AgreementValue / ObjAgrements.NumberShares
                If ObjAgrements.CurrentBalance < AgreementValue Then
                    AgreementValue = ObjAgrements.CurrentBalance
                End If
            End If
        ElseIf ObjAgrements.LiquidationType = 1 And ObjAgrements.TermType = 2 Then 'Fijo - Variable
            AgreementValue = ObjAgrements.AgreementValue
        ElseIf ObjAgrements.LiquidationType = 2 And ObjAgrements.TermType = 2 Then 'Variable - Variable
            AgreementValue = ObjAgrements.AgreementValue
        End If

        Return AgreementValue
    End Function

    Public Function CalculateForeclousureValue(ObjForeclousure As Foreclousure, ValueVacation As Decimal) As Decimal

        Dim ValueForeclosusure As Decimal = 0

        If ObjForeclousure.AffectVacation = True Then
            If ObjForeclousure.DiscountType = 1 Or ObjForeclousure.DiscountType = 4 Then
                'Porcentaje
                ValueForeclosusure = ((ValueVacation * ObjForeclousure.Percentage) / 100)
            Else
                'Valor Fijo
                ValueForeclosusure = ObjForeclousure.QuoteValue
            End If
        End If

        Return ValueForeclosusure

    End Function

    Public Function CreateJournalVoucher(ListVacationDetail As List(Of VacationDetail), Employee As Employee, Contract As Contract, group As Group, audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionResult(Of Domain.Entities.JournalVouchers) Implements IVacationPeriodDomain.CreateJournalVoucher

        Dim ResultObjJournalVouchers As New ActionResult(Of Domain.Entities.JournalVouchers)

        Try

            If group.PayrollParameter.IdLiquidationVacationAccount Is Nothing Then
                Return New ActionResult(Of Domain.Entities.JournalVouchers) With {.StateResult = False, .Message = "No ha parametrizado por Grupos la Cuenta Contable de Liquidación de Vacaciones"}
            End If

            Dim ListConceptAccount = _IConceptAccountingStructureRepository.GetAccountingStructureIntegrated(Contract.FunctionalUnitId)
            Dim ObjListForeclousure = _ForeclousureRepository.GetForeclousureByEmployeeStatus(Employee.Id, 2)

            Dim JournalVoucher As New Domain.Entities.JournalVouchers
            Dim ListJournalVoucherDetailas As New List(Of Domain.Entities.JournalVoucherDetails)

            Dim OfficialBook As Domain.Entities.LegalBook = _bookRepository.ValidateOfficialBook()

            With JournalVoucher

                For Each ObjObjVacationDetail As VacationDetail In ListVacationDetail
                    Dim NewObjJournalVoucherDetail As New Domain.Entities.JournalVoucherDetails

                    Dim ObjConcept = _conceptRepository.GetConceptId(ObjObjVacationDetail.IdConcept)
                    Dim ObjListAgreements = _AgreementsCRepository.GetAgreementsByEmployeeStatusVacation(Employee.Id, 2, ObjObjVacationDetail.Vacation.VacationStartDate)

                    If ListConceptAccount.Any(Function(x) x.ConceptId = ObjObjVacationDetail.IdConcept) = False Then
                        Return New ActionResult(Of Domain.Entities.JournalVouchers) With {.StateResult = False, .Message = "El Concepto " + ObjConcept.Code + " - " + ObjConcept.Name + " no tiene parametrizada cuenta contable"}
                    End If

                    Dim thirdPartyId As Integer
                    Dim IdMainAccount As Integer
                    Dim AdcruedValue As Decimal = 0
                    Dim DeductetValue As Decimal = 0
                    Dim ValidateAccount As Domain.Payroll.Entities.MainAccounts = Nothing

                    If ObjObjVacationDetail.Accrued > 0 Then
                        thirdPartyId = Employee.ThirdPartyId
                        AdcruedValue = ObjObjVacationDetail.Accrued
                        DeductetValue = 0

                        Dim tmpAccount = ListConceptAccount.Where(Function(x) x.ConceptId = ObjObjVacationDetail.IdConcept).FirstOrDefault()

                        ValidateAccount = _CostDistributionRepository.GetMainAccountByNumber(tmpAccount.AccruedAccount)
                        If ValidateAccount Is Nothing Then
                            Return New ActionResult(Of Domain.Entities.JournalVouchers) With {.StateResult = False, .Message = "La cuenta Contable " + tmpAccount.AccruedAccount + " del Concepto " + ObjConcept.Code + " - " + ObjConcept.Name + " no existe"}
                        End If

                        IdMainAccount = _CostDistributionRepository.GetMainAccountByNumber(tmpAccount.AccruedAccount).Id
                    Else

                        If ObjConcept.ConceptClass = "041" Then
                            'Convenios
                            If ObjListAgreements.Any(Function(x) x.ConceptId = ObjConcept.Id) Then
                                Dim tmpObjAgreements = ObjListAgreements.Where(Function(x) x.ConceptId = ObjConcept.Id).FirstOrDefault()
                                thirdPartyId = tmpObjAgreements.Company.ThirdPartyId
                            End If
                        ElseIf ObjConcept.ConceptClass = "017" Then
                            'Aporte a Salud
                            Dim HealthFundContract = Contract.FundContract.Where(Function(x) x.FundType = 1 And x.State = True).FirstOrDefault()
                            Dim HealthFund = _FundsRepository.GetFundsById(HealthFundContract.FundId)
                            thirdPartyId = HealthFund.ThirdPartyId

                        ElseIf ObjConcept.ConceptClass = "014" Or ObjConcept.ConceptClass = "038" Then
                            'Aporte a Pension
                            Dim PenionFundContract = Contract.FundContract.Where(Function(x) x.FundType = 2 And x.State = True).FirstOrDefault()
                            Dim PensionFund = _FundsRepository.GetFundsById(PenionFundContract.FundId)
                            thirdPartyId = PensionFund.ThirdPartyId
                        Else
                            thirdPartyId = Employee.ThirdPartyId
                        End If

                        AdcruedValue = 0
                        DeductetValue = ObjObjVacationDetail.Deducted

                        Dim tmpAccount = ListConceptAccount.Where(Function(x) x.ConceptId = ObjObjVacationDetail.IdConcept).FirstOrDefault()

                        ValidateAccount = _CostDistributionRepository.GetMainAccountByNumber(tmpAccount.DeductedAccount)
                        If ValidateAccount Is Nothing Then
                            Return New ActionResult(Of Domain.Entities.JournalVouchers) With {.StateResult = False, .Message = "La cuenta Contable " + tmpAccount.DeductedAccount + " del Concepto " + ObjConcept.Code + " - " + ObjConcept.Name + " no existe"}
                        End If

                        IdMainAccount = _CostDistributionRepository.GetMainAccountByNumber(tmpAccount.DeductedAccount).Id

                    End If

                    With NewObjJournalVoucherDetail
                        .IdMainAccount = IdMainAccount
                        .IdThirdParty = thirdPartyId
                        If ValidateAccount IsNot Nothing AndAlso ValidateAccount.HandlesCostCenter = True Then
                            .IdCostCenter = Contract.FunctionalUnit.CostCenterId
                        End If
                        .DebitValue = AdcruedValue
                        .CreditValue = DeductetValue
                        .Detail = "Pago Generado desde Vacaciones"
                    End With

                    'Insertamos los detalles
                    .JournalVoucherDetails.Add(NewObjJournalVoucherDetail)
                Next

                'Insertamos las líneas de pasivo discriminadas por concepto de devengo
                Dim TotalPaid = .JournalVoucherDetails.Sum(Function(x) x.DebitValue) - .JournalVoucherDetails.Sum(Function(x) x.CreditValue)

                Dim AccruedDetails = ListVacationDetail.Where(Function(x) x.Accrued > 0D).ToList()
                Dim LiabilityLines As New Dictionary(Of Integer, Tuple(Of Decimal, Domain.Payroll.Entities.MainAccounts))
                Dim SumNonPrincipalAccrued As Decimal = 0D

                For Each detail In AccruedDetails
                    Dim conceptAccount = ListConceptAccount.Where(Function(x) x.ConceptId = detail.IdConcept).FirstOrDefault()
                    If conceptAccount Is Nothing OrElse conceptAccount.DeductedAccount Is Nothing Then
                        Continue For
                    End If

                    Dim liabilityAccount = _CostDistributionRepository.GetMainAccountByNumber(conceptAccount.DeductedAccount)
                    If liabilityAccount Is Nothing Then
                        Continue For
                    End If

                    Dim concept = _conceptRepository.GetConceptId(detail.IdConcept)
                    If concept.ConceptClass <> "030" Then
                        SumNonPrincipalAccrued += detail.Accrued
                        If LiabilityLines.ContainsKey(liabilityAccount.Id) Then
                            LiabilityLines(liabilityAccount.Id) = Tuple.Create(LiabilityLines(liabilityAccount.Id).Item1 + detail.Accrued, liabilityAccount)
                        Else
                            LiabilityLines(liabilityAccount.Id) = Tuple.Create(detail.Accrued, liabilityAccount)
                        End If
                    End If
                Next

                ' Concepto principal 030 (Vacaciones) absorbe el residuo para garantizar balance
                Dim principalDetail = AccruedDetails.FirstOrDefault(Function(x)
                                                                        Dim c = _conceptRepository.GetConceptId(x.IdConcept)
                                                                        Return c.ConceptClass = "030"
                                                                    End Function)

                If principalDetail IsNot Nothing Then
                    Dim principalAccount = ListConceptAccount.Where(Function(x) x.ConceptId = principalDetail.IdConcept).FirstOrDefault()
                    If principalAccount IsNot Nothing AndAlso principalAccount.DeductedAccount IsNot Nothing Then
                        Dim principalLiabilityAccount = _CostDistributionRepository.GetMainAccountByNumber(principalAccount.DeductedAccount)
                        If principalLiabilityAccount IsNot Nothing Then
                            Dim principalValue As Decimal = TotalPaid - SumNonPrincipalAccrued
                            If LiabilityLines.ContainsKey(principalLiabilityAccount.Id) Then
                                LiabilityLines(principalLiabilityAccount.Id) = Tuple.Create(LiabilityLines(principalLiabilityAccount.Id).Item1 + principalValue, principalLiabilityAccount)
                            Else
                                LiabilityLines(principalLiabilityAccount.Id) = Tuple.Create(principalValue, principalLiabilityAccount)
                            End If
                        End If
                    End If
                End If

                ' Fallback: si no se generaron líneas discriminadas, usar cuenta única parametrizada
                If Not LiabilityLines.Any() Then
                    Dim fallbackAccountId As Integer = If(group.PayrollParameter.IdLiquidationVacationAccount IsNot Nothing, CInt(group.PayrollParameter.IdLiquidationVacationAccount), 0)
                    LiabilityLines(fallbackAccountId) = Tuple.Create(TotalPaid, DirectCast(Nothing, Domain.Payroll.Entities.MainAccounts))
                End If

                For Each liability In LiabilityLines
                    Dim NewPaidVoucherDetail As New Domain.Entities.JournalVoucherDetails
                    With NewPaidVoucherDetail
                        .IdMainAccount = liability.Key
                        .IdThirdParty = Employee.ThirdPartyId
                        .DebitValue = 0
                        .CreditValue = liability.Value.Item1
                        .Detail = "Pago Generado desde Vacaciones"

                        If liability.Value.Item2 IsNot Nothing AndAlso liability.Value.Item2.HandlesCostCenter = True Then
                            .IdCostCenter = Contract.FunctionalUnit.CostCenterId
                        End If
                    End With
                    .JournalVoucherDetails.Add(NewPaidVoucherDetail)
                Next
                'Insertamos la Cabecera

                .LegalBookId = OfficialBook.Id
                .DateTRM = Date.Now()
                .BookCurrencyId = _PayrollSettings.GetSettingPayroll().CurrencyId
                .IdJournalVoucher = group.PayrollParameter.IdPayrollVoucherType
                .VoucherDate = Date.Now()
                .Detail = "Comprobante Contable de Nómina - Vacaciones - Empleado: " + Employee.ThirdParty.Nit + " - " + Employee.ThirdParty.Name
                .EntityName = "PayrollLiquidation"
                .IsClosedYear = 0
                .CreationUser = audit.CodeUser
                .CreationDate = Date.Now()
                .Status = 2
            End With

            ResultObjJournalVouchers.ObjectEmbbeded = JournalVoucher
            ResultObjJournalVouchers.StateResult = True

        Catch ex As Exception
            ResultObjJournalVouchers.ObjectEmbbeded = Nothing
            ResultObjJournalVouchers.StateResult = False
            ResultObjJournalVouchers.MessageResult.Add(ex.Message.ToString())
        End Try

        Return ResultObjJournalVouchers
    End Function

    Public Function CreateVoucherTransaction(Vacation As Vacation, Employee As Employee, group As Group, Session As Infrastructure.CrossCutting.Base.SessionValues) As ActionResult(Of Domain.Entities.VoucherTransaction) Implements IVacationPeriodDomain.CreateVoucherTransaction
        Dim ResultObjJournalVouchers As New ActionResult(Of Domain.Entities.VoucherTransaction)

        Try

            Dim PayrollSettings = _PayrollSettings.GetSettingPayroll()

            If PayrollSettings Is Nothing Then
                Return New ActionResult(Of Domain.Entities.VoucherTransaction) With {.StateResult = False, .Message = "No se encontraron parámetros de Nómina creados"}
            End If

            If PayrollSettings.IdExpenseConceptsVacation Is Nothing Then
                Return New ActionResult(Of Domain.Entities.VoucherTransaction) With {.StateResult = False, .Message = "No ha parametrizado el Concepto de Egreso para Vacaciones en Parámetros de Nómina"}
            End If

            Dim VoucherTransaction As New Domain.Entities.VoucherTransaction

            Dim ListMessageError As New List(Of String)

            'Dim PayrollDate = Date.Now

            With VoucherTransaction
                Dim VoucherTransactionDetail As New Domain.Entities.VoucherTransactionDetails

                Dim ObjExpenseConcepts = _expenseConceptRepository.GetExpenseConceptById(PayrollSettings.IdExpenseConceptsVacation)

                'Se crea el detalle
                With VoucherTransactionDetail
                    .IdThirdParty = Employee.ThirdPartyId
                    VoucherTransactionDetail.IdExpenseConcept = PayrollSettings.IdExpenseConceptsVacation
                    VoucherTransactionDetail.IdMainAccount = ObjExpenseConcepts.IdMainAccount
                    VoucherTransactionDetail.Nature = 1
                    VoucherTransactionDetail.Value = Vacation.VacationValueNet
                    VoucherTransactionDetail.PercentRetention = 0
                    VoucherTransactionDetail.Detail = "Pago de Vacaciones del Empleado " + Employee.ThirdParty.Nit + " - " + Employee.ThirdParty.Name + " del día " + Vacation.VacationStartDate.ToShortDateString + " a " + Vacation.VacationEndDate.ToShortDateString
                End With

                .VoucherTransactionDetails.Add(VoucherTransactionDetail)

                'Ahora creamos la cabecera
                .Code = ""
                .IdThirdParty = group.Company.ThirdPartyId
                .VoucherClass = 1
                .ExpenseType = PayrollSettings.ExpenseTypeVacation
                .Detail = "PAGO DE VACACIONES DE " + Employee.ThirdParty.Nit + " - " + Employee.ThirdParty.Name
                .DocumentDate = Date.Now()

                If PayrollSettings.ExpenseTypeVacation = 1 Then
                    Dim ObjEntityBankAccount = _entityBankAccount.GetEntityBankAccountById(PayrollSettings.IdEntityBankAccountVacation)
                    .IdEntityBankAccount = PayrollSettings.IdEntityBankAccountVacation
                    .IdMainAccount = ObjEntityBankAccount.IdMainAccount
                    .PaymentMethod = PayrollSettings.PaymentMethodVacation
                    .NoteNumber = 1
                    .BankAccountNumber = ObjEntityBankAccount.Number
                    .BankName = ObjEntityBankAccount.Bank.Name
                Else
                    .IdCashRegister = PayrollSettings.IdCashRegisterVacation
                    .IdMainAccount = _cashRegisterRepository.GetCashRegisterById(PayrollSettings.IdCashRegisterVacation).IdMainAccount
                End If

                .Value = VoucherTransaction.VoucherTransactionDetails.Sum(Function(x) x.Value)
                .CheckNumber = 0
                .TaxByMil = 0
                .TaxByMilValue = 0
                .CashRegisterExpense = 0
                .RefundCashRegisterExpense = 0
                .BeneficiaryIdentification = group.Company.ThirdParty.Nit
                .Beneficiary = group.Company.ThirdParty.Name
                .TransactionRelationship = 0
                .CheckReconciled = 0
                .Printed = 0
                .RTEValue = 0
                .IVAValue = 0
                .ICAValue = 0
                .OtherValue = 0
                .IdUnitOperative = Session.IndigoOperatingUnitId
                .Status = 1
                .CreationUser = Session.UserIndigo
                .CreationDate = Date.Now()
                .EmailSent = 0

            End With


            ResultObjJournalVouchers.ObjectEmbbeded = VoucherTransaction
            ResultObjJournalVouchers.StateResult = True

        Catch ex As Exception
            ResultObjJournalVouchers.ObjectEmbbeded = Nothing
            ResultObjJournalVouchers.StateResult = False
            ResultObjJournalVouchers.MessageResult.Add(ex.Message.ToString())
        End Try

        Return ResultObjJournalVouchers
    End Function


#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _noveltyDomain.Dispose()
            End If
            _liquidationRepository = Nothing
            _holidayRepository = Nothing
            _PayrollSettings = Nothing
            _incentivePaymentRepository = Nothing
            _retroactiveRepository = Nothing
            _conceptRepository = Nothing
            _PositionRepository = Nothing
            _employeeRepository = Nothing
            _liquidationDetailRepository = Nothing
            _noveltyDomain = Nothing
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
