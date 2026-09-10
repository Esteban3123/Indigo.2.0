'***********************************************************************
' Assembly         : Domain.FixedAsset
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 17/02/2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities

#End Region

Public Interface IFixedAssetEntryRepository
    Inherits IRepository(Of FixedAssetEntry)

    ''' <summary>
    ''' Obtiene un Ingreso de Activos por Código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFixedAssetEntry(code As String) As FixedAssetEntry

    ''' <summary>
    ''' Obtiene un ingreso de activos por id sin agregados
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Function GetFixedAssetEntrySimpleById(Id As Integer) As FixedAssetEntry

    ''' <summary>
    ''' Obtiene un ingreso por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFixedAssetEntryById(Id As Integer) As FixedAssetEntry

    ''' <summary>
    ''' Guarda un ingreso de activos
    ''' </summary>
    ''' <param name="ListDeleteString">Objeto xml de los listados de eliminados</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_SaveFixedAssetEntry(FixedAssetEntryXml As String, ListDeleteString As List(Of String), codeUser As String) As SP_SaveFixedAssetEntry_Result

    ''' <summary>
    ''' Obtiene los parametros de activo fijo por unidad operativa
    ''' </summary>
    ''' <param name="OperatingUnitId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSettingFixedAssetByOperatingUnidId(OperatingUnitId As Integer) As SettingFixedAsset

    ''' <summary>
    ''' Obtiene el tercero con el id de la relacion de linea de distribucion y proveedor
    ''' </summary>
    ''' <param name="SupplierDistributionLineId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAccountPayableConceptBySupplierDistributionLineId(SupplierDistributionLineId As Integer) As AccountPayableConcepts

    ''' <summary>
    ''' Obtiene concepto de pago por id
    ''' </summary>
    ''' <param name="AccountPayableConceptId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAccountPayableConceptById(AccountPayableConceptId As Integer) As AccountPayableConcepts

    ''' <summary>
    ''' Obtiene la linea de distribucion por id de la relacion de proveedor y linea de distribucion
    ''' </summary>
    ''' <param name="SupplierDistributionLineId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDistributionLineBySupplierDistributionLineId(SupplierDistributionLineId As Integer) As DistributionLines

    ''' <summary>
    ''' Obtiene el tercero por la relacion de linea distribucion y proveedor
    ''' </summary>
    ''' <param name="SupplierDistributionLineId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetThirdPartyBySupplierDistributionLineId(SupplierDistributionLineId As Integer) As ThirdParty

    ''' <summary>
    ''' Obtiene el catalogo que tiene relacionado el articulo
    ''' </summary>
    ''' <param name="ItemId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFixedAssetItemCatalogByItemId(ItemId As Integer) As FixedAssetItemCatalog

    ''' <summary>
    ''' Obtiene el proveedor por la relacion del proveedor y linea de distribucion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSupplierBySupplierDistributionLineId(SupplierDistributionLineId As Integer) As Supplier

    ''' <summary>
    ''' Obtiene el articulo por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFixedAssetItemById(Id As Integer) As FixedAssetItem

    ''' <summary>
    ''' Obtiene un catalogo de articulos
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFixedAssetItemCatalogById(Id As Integer) As FixedAssetItemCatalog

    ''' <summary>
    ''' Obtiene el tipo de comprobante
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetJournalVoucherType(Id As Integer) As JournalVoucherTypes

    ''' <summary>
    ''' Obtiene la localización por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFixedAssetLocationById(Id As Integer) As FixedAssetLocation

    ''' <summary>
    ''' Obtiene el registro del libro oficial de los libros parametrizados en el catalogo
    ''' </summary>
    ''' <param name="FixedAssetItemCatalogId"></param>
    ''' <returns></returns>
    Function GetOfficialBookInCatalogBooks(FixedAssetItemCatalogId As Integer) As FixedAssetItemCatalogAdquisitionType

    ''' <summary>
    ''' Metodo que obtiene todos los catalogos de los articulos
    ''' </summary>
    ''' <param name="ListItemId"></param>
    ''' <returns></returns>
    Function GetItemByItemIds(ListItemId As List(Of Integer)) As List(Of FixedAssetItem)

    ''' <summary>
    ''' Valida que la cantidad de los productos que tengan presupuesto asociado al grupo sea igual que la sumatoria de los compromisos
    ''' </summary>
    ''' <returns></returns>
    Function ValidateItemsAndCommitments(listFixedAssetEntryItem As List(Of FixedAssetEntryItem), listFixedAssetEntryCommitment As List(Of FixedAssetEntryCommitment)) As Boolean

End Interface
