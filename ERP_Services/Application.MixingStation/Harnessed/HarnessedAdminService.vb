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
Public Class HarnessedAdminService
    Implements IHarnessedAdminService, Inject

#Region "Properties"
    Private Const FORM_NAME As String = "FrmPackage"

    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _HarnessedRepository As IHarnessedRepository
    Private _campaignKardexAdminService As ICampaignKardexAdminService
    Private _quantityRemainingAdminService As IQuantityRemainingAdminService

#End Region

#Region "Methods"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal HarnessedRepository As IHarnessedRepository, ByVal CampaignKardexAdminService As ICampaignKardexAdminService _
                   , ByVal QuantityRemainingAdminService As IQuantityRemainingAdminService)
        If HarnessedRepository Is Nothing Then
            Throw New ArgumentNullException("HarnessedRepository Vacio")
        End If
        If CampaignKardexAdminService Is Nothing Then
            Throw New ArgumentNullException("CampaignKardexAdminServiceRepository Vacio")
        End If
        If QuantityRemainingAdminService Is Nothing Then
            Throw New ArgumentNullException("QuantityRemainingAdminService Vacio")
        End If
        Me._quantityRemainingAdminService = QuantityRemainingAdminService
        Me._HarnessedRepository = HarnessedRepository
        Me._campaignKardexAdminService = CampaignKardexAdminService
    End Sub


    ''' <summary>
    ''' Guarda o actualiza la tabla de  Sobrantes
    ''' </summary>
    ''' <param name="ListHarnessed"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveHarnessed(ListHarnessed As List(Of Harnessed), audit As AuditMessage) As ActionResult Implements IHarnessedAdminService.SaveHarnessed
        If ListHarnessed Is Nothing Then
            Throw New ArgumentNullException("ObjEntity")
        End If
        Dim unitOfWork As IUnitWork = Me._HarnessedRepository.UnitWork
        Try

            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty
                ListHarnessed.ForEach(Sub(x)
                                          If x.Id > 0 Then
                                              x.MarkAsModified()
                                          Else
                                              x.CreationUser = audit.CodeUser
                                              x.CreationDate = Date.Now()
                                              _campaignKardexAdminService.Savekardex(Of Harnessed)(x.CampaignDetailId, eMovementType.Input, x.ProductId, x.BatchSerialId, x.Quantity, x.UnitMeasurementId _
                                                            , "Movimiento de aprovechamiento", audit)
                                          End If

                                          _HarnessedRepository.SaveEntity(x)
                                      End Sub)
                unitOfWork.Commit()
                scope.Complete()
                Return New ActionResult With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' funcion para registrar el movimiento de aprovechamiento afectando a  la tabla de sobrantes y kardex
    ''' </summary>
    ''' <param name="ListQuantityRemaining"></param>
    ''' <param name="CampaignDetailId"></param>
    ''' <param name="Audit"></param>
    ''' <returns></returns>
    Public Function RegisterHarnessed(ListQuantityRemaining As List(Of QuantityRemaining), CampaignDetailId As Integer, Audit As AuditMessage) As ActionResult Implements IHarnessedAdminService.RegisterHarnessed
        If ListQuantityRemaining Is Nothing OrElse ListQuantityRemaining.Count = 0 Then
            Throw New ArgumentNullException("ObjEntity")
        End If
        Dim unitOfWork As IUnitWork = Me._HarnessedRepository.UnitWork

        Try
            Dim sb As New StringBuilder()
            Dim ListHarnessed = New List(Of Harnessed)
            ListQuantityRemaining.ForEach(Sub(v)
                                              v.SpendQuantity = v.SpendQuantity + v.QuantityHarnessed
                                              v.Balance = v.Quantity - v.SpendQuantity
                                              If v.Balance > v.Quantity Then
                                                  sb.AppendLine($"La Cantidad Ingresada supera la Asociada a la cantidad del sobrante sobrante por {v.Balance - v.Quantity}")
                                                  Exit Sub
                                              End If
                                              Dim Harnessed = New Harnessed
                                              With Harnessed
                                                  .QuantityRemainingId = v.Id
                                                  .CampaignDetailId = CampaignDetailId
                                                  .Quantity = v.QuantityHarnessed
                                                  .ProductId = v.ProductId
                                                  .BatchSerialId = v.BatchSerialId
                                                  .UnitMeasurementId = v.UnitMeasurementId
                                              End With
                                              ListHarnessed.Add(Harnessed)
                                          End Sub)

            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})

                Dim ResultQR = _quantityRemainingAdminService.SaveQuantityRemaining(ListQuantityRemaining, Audit, True)

                If ResultQR.StateResult = False Then
                    unitOfWork.RollbackChanges()
                    Return ResultQR
                End If

                Dim ResultH = SaveHarnessed(ListHarnessed, Audit)
                If ResultH.StateResult = False Then
                    unitOfWork.RollbackChanges()
                    Return ResultH
                End If

                unitOfWork.Commit()
                scope.Complete()
                Return New ActionResult With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .Message = IIf(sb.Length > 0, String.Join(" ;", sb), "")}

            End Using
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function


#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If

            _HarnessedRepository = Nothing
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
