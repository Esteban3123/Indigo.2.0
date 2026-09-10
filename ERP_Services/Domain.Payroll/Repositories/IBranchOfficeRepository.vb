Imports Domain.Base
Imports Domain.Payroll.Entities

Public Interface IBranchOfficeRepository
    Inherits IRepository(Of BranchOffice)

    ''' <summary>
    ''' Lista todos las sucursales
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllBranchOffice() As List(Of BranchOffice)

    ''' <summary>
    ''' Obtiene una sucursal especifica
    ''' </summary>
    ''' <param name="code">Codigo de la sucursal</param>
    ''' <returns>Sucursal</returns>
    ''' <remarks></remarks>
    Function GetBranchOffice(ByVal code As String, Optional ByVal tracking As Boolean = True) As BranchOffice

    ''' <summary>
    ''' Obtiene un listado de sucursales filtrado por el id de la empresa
    ''' </summary>
    ''' <param name="CompanyId">id de la empresa</param>
    ''' <returns>listado de sucursales</returns>
    ''' <remarks></remarks>
    Function GetBranchOfficeByCompanyId(CompanyId As Integer) As List(Of BranchOffice)

    ''' <summary>
    ''' Obtiene la sucursal por el id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Function GetBranchOfficeById(Id As Integer) As BranchOffice

End Interface
