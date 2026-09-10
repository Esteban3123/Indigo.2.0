#Region "Imports"

Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

#End Region

<ServiceContract()>
Public Interface IFixedAssetCatalogOfPropertyandServicesService

    ''' <summary>
    ''' guarda un catalogo de bienes y servicios
    ''' </summary>
    ''' <param name="FixedAssetCatalogOfPropertyandServices"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveFixedAssetCatalogOfPropertyandServices(FixedAssetCatalogOfPropertyandServices As FixedAssetCatalogOfPropertyandServices, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of FixedAssetCatalogOfPropertyandServices)

    ''' <summary>
    ''' Obtiene un catalogo de bienes y servicios por medio del codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetFixedAssetCatalogOfPropertyandServicesByCode(code As String, audit As AuditMessage) As ActionResult(Of FixedAssetCatalogOfPropertyandServices)

    ''' <summary>
    ''' Función para Eliminar las Marcas
    ''' </summary>
    ''' <param name="FixedAssetCatalogOfPropertyandServices"></param>
    ''' <param name="audit"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function DeleteFixedAssetCatalogOfPropertyandServices(FixedAssetCatalogOfPropertyandServices As FixedAssetCatalogOfPropertyandServices, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Función para Actualizar estado
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ChangeFixedAssetCatalogOfPropertyandServicesStatus(Code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of FixedAssetCatalogOfPropertyandServices)

End Interface
