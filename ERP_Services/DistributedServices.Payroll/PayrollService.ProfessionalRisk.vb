'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 02-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.IOC
Imports Application.Payroll
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Partial Class PayrollService

    ''' <summary>
    ''' Elimina un Riesgo Profesional
    ''' </summary>
    ''' <param name="professionalRisk">Riesgo Profesional</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeleteProfessionalRisk(professionalRisk As Domain.Payroll.Entities.ProfessionalRisk, session As SessionValues) As ActionMessageResult(Of ProfessionalRisk) Implements IPayrollProfessionalRisk.DeleteProfessionalRisk
        Using professionalRiskAdmin As IProfessionalRiskAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IProfessionalRiskAdminService)()
            Return professionalRiskAdmin.DeleteProfessionalRisk(professionalRisk, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un Riesgo Profesional Determinado
    ''' </summary>
    ''' <param name="code">Código del Riesgo Profesional</param>
    ''' <returns>Riesgo Profesional</returns>
    ''' <remarks></remarks>
    Public Function GetProfessionalRisk(code As String, session As SessionValues) As Domain.Payroll.Entities.ProfessionalRisk Implements IPayrollProfessionalRisk.GetProfessionalRisk
        Using professionalRiskAdmin As IProfessionalRiskAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IProfessionalRiskAdminService)()
            Return professionalRiskAdmin.GetProfessionalRisk(code)
        End Using
    End Function

    ''' <summary>
    ''' Lista Riesgos Profesionales
    ''' </summary>
    ''' <returns>Riesgos Profesionales</returns>
    ''' <remarks></remarks>
    Public Function ListAllProfessionalRisk(session As SessionValues) As List(Of Domain.Payroll.Entities.ProfessionalRisk) Implements IPayrollProfessionalRisk.ListAllProfessionalRisk
        Using professionalRiskAdmin As IProfessionalRiskAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IProfessionalRiskAdminService)()
            Return professionalRiskAdmin.ListAllProfessionalRisk()
        End Using
    End Function

    ''' <summary>
    ''' Almacena o Actualiza Riesgos Profesionales
    ''' </summary>
    ''' <param name="professionalRisk">Riesgos Profesionales</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveProfessionalRisk(professionalRisk As Domain.Payroll.Entities.ProfessionalRisk, session As SessionValues) As Boolean Implements IPayrollProfessionalRisk.SaveProfessionalRisk
        Using professionalRiskAdmin As IProfessionalRiskAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IProfessionalRiskAdminService)()
            Return professionalRiskAdmin.SaveProfessionalRisk(professionalRisk, session.AuditMessageWcf)
        End Using
    End Function

End Class
