Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()>
Public Interface IPayrollElectronicPayroll

    ''' <summary>
    ''' Genera una nota de ajuste a partir de un documento electrónico de nómina
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GenerateAdjustmentNote(operatingUnitId As Integer, electronicPayroll As ElectronicPayroll, session As SessionValues) As ActionResult(Of ElectronicPayroll)

End Interface
