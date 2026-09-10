'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 18-09-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Payroll.Entities
Imports Domain.Payroll
Public Class NoveltyScheduleDetailRepository
    Inherits GenericRepository(Of NoveltyScheduleDetail)
    Implements INoveltyScheduleDetailRepository

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
    ''' obtiene un listado de los detalles de las novedades que deben tener deducciones
    ''' </summary>
    ''' <param name="dateInitial">Fecha Inicial</param>
    ''' <param name="dateEnd">Fecha Final</param>
    ''' <returns>Lista de detalles de las novedades</returns>
    ''' <remarks></remarks>
    Public Function GetNoveltyScheduleDetailBetweenDate(dateInitial As Date, dateEnd As Date) As List(Of NoveltyScheduleDetail) Implements INoveltyScheduleDetailRepository.GetNoveltyScheduleDetailBetweenDate
        Dim noveltySchedule = From e In _context.NoveltyScheduleDetail.Include("NoveltyScheduleDetailHour") _
                       .Include("NoveltyScheduleDetailHour.NoveltyScheduleDetailConcept")
                        Where e.DateDetail >= dateInitial And e.DateDetail <= dateEnd
                       Select e
        If noveltySchedule.Count > 0 Then
            Return noveltySchedule.ToList()
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Obtiene un listado de los detalles de una novedad
    ''' </summary>
    ''' <param name="employeeId">id empleado</param>
    ''' <param name="noveltyId">Id novedad</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetNoveltyScheduleDetailByEmployeeNoveltyId(employeeId As Integer, noveltyId As Integer) As List(Of NoveltyScheduleDetail) Implements INoveltyScheduleDetailRepository.GetNoveltyScheduleDetailByEmployeeNoveltyId
        Dim queryNovelty = From e In _context.Novelty.Where(Function(x) x.Id = noveltyId)
        If queryNovelty.Count = 0 Then
            Throw New ArgumentException("El Id " & noveltyId & " no existe como novedad")
        End If
        Dim novelty As Novelty = queryNovelty.SingleOrDefault()
        Dim noveltySchedule = From e In _context.NoveltyScheduleDetail.Include("NoveltyScheduleDetailHour") _
                       .Include("NoveltyScheduleDetailHour.NoveltyScheduleDetailConcept")
                              Where e.EmployeeId = employeeId And e.DateDetail >= novelty.RealDate And e.DateDetail <= novelty.EndDate
                              Select e
        If noveltySchedule.Count > 0 Then
            Return noveltySchedule.ToList()
        Else
            Return New List(Of NoveltyScheduleDetail)
        End If
    End Function

    Public Function GetNoveltyScheduleDetailByEmployeeUnitBetweenDateWithoutNovelties(employeeId As Integer, PayrollDate As Date, Status As Integer, IdGroup As Integer) As List(Of NoveltyScheduleDetail) Implements INoveltyScheduleDetailRepository.GetNoveltyScheduleDetailByEmployeeUnitBetweenDateWithoutNovelties
        Dim schedule = From e In _context.NoveltyScheduleDetail.Include("NoveltyScheduleDetailHour") _
                       .Include("NoveltyScheduleDetailHour.NoveltyScheduleDetailConcept").Include("NoveltyScheduleDetailHour.NoveltyScheduleDetailConcept.Concept").Include("FunctionalUnit").Include("FunctionalUnit.AccountingStructure")
                       Where e.PayrollDate = PayrollDate And e.EmployeeId = employeeId And e.TotalNumberHours > 0 And e.Status = 0 And e.GroupId = IdGroup

        If schedule.Count > 0 Then
            Return schedule.ToList()
        Else
            Return Nothing
        End If
    End Function
End Class
