Imports Domain.Base.Entities
Imports Application.Payroll
Imports Infrastructure.CrossCutting.IOC
Imports Infrastructure.CrossCutting.Base
Imports System.Text
Imports Domain.Payroll.Entities
Imports DistributedServices.Payroll

Partial Class PayrollService
    Implements IPayrollBankFileDetail

    ''' <summary>
    ''' obtiene el detalle de un archivo plano de bancos por Id
    ''' por código de usuario
    ''' </summary>
    ''' <param name="id">Identificador del registro</param>
    ''' <returns></returns>
    Public Function GetBankFileDetailByBankFileId(id As Integer, session As SessionValues) As List(Of BankFileDetail) Implements IPayrollBankFileDetail.GetBankFileDetailByBankFileId
        Using service As IBankFileDetailAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IBankFileDetailAdminService)()
            Return service.GetBankFileDetailByBankFileId(id)
        End Using
    End Function

End Class
