
'************************************************************
' Assembly         : Domain.Maintenance
' Author           : Oscar Ivan Sierra
' Created          : 08-08-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************



#Region "Importar"
Imports Domain.Base.Entities
Imports Domain.Base
Imports Domain.Maintenance.Entities
#End Region


''' <summary>
''' clase para definir cada una de la spropiedades y metodos que se van a persistir en la clase del repositorio
''' </summary>
Public Interface ISupplierRepository
    Inherits IRepository(Of Supplier)

    Function GetSupplierByIdThirdPartyWithThirdAdded(Id As Integer) As Domain.Entities.Supplier

    ''' <summary>
    ''' funcion que lista todas los fabricantes
    ''' </summary>
    ''' <returns>Lista de fabricantes</returns>
    Function ListAllSupplier() As List(Of Supplier)
    ''' <summary>
    ''' consulta para retornar un fabricante teniendo en cuenta el codigo
    ''' </summary>
    ''' <param name="Nit">el codigo del fabricante</param>
    ''' <returns>Objeto fabricanre</returns>
    Function GetSupplier(ByVal Nit As String, Optional tracking As Boolean = True) As Supplier
    ''' <summary>
    ''' consulta para retornar un fabricante teniendo en cuenta el codigo
    ''' </summary>
    ''' <returns>Objeto fabricanre</returns>
    Function GetThirdPartyById(ByVal Id As Integer) As ThirdParty

    ''' <summary>
    ''' Obtiene un proveedor por id del tercero
    ''' </summary>
    ''' <returns></returns>
    Function GetSupplierByIdThirdParty(ByVal Id As Integer) As Domain.Entities.Supplier

    ''' <summary>
    ''' Obtiene un proveedor por id del tercero e id de la cuenta contable
    ''' </summary>
    ''' <param name="IdThird">The identifier third.</param>
    ''' <param name="IdAccountAccounting">The identifier account accounting.</param>
    ''' <returns></returns>
    Function GetSupplierByIdThirdPartyAndIdAccountAccounting(ByVal IdThird As Integer, ByVal IdAccountAccounting As Integer) As Domain.Entities.Supplier

    ''' <summary>
    ''' funcion para almacenar un fabricante
    ''' </summary>
    ''' <param name="Person"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveSupplier(Person As Supplier) As Boolean
    ''' <summary>
    ''' consulta para retornar un fabricante teniendo en cuenta el codigo
    ''' </summary>
    ''' <returns>Objeto fabricanre</returns>
    Function GetSupplierById(ByVal Id As Integer, Optional tracking As Boolean = True) As Supplier
    ''' <summary>
    ''' consulta para retornar un fabricante teniendo en cuenta el id
    ''' </summary>
    ''' <returns>Objeto fabricanre</returns>
    Function GetThirdPartyByIdSupplier(ByVal Id As Integer, Optional tracking As Boolean = True) As ThirdParty

    ''' <summary>
    ''' Obtiene un proveedor por el nit del tercero
    ''' </summary>
    ''' <param name="nitThirdParty"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSupplierByNitThirdParty(ByVal nitThirdParty As String) As Supplier

End Interface
