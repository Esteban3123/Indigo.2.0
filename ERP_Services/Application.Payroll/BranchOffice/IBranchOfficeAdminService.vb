Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IBranchOfficeAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista de sucursales
    ''' </summary>
    ''' <returns>Lista de sucursales</returns>
    Function ListAllBranchOffice() As List(Of BranchOffice)

    ''' <summary>
    ''' Elimina una sucursal
    ''' </summary>
    ''' <param name="branchOffice">Sucursal</param>
    ''' <returns></returns>
    Function DeleteBranchOffice(ByVal branchOffice As BranchOffice, ByVal audit As AuditMessage) As ActionMessageResult(Of BranchOffice)

    ''' <summary>
    ''' Guarda una sucursal
    ''' </summary>
    ''' <param name="branchOffice">Sucursal</param>
    ''' <returns></returns>
    Function SaveBranchOffice(ByVal branchOffice As BranchOffice, ByVal audit As AuditMessage) As Boolean

    ''' <summary>
    ''' Obtiene una Sucursal
    ''' </summary>
    ''' <param name="code">Código de la sucursal</param>
    ''' <returns> Sucursal</returns>
    Function GetBranchOffice(ByVal code As String) As BranchOffice

    ''' <summary>
    ''' Obtiene un listado de sucursales filtrado por el id de la empresa
    ''' </summary>
    ''' <param name="CompanyId">id de la empresa</param>
    ''' <returns>listado de sucursales</returns>
    ''' <remarks></remarks>
    Function GetBranchOfficeByCompanyId(CompanyId As Integer) As List(Of BranchOffice)

End Interface
