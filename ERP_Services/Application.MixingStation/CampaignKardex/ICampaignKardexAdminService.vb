'***********************************************************************
' Assembly         : Application.MixingStation
' Author           : Diego A. Roldan
' Created          : 2021-09-09
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface ICampaignKardexAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Registra un movimiento
    ''' </summary>
    ''' <returns></returns>
    Function Savekardex(Of TEntity As {IObjectWithChangeTracker})(
        campaignDetailId As Integer,
        movementType As eMovementType,
        productId As Integer,
        batchSerialId As Integer?,
        quantity As Decimal,
        measurementUnitId As Integer,
        description As String,
        audit As AuditMessage
    ) As ActionResult(Of CampaignKardex)

    ''' <summary>
    ''' registrar movimiento en el kardex desde Materia Prima Indirecta
    ''' </summary>
    ''' <param name="LisIndirectMPQuantity"></param>
    ''' <param name="Audit"></param>
    ''' <returns></returns>
    Function RegisterIndirectMPQuantity(LisIndirectMPQuantity As List(Of IndirectMPQuantity), Audit As AuditMessage) As ActionResult
End Interface
