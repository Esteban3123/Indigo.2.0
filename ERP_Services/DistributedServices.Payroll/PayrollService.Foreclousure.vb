'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Rafael Eduardo Patiño
' Created          : 13-01-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Payroll
Imports Infrastructure.CrossCutting.IOC
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Payroll.Entities

Partial Class PayrollService
    ''' <summary>
    ''' Funcion para eliminar una clase de convenios
    ''' </summary>
    ''' <param name="AgreementsC">Obj. convenio a eliminar</param>
    ''' <param name="audit">Objeto inf. auditoria</param>
    ''' <returns></returns>
    ''' <remarks></remarks> 
    Public Function DeleteForeclousure(ByVal Foreclousure As Foreclousure, ByVal session As SessionValues) As ActionResult Implements IPayrollForeclousure.DeleteForeclousure
        Using ForeclousureAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IForeclousureAdminService)()
            Return ForeclousureAdminService.DeleteForeclousure(Foreclousure, session.AuditMessageWcf)
        End Using
    End Function
    ''' <summary>
    ''' Obtiene un convenio por el consecutivo
    ''' </summary>
    ''' <param name="consecutive">consecutivo</param>
    ''' <param name="audit">Objeto Inf. Auditoria</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetForeclousure(ByVal consecutive As String, ByVal session As SessionValues) As Foreclousure Implements IPayrollForeclousure.GetForeclousure
        Using ForeclousureAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IForeclousureAdminService)()
            Return ForeclousureAdminService.GetForeclousure(consecutive, session.AuditMessageWcf)
        End Using
    End Function
    ''' <summary>
    ''' Lista de convenios
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListForeclousure(ByVal session As SessionValues) As List(Of Foreclousure) Implements IPayrollForeclousure.ListForeclosure
        Using ForeclousureAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IForeclousureAdminService)()
            Return ForeclousureAdminService.ListForeclousure(session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Funcion para guardar un convenio
    ''' </summary>
    ''' <param name="AgreementsC">Objeto convenio</param>
    ''' <param name="audit">Objeto inf. auditoria</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveForeclousure(ByVal Foreclousure As Foreclousure, idSequense As Int64, ByVal session As SessionValues) As ActionResult(Of Foreclousure) Implements IPayrollForeclousure.SaveForeclousure
        Using ForeclousureAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IForeclousureAdminService)()
            Return ForeclousureAdminService.SaveForeclousure(Foreclousure, session.AuditMessageWcf, idSequense)
        End Using
    End Function
End Class
