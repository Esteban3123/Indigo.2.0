'***********************************************************************
' Assembly         : DistributedServices.Maintenance
' Author           : Daniel Eduardo Arévalo
' Created          : 26-08-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.ServiceModel
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region

<ServiceContract()> _
Public Interface IFixedAssetPartsAccesoriesConsumablesService

    ''' <summary>
    ''' Función que obtiene PartsAccesoriesConsumables
    ''' </summary>
    ''' <returns>Lista de PartsAccesoriesConsumables</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function ListAllPartsAccesoriesConsumables(session As SessionValues) As List(Of FixedAssetPartsAccesoriesConsumables)

    ''' <summary>
    ''' Función que obtiene PartsAccesoriesConsumables
    ''' </summary>
    ''' <param name="Code">Código de PartsAccesoriesConsumables</param>
    ''' <returns>PartsAccesoriesConsumables</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetPartsAccesoriesConsumablesByCode(Code As String, session As SessionValues) As FixedAssetPartsAccesoriesConsumables

    ''' <summary>
    ''' Función para Almacenar PartsAccesoriesConsumables
    ''' </summary>
    ''' <param name="PartsAccesoriesConsumables">Objeto PartsAccesoriesConsumables</param>
    ''' <param name="session"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SavePartsAccesoriesConsumables(PartsAccesoriesConsumables As FixedAssetPartsAccesoriesConsumables, idSequense As Int64, audit As AuditMessage) As ActionResult(Of FixedAssetPartsAccesoriesConsumables)

    ''' <summary>
    ''' Función para Eliminar PartsAccesoriesConsumables
    ''' </summary>
    ''' <param name="PartsAccesoriesConsumables">Objeto PartsAccesoriesConsumables</param>
    ''' <param name="session"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function DeletePartsAccesoriesConsumables(PartsAccesoriesConsumables As Domain.Entities.FixedAssetPartsAccesoriesConsumables, audit As AuditMessage) As ActionResult

    <OperationContract()>
    Function GetPartsAccesoriesConsumablesByEquipmentType(IdEquipmentType As Integer) As List(Of FixedAssetItemTypePartsAccesories)

    <OperationContract()>
    Function ChangeFixedAssetPartsAccesoriesConsumablesState(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of FixedAssetPartsAccesoriesConsumables)


End Interface
