'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Juan Diego Díaz
' Created          : 05-09-2018
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Payroll
Imports Infrastructure.CrossCutting.IOC
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Partial Class PayrollService


    ''' <summary>
    ''' Elimina una Actividad de Tiempo Libre
    ''' </summary>
    ''' <param name="freeTimeUse">Actividad de Tiempo Libre</param>
    ''' <param name="session">Objeto Sesión</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeleteSportPractice(freeTimeUse As Domain.Payroll.Entities.FreeTimeUse, session As SessionValues) As ActionResult Implements IPayrollFreeTimeUse.DeleteFreeTimeUse
        Using freeTimeUseAdminService As IFreeTimeUseAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IFreeTimeUseAdminService)()
            Return freeTimeUseAdminService.DeleteFreeTimeUse(freeTimeUse, session.AuditMessageWcf)
        End Using
    End Function


    ''' <summary>
    ''' Obtiene una Actividad de Tiempo Libre
    ''' </summary>
    ''' <param name="code">Código de la Actividad de Tiempo Libre</param>
    ''' <param name="session">Objeto Sesión</param>
    ''' <returns>Actividad de Tiempo Libre</returns>
    ''' <remarks></remarks>
    Public Function GetFreeTimeUse(code As String, tracking As Boolean, session As SessionValues) As ActionResult(Of Domain.Payroll.Entities.FreeTimeUse) Implements IPayrollFreeTimeUse.GetFreeTimeUse
        Using freeTimeUseAdminService As IFreeTimeUseAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IFreeTimeUseAdminService)()
            Return freeTimeUseAdminService.GetFreeTimeUse(code, tracking, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Almacena una Actividad de Tiempo Libre
    ''' </summary>
    ''' <param name="sportPractice">Actividad de Tiempo Libre</param>
    ''' <param name="session">Objeto Sesión</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveFreeTimeUse(freeTimeUse As Domain.Payroll.Entities.FreeTimeUse, session As SessionValues, idSequense As Int64) As ActionResult(Of Domain.Payroll.Entities.FreeTimeUse) Implements IPayrollFreeTimeUse.SaveFreeTimeUse
        Using freeTimeUseAdminService As IFreeTimeUseAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IFreeTimeUseAdminService)()
            Return freeTimeUseAdminService.SaveFreeTimeUse(freeTimeUse, session.AuditMessageWcf, idSequense)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene una Actividad de Tiempo Libre
    ''' </summary>
    ''' <param name="ID">ID de la Actividad de Tiempo Libre</param>
    ''' <returns>Actividad de Tiempo Libre</returns>
    ''' <remarks></remarks>
    Public Function GetFreeTimeUseById(ID As String, tracking As Boolean, session As SessionValues) As ActionResult(Of Domain.Payroll.Entities.FreeTimeUse) Implements IPayrollFreeTimeUse.GetFreeTimeUseById
        Using freeTimeUseAdminService As IFreeTimeUseAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IFreeTimeUseAdminService)()
            Return freeTimeUseAdminService.GetFreeTimeUseById(ID, tracking, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code">Código de la Actividad de Tiempo Libre</param>
    ''' <returns>Actividad de Tiempo Libre</returns>
    ''' <remarks></remarks>
    Public Function ChangeStateFreeTimeUse(code As String, state As Boolean, session As SessionValues) As Domain.Base.Entities.ActionResult(Of Domain.Payroll.Entities.FreeTimeUse) Implements IPayrollFreeTimeUse.ChangeStateFreeTimeUse
        Using freeTimeUseAdminService As IFreeTimeUseAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IFreeTimeUseAdminService)()
            Return freeTimeUseAdminService.ChangeState(code, state, session.AuditMessageWcf)
        End Using
    End Function

End Class
