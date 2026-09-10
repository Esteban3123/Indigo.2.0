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
    ''' Elimina una Enfermedad Diagnosticada
    ''' </summary>
    ''' <param name="diagnosedDisease">Enfermedad Diagnosticada</param>
    ''' <param name="session">Objeto Sesión</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeleteDiagnosedDisease(diagnosedDisease As Domain.Payroll.Entities.DiagnosedDisease, session As SessionValues) As ActionResult Implements IPayrollDiagnosedDisease.DeleteDiagnosedDisease
        Using diagnosedDiseaseAdminService As IDiagnosedDiseaseAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IDiagnosedDiseaseAdminService)()
            Return diagnosedDiseaseAdminService.DeleteDiagnosedDisease(diagnosedDisease, session.AuditMessageWcf)
        End Using
    End Function


    ''' <summary>
    ''' Obtiene una Enfermedad Diagnosticada
    ''' </summary>
    ''' <param name="code">Código de la Enfermedad Diagnosticada</param>
    ''' <param name="session">Objeto Sesión</param>
    ''' <returns>Enfermedad Diagnosticada</returns>
    ''' <remarks></remarks>
    Public Function GetDiagnosedDisease(code As String, tracking As Boolean, session As SessionValues) As ActionResult(Of Domain.Payroll.Entities.DiagnosedDisease) Implements IPayrollDiagnosedDisease.GetDiagnosedDisease
        Using diagnosedDiseaseAdminService As IDiagnosedDiseaseAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IDiagnosedDiseaseAdminService)()
            Return diagnosedDiseaseAdminService.GetDiagnosedDisease(code, tracking, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Almacena una Enfermedad Diagnosticada
    ''' </summary>
    ''' <param name="diagnosedDisease">Enfermedad Diagnosticada</param>
    ''' <param name="session">Objeto Sesión</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveDiagnosedDisease(diagnosedDisease As Domain.Payroll.Entities.DiagnosedDisease, session As SessionValues, idSequense As Int64) As ActionResult(Of Domain.Payroll.Entities.DiagnosedDisease) Implements IPayrollDiagnosedDisease.SaveDiagnosedDisease
        Using diagnosedDiseaseAdminService As IDiagnosedDiseaseAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IDiagnosedDiseaseAdminService)()
            Return diagnosedDiseaseAdminService.SaveDiagnosedDisease(diagnosedDisease, session.AuditMessageWcf, idSequense)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene una Enfermedad Diagnosticada
    ''' </summary>
    ''' <param name="ID">ID de la Actividad de Tiempo Libre</param>
    ''' <returns>Enfermedad Diagnosticada</returns>
    ''' <remarks></remarks>
    Public Function GetDiagnosedDiseaseById(ID As String, tracking As Boolean, session As SessionValues) As ActionResult(Of Domain.Payroll.Entities.DiagnosedDisease) Implements IPayrollDiagnosedDisease.GetDiagnosedDiseaseById
        Using diagnosedDiseaseAdminService As IDiagnosedDiseaseAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IDiagnosedDiseaseAdminService)()
            Return diagnosedDiseaseAdminService.GetDiagnosedDiseaseById(ID, tracking, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code">Código de la Enfermedad Diagnosticada</param>
    ''' <returns>Enfermedad Diagnosticada</returns>
    ''' <remarks></remarks>
    Public Function ChangeStateDiagnosedDisease(code As String, state As Boolean, session As SessionValues) As Domain.Base.Entities.ActionResult(Of Domain.Payroll.Entities.DiagnosedDisease) Implements IPayrollDiagnosedDisease.ChangeStateDiagnosedDisease
        Using diagnosedDiseaseAdminService As IDiagnosedDiseaseAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IDiagnosedDiseaseAdminService)()
            Return diagnosedDiseaseAdminService.ChangeState(code, state, session.AuditMessageWcf)
        End Using
    End Function

End Class
