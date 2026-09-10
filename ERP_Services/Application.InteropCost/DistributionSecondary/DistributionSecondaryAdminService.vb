'***********************************************************************
' Assembly         : Application.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 11-12-2014
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
Imports Domain.InteropCost
Imports Domain.InteropCost.Entities
Imports Domain.Entities.Service
Imports System.Transactions
Imports System.Text
Imports System.Data.SqlClient

Public Class DistributionSecondaryAdminService
    Implements IDistributionSecondaryAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de distribucion secundaria
    ''' </summary>
    Private _distributionSecondaryRepository As IDistributionSecondaryRepository

    ''' <summary>
    ''' repositorio de secuencias numericas
    ''' </summary>
    Private _sequenceDRepository As IInteropCostSequenceDetailRepository
    Private _interopCostService As IInteropCostServices
#End Region

#Region "Methods"

    Public Sub New(ByVal distributionSecondaryRepository As IDistributionSecondaryRepository, ByVal sequenceDRepository As IInteropCostSequenceDetailRepository,
                   interopCostService As IInteropCostServices)
        If distributionSecondaryRepository Is Nothing Then
            Throw New ArgumentNullException("distributionSecondaryRepository")
        End If
        If sequenceDRepository Is Nothing Then
            Throw New ArgumentNullException("sequenceDRepository")
        End If
        _distributionSecondaryRepository = distributionSecondaryRepository
        _sequenceDRepository = sequenceDRepository
        _interopCostService = interopCostService
    End Sub

    ''' <summary>
    ''' Elimina una distribucion secundaria
    ''' </summary>
    Public Function DeleteDistributionSecondary(distributionSecondary As DistributionSecondary, audit As AuditMessage) As ActionResult Implements IDistributionSecondaryAdminService.DeleteDistributionSecondary
        If distributionSecondary Is Nothing Then
            Throw New ArgumentNullException("distributionSecondary")
        End If
        Dim unitOfWork As IUnitWork = Me._distributionSecondaryRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                While distributionSecondary.DistributionSecondaryBase.Count > 0
                    Dim _distribBase As DistributionSecondaryBase = distributionSecondary.DistributionSecondaryBase.Item(distributionSecondary.DistributionSecondaryBase.Count() - 1)
                    If _distribBase.DistributionSecondaryBaseDetail IsNot Nothing AndAlso _distribBase.DistributionSecondaryBaseDetail.Count > 0 Then
                        While _distribBase.DistributionSecondaryBaseDetail.Count > 0
                            _distribBase.DistributionSecondaryBaseDetail.Item(_distribBase.DistributionSecondaryBaseDetail.Count() - 1).MarkAsDeleted()
                        End While
                    End If
                    _distribBase.MarkAsDeleted()
                End While

                distributionSecondary.MarkAsDeleted()
                distributionSecondary.ModificationUser = audit.CodeUser
                distributionSecondary.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of DistributionSecondary)(distributionSecondary, audit, status)

                Me._distributionSecondaryRepository.SaveEntity(distributionSecondary)
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
    ''' Obtiene una distribucion secundaria por codigo
    ''' </summary>
    Public Function GetDistributionSecondary(code As String, audit As AuditMessage) As ActionResult(Of DistributionSecondary) Implements IDistributionSecondaryAdminService.GetDistributionSecondary
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim distributionSecondary As DistributionSecondary = Me._distributionSecondaryRepository.GetDistributionSecondary(code.Trim())
            If distributionSecondary IsNot Nothing AndAlso distributionSecondary.Id > 0 Then
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Print
                Dim auditProcess As New IndigoAuditSimpleEntity(Of DistributionSecondary)(distributionSecondary, audit, status)
                auditProcess.Execute()
            End If
            Return New ActionResult(Of DistributionSecondary) With {.StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = distributionSecondary}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of DistributionSecondary) With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una distribucion secundaria por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">Id</exception>
    Public Function GetDistributionSecondaryById(id As Integer) As DistributionSecondary Implements IDistributionSecondaryAdminService.GetDistributionSecondaryById
        If id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Try
            Return Me._distributionSecondaryRepository.GetDistributionSecondaryById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guarda una distribucion secundaria
    ''' </summary>
    Public Function SaveDistributionSecondary(distributionSecondary As DistributionSecondary, audit As AuditMessage, Optional idSequence As Long = 0) As ActionResult(Of DistributionSecondary) Implements IDistributionSecondaryAdminService.SaveDistributionSecondary
        If distributionSecondary Is Nothing Then
            Throw New ArgumentNullException("distributionSecondary")
        End If
        Dim unitOfWork As IUnitWork = Me._distributionSecondaryRepository.UnitWork
        Dim sequenceUnitOfWork As IUnitWork = Me._sequenceDRepository.UnitWork
        Try

            'Dim resultValidate As ActionResult = _interopCostService.ValidateDistributionSecondarySave(distributionSecondary)
            'If Not resultValidate.StateResult Then
            '    unitOfWork.RollbackChangesUnitOfWork()
            '    Return New ActionResult(Of DistributionSecondary) With {.StatusCode = eStatusResult.WARNING, .Message = resultValidate.Message, .MessageResult = resultValidate.MessageResult}
            'End If

            'Se valida que el centro de produccion que se escogio en el form no exista dentro de otro registro
            Dim temp = _distributionSecondaryRepository.GetDistributionSecondaryByProductionCenterId(distributionSecondary.ProductionCenterId, distributionSecondary.Id)
            If temp IsNot Nothing Then
                unitOfWork.RollbackChanges()
                Return New ActionResult(Of DistributionSecondary) With {.StatusCode = eStatusResult.WARNING, .Message = "No se puede guardar porque ya existe un registro(" + temp.Code + " - " + temp.Description + ") con el centro de producción escogido"}
            End If

            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty
                If String.IsNullOrEmpty(distributionSecondary.Code) Then
                    Dim seq As InteropCostSecuenceDetail = _sequenceDRepository.GetSequenseDById(idSequence)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.InteropCostSecuence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            distributionSecondary.Code = res
                            seq.Next += 1
                            Me._sequenceDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of DistributionSecondary) With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("SequenceNotFound")}
                        End If
                        MessageResult = If(seq.InteropCostSecuence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), distributionSecondary.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of DistributionSecondary) With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("SequenceNotFound")}
                    End If
                End If

                Dim auxDistributionSecondary As DistributionSecondary = Nothing
                Dim status As Integer
                If distributionSecondary.ChangeTracker.State = ObjectState.Added Then
                    If String.IsNullOrEmpty(MessageResult) Then
                        MessageResult = String.Format(ResourceManager.GetString("SavedWithCode"), distributionSecondary.Code)
                    End If
                    distributionSecondary.CreationDate = Date.Now
                    distributionSecondary.CreationUser = audit.CodeUser
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    distributionSecondary.ModificationDate = Date.Now
                    distributionSecondary.ModificationUser = audit.CodeUser
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                    auxDistributionSecondary = distributionSecondary.OriginalValue
                End If

                Me._distributionSecondaryRepository.SaveEntity(distributionSecondary)
                unitOfWork.Commit()
                Dim auditProcess As New IndigoAuditSimpleEntity(Of DistributionSecondary)(distributionSecondary, audit, status, auxDistributionSecondary)
                auditProcess.Execute()
                scope.Complete()
                Return New ActionResult(Of DistributionSecondary) With {.StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = distributionSecondary, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of DistributionSecondary) With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of DistributionSecondary) With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Actualiza una distribucion secundaria
    ''' </summary>
    Public Function UpdateStateDistributionSecondary(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of DistributionSecondary) Implements IDistributionSecondaryAdminService.UpdateStateDistributionSecondary
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
            Dim distributionDirectCost As DistributionSecondary = Me._distributionSecondaryRepository.GetDistributionSecondary(code.Trim())
            If distributionDirectCost IsNot Nothing AndAlso distributionDirectCost.Id > 0 Then
                distributionDirectCost.Status = state
            End If
            Return Me.SaveDistributionSecondary(distributionDirectCost, audit)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of DistributionSecondary) With {.StatusCode = eStatusResult.EXCEPTION, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    ''' <summary>
    ''' Lista las distribuciones secundarias por año y mes
    ''' </summary>
    Public Function ListDistributionSecondaryByYearMonth(year As Integer, month As Integer) As List(Of DistributionSecondary) Implements IDistributionSecondaryAdminService.ListDistributionSecondaryByYearMonth
        If year = 0 Then
            Throw New ArgumentNullException("year")
        End If
        If month = 0 Then
            Throw New ArgumentNullException("month")
        End If
        Try
            Return Me._distributionSecondaryRepository.ListDistributionSecondaryByYearMonth(year, month)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Lista los periodos anteriores que contienen datos al año y mes dados
    ''' </summary>
    Public Function ListPeriodWithDataByMaximumPeriod(year As Integer, month As Integer) As List(Of String) Implements IDistributionSecondaryAdminService.ListPeriodWithDataByMaximumPeriod
        If year = 0 Then
            Throw New ArgumentNullException("year")
        End If
        If month = 0 Then
            Throw New ArgumentNullException("month")
        End If
        Try
            Return Me._distributionSecondaryRepository.ListPeriodWithDataByMaximumPeriod(year, month)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function SP_CopyPasteSecondaryDistribution(data As List(Of List(Of String)), DistributionSecondaryId As Integer, InitialDistribution As Decimal) As ActionResult(Of List(Of DirectDistributionSecondaryDetail), List(Of Tuple(Of String, Integer))) Implements IDistributionSecondaryAdminService.SP_CopyPasteSecondaryDistribution
        If data Is Nothing OrElse data.Count = 0 Then
            Throw New ArgumentNullException("data")
        End If
        'Listado que se devuelve para pegar a la rejilla del form
        Dim ListDirectDistributionSecondaryDetail As New List(Of DirectDistributionSecondaryDetail)
        'Listado de errores
        Dim listErrors As New List(Of Tuple(Of String, Integer))
        Try
            'Objeto xml
            Dim xmlObject = ConvertToXml(data)
            'Se consume el procedimiento almacenado
            Dim resultStore = _distributionSecondaryRepository.SP_CopyPasteSecondaryDistribution(xmlObject, DistributionSecondaryId)

            'Se crean los objetos para devolver y pegar en la rejilla
            If resultStore IsNot Nothing AndAlso resultStore.Count > 0 Then
                For Each itemXml In resultStore
                    If itemXml.StatusField = 1 Then 'Si el estado del item es True y pasó todas las validaciones
                        If ListDirectDistributionSecondaryDetail IsNot Nothing AndAlso ListDirectDistributionSecondaryDetail.Count > 0 Then
                            Dim itemAddeed = ListDirectDistributionSecondaryDetail.Find(Function(x) x.ProductionCenterId = itemXml.ProductionCenterId And x.MeasurementUnitId = itemXml.MeasurementUnitId)
                            If itemAddeed IsNot Nothing Then
                                Continue For
                            End If
                        End If
                        Dim DirectDistributionSecondaryDetail As New DirectDistributionSecondaryDetail
                        With DirectDistributionSecondaryDetail
                            .ProductionCenterId = itemXml.ProductionCenterId
                            .CodeNameProductionCenter = itemXml.ProductionCenterDescription
                            .MeasurementUnitId = itemXml.MeasurementUnitId
                            .CodeNameMeasureUnit = itemXml.MeasurementUnitDescription
                            .Value = itemXml.Value
                            .Percentage = (.Value * 100) / InitialDistribution
                        End With
                        ListDirectDistributionSecondaryDetail.Add(DirectDistributionSecondaryDetail)
                    Else 'Si el estado del item es False y no pasó alguna validación
                        listErrors.Add(New Tuple(Of String, Integer)(itemXml.MessageField, 2))
                    End If
                Next
            End If

            'Se devuelve el mensaje
            Return New ActionResult(Of List(Of DirectDistributionSecondaryDetail), List(Of Tuple(Of String, Integer))) With {.ObjectEmbbeded = ListDirectDistributionSecondaryDetail, .ObjectEmbbededAux = listErrors, .StateResult = True}
        Catch ex As SqlException
            If ex.ErrorCode = -2146232060 Then
                Return New ActionResult(Of List(Of DirectDistributionSecondaryDetail), List(Of Tuple(Of String, Integer))) With {.StateResult = False, .Message = "Los valores contienen decimales con un formato no valido, por favor corrija para poder continuar"}
            Else
                Return New ActionResult(Of List(Of DirectDistributionSecondaryDetail), List(Of Tuple(Of String, Integer))) With {.StateResult = False, .Message = ex.ToString}
            End If
        Catch ex As Exception
            Return New ActionResult(Of List(Of DirectDistributionSecondaryDetail), List(Of Tuple(Of String, Integer))) With {.StateResult = False, .Message = ex.ToString}
        End Try
    End Function

    Private Function ConvertToXml(data As List(Of List(Of String)))
        Dim builder As StringBuilder = New StringBuilder()
        builder.Append("<Data>")

        For Each item In data
            builder.Append("<Row>")

            builder.Append("<CountFields>" & item.Count & "</CountFields>")
            builder.Append("<StatusField>" & 0 & "</StatusField>")
            builder.Append("<MessageField>" & "Ok" & "</MessageField>")
            If item.Count = 1 OrElse item.Count > 1 Then
                builder.Append("<ProductionCenterCode>" & item(0) & "</ProductionCenterCode>")
            Else
                builder.Append("<ProductionCenterCode>" & "---" & "</ProductionCenterCode>")
            End If
            builder.Append("<ProductionCenterId>" & 0 & "</ProductionCenterId>")
            builder.Append("<ProductionCenterDescription>" & "---" & "</ProductionCenterDescription>")
            If item.Count = 2 OrElse item.Count > 2 Then
                builder.Append("<MeasurementUnitCode>" & item(1) & "</MeasurementUnitCode>")
            Else
                builder.Append("<MeasurementUnitCode>" & "---" & "</MeasurementUnitCode>")
            End If
            builder.Append("<MeasurementUnitId>" & 0 & "</MeasurementUnitId>")
            builder.Append("<MeasurementUnitDescription>" & "---" & "</MeasurementUnitDescription>")
            If item.Count = 3 OrElse item.Count > 3 Then
                builder.Append("<Quantity>" & item(2) & "</Quantity>")
            Else
                builder.Append("<Quantity>" & "0" & "</Quantity>")
            End If
            If item.Count = 4 OrElse item.Count > 4 Then
                builder.Append("<Value>" & item(3) & "</Value>")
            Else
                builder.Append("<Value>" & "0" & "</Value>")
            End If

            builder.Append("</Row>")
        Next

        builder.Append("</Data>")
        Return builder.ToString
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _interopCostService.Dispose()
            End If
            _distributionSecondaryRepository = Nothing
            _sequenceDRepository = Nothing
            _interopCostService = Nothing
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