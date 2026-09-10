Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()> _
Public Interface IPayrollBranchOffice

    ''' <summary>
    ''' Lista de sucursales
    ''' </summary>
    ''' <returns>Lista de sucursales</returns>
    <OperationContract()> _
    Function ListAllBranchOffice(session As SessionValues) As List(Of BranchOffice)

    ''' <summary>
    ''' Elimina una sucursal
    ''' </summary>
    ''' <param name="branchOffice">Sucursal</param>
    ''' <returns></returns>
    <OperationContract()> _
    Function DeleteBranchOffice(ByVal branchOffice As BranchOffice, session As SessionValues) As ActionMessageResult(Of BranchOffice)

    ''' <summary>
    ''' Guarda una sucursal
    ''' </summary>
    ''' <param name="branchOffice">Sucursal</param>
    ''' <returns></returns>
    <OperationContract()> _
    Function SaveBranchOffice(ByVal branchOffice As BranchOffice, session As SessionValues) As Boolean

    ''' <summary>
    ''' Obtiene una Sucursal
    ''' </summary>
    ''' <param name="code">Código de la sucursal</param>
    ''' <returns> Sucursal</returns>
    <OperationContract()> _
    Function GetBranchOffice(ByVal code As String, session As SessionValues) As BranchOffice

    ''' <summary>
    ''' Obtiene un listado de sucursales filtrado por el id de la empresa
    ''' </summary>
    ''' <param name="CompanyId">id de la empresa</param>
    ''' <returns>listado de sucursales</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetBranchOfficeByCompanyId(CompanyId As Integer, session As SessionValues) As List(Of BranchOffice)

End Interface
