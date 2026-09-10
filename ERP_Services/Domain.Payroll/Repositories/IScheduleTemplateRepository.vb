'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 18-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Payroll.Entities
Public Interface IScheduleTemplateRepository
    Inherits IRepository(Of ScheduleTemplate)

    ''' <summary>
    ''' Lista todas las plantillas de turnos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllScheduleTemplate() As List(Of ScheduleTemplate)

    ''' <summary>
    ''' Obtiene una plantilla a través del codigo
    ''' </summary>
    ''' <param name="code">Codigo dela plANTILLA</param>
    ''' <returns>Plantilla de contrato</returns>
    ''' <remarks></remarks>
    Function GetScheduleTemplate(ByVal code As String) As ScheduleTemplate

    ''' <summary>
    ''' Obtiene una plantilla a través del codigo
    ''' </summary>
    ''' <param name="id">id dela plANTILLA</param>
    ''' <returns>Plantilla de contrato</returns>
    ''' <remarks></remarks>
    Function GetScheduleTemplateById(ByVal id As String, Optional tracking As Boolean = True) As ScheduleTemplate

    ''' <summary>
    ''' Obtiene el Listado de Plantillas por Estado
    ''' </summary>
    ''' <param name="State"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllScheduleTemplateByStatus(State As Boolean) As List(Of ScheduleTemplate)

End Interface
