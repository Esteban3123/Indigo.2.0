'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 12-04-2011
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Payroll
Imports Infrastructure.CrossCutting.IOC
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Partial Class PayrollService

    ''' <summary>
    ''' Elimina un nivel de educacion
    ''' </summary>
    ''' <returns>True o False</returns>
    ''' <remarks></remarks>
    Public Function DeleteEducationLevels(EducationLevel As Domain.Payroll.Entities.EducationLevel, session As SessionValues, audit As AuditMessage) As ActionResult Implements IPayrollService.DeleteEducationLevels
        Using EducationAdminService As IEducationLevelsAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IEducationLevelsAdminService)()
            Return EducationAdminService.DeleteEducationLevels(EducationLevel, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un nivel de educacion especifico
    ''' </summary>
    ''' <param name="code">Codigo del nivel de educacion</param>
    ''' <returns>Nivel de educacion</returns>
    ''' <remarks></remarks>
    Public Function GetEducationLevels(code As String, session As SessionValues) As Domain.Payroll.Entities.EducationLevel Implements IPayrollService.GetEducationLevels
        Using EducationAdminService As IEducationLevelsAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IEducationLevelsAdminService)()
            Return EducationAdminService.GetEducationLevels(code)
        End Using
    End Function

    ''' <summary>
    ''' Lista todos los niveles de educacion
    ''' </summary>
    ''' <returns>Lista de niveles de educacion</returns>
    ''' <remarks></remarks>
    Public Function ListAllEducationLevels(session As SessionValues) As List(Of Domain.Payroll.Entities.EducationLevel) Implements IPayrollService.ListAllEducationLevels
        Using EducationAdminService As IEducationLevelsAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IEducationLevelsAdminService)()
            Return EducationAdminService.ListAllEducationLevels()
        End Using
    End Function

    ''' <summary>
    ''' Graba o Actualiza los niveles de educacion
    ''' </summary>
    ''' <param name="EducationLevel">Nivel de educacion</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>True o False</returns>
    ''' <remarks></remarks>
    Public Function SaveEducationLevels(EducationLevel As Domain.Payroll.Entities.EducationLevel, session As SessionValues, idSequense As Int64, audit As AuditMessage) As ActionResult(Of Domain.Payroll.Entities.EducationLevel) Implements IPayrollService.SaveEducationLevels
        Using EducationAdminService As IEducationLevelsAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IEducationLevelsAdminService)()
            Return EducationAdminService.SaveEducationLevels(EducationLevel, audit, idSequense)
        End Using
    End Function

    Function ChangeStateEducationLevel(ByVal code As String, ByVal state As Boolean, session As SessionValues, audit As AuditMessage) As ActionResult(Of Domain.Payroll.Entities.EducationLevel) Implements IPayrollService.ChangeStateEducationLevel
        Using EducationAdminService As IEducationLevelsAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IEducationLevelsAdminService)()
            Return EducationAdminService.ChangeStateEducationLevel(code, state, audit)
        End Using
    End Function
End Class
