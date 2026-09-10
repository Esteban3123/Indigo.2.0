Imports Application.ElectronicDocuments
Imports Domain.Base.Entities
Imports Domain.Payroll.Entities
Imports Microsoft.Practices.Unity

Partial Class ElectronicDocumentsService
    Implements IElectronicPayroll

    Public Async Function ProcessElectronicPayroll(operatingUnitId As Integer, electronicPayrollIds As List(Of Integer)) As Task(Of ActionResult(Of String)) Implements IElectronicPayroll.ProcessElectronicPayroll
        Using service As IElectronicPayrollAdminService = Container.Current.Resolve(Of IElectronicPayrollAdminService)()
            Return Await service.ProcessElectronicPayroll(operatingUnitId, electronicPayrollIds)
        End Using
    End Function

    Public Async Function GetElectronicPaymentSupportXML(consecutive As String) As Task(Of ActionResult(Of NominaIndividual)) Implements IElectronicPayroll.GetElectronicPaymentSupportXML
        Using service As IElectronicPayrollAdminService = Container.Current.Resolve(Of IElectronicPayrollAdminService)()
            Return Await service.GetElectronicPaymentSupportXML(consecutive)
        End Using
    End Function

End Class
