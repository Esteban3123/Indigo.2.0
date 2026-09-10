#Region "Imports"
Imports System.ServiceModel
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region

<ServiceContract()> _
Public Interface IFixedAssetItemCatalogService
    <OperationContract()>
    Function DeleteEquipmentCatalog(EquipmentCatalog As Domain.Entities.FixedAssetItemCatalog, audit As AuditMessage) As ActionResult

    <OperationContract()>
    Function SaveEquipmentCatalog(FixedAssetItemCatalog As Domain.Entities.FixedAssetItemCatalog, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetItemCatalog)

    <OperationContract()> _
    Function GetEquipmentCatalog(ByVal codeEquipmentCatalog As String) As FixedAssetItemCatalog

    ''' <summary>
    ''' Cambia el estado  del artículo según el código
    ''' </summary>
    ''' <param name="code">Código del artículo</param>
    <OperationContract()>
    Function Change_StateEquipmentCatalog(code As String, state As Boolean, session As SessionValues) As ActionResult(Of Domain.Entities.FixedAssetItemCatalog)

    ''' <summary>
    ''' Función para importar detalles de catálogo de artículos desde archivo
    ''' </summary>
    ''' <param name="DataImport"></param>
    ''' <param name="data"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function SetFixedAssetItemCatalogDetailFromFile(DataImport As List(Of ImportFileRow), data As List(Of List(Of String))) As List(Of SP_SetFixedAssetItemCatalogDetailFromFile_Result)
End Interface
