'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 17-04-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Payroll
Imports Infrastructure.CrossCutting.IOC
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Partial Class PayrollService

    ''' <summary>
    ''' Elimina una Razón de Retiro
    ''' </summary>
    ''' <param name="RetirementReason">Razón de Retiro</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    Public Function DeleteRetirementReason(RetirementReason As Domain.Payroll.Entities.RetirementReason, session As SessionValues, audit As AuditMessage) As ActionResult Implements IPayrollService.DeleteRetirementReason
        Using RetirementReasonAdmin As IRetirementReasonAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IRetirementReasonAdminService)()
            Return RetirementReasonAdmin.DeleteRetirementReason(RetirementReason, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene una Razón de Retiro Específica
    ''' </summary>
    ''' <param name="code">Código Razón de Retiro</param>
    ''' <returns>Razón de Retiro</returns>
    Public Function GetRetirementReason(code As String, session As SessionValues) As Domain.Payroll.Entities.RetirementReason Implements IPayrollService.GetRetirementReason
        Using RetirementReasonAdmin As IRetirementReasonAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IRetirementReasonAdminService)()
            Return RetirementReasonAdmin.GetRetirementReason(code)
        End Using
    End Function

    ''' <summary>
    ''' Lista Todas las Razones de Retiro
    ''' </summary>
    ''' <returns>Lista de Razones de Retiro</returns>
    ''' <remarks></remarks>
    Public Function ListAllRetirementReason(session As SessionValues) As List(Of Domain.Payroll.Entities.RetirementReason) Implements IPayrollService.ListAllRetirementReason
        Using RetirementReasonAdmin As IRetirementReasonAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IRetirementReasonAdminService)()
            Return RetirementReasonAdmin.ListAllRetirementReason()
        End Using
    End Function

    ''' <summary>
    ''' Graba o Actualiza una Razón de Retiro
    ''' </summary>
    ''' <param name="RetirementReason">Razón de Retiro</param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>Boolean</returns>
    Public Function SaveRetirementReason(RetirementReason As Domain.Payroll.Entities.RetirementReason, session As SessionValues, idSequense As Int64, audit As AuditMessage) As ActionResult(Of Domain.Payroll.Entities.RetirementReason) Implements IPayrollService.SaveRetirementReason
        Using RetirementReasonAdmin As IRetirementReasonAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IRetirementReasonAdminService)()
            Return RetirementReasonAdmin.SaveRetirementReason(RetirementReason, audit, idSequense)
        End Using
    End Function

    Function ChangeStateRetirementReason(ByVal code As String, ByVal state As Boolean, session As SessionValues, audit As AuditMessage) As ActionResult(Of Domain.Payroll.Entities.RetirementReason) Implements IPayrollService.ChangeStateRetirementReason
        Using RetirementReasonAdmin As IRetirementReasonAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IRetirementReasonAdminService)()
            Return RetirementReasonAdmin.ChangeStateRetirementReason(code, state, audit)
        End Using
    End Function
End Class
