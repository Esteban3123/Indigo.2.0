'***********************************************************************
' Assembly         : Application.MixingStation
' Author           : Giovanny Plazas L
' Created          : 16-09-2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports System.Data.Entity.Infrastructure
Imports System.Text
Imports System.Transactions
Imports Application.Base
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Resources

#End Region
Public Class QuantityRemainingAdminService
    Implements IQuantityRemainingAdminService, Inject

#Region "Properties"
    Private Const FORM_NAME As String = "FrmPackage"

    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _quantityRemainingRepository As IQuantityRemainingRepository

    Private _campaignKardexAdminService As ICampaignKardexAdminService
    Private _campaignKardexRepository As ICampaignKardexRepository
    Private _campaignDetailValidationRepository As ICampaignDetailValidationRepository

#End Region

#Region "Methods"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal QuantityRemainingRepository As IQuantityRemainingRepository, ByVal CampaignKardexAdminService As ICampaignKardexAdminService _
                   , ByVal CampaignKardexRepository As ICampaignKardexRepository, ByVal CampaignDetailValidationRepository As ICampaignDetailValidationRepository)
        If QuantityRemainingRepository Is Nothing Then
            Throw New ArgumentNullException("QuantityRemainingRepository Vacio")
        End If
        If CampaignKardexAdminService Is Nothing Then
            Throw New ArgumentNullException("CampaignKardexAdminServiceRepository Vacio")
        End If
        If CampaignKardexRepository Is Nothing Then
            Throw New ArgumentNullException("CampaignKardexRepository Vacio")
        End If
        If CampaignDetailValidationRepository Is Nothing Then
            Throw New ArgumentNullException("CampaignDetailValidationRepository Vacio")
        End If
        Me._campaignDetailValidationRepository = CampaignDetailValidationRepository
        Me._quantityRemainingRepository = QuantityRemainingRepository
        Me._campaignKardexAdminService = CampaignKardexAdminService
        Me._campaignKardexRepository = CampaignKardexRepository
    End Sub

    ''' <summary>
    ''' Devuelve todos los sobrantes
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllQuantityRemaining() As ActionResult(Of List(Of QuantityRemaining)) Implements IQuantityRemainingAdminService.GetAllQuantityRemaining
        Try
            Dim Result = _quantityRemainingRepository.GetAllQuantityRemaining()
            If Result.Count = 0 Then
                Return New ActionResult(Of List(Of QuantityRemaining)) With {.StateResult = False, .Message = "No se encontraron datos"}
            End If

            Return New ActionResult(Of List(Of QuantityRemaining)) With {.StateResult = True, .ObjectEmbbeded = Result}
        Catch ex As Exception
            Return New ActionResult(Of List(Of QuantityRemaining)) With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' funcion para consultar la tabla de remanentes por almacen de remanente el cual se parametriza por central de mezclas
    ''' </summary>
    ''' <returns></returns>
    Public Function GetQuantityRemainingByMixingStation(_cMConfigurationId As Integer) As ActionResult(Of List(Of QuantityRemaining)) Implements IQuantityRemainingAdminService.GetQuantityRemainingByMixingStation
        Try
            Dim Result = _quantityRemainingRepository.GetQuantityRemainingByMixingStation(_cMConfigurationId)
            If Not Result.Any() Then
                Return New ActionResult(Of List(Of QuantityRemaining)) With {.StateResult = False, .Message = "No se encontraron datos"}
            End If

            Return New ActionResult(Of List(Of QuantityRemaining)) With {.StateResult = True, .ObjectEmbbeded = Result}
        Catch ex As Exception
            Return New ActionResult(Of List(Of QuantityRemaining)) With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function



    ''' <summary>
    ''' Guarda o actualiza la tabla de  Sobrantes
    ''' </summary>
    ''' <param name="ListQuantityRemaining"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveQuantityRemaining(ListQuantityRemaining As List(Of QuantityRemaining), audit As AuditMessage, Optional InvokeFromHernessed As Boolean = False) As ActionResult Implements IQuantityRemainingAdminService.SaveQuantityRemaining
        If ListQuantityRemaining Is Nothing Then
            Throw New ArgumentNullException("ObjEntity")
        End If
        Dim unitOfWork As IUnitWork = Me._quantityRemainingRepository.UnitWork
        Try
            If InvokeFromHernessed = False Then
                ValidationsQuantity(ListQuantityRemaining)
            End If

            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                ListQuantityRemaining.ForEach(Sub(x As QuantityRemaining)
                                                  If x.CreationUser Is Nothing Then
                                                      x.Balance = x.Quantity
                                                      x.CreationDate = Date.Now()
                                                      x.CreationUser = audit.CodeUser
                                                  End If

                                                  If x.Id > 0 Then
                                                      x.MarkAsModified()

                                                      Dim kardex = _campaignKardexRepository.FirstOrDefault(Function(m) m.CampaignDetailId = x.CampaignDetailId _
                                                                            AndAlso m.EntityName = GetType(QuantityRemaining).Name _
                                                                            AndAlso m.MovementType = eMovementType.Output _
                                                                            AndAlso m.ProductId = x.ProductId _
                                                                            AndAlso m.BatchSerialId = x.BatchSerialId)

                                                      If kardex IsNot Nothing Then
                                                          _campaignKardexRepository.DeleteEntity(kardex)
                                                      End If
                                                  End If

                                                  _campaignKardexAdminService.Savekardex(Of QuantityRemaining)(x.CampaignDetailId, eMovementType.Output, x.ProductId, x.BatchSerialId, x.Quantity, x.UnitMeasurementId _
                                                            , "Movimiento Sobrante", audit)

                                                  _quantityRemainingRepository.SaveEntity(x)
                                              End Sub)
                unitOfWork.Commit()
                scope.Complete()
                Return New ActionResult With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As IndigoValidationException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = ex.Message}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' validacion que no permite que la cantidad de sorbante sea mayor al de la campaña
    ''' </summary>
    ''' <param name="ListQuantityRemaining"></param>
    Private Sub ValidationsQuantity(ListQuantityRemaining As List(Of QuantityRemaining))
        If ListQuantityRemaining Is Nothing OrElse ListQuantityRemaining.Count = 0 Then
            Throw New IndigoValidationException("El sobrante no se ha podido validar")
        End If

        Dim campaignDetailValidation = _campaignDetailValidationRepository.GetCampaignDetailValidationByCampaignDetailId(ListQuantityRemaining(0).CampaignDetailId)
        Dim sb As New StringBuilder()

        For Each r In ListQuantityRemaining
            Dim quantityRemaining = _quantityRemainingRepository.GetListQuantityRemaining(r.CampaignDetailId, r.ProductId, r.BatchSerialId)
            If quantityRemaining Is Nothing Then
                sb.AppendLine("")
                Continue For
            End If

            Dim validation = campaignDetailValidation.FirstOrDefault(Function(m) m.ProductId = r.ProductId AndAlso m.BatchSerialId = r.BatchSerialId)
            Dim remainingQuantity = r.Quantity

            If validation.DeliveredQuantityUnitMeasurement < remainingQuantity Then
                sb.AppendLine($"La cantidad Digitada Supera la cantidad de entrada a la campaña del producto {validation.ProductFullName} - {validation.BatchSerialCode} por una diferencia de {Math.Round(remainingQuantity - validation.DeliveredQuantityUnitMeasurement, 2)} ")
            End If

            If quantityRemaining.Count = 0 Then
                Continue For
            End If

            If quantityRemaining.Count <> 1 Then
                sb.AppendLine($"Existe más de un registro adicionado como remanente")
                Continue For
            End If

            If quantityRemaining.First().Id <> r.Id Then
                sb.AppendLine($"La información del remanente no coincide con el registro editado")
                Continue For
            End If
        Next

        If sb.Length > 0 Then
            Throw New IndigoValidationException(sb.ToString())
        End If
    End Sub


#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If

            _quantityRemainingRepository = Nothing
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

#Region "Properties"

#End Region
End Class
