'***********************************************************************
' Assembly         : Application.MixingStation
' Author           : Diego A. Roldan
' Created          : 2021-09-09
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions

Public Class CampaignKardexAdminService
    Implements ICampaignKardexAdminService, Inject

    ''' <summary>
    ''' Repositorio del kardex
    ''' </summary>
    Private ReadOnly _campaignKardexRepository As ICampaignKardexRepository

    ''' <summary>
    ''' Constructor
    ''' </summary>
    Public Sub New(campaignKardexRepository As ICampaignKardexRepository)
        _campaignKardexRepository = campaignKardexRepository
    End Sub

    ''' <summary>
    ''' Guarda un movimiento en el kardex de las campañas
    ''' </summary>
    ''' <param name="campaignDetailId"></param>
    ''' <param name="movementType"></param>
    ''' <param name="productId"></param>
    ''' <param name="batchSerialId"></param>
    ''' <param name="quantity"></param>
    ''' <param name="measurementUnitId"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function Savekardex(Of TEntity As {IObjectWithChangeTracker})(
        campaignDetailId As Integer,
        movementType As eMovementType,
        productId As Integer,
        batchSerialId As Integer?,
        quantity As Decimal,
        measurementUnitId As Integer,
        description As String,
        audit As AuditMessage
    ) As ActionResult(Of CampaignKardex) Implements ICampaignKardexAdminService.Savekardex
        Try

            Dim newKardex As New CampaignKardex With {
                .CampaignDetailId = campaignDetailId,
                .MovementType = movementType.GetHashCode(),
                .MovementDate = Date.Now,
                .EntityName = GetType(TEntity).Name,
                .Description = description,
                .ProductId = productId,
                .BatchSerialId = batchSerialId,
                .Quantity = quantity,
                .MeasurementUnitId = measurementUnitId,
                .CreationUser = audit.CodeUser,
                .CreationDate = Date.Now
            }

            _campaignKardexRepository.SaveEntity(newKardex)
            _campaignKardexRepository.UnitWork.Commit()

            Return New ActionResult(Of CampaignKardex) With {.StateResult = True, .ObjectEmbbeded = newKardex}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CampaignKardex) With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' Funcion para registra Materia prima indirecta en el kardex
    ''' </summary>
    ''' <param name="LisIndirectMPQuantity"></param>
    ''' <param name="Audit"></param>
    ''' <returns></returns>
    Public Function RegisterIndirectMPQuantity(LisIndirectMPQuantity As List(Of IndirectMPQuantity), Audit As AuditMessage) As ActionResult Implements ICampaignKardexAdminService.RegisterIndirectMPQuantity
        Try
            If LisIndirectMPQuantity Is Nothing OrElse LisIndirectMPQuantity.Count = 0 Then
                Return New ActionResult With {.StateResult = False, .Message = "Se ha enviado un objeto vacio"}
            End If
            Dim Result = New ActionResult(Of CampaignKardex)
            Result.StateResult = True

            LisIndirectMPQuantity.ForEach(Sub(x)
                                              If Result.StateResult Then
                                                  Result = Savekardex(Of IndirectMPQuantity)(x.CampaignDetailId, x.MovementType, x.ProductId, x.BatchSerialId, x.Quantity, x.MeasurementUnitId, x.Description, Audit)
                                              Else
                                                  Exit Sub
                                              End If
                                          End Sub
            )
            If Result.StateResult = False Then
                Return New ActionResult With {.StateResult = Result.StateResult, .Message = Result.Message}
            End If

            Return New ActionResult With {.StateResult = True}

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If

            IndigoGC.Execute()
        End If
        disposedValue = True
    End Sub

    ' Visual Basic agrega este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

#End Region

End Class
