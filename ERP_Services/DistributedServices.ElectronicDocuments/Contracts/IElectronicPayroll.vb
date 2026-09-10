Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Domain.Payroll.Entities

<ServiceContract()>
Public Interface IElectronicPayroll

    <OperationContract()>
    Function ProcessElectronicPayroll(operatingUnitId As Integer, electronicPayrollIds As List(Of Integer)) As Task(Of ActionResult(Of String))

    <OperationContract()>
    Function GetElectronicPaymentSupportXML(consecutive As String) As Task(Of ActionResult(Of NominaIndividual))
End Interface
