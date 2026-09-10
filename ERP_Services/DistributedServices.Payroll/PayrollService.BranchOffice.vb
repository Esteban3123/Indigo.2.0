Imports Application.Payroll
Imports Infrastructure.CrossCutting.IOC
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Payroll.Entities

Partial Class PayrollService

    ''' <summary>
    ''' Elimina una sucursal
    ''' </summary>
    ''' <param name="branchOffice">Sucursal</param>
    ''' <returns></returns>
    Public Function DeleteBranchOffice(branchOffice As Domain.Payroll.Entities.BranchOffice, session As SessionValues) As ActionMessageResult(Of BranchOffice) Implements IPayrollBranchOffice.DeleteBranchOffice
        Using branchOfficeAdminService As IBranchOfficeAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IBranchOfficeAdminService)()
            Return branchOfficeAdminService.DeleteBranchOffice(branchOffice, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene una Sucursal
    ''' </summary>
    ''' <param name="code">Código de la sucursal</param>
    ''' <returns> Sucursal</returns>
    Public Function GetBranchOffice(code As String, session As SessionValues) As Domain.Payroll.Entities.BranchOffice Implements IPayrollBranchOffice.GetBranchOffice
        Using branchOfficeAdminService As IBranchOfficeAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IBranchOfficeAdminService)()
            Return branchOfficeAdminService.GetBranchOffice(code)
        End Using
    End Function

    ''' <summary>
    ''' Lista de sucursales
    ''' </summary>
    ''' <returns>Lista de sucursales</returns>
    Public Function ListAllBranchOffice(session As SessionValues) As List(Of Domain.Payroll.Entities.BranchOffice) Implements IPayrollBranchOffice.ListAllBranchOffice
        Using branchOfficeAdminService As IBranchOfficeAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IBranchOfficeAdminService)()
            Return branchOfficeAdminService.ListAllBranchOffice()
        End Using
    End Function

    ''' <summary>
    ''' Guarda una sucursal
    ''' </summary>
    ''' <param name="branchOffice">Sucursal</param>
    ''' <returns></returns>
    Public Function SaveBranchOffice(branchOffice As Domain.Payroll.Entities.BranchOffice, session As SessionValues) As Boolean Implements IPayrollBranchOffice.SaveBranchOffice
        Using branchOfficeAdminService As IBranchOfficeAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IBranchOfficeAdminService)()
            Return branchOfficeAdminService.SaveBranchOffice(branchOffice, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un listado de sucursales filtrado por el id de la empresa
    ''' </summary>
    ''' <param name="CompanyId">id de la empresa</param>
    ''' <returns>listado de sucursales</returns>
    ''' <remarks></remarks>
    Public Function GetBranchOfficeByCompanyId(CompanyId As Integer, session As Infrastructure.CrossCutting.Base.SessionValues) As List(Of Domain.Payroll.Entities.BranchOffice) Implements IPayrollBranchOffice.GetBranchOfficeByCompanyId
        Using branchOfficeAdminService As IBranchOfficeAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IBranchOfficeAdminService)()
            Return branchOfficeAdminService.GetBranchOfficeByCompanyId(CompanyId)
        End Using
    End Function
End Class
