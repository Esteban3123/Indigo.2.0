#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region

Public Interface IFixedAssetEntryAdminService
    Inherits IDisposable

    ''' <summary>
    ''' funcion que sirve para guardar una aseguradora
    ''' </summary>
    ''' <param name="FixedAssetEntry">FixedAssetEntry</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveFixedAssetEntry(ByVal FixedAssetEntry As FixedAssetEntry, ListDeleteFixedAssetEntryItemDetailPartBook As List(Of FixedAssetEntryItemDetailPartBook),
                                 ListDeleteFixedAssetEntryItemDetailPart As List(Of FixedAssetEntryItemDetailPart), ListDeleteFixedAssetEntryItemDetailBook As List(Of FixedAssetEntryItemDetailBook),
                                 ListDeleteFixedAssetEntryItemDetail As List(Of FixedAssetEntryItemDetail), ListDeleteFixedAssetEntryItem As List(Of FixedAssetEntryItem),
                                 ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of FixedAssetEntry)

    ''' <summary>
    ''' Confirma el ingreso
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ConfirmFixedAssetEntry(ByVal FixedAssetEntry As FixedAssetEntry, ListDeleteFixedAssetEntryItemDetailPartBook As List(Of FixedAssetEntryItemDetailPartBook),
                                 ListDeleteFixedAssetEntryItemDetailPart As List(Of FixedAssetEntryItemDetailPart), ListDeleteFixedAssetEntryItemDetailBook As List(Of FixedAssetEntryItemDetailBook),
                                 ListDeleteFixedAssetEntryItemDetail As List(Of FixedAssetEntryItemDetail), ListDeleteFixedAssetEntryItem As List(Of FixedAssetEntryItem),
                                 ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of FixedAssetEntry)

    ''' <summary>
    ''' funciona que sirve para listar una aseguradora
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFixedAssetEntry(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of FixedAssetEntry)

    ''' <summary>
    ''' Obtiene un ingreso por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFixedAssetEntryById(Id As Integer, ByVal audit As AuditMessage) As ActionResult(Of FixedAssetEntry)

    ''' <summary>
    ''' Obtiene el catalogo que tiene relacionado el articulo
    ''' </summary>
    ''' <param name="ItemId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFixedAssetItemCatalogByItemId(ItemId As Integer) As FixedAssetItemCatalog
    ''' <summary>
    ''' Obtiene la información necesaria para el  informe de Hoja de Vida del Activo
    ''' </summary>
    ''' <param name="AdquisitionDateStart">The adquisition date start.</param>
    ''' <param name="AdquisitionDateEnd">The adquisition date end.</param>
    ''' <param name="PlateStart">The plate start.</param>
    ''' <param name="PlateEnd">The plate end.</param>
    ''' <param name="session">The session.</param>
    ''' <returns></returns>
    Function GetListFixedAssetPhysicalAsset(AdquisitionDateStart As Integer, AdquisitionDateEnd As Integer, PlateStart As Integer, PlateEnd As Integer, ItemStart As Integer, ItemEnd As Integer, ItemTypeStart As Integer, ItemTypeEnd As Integer, LocationStart As Integer, LocationEnd As Integer, Responsible As String, ItemCatalog As String, StatusAsset As String, session As SessionValues) As DataSet

    ''' <summary>
    ''' Importa detalles de catálogos de artículos desde archivo
    ''' </summary>
    ''' <param name="dataimport"></param>
    ''' <param name="data"></param>
    ''' <returns></returns>
    Function SetFixedAssetItemCatalogDetailFromFile(dataimport As List(Of ImportFileRow), data As List(Of List(Of String))) As List(Of SP_SetFixedAssetItemCatalogDetailFromFile_Result)

End Interface
