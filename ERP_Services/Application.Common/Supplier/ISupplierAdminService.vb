
#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region


''' <summary>
''' interface que especifica las metodos y funciones que manejara todas las acciones sobre la entidad fabricante
''' </summary>
''' <remarks></remarks>
Public Interface ISupplierAdminService
    Inherits IDisposable

    ''' <summary>
    ''' funcion que sirve para lñistar todas los fabricantes
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllSupplier() As List(Of Supplier)

    ''' <summary>
    ''' Funcion que lista el porcentaje de retencion de iva que maneja el proveedor
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetIVARetentionPercentageBySupplierId(SupplierId As Integer) As Decimal

    ''' <summary>
    ''' funcion que sirve para eliminar un fabricante
    ''' </summary>
    ''' <param name="Supplier"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DeleteSupplier(ByVal Supplier As Supplier, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene el id del proveedor por el id de la linea de distribucción
    ''' </summary>
    ''' <param name="IdDistributionLines"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSupplierIdByIdDistributionLines(ByVal IdDistributionLines As Integer) As Integer

    ''' <summary>
    ''' funcion que sirve para guardar un fabricante
    ''' </summary>
    ''' <param name="Supplier"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveSupplier(ByVal Supplier As Supplier, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of Supplier)

    ''' <summary>
    ''' funciona que sirve para listar un fabricante
    ''' </summary>
    ''' <param name="codeSupplier"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSupplier(ByVal codeSupplier As String) As Supplier

    ''' <summary>
    ''' funciona que sirve para listar un fabricante
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSupplierById(ByVal id As Integer) As Supplier

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
    ''' funciona que sirve para listar un fabricante
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetThirdPartyById(ByVal id As Integer) As ThirdParty

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeState(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of Supplier)
    ''' <summary>
    ''' consulta para retornar un fabricante teniendo en cuenta el id
    ''' </summary>
    ''' <returns>Objeto fabricanre</returns>
    Function GetThirdPartyByIdSupplier(ByVal Id As Integer) As ThirdParty
End Interface
