'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 16-04-2011
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
    ''' Elimina una profesion
    ''' </summary>
    ''' <param name="profession">Profesion</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeleteProfession(profession As Domain.Payroll.Entities.Profession, session As SessionValues, audit As AuditMessage) As ActionResult Implements IPayrollService.DeleteProfession
        Using ProfessionsAdmin As IProfessionsAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IProfessionsAdminService)()
            Return ProfessionsAdmin.DeleteProfession(profession, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene una profesion especifica
    ''' </summary>
    ''' <param name="code">Codigo Profesion</param>
    ''' <returns>Profesion</returns>
    ''' <remarks></remarks>
    Public Function GetProfessions(code As String, session As SessionValues) As Domain.Payroll.Entities.Profession Implements IPayrollService.GetProfessions
        Using ProfessionsAdmin As IProfessionsAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IProfessionsAdminService)()
            Return ProfessionsAdmin.GetProfessions(code)
        End Using
    End Function

    ''' <summary>
    ''' Lista todas las profesiones
    ''' </summary>
    ''' <returns>Lista de profesiones</returns>
    ''' <remarks></remarks>
    Public Function ListAllProfessions(session As SessionValues) As List(Of Domain.Payroll.Entities.Profession) Implements IPayrollService.ListAllProfessions
        Using ProfessionsAdmin As IProfessionsAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IProfessionsAdminService)()
            Return ProfessionsAdmin.ListAllProfessions()
        End Using
    End Function

    ''' <summary>
    ''' Graba o Actualiza las Profesiones
    ''' </summary>
    ''' <param name="profession">Profesion</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveProfessions(profession As Domain.Payroll.Entities.Profession, session As SessionValues, idSequense As Int64, audit As AuditMessage) As ActionResult(Of Domain.Payroll.Entities.Profession) Implements IPayrollService.SaveProfessions
        Using ProfessionsAdmin As IProfessionsAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IProfessionsAdminService)()
            Return ProfessionsAdmin.SaveProfessions(profession, audit, idSequense)
        End Using
    End Function

    Function ChangeStateProfession(ByVal code As String, ByVal state As Boolean, session As SessionValues, audit As AuditMessage) As ActionResult(Of Profession) Implements IPayrollService.ChangeStateProfession
        Using ProfessionsAdmin As IProfessionsAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IProfessionsAdminService)()
            Return ProfessionsAdmin.ChangeStateProfession(code, state, audit)
        End Using
    End Function
End Class
