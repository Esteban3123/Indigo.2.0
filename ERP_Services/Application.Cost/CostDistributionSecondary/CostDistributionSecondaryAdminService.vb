'***********************************************************************
' Assembly         : Application.Cost
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
Imports Domain.Entities.Service
Imports System.Transactions
Imports System.Data.SqlClient
Imports System.Text

Public Class CostDistributionSecondaryAdminService
    Implements ICostDistributionSecondaryAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de distribucion secundaria
    ''' </summary>
    Private _distributionSecondaryRepository As ICostDistributionSecondaryRepository

    ''' <summary>
    ''' repositorio de secuencias numericas
    ''' </summary>
    Private _sequenceDRepository As ICostSequenceDetailRepository
    Private _interopCostService As ICostServices
#End Region

#Region "Methods"

    Public Sub New(ByVal distributionSecondaryRepository As ICostDistributionSecondaryRepository, ByVal sequenceDRepository As ICostSequenceDetailRepository,
                   interopCostService As ICostServices)
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
    Public Function DeleteDistributionSecondary(distributionSecondary As CostDistributionSecondary, audit As AuditMessage) As ActionResult Implements ICostDistributionSecondaryAdminService.DeleteDistributionSecondary
        If distributionSecondary Is Nothing Then
            Throw New ArgumentNullException("distributionSecondary")
        End If
        Dim unitOfWork As IUnitWork = Me._distributionSecondaryRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                While distributionSecondary.CostDistributionSecondaryBase.Count > 0
                    Dim _distribBase As CostDistributionSecondaryBase = distributionSecondary.CostDistributionSecondaryBase.Item(distributionSecondary.CostDistributionSecondaryBase.Count() - 1)
                    If _distribBase.CostDistributionSecondaryBaseDetail IsNot Nothing AndAlso _distribBase.CostDistributionSecondaryBaseDetail.Count > 0 Then
                        While _distribBase.CostDistributionSecondaryBaseDetail.Count > 0
                            _distribBase.CostDistributionSecondaryBaseDetail.Item(_distribBase.CostDistributionSecondaryBaseDetail.Count() - 1).MarkAsDeleted()
                        End While
                    End If
                    If _distribBase.CostDistributionSecondaryMeasurementUnit IsNot Nothing AndAlso _distribBase.CostDistributionSecondaryMeasurementUnit.Count > 0 Then
                        While _distribBase.CostDistributionSecondaryMeasurementUnit.Count > 0
                            _distribBase.CostDistributionSecondaryMeasurementUnit(_distribBase.CostDistributionSecondaryMeasurementUnit.Count - 1).MarkAsDeleted()
                        End While
                    End If
                    _distribBase.MarkAsDeleted()
                End While

                distributionSecondary.MarkAsDeleted()
                distributionSecondary.ModificationUser = audit.CodeUser
                distributionSecondary.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of CostDistributionSecondary)(distributionSecondary, audit, status)

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
    Public Function GetDistributionSecondary(code As String, audit As AuditMessage) As ActionResult(Of CostDistributionSecondary) Implements ICostDistributionSecondaryAdminService.GetDistributionSecondary
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim distributionSecondary As CostDistributionSecondary = Me._distributionSecondaryRepository.GetDistributionSecondary(code.Trim())
            If distributionSecondary IsNot Nothing AndAlso distributionSecondary.Id > 0 Then
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Print
                Dim auditProcess As New IndigoAuditSimpleEntity(Of CostDistributionSecondary)(distributionSecondary, audit, status)
                auditProcess.Execute()
            End If
            Return New ActionResult(Of CostDistributionSecondary) With {.StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = distributionSecondary}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CostDistributionSecondary) With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una distribucion secundaria por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">Id</exception>
    Public Function GetDistributionSecondaryById(id As Integer) As CostDistributionSecondary Implements ICostDistributionSecondaryAdminService.GetDistributionSecondaryById
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
    Public Function SaveDistributionSecondary(distributionSecondary As CostDistributionSecondary, audit As AuditMessage, Optional idSequence As Long = 0) As ActionResult(Of CostDistributionSecondary) Implements ICostDistributionSecondaryAdminService.SaveDistributionSecondary
        If distributionSecondary Is Nothing Then
            Throw New ArgumentNullException("distributionSecondary")
        End If
        Dim unitOfWork As IUnitWork = Me._distributionSecondaryRepository.UnitWork
        Dim sequenceUnitOfWork As IUnitWork = Me._sequenceDRepository.UnitWork
        Try

            'Dim resultValidate As ActionResult = _interopCostService.ValidateDistributionSecondarySave(distributionSecondary)
            'If Not resultValidate.StateResult Then
            '    unitOfWork.RollbackChangesUnitOfWork()
            '    Return New ActionResult(Of CostDistributionSecondary) With {.StatusCode = eStatusResult.WARNING, .Message = resultValidate.Message, .MessageResult = resultValidate.MessageResult}
            'End If
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty
                If String.IsNullOrEmpty(distributionSecondary.Code) Then
                    Dim seq As CostSecuenceDetail = _sequenceDRepository.GetSequenseDById(idSequence)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.CostSecuence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            distributionSecondary.Code = res
                            seq.Next += 1
                            Me._sequenceDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of CostDistributionSecondary) With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("SequenceNotFound")}
                        End If
                        MessageResult = If(seq.CostSecuence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), distributionSecondary.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of CostDistributionSecondary) With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("SequenceNotFound")}
                    End If
                End If

                Dim auxDistributionSecondary As CostDistributionSecondary = Nothing
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
                Dim auditProcess As New IndigoAuditSimpleEntity(Of CostDistributionSecondary)(distributionSecondary, audit, status, auxDistributionSecondary)
                auditProcess.Execute()
                scope.Complete()
                Return New ActionResult(Of CostDistributionSecondary) With {.StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = distributionSecondary, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of CostDistributionSecondary) With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CostDistributionSecondary) With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Actualiza una distribucion secundaria
    ''' </summary>
    Public Function UpdateStateDistributionSecondary(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of CostDistributionSecondary) Implements ICostDistributionSecondaryAdminService.UpdateStateDistributionSecondary
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
            Dim distributionDirectCost As CostDistributionSecondary = Me._distributionSecondaryRepository.GetDistributionSecondary(code.Trim())
            If distributionDirectCost IsNot Nothing AndAlso distributionDirectCost.Id > 0 Then
                distributionDirectCost.Status = state
            End If
            Return Me.SaveDistributionSecondary(distributionDirectCost, audit)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CostDistributionSecondary) With {.StatusCode = eStatusResult.EXCEPTION, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    ''' <summary>
    ''' Lista las distribuciones secundarias por año y mes
    ''' </summary>
    Public Function ListDistributionSecondaryByYearMonth(year As Integer, month As Integer) As List(Of CostDistributionSecondary) Implements ICostDistributionSecondaryAdminService.ListDistributionSecondaryByYearMonth
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
    Public Function ListPeriodWithDataByMaximumPeriod(year As Integer, month As Integer) As List(Of String) Implements ICostDistributionSecondaryAdminService.ListPeriodWithDataByMaximumPeriod
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

    Public Function SP_CopyPasteCostSecondaryDistribution(data As List(Of List(Of String)), DistributionSecondaryId As Integer, InitialDistribution As Decimal) As ActionResult(Of List(Of CostDirectDistributionSecondaryDetail), List(Of Tuple(Of String, Integer))) Implements ICostDistributionSecondaryAdminService.SP_CopyPasteCostSecondaryDistribution
        If data Is Nothing OrElse data.Count = 0 Then
            Throw New ArgumentNullException("data")
        End If
        'Listado que se devuelve para pegar a la rejilla del form
        Dim ListDirectDistributionSecondaryDetail As New List(Of CostDirectDistributionSecondaryDetail)
        'Listado de errores
        Dim listErrors As New List(Of Tuple(Of String, Integer))
        Try
            'Objeto xml
            Dim xmlObject = ConvertToXml(data)
            'Se consume el procedimiento almacenado
            Dim resultStore = _distributionSecondaryRepository.SP_CopyPasteCostSecondaryDistribution(xmlObject, DistributionSecondaryId)

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
                        Dim DirectDistributionSecondaryDetail As New CostDirectDistributionSecondaryDetail
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
            Return New ActionResult(Of List(Of CostDirectDistributionSecondaryDetail), List(Of Tuple(Of String, Integer))) With {.ObjectEmbbeded = ListDirectDistributionSecondaryDetail, .ObjectEmbbededAux = listErrors, .StateResult = True}
        Catch ex As SqlException
            If ex.ErrorCode = -2146232060 Then
                Return New ActionResult(Of List(Of CostDirectDistributionSecondaryDetail), List(Of Tuple(Of String, Integer))) With {.StateResult = False, .Message = "Los valores contienen decimales con un formato no valido, por favor corrija para poder continuar"}
            Else
                Return New ActionResult(Of List(Of CostDirectDistributionSecondaryDetail), List(Of Tuple(Of String, Integer))) With {.StateResult = False, .Message = ex.ToString}
            End If
        Catch ex As Exception
            Return New ActionResult(Of List(Of CostDirectDistributionSecondaryDetail), List(Of Tuple(Of String, Integer))) With {.StateResult = False, .Message = ex.ToString}
        End Try
    End Function

    Public Function ImportDetailsToCostDistributionSecondaryBase(DistributionType As Byte, MeasurementUnit As Byte, ListDistributionSecondaryBaseDetail As List(Of CostDistributionSecondaryBaseDetail), Data As List(Of List(Of String))) As ActionResult(Of List(Of CostDistributionSecondaryBaseDetail)) Implements ICostDistributionSecondaryAdminService.ImportDetailsToCostDistributionSecondaryBase
        If Data Is Nothing OrElse Data.Count = 0 Then
            Throw New ArgumentNullException("Data")
        End If

        'Listado de errores
        Dim listErrors As New List(Of String)

        'Listado que se devuelve para pegar a la rejilla del form
        Dim ListImportData As New List(Of CostDistributionSecondaryBaseDetail)

        Try
            Dim xmlListDistributionSecondaryBaseDetail = ConvertListDistributionSecondaryBaseDetailToXml(ListDistributionSecondaryBaseDetail)
            Dim xmlData = ConvertDataToXml(DistributionType, MeasurementUnit, Data)

            'Se consume el procedimiento almacenado
            Dim resultStore = _distributionSecondaryRepository.SP_ImportDetailsToCostDistributionSecondaryBase(DistributionType, MeasurementUnit, xmlListDistributionSecondaryBaseDetail, xmlData)

            'Se crean los objetos para devolver y pegar en la rejilla
            If resultStore IsNot Nothing AndAlso resultStore.Count > 0 Then
                For Each itemXml In resultStore
                    If itemXml.StatusField Then 'Si el estado del item es True y pasó todas las validaciones
                        'Se crea el nuevo objeto para agregarlo al listado
                        Dim costDistributionSecondaryBaseDetail As New CostDistributionSecondaryBaseDetail() With
                        {
                            .ProductionCenterId = itemXml.ProductionCenterId,
                            .Quantity = itemXml.Quantity
                        }

                        ListImportData.Add(CostDistributionSecondaryBaseDetail)
                    Else 'Si el estado del item es False y no pasó alguna validación
                        listErrors.Add(itemXml.MessageField)
                    End If
                Next
            End If

            Return New ActionResult(Of List(Of CostDistributionSecondaryBaseDetail)) With {.StateResult = True, .ObjectEmbbeded = ListImportData, .MessageResult = listErrors}
        Catch ex As SqlException
            If ex.ErrorCode = -2146232060 Then
                Return New ActionResult(Of List(Of CostDistributionSecondaryBaseDetail)) With {.StateResult = False, .MessageResult = {"Los valores contienen decimales con un formato no valido, por favor corrija para poder continuar"}.ToList()}
            Else
                Return New ActionResult(Of List(Of CostDistributionSecondaryBaseDetail)) With {.StateResult = False, .MessageResult = {Utils.GetInnerExceptionMessageToString(ex)}.ToList()}
            End If
        Catch ex As Exception
            Return New ActionResult(Of List(Of CostDistributionSecondaryBaseDetail)) With {.StateResult = False, .MessageResult = {Utils.GetInnerExceptionMessageToString(ex)}.ToList()}
        End Try
    End Function

#End Region

#Region "Private Methods"

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

    Private Function ConvertListDistributionSecondaryBaseDetailToXml(ListDistributionSecondaryBaseDetail As List(Of CostDistributionSecondaryBaseDetail))
        Dim builder As StringBuilder = New StringBuilder()
        builder.Append("<ListDistributionSecondaryBaseDetail>")

        If ListDistributionSecondaryBaseDetail IsNot Nothing Then
            For Each distributionSecondaryBaseDetail In ListDistributionSecondaryBaseDetail
                builder.Append("<CostDistributionSecondaryBaseDetail>")

                builder.Append("<ProductionCenterId>" & distributionSecondaryBaseDetail.ProductionCenterId & "</ProductionCenterId>")
                builder.Append("<Quantity>" & distributionSecondaryBaseDetail.Quantity.ToString().Replace(",", ".") & "</Quantity>")

                builder.Append("</CostDistributionSecondaryBaseDetail>")
            Next
        End If

        builder.Append("</ListDistributionSecondaryBaseDetail>")
        Return builder.ToString
    End Function

    Private Function ConvertDataToXml(DistributionType As Byte, MeasurementUnit As Byte, data As List(Of List(Of String)))
        Dim builder As StringBuilder = New StringBuilder()
        builder.Append("<Data>")

        Dim validColumns As Integer = IIf(DistributionType = 2, 2, 1)
        Dim position As Integer = 0
        For Each item In data
            position = position + 1

            If item.Count <> validColumns Then
                builder.Append("<Row>")
                builder.Append("<StatusField>" & 0 & "</StatusField>")
                builder.Append("<MessageField>" & String.Format("El registro {0} no tiene la estructura requerida", position) & "</MessageField>")
                builder.Append("</Row>")
                Continue For
            End If

            builder.Append("<Row>")
            builder.Append("<Position>" & position & "</Position>")
            builder.Append("<StatusField>" & 1 & "</StatusField>")
            builder.Append("<MessageField>" & "Ok" & "</MessageField>")
            builder.Append("<ProductionCenterCode>" & item(0) & "</ProductionCenterCode>")
            If item.Count > 1 Then
                builder.Append("<Quantity>" & item(1).ToString().Replace(",", ".") & "</Quantity>")
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