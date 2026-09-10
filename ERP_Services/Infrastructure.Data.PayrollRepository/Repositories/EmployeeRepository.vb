'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Cristhian Mauricio Salazar
' Created          : 08-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Data.Entity
Imports System.Threading.Tasks
Imports Domain.Payroll
Imports Domain.Payroll.Entities
Imports Infrastructure.Data.Base

Public Class EmployeeRepository
    Inherits GenericRepository(Of Employee)
    Implements IEmployeeRepository

    ''' <summary>
    ''' Contexto de payrrol
    ''' </summary>
    Private _context As IPayrollUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de payrrol
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IPayrollUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene un empleado atraves del nit del tercero (ASYNC)
    ''' </summary>
    ''' <param name="nit">nit del tercero</param>
    ''' <returns>Task con el Empleado</returns>
    ''' <remarks></remarks>
    Public Async Function GetEmployeeAsync(nit As String) As Task(Of Employee) Implements IEmployeeRepository.GetEmployeeAsync
        Dim thirdParty As ThirdParty
        Dim employee As Employee

        ' Busco la persona ya que si esta no existe no pueden existir ni tercero ni empleado
        Dim queryPerson = Await (From e In _context.Person.Include("PersonLanguage").Include("PersonLanguage.Language") _
                                   .Include("PersonStudy").Include("PersonStudy.StudyCenter").Include("PersonStudy.StudyType").Include("PersonDisability").Include("PersonDisability.Disability") _
                                   .Include("PersonProfession").Include("PersonProfession.Profession").Include("PersonNationality").Include("PersonNationality.Country") _
                                   .Include("Address").Include("Email").Include("Phone").Include("PersonDiagnosedDisease").Include("PersonDiagnosedDisease.DiagnosedDisease") _
                                   .Include("PersonFreeTimeUse").Include("PersonFreeTimeUse.FreeTimeUse").Include("ADTIPOIDENTIFICA") _
                                   .Include("City").Include("City1").Include("EthnicGroups").Include("ReligiousBeliefs") _
                                   .Include("Address.City").Include("Address.Department").Include("PersonStudy.City")
                                 Where e.IdentificationNumber = nit
                                 Select e).FirstOrDefaultAsync()

        If queryPerson IsNot Nothing Then

            If queryPerson.City IsNot Nothing Then
                queryPerson.BirthCityName = queryPerson.City.Name
            End If

            If queryPerson.ReligiousBeliefs IsNot Nothing Then
                queryPerson.ReligiousBeliefsDescription = queryPerson.ReligiousBeliefs.Code + " - " + queryPerson.ReligiousBeliefs.Description
            End If

            If queryPerson.EthnicGroups IsNot Nothing Then
                queryPerson.EthnicGroupsDescription = queryPerson.EthnicGroups.Code + " - " + queryPerson.EthnicGroups.Description
            End If

            If queryPerson.City1 IsNot Nothing Then
                queryPerson.IdentificationCityName = queryPerson.City1.Name
            End If

            If queryPerson.Address IsNot Nothing AndAlso queryPerson.Address.Count > 0 Then
                For Each address In queryPerson.Address
                    If address.Department IsNot Nothing Then
                        address.DepartmentName = address.Department.Name
                    End If
                    If address.City IsNot Nothing Then
                        address.CityName = address.City.Name
                    End If
                Next
            End If

            If queryPerson.PersonStudy IsNot Nothing AndAlso queryPerson.PersonStudy.Any() Then
                For Each study In queryPerson.PersonStudy
                    If study.City IsNot Nothing Then
                        study.CityName = study.City.Name
                    End If
                Next
            End If

            'Ahora busco el tercero que sea tipo natural y que este asociado a la persona ya que sin este el empleado no podria existir
            Dim queryThirdParty = Await (From e In _context.ThirdParty
                                         Where e.PersonId = queryPerson.Id And e.PersonType = 1 And e.Nit = nit
                                         Select e).FirstOrDefaultAsync()
            If queryThirdParty IsNot Nothing Then
                thirdParty = queryThirdParty
                thirdParty.Person = queryPerson
                'Busco el empleado
                Dim queryEmployee = Await (From e In _context.Employee.AsNoTracking() _
                                               .Include("EmployeeType").Include("Contract").Include("Contract.Position").Include("Contract.ContractType") _
                                               .Include("Relationship").Include("Relationship.Kinship").Include("Contract.FundContract").Include("Contract.FundContract.Fund") _
                                               .Include("Contract.Group").Include("Contract.Group.GroupEventConcept").Include("Contract.FunctionalUnit").Include("CostCenter").Include("Contract.Bank") _
                                               .Include("Contract.ContractType.JobBondingType").Include("WorkCenter").Include("Contract.ContractModificationReason") _
                                               .Include("Contract.RetirementReason").Include("Contract.FunctionalUnit.BranchOffice").Include("TradeUnionEmployee").Include("TradeUnionEmployee.TradeUnion") _
                                               .Include("Contract.Group.PayrollParameter")
                                           Where e.ThirdPartyId = thirdParty.Id
                                           Select e).FirstOrDefaultAsync()
                If queryEmployee IsNot Nothing Then
                    employee = queryEmployee
                    If employee.PensionaryTypeId IsNot Nothing Then
                        employee.PensionaryTypeName = Await (From p In _context.PensionaryType Where p.Id = employee.PensionaryTypeId Select p.Name).FirstOrDefaultAsync()
                    End If
                    employee.ThirdParty = thirdParty
                Else ' Si el empleado no existe
                    employee = New Employee()
                    employee.ThirdParty = thirdParty
                End If
            Else 'Si el tercero no existe
                employee = New Employee()
                employee.ThirdParty = New ThirdParty()
                employee.ThirdParty.Person = queryPerson
            End If
        Else 'Si la persona no existe
            employee = New Employee()
            employee.ThirdParty = New ThirdParty()
            employee.ThirdParty.Person = New Person()
            Return employee
        End If
        Return employee
    End Function

    ''' <summary>
    ''' Lista todos los empleados y sus agregados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllEmployee() As List(Of Employee) Implements IEmployeeRepository.ListAllEmployee
        Dim employee = From e In _context.Employee.Include("ThirdParty").Include("ThirdParty.Person")
                       Select e
        Return employee.ToList()
    End Function

    ''' <summary>
    ''' Guarda o actualiza un empleado y todos sus agregados
    ''' </summary>
    ''' <param name="employee">Empleado</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveEmployee(employee As Employee) As Boolean Implements IEmployeeRepository.SaveEmployee
        _context.Employee.ApplyChanges(employee)
        Return True
    End Function

    ''' <summary>
    ''' Obtiene un empleado y los agregaos de contratos y fondos de contratos atraves del nit del tercero
    ''' </summary>
    ''' <param name="nit">nit del tercero</param>
    ''' <returns>Empleado</returns>
    ''' <remarks></remarks>
    Public Function GetEmployeeBasicContract(nit As String) As Employee Implements IEmployeeRepository.GetEmployeeBasicContract
        Dim employee = From e In _context.Employee.Include("ThirdParty").Include("Contract").Include("FundContract")
                       Where e.ThirdParty.Nit = nit
                       Select e
        If employee.Count > 0 Then
            Return employee.SingleOrDefault()
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Lista todos los empleados que tiene una unidad funcional
    ''' </summary>
    ''' <param name="functionalUnitId">Id unidad funcional</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEmployeesByFunctionalUnit(functionalUnitId As Integer) As List(Of Employee) Implements IEmployeeRepository.GetEmployeesByFunctionalUnit
        Dim employees = From e In _context.Employee.Include("CostCenter").Include("ThirdParty").Include("ThirdParty.Person") _
                        .Include("CostCenter.FunctionalUnit").Include("CostCenter.FunctionalUnit.BranchOffice").Include("CostCenter.FunctionalUnit.BranchOffice.Company") _
                        .Include("Contract").Include("Contract.Position").Include("Contract.Group").Include("Contract.Group.GroupEventConcept") _
                        .Include("Contract.Group.PayrollParameter")
                        Where e.Contract.Any(Function(x) x.Valid = True And x.Position.HandlesTurnsChart = True And x.FunctionalUnitId = functionalUnitId)
                        Select e
        Return employees.ToList()
    End Function

    ''' <summary>
    ''' Obtiene un empleado y todos sus agregados atraves del id
    ''' Este metodo fue realizado para el frontal de AuthorizationConcept
    ''' </summary>
    ''' <param name="id">id del empleado</param>
    ''' <returns>Empleado</returns>
    ''' <remarks></remarks>
    Public Function GetEmployeeById(id As Integer, Optional tracking As Boolean = True) As Employee Implements IEmployeeRepository.GetEmployeeById
        Dim qEmployee As IQueryable(Of Employee)
        If tracking = True Then
            qEmployee = From e In _context.Employee.Include("ThirdParty").Include("ThirdParty.Person").Include("Contract").Include("Contract.FundContract").Include("Contract.Group") _
                        .Include("Contract.Group.PayrollParameter").Include("Contract.ContractType").Include("Contract.ContractType.JobBondingType").Include("Contract.Group.GroupEventConcept") _
                        .Include("Contract.Group.Company") _
        .Include("Contract.FunctionalUnit").Include("Contract.FunctionalUnit.CostCenter").Include("Contract.FunctionalUnit.BranchOffice").Include("Contract.Position") _
        .Include("Contract.FundContract.Fund").Include("Contract.Position.ProfessionalRisk").Include("Contract.FundContract.Fund.ThirdParty").Include("CostCenter")
                        Where e.Id = id
                        Select e
        Else
            qEmployee = From e In _context.Employee.AsNoTracking().Include("ThirdParty").AsNoTracking().Include("ThirdParty.Person").AsNoTracking() _
                        .Include("Contract").AsNoTracking().Include("Contract.FundContract").AsNoTracking().Include("Contract.Group").AsNoTracking().Include("Contract.Bank") _
                        .Include("Contract.Group.PayrollParameter").AsNoTracking().Include("Contract.ContractType").AsNoTracking().Include("Contract.ContractType.JobBondingType").AsNoTracking() _
                        .Include("Contract.Group.GroupEventConcept").AsNoTracking().Include("Contract.Group.Company").AsNoTracking().Include("Contract.FunctionalUnit").AsNoTracking() _
                        .Include("Contract.FunctionalUnit.CostCenter").AsNoTracking().Include("Contract.FunctionalUnit.BranchOffice").AsNoTracking() _
                        .Include("Contract.Position").AsNoTracking() _
                        .Include("Contract.FundContract.Fund").AsNoTracking().Include("Contract.Position.ProfessionalRisk").AsNoTracking().Include("CostCenter")
                        Where e.Id = id
                        Select e
        End If
        Dim emp = qEmployee.SingleOrDefault()

        Dim contractListIds = (From ci In _context.Contract
                               Join c In _context.Contract On ci.InitialContractNumber Equals c.InitialContractNumber
                               Where ci.Valid AndAlso ci.Status = 1 AndAlso ci.EmployeeId = id AndAlso c.EmployeeId = id
                               Select New With {c.Id}).Select(Function(m) m.Id).ToList()

        Dim VacationPeriod = (From vp In _context.VacationPeriod.Include("Vacation.VacationDetail")
                              Where contractListIds.Contains(vp.ContractId)
                              Order By vp.InitialDatePeriod
                              Select vp)

        If Not tracking Then
            VacationPeriod = VacationPeriod.AsNoTracking()
        End If

        VacationPeriod.ToList().ForEach(Sub(i) emp.VacationPeriod.Add(i))

        Return emp
    End Function

    ''' <summary>
    ''' Obtiene un empleado y los agregados requeridos para ser mostrados de manera informativa en el frontal de liquidacion de contrato
    ''' </summary>
    ''' <param name="id">id del empleado</param>
    ''' <returns>Empleado</returns>
    Public Function GetEmployeeByIdForContractLiquidation(id As Integer) As Employee Implements IEmployeeRepository.GetEmployeeByIdForContractLiquidation
        Dim qEmployee = From e In _context.Employee.Include("ThirdParty").Include("ThirdParty.Person").Include("Contract").Include("Contract.Group") _
                        .Include("Contract.Group.PayrollParameter").Include("Contract.ContractType").Include("Contract.ContractType.JobBondingType") _
                        .Include("Contract.Group.Company") _
                        .Include("Contract.FunctionalUnit").Include("Contract.FunctionalUnit.BranchOffice").Include("Contract.Position")
                        Where e.Id = id And e.Contract.Any(Function(i) i.Valid = True)
                        Select e

        'And e.Contract.Any(Function(i) i.Status <> CByte(2) And i.Valid = True And (i.ContractType.ContractClass = CByte(3) Or i.ContractType.ContractClass = CByte(4)))


        If qEmployee.Count > 0 Then
            Dim employee = qEmployee.SingleOrDefault

            Dim cc = employee.Contract.Where(Function(i) i.Status <> CByte(2) And (i.ContractType.ContractClass <> 1)).ToList
            employee.Contract.Clear()

            For Each item As Contract In cc

                employee.Contract.Add(item)

            Next

            Return employee

        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Obtiene los empleados pertenecientes a un grupo
    ''' </summary>
    ''' <param name="groupId">id del grupo</param>
    ''' <param name="agregates">para saber si se envian mas agregados o solo el tercero y el contrato</param>
    ''' <param name="SpecificEmployeeId">Id del empleado específico (opcional, para filtrar por un empleado en particular)</param>
    ''' <returns>Lista de Empleado</returns>
    ''' <remarks></remarks>
    Public Function GetEmployeeByGroup(groupId As Integer, Optional agregates As Boolean = True, Optional InitialDate As Date? = Nothing, Optional EndingDate As Date? = Nothing, Optional SpecificEmployeeId As Integer = 0) As List(Of Employee) Implements IEmployeeRepository.GetEmployeeByGroup

        Dim ListStatusContract As New List(Of Integer)
        ListStatusContract.Add(1)
        'ListStatusContract.Add(4)
        ListStatusContract.Add(5)


        If SpecificEmployeeId > 0 Then
            Dim querySpecific = From e In _context.Employee.Include("Contract.FunctionalUnit").Include("ThirdParty").Include("ThirdParty.Person").Include("Contract").Include("Contract.Group") _
                    .Include("Contract.Group.PayrollParameter").Include("Contract.Position").Include("Contract.ContractType").Include("VacationPeriod") _
                    .Include("VacationPeriod.Vacation").Include("Contract.FundContract").Include("Contract.FundContract.Fund.ThirdParty").Include("CostCenter").Include("TradeUnionEmployee").Include("VacationPeriod.Vacation.VacationDetail")
                                Where e.Id = SpecificEmployeeId And e.Contract.Any(Function(x) x.Valid = True And x.GroupId = groupId And x.Status = 1)
                                Select e
            Return querySpecific.ToList()
        ElseIf agregates = True Then
            Dim query = From e In _context.Employee.Include("Contract.FunctionalUnit").Include("ThirdParty").Include("ThirdParty.Person").Include("Contract").Include("Contract.Group") _
                    .Include("Contract.Group.PayrollParameter").Include("Contract.Position").Include("Contract.ContractType").Include("VacationPeriod") _
                    .Include("VacationPeriod.Vacation").Include("Contract.FundContract").Include("Contract.FundContract.Fund.ThirdParty").Include("CostCenter").Include("TradeUnionEmployee").Include("VacationPeriod.Vacation.VacationDetail")
                        Where e.Contract.Any(Function(x) x.Valid = True And x.GroupId = groupId And x.Status = 1)
                        Select e
            Return query.ToList()
        ElseIf InitialDate Is Nothing And EndingDate Is Nothing Then
            Dim query = From e In _context.Employee.Include("ThirdParty").Include("Contract").Include("Contract.Group") _
                    .Include("Contract.Group.PayrollParameter").Include("Contract.FundContract").Include("Contract.ContractType").Include("TradeUnionEmployee").Include("Contract.Position").Include("Contract.FundContract.Fund").Include("VacationPeriod.Vacation.VacationDetail")
                        Where e.Contract.Any(Function(x) x.Valid = True And x.GroupId = groupId)
                        Select e
            Return query.ToList()
        Else
            Dim query = From e In _context.Employee.Include("ThirdParty").Include("Contract").Include("Contract.Group") _
                    .Include("Contract.Group.PayrollParameter").Include("Contract.FundContract").Include("Contract.ContractType").Include("TradeUnionEmployee").Include("Contract.Position").Include("Contract.FundContract.Fund").Include("VacationPeriod.Vacation.VacationDetail")
                        Where e.Contract.Any(Function(x) x.GroupId = groupId And x.ContractEndingDate >= InitialDate And x.JobBondingDate <= EndingDate And ListStatusContract.Any(Function(y) y = x.Status))
                        Select e
            Return query.ToList()
        End If

    End Function

    Public Function GetContractByIncreaseSalary(groupId As Integer, Optional FunctionalUnitId As Integer = 0, Optional PositionId As Integer = 0) As List(Of Contract) Implements IEmployeeRepository.GetContractByIncreaseSalary

        Dim ListReturn As New List(Of Contract)

        Dim ListEmployee = From e In _context.Contract.Include("FunctionalUnit").Include("Group").Include("Group.PayrollParameter").Include("Employee").Include("Employee.ThirdParty").Include("Position").Include("FunctionalUnit")
                           Where e.Valid = True And e.GroupId = groupId And e.Status = 1
                           Select e

        'ListEmployee.ToList()

        If ListEmployee IsNot Nothing AndAlso ListEmployee.Count > 0 Then
            If FunctionalUnitId > 0 Then
                ListReturn = ListEmployee.Where(Function(x) x.FunctionalUnitId = FunctionalUnitId).ToList()
            Else
                ListReturn = ListEmployee.ToList()
            End If
        End If

        If ListReturn IsNot Nothing AndAlso ListReturn.Count > 0 Then
            If PositionId > 0 Then
                ListReturn = ListEmployee.ToList().Where(Function(x) x.PositionId = PositionId).ToList()
            End If
        End If

        If PositionId = 0 And FunctionalUnitId = 0 Then
            ListReturn = ListEmployee.ToList()
        End If

        Return ListReturn

    End Function

    ''' <summary>
    ''' Funcion para obtener el contrato actual del empleado
    ''' </summary>
    ''' <param name="employeeId">Id del empleado</param>
    ''' <returns>Contrato</returns>
    ''' <remarks></remarks>
    Public Function GetContractValidByEmployee(employeeId As Integer) As Contract Implements IEmployeeRepository.GetContractValidByEmployee
        'No incluir el agregado de empleado ya que me sale error en vacaciones por que ya tengo cargado el mismo objeto
        Dim query = From e In _context.Contract.Include("Employee").Include("FunctionalUnit").Include("FunctionalUnit.BranchOffice")
                    Where e.Valid = True And e.EmployeeId = employeeId
                    Select e
        If query.Count > 0 Then
            Return query.SingleOrDefault()
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Obtiene un empleado y todos sus agregados atraves del id
    ''' Este metodo fue realizado para el frontal de AuthorizationConcept
    ''' </summary>
    ''' <param name="id">id del empleado</param>
    ''' <returns>Empleado</returns>
    ''' <remarks></remarks>
    Public Function GetEmployeeByIdContractLiquidation(id As Integer, Optional tracking As Boolean = True) As Employee Implements IEmployeeRepository.GetEmployeeByIdContractLiquidation
        Dim qEmployee As IQueryable(Of Employee)
        If tracking = True Then
            qEmployee = From e In _context.Employee _
                            .Include("ThirdParty.Person") _
                            .Include("Contract.Group.PayrollParameter") _
                            .Include("Contract.ContractType") _
                            .Include("Contract.Group.GroupEventConcept") _
                            .Include("Contract.FunctionalUnit") _
                            .Include("Contract.Position") _
                            .Include("VacationPeriod.Vacation") _
                            .Include("Contract.FundContract.Fund") _
                            .Include("EmployeeType") _
                            .Include("WorkCenter")
                        Where e.Id = id
                        Select e
        Else
            qEmployee = From e In _context.Employee.AsNoTracking()
                        Where e.Id = id
                        Select e
        End If
        If qEmployee.Count > 0 Then
            Return qEmployee.SingleOrDefault
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Función que devuelve el Empleado y su Grupo Familiar
    ''' </summary>
    ''' <param name="Id">Id del Empleado</param>
    ''' <returns>Employee</returns>
    ''' <remarks></remarks>
    Public Function GetEmployeeRelationshipByEmployeeId(id As Integer) As Employee Implements IEmployeeRepository.GetEmployeeRelationshipByEmployeeId
        Return (From e In _context.Employee.AsNoTracking.Include("Relationship").AsNoTracking
                Where e.Id = id
                Select e).FirstOrDefault()
    End Function

    Public Function GetEmployeeWithGroup(employeeId As Integer) As Employee Implements IEmployeeRepository.GetEmployeeWithGroup
        Dim verdad As Boolean = True
        Dim one As Byte = 1
        Dim employee As Employee = (From e In _context.Employee.AsNoTracking() Where e.Id = employeeId).FirstOrDefault()
        If employee IsNot Nothing AndAlso employee.Id > 0 Then
            Dim contract As Contract = (From c In _context.Contract.AsNoTracking().Include("Group").AsNoTracking() Where c.EmployeeId = employeeId AndAlso c.Valid = verdad AndAlso c.Status = 1 Select c).FirstOrDefault()
            employee.Contract.Add(contract)
        End If
        Return employee
    End Function

    ''' <summary>
    ''' Obtiene los empleados pertenecientes a un grupo
    ''' </summary>
    ''' <param name="groupId">id del grupo</param>
    ''' <param name="agregates">para saber si se envian mas agregados o solo el tercero y el contrato</param>
    ''' <returns>Lista de Empleado</returns>
    ''' <remarks></remarks>
    Public Function GetEmployeePensionary() As List(Of Employee) Implements IEmployeeRepository.GetEmployeePensionary
        Dim query = From e In _context.Employee.Include("Contract")
                    Where e.Pensionary = True And e.Contract.Any(Function(x) x.Valid = True)
                    Select e

        If query IsNot Nothing Then

            For Each ObjEmployee As Employee In query

                Dim ObjThirdParty = (From a In _context.ThirdParty.AsNoTracking Where a.Id = ObjEmployee.ThirdPartyId Select a).FirstOrDefault()

                ObjEmployee.Nit = ObjThirdParty.Nit
                ObjEmployee.EmployeeName = ObjThirdParty.Name

            Next

        End If

        Return query.ToList()

    End Function

    ''' <summary>
    ''' Obtiene un empleado por id sin sus agregados
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function GetEmployeeSimpleById(Id As Integer) As Employee Implements IEmployeeRepository.GetEmployeeSimpleById
        Return (From e In _context.Employee.Include("Contract") Where e.Id = Id Select e).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Funcion para obtener el contrato actual del empleado
    ''' </summary>
    ''' <param name="employeeId">Id del empleado</param>
    ''' <returns>Contrato</returns>
    ''' <remarks></remarks>
    Public Function GetThirdPartyIdByEmployeeId(employeeId As Integer) As Integer Implements IEmployeeRepository.GetThirdPartyIdByEmployeeId
        Return (From e In _context.Employee.AsNoTracking Where e.Id = employeeId Select e.ThirdPartyId).FirstOrDefault()
    End Function

End Class
