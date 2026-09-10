'***********************************************************************
' Assembly         : DistributedServices.Common
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 18-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.CrossCutting.IOC
Imports Application.Common
Imports Infrastructure.CrossCutting.Base

Partial Class CommonERPService

    Implements ICommonERPHoliday

    ''' <summary>
    ''' Elimina un Domingo y/o Festivo
    ''' </summary>
    ''' <param name="holiday">Holiday</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeleteHoliday(holiday As Domain.Entities.Holiday, session As SessionValues) As Boolean Implements ICommonERPHoliday.DeleteHoliday
        Using holidayAdminService As IHolidayAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IHolidayAdminService)()
            Return holidayAdminService.DeleteHoliday(holiday, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un Domingo y/o Festivo
    ''' </summary>
    ''' <param name="holiDate">Fecha</param>
    ''' <returns>Domingo y/o Festivo</returns>
    ''' <remarks></remarks>
    Public Function GetHoliday(holiDate As Date, session As SessionValues) As Domain.Entities.Holiday Implements ICommonERPHoliday.GetHoliday
        Using holidayAdminService As IHolidayAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IHolidayAdminService)()
            Return holidayAdminService.GetHoliday(holiDate)
        End Using
    End Function

    ''' <summary>
    ''' Lista Todos los Domingos y/o Festivos
    ''' </summary>
    ''' <returns>Lista Domingos y/o Festivos</returns>
    ''' <remarks></remarks>
    Public Function ListAllHolidays(session As SessionValues) As List(Of Domain.Entities.Holiday) Implements ICommonERPHoliday.ListAllHolidays
        Using holidayAdminService As IHolidayAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IHolidayAdminService)()
            Return holidayAdminService.ListAllHolidays()
        End Using
    End Function

    ''' <summary>
    ''' Almacena o Actualiza un Domingo y/o Festivo
    ''' </summary>
    ''' <param name="holiday">Holiday</param>
    ''' <param name="session">Objeto session</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveHoliday(holiday As Domain.Entities.Holiday, session As SessionValues) As Boolean Implements ICommonERPHoliday.SaveHoliday
        Using holidayAdminService As IHolidayAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IHolidayAdminService)()
            Return holidayAdminService.SaveHoliday(holiday, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Lista Todos los Domingos y/o Festivos de un Año (Un año Atrás y otro Adelante de acuerdo al enviado)
    ''' </summary>
    ''' <param name="year">Año</param>
    ''' <returns>Lista de Domingos y/o festivos</returns>
    ''' <remarks></remarks>
    Public Function ListHolidaybyYear(year As Integer, session As SessionValues) As List(Of Domain.Entities.Holiday) Implements ICommonERPHoliday.ListHolidaybyYear
        Using holidayAdminService As IHolidayAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IHolidayAdminService)()
            Return holidayAdminService.ListHolidaybyYear(year)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene los festivos que estan dentro de un rango de fecha
    ''' </summary>
    ''' <param name="initialDate">Fecha Inicial</param>
    ''' <param name="endDate">Fecha Final</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListHolidayBetweenDate(initialDate As Date, endDate As Date, session As SessionValues) As List(Of Domain.Entities.Holiday) Implements ICommonERPHoliday.ListHolidayBetweenDate
        Using holidayAdminService As IHolidayAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IHolidayAdminService)()
            Return holidayAdminService.ListHolidayBetweenDate(initialDate, endDate)
        End Using
    End Function
End Class
