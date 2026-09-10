'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Cristhian Mauricio Salazar
' Created          : 14-08-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Payroll.Entities
Imports Domain.Payroll
Public Class ScheduleDetailRepository
    Inherits GenericRepository(Of ScheduleDetail)
    Implements IScheduleDetailRepository

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
    ''' obtiene un detalle del calendario
    ''' </summary>
    ''' <param name="id">id del detalle del calendario</param>
    ''' <returns>ScheduleDetail</returns>
    ''' <remarks></remarks>
    Public Function GetScheduleDetail(id As String, Optional tracking As Boolean = True) As ScheduleDetail Implements IScheduleDetailRepository.GetScheduleDetail
        If tracking Then
            Dim schedule = From e In _context.ScheduleDetail.Include("ScheduleDetailHour").Include("ScheduleTemplate") _
                      .Include("ScheduleDetailHour.ScheduleDetailConcept").Include("FunctionalUnit")
       Where e.Id = id
                      Select e
            If schedule.Count > 0 Then
                Return schedule.SingleOrDefault()
            Else
                Return Nothing
            End If
        Else
            Dim schedule = From e In _context.ScheduleDetail.AsNoTracking
       Where e.Id = id
                      Select e
            If schedule.Count > 0 Then
                Return schedule.SingleOrDefault()
            Else
                Return Nothing
            End If
        End If

    End Function

    ''' <summary>
    ''' obtiene un listado de los detalles que estan dentro de un rango de fechas
    ''' </summary>
    ''' <param name="dateInitial">Fecha Inicial</param>
    ''' <param name="dateEnd">Fecha Final</param>
    ''' <returns>Lista de detalles de un calendario</returns>
    ''' <remarks></remarks>
    Public Function GetScheduleDetailBetweenDate(dateInitial As Date, dateEnd As Date) As List(Of ScheduleDetail) Implements IScheduleDetailRepository.GetScheduleDetailBetweenDate
        Dim schedule = From e In _context.ScheduleDetail.Include("ScheduleDetailHour") _
                       .Include("ScheduleDetailHour.ScheduleDetailConcept").Include("FunctionalUnit")
                        Where e.DateDetail >= dateInitial And e.DateDetail <= dateEnd
                       Select e
        If schedule.Count > 0 Then
            Return schedule.ToList()
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' obtiene el listado de los detalles que estan dentro de un rango de fechas, de un determinado grupo, y que estan marcados como eventos
    ''' </summary>
    ''' <param name="groupId">id del grupo</param>
    ''' <param name="dateInitial">fecha inicio</param>
    ''' <param name="dateEnd">fecha fin</param>
    ''' <returns>los schedule details</returns>
    ''' <remarks></remarks>
    Public Function GetScheduleDetailWithEvents(functionalunitId As Integer, dateInitial As Date, dateEnd As Date) As List(Of ScheduleDetail) Implements IScheduleDetailRepository.GetScheduleDetailWithEvents
        Dim schedule = From e In _context.ScheduleDetail.Include("ScheduleDetailHour") _
                       .Include("ScheduleDetailHour.ScheduleDetailConcept").Include("Group").Include("Employee").Include("Employee.ThirdParty").Include("FunctionalUnit")
                       Where e.ScheduleFunctionalUnitId = functionalunitId And e.DateDetail >= dateInitial And e.DateDetail <= dateEnd _
                        And e.ScheduleDetailHour.Any(Function(x) x.Event = True)
                       Select e
        If schedule.Count > 0 Then
            Return schedule.ToList()
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' obtiene un listado de los detalles de un empleado que estan dentro de un rango de fechas
    ''' </summary>
    ''' <param name="employeeId">Id del empleado</param>
    ''' <param name="dateInitial">Fecha Inicial</param>
    ''' <param name="dateEnd">Fecha Final</param>
    ''' <returns>Lista de detalles de un calendario</returns>
    ''' <remarks></remarks>
    Public Function GetScheduleDetailByEmployeeBetweenDate(employeeId As Integer, dateInitial As Date, dateEnd As Date) As List(Of ScheduleDetail) Implements IScheduleDetailRepository.GetScheduleDetailByEmployeeBetweenDate
        Dim schedule = From e In _context.ScheduleDetail.Include("ScheduleDetailHour").Include("ScheduleTemplate").Include("FunctionalUnit1") _
                       .Include("ScheduleDetailHour.ScheduleDetailConcept").Include("ScheduleDetailHour.ScheduleDetailConcept.Concept").Include("FunctionalUnit")
                        Where e.DateDetail >= dateInitial And e.DateDetail <= dateEnd And e.EmployeeId = employeeId
                       Select e
        If schedule.Count > 0 Then
            Return schedule.ToList()
        Else
            Return New List(Of ScheduleDetail)
        End If
    End Function

    ''' <summary>
    ''' obtiene el numero de horas totales de un empleado en un determinado rango de fechas
    ''' </summary>
    ''' <param name="employeeId">Id del empleado</param>
    ''' <param name="dateInitial">Fecha Inicial</param>
    ''' <param name="dateEnd">Fecha Final</param>
    ''' <returns>Numero de horas que se tiene en general</returns>
    ''' <remarks></remarks>
    Public Function GetHoursNumber_ScheduleDetailByEmployeeBetweenDate(employeeId As Integer, dateInitial As Date, dateEnd As Date) As Integer Implements IScheduleDetailRepository.GetHoursNumber_ScheduleDetailByEmployeeBetweenDate
        Dim HoursNumber As Integer = 0
        Dim _select = (From e In _context.ScheduleDetail
                        Where e.DateDetail >= dateInitial And e.DateDetail <= dateEnd And e.EmployeeId = employeeId
                       Select e)
        If _select.Count > 0 Then
            HoursNumber = _select.Sum(Function(x) x.TotalNumberHours)
        End If
        Return HoursNumber
    End Function

    ''' <summary>
    ''' obtiene un listado de los detalles que estan dentro de un rango de fechas
    ''' </summary>
    ''' <param name="employeeId">Id del empleado</param>
    ''' <param name="functionalUnitId">Id de la unidad funcional</param>
    ''' <param name="dateInitial">Fecha Inicial</param>
    ''' <param name="dateEnd">Fecha Final</param>
    ''' <returns>Lista de detalles de un calendario</returns>
    ''' <remarks></remarks>
    Public Function GetScheduleDetailByEmployeeFuctionalUnitBetweenDate(employeeId As Integer, functionalUnitId As Integer, dateInitial As Date, dateEnd As Date) As List(Of ScheduleDetail) Implements IScheduleDetailRepository.GetScheduleDetailByEmployeeFuctionalUnitBetweenDate
        Dim schedule = From e In _context.ScheduleDetail.Include("ScheduleDetailHour").Include("ScheduleTemplate") _
                       .Include("ScheduleDetailHour.ScheduleDetailConcept").Include("FunctionalUnit")
                        Where e.DateDetail >= dateInitial And e.DateDetail <= dateEnd And e.EmployeeId = employeeId And e.FunctionalUnitId = functionalUnitId
                       Select e
        If schedule.Count > 0 Then
            Return schedule.ToList()
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' obtiene el listado de los detalles que están dentro de un rango de fechas SIN Novedades
    ''' </summary>
    ''' <param name="employeeId">Id del Empleado</param>
    ''' <param name="functionalUnitId">Id de la Unidad Funcional</param>
    ''' <param name="dateInitial">Fecha Inicial</param>
    ''' <param name="dateEnd">Fecha Final</param>
    ''' <returns>Lista de Detalles de un Calendario</returns>
    ''' <remarks></remarks>
    Public Function GetScheduleDetailByEmployeeFuctionalUnitBetweenDateWithoutNovelties(employeeId As Integer, functionalUnitId As Integer, dateInitial As Date, dateEnd As Date) As List(Of ScheduleDetail) Implements IScheduleDetailRepository.GetScheduleDetailByEmployeeFuctionalUnitBetweenDateWithoutNovelties
        Dim schedule = From e In _context.ScheduleDetail.Include("ScheduleDetailHour") _
                       .Include("ScheduleDetailHour.ScheduleDetailConcept").Include("FunctionalUnit")
                        Where e.DateDetail >= dateInitial And e.DateDetail <= dateEnd And e.EmployeeId = employeeId And e.FunctionalUnitId = functionalUnitId And e.TotalNumberHours > 0
                       Select e
        If schedule.Count > 0 Then
            Return schedule.ToList()
        Else
            Return Nothing
        End If
    End Function


    ''' <summary>
    ''' Función para cargar el Listado de Detallas dentro de un Rango de Fechas
    ''' </summary>
    ''' <param name="employeeId">Id del Empleado</param>
    ''' <param name="dateInitial">Fecha Inicio</param>
    ''' <param name="dateEnd">fecha Fin</param>
    ''' <returns>List(Of ScheduleDetail)</returns>
    ''' <remarks></remarks>
    Public Function GetScheduleDetailByEmployeeUnitBetweenDateWithoutNovelties(employeeId As Integer, dateInitial As Date, dateEnd As Date) As List(Of ScheduleDetail) Implements IScheduleDetailRepository.GetScheduleDetailByEmployeeBetweenDateWithoutNovelties
        Dim schedule = From e In _context.ScheduleDetail.AsNoTracking.Include("ScheduleDetailHour").AsNoTracking() _
                        .Include("ScheduleDetailHour.ScheduleDetailConcept").AsNoTracking()
                       Where e.DateDetail >= dateInitial And e.DateDetail <= dateEnd And e.EmployeeId = employeeId And e.TotalNumberHours > 0

        If schedule.Count > 0 Then
            Return schedule.ToList()
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Función para cargar el Listado de Detallas dentro de un Rango de Fechas
    ''' </summary>
    ''' <param name="employeeId">Id del Empleado</param>
    ''' <param name="dateInitial">Fecha Inicio</param>
    ''' <param name="dateEnd">fecha Fin</param>
    ''' <returns>List(Of ScheduleDetail)</returns>
    ''' <remarks></remarks>
    Public Function GetScheduleDetailByEmployeeUnitBetweenDateWithoutNoveltiesLiquidation(employeeId As Integer, dateInitial As Date, dateEnd As Date) As List(Of ScheduleDetail) Implements IScheduleDetailRepository.GetScheduleDetailByEmployeeUnitBetweenDateWithoutNoveltiesLiquidation
        Dim schedule = From e In _context.ScheduleDetail.AsNoTracking().Include("ScheduleDetailHour").AsNoTracking()
                       Where e.DateDetail >= dateInitial And e.DateDetail <= dateEnd And e.EmployeeId = employeeId And e.TotalNumberHours > 0 And e.ScheduleDetailHour.Any(Function(x) x.EventLastMonth = True)

        If schedule.Count > 0 Then
            Return schedule.ToList()
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' obtiene un listado de los detalles de un empleado que este dentro de una novedad
    ''' </summary>
    ''' <param name="employeeId">Id del empleado</param>
    ''' <param name="noveltyId">Id de la novedad</param>
    ''' <returns>Lista de detalles de un calendario</returns>
    ''' <remarks></remarks>
    Public Function GetScheduleDetailTypeNoveltyByEmployeeNoveltyId(employeeId As Integer, noveltyId As Integer) As List(Of ScheduleDetail) Implements IScheduleDetailRepository.GetScheduleDetailTypeNoveltyByEmployeeNoveltyId
        Dim queryNovelty = From e In _context.Novelty.Where(Function(x) x.Id = noveltyId)
        If queryNovelty.Count = 0 Then
            Throw New ArgumentException("El Id " & noveltyId & " no existe como novedad")
        End If
        Dim novelty As Novelty = queryNovelty.SingleOrDefault()
        Dim schedule = From e In _context.ScheduleDetail.Include("ScheduleDetailHour").Include("ScheduleTemplate").Include("FunctionalUnit1") _
                       .Include("ScheduleDetailHour.ScheduleDetailConcept").Include("ScheduleDetailHour.ScheduleDetailConcept.Concept").Include("FunctionalUnit")
                        Where e.DateDetail >= novelty.RealDate And e.DateDetail <= novelty.EndDate And e.EmployeeId = employeeId And e.ScheduleTemplate Is Nothing
                       Select e
        If schedule.Count > 0 Then
            Return schedule.ToList()
        Else
            Return New List(Of ScheduleDetail)
        End If
    End Function

    ''' <summary>
    ''' Obtiene la lista de detalles que tiene un contrato especifico
    ''' </summary>
    ''' <param name="contractId">Id del Contrato</param>
    ''' <returns>Lista de ScheduleDetail</returns>
    ''' <remarks></remarks>
    Public Function GetScheduleDetailByContractId(contractId As Integer) As List(Of ScheduleDetail) Implements IScheduleDetailRepository.GetScheduleDetailByContractId
        Dim query = From e In _context.ScheduleDetail.Include("ScheduleDetailHour").Include("ScheduleDetailHour.ScheduleDetailConcept")
                    Where e.ContractId = contractId
                    Select e
        Return query.ToList()
    End Function

    ''' <summary>
    ''' Obtiene los detalles de calendario de varios empleados en un rango de fechas
    ''' </summary>
    ''' <param name="listIdEmpleoyee">Lista de ids de empleados</param>
    ''' <param name="dateInitial">Fecha Inicial</param>
    ''' <param name="dateEnd">Fecha Final</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetScheduleDetailByListEmployeeBetweenDate(listIdEmpleoyee As List(Of Integer), dateInitial As Date, dateEnd As Date) As List(Of ScheduleDetail) Implements IScheduleDetailRepository.GetScheduleDetailByListEmployeeBetweenDate
        Dim query = From e In _context.ScheduleDetail.Include("ScheduleDetailHour").Include("ScheduleTemplate").Include("FunctionalUnit1") _
                       .Include("ScheduleDetailHour.ScheduleDetailConcept").Include("ScheduleDetailHour.ScheduleDetailConcept.Concept").Include("FunctionalUnit") _
                       .Include("Employee").Include("Employee.ThirdParty")
                       Where listIdEmpleoyee.Any(Function(x) x = e.EmployeeId) = True And e.DateDetail >= dateInitial And e.DateDetail <= dateEnd
                       Select e
        Return query.ToList()
    End Function

    ''' <summary>
    ''' obtiene un listado de los detalles de un empleado 
    ''' </summary>
    ''' <param name="employeeId">Id del empleado</param>
    ''' <returns>Lista de detalles de un calendario</returns>
    ''' <remarks></remarks>
    Public Function GetScheduleDetailByEmployee(employeeId As Integer) As List(Of ScheduleDetail) Implements IScheduleDetailRepository.GetScheduleDetailByEmployee
        Dim query = From e In _context.ScheduleDetail
                    Where e.EmployeeId = employeeId
                    Select e
        Return query.ToList()
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="employeeId"></param>
    ''' <param name="dateInitial"></param>
    ''' <param name="dateEnd"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetScheduleDetailForCostDistributions(employeeId As Integer, dateInitial As Date, dateEnd As Date) As List(Of CostDistributionCostCenter) Implements IScheduleDetailRepository.GetScheduleDetailForCostDistributions
        Dim schedule = From sd In _context.ScheduleDetail Join sdh In _context.ScheduleDetailHour On sd.Id Equals sdh.ScheduleDetailId
                        Join sdc In _context.ScheduleDetailConcept On sdh.Id Equals sdc.ScheduleDetailHourId _
                        Join fu In _context.FunctionalUnit On fu.Id Equals sd.ScheduleFunctionalUnitId _
                        Join acs In _context.AccountingStructure On fu.AccountingStructureId Equals acs.Id _
                        Join cas In _context.ConceptAccountingStructure On acs.Id Equals cas.AccountingStructureId And sdc.ConceptId Equals cas.ConceptId
                        Where sd.DateDetail >= dateInitial And sd.DateDetail <= dateEnd And sd.EmployeeId = employeeId And sd.TotalNumberHours > 0
                        Group By fu.CostCenterId, sdc.ConceptId, cas.AccruedAccount, cas.DeductedAccount
                        Into TotalHoras = Sum(sdh.TotalNumberHours)
                        Select New With {.TotalHours = TotalHoras, .CostCenter = CostCenterId, .ConceptId = ConceptId, .AccruedAccount = AccruedAccount, .DeductedAccount = DeductedAccount}
        Dim ScheduleList = schedule.ToList()

        Dim ListCostDistribution As New List(Of CostDistributionCostCenter)

        For i As Integer = 0 To ScheduleList.Count() - 1
            Dim ObjCostDistribution As New CostDistributionCostCenter

            ObjCostDistribution.AccruedAccount = ScheduleList.Item(i).AccruedAccount
            ObjCostDistribution.ConceptId = ScheduleList.Item(i).ConceptId
            ObjCostDistribution.CostCenterId = ScheduleList.Item(i).CostCenter
            ObjCostDistribution.DeductedAccount = ScheduleList.Item(i).DeductedAccount
            ObjCostDistribution.TotalHours = ScheduleList.Item(i).TotalHours

            ListCostDistribution.Add(ObjCostDistribution)

        Next

        Return ListCostDistribution

    End Function


End Class
