'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Cristhian Mauricio Salazar
' Created          : 05-08-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Payroll.Entities
Imports Domain.Payroll
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Core.Objects

Public Class ScheduleRepository
    Inherits GenericRepository(Of Schedule)
    Implements IScheduleRepository

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
    ''' Devuelve el schedule por id desatachado
    ''' </summary>
    ''' <param name="id">id del schedule</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetScheduleByIdAsNoTracking(id As Integer) As Schedule Implements IScheduleRepository.GetScheduleByIdAsNoTracking
        Dim _schedule = From e In _context.Schedule.AsNoTracking
                        Where e.Id = id
                        Select e

        If _schedule.Count > 0 Then
            Return _schedule.FirstOrDefault
        Else
            Return New Schedule
        End If
    End Function

    ''' <summary>
    ''' Obtiene un horario especifico
    ''' </summary>
    ''' <param name="functionalUnitId">Codigo unidad funcional</param>
    ''' <param name="period">Peridodo del horario</param>
    ''' <returns>Horario</returns>
    ''' <remarks></remarks>
    Public Function GetSchedule(functionalUnitId As String, period As String) As List(Of Schedule) Implements IScheduleRepository.GetSchedule
        Dim scheduleList = From e In _context.Schedule.Include("FunctionalUnit").Include("FunctionalUnit.BranchOffice").Include("Employee").Include("Employee.ThirdParty") _
        .Include("Employee.Contract").Include("Employee.Contract.Position").Include("Employee.Contract.Group").Include("Employee.Contract.Group.GroupEventConcept") _
        .Include("Employee.Contract.Group.PayrollParameter").Include("Employee.ThirdParty.Person") _
        .Include("ScheduleDetail") _
        .Include("ScheduleDetail1") _
        .Include("ScheduleDetail2") _
        .Include("ScheduleDetail3") _
        .Include("ScheduleDetail4") _
        .Include("ScheduleDetail5") _
        .Include("ScheduleDetail6") _
        .Include("ScheduleDetail7") _
        .Include("ScheduleDetail8") _
        .Include("ScheduleDetail9") _
        .Include("ScheduleDetail10") _
        .Include("ScheduleDetail11") _
        .Include("ScheduleDetail12") _
        .Include("ScheduleDetail13") _
        .Include("ScheduleDetail14") _
        .Include("ScheduleDetail15") _
        .Include("ScheduleDetail16") _
        .Include("ScheduleDetail17") _
        .Include("ScheduleDetail18") _
        .Include("ScheduleDetail19") _
        .Include("ScheduleDetail20") _
        .Include("ScheduleDetail21") _
        .Include("ScheduleDetail22") _
        .Include("ScheduleDetail23") _
        .Include("ScheduleDetail24") _
        .Include("ScheduleDetail25") _
        .Include("ScheduleDetail26") _
        .Include("ScheduleDetail27") _
        .Include("ScheduleDetail28") _
        .Include("ScheduleDetail29") _
        .Include("ScheduleDetail30")
        Where e.Period = period And e.FunctionalUnitId = functionalUnitId And e.Employee.Contract.Any(Function(x) x.Valid = True And x.Position.HandlesTurnsChart = True)
        Select e
        Return scheduleList.ToList()
        'Where e.Period = period And (e.FunctionalUnit.Id = functionalUnitId Or e.ScheduleDetail.FunctionalUnitId = functionalUnitId Or _
        'e.ScheduleDetail1.FunctionalUnitId = functionalUnitId Or e.ScheduleDetail2.FunctionalUnitId = functionalUnitId Or _
        'e.ScheduleDetail3.FunctionalUnitId = functionalUnitId Or e.ScheduleDetail4.FunctionalUnitId = functionalUnitId Or _
        'e.ScheduleDetail5.FunctionalUnitId = functionalUnitId Or e.ScheduleDetail6.FunctionalUnitId = functionalUnitId Or _
        'e.ScheduleDetail7.FunctionalUnitId = functionalUnitId Or e.ScheduleDetail8.FunctionalUnitId = functionalUnitId Or _
        'e.ScheduleDetail9.FunctionalUnitId = functionalUnitId Or e.ScheduleDetail10.FunctionalUnitId = functionalUnitId Or _
        'e.ScheduleDetail11.FunctionalUnitId = functionalUnitId Or e.ScheduleDetail12.FunctionalUnitId = functionalUnitId Or _
        'e.ScheduleDetail13.FunctionalUnitId = functionalUnitId Or e.ScheduleDetail14.FunctionalUnitId = functionalUnitId Or _
        'e.ScheduleDetail15.FunctionalUnitId = functionalUnitId Or e.ScheduleDetail16.FunctionalUnitId = functionalUnitId Or _
        'e.ScheduleDetail17.FunctionalUnitId = functionalUnitId Or e.ScheduleDetail18.FunctionalUnitId = functionalUnitId Or _
        'e.ScheduleDetail19.FunctionalUnitId = functionalUnitId Or e.ScheduleDetail20.FunctionalUnitId = functionalUnitId Or _
        'e.ScheduleDetail21.FunctionalUnitId = functionalUnitId Or e.ScheduleDetail22.FunctionalUnitId = functionalUnitId Or _
        'e.ScheduleDetail23.FunctionalUnitId = functionalUnitId Or e.ScheduleDetail24.FunctionalUnitId = functionalUnitId Or _
        'e.ScheduleDetail25.FunctionalUnitId = functionalUnitId Or e.ScheduleDetail26.FunctionalUnitId = functionalUnitId Or _
        'e.ScheduleDetail27.FunctionalUnitId = functionalUnitId Or e.ScheduleDetail28.FunctionalUnitId = functionalUnitId Or _
        'e.ScheduleDetail29.FunctionalUnitId = functionalUnitId Or e.ScheduleDetail30.FunctionalUnitId = functionalUnitId)


    End Function

    ''' <summary>
    ''' Lista todos los horarios
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllSchedule() As List(Of Schedule) Implements IScheduleRepository.ListAllSchedule
        Dim schedule = From e In _context.Schedule.Include("FunctionalUnit").Include("Employee")
                       Select e
        Return schedule.ToList()
    End Function

    ''' <summary>
    ''' funcion para obtener todos lo turnos de un empleado en cierto rango de un periodo
    ''' </summary>
    ''' <param name="idEmployee">id del empleado</param>
    ''' <param name="period">periodo del calendario</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetScheduleByEmployeePeriod(idEmployee As Integer, period As String, diaMin As Integer, diaMaximo As Integer) As List(Of Schedule) Implements IScheduleRepository.GetScheduleByEmployeePeriod

        Dim objSetSchedule = _context.Schedule
        Dim objQuery As DbQuery(Of Schedule)
        objQuery = objSetSchedule.Include("FunctionalUnit")
        For index As Integer = diaMin To diaMaximo Step 1
            If index = 1 Then
                objQuery = objQuery.Include("ScheduleDetail").Include("ScheduleDetail.ScheduleDetailHour").Include("ScheduleDetail.ScheduleDetailHour.ScheduleDetailConcept")
            Else
                objQuery = objQuery.Include("ScheduleDetail" & index - 1).Include("ScheduleDetail" & index - 1 & ".ScheduleDetailHour").Include("ScheduleDetail" & index - 1 & ".ScheduleDetailHour.ScheduleDetailConcept")
            End If
        Next
        Dim query = From e In objQuery
                    Where e.EmployeeId = idEmployee And e.Period = period
                    Select e
        Return query.ToList()
    End Function

    ''' <summary>
    ''' Funcion para obtener todos los turno que hay en un periodo y que tenga plantilla en un dia especifico
    ''' </summary>
    ''' <param name="period">periodo del calendario</param>
    ''' <param name="day">dia que debe tener plantilla</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetScheduleByPeriodDay(period As String, day As Integer, functionalUnitId As Integer) As List(Of Schedule) Implements IScheduleRepository.GetScheduleByPeriodDay
        Dim objSetSchedule = CType(_context, IObjectContextAdapter).ObjectContext.CreateObjectSet(Of Schedule)()
        Dim objQuery As ObjectQuery(Of Schedule) = objSetSchedule.Include("Employee").Include("Employee.ThirdParty").Include("FunctionalUnit").Include("FunctionalUnit.BranchOffice")
        Dim where As String = String.Empty
        Dim parametros As List(Of ObjectParameter) = New List(Of ObjectParameter)()
        If day = 1 Then
            objQuery = objQuery.Include("ScheduleDetail")
            where = "it.ScheduleDetail is not null "
        Else
            objQuery = objQuery.Include("ScheduleDetail" & day - 1)
            where = "it.ScheduleDetail" & day - 1 & " is not null "
        End If
        where += " AND it.Period = @Periodo"
        where += " AND it.FunctionalUnitId = @FunctionalUnitId"
        'parametros.Add(New ObjectParameter("Parametro", 0))
        parametros.Add(New ObjectParameter("Periodo", period))
        parametros.Add(New ObjectParameter("FunctionalUnitId", functionalUnitId))
        Dim query = objQuery.Where(where, parametros.ToArray())
        Return query.ToList()
        'Return objQuery.ToList
    End Function

    ''' <summary>
    ''' funcion la cual obtiene un turno del empleado en un periodo especifico y en una unidad funcional especifica
    ''' </summary>
    ''' <param name="idEmployee">empleado</param>
    ''' <param name="period">Periodo</param>
    ''' <param name="functionalUnitId">unidad funcional</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetScheduleByEmployeePeriodFunctionalUnit(idEmployee As Integer, period As String, functionalUnitId As Integer, Optional IncludeDetails As Boolean = True) As Schedule Implements IScheduleRepository.GetScheduleByEmployeePeriodFunctionalUnit
        Dim objSetSchedule = _context.Schedule
        Dim objQuery As DbQuery(Of Schedule)
        objQuery = objSetSchedule.AsNoTracking.Include("FunctionalUnit").AsNoTracking.Include("FunctionalUnit.BranchOffice").AsNoTracking.Include("Employee").AsNoTracking.Include("Employee.ThirdParty") _
        .AsNoTracking.Include("Employee.Contract").AsNoTracking.Include("Employee.Contract.Position").AsNoTracking.Include("Employee.Contract.Group").AsNoTracking.Include("Employee.Contract.Group.GroupEventConcept") _
        .AsNoTracking.Include("Employee.Contract.Group.PayrollParameter").AsNoTracking
        If IncludeDetails = True Then
            For index As Integer = 1 To 31 Step 1
                If index = 1 Then
                    objQuery = objQuery.Include("ScheduleDetail") '.Include("ScheduleDetail.ScheduleDetailHour").Include("ScheduleDetail.ScheduleDetailHour.ScheduleDetailConcept")
                Else
                    objQuery = objQuery.Include("ScheduleDetail" & index - 1) '.Include("ScheduleDetail" & index - 1 & ".ScheduleDetailHour").Include("ScheduleDetail" & index - 1 & ".ScheduleDetailHour.ScheduleDetailConcept")
                End If
            Next
        End If

        Dim query = From e In objQuery
                    Where e.EmployeeId = idEmployee And e.Period = period And e.FunctionalUnitId = functionalUnitId
                    Select e
        If query.Count() > 0 Then
            Return query.SingleOrDefault()
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' funcion para obtener todos lo turnos de un empleado en cierto rango de un periodo y en una unidad funcional especifica
    ''' </summary>
    ''' <param name="idEmployee">id del empleado</param>
    ''' <param name="period">periodo del calendario</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetScheduleByEmployeePeriodFunctionalUnitRangeDays(idEmployee As Integer, period As String, functionalUnitId As Integer, diaMin As Integer, diaMax As Integer) As Schedule Implements IScheduleRepository.GetScheduleByEmployeePeriodFunctionalUnitRangeDays
        Dim objSetSchedule = _context.Schedule
        Dim objQuery As DbQuery(Of Schedule)
        objQuery = objSetSchedule.Include("FunctionalUnit")
        For index As Integer = diaMin To diaMax Step 1
            If index = 1 Then
                objQuery = objQuery.Include("ScheduleDetail").Include("ScheduleDetail.ScheduleTemplate")
            Else
                objQuery = objQuery.Include("ScheduleDetail" & (index - 1).ToString()).Include("ScheduleDetail" & (index - 1).ToString() & ".ScheduleTemplate")
            End If
        Next
        Dim query = From e In objQuery
                    Where e.EmployeeId = idEmployee And e.Period = period And e.FunctionalUnitId = functionalUnitId
                    Select e
        If query.Count() > 0 Then
            Return query.SingleOrDefault()
        Else
            Return Nothing
        End If
    End Function

    Public Function AnalisisEmployeeSchedule(InitialDate As Date, EndDate As Date, ByVal IdFunctionalUnit As Integer, ByVal IdPosition As Integer, IdEmployee As Integer) As List(Of SP_AnalisEmployeeSchedule_Result) Implements IScheduleRepository.AnalisisEmployeeSchedule
        Dim result = _context.SP_AnalisEmployeeSchedule(InitialDate, EndDate, IdFunctionalUnit, IdPosition, IdEmployee).ToList()
        Return result
    End Function

    Public Function SaveExtraHour(XmlObject As String) As SP_SaveExtraHours_Result Implements IScheduleRepository.ExecuteSaveExtraHours
        Dim result = _context.SP_SaveExtraHours(XmlObject).FirstOrDefault()

        Return result
    End Function
End Class
