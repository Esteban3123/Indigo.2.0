#Region "Imports"
Imports System.ServiceModel
Imports Domain.Maintenance.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region

<ServiceContract()> _
Public Interface ISupplierMaintenanceService

    ''' <summary>
    ''' Elimina un Proveedor 
    ''' </summary>
    ''' <param name="Supplier">Supplier</param>
    ''' <param name="audit">audit</param>
    ''' <returns>ActionResult</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function DeleteSupplierMaintenance(Supplier As SupplierMaintenance, ByVal session As SessionValues) As ActionResult

    ''' <summary>
    ''' Obtiene el Proveedor por Nit
    ''' </summary>
    ''' <param name="Nit">Nit</param>
    ''' <returns>SupplierMaintenance</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetSupplierMaintenance(Nit As String, ByVal session As SessionValues) As SupplierMaintenance

    ''' <summary>
    ''' Obtiene un Proveedor por ID
    ''' </summary>
    ''' <param name="id">ID</param>
    ''' <returns>SupplierMaintenance</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetSupplierMaintenanceById(id As Integer, session As SessionValues) As SupplierMaintenance

    ''' <summary>
    ''' Lista todos los Proveedores (Mantenimiento)
    ''' </summary>
    ''' <returns>List(Of SupplierMaintenance)</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function ListAllSupplierMaintenance(session As SessionValues) As List(Of SupplierMaintenance)

    ''' <summary>
    ''' Almacena un Proveedor (Mantenimiento)
    ''' </summary>
    ''' <param name="Supplier">Supplier</param>
    ''' <param name="audit">audit</param>
    ''' <returns>ActionResult(Of SupplierMaintenance)</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function SaveSupplierMaintenance(Supplier As SupplierMaintenance, session As SessionValues) As ActionResult(Of SupplierMaintenance)

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function ChangeStateSupplierMaintenance(code As String, state As Boolean, session As SessionValues) As ActionResult(Of SupplierMaintenance)

End Interface
