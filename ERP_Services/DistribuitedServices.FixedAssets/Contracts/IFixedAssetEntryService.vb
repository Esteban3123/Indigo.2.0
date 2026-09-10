#Region "Imports"
Imports System.ServiceModel
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region

<ServiceContract()> _
Public Interface IFixedAssetEntryService

    ''' <summary>
    ''' funcion que sirve para guardar una aseguradora
    ''' </summary>
    ''' <param name="FixedAssetEntry">FixedAssetEntry</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveFixedAssetEntry(FixedAssetEntry As Domain.Entities.FixedAssetEntry, ListDeleteFixedAssetEntryItemDetailPartBook As List(Of Domain.Entities.FixedAssetEntryItemDetailPartBook), ListDeleteFixedAssetEntryItemDetailPart As List(Of Domain.Entities.FixedAssetEntryItemDetailPart), ListDeleteFixedAssetEntryItemDetailBook As List(Of Domain.Entities.FixedAssetEntryItemDetailBook), ListDeleteFixedAssetEntryItemDetail As List(Of Domain.Entities.FixedAssetEntryItemDetail), ListDeleteFixedAssetEntryItem As List(Of Domain.Entities.FixedAssetEntryItem), idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetEntry)

    ''' <summary>
    ''' Confirma el ingreso
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ConfirmFixedAssetEntry(FixedAssetEntry As Domain.Entities.FixedAssetEntry, ListDeleteFixedAssetEntryItemDetailPartBook As List(Of Domain.Entities.FixedAssetEntryItemDetailPartBook), ListDeleteFixedAssetEntryItemDetailPart As List(Of Domain.Entities.FixedAssetEntryItemDetailPart), ListDeleteFixedAssetEntryItemDetailBook As List(Of Domain.Entities.FixedAssetEntryItemDetailBook), ListDeleteFixedAssetEntryItemDetail As List(Of Domain.Entities.FixedAssetEntryItemDetail), ListDeleteFixedAssetEntryItem As List(Of Domain.Entities.FixedAssetEntryItem), idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetEntry)

    ''' <summary>
    ''' funciona que sirve para listar una aseguradora
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetFixedAssetEntry(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetEntry)

    ''' <summary>
    ''' Obtiene un ingreso por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetFixedAssetEntryById(Id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetEntry)

    ''' <summary>
    ''' Obtiene el catalogo que tiene relacionado el articulo
    ''' </summary>
    ''' <param name="ItemId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetFixedAssetItemCatalogByItemId(ItemId As Integer) As FixedAssetItemCatalog

    ''' <summary>
    ''' Obtiene la información necesaria para el  informe de Hoja de Vida del Activo.
    ''' </summary>
    ''' <param name="AdquisitionDateStart">The adquisition date start.</param>
    ''' <param name="AdquisitionDateEnd">The adquisition date end.</param>
    ''' <param name="PlateStart">The plate start.</param>
    ''' <param name="PlateEnd">The plate end.</param>
    ''' <param name="session">The session.</param>
    ''' <returns></returns>
    <OperationContract()> _
    Function GetListFixedAssetPhysicalAsset(AdquisitionDateStart As Integer, AdquisitionDateEnd As Integer, PlateStart As Integer, PlateEnd As Integer, ItemStart As Integer, ItemEnd As Integer, ItemTypeStart As Integer, ItemTypeEnd As Integer, LocationStart As Integer, LocationEnd As Integer, Responsible As String, ItemCatalog As String, StatusAsset As String, session As SessionValues) As DataSet

End Interface
