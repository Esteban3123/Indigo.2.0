'***********************************************************************
' Assembly         : DistributedServices.MixinStation
' Author           : Yoe Andres Cardenas
' Created          : 06/06/2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

<ServiceContract()>
Public Interface IMixingStationServiceCampaignKardex

    ''' <summary>
    ''' Servicio para guardar en el kardex de la campaña
    ''' </summary>
    ''' <typeparam name="TEntity"></typeparam>
    ''' <param name="campaignDetailId"></param>
    ''' <param name="movementType"></param>
    ''' <param name="productId"></param>
    ''' <param name="batchSerialId"></param>
    ''' <param name="quantity"></param>
    ''' <param name="measurementUnitId"></param>
    ''' <param name="description"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function Savekardex(Of TEntity As {IObjectWithChangeTracker})(
        ByVal campaignDetailId As Integer,
        ByVal movementType As eMovementType,
        ByVal productId As Integer,
        ByVal batchSerialId As Integer,
        ByVal quantity As Decimal,
        ByVal measurementUnitId As Integer,
        ByVal description As String,
        ByVal audit As AuditMessage
    ) As ActionResult(Of CampaignKardex)

    ''' <summary>
    ''' registrar movimiento en el kardex desde Materia Prima Indirecta
    ''' </summary>
    ''' <param name="LisIndirectMPQuantity"></param>
    ''' <param name="Audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function RegisterIndirectMPQuantity(LisIndirectMPQuantity As List(Of IndirectMPQuantity), Audit As AuditMessage) As ActionResult


End Interface
