'***********************************************************************
' Assembly         : Application.Maintenance
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 27-08-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
#End Region

Public Interface IFixedAssetItemTypePartsAccesoriesConsumablesAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Función para Almacenar una Marca
    ''' </summary>
    ''' <param name="ListEquipmentTypeTechnicalLog">Objeto ListEquipmentTypeTechnicalLog</param>
    ''' <param name="audit"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function SaveEquipmentTypePartsAccesoriesConsumables(ListEquipmentTypePartsAccesoriesConsumables As List(Of FixedAssetItemTypePartsAccesories), audit As AuditMessage) As Boolean

    ''' <summary>
    ''' Elimina una Lista de Equipos x Registro Técnico
    ''' </summary>
    ''' <param name="ListEquipmentTypeTechnicalLog"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DeleteEquipmentTypePartsAccesoriesConsumables(ListEquipmentTypePartsAccesoriesConsumables As List(Of Domain.Entities.FixedAssetItemTypePartsAccesories), audit As AuditMessage) As Boolean

End Interface
