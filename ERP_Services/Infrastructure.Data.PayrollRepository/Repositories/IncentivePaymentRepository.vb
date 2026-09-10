'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 11-12-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Payroll.Entities
Imports Domain.Payroll
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity

Public Class IncentivePaymentRepository
    Inherits GenericRepository(Of IncentivePayment)
    Implements IIncentivePaymentRepository

    ''' <summary>
    ''' Contexto de payrrol
    ''' </summary>
    Private _context As IPayrollUnitOfWork

    ''' <summary>
    ''' Repositorio de Liquidación
    ''' </summary>
    ''' <remarks></remarks>
    Private _liquitationRepository As IPayrollLiquidationRepository

    ''' <summary>
    ''' Inicia el contexto de payrrol
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IPayrollUnitOfWork, liquidationRepository As IPayrollLiquidationRepository)
        MyBase.New(context)
        _context = context
        _liquitationRepository = liquidationRepository
    End Sub

    ''' <summary>
    ''' Obtiene una lista de Primas por Fecha Inicio, Fecha Fin y Id del Grupo
    ''' </summary>
    ''' <param name="groupId">Id del Grupo</param>
    ''' <param name="InitialDate">Fecha Inicio Periodo</param>
    ''' <param name="EndDate">Fecha Fin Periodo</param>
    ''' <param name="EmployeeId">Id del Empleado (opcional, para filtrar por empleado específico)</param>
    ''' <returns>Lista de Primas</returns>
    ''' <remarks></remarks>
    Public Function GetIncentivePaymentByPayrollInitialEndDateGroupId(groupId As String, InitialDate As Date, EndDate As Date, Status As Byte, Optional tracking As Boolean = True, Optional EmployeeId As Integer = 0) As List(Of IncentivePayment) Implements IIncentivePaymentRepository.GetIncentivePaymentByPayrollDateGroupId

        Dim ListIncentivePayment As New List(Of IncentivePayment)

        Dim incentivePayment As IQueryable(Of IncentivePayment)

        If tracking Then
            If EmployeeId > 0 Then
                ' Optimización: Filtrar directamente por EmployeeId en la consulta SQL
                incentivePayment = From e In _context.IncentivePayment.Include("Contract").Include("Contract.Employee.ThirdParty").Include("Contract.Bank").Include("Group.Company").Include("Contract.Position").Include("IncentivePaymentDetail").Include("IncentivePaymentDetail.Concept")
                                   Where e.GroupId = groupId And e.PeriodInitialDate = InitialDate And e.PeriodEndDate = EndDate And e.RegisterStatus = Status And e.Contract.EmployeeId = EmployeeId
            Else
                ' Comportamiento original: Traer todos los empleados del grupo
                incentivePayment = From e In _context.IncentivePayment.Include("Contract").Include("Contract.Employee.ThirdParty").Include("Contract.Bank").Include("Group.Company").Include("Contract.Position").Include("IncentivePaymentDetail").Include("IncentivePaymentDetail.Concept")
                                   Where e.GroupId = groupId And e.PeriodInitialDate = InitialDate And e.PeriodEndDate = EndDate And e.RegisterStatus = Status
            End If
        Else
            If EmployeeId > 0 Then
                ' Optimización: Filtrar directamente por EmployeeId en la consulta SQL
                incentivePayment = From e In _context.IncentivePayment.AsNoTracking().Include("Contract").AsNoTracking().Include("Contract.Employee.ThirdParty").AsNoTracking().Include("Contract.Bank").AsNoTracking().Include("Group.Company").AsNoTracking()
                                   Where e.GroupId = groupId And e.PeriodInitialDate = InitialDate And e.PeriodEndDate = EndDate And e.RegisterStatus = Status And e.Contract.EmployeeId = EmployeeId
            Else
                ' Comportamiento original: Traer todos los empleados del grupo
                incentivePayment = From e In _context.IncentivePayment.AsNoTracking().Include("Contract").AsNoTracking().Include("Contract.Employee.ThirdParty").AsNoTracking().Include("Contract.Bank").AsNoTracking().Include("Group.Company").AsNoTracking()
                                   Where e.GroupId = groupId And e.PeriodInitialDate = InitialDate And e.PeriodEndDate = EndDate And e.RegisterStatus = Status
            End If
        End If

        If incentivePayment.Count > 0 Then

            If tracking Then
                For Each ObjIncentivePayment As IncentivePayment In incentivePayment
                    Dim thirdId = (From ge In _context.Employee Where ge.Id = ObjIncentivePayment.Contract.EmployeeId Select ge.ThirdPartyId).FirstOrDefault()
                    ObjIncentivePayment.FullNameEmployee = (From t In _context.ThirdParty Where t.Id = thirdId Select String.Concat(t.Nit, " - ", t.Name)).FirstOrDefault()
                    ObjIncentivePayment.NitEmployee = (From t In _context.ThirdParty Where t.Id = thirdId Select t.Nit).FirstOrDefault()

                    ListIncentivePayment.Add(ObjIncentivePayment)
                Next
            Else
                For Each ObjIncentivePayment As IncentivePayment In incentivePayment
                    Dim thirdId = (From ge In _context.Employee.AsNoTracking Where ge.Id = ObjIncentivePayment.Contract.EmployeeId Select ge.ThirdPartyId).FirstOrDefault()
                    ObjIncentivePayment.FullNameEmployee = (From t In _context.ThirdParty.AsNoTracking Where t.Id = thirdId Select String.Concat(t.Nit, " - ", t.Name)).FirstOrDefault()
                    ObjIncentivePayment.NitEmployee = (From t In _context.ThirdParty.AsNoTracking Where t.Id = thirdId Select t.Nit).FirstOrDefault()

                    ListIncentivePayment.Add(ObjIncentivePayment)
                Next
            End If

            Return ListIncentivePayment
        Else
            Return Nothing
        End If

    End Function

    ''' <summary>
    ''' Obtiene el conteo de liquidaciones de primas (optimizado, sin cargar relaciones)
    ''' </summary>
    Public Function GetIncentivePaymentCountByDateGroup(groupId As String, initialDate As Date, endDate As Date, status As Byte, period As Char) As Integer Implements IIncentivePaymentRepository.GetIncentivePaymentCountByDateGroup
        Dim initialDateOnly = initialDate.Date
        Dim endDateOnly = endDate.Date
        Dim periodInt = CInt(Char.GetNumericValue(period))
        Dim count As Integer = (From e In _context.IncentivePayment
                                Where e.GroupId = groupId And
                                      DbFunctions.TruncateTime(e.PeriodInitialDate) = initialDateOnly And
                                      DbFunctions.TruncateTime(e.PeriodEndDate) = endDateOnly And
                                      e.RegisterStatus = status And
                                      e.Period = periodInt
                                Select e).Count()
        Return count
    End Function


    ''' <summary>
    ''' Obtiene la lista de Primas por Id del Contrato y Fecha Próxima Nómina
    ''' </summary>
    ''' <param name="contractId">Id del Contrato</param>
    ''' <param name="payrollNextDate">Fecha Próxima Nómina</param>
    ''' <returns>Lista de Primas</returns>
    ''' <remarks></remarks>
    Public Function GetIncentivePaymentByContractIdPayrollNextDate(contractId As String, payrollNextDate As Date) As IncentivePayment Implements IIncentivePaymentRepository.GetIncentivePaymentByContractIdPayrollNextDate
        Dim incentivePayment = From e In _context.IncentivePayment
                               Where e.ContractId = contractId And e.PayrollNextDate = payrollNextDate And e.PaymentType = "1"


        Return incentivePayment.FirstOrDefault()

    End Function

    ''' <summary>
    ''' Funciona que retorna la Lista de Fechas de Liquidaciones de Primas Confirmadas y que se pagan por Periodo Independiente
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetConfirmIncentivenDates() As List(Of Date) Implements IIncentivePaymentRepository.GetConfirmIncentivenDates
        Dim dates = (From e In _context.IncentivePayment
                     Where e.PaymentType = "2"
                     Select e.PeriodEndDate).Distinct
        If dates.Count > 0 Then
            Return dates.ToList
        Else
            Return New List(Of Date)
        End If
    End Function


    Public Function GetListIncentivePaymentBankFile(PeriodEndDate As Date) As List(Of IncentivePayment) Implements IIncentivePaymentRepository.GetListIncentivePaymentBankFile
        Dim incentivePayment = From e In _context.IncentivePayment.Include("Contract").Include("Contract.Employee").Include("Contract.Employee.ThirdParty")
                               Where e.PaymentType = "2" And e.PeriodEndDate = PeriodEndDate
        If incentivePayment.Count > 0 Then
            Return incentivePayment.ToList
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
    Public Function GetIncentivePaymentByEmployeeIdLastLiquidation(EmployeeId As Integer) As List(Of IncentivePayment) Implements IIncentivePaymentRepository.GetIncentivePaymentByEmployeeIdLastLiquidation

        Dim incentivePayment = From e In _context.IncentivePayment.Include("Contract.Employee").Include("IncentivePaymentDetail").Include("IncentivePaymentDetail.Concept")
                               Where e.Contract.EmployeeId = EmployeeId Order By e.PeriodEndDate Descending
                               Select e

        If incentivePayment IsNot Nothing Then
            Return incentivePayment.ToList()
        Else
            Return Nothing
        End If

    End Function

    Public Function GetIncentivePaymentByEmployeeIdRetefuente(EmployeeId As Integer, VarYear As Integer) As List(Of IncentivePayment) Implements IIncentivePaymentRepository.GetIncentivePaymentByEmployeeIdRetefuente
        Dim incentivePayment = From e In _context.IncentivePayment.Include("Contract.Employee")
                               Where e.Contract.EmployeeId = EmployeeId And Year(e.PeriodEndDate) = VarYear
                               Select e

        If incentivePayment IsNot Nothing Then
            Return incentivePayment.ToList()
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
    Public Function GetHeadIncentivePayment(GroupId As Integer, ByVal PeriodEndDate As Date, Period As Integer) As List(Of IncentivePayment) Implements IIncentivePaymentRepository.GetHeadIncentivePayment

        Dim ListIncentivePayment As New List(Of IncentivePayment)

        Dim IncentivePayment = From e In _context.IncentivePayment
                               Where e.GroupId = GroupId And e.Period = Period And e.PeriodEndDate = PeriodEndDate And e.RegisterStatus = 1

        If IncentivePayment.Count > 0 Then
            Return IncentivePayment.ToList()
        Else
            Return Nothing
        End If

    End Function

    Public Function GetDetailIncentivePaymentDetail(GroupId As Integer, ByVal PeriodEndDate As Date, Period As Integer) As List(Of IncentivePayment) Implements IIncentivePaymentRepository.GetDetailIncentivePaymentDetail

        ' Query optimizada con Eager Loading de todas las relaciones necesarias
        Dim IncentivePaymentQuery = From e In _context.IncentivePayment _
                                        .Include("IncentivePaymentDetail") _
                                        .Include("Contract") _
                                        .Include("Contract.Employee") _
                                        .Include("Contract.Employee.ThirdParty") _
                                        .Include("Contract.Group") _
                                        .Include("Contract.Position")
                                    Where e.GroupId = GroupId And e.Period = Period And e.PeriodEndDate = PeriodEndDate And e.RegisterStatus = 1

        ' Materializar la query una sola vez
        Dim IncentivePaymentList = IncentivePaymentQuery.ToList()

        If IncentivePaymentList Is Nothing OrElse IncentivePaymentList.Count = 0 Then
            Return Nothing
        End If

        ' Precargar todos los Concepts necesarios en un diccionario (una sola query)
        Dim conceptIds = IncentivePaymentList _
            .Where(Function(x) x.IncentivePaymentDetail IsNot Nothing) _
            .SelectMany(Function(x) x.IncentivePaymentDetail) _
            .Select(Function(d) d.ConceptId) _
            .Distinct() _
            .ToList()

        Dim concepts = _context.Concept _
            .Where(Function(c) conceptIds.Contains(c.Id)) _
            .ToDictionary(Function(c) c.Id)

        ' Asignar propiedades usando las relaciones ya cargadas (sin queries adicionales)
        For Each ObjIncentivePayment As IncentivePayment In IncentivePaymentList

            Dim thirdParty = ObjIncentivePayment.Contract.Employee.ThirdParty
            ObjIncentivePayment.FullNameEmployee = String.Concat(thirdParty.Nit, " - ", thirdParty.Name)
            ObjIncentivePayment.NitEmployee = thirdParty.Nit

            ObjIncentivePayment.GroupName = ObjIncentivePayment.Contract.Group.Name
            ObjIncentivePayment.PositionName = ObjIncentivePayment.Contract.Position.Name
            ObjIncentivePayment.ContractNumber = ObjIncentivePayment.Contract.InitialContractNumber

            ' Asignar Concepts desde el diccionario precargado
            If ObjIncentivePayment.IncentivePaymentDetail IsNot Nothing Then
                For Each ObjIncentivePaymentDetail As IncentivePaymentDetail In ObjIncentivePayment.IncentivePaymentDetail
                    If concepts.ContainsKey(ObjIncentivePaymentDetail.ConceptId) Then
                        Dim concept = concepts(ObjIncentivePaymentDetail.ConceptId)
                        ObjIncentivePaymentDetail.ConceptName = concept.Name
                        ObjIncentivePaymentDetail.ConceptType = concept.ConceptType
                    End If
                Next
            End If

        Next

        Return IncentivePaymentList

    End Function

    Public Function GetIncentivePaymentByContractId(ContractId As Integer) As List(Of IncentivePayment) Implements IIncentivePaymentRepository.GetIncentivePaymentByContractId

        Dim IncentivePayment = From e In _context.IncentivePayment
                               Where e.ContractId = ContractId

        Return IncentivePayment.ToList()

    End Function

    Public Function SP_GenerateJournalVouchersIncentivePayment(GroupId As Integer, PeriodInitialDate As Date, PeriodEndDate As Date, Period As Integer, codeUser As String) As List(Of SP_GenerateJournalVouchersIncentivePayment_Result) Implements IIncentivePaymentRepository.SP_GenerateJournalVouchersIncentivePayment
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_GenerateJournalVouchersIncentivePayment(GroupId, PeriodInitialDate, PeriodEndDate, Period, codeUser).ToList
    End Function
    ''' <summary>
    ''' Funcion para traer el listado de Primas para Pago por Archivo de Bancos
    ''' </summary>
    ''' <param name="BankFile"></param>
    ''' <returns></returns>
    Function GetincentivePaymentBankFileProcess(BankFile As BankFile) As List(Of IncentivePayment) Implements IIncentivePaymentRepository.GetIncentivePaymentBankFileProcess


        ' Obtener los IDs únicos de contratos  de los detalles
        Dim contractIds = BankFile.BankFileDetail.Select(Function(d) d.ContractId).Distinct().ToList()


        'Traer las primas  que coincidan con cualquier contrato 
        Dim incentivePayment = (From e In _context.IncentivePayment _
                            .Include("Contract") _
                            .Include("Contract.Employee") _
                            .Include("Contract.Employee.ThirdParty")
                                Where contractIds.Contains(e.ContractId) AndAlso e.RegisterStatus = 2 AndAlso
                                  e.PaymentType = "2" And e.PeriodEndDate = BankFile.LiquidationDate
                                Select e)

        If incentivePayment.Any() Then
            Return incentivePayment.ToList()
        Else
            Return Nothing
        End If
    End Function
End Class
