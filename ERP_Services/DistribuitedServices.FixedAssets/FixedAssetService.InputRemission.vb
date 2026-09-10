Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Application.FixedAsset
Imports Microsoft.Practices.Unity

Partial Public Class FixedAssetService

    ''' <summary>
    ''' Guarda o actualiza una Ubicación
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveFixedAssetRemissionEntrance(ByVal FixedAssetRemissionEntrance As FixedAssetRemissionEntrance, idSequense As Int64, audit As AuditMessage) As ActionResult(Of FixedAssetRemissionEntrance) Implements IFixedAssetFixedAssetRemissionEntranceService.SaveFixedAssetRemissionEntrance
        Using service As IFixedAssetRemissionEntranceAdminService = Container.Current.Resolve(Of IFixedAssetRemissionEntranceAdminService)()
            Return service.SaveFixedAssetRemissionEntrance(FixedAssetRemissionEntrance, audit, idSequense)
        End Using
        'Return Me._fixedAssetFixedAssetRemissionEntranceAdminService.SaveFixedAssetRemissionEntrance(FixedAssetRemissionEntrance, audit, idSequense)
    End Function

    ''' <summary>
    ''' Elimina una Ubicación
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteFixedAssetRemissionEntrance(FixedAssetRemissionEntrance As FixedAssetRemissionEntrance, audit As AuditMessage) As Boolean Implements IFixedAssetFixedAssetRemissionEntranceService.DeleteFixedAssetRemissionEntrance
        Using service As IFixedAssetRemissionEntranceAdminService = Container.Current.Resolve(Of IFixedAssetRemissionEntranceAdminService)()
            Return service.DeleteFixedAssetRemissionEntrance(FixedAssetRemissionEntrance, audit)
        End Using
        'Return Me._fixedAssetFixedAssetRemissionEntranceAdminService.DeleteFixedAssetRemissionEntrance(FixedAssetRemissionEntrance, audit)
    End Function

    ''' <summary>
    ''' Obtiene una Ubicación
    ''' </summary>
    ''' <param name="codeFixedAssetRemissionEntrance"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFixedAssetRemissionEntrance(ByVal codeFixedAssetRemissionEntrance As String) As FixedAssetRemissionEntrance Implements IFixedAssetFixedAssetRemissionEntranceService.GetFixedAssetRemissionEntrance
        Using service As IFixedAssetRemissionEntranceAdminService = Container.Current.Resolve(Of IFixedAssetRemissionEntranceAdminService)()
            Return service.GetFixedAssetRemissionEntranceByCode(codeFixedAssetRemissionEntrance)
        End Using
        'Return Me._fixedAssetFixedAssetRemissionEntranceAdminService.GetFixedAssetRemissionEntranceByCode(codeFixedAssetRemissionEntrance)
    End Function

End Class
