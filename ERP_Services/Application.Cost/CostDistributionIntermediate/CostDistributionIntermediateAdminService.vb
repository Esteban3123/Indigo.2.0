'***********************************************************************
' Assembly         : Application.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 15-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Core
Imports Infrastructure.CrossCutting.Resources
Imports Domain.Entities.Service
Imports System.Transactions

Public Class CostDistributionIntermediateAdminService
    Implements ICostDistributionIntermediateAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de distribución intermedia
    ''' </summary>
    Private _distributionIntermediateRepository As ICostDistributionIntermediateRepository

    ''' <summary>
    ''' repositorio de secuencias numericas
    ''' </summary>
    Private _sequenceDRepository As ICostSequenceDetailRepository
    Private _costService As ICostServices

#End Region

#Region "Methods"

    Public Sub New(ByVal distributionIntermediateRepository As ICostDistributionIntermediateRepository, ByVal sequenceDRepository As ICostSequenceDetailRepository,
                   interopCostService As ICostServices)
        If distributionIntermediateRepository Is Nothing Then
            Throw New ArgumentNullException("distributionIntermediateRepository")
        End If
        If sequenceDRepository Is Nothing Then
            Throw New ArgumentNullException("sequenceDRepository")
        End If
        _distributionIntermediateRepository = distributionIntermediateRepository
        _sequenceDRepository = sequenceDRepository
        _costService = interopCostService
    End Sub
#End Region

    ''' <summary>
    ''' Elimina una distribución intermedia
    ''' </summary>
    Public Function DeleteDistributionIntermediate(distributionIntermediate As CostDistributionIntermediate, audit As AuditMessage) As ActionResult Implements ICostDistributionIntermediateAdminService.DeleteDistributionIntermediate
        If distributionIntermediate Is Nothing Then
            Throw New ArgumentNullException("distributionIntermediate")
        End If
        Dim unitOfWork As IUnitWork = Me._distributionIntermediateRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                While distributionIntermediate.CostDistributionIntermediateDetail.Count > 0
                    distributionIntermediate.CostDistributionIntermediateDetail.Item(distributionIntermediate.CostDistributionIntermediateDetail.Count() - 1).MarkAsDeleted()
                End While
                distributionIntermediate.MarkAsDeleted()
                distributionIntermediate.ModificationUser = audit.CodeUser
                distributionIntermediate.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of CostDistributionIntermediate)(distributionIntermediate, audit, status)
                Me._distributionIntermediateRepository.SaveEntity(distributionIntermediate)
                unitOfWork.Commit()
                auditProcess.Execute()
                scope.Complete()
                Return New ActionResult With {.StatusCode = eStatusResult.SUCCESS, .Message = ResourceManager.GetString("RecordDeleted")}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una distribucion Intermedia por codigo
    ''' </summary>
    Public Function GetDistributionIntermediate(code As String, ByVal audit As AuditMessage) As ActionResult(Of CostDistributionIntermediate) Implements ICostDistributionIntermediateAdminService.GetDistributionIntermediate
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim distributionIntermediate As CostDistributionIntermediate = Me._distributionIntermediateRepository.GetDistributionIntermediate(code.Trim())
            If distributionIntermediate IsNot Nothing AndAlso distributionIntermediate.Id > 0 Then
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Print
                Dim auditProcess As New IndigoAuditSimpleEntity(Of CostDistributionIntermediate)(distributionIntermediate, audit, status)
                auditProcess.Execute()
            End If
            Return New ActionResult(Of CostDistributionIntermediate) With {.StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = distributionIntermediate}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CostDistributionIntermediate) With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una distribucion Intermedia por id
    ''' </summary>
    Public Function GetDistributionIntermediateById(id As Integer) As CostDistributionIntermediate Implements ICostDistributionIntermediateAdminService.GetDistributionIntermediateById
        If id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Try
            Return Me._distributionIntermediateRepository.GetDistributionIntermediateById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Lista las distribuciones Intermedia por año y mes
    ''' </summary>
    Public Function ListDistributionIntermediateByYearMonth(year As Integer, month As Integer) As List(Of CostDistributionIntermediate) Implements ICostDistributionIntermediateAdminService.ListDistributionIntermediateByYearMonth
        If year = 0 Then
            Throw New ArgumentNullException("year")
        End If
        If month = 0 Then
            Throw New ArgumentNullException("month")
        End If
        Try
            Dim _listDistributionIntermediate As List(Of CostDistributionIntermediate) = Me._distributionIntermediateRepository.ListDistributionIntermediateByYearMonth(year, month)
            If _listDistributionIntermediate IsNot Nothing AndAlso _listDistributionIntermediate.Count > 0 Then
                'For Each item As DistributionIntermediate In _listDistributionIntermediate
                '    'item.FullNameFixedAsset = String.Concat(mainAccount.AACCODACT.Trim(), " - ", mainAccount.AFNPRODUC.APRNOMBRE.Trim())
                'Next
            End If
            Return _listDistributionIntermediate
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Lista los periodos anteriores que contienen datos al año y mes dados
    ''' </summary>
    Public Function ListPeriodWithDataByMaximumPeriodDistributionIntermediate(year As Integer, month As Integer) As List(Of String) Implements ICostDistributionIntermediateAdminService.ListPeriodWithDataByMaximumPeriodDistributionIntermediate
        If year = 0 Then
            Throw New ArgumentNullException("year")
        End If
        If month = 0 Then
            Throw New ArgumentNullException("month")
        End If
        Try
            Return Me._distributionIntermediateRepository.ListPeriodWithDataByMaximumPeriodDistributionIntermediate(year, month)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guarda una distribución intermedia
    ''' </summary>
    Public Function SaveDistributionIntermediate(distributionIntermediate As CostDistributionIntermediate, audit As AuditMessage, Optional idSequence As Long = 0) As ActionResult(Of CostDistributionIntermediate) Implements ICostDistributionIntermediateAdminService.SaveDistributionIntermediate
        If distributionIntermediate Is Nothing Then
            Throw New ArgumentNullException("distributionIntermediate")
        End If
        Dim unitOfWork As IUnitWork = Me._distributionIntermediateRepository.UnitWork
        Dim sequenceUnitOfWork As IUnitWork = Me._sequenceDRepository.UnitWork
        Try

            Dim resultValidate As ActionResult = _costService.ValidateDistributionIntermediateSave(distributionIntermediate)
            If Not resultValidate.StateResult Then
                unitOfWork.RollbackChangesUnitOfWork()
                Return New ActionResult(Of CostDistributionIntermediate) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = resultValidate.Message, .MessageResult = resultValidate.MessageResult}
            End If
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty
                If String.IsNullOrEmpty(distributionIntermediate.Code) Then
                    Dim seq As CostSecuenceDetail = _sequenceDRepository.GetSequenseDById(idSequence)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.CostSecuence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            distributionIntermediate.Code = res
                            seq.Next += 1
                            Me._sequenceDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of CostDistributionIntermediate) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("SequenceNotFound")}
                        End If
                        MessageResult = If(seq.CostSecuence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), distributionIntermediate.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of CostDistributionIntermediate) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("SequenceNotFound")}
                    End If
                End If

                Dim auxdistributionIntermediate As CostDistributionIntermediate = Nothing
                Dim status As Integer
                If distributionIntermediate.ChangeTracker.State = ObjectState.Added Then
                    If String.IsNullOrEmpty(MessageResult) Then
                        MessageResult = String.Format(ResourceManager.GetString("SavedWithCode"), distributionIntermediate.Code)
                    End If
                    distributionIntermediate.CreationDate = Date.Now
                    distributionIntermediate.CreationUser = audit.CodeUser
                    distributionIntermediate.Status = 1 ' Estado: 1 - Registrado
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    distributionIntermediate.ModificationDate = Date.Now
                    distributionIntermediate.ModificationUser = audit.CodeUser
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                    auxdistributionIntermediate = distributionIntermediate.OriginalValue
                End If
                Me._distributionIntermediateRepository.SaveEntity(distributionIntermediate)

                ' ==================================================================================
                ' COMENTADO - SaveServiceQuantitiesForDistribution (no se usa por ahora)
                ' ==================================================================================
                'If distributionIntermediate.IntermediateDistributionElementId.HasValue Then
                '    System.Diagnostics.Debug.WriteLine($"[DEBUG AdminService] Elemento de distribución detectado: {distributionIntermediate.IntermediateDistributionElementId.Value}")
                '    Me._distributionIntermediateRepository.SaveServiceQuantitiesForDistribution(
                '        distributionIntermediate.Id,
                '        distributionIntermediate.IntermediateDistributionElementId.Value,
                '        distributionIntermediate.Year,
                '        distributionIntermediate.Month,
                '        audit.CodeUser)
                '    System.Diagnostics.Debug.WriteLine($"[DEBUG AdminService] ✅ Cantidades de servicios agregadas al contexto")
                'Else
                '    System.Diagnostics.Debug.WriteLine($"[DEBUG AdminService] No hay IntermediateDistributionElementId configurado. No se guardarán cantidades.")
                'End If
                ' ==================================================================================

                ' UN SOLO Commit() al final para que sea TRANSACCIONAL
                unitOfWork.Commit()
                Dim auditProcess As New IndigoAuditSimpleEntity(Of CostDistributionIntermediate)(distributionIntermediate, audit, status, auxdistributionIntermediate)
                auditProcess.Execute()
                scope.Complete()
                Return New ActionResult(Of CostDistributionIntermediate) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = distributionIntermediate, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of CostDistributionIntermediate) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CostDistributionIntermediate) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Updates the state distribution intermediate.
    ''' </summary>
    Public Function UpdateStateDistributionIntermediate(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of CostDistributionIntermediate) Implements ICostDistributionIntermediateAdminService.UpdateStateDistributionIntermediate
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If String.IsNullOrEmpty(state) Then
            Throw New ArgumentNullException("state")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If

        Try
            Dim distributionIntermediate As CostDistributionIntermediate = Me._distributionIntermediateRepository.GetDistributionIntermediate(code.Trim())
            If distributionIntermediate IsNot Nothing AndAlso distributionIntermediate.Id > 0 Then
                distributionIntermediate.Status = state
            End If
            Return Me.SaveDistributionIntermediate(distributionIntermediate, audit)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CostDistributionIntermediate) With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Confirma una distribución intermedia
    ''' </summary>
    Public Function ConfirmDistributionIntermediate(code As String, audit As AuditMessage) As ActionResult(Of CostDistributionIntermediate) Implements ICostDistributionIntermediateAdminService.ConfirmDistributionIntermediate
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If

        Try
            Dim distributionIntermediate As CostDistributionIntermediate = Me._distributionIntermediateRepository.GetDistributionIntermediate(code.Trim())
            If distributionIntermediate IsNot Nothing AndAlso distributionIntermediate.Id > 0 Then
                ' Verificar si ya está confirmado
                If Not String.IsNullOrEmpty(distributionIntermediate.ConfirmUser) AndAlso distributionIntermediate.ConfirmDate.HasValue Then
                    Return New ActionResult(Of CostDistributionIntermediate) With {
                        .StateResult = False,
                        .StatusCode = eStatusResult.WARNING,
                        .Message = String.Format("La distribución intermedia ya fue confirmada por {0} el {1:dd/MM/yyyy HH:mm}", distributionIntermediate.ConfirmUser, distributionIntermediate.ConfirmDate.Value)
                    }
                End If

                ' Iniciar tracking si no está activo
                If Not distributionIntermediate.ChangeTracker.ChangeTrackingEnabled Then
                    distributionIntermediate.StartTracking()
                End If

                distributionIntermediate.ConfirmUser = audit.CodeUser
                distributionIntermediate.ConfirmDate = DateTime.Now
                distributionIntermediate.Status = 2 ' Estado: 2 - Confirmado

                System.Diagnostics.Debug.WriteLine($"[DEBUG Confirm] Status asignado: {distributionIntermediate.Status}, ChangeTracker.State: {distributionIntermediate.ChangeTracker.State}")
            End If
            Return Me.SaveDistributionIntermediate(distributionIntermediate, audit)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CostDistributionIntermediate) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Anula una distribución intermedia
    ''' </summary>
    Public Function AnnulDistributionIntermediate(code As String, audit As AuditMessage) As ActionResult(Of CostDistributionIntermediate) Implements ICostDistributionIntermediateAdminService.AnnulDistributionIntermediate
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If

        Try
            Dim distributionIntermediate As CostDistributionIntermediate = Me._distributionIntermediateRepository.GetDistributionIntermediate(code.Trim())
            If distributionIntermediate IsNot Nothing AndAlso distributionIntermediate.Id > 0 Then
                ' Verificar si ya está confirmado
                If Not String.IsNullOrEmpty(distributionIntermediate.ConfirmUser) AndAlso distributionIntermediate.ConfirmDate.HasValue Then
                    Return New ActionResult(Of CostDistributionIntermediate) With {
                        .StateResult = False,
                        .StatusCode = eStatusResult.WARNING,
                        .Message = String.Format("No se puede anular una distribución intermedia confirmada. Fue confirmada por {0} el {1:dd/MM/yyyy HH:mm}", distributionIntermediate.ConfirmUser, distributionIntermediate.ConfirmDate.Value)
                    }
                End If

                ' Verificar si ya está anulado
                If Not String.IsNullOrEmpty(distributionIntermediate.AnnulmentUser) AndAlso distributionIntermediate.AnnulmentDate.HasValue Then
                    Return New ActionResult(Of CostDistributionIntermediate) With {
                        .StateResult = False,
                        .StatusCode = eStatusResult.WARNING,
                        .Message = String.Format("La distribución intermedia ya fue anulada por {0} el {1:dd/MM/yyyy HH:mm}", distributionIntermediate.AnnulmentUser, distributionIntermediate.AnnulmentDate.Value)
                    }
                End If

                ' Iniciar tracking si no está activo
                If Not distributionIntermediate.ChangeTracker.ChangeTrackingEnabled Then
                    distributionIntermediate.StartTracking()
                End If

                distributionIntermediate.AnnulmentUser = audit.CodeUser
                distributionIntermediate.AnnulmentDate = DateTime.Now
                distributionIntermediate.Status = 3 ' Estado: 3 - Anulado

                System.Diagnostics.Debug.WriteLine($"[DEBUG Annul] Status asignado: {distributionIntermediate.Status}, ChangeTracker.State: {distributionIntermediate.ChangeTracker.State}")
            End If
            Return Me.SaveDistributionIntermediate(distributionIntermediate, audit)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CostDistributionIntermediate) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Calcula la distribución intermedia basándose en las bases de distribución configuradas
    ''' PBI #26830
    ''' </summary>
    Public Function CalculateDistributionIntermediate(costIntermediateDistributionId As Integer, year As Integer, month As Integer) As ActionResult(Of List(Of CostDistributionIntermediateDetail)) Implements ICostDistributionIntermediateAdminService.CalculateDistributionIntermediate
        If costIntermediateDistributionId <= 0 Then
            Throw New ArgumentNullException("costIntermediateDistributionId debe ser mayor que 0")
        End If
        If year <= 0 Then
            Throw New ArgumentNullException("year debe ser mayor que 0")
        End If
        If month < 1 OrElse month > 12 Then
            Throw New ArgumentNullException("month debe estar entre 1 y 12")
        End If

        Try
            ' Llamar al repositorio para ejecutar la función SQL
            Dim spResults = _distributionIntermediateRepository.CalculateDistributionIntermediate(costIntermediateDistributionId, year, month)

            If spResults Is Nothing OrElse spResults.Count = 0 Then
                Return New ActionResult(Of List(Of CostDistributionIntermediateDetail)) With {
                    .StateResult = False,
                    .StatusCode = eStatusResult.WARNING,
                    .Message = "No se encontraron datos para calcular la distribución intermedia"
                }
            End If

            ' Mapear resultados de SP a CostDistributionIntermediateDetail
            Dim details As New List(Of CostDistributionIntermediateDetail)()
            Dim tempId As Integer = -1

            For Each spResult In spResults
                If Not spResult.ProductionCenterId.HasValue Then
                    Continue For
                End If

                Dim detail As New CostDistributionIntermediateDetail()
                detail.Id = tempId
                detail.DistributionIntermediateId = costIntermediateDistributionId
                detail.ProductionCenterId = spResult.ProductionCenterId.Value
                detail.Proportion = If(spResult.Percentage.HasValue, spResult.Percentage.Value, 0)
                detail.Value = If(spResult.Value.HasValue, spResult.Value.Value, 0)
                detail.ChangeTracker.ChangeTrackingEnabled = True
                detail.ChangeTracker.State = ObjectState.Added
                details.Add(detail)
                tempId -= 1
            Next

            If details.Count = 0 Then
                Return New ActionResult(Of List(Of CostDistributionIntermediateDetail)) With {
                    .StateResult = False,
                    .StatusCode = eStatusResult.WARNING,
                    .Message = "Los registros retornados no tienen ProductionCenterId válido"
                }
            End If

            Return New ActionResult(Of List(Of CostDistributionIntermediateDetail)) With {
                .StateResult = True,
                .StatusCode = eStatusResult.SUCCESS,
                .ObjectEmbbeded = details
            }

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of CostDistributionIntermediateDetail)) With {
                .StateResult = False,
                .StatusCode = eStatusResult.EXCEPTION,
                .Message = IndigoManagementExceptions.GetExceptionDetails(ex)
            }
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _costService.Dispose()
            End If
            _distributionIntermediateRepository = Nothing
            _costService = Nothing
            _sequenceDRepository = Nothing
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