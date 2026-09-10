#Region "Imports"

Imports Domain.Base.Entities
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base

#End Region

Public Interface IElectronicPayrollAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Genera una nota de ajuste a partir de un documento electrónico de nómina
    ''' </summary>
    ''' <returns></returns>
    Function GenerateAdjustmentNote(operatingUnitId As Integer, electronicPayroll As ElectronicPayroll, session As SessionValues) As ActionResult(Of ElectronicPayroll)

End Interface