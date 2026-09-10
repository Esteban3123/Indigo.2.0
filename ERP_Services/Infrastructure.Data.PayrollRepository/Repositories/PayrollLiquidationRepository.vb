'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 05-08-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Payroll.Entities
Imports Domain.Payroll
Imports System.Dynamic
Imports System.Data.Entity.Infrastructure

Public Class PayrollLiquidationRepository

    Inherits GenericRepository(Of Liquidation)
    Implements IPayrollLiquidationRepository

    'Devuelve el contexto en este repositorio 
    Private _context As IPayrollUnitOfWork

    ''' <summary>
    '''inicializa la neva instancia d clase.
    ''' </summary>
    ''' <param name="contex">el contexto.</param>
    Public Sub New(ByVal contex As IPayrollUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

    ''' <summary>
    ''' Obtiene la Lista de los empleados que se les paga Nómina
    ''' </summary>
    ''' <param name="groupId">Id del Grupo</param>
    ''' <param name="endDatePayroll">Fecha Final Nómina</param>
    ''' <param name="EmployeeNit">Nit del Empleado</param>
    ''' <returns>Lista de Empleados para liquidar nómina</returns>
    ''' <remarks></remarks>
    Function GetEmployesPayroll(groupId As String, ByVal endDatePayroll As Date, ByVal initialDatePayroll As Date, Optional ByVal EmployeeNit As String = "") As List(Of Contract) Implements IPayrollLiquidationRepository.GetEmployesPayroll

        Dim ReturnEmployee As IQueryable(Of Contract)

        If EmployeeNit = "" Then
            ReturnEmployee = From e In _context.Contract.AsNoTracking.Include("ContractType").AsNoTracking.Include("FundContract").AsNoTracking.Include("FundContract.Fund").AsNoTracking.Include("Employee.ThirdParty").AsNoTracking.Include("Position").AsNoTracking.Include("Position.ProfessionalRisk").AsNoTracking.Include("Group").AsNoTracking.Include("Group.PayrollParameter").AsNoTracking.Include("FunctionalUnit").AsNoTracking.Include("Employee.EmployeeType").AsNoTracking.Include("Employee.WorkCenter")
                             Where e.GroupId = groupId And e.LiquidationPayroll = "1" And e.ContractEndingDate >= initialDatePayroll And e.ContractInitialDate <= endDatePayroll And (e.LastLiquidationDate Is Nothing Or e.LastLiquidationDate < initialDatePayroll) And e.RetirementDate Is Nothing
                             Select e
        Else
            ReturnEmployee = From e In _context.Contract.AsNoTracking.Include("ContractType").AsNoTracking.Include("FundContract").AsNoTracking.Include("FundContract.Fund").AsNoTracking.Include("Employee.ThirdParty").AsNoTracking.Include("Position").AsNoTracking.Include("Position.ProfessionalRisk").AsNoTracking.Include("Group").Include("Group.PayrollParameter").AsNoTracking.Include("FunctionalUnit").AsNoTracking.Include("Employee.EmployeeType").AsNoTracking.Include("Employee.WorkCenter")
                             Where e.GroupId = groupId And e.LiquidationPayroll = "1" And e.Employee.ThirdParty.Nit = EmployeeNit And e.ContractEndingDate >= initialDatePayroll And e.ContractInitialDate <= endDatePayroll And (e.LastLiquidationDate Is Nothing Or e.LastLiquidationDate < initialDatePayroll) And e.RetirementDate Is Nothing
                             Select e
        End If

        If ReturnEmployee.Count > 0 Then
            Return ReturnEmployee.ToList()
        Else
            Return Nothing
        End If

    End Function

    ''' <summary>
    ''' Obtiene la Lista de los empleados que se les paga Nómina
    ''' </summary>
    ''' <param name="groupId">Id del Grupo</param>
    ''' <param name="endDatePayroll">Fecha Final Nómina</param>
    ''' <param name="EmployeeNit">Nit del Empleado</param>
    ''' <returns>Lista de Empleados para liquidar nómina</returns>
    ''' <remarks></remarks>
    Public Function GetEmployesPayrollLiquidation(groupId As String, ByVal endDatePayroll As Date, ByVal initialDatePayroll As Date, Optional ByVal EmployeeNit As String = "") As List(Of Employee) Implements IPayrollLiquidationRepository.GetEmployesPayrollLiquidation

        Dim employee As IQueryable(Of Employee)
        Dim ListEmployee As New List(Of Employee)
        Dim ListStatusContract As New List(Of Integer)
        ListStatusContract.Add(1)
        ListStatusContract.Add(4)
        ListStatusContract.Add(5)

        If EmployeeNit = "" Then
            employee = From e In _context.Employee.AsNoTracking _
                           .Include("Contract.ContractType").AsNoTracking _
                           .Include("Contract.Position.ProfessionalRisk").AsNoTracking _
                           .Include("Contract.FunctionalUnit").AsNoTracking _
                           .Include("TradeUnionEmployee.TradeUnion").AsNoTracking _
                           .Include("EmployeeType").AsNoTracking _
                           .Include("WorkCenter").AsNoTracking
                       Where e.Contract.Any(Function(x) x.GroupId = groupId And x.LiquidationPayroll = "1" And x.ContractEndingDate >= initialDatePayroll And x.ContractInitialDate <= endDatePayroll And (x.LastLiquidationDate Is Nothing Or x.LastLiquidationDate < initialDatePayroll) And ListStatusContract.Any(Function(y) y = x.Status))
                       Select e

            ListEmployee = employee.ToList()

        Else
            employee = From e In _context.Employee.AsNoTracking _
                           .Include("Contract.ContractType").AsNoTracking _
                           .Include("Contract.Position.ProfessionalRisk").AsNoTracking _
                           .Include("Contract.FunctionalUnit").AsNoTracking _
                           .Include("TradeUnionEmployee.TradeUnion").AsNoTracking _
                           .Include("EmployeeType").AsNoTracking _
                           .Include("WorkCenter").AsNoTracking
                       Where e.ThirdParty.Nit = EmployeeNit And e.Contract.Any(Function(x) x.GroupId = groupId And x.LiquidationPayroll = "1" And x.ContractEndingDate >= initialDatePayroll And x.ContractInitialDate <= endDatePayroll And (x.LastLiquidationDate Is Nothing Or x.LastLiquidationDate < initialDatePayroll) And ListStatusContract.Any(Function(y) y = x.Status))
                       Select e

            For Each VarEmployee As Employee In employee
                Dim thirdId = (From ge In _context.Employee Where ge.Id = VarEmployee.Id Select ge.ThirdPartyId).FirstOrDefault()
                VarEmployee.Nit = (From t In _context.ThirdParty Where t.Id = thirdId Select t.Nit).FirstOrDefault()
                VarEmployee.EmployeeName = (From t In _context.ThirdParty Where t.Id = thirdId Select t.Name).FirstOrDefault()

                ListEmployee.Add(VarEmployee)
            Next
        End If

        If ListEmployee.Count > 0 Then
            Return ListEmployee
        Else
            Return Nothing
        End If

    End Function


    ''' <summary>
    ''' Obtiene los Cargos de los Empleados
    ''' </summary>
    ''' <param name="employeeId">Id del Empleado</param>
    ''' <returns>Lista de Cargos</returns>
    ''' <remarks></remarks>
    Public Function GetEmployesPosition(employeeId As String) As Position Implements IPayrollLiquidationRepository.GetEmployesPosition

        Dim position = (From e In _context.Employee.Include("Contract").Include("Contract.Position")
                        Where e.Id = employeeId And e.Contract.Any(Function(x) x.Valid = True)
                        Select e)


        If position.Count > 0 Then
            Return position.ToList().Item(0).Contract.Item(0).Position
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Obtiene la lista de Contratos de Empleados por Id del Empleado
    ''' </summary>
    ''' <param name="employeeId">Id del Empleado</param>
    ''' <returns>Lista de Empleados</returns>
    ''' <remarks></remarks>
    Public Function GetEmployesContract(employeeId As String) As List(Of Contract) Implements IPayrollLiquidationRepository.GetEmployesContract
        Dim contractEmployee = From e In _context.Contract.Include("ContractType").Include("FundContract").Include("FundContract.Fund")
                               Where e.EmployeeId = employeeId
                               Select e


        Return contractEmployee.ToList()

    End Function

    ''' <summary>
    ''' Obtiene para saber si a un empleado se le va a pagar la nómina
    ''' </summary>
    ''' <param name="employeeId">Id dem Empelado</param>
    ''' <param name="PayrollLiquidation">Tipo de Liquidación de Nómina</param>
    ''' <param name="PayrollEndDate">Fecha Fin Nómina</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function GetPayrollValidation(employeeId As String, PayrollLiquidation As Integer, PayrollEndDate As Date) As Boolean Implements IPayrollLiquidationRepository.GetPayrollValidation

        Dim Valida As Boolean = False

        Dim contractEmployee = GetEmployesContract(employeeId)
        Dim ContractInitialDate As Date = contractEmployee.Item(0).ContractInitialDate
        Dim UndefinedContract As String = contractEmployee.Item(0).ContractType.Undefined.ToString()
        'Dim UndefinedContract As String = "False"
        Dim ContractEndingDate As Date = contractEmployee.Item(0).ContractEndingDate

        If UndefinedContract = "True" Then '' Para Contratos Indefinidos
            If DateDiff(DateInterval.Day, ContractInitialDate, PayrollEndDate) >= 0 Then
                Valida = True
            Else
                Valida = False
            End If
        Else '' Para Contratos a término fijo
            If PayrollLiquidation = 1 Then '' Para Nóminas Mensuales
                If DateDiff(DateInterval.Day, ContractEndingDate, PayrollEndDate) <= 31 Then
                    Valida = True
                Else
                    Valida = False
                End If
            ElseIf PayrollLiquidation = 2 Then '' Para Nóminas Quincenales
                If DateDiff(DateInterval.Day, ContractEndingDate, PayrollEndDate) <= 15 Then
                    Valida = True
                Else
                    Valida = False
                End If
            End If
        End If

        Return Valida

    End Function

    ''' <summary>
    ''' Guarda la Liquidación de Nómina
    ''' </summary>
    ''' <param name="liquidation">Objeto Liquidación</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveLiquidation(liquidation As Liquidation) As Boolean Implements IPayrollLiquidationRepository.SaveLiquitadion
        _context.Liquidation.ApplyChanges(liquidation)
        Return True
    End Function

    ''' <summary>
    ''' Lista los empleados que se ha pagado nomina segun fecha de pago y grupo
    ''' </summary>
    ''' <param name="PayrollDateLiquidated">Fecha de Liquidación</param>
    ''' <returns>Lista de Empleados que se les ha pagado una nómina</returns>
    ''' <remarks></remarks>
    Public Function ListLiquitadionByGroupAndDateLiquidated(PayrollDateLiquidated As Date, ByVal groupId As String) As List(Of Liquidation) Implements IPayrollLiquidationRepository.ListLiquitadionByGroupAndDateLiquidated
        Dim employeeLiquidated = From e In _context.Liquidation.AsNoTracking.Include("Contract").AsNoTracking.Include("Contract.Employee").AsNoTracking.Include("Contract.Employee.ThirdParty").AsNoTracking.Include("LiquidationDetail.Concept").AsNoTracking.Include("Group").AsNoTracking.Include("Message").AsNoTracking.Include("Bank").AsNoTracking.Include("Group.Company").AsNoTracking.Include("Group.PayrollParameter").AsNoTracking.Include("Contract.FunctionalUnit").AsNoTracking.Include("Contract.FunctionalUnit.AccountingStructure").AsNoTracking.Include("Contract.FunctionalUnit.AccountingStructure.ConceptAccountingStructure").AsNoTracking.Include("Contract.FundContract").AsNoTracking
                                 Where e.PayrollDateLiquidated = PayrollDateLiquidated And e.GroupId = groupId And e.RegisterStatus = "C"
                                 Select e

        If employeeLiquidated.Count > 0 Then
            Return employeeLiquidated.ToList()
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Lista los empleados que se ha pagado nomina segun fecha de pago y grupo
    ''' </summary>
    ''' <param name="PayrollDateLiquidated">Fecha de Liquidación</param>
    ''' <returns>Lista de Empleados que se les ha pagado una nómina</returns>
    ''' <remarks></remarks>
    Public Function ListLiquitadionByGroupAndDateLiquidatedByPayroll(PayrollDateLiquidated As Date, ByVal groupId As String, ListIdEmployee As List(Of Integer)) As List(Of Liquidation) Implements IPayrollLiquidationRepository.ListLiquitadionByGroupAndDateLiquidatedByPayroll
        Dim employeeLiquidated = From e In _context.Liquidation.AsNoTracking.Include("Contract").AsNoTracking.Include("LiquidationDetail.Concept")
                                 Where e.PayrollDateLiquidated = PayrollDateLiquidated And e.GroupId = groupId And e.RegisterStatus = "C" And ListIdEmployee.Any(Function(x) x = e.EmployeeId)
                                 Select e

        If employeeLiquidated.Count > 0 Then
            Return employeeLiquidated.ToList()
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Lista los empleados que se les ha pagado una Nómina
    ''' </summary>
    ''' <param name="PayrollDateLiquidated">Fecha de Liquidación</param>
    ''' <returns>Lista de Empleados que se les ha pagado una nómina</returns>
    ''' <remarks>Modificado el 21-11-2014</remarks>
    Public Function ListLiquitadionEmployee(PayrollDateLiquidated As Date, ByVal groupId As String) As List(Of Liquidation) Implements IPayrollLiquidationRepository.GetEmployeeLiquidated

        Dim ListLiquidation As New List(Of Liquidation)

        Dim employeeLiquidated = From e In _context.Liquidation.Include("LiquidationDetail").Include("Message")
                                 Where e.PayrollDateLiquidated = PayrollDateLiquidated And e.GroupId = groupId And e.RegisterStatus = "C"
                                 Select e


        If employeeLiquidated.ToList().Count() > 0 Then
            For Each Liquidation As Liquidation In employeeLiquidated
                Dim thirdId = (From ge In _context.Employee Where ge.Id = Liquidation.EmployeeId Select ge.ThirdPartyId).FirstOrDefault()
                Liquidation.FullNameEmployee = (From t In _context.ThirdParty Where t.Id = thirdId Select String.Concat(t.Nit, " - ", t.Name)).FirstOrDefault()
                Liquidation.NitEmployee = (From t In _context.ThirdParty Where t.Id = thirdId Select t.Nit).FirstOrDefault()

                ListLiquidation.Add(Liquidation)
            Next

            Return ListLiquidation
        Else
            Return Nothing
        End If


    End Function

    ''' <summary>
    ''' Obtiene una validación para saber si la nómina ya fue Confirmada o no
    ''' </summary>
    ''' <param name="PayrollDateLiquidated">Fecha de la Nómina</param>
    ''' <param name="groupId">Id del Grupo</param>
    ''' <returns>Liquitadion</returns>
    ''' <remarks></remarks>
    Public Function PayrollNotCheckLiquidated(PayrollDateLiquidated As Date, groupId As String) As List(Of Liquidation) Implements IPayrollLiquidationRepository.PayrollNotCheckLiquidated
        Dim payrollLiquidated = From e In _context.Liquidation.Include("LiquidationDetail").Include("Message")
                                Where e.PayrollDateLiquidated = PayrollDateLiquidated And e.GroupId = groupId And e.RegisterStatus = "C"
                                Select e


        Return payrollLiquidated.ToList()

    End Function

    ''' <summary>
    ''' Lista de Liquidaciones ya confirmadas de un Empleado
    ''' </summary>
    ''' <param name="employeeId">Id del Empleado</param>
    ''' <returns>Lista de Liquidaciones</returns>
    ''' <remarks></remarks>
    Public Function EmployeePayrollCheckLiquidation(employeeId As String) As List(Of Liquidation) Implements IPayrollLiquidationRepository.EmployeePayrollCheckLiquidation
        Dim employeePayrollCheck = From e In _context.Liquidation.Include("LiquidationDetail")
                                   Where e.Contract.EmployeeId = employeeId And e.RegisterStatus = "C"
                                   Select e

        If employeePayrollCheck.Count > 0 Then
            Return employeePayrollCheck.ToList()
        Else
            Return New List(Of Liquidation)
        End If

    End Function

    ''' <summary>
    ''' Lista de Liquidaciones Sin Confirmar
    ''' </summary>
    ''' <param name="PayrollDateLiquidated">Fecha de Liquidación </param>
    ''' <param name="groupId">Id del Grupo</param>
    ''' <returns>Lista de Liquidaciones</returns>
    ''' <remarks></remarks>
    Public Function PayrollNotCheckLiquidatedToDelete(PayrollDateLiquidated As Date, groupId As String) As List(Of Liquidation) Implements IPayrollLiquidationRepository.PayrollNotCheckLiquidatedToDelete

        Dim ListLiquidation As New List(Of Liquidation)

        Dim ArrayGroups() As String = Split(groupId, ",")

        If ArrayGroups IsNot Nothing Then
            For G As Integer = 0 To (ArrayGroups.Count() - 1)
                groupId = ArrayGroups.GetValue(G)

                Dim payrollLiquidated = From e In _context.Liquidation.Include("LiquidationDetail").Include("Message")
                                        Where e.PayrollDateLiquidated = PayrollDateLiquidated And e.GroupId = groupId And e.RegisterStatus = ""
                                        Select e

                If payrollLiquidated.ToList().Count() > 0 Then
                    For Each Liquidation As Liquidation In payrollLiquidated
                        Dim thirdId = (From ge In _context.Employee Where ge.Id = Liquidation.EmployeeId Select ge.ThirdPartyId).FirstOrDefault()
                        Liquidation.FullNameEmployee = (From t In _context.ThirdParty Where t.Id = thirdId Select String.Concat(t.Nit, " - ", t.Name)).FirstOrDefault()
                        Liquidation.NitEmployee = (From t In _context.ThirdParty Where t.Id = thirdId Select t.Nit).FirstOrDefault()

                        ListLiquidation.Add(Liquidation)
                    Next



                End If
            Next
            Return ListLiquidation
        Else
            Return Nothing
        End If

    End Function

    ''' <summary>
    ''' Optioene la ultima fecha de liquidacion de un grupo
    ''' </summary>
    ''' <param name="groupId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function PayrollMaxDateLiquidation(groupId As String) As Date Implements IPayrollLiquidationRepository.PayrollMaxDateLiquidation
        Dim payrollLiquidated = From e In _context.Liquidation
                                Where e.GroupId = groupId And e.RegisterStatus = "C"
                                Select e.PayrollDateLiquidated


        Return payrollLiquidated.Max()
    End Function


    ''' <summary>
    ''' Función que me consulta si un Contrato tiene una Nómina Confirmada
    ''' </summary>
    ''' <param name="contractId">Id del Contrato</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function LiquidationConfirmatedByContract(ByVal contractId As String) As Boolean Implements IPayrollLiquidationRepository.LiquidationConfirmatedByContract
        Dim payrollLiquidated = From e In _context.Liquidation
                                Where e.ContractId = contractId And e.RegisterStatus = "C"

        If payrollLiquidated.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    Public Function LiquidationByContract(ByVal contractId As String) As Boolean Implements IPayrollLiquidationRepository.LiquidationByContract
        Dim payrollLiquidated = From e In _context.Liquidation
                                Where e.ContractId = contractId

        If payrollLiquidated.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>
    ''' Obtiene las liquidacion de un contrato de los ultimos meses que le envien como parametro
    ''' </summary>
    ''' <param name="contractId">Id del contrato</param>
    ''' <param name="numberLastMonth">numero de meses</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function LiquidationLastMonths(contractId As Integer, numberLastMonth As Integer, PayrollDate As Date) As List(Of Liquidation) Implements IPayrollLiquidationRepository.LiquidationLastMonths
        Dim dateMonth As Date = PayrollDate
        dateMonth = dateMonth.AddDays(-dateMonth.Day + 1)
        Dim initialDateMonth = dateMonth.AddMonths(-numberLastMonth)
        Dim payrollLiquidated = From e In _context.Liquidation.Include("LiquidationDetail").Include("LiquidationDetail.Concept").Include("Group")
                                Where e.InitialContractNumber = contractId And e.RegisterStatus <> "" And e.PayrollDateLiquidated >= initialDateMonth And e.PayrollDateLiquidated <= dateMonth
        If payrollLiquidated.Count > 0 Then
            Return payrollLiquidated.ToList()
        Else
            Return New List(Of Liquidation)
        End If
    End Function

    ''' <summary>
    ''' Función Para Eliminar Liquidaciones por Id del Contrato
    ''' </summary>
    ''' <param name="contractId">Id del Contrato</param>
    ''' <returns>Liquidation</returns>
    ''' <remarks></remarks>
    Public Function ContractNotCheckLiquidation(contractId As Integer) As List(Of Liquidation) Implements IPayrollLiquidationRepository.ContractNotCheckLiquidation

        Dim payrollEmployeeLiquidated = From e In _context.Liquidation.Include("LiquidationDetail").Include("Message")
                                        Where e.ContractId = contractId And e.RegisterStatus = "C"
                                        Select e

        If payrollEmployeeLiquidated.Count > 0 Then
            Return Nothing
        End If

        Dim payrollEmployeeNotLiquidated = From e In _context.Liquidation.Include("LiquidationDetail").Include("Message")
                                           Where e.ContractId = contractId And e.RegisterStatus = ""
                                           Select e

        If payrollEmployeeNotLiquidated.Count > 0 Then
            Return payrollEmployeeNotLiquidated.ToList
        Else
            Return Nothing
        End If

    End Function

    ''' <summary>
    ''' Funcion para calcular las liquidacion en cierto rango de fecha
    ''' </summary>
    ''' <param name="contractId">Id del contrato</param>
    ''' <param name="initialDate">Fecha Inicial</param>
    ''' <param name="endDate">Fecha Final</param>
    ''' <returns>Lista de liquidaciones</returns>
    ''' <remarks></remarks>
    Public Function LiquidationByDate(contractId As Integer, initialDate As Date, endDate As Date) As List(Of Liquidation) Implements IPayrollLiquidationRepository.LiquidationByDate
        Dim payrollLiquidated = From e In _context.Liquidation.Include("LiquidationDetail").Include("Contract")
                                Where e.ContractId = contractId And e.PayrollDateLiquidated >= initialDate And e.PayrollDateLiquidated <= endDate
        If payrollLiquidated.Count > 0 Then
            Return payrollLiquidated.ToList()
        Else
            Return New List(Of Liquidation)
        End If
    End Function

    ''' <summary>
    ''' Funcion para calcular las liquidacion en cierto rango de fecha para Primas
    ''' </summary>
    ''' <param name="InitialContractNumber">Id del Contrato Inicial</param>
    ''' <param name="initialDate">Fecha Inicial</param>
    ''' <param name="endDate">Fecha Final</param>
    ''' <returns>Lista de liquidaciones</returns>
    ''' <remarks></remarks>
    Public Function LiquidationByDateIncentivePayment(InitialContractNumber As Integer, initialDate As Date, endDate As Date) As List(Of Liquidation) Implements IPayrollLiquidationRepository.LiquidationByDateIncentivePayment
        Dim payrollLiquidated = From e In _context.Liquidation.Include("LiquidationDetail").Include("Contract").Include("LiquidationDetail.Concept")
                                Where e.Contract.InitialContractNumber = InitialContractNumber And e.PayrollDateLiquidated >= initialDate And e.PayrollDateLiquidated <= endDate
        If payrollLiquidated.Count > 0 Then
            Return payrollLiquidated.ToList()
        Else
            Return New List(Of Liquidation)
        End If
    End Function

    ''' <summary>
    ''' Funcion para calcular las liquidacion por id de empleado en cierto rango de fecha
    ''' </summary>
    ''' <param name="employeeId">Id del empleado</param>
    ''' <param name="initialDate">Fecha Inicial</param>
    ''' <param name="endDate">Fecha Final</param>
    ''' <returns>Lista de liquidaciones</returns>
    ''' <remarks></remarks>
    Public Function LiquidationEmployeeByDate(employeeId As Integer, initialDate As Date, endDate As Date) As List(Of Liquidation) Implements IPayrollLiquidationRepository.LiquidationEmployeeByDate
        Dim payrollLiquidated = From e In _context.Liquidation _
                                    .Include("LiquidationDetail.Concept")
                                Where e.EmployeeId = employeeId And e.PayrollDateLiquidated >= initialDate And e.PayrollDateLiquidated <= endDate And e.RegisterStatus <> ""
        Return payrollLiquidated.ToList()
    End Function

    ''' <summary>
    ''' Liquidaciones por empleado para contrato en determinados estados
    ''' </summary>
    ''' <param name="employeeId"></param>
    ''' <param name="initialDate"></param>
    ''' <param name="endDate"></param>
    ''' <returns></returns>
    Public Function LiquidationEmployeeByDateAndContractStatus(employeeId As Integer, initialDate As Date, endDate As Date, status As List(Of Byte)) As List(Of Liquidation) Implements IPayrollLiquidationRepository.LiquidationEmployeeByDateAndContractStatus
        Dim payrollLiquidated = From e In _context.Liquidation _
                .Include("LiquidationDetail.Concept")
                                Where e.EmployeeId = employeeId And status.Contains(e.Contract.Status) And e.PayrollDateLiquidated >= initialDate And e.PayrollDateLiquidated <= endDate And e.RegisterStatus <> ""
        Return payrollLiquidated.ToList()
    End Function

    ''' <summary>
    ''' Devuelve la fecha menor de liquidaciones de nomina
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetLiquidationMinDate() As Date Implements IPayrollLiquidationRepository.GetLiquidationMinDate
        Dim _count As Integer = _context.Liquidation.Count
        If _count > 0 Then
            Dim _date = (From e In _context.Liquidation Where e.RegisterStatus <> String.Empty
                         Select e).Min(Function(x) x.PayrollDateLiquidated)
            Return _date
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Devuelve la fecha mayor de liquidaciones de nomina
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetLiquidationMaxDate() As Date Implements IPayrollLiquidationRepository.GetLiquidationMaxDate
        Dim _count As Integer = _context.Liquidation.Count
        If _count > 0 Then
            Dim _date = (From e In _context.Liquidation Where e.RegisterStatus <> String.Empty
                         Select e).Max(Function(x) x.PayrollDateLiquidated)
            Return _date
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Obtiene las fechas de liquidacion existentes segun un grupo
    ''' </summary>
    ''' <param name="groupId">id Grupo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetLiquidationDatesByGroup(groupId As Integer) As List(Of Date) Implements IPayrollLiquidationRepository.GetLiquidationDatesByGroup
        Dim dates = (From e In _context.Liquidation
                     Where e.GroupId = groupId
                     Select e.PayrollDateLiquidated).Distinct
        If dates.Count > 0 Then
            Return dates.ToList
        Else
            Return New List(Of Date)
        End If
    End Function

    ''' <summary>
    ''' Obtiene una lista de Liquidaciones Confirmadas por Grupos, Mes Inicial, Mes Final y Año
    ''' </summary>
    ''' <param name="groupId">Id Grupo</param>
    ''' <param name="InitialMonth">Mes Inicial</param>
    ''' <param name="EndMonth">Mes Final</param>
    ''' <param name="YearActive">Año</param>
    ''' <returns>Lista de Liquidaciones</returns>
    ''' <remarks></remarks>
    Public Function LiquidationConfirmatedByGroupAndMonthYear(ByVal groupId As String, ByVal InitialMonth As Integer, ByVal EndMonth As Integer, YearActive As Integer) As List(Of Liquidation) Implements IPayrollLiquidationRepository.LiquidationConfirmatedByGroupAndMonthYear
        Dim payrollLiquidated = From e In _context.Liquidation.Include("Employee").Include("Employee.ThirdParty") _
                               .Include("Employee.ThirdParty.Person").Include("Contract").Include("Group").Include("Group.PayrollParameter").Include("Contract.ContractType").Include("Contract.FundContract").Include("Contract.FundContract.Fund").Include("Contract.Position").Include("Contract.Position.ProfessionalRisk")
                                Where e.GroupId = groupId And e.RegisterStatus = "C" And Month(e.PayrollDateLiquidated) >= InitialMonth And Month(e.PayrollDateLiquidated) <= EndMonth And Year(e.PayrollDateLiquidated) = YearActive And (e.Contract.ContractType.ContractClass = 3 Or e.Contract.ContractType.ContractClass = 4)

        If payrollLiquidated.Count > 0 Then
            Return payrollLiquidated.ToList()
        Else
            Return Nothing
        End If

    End Function

    ''' <summary>
    ''' Funcion que retorna las liquidacion de un periodo especifico
    ''' </summary>
    ''' <param name="periodLiquidation">Periodo de liquidacion</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetLiquidationByPeriod(periodLiquidation As String) As List(Of Liquidation) Implements IPayrollLiquidationRepository.GetLiquidationByPeriod
        Dim year As String = Left(periodLiquidation, 4)
        Dim month As String = Right(periodLiquidation, 2)
        Dim queryLiquidation = From e In _context.Liquidation.Include("Employee").Include("Employee.ThirdParty").Include("Employee.WorkCenter") _
                               .Include("Employee.ThirdParty.Person").Include("Contract").Include("Contract.ContractType") _
                               .Include("Contract.FunctionalUnit").Include("Contract.FunctionalUnit.BranchOffice") _
                               .Include("Contract.FunctionalUnit.BranchOffice.City").Include("Contract.FunctionalUnit.BranchOffice.City.Department") _
                               .Include("Contract.FundContract").Include("Contract.FundContract.Fund").Include("Contract.Position").Include("Contract.Position.ProfessionalRisk") _
                                .Include("LiquidationDetail").Include("Contract.Group.PayrollParameter")
                               Where e.PayrollDateLiquidated.Year = year And e.PayrollDateLiquidated.Month = month And e.RegisterStatus = "C"
                               Select e
        Return queryLiquidation.ToList()
    End Function
    ''' <summary>
    ''' Funcion para consultar las liquidaciones por el filtro de fecha de liquidacion y centro de trabajo 
    ''' </summary>
    ''' <param name="periodLiquidation"></param>
    ''' <param name="workCenter"></param>
    ''' <returns></returns>
    Private Function IPayrollLiquidationRepository_GetLiquidationByPeriodAndWorkCenter(periodLiquidation As String, workCenter As Integer) As List(Of Liquidation) Implements IPayrollLiquidationRepository.GetLiquidationByPeriodAndWorkCenter
        Dim year As String = Left(periodLiquidation, 4)
        Dim month As String = Right(periodLiquidation, 2)
        Dim queryLiquidation = From e In _context.Liquidation.Include("Employee").Include("Employee.ThirdParty").Include("Employee.WorkCenter") _
                               .Include("Employee.ThirdParty.Person").Include("Contract").Include("Contract.ContractType") _
                               .Include("Contract.FunctionalUnit").Include("Contract.FunctionalUnit.BranchOffice") _
                               .Include("Contract.FunctionalUnit.BranchOffice.City").Include("Contract.FunctionalUnit.BranchOffice.City.Department") _
                               .Include("Contract.FundContract").Include("Contract.FundContract.Fund").Include("Contract.Position").Include("Contract.Position.ProfessionalRisk") _
                                .Include("LiquidationDetail").Include("Contract.Group.PayrollParameter")
                               Where e.PayrollDateLiquidated.Year = year And e.PayrollDateLiquidated.Month = month And e.RegisterStatus = "C" And e.WorkCenterId = workCenter
                               Select e
        Return queryLiquidation.ToList()
    End Function

    ''' <summary>
    ''' Función que obtiene todas las Liquidaciones de Determinado Grupo
    ''' </summary>
    ''' <param name="GroupId">Id del Grupo</param>
    ''' <returns>Lista de Liquidaciones</returns>
    ''' <remarks></remarks>
    Public Function GetLiquidationByGrupoId(GroupId As String) As List(Of Liquidation) Implements IPayrollLiquidationRepository.GetLiquidationByGrupoId
        'Dim payrollLiquidated = From e In _context.Liquidation.Include("Group")
        'Where e.GroupId = GroupId And e.RegisterStatus = "C"

        Dim payrollLiquidated = From e In _context.Liquidation.Include("LiquidationDetail").Include("Contract").Include("Contract.Employee").Include("Contract.Employee.ThirdParty").Include("LiquidationDetail.Concept").Include("Group").Include("Message")
                                Where e.GroupId = GroupId And e.RegisterStatus = "C"

        If payrollLiquidated.Count > 0 Then
            Return payrollLiquidated.ToList()
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Función que obtiene todas las Liquidaciones de Determinado Grupo
    ''' </summary>
    ''' <param name="GroupId">Id del Grupo</param>
    ''' <returns>Lista de Liquidaciones</returns>
    ''' <remarks></remarks>
    Public Function GetLiquidationByGrupoIdConsultLiquidation(GroupId As String) As List(Of Liquidation) Implements IPayrollLiquidationRepository.GetLiquidationByGrupoIdConsultLiquidation
        'Dim payrollLiquidated = From e In _context.Liquidation.Include("Group")
        'Where e.GroupId = GroupId And e.RegisterStatus = "C"

        Dim payrollLiquidated = From e In _context.Liquidation.Include("Group")
                                Where e.GroupId = GroupId And e.RegisterStatus = "C"

        If payrollLiquidated.Count > 0 Then
            Return payrollLiquidated.ToList()
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Obtiene un Diccionario con el Id del Grupo y Verdadero en caso de que exista una liquidación EN BORRADOR para el grupo y la Fecha Seleccionada (Válido únicamente para la carga de la Liquidación de Nómina)
    ''' </summary>
    ''' <param name="IdGroup">IdGroup</param>
    ''' <returns>Diccionario</returns>
    ''' <remarks></remarks>
    Public Function GetOnlyLiquidationByGrupoIdDateLiquidation(IdGroup As String, PayrollDateLiquidated As Date) As Dictionary(Of String, Date) Implements IPayrollLiquidationRepository.GetOnlyLiquidationByGrupoIdDateLiquidation
        Dim descriptions As New Dictionary(Of String, Date)


        Dim payrollLiquidated = From e In _context.Liquidation
                                Where e.PayrollDateLiquidated = PayrollDateLiquidated And e.GroupId = IdGroup And e.RegisterStatus = ""

        If payrollLiquidated.Count > 0 Then

            descriptions.Add(payrollLiquidated.FirstOrDefault().GroupId.ToString(), payrollLiquidated.FirstOrDefault().PayrollDateLiquidated)
            'descriptions.Add("Fecha", payrollLiquidated.FirstOrDefault().PayrollDateLiquidated)

            Return descriptions
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Lista los empleados que se ha pagado nomina segun fecha de pago para los Archivos de Bancos
    ''' </summary>
    ''' <param name="PayrollDateLiquidated">Fecha de Liquidación</param>
    ''' <returns>Lista de Empleados que se les ha pagado una nómina</returns>
    ''' <remarks></remarks>
    Public Function ListLiquitadionByDateLiquidatedBankFile(PayrollDateLiquidated As Date, CompanyId As Integer, BankId As Integer) As List(Of Liquidation) Implements IPayrollLiquidationRepository.ListLiquitadionByDateLiquidatedBankFile
        Dim employeeLiquidated = From e In _context.Liquidation.Include("Contract").Include("Contract.Position").Include("Contract.Employee").Include("Contract.Employee.ThirdParty").Include("LiquidationDetail.Concept").Include("Group").Include("Message").Include("Bank").Include("Group.Company").Include("Group.PayrollParameter").Include("Contract.FunctionalUnit").Include("Contract.FunctionalUnit.AccountingStructure").Include("Contract.FunctionalUnit.AccountingStructure.ConceptAccountingStructure")
                                 Where e.PayrollDateLiquidated = PayrollDateLiquidated And e.RegisterStatus = "C" And e.Group.CompanyId = CompanyId And e.BankId = BankId
                                 Select e

        If employeeLiquidated.Count > 0 Then
            Return employeeLiquidated.ToList()
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Obtiene una liquidación por id
    ''' </summary>
    Public Function GetLiquidationById(id As Integer) As Liquidation Implements IPayrollLiquidationRepository.GetLiquidationById
        Dim query = (From l In _context.Liquidation.Include("Employee").AsNoTracking().Include("Contract").AsNoTracking().Include("Contract.Position").AsNoTracking().Include("Contract.FunctionalUnit").AsNoTracking()
                     Where l.Id = id Select l).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            Dim thirdId = (From ge In _context.Employee Where ge.Id = query.EmployeeId Select ge.ThirdPartyId).FirstOrDefault()
            query.FullNameEmployee = (From t In _context.ThirdParty Where t.Id = thirdId Select String.Concat(t.Nit, " - ", t.Name)).FirstOrDefault()
            query.NitEmployee = (From t In _context.ThirdParty Where t.Id = thirdId Select t.Nit).FirstOrDefault()
            Return query
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Obtiene las fechas de liquidacion existentes de las Nóminas Confirmadas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetLiquidationDatesConfirmPayroll() As List(Of Date) Implements IPayrollLiquidationRepository.GetLiquidationDatesConfirmPayroll
        Dim dates = (From e In _context.Liquidation
                     Where e.RegisterStatus = "C"
                     Select e.PayrollDateLiquidated).Distinct
        If dates.Count > 0 Then
            Return dates.ToList
        Else
            Return New List(Of Date)
        End If
    End Function

    ''' <summary>
    ''' Lista los empleados que se ha pagado nomina segun fecha de pago para los Archivos de Bancos
    ''' </summary>
    ''' <param name="PayrollDateLiquidated">Fecha de Liquidación</param>
    ''' <returns>Lista de Empleados que se les ha pagado una nómina</returns>
    ''' <remarks></remarks>
    Public Function ListLiquitadionByDateLiquidatedNationalSavingsFund(PayrollDateLiquidated As Date, CompanyId As Integer) As List(Of Liquidation) Implements IPayrollLiquidationRepository.ListLiquitadionByDateLiquidatedNationalSavingsFund
        Dim employeeLiquidated = From e In _context.Liquidation.Include("Contract").Include("Contract.Employee").Include("Contract.Employee.ThirdParty").Include("Contract.Employee.ThirdParty.Person").Include("Group").Include("Contract.FundContract.Fund.ThirdParty").Include("Contract.FunctionalUnit.BranchOffice.City").Include("Contract.FunctionalUnit.BranchOffice.City.Department").Include("Group.Company").Include("LiquidationDetail").Include("Group.Company.ThirdParty")
                                 Where e.PayrollDateLiquidated = PayrollDateLiquidated And e.RegisterStatus <> "" And e.Group.CompanyId = CompanyId
                                 Select e

        If employeeLiquidated.Count > 0 Then
            Return employeeLiquidated.ToList()
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Obtiene una liquidación por id del empleado, año y mes
    ''' </summary>
    Public Function GetLiquidationByEmployeeIdYearMonth(employeeId As Integer, year As Integer, month As Integer) As Liquidation Implements IPayrollLiquidationRepository.GetLiquidationByEmployeeIdYearMonth
        Dim query = (From l In _context.Liquidation.Include("Employee").AsNoTracking().Include("Contract").AsNoTracking().Include("Contract.Position").AsNoTracking().Include("Contract.FunctionalUnit").AsNoTracking().Include("LiquidationDetail").AsNoTracking().Include("LiquidationDetail.Concept").AsNoTracking()
                     Where l.EmployeeId = employeeId AndAlso l.PayrollDateLiquidated.Year = year AndAlso l.PayrollDateLiquidated.Month = month AndAlso l.RegisterStatus = "C" Select l).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            Dim thirdId = (From ge In _context.Employee Where ge.Id = query.EmployeeId Select ge.ThirdPartyId).FirstOrDefault()
            query.FullNameEmployee = (From t In _context.ThirdParty Where t.Id = thirdId Select String.Concat(t.Nit, " - ", t.Name)).FirstOrDefault()
            Return query
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Obtiene una liquidación por id del empleado, año y mes
    ''' </summary>
    Public Function GetPatronalesValue(employeeId As Integer, year As Integer, month As Integer) As Decimal Implements IPayrollLiquidationRepository.GetPatronalesValue
        Dim query = (From l In _context.Liquidation
                     Join d In _context.LiquidationDetail On d.PayrollId Equals l.Id
                     Join c In _context.Concept On d.ConceptId Equals c.Id
                     Where l.EmployeeId = employeeId AndAlso l.PayrollDateLiquidated.Year = year AndAlso l.PayrollDateLiquidated.Month = month AndAlso l.RegisterStatus = "C"
                     Select l).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            Dim tres As Byte = 3
            Return query.LiquidationDetail.Where(Function(o) o.Concept.ConceptType = tres).Sum(Function(o) o.DeductedValue)
        Else
            Return 0
        End If
    End Function


    ''' <summary>
    ''' Obtiene una Lista de Liquidaciones CONFIRMADAS por Fecha Inicio y Fecha Fin
    ''' </summary>
    ''' <param name="InitialDate">Fecha Inicio</param>
    ''' <param name="EndDate">Fecha fin</param>
    ''' <returns>List(Of Liquidation)</returns>
    ''' <remarks></remarks>
    Public Function GetConfirmLiquidationByStarEndDate(InitialDate As Date, EndDate As Date) As List(Of Liquidation) Implements IPayrollLiquidationRepository.GetConfirmLiquidationByStarEndDate
        Dim payrollLiquidated = From e In _context.Liquidation.Include("LiquidationDetail")
                                Where e.PayrollDateLiquidated >= InitialDate And e.PayrollDateLiquidated <= EndDate And e.RegisterStatus = "C"

        If payrollLiquidated.Count > 0 Then
            Return payrollLiquidated.ToList()
        Else
            Return Nothing
        End If
    End Function

    Public Function DeleteLiquidation(ListLiquidation As List(Of Liquidation)) As Boolean Implements IPayrollLiquidationRepository.DeleteLiquidation

        Dim NewListLiquidationDetail As New List(Of LiquidationDetail)
        Dim NewListMessage As New List(Of Message)

        Try


            For i As Integer = 0 To ListLiquidation.Count() - 1
                For j As Integer = 0 To ListLiquidation.Item(i).LiquidationDetail.Count() - 1
                    NewListLiquidationDetail.Add(ListLiquidation.Item(i).LiquidationDetail(j))
                Next
            Next

            For i As Integer = 0 To ListLiquidation.Count() - 1
                For j As Integer = 0 To ListLiquidation.Item(i).Message.Count() - 1
                    NewListMessage.Add(ListLiquidation.Item(i).Message(j))
                Next
            Next

            'For Each ListLiquidationDetail As LiquidationDetail In (From e In ListLiquidation Select e.LiquidationDetail)
            '    ' ListLiquidationDetail.Add(LiquidationDetail.LiquidationDetail.SingleOrDefault())
            'Next


            _context.LiquidationDetail.RemoveRange(NewListLiquidationDetail)
            _context.Message.RemoveRange(NewListMessage)
            _context.Liquidation.RemoveRange(ListLiquidation)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function SaveListLiquidation(ListLiquidation As List(Of Liquidation)) As Boolean Implements IPayrollLiquidationRepository.SaveListLiquidation
        Dim NewListLiquidationDetail As New List(Of LiquidationDetail)
        Dim NewListMessage As New List(Of Message)

        Try

            For i As Integer = 0 To ListLiquidation.Count() - 1
                For j As Integer = 0 To ListLiquidation.Item(i).LiquidationDetail.Count() - 1
                    NewListLiquidationDetail.Add(ListLiquidation.Item(i).LiquidationDetail(j))
                Next
            Next

            For i As Integer = 0 To ListLiquidation.Count() - 1
                For j As Integer = 0 To ListLiquidation.Item(i).Message.Count() - 1
                    NewListMessage.Add(ListLiquidation.Item(i).Message(j))
                Next
            Next

            'For Each ListLiquidationDetail As LiquidationDetail In (From e In ListLiquidation Select e.LiquidationDetail)
            '    ' ListLiquidationDetail.Add(LiquidationDetail.LiquidationDetail.SingleOrDefault())
            'Next


            '_context.LiquidationDetail.AddRange(NewListLiquidationDetail)
            '_context.Message.AddRange(NewListMessage)
            _context.Liquidation.AddRange(ListLiquidation)
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    ''' <summary>
    ''' Obtiene la Lista de los empleados que se les paga Nómina
    ''' </summary>
    ''' <param name="groupId">Id del Grupo</param>
    ''' <param name="endDatePayroll">Fecha Final Nómina</param>
    ''' <param name="EmployeeNit">Nit del Empleado</param>
    ''' <returns>Lista de Empleados para liquidar nómina</returns>
    ''' <remarks></remarks>
    Public Function GetEmployesIncentivePayment(groupId As String, ByVal endDatePayroll As Date, ByVal initialDatePayroll As Date, Optional ByVal EmployeeNit As String = "") As List(Of Employee) Implements IPayrollLiquidationRepository.GetEmployesIncentivePayment

        Dim employee As IQueryable(Of Employee)

        If EmployeeNit = "" Then
            employee = From e In _context.Employee.Include("Contract").Include("ThirdParty").Include("Contract.Position").Include("EmployeeType").Include("Contract.ContractType").Include("WorkCenter")
                       Where e.Contract.Any(Function(x) x.GroupId = groupId And x.ContractEndingDate >= initialDatePayroll And x.ContractInitialDate <= endDatePayroll And x.RetirementDate Is Nothing And x.Status <> 2)
                       Select e
        Else
            employee = From e In _context.Employee.Include("Contract").Include("ThirdParty").Include("Contract.Position").Include("EmployeeType").Include("Contract.ContractType").Include("WorkCenter")
                       Where e.ThirdParty.Nit = EmployeeNit And e.Contract.Any(Function(x) x.GroupId = groupId And x.ContractEndingDate >= initialDatePayroll And x.ContractInitialDate <= endDatePayroll And x.RetirementDate Is Nothing And x.Status <> 2)
                       Select e
        End If

        If employee.Count > 0 Then
            Return employee.ToList()
        Else
            Return Nothing
        End If

    End Function
    ''' <summary>
    ''' Obtiene el conteo de empleados para liquidar primas (optimizado, sin cargar entidades completas)
    ''' </summary>
    ''' <param name="groupId">Id del Grupo</param>
    ''' <param name="endDatePayroll">Fecha final de liquidación</param>
    ''' <param name="initialDatePayroll">Fecha inicial de liquidación</param>
    ''' <returns>Cantidad de empleados</returns>
    Public Function GetEmployesIncentivePaymentCount(groupId As String, endDatePayroll As Date, initialDatePayroll As Date) As Integer Implements IPayrollLiquidationRepository.GetEmployesIncentivePaymentCount
        Dim count As Integer = (From e In _context.Employee
                                Where e.Contract.Any(Function(x) x.GroupId = groupId And
                                                                x.ContractEndingDate >= initialDatePayroll And
                                                                x.ContractInitialDate <= endDatePayroll And
                                                                x.RetirementDate Is Nothing And
                                                                x.Status <> 2)
                                Select e).Count()

        Return count
    End Function

    ''' <summary>
    ''' Función que obtiene la Lista de Grupos con Fecha para el Frontal de Liquidación, Consulta Liquidación
    ''' </summary>
    ''' <param name="GroupId">Id Grupo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetLiquidationByGroupIdConsultLiquidation(GroupId As String) As Dynamic.ExpandoObject Implements IPayrollLiquidationRepository.GetLiquidationByGroupIdConsultLiquidation
        Dim payrollLiquidated = (From e In _context.Liquidation.Include("Group")
                                 Where e.GroupId = GroupId And e.RegisterStatus = "C" Select New With {e.GroupId, e.Group.Code, e.Group.Name, e.PayrollDateLiquidated}).Distinct().ToList()

        If payrollLiquidated.Count() > 0 Then

            Dim IncreaseSalaryObj As Object = New ExpandoObject()
            IncreaseSalaryObj.List = New List(Of Object)()

            For Each Liquidation As Object In payrollLiquidated
                Dim Aux As Object = New ExpandoObject()
                Aux.GroupId = Liquidation.GroupId
                Aux.GroupCode = Liquidation.Code
                Aux.GroupName = Liquidation.Name
                Aux.PayrollDateLiquidated = Liquidation.PayrollDateLiquidated

                CType(IncreaseSalaryObj.List, List(Of Object)).Add(Aux)

            Next

            Return IncreaseSalaryObj
        Else
            Return Nothing
        End If

    End Function


    ''' <summary>
    ''' Función para Cargar la Cabecera, para el precargue de las Liquidaciones de Nómina, cuando se abre el frontal
    ''' </summary>
    ''' <param name="GroupId"></param>
    ''' <param name="PayrollDateLiquidated"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetHeadLiquidation(GroupId As Integer, ByVal PayrollDateLiquidated As Date) As List(Of Liquidation) Implements IPayrollLiquidationRepository.GetHeadLiquidation
        Dim PayrollLiquidated = From e In _context.Liquidation
                                Where e.PayrollDateLiquidated = PayrollDateLiquidated And e.GroupId = GroupId And e.RegisterStatus = ""

        If PayrollLiquidated IsNot Nothing Then
            Return PayrollLiquidated.ToList()
        Else
            Return Nothing
        End If

    End Function

    Public Function GetDetailMessageLiquidation(GroupId As Integer, ByVal PayrollDateLiquidated As Date) As List(Of Liquidation) Implements IPayrollLiquidationRepository.GetDetailMessageLiquidation

        Dim ListLiquidation As New List(Of Liquidation)
        Dim PayrollLiquidated = From e In _context.Liquidation.AsNoTracking().
                                    Include("LiquidationDetail").AsNoTracking().
                                    Include("Message").AsNoTracking()
                                Where e.PayrollDateLiquidated = PayrollDateLiquidated And e.GroupId = GroupId And e.RegisterStatus = ""

        Dim payrollStartDate As New Date(PayrollDateLiquidated.Year, PayrollDateLiquidated.Month, 1)
        Dim payrollEndDate As Date = PayrollDateLiquidated

        For Each VarLiquidation As Liquidation In PayrollLiquidated
            Dim thirdId = (From ge In _context.Employee.AsNoTracking() Where ge.Id = VarLiquidation.EmployeeId Select ge.ThirdPartyId).FirstOrDefault()
            VarLiquidation.NitEmployee = (From t In _context.ThirdParty.AsNoTracking() Where t.Id = thirdId Select t.Nit).FirstOrDefault()
            VarLiquidation.FullNameEmployee = (From t In _context.ThirdParty.AsNoTracking() Where t.Id = thirdId Select String.Concat(t.Nit, " - ", t.Name)).FirstOrDefault()
            ''Novedades
            Dim employeeNovelty = GetNoveltyByEmployeeIdPayrollLiquidation(VarLiquidation.EmployeeId)
            ' Inicializar acumuladores
            Dim totalCalamityDays As Integer = 0
            Dim totalPaidLeaveDays As Integer = 0
            Dim totalEPSDays As Integer = 0
            Dim totalEmployerDays As Integer = 0

            For Each ObjNovelty In employeeNovelty
                If ObjNovelty.TypeNovelty = 3 And ObjNovelty.LicenseClass = 5 Then ''Licencia calamidad
                    totalCalamityDays += ObjNovelty.Days
                End If
                If ObjNovelty.TypeNovelty = 3 And ObjNovelty.LicenseClass = 1 Then ''Licencia remunerada
                    totalPaidLeaveDays += ObjNovelty.Days
                End If
                If ObjNovelty.TypeNovelty = 1 And ObjNovelty.InabilityClass = 4 Then ''Licencia incapacidad riesgo laboral
                    ' Calcular los días efectivos dentro del periodo de liquidacion
                    If ObjNovelty.RealDate < payrollStartDate Then
                        Dim startDate As Date = If(ObjNovelty.RealDate < payrollStartDate, payrollStartDate, ObjNovelty.RealDate)
                        Dim endDate As Date = If(ObjNovelty.EndDate > payrollEndDate, payrollEndDate, ObjNovelty.EndDate)
                        If startDate <= endDate Then
                            Dim effectiveDays As Integer = (endDate - startDate).Days + 1
                            totalEPSDays += effectiveDays
                        End If
                        totalEmployerDays += ObjNovelty.EmployerDays
                    Else
                        totalEPSDays += ObjNovelty.EPSDays
                        totalEmployerDays += ObjNovelty.EmployerDays
                    End If
                End If
            Next
            VarLiquidation.CalamityDays = totalCalamityDays
            VarLiquidation.PaidLeaveDays = totalPaidLeaveDays
            VarLiquidation.ERPProfessionalDisabilityDays = totalEPSDays
            VarLiquidation.EmployerProfessionalDisabilityDays = totalEmployerDays
            'Vacaciones Compensadas 
            Dim FirstDay As Date = New Date(PayrollDateLiquidated.Year, PayrollDateLiquidated.Month, 1)
            Dim compensatedVacations = GetVacationsByEmployeeAndTypeVacation(VarLiquidation.EmployeeId)
            Dim totalCompensatedDays = 0
            If compensatedVacations IsNot Nothing And compensatedVacations.Count > 0 Then

                compensatedVacations = compensatedVacations.Where(Function(v) v.LiquidationDate.HasValue AndAlso v.LiquidationDate.Value >= FirstDay AndAlso v.LiquidationDate.Value <= PayrollDateLiquidated).ToList()

                For Each vacation In compensatedVacations
                    totalCompensatedDays += vacation.TakenDays
                Next

            End If
            VarLiquidation.VacationDaysInCash = totalCompensatedDays
            ListLiquidation.Add(VarLiquidation)
        Next


        If ListLiquidation IsNot Nothing And ListLiquidation.Count > 0 Then
            Return ListLiquidation
        Else
            Return Nothing
        End If

    End Function

    ''' <summary>
    ''' Obtiene las novedades por empleado
    ''' </summary>
    ''' <param name="EmployeeId"></param>
    ''' <returns></returns>
    Public Function GetNoveltyByEmployeeIdPayrollLiquidation(EmployeeId As Integer) As List(Of Novelty)
        Dim inability = From e In _context.Novelty
                        Where e.EmployeeId = EmployeeId And e.Status <> 1
                        Select e
        Return inability.ToList()
    End Function

    ''' <summary>
    ''' Obtiene las vaciones por empleado
    ''' </summary>
    ''' <param name="employeeId"></param>
    ''' <returns></returns>
    Public Function GetVacationsByEmployeeAndTypeVacation(employeeId As Integer) As List(Of Vacation)
        Dim query = From v In _context.Vacation
                    Join vp In _context.VacationPeriod On v.VacationPeriodId Equals vp.Id
                    Where vp.EmployeeId = employeeId And v.TypeVacation = 1
                    Select v
        Return query.ToList()
    End Function

    ''' <summary>
    ''' Función que carga los fondos por Id
    ''' </summary>
    ''' <param name="IdContract">IdContract</param>
    ''' <returns>Fund</returns>
    ''' <remarks></remarks>
    Public Function GetFundsContractFundByIdFund(IdContract As Integer) As List(Of FundContract) Implements IPayrollLiquidationRepository.GetFundsContractFundByIdFund

        Dim Funds = From e In _context.FundContract.AsNoTracking.Include("Fund").AsNoTracking
                    Where e.ContractId = IdContract
                    Select e

        If Funds IsNot Nothing Then
            Return Funds.ToList()
        Else
            Return Nothing
        End If
    End Function

    Public Function GetThirdParty(IdThirdParty As Integer) As ThirdParty Implements IPayrollLiquidationRepository.GetThirdParty

        Dim thirdPArty = From e In _context.ThirdParty.AsNoTracking()
                         Where e.Id = IdThirdParty
                         Select e

        If thirdPArty IsNot Nothing Then
            Return thirdPArty.FirstOrDefault()
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Obtiene una Lista de Liquidaciones CONFIRMADAS por Fecha Inicio y Fecha Fin (Retroactivo)
    ''' </summary>
    ''' <param name="InitialDate">Fecha Inicio</param>
    ''' <param name="EndDate">Fecha fin</param>
    ''' <param name="idEmployee">Id del Empleado (opcional, para filtrar por empleado específico)</param>
    ''' <returns>List(Of Liquidation)</returns>
    ''' <remarks></remarks>
    Public Function GetConfirmLiquidationByStarEndDateRetroactive(InitialDate As Date, EndDate As Date, GroupId As Integer, Optional idEmployee As Integer = 0) As List(Of Liquidation) Implements IPayrollLiquidationRepository.GetConfirmLiquidationByStarEndDateRetroactive
        Dim payrollLiquidated As IQueryable(Of Liquidation)
        If idEmployee > 0 Then
            ' Optimización: Filtrar directamente por EmployeeId en la consulta SQL
            payrollLiquidated = From e In _context.Liquidation.Include("LiquidationDetail")
                                Where e.PayrollDateLiquidated >= InitialDate And e.PayrollDateLiquidated <= EndDate And e.RegisterStatus <> "" And e.EmployeeId = idEmployee
        Else
            ' Comportamiento original: Filtrar por todos los empleados del grupo
            Dim EmployeeIdList = (From e In _context.Contract Where e.GroupId = GroupId Select e.EmployeeId).Distinct.ToList
            payrollLiquidated = From e In _context.Liquidation.Include("LiquidationDetail")
                                Where e.PayrollDateLiquidated >= InitialDate And e.PayrollDateLiquidated <= EndDate And e.RegisterStatus <> "" And EmployeeIdList.Contains(e.EmployeeId)
        End If

        If payrollLiquidated.Count > 0 Then
            Return payrollLiquidated.ToList()
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Función para la Liquidación de Nómina de un empleado en vacaciones, para que me cargue la prima de servicios
    ''' </summary>
    ''' <param name="ContractId">Id del Contrato</param>
    ''' <returns>IncentivePayment</returns>
    ''' <remarks></remarks>
    Public Function GetLastConceptClass(ContractId As Integer, ConceptClass As String) As LiquidationDetail Implements IPayrollLiquidationRepository.GetLastConceptClass

        Dim ObjLiquidation = From e In _context.LiquidationDetail.Include("Liquidation") Where e.Liquidation.RegisterStatus = "C" And e.ConceptClass = ConceptClass And e.Liquidation.InitialContractNumber = ContractId
                             Order By e.Liquidation.PayrollDateLiquidated Descending
                             Select e

        If ObjLiquidation IsNot Nothing Then
            Return ObjLiquidation.FirstOrDefault()
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Función para la Liquidación de Nómina de un empleado en vacaciones, para que me cargue la prima de servicios de un periodo en adelante
    ''' </summary>
    ''' <param name="ContractId">Id del Contrato</param>
    ''' <returns>IncentivePayment</returns>
    ''' <remarks></remarks>
    Public Function GetLastConceptClassListDate(ContractId As Integer, ConceptClass As String, DateInitial As Date) As List(Of LiquidationDetail) Implements IPayrollLiquidationRepository.GetLastConceptClassListDate

        Dim ObjLiquidation = From e In _context.LiquidationDetail.Include("Liquidation") Where e.ConceptClass = ConceptClass And e.Liquidation.InitialContractNumber = ContractId And e.Liquidation.PayrollDateLiquidated >= DateInitial
                             Order By e.Liquidation.PayrollDateLiquidated Descending
                             Select e

        If ObjLiquidation.Count > 0 Then
            Return ObjLiquidation.ToList()
        Else
            Return New List(Of LiquidationDetail)()
        End If
    End Function

    ''' <summary>
    ''' Lista los empleados que se ha pagado nomina segun fecha de pago y grupo
    ''' </summary>
    ''' <param name="PayrollDateLiquidated">Fecha de Liquidación</param>
    ''' <returns>Lista de Empleados que se les ha pagado una nómina</returns>
    ''' <remarks></remarks>
    Public Function ListLiquitadionByGroupAndDateLiquidatedCostDistribution(PayrollDateLiquidated As Date, ByVal groupId As String) As List(Of Liquidation) Implements IPayrollLiquidationRepository.ListLiquitadionByGroupAndDateLiquidatedCostDistribution
        Dim ListLiquidation As New List(Of Liquidation)

        Dim employeeLiquidated = From e In _context.Liquidation.Include("Contract").Include("Contract.Employee").Include("LiquidationDetail").Include("Contract.FunctionalUnit")
                                 Where e.PayrollDateLiquidated = PayrollDateLiquidated And e.GroupId = groupId And e.RegisterStatus = "C"
                                 Select e

        If employeeLiquidated.Count > 0 Then

            For Each VarLiquidation As Liquidation In employeeLiquidated
                Dim thirdId = (From ge In _context.Employee Where ge.Id = VarLiquidation.EmployeeId Select ge.ThirdPartyId).FirstOrDefault()
                VarLiquidation.NitEmployee = (From t In _context.ThirdParty Where t.Id = thirdId Select t.Nit).FirstOrDefault()
                VarLiquidation.FullNameEmployee = (From t In _context.ThirdParty Where t.Id = thirdId Select String.Concat(t.Nit, " - ", t.Name)).FirstOrDefault()

                ListLiquidation.Add(VarLiquidation)
            Next



            Return ListLiquidation
        Else
            Return Nothing
        End If
    End Function


    ''' <summary>
    ''' Obtiene la Lista de los empleados que se les paga Nómina
    ''' </summary>
    ''' <param name="groupId">Id del Grupo</param>
    ''' <param name="endDatePayroll">Fecha Final Nómina</param>
    ''' <param name="EmployeeNit">Nit del Empleado</param>
    ''' <returns>Lista de Empleados para liquidar nómina</returns>
    ''' <remarks></remarks>
    Public Function GetEmployesIncentivePaymentPrivateCompany(groupId As String, ByVal endDatePayroll As Date, ByVal initialDatePayroll As Date, Optional ByVal EmployeeNit As String = "") As List(Of Employee) Implements IPayrollLiquidationRepository.GetEmployesIncentivePaymentPrivateCompany

        Dim employee As IQueryable(Of Employee)

        If EmployeeNit = "" Then
            employee = From e In _context.Employee.Include("Contract").Include("Contract.ContractType").Include("Contract.FundContract").Include("Contract.FundContract.Fund").Include("ThirdParty").Include("Contract.Position").Include("Contract.Position.ProfessionalRisk").Include("Contract.FunctionalUnit").Include("EmployeeType")
                       Where e.Contract.Any(Function(x) x.GroupId = groupId And x.LiquidationPayroll = "1" And x.ContractEndingDate >= initialDatePayroll And x.ContractInitialDate <= endDatePayroll And x.RetirementDate Is Nothing And x.Status <> 2)
                       Select e
        Else
            employee = From e In _context.Employee.Include("Contract").Include("Contract.ContractType").Include("Contract.FundContract").Include("Contract.FundContract.Fund").Include("ThirdParty").Include("Contract.Position").Include("Contract.Position.ProfessionalRisk").Include("Contract.FunctionalUnit").Include("EmployeeType")
                       Where e.ThirdParty.Nit = EmployeeNit And e.Contract.Any(Function(x) x.GroupId = groupId And x.LiquidationPayroll = "1" And x.ContractEndingDate >= initialDatePayroll And x.ContractInitialDate <= endDatePayroll And x.RetirementDate Is Nothing And x.Status <> 2)
                       Select e
        End If

        If employee.Count > 0 Then
            Return employee.ToList()
        Else
            Return Nothing
        End If

    End Function

    ''' <summary>
    ''' Obtiene una Lista de Liquidaciones CONFIRMADAS por Fecha Inicio y Fecha Fin
    ''' </summary>
    ''' <param name="InitialDate">Fecha Inicio</param>
    ''' <param name="EndDate">Fecha fin</param>
    ''' <returns>List(Of Liquidation)</returns>
    ''' <remarks></remarks>
    Public Function GetConfirmLiquidationByStarEndDateRetefuente(InitialDate As Date, EndDate As Date, IdEmployee As Integer) As List(Of Liquidation) Implements IPayrollLiquidationRepository.GetConfirmLiquidationByStarEndDateRetefuente
        Dim payrollLiquidated = From e In _context.Liquidation.Include("LiquidationDetail").Include("LiquidationDetail.Concept")
                                Where e.PayrollDateLiquidated >= InitialDate And e.PayrollDateLiquidated <= EndDate And e.RegisterStatus = "C" And e.EmployeeId = IdEmployee

        If payrollLiquidated.Count > 0 Then
            Return payrollLiquidated.ToList()
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Función para la Liquidación de Nómina de un empleado en vacaciones, para que me cargue la prima de servicios
    ''' </summary>
    ''' <param name="ContractId">Id del Contrato</param>
    ''' <returns>IncentivePayment</returns>
    ''' <remarks></remarks>
    Public Function GetLastConceptByCode(ContractId As Integer, ConceptCode As String) As LiquidationDetail Implements IPayrollLiquidationRepository.GetLastConceptByCode

        Dim ObjLiquidation = From e In _context.LiquidationDetail.Include("Liquidation") Where e.Liquidation.RegisterStatus = "C" And e.ConceptCode = ConceptCode And e.Liquidation.InitialContractNumber = ContractId
                             Order By e.Liquidation.PayrollDateLiquidated Descending
                             Select e

        If ObjLiquidation IsNot Nothing Then
            Return ObjLiquidation.FirstOrDefault()
        Else
            Return Nothing
        End If
    End Function

    Public Function GetLastConceptClassListDateBetween(ContractId As Integer, ConceptClass As String, DateInitial As Date, endDate As Date) As List(Of LiquidationDetail) Implements IPayrollLiquidationRepository.GetLastConceptClassListDateBetween
        Dim ObjLiquidation = From e In _context.LiquidationDetail.Include("Liquidation").Include("Concept") Where e.Concept.ConceptClass = ConceptClass And e.Liquidation.InitialContractNumber = ContractId And e.Liquidation.PayrollDateLiquidated >= DateInitial _
                                                                                                            And e.Liquidation.PayrollDateLiquidated <= endDate And e.Liquidation.RegisterStatus <> ""
                             Order By e.Liquidation.PayrollDateLiquidated Descending
                             Select e

        If ObjLiquidation.Count > 0 Then
            Return ObjLiquidation.ToList()
        Else
            Return New List(Of LiquidationDetail)()
        End If
    End Function

    Public Function GetLastConceptClassBetweenDate(ConceptClass As String, DateInitial As Date, endDate As Date) As List(Of LiquidationDetail) Implements IPayrollLiquidationRepository.GetLastConceptClassBetweenDate
        Dim ObjLiquidation = From e In _context.LiquidationDetail.Include("Liquidation")
                             Where e.ConceptClass = ConceptClass And e.Liquidation.PayrollDateLiquidated >= DateInitial And e.Liquidation.PayrollDateLiquidated <= endDate And e.Liquidation.RegisterStatus <> " "
                             Order By e.Liquidation.PayrollDateLiquidated Descending
                             Select e

        If ObjLiquidation.Count > 0 Then
            Return ObjLiquidation.ToList()
        Else
            Return New List(Of LiquidationDetail)()
        End If
    End Function

    Public Function GetLastListConceptClassListDateBetween(ContractId As Integer, ListConceptClass As List(Of String), DateInitial As Date, endDate As Date) As List(Of LiquidationDetail) Implements IPayrollLiquidationRepository.GetLastListConceptClassListDateBetween
        Dim ObjLiquidation = From e In _context.LiquidationDetail.Include("Liquidation").Include("Concept") Where ListConceptClass.Contains(e.Concept.ConceptClass) And e.Liquidation.InitialContractNumber = ContractId And e.Liquidation.PayrollDateLiquidated >= DateInitial _
                                                                                                            And e.Liquidation.PayrollDateLiquidated <= endDate And e.Liquidation.RegisterStatus <> ""
                             Order By e.Liquidation.PayrollDateLiquidated Descending
                             Select e

        If ObjLiquidation.Count > 0 Then
            Return ObjLiquidation.ToList()
        Else
            Return New List(Of LiquidationDetail)()
        End If
    End Function

    ''' <summary>
    ''' Obtiene la Lista de los empleados que se les paga Nómina
    ''' </summary>
    ''' <param name="groupId">Id del Grupo</param>
    ''' <param name="endDatePayroll">Fecha Final Nómina</param>
    ''' <param name="EmployeeNit">Nit del Empleado</param>
    ''' <returns>Lista de Empleados para liquidar nómina</returns>
    ''' <remarks></remarks>
    Public Function GetEmployeeLiquidation(IdEmployee As String, ByVal groupId As Integer, ByVal endDatePayroll As Date, ByVal initialDatePayroll As Date) As Employee Implements IPayrollLiquidationRepository.GetEmployeeLiquidation

        Dim employee As IQueryable(Of Employee)

        Dim ListStatusContract As New List(Of Integer)
        ListStatusContract.Add(1)
        ListStatusContract.Add(4)
        ListStatusContract.Add(5)

        employee = From e In _context.Employee.AsNoTracking.Include("Contract").AsNoTracking.Include("Contract.ContractType").AsNoTracking.Include("Contract.Position").AsNoTracking.Include("Contract.Position.ProfessionalRisk").AsNoTracking.Include("Contract.FunctionalUnit").AsNoTracking.Include("TradeUnionEmployee").AsNoTracking.Include("TradeUnionEmployee.TradeUnion").AsNoTracking.Include("EmployeeType").AsNoTracking.Include("WorkCenter").AsNoTracking
                   Where e.Contract.Any(Function(x) x.GroupId = groupId And x.LiquidationPayroll = "1" And x.ContractEndingDate >= initialDatePayroll And x.ContractInitialDate <= endDatePayroll And (x.LastLiquidationDate Is Nothing Or x.LastLiquidationDate < initialDatePayroll) And ListStatusContract.Any(Function(y) y = x.Status))
                   Select e

        For Each VarEmployee As Employee In employee
            Dim thirdId = (From ge In _context.Employee Where ge.Id = VarEmployee.Id Select ge.ThirdPartyId).FirstOrDefault()
            VarEmployee.Nit = (From t In _context.ThirdParty Where t.Id = thirdId Select t.Nit).FirstOrDefault()
            VarEmployee.EmployeeName = (From t In _context.ThirdParty Where t.Id = thirdId Select t.Name).FirstOrDefault()
        Next

        If employee IsNot Nothing Then
            Return employee.FirstOrDefault()
        Else
            Return Nothing
        End If

    End Function

    Public Function SP_DeleteLiquidation(PayrollEndDate As Date, GroupId As Integer, EmployeeNit As String) As Integer Implements IPayrollLiquidationRepository.SP_DeleteLiquidation
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_DeleteLiquidationNoConfirm(PayrollEndDate, GroupId, EmployeeNit)
    End Function

    ''' <summary>
    ''' Obtiene la Lista de los empleados que se les paga Nómina
    ''' </summary>
    ''' <param name="groupId">Id del Grupo</param>
    ''' <param name="endDatePayroll">Fecha Final Nómina</param>
    ''' <param name="EmployeeNit">Nit del Empleado</param>
    ''' <returns>Lista de Empleados para liquidar nómina</returns>
    ''' <remarks></remarks>
    Public Function CountEmployeePayroll(groupId As String, ByVal endDatePayroll As Date, ByVal initialDatePayroll As Date) As List(Of Employee) Implements IPayrollLiquidationRepository.CountEmployeePayroll

        Dim employee As IQueryable(Of Employee)
        Dim ListEmployee As New List(Of Employee)
        Dim ListStatusContract As New List(Of Integer)
        ListStatusContract.Add(1)
        ListStatusContract.Add(4)
        ListStatusContract.Add(5)

        employee = From e In _context.Employee.AsNoTracking.Include("Contract")
                   Where e.Contract.Any(Function(x) x.GroupId = groupId And x.LiquidationPayroll = "1" And x.ContractEndingDate >= initialDatePayroll And x.ContractInitialDate <= endDatePayroll And (x.LastLiquidationDate Is Nothing Or x.LastLiquidationDate < initialDatePayroll) And ListStatusContract.Any(Function(y) y = x.Status))
                   Select e

        ListEmployee = employee.ToList()

        If ListEmployee.Count > 0 Then
            Return ListEmployee
        Else
            Return Nothing
        End If

    End Function

    Public Function GetListEmployesPayrollLiquidation(IdListEmployee As List(Of Integer), groupId As String, ByVal endDatePayroll As Date, ByVal initialDatePayroll As Date) As List(Of Employee) Implements IPayrollLiquidationRepository.GetListEmployesPayrollLiquidation

        Dim employee As IQueryable(Of Employee)
        Dim ListEmployee As New List(Of Employee)
        Dim ListStatusContract As New List(Of Integer)
        ListStatusContract.Add(1)
        ListStatusContract.Add(4)
        ListStatusContract.Add(5)

        employee = From e In _context.Employee.AsNoTracking.Include("Contract").AsNoTracking.Include("Contract.ContractType").AsNoTracking.Include("Contract.Position").AsNoTracking.Include("Contract.Position.ProfessionalRisk").AsNoTracking.Include("Contract.FunctionalUnit").AsNoTracking.Include("Liquidation").AsNoTracking.Include("TradeUnionEmployee").AsNoTracking.Include("TradeUnionEmployee.TradeUnion").AsNoTracking.Include("EmployeeType").AsNoTracking.Include("WorkCenter").AsNoTracking
                   Where IdListEmployee.Any(Function(z) z = e.Id) And e.Contract.Any(Function(x) x.GroupId = groupId And x.LiquidationPayroll = "1" And x.ContractEndingDate >= initialDatePayroll And x.ContractInitialDate <= endDatePayroll And (x.LastLiquidationDate Is Nothing Or x.LastLiquidationDate < initialDatePayroll) And ListStatusContract.Any(Function(y) y = x.Status))
                   Select e

        If employee.Count > 0 Then
            Return employee.ToList()
        Else
            Return Nothing
        End If


    End Function

    Public Function SP_DeleteLiquidationNoConfirm(payrollEndDate As Date, groupId As String, employeeNit As String) As Integer Implements IPayrollLiquidationRepository.SP_DeleteLiquidationNoConfirm
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_DeleteLiquidationNoConfirm(payrollEndDate, groupId, employeeNit)
    End Function

    ''' <summary>
    ''' Obtiene el reporte de detalle de liquidación con conceptos dinámicos como columnas
    ''' </summary>
    ''' <param name="initialDate">Fecha inicial del período</param>
    ''' <param name="endDate">Fecha final del período</param>
    ''' <param name="employeeId">Id del empleado (opcional)</param>
    ''' <param name="groupInitial">Id del grupo inicial para filtrar (opcional)</param>
    ''' <param name="groupFinal">Id del grupo final para filtrar (opcional)</param>
    ''' <param name="branchOfficeInitial">Id de la sucursal inicial para filtrar (opcional)</param>
    ''' <param name="branchOfficeFinal">Id de la sucursal final para filtrar (opcional)</param>
    ''' <param name="registerStatus">Estado de registro de liquidación: 'C' (Confirmados), '' (Sin Confirmar), 'S' (Saldo Inicial), 'T' (Todos) (opcional)</param>
    ''' <param name="session">Valores de sesión para obtener la conexión</param>
    ''' <returns>DataTable con el reporte de liquidación detallado</returns>
    Public Function GetLiquidationDetailReport(initialDate As Date, endDate As Date, Optional employeeId As Integer? = Nothing, Optional groupInitial As Integer? = Nothing, Optional groupFinal As Integer? = Nothing, Optional branchOfficeInitial As Integer? = Nothing, Optional branchOfficeFinal As Integer? = Nothing, Optional registerStatus As Char? = Nothing, Optional session As Infrastructure.CrossCutting.Base.SessionValues = Nothing) As System.Data.DataTable Implements IPayrollLiquidationRepository.GetLiquidationDetailReport
        Dim dtLiquidationDetailReport As New System.Data.DataTable("LiquidationDetailReport")

        Using sqlCnn As New System.Data.SqlClient.SqlConnection(Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(
            Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS_REPORTS,
            String.Empty,
            session.TransactionalContainer,
            False))

            Using sqlCmd As New System.Data.SqlClient.SqlCommand("Payroll.SP_LiquidationDetailReport", sqlCnn)
                sqlCmd.CommandType = System.Data.CommandType.StoredProcedure
                sqlCmd.CommandTimeout = 3600

                Dim sqlPrm As System.Data.SqlClient.SqlParameter

                sqlPrm = New System.Data.SqlClient.SqlParameter
                sqlPrm.ParameterName = "@initialDate"
                sqlPrm.Value = initialDate
                sqlCmd.Parameters.Add(sqlPrm)

                sqlPrm = New System.Data.SqlClient.SqlParameter
                sqlPrm.ParameterName = "@EndDate"
                sqlPrm.Value = endDate
                sqlCmd.Parameters.Add(sqlPrm)

                sqlPrm = New System.Data.SqlClient.SqlParameter
                sqlPrm.ParameterName = "@EmployeeId"
                If employeeId.HasValue Then
                    sqlPrm.Value = employeeId.Value
                Else
                    sqlPrm.Value = DBNull.Value
                End If
                sqlCmd.Parameters.Add(sqlPrm)

                sqlPrm = New System.Data.SqlClient.SqlParameter
                sqlPrm.ParameterName = "@GroupInitial"
                If groupInitial.HasValue Then
                    sqlPrm.Value = groupInitial.Value
                Else
                    sqlPrm.Value = DBNull.Value
                End If
                sqlCmd.Parameters.Add(sqlPrm)

                sqlPrm = New System.Data.SqlClient.SqlParameter
                sqlPrm.ParameterName = "@GroupFinal"
                If groupFinal.HasValue Then
                    sqlPrm.Value = groupFinal.Value
                Else
                    sqlPrm.Value = DBNull.Value
                End If
                sqlCmd.Parameters.Add(sqlPrm)

                sqlPrm = New System.Data.SqlClient.SqlParameter
                sqlPrm.ParameterName = "@BranchOfficeInitial"
                If branchOfficeInitial.HasValue Then
                    sqlPrm.Value = branchOfficeInitial.Value
                Else
                    sqlPrm.Value = DBNull.Value
                End If
                sqlCmd.Parameters.Add(sqlPrm)

                sqlPrm = New System.Data.SqlClient.SqlParameter
                sqlPrm.ParameterName = "@BranchOfficeFinal"
                If branchOfficeFinal.HasValue Then
                    sqlPrm.Value = branchOfficeFinal.Value
                Else
                    sqlPrm.Value = DBNull.Value
                End If
                sqlCmd.Parameters.Add(sqlPrm)

                sqlPrm = New System.Data.SqlClient.SqlParameter
                sqlPrm.ParameterName = "@RegisterStatus"
                sqlPrm.SqlDbType = System.Data.SqlDbType.Char
                sqlPrm.Size = 1
                If registerStatus.HasValue Then
                    sqlPrm.Value = registerStatus.Value
                Else
                    sqlPrm.Value = DBNull.Value
                End If
                sqlCmd.Parameters.Add(sqlPrm)

                sqlCnn.Open()

                Using sqlDR As System.Data.SqlClient.SqlDataReader = sqlCmd.ExecuteReader
                    dtLiquidationDetailReport.Load(sqlDR)
                End Using
            End Using
        End Using

        Return dtLiquidationDetailReport
    End Function

    ''' <summary>
    ''' Obtiene el reporte de talento humano
    ''' </summary>
    ''' <param name="initialDate">Fecha inicial</param>
    ''' <param name="finalDate">Fecha final</param>
    ''' <param name="employeeId">Id del empleado (opcional)</param>
    ''' <returns>Lista de información de empleados</returns>
    Public Function GetReportHumanTalent(initialDate As Date, finalDate As Date, Optional employeeId As Integer? = Nothing) As List(Of SP_ReportHumanTalent_Result) Implements IPayrollLiquidationRepository.GetReportHumanTalent
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Dim employeeIdParam As Integer? = If(employeeId.HasValue AndAlso employeeId.Value > 0, employeeId, Nothing)

        Return _context.SP_ReportHumanTalent(initialDate, finalDate, employeeIdParam).ToList()
    End Function

End Class
