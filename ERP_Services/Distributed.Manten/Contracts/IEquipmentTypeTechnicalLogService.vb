'***********************************************************************
' Assembly         : DistributedServices.Maintenance
' Author           : Daniel Eduardo Arévalo
' Created          : 25-03-2015
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
Public Interface IEquipmentTypeTechnicalLogService

    ''' <summary>
    ''' Función para Almacenar una Marca
    ''' </summary>
    ''' <param name="ListEquipmentTypeTechnicalLog">Lista EquipmentTypeTechnicalLog</param>
    ''' <param name="session"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function SaveEquipmentTypeTechnicalLog(ListEquipmentTypeTechnicalLog As List(Of EquipmentTypeTechnicalLog), session As SessionValues) As Boolean

    ''' <summary>
    ''' Función para Eliminar los Registros Técnicos x Equipo
    ''' </summary>
    ''' <param name="ListEquipmentTypeTechnicalLog">ListEquipmentTypeTechnicalLog</param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function DeleteEquipmentTypeTechnicalLog(ListEquipmentTypeTechnicalLog As List(Of EquipmentTypeTechnicalLog), session As SessionValues) As Boolean

End Interface
