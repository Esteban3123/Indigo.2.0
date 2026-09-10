Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Microsoft.Practices.Unity
Imports Application.FixedAsset

Partial Public Class FixedAssetService

    ''' <summary>
    ''' Guarda o actualiza una Ubicación
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveEquipmentCatalog(FixedAssetItemCatalog As Domain.Entities.FixedAssetItemCatalog, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetItemCatalog) Implements IFixedAssetItemCatalogService.SaveEquipmentCatalog
        Using service As IEquipmentCatalogAdminService = Container.Current.Resolve(Of IEquipmentCatalogAdminService)()
            Return service.SaveEquipmentCatalog(FixedAssetItemCatalog, audit, idSequense)
        End Using
        'Return Me._FixedAssetItemCatalogAdminService.SaveEquipmentCatalog(FixedAssetItemCatalog, audit, idSequense)
    End Function

    ''' <summary>
    ''' Elimina una Ubicación
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteEquipmentCatalog(EquipmentCatalog As Domain.Entities.FixedAssetItemCatalog, audit As AuditMessage) As ActionResult Implements IFixedAssetItemCatalogService.DeleteEquipmentCatalog
        Using service As IEquipmentCatalogAdminService = Container.Current.Resolve(Of IEquipmentCatalogAdminService)()
            Return service.DeleteEquipmentCatalog(EquipmentCatalog, audit)
        End Using
        'Return Me._FixedAssetItemCatalogAdminService.DeleteEquipmentCatalog(EquipmentCatalog, audit)
    End Function

    ''' <summary>
    ''' Obtiene una Ubicación
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEquipmentCatalog(codeEquipmentCatalog As String) As Domain.Entities.FixedAssetItemCatalog Implements IFixedAssetItemCatalogService.GetEquipmentCatalog
        Using service As IEquipmentCatalogAdminService = Container.Current.Resolve(Of IEquipmentCatalogAdminService)()
            Return service.GetEquipmentCatalogByCode(codeEquipmentCatalog)
        End Using
        'Return Me._FixedAssetItemCatalogAdminService.GetEquipmentCatalogByCode(codeEquipmentCatalog)
    End Function

    ''' <summary>
    ''' Cambia el estado  del artículo según el código
    ''' </summary>
    ''' <param name="code">Código del artículo</param>
    ''' <returns>Cargo</returns> 
    Public Function Change_StateEquipmentCatalog(code As String, state As Boolean, session As SessionValues) As ActionResult(Of Domain.Entities.FixedAssetItemCatalog) Implements IFixedAssetItemCatalogService.Change_StateEquipmentCatalog
        Using service As IEquipmentCatalogAdminService = Container.Current.Resolve(Of IEquipmentCatalogAdminService)()
            Return service.ChangeState(code, state, session.AuditMessageWcf)
        End Using
        'Return _FixedAssetItemCatalogAdminService.ChangeState(code, state, session.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Función para importar detalles de catálogos de artículos desde archivo
    ''' </summary>
    ''' <param name="DataImport"></param>
    ''' <param name="data"></param>
    ''' <returns></returns>
    Private Function IFixedAssetItemCatalogService_SetFixedAssetItemCatalogDetailFromFile(DataImport As List(Of ImportFileRow), data As List(Of List(Of String))) As List(Of SP_SetFixedAssetItemCatalogDetailFromFile_Result) Implements IFixedAssetItemCatalogService.SetFixedAssetItemCatalogDetailFromFile
        Using service As IFixedAssetEntryAdminService = Container.Current.Resolve(Of IFixedAssetEntryAdminService)()
            Return service.SetFixedAssetItemCatalogDetailFromFile(DataImport, data)
        End Using
    End Function

End Class
