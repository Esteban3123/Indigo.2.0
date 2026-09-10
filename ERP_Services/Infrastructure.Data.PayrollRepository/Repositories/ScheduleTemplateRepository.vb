'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Cristhian Mauricio Salazar
' Created          : 08-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Payroll.Entities
Imports Domain.Payroll
Public Class ScheduleTemplateRepository
    Inherits GenericRepository(Of ScheduleTemplate)
    Implements IScheduleTemplateRepository

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
    ''' Obtiene una plantilla a través del codigo
    ''' </summary>
    ''' <param name="code">Codigo dela plANTILLA</param>
    ''' <returns>Plantilla de contrato</returns>
    ''' <remarks></remarks>
    Public Function GetScheduleTemplate(code As String) As ScheduleTemplate Implements IScheduleTemplateRepository.GetScheduleTemplate
        'Dim schedule As IQueryable(Of ScheduleTemplate)
        'Dim objSchedule As ScheduleTemplate
        'Using ddd As New GENESISEntitiesPayroll()
        '    ddd.Concept.MergeOption = Objects.MergeOption.NoTracking
        '    schedule = From e In ddd.ScheduleTemplate.Include("ScheduleTemplateFunctionalUnit").Include("ScheduleTemplateConcept").Include("Group") _
        '               .Include("ScheduleTemplateConcept.ScheduleTemplateConceptDetail").Include("ScheduleTemplateConcept.ScheduleTemplateConceptDetail.Concept")
        '                  Where e.Code = code
        '                  Select e
        '    If schedule.Count > 0 Then
        '        objSchedule = schedule.SingleOrDefault
        '    Else
        '        objSchedule = Nothing
        '    End If
        'End Using
        'Return objSchedule
        Dim schedule = From e In _context.ScheduleTemplate.Include("ScheduleTemplateFunctionalUnit").Include("ScheduleTemplateConcept").Include("Group") _
                       .Include("ScheduleTemplateConcept.ScheduleTemplateConceptDetail").Include("ScheduleTemplateConcept.ScheduleTemplateConceptDetail.Concept")
                      Where e.Code = code
                      Select e
        If schedule.Count > 0 Then
            Return schedule.SingleOrDefault
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Obtiene una plantilla a través del codigo
    ''' </summary>
    ''' <param name="id">id dela plANTILLA</param>
    ''' <returns>Plantilla de contrato</returns>
    ''' <remarks></remarks>
    Public Function GetScheduleTemplateById(id As String, Optional tracking As Boolean = True) As ScheduleTemplate Implements IScheduleTemplateRepository.GetScheduleTemplateById
        Dim schedule As IQueryable(Of ScheduleTemplate)
        If tracking = True Then
            schedule = From e In _context.ScheduleTemplate
            Where e.Id = id
                      Select e
        Else
            schedule = From e In _context.ScheduleTemplate.AsNoTracking()
            Where e.Id = id
                      Select e
        End If
        If schedule.Count > 0 Then
            Return schedule.SingleOrDefault
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Lista todas las plantillas de turnos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllScheduleTemplate() As List(Of ScheduleTemplate) Implements IScheduleTemplateRepository.ListAllScheduleTemplate
        Dim schedule = From e In _context.ScheduleTemplate.Include("ScheduleTemplateFunctionalUnit").Include("ScheduleTemplateConcept").Include("ScheduleTemplateConcept.ScheduleTemplateConceptDetail").Include("Group")
                      Select e
        Return schedule.ToList()
    End Function


    ''' <summary>
    ''' Lista todas las plantillas de turnos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllScheduleTemplateByStatus(State As Boolean) As List(Of ScheduleTemplate) Implements IScheduleTemplateRepository.ListAllScheduleTemplateByStatus
        Dim schedule = From e In _context.ScheduleTemplate.Include("ScheduleTemplateFunctionalUnit").Include("ScheduleTemplateConcept").Include("ScheduleTemplateConcept.ScheduleTemplateConceptDetail").Include("Group")
                       Where e.State = State
                      Select e
        Return schedule.ToList()
    End Function
End Class
