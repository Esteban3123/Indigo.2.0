'***********************************************************************
' Author           : Andrés Steven Rojas
' Created          : 26-09-2023
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Application.Payroll
Imports Infrastructure.CrossCutting.IOC
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Payroll.Entities
Public Class PayrollService

    Public Function ListAllLicensingConcepts(session As SessionValues) As List(Of LicensingConcepts) Implements IPayrollLicensingConcepts.listAllLicensingConcepts
        Using LicensingConceptsAdminService As ILicensingConceptsAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ILicensingConceptsAdminService)()
            Return LicensingConceptsAdminService.ListAllLicensingConcepts
        End Using
    End Function

    Public Function DeleteLicensingConcepts(licensingConcepts As LicensingConcepts, sesion As SessionValues) As ActionMessageResult(Of LicensingConcepts) Implements IPayrollLicensingConcepts.DeleteLicensingConcepts
        Using LicensingConceptsAdminService As ILicensingConceptsAdminService = IocFactory.Instance(sesion.TransactionalContainer).CurrentContainer.Resolve(Of ILicensingConceptsAdminService)()
            Return LicensingConceptsAdminService.DeleteLicensingConcepts(licensingConcepts, sesion.AuditMessageWcf)
        End Using
    End Function

    Public Function SaveLicensingConcepts(licensingConcepts As LicensingConcepts, session As SessionValues, idSequence As Long) As ActionResult(Of LicensingConcepts) Implements IPayrollLicensingConcepts.SaveLicensingConcepts
        Using LicensingConceptsAdminService As ILicensingConceptsAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ILicensingConceptsAdminService)()
            Return LicensingConceptsAdminService.SavelicensingConcepts(licensingConcepts, session.AuditMessageWcf, idSequence)
        End Using
    End Function

    Public Function UpdateLicensingConcepts(code As String, status As Boolean, session As SessionValues) As ActionResult(Of LicensingConcepts) Implements IPayrollLicensingConcepts.UpdateLicensingConcepts
        Using LicensingConceptsAdminService As ILicensingConceptsAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ILicensingConceptsAdminService)()
            Return LicensingConceptsAdminService.UpdatelicensingConcepts(code, status, session.AuditMessageWcf)
        End Using
    End Function

    Public Function GetLicensingConcepts(code As String, session As SessionValues) As LicensingConcepts Implements IPayrollLicensingConcepts.GetLicensingConcepts
        Using LicensingConceptsAdminService As ILicensingConceptsAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ILicensingConceptsAdminService)()
            Return LicensingConceptsAdminService.GetLicensingConcepts(code, session.AuditMessageWcf)
        End Using
    End Function

    Public Function GetLicensingConceptsById(Id As Integer, session As SessionValues) As LicensingConcepts Implements IPayrollLicensingConcepts.GetLicensingConceptsById
        Using LicensingConceptsAdminService As ILicensingConceptsAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ILicensingConceptsAdminService)()
            Return LicensingConceptsAdminService.GetLicensingConceptsById(Id)
        End Using
    End Function
End Class
