Imports Domain.Base.Entities
Imports Domain.Payroll.Entities

Public Interface IElectronicPayrollAdminService
    Inherits IDisposable

    Function ProcessElectronicPayroll(operatingUnitId As Integer, electronicPayrollIds As List(Of Integer)) As Task(Of ActionResult(Of String))

    Function GetElectronicPaymentSupportXML(consecutive As String) As Task(Of ActionResult(Of NominaIndividual))

End Interface