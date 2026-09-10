'***********************************************************************
' Assembly         : DistributedService.MixingStation
' Author           : 
' Created          : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Application.MixingStation
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity
Imports System.ServiceModel
#End Region

Partial Class MixingStationService
    Implements IMixingStationServiceCampaignKardex

    ''' <summary>
    ''' Registra movimientos en el kardex de la campaña
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
    Public Function Savekardex(Of TEntity As {IObjectWithChangeTracker})(
        campaignDetailId As Integer,
        movementType As eMovementType,
        productId As Integer,
        batchSerialId As Integer,
        quantity As Decimal,
        measurementUnitId As Integer,
        description As String,
        audit As AuditMessage
    ) As ActionResult(Of CampaignKardex) Implements IMixingStationServiceCampaignKardex.Savekardex
        Using service As ICampaignKardexAdminService = Container.Current.Resolve(Of ICampaignKardexAdminService)()
            Return service.Savekardex(Of TEntity)(campaignDetailId, movementType, productId, batchSerialId, quantity, measurementUnitId, description, audit)
        End Using
    End Function

    ''' <summary>
    ''' registrar movimiento en el kardex desde Materia Prima Indirecta
    ''' </summary>
    ''' <param name="LisIndirectMPQuantity"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function RegisterIndirectMPQuantity(LisIndirectMPQuantity As List(Of IndirectMPQuantity), audit As AuditMessage) As ActionResult Implements IMixingStationServiceCampaignKardex.RegisterIndirectMPQuantity
        Using service As ICampaignKardexAdminService = Container.Current.Resolve(Of ICampaignKardexAdminService)()
            Return service.RegisterIndirectMPQuantity(LisIndirectMPQuantity, audit)
        End Using
    End Function

End Class
