'***********************************************************************
' Author           : Cesar Collazos
' Created          : 09-02-2024
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************


#Region "Imports"
Imports Application.Payroll
Imports Infrastructure.CrossCutting.IOC
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Domain.Payroll.Entities
#End Region
Partial Class PayrollService

    ''' <summary>
    ''' Obtiene el registro de parametrización de talento humano
    ''' </summary>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Function GetHumanTalentParameterization(session As SessionValues) As HumanTalentParameterization Implements IPayrollHumanTalentParameterization.GetHumanTalentParameterization
        Using HumanTalentParameterizationAdminService As IHumanTalentParameterizationAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IHumanTalentParameterizationAdminService)()
            Return HumanTalentParameterizationAdminService.GetHumanTalentParameterization()
        End Using
    End Function

    ''' <summary>
    ''' Guarda/Actuliza la entidad HumanTalentParameterization 
    ''' </summary>
    ''' <param name="HumanTalentParameterization"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Function SaveHumanTalentParameterization(HumanTalentParameterization As HumanTalentParameterization, session As SessionValues) Implements IPayrollHumanTalentParameterization.SaveHumanTalentParameterization
        Using HumanTalentParameterizationAdminService As IHumanTalentParameterizationAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IHumanTalentParameterizationAdminService)()
            Return HumanTalentParameterizationAdminService.SaveHumanTalentParameterization(HumanTalentParameterization, session.AuditMessageWcf)
        End Using
    End Function

End Class
