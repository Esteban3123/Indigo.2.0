'***********************************************************************
' Assembly         : DistributedServices.Maintenance
' Author           : Daniel Eduardo Arévalo
' Created          : 27-08-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.ServiceModel
Imports Domain.Maintenance.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region

<ServiceContract()> _
Public Interface IEquipmentTypePartsAccesoriesConsumablesService

    ''' <summary>
    ''' Función para Almacenar una Marca
    ''' </summary>
    ''' <param name="ListEquipmentTypePartsAccesoriesConsumablesService">Lista ListEquipmentTypePartsAccesoriesConsumablesService</param>
    ''' <param name="session"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function SaveEquipmentTypePartsAccesoriesConsumablesService(ListEquipmentTypePartsAccesoriesConsumablesService As List(Of EquipmentTypePartsAccesoriesConsumibles), session As SessionValues) As Boolean

    ''' <summary>
    ''' Función para Eliminar los Registros Técnicos x Equipo
    ''' </summary>
    ''' <param name="ListEquipmentTypePartsAccesoriesConsumablesService">ListEquipmentTypePartsAccesoriesConsumablesService</param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function DeleteEquipmentTypePartsAccesoriesConsumablesService(ListEquipmentTypePartsAccesoriesConsumablesService As List(Of EquipmentTypePartsAccesoriesConsumibles), session As SessionValues) As Boolean

End Interface
