'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 25-03-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.FixedAsset
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

Partial Class FixedAssetService

    Implements IFixedAssetItemTypePartsAccesoriesConsumablesService

    ''' <summary>
    ''' Función para Almacenar los Tipo de Equipo x Partes, Accesorios y Consumibles
    ''' </summary>
    ''' <param name="ListEquipmentTypePartsAccesoriesConsumablesService"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteEquipmentTypePartsAccesoriesConsumablesService(ListEquipmentTypePartsAccesoriesConsumablesService As List(Of Domain.Entities.FixedAssetItemTypePartsAccesories), session As SessionValues) As Boolean Implements IFixedAssetItemTypePartsAccesoriesConsumablesService.DeleteEquipmentTypePartsAccesoriesConsumablesService
        Using service As IFixedAssetItemTypePartsAccesoriesConsumablesAdminService = Container.Current.Resolve(Of IFixedAssetItemTypePartsAccesoriesConsumablesAdminService)()
            Return service.DeleteEquipmentTypePartsAccesoriesConsumables(ListEquipmentTypePartsAccesoriesConsumablesService, session.AuditMessageWcf)
        End Using
        'Return _FixedAssetItemTypePartsAccesoriesAdminService.DeleteEquipmentTypePartsAccesoriesConsumables(ListEquipmentTypePartsAccesoriesConsumablesService, session.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Función para Almacenar los Tipo de Equipo x Partes, Accesorios y Consumibles
    ''' </summary>
    ''' <param name="ListEquipmentTypePartsAccesoriesConsumablesService">ListEquipmentTypePartsAccesoriesConsumablesService</param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveEquipmentTypePartsAccesoriesConsumablesService(ListEquipmentTypePartsAccesoriesConsumablesService As List(Of Domain.Entities.FixedAssetItemTypePartsAccesories), session As SessionValues) As Boolean Implements IFixedAssetItemTypePartsAccesoriesConsumablesService.SaveEquipmentTypePartsAccesoriesConsumablesService
        Using service As IFixedAssetItemTypePartsAccesoriesConsumablesAdminService = Container.Current.Resolve(Of IFixedAssetItemTypePartsAccesoriesConsumablesAdminService)()
            Return service.SaveEquipmentTypePartsAccesoriesConsumables(ListEquipmentTypePartsAccesoriesConsumablesService, session.AuditMessageWcf)
        End Using
        'Return _FixedAssetItemTypePartsAccesoriesAdminService.SaveEquipmentTypePartsAccesoriesConsumables(ListEquipmentTypePartsAccesoriesConsumablesService, session.AuditMessageWcf)
    End Function
End Class
