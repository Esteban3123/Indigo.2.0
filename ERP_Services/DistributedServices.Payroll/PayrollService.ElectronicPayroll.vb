Imports Application.Payroll
Imports Domain.Base.Entities
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.IOC

Partial Class PayrollService
    Implements IPayrollElectronicPayroll

    ''' <summary>
    ''' Genera una nota de ajuste a partir de un documento electrónico de nómina
    ''' </summary>
    ''' <returns></returns>
    Public Function GenerateAdjustmentNote(operatingUnitId As Integer, electronicPayroll As ElectronicPayroll, session As SessionValues) As ActionResult(Of ElectronicPayroll) Implements IPayrollElectronicPayroll.GenerateAdjustmentNote
        Using service As IElectronicPayrollAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IElectronicPayrollAdminService)()
            Return service.GenerateAdjustmentNote(operatingUnitId, electronicPayroll, session)
        End Using
    End Function
End Class
