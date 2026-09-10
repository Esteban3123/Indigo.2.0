'***********************************************************************
' Assembly         : Application.Cost
' Author           : Diego Andrés Roldán Lozano
' Created          : 25-06-2025
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Data.Entity.Core
Imports System.Data.Entity.Infrastructure
Imports System.Data.SqlClient
Imports System.Text
Imports System.Transactions
Imports Application.Base
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Domain.Entities.Service
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Resources

Public Class CostIntermediateDistributionAdminService
    Implements ICostIntermediateDistributionAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de distribucion intermedia
    ''' </summary>
    Private _intermediateDistributionRepository As ICostIntermediateDistributionRepository

    ''' <summary>
    ''' repositorio de secuencias numericas
    ''' </summary>
    Private _sequenceDRepository As ICostSequenceDetailRepository

    Private _interopCostService As ICostServices
#End Region

#Region "Methods"

    Public Sub New(intermediateDistributionRepository As ICostIntermediateDistributionRepository, ByVal sequenceDRepository As ICostSequenceDetailRepository, interopCostService As ICostServices)
        _intermediateDistributionRepository = intermediateDistributionRepository
        _sequenceDRepository = sequenceDRepository
        _interopCostService = interopCostService
    End Sub

    ''' <summary>
    ''' Elimina una distribucion intermedia
    ''' </summary>
    Public Function DeleteIntermediateDistribution(intermediateDistribution As CostIntermediateDistribution, audit As AuditMessage) As ActionResult Implements ICostIntermediateDistributionAdminService.DeleteIntermediateDistribution
        If intermediateDistribution Is Nothing Then
            Throw New ArgumentNullException("intermediateDistribution")
        End If
        Dim unitOfWork As IUnitWork = Me._intermediateDistributionRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                While intermediateDistribution.CostIntermediateDistributionBase.Any()
                    Dim _distribBase = intermediateDistribution.CostIntermediateDistributionBase.Item(intermediateDistribution.CostIntermediateDistributionBase.Count() - 1)
                    If _distribBase.CostIntermediateDistributionBaseDetail IsNot Nothing AndAlso _distribBase.CostIntermediateDistributionBaseDetail.Count > 0 Then
                        While _distribBase.CostIntermediateDistributionBaseDetail.Count > 0
                            _distribBase.CostIntermediateDistributionBaseDetail.Item(_distribBase.CostIntermediateDistributionBaseDetail.Count() - 1).MarkAsDeleted()
                        End While
                    End If
                    _distribBase.MarkAsDeleted()
                End While

                intermediateDistribution.MarkAsDeleted()
                intermediateDistribution.ModificationUser = audit.CodeUser
                intermediateDistribution.ModificationDate = Date.Now

                Me._intermediateDistributionRepository.SaveEntity(intermediateDistribution)
                unitOfWork.Commit()
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
    ''' Obtiene una distribucion intermedia por codigo
    ''' </summary>
    Public Function GetIntermediateDistribution(code As String, audit As AuditMessage) As ActionResult(Of CostIntermediateDistribution) Implements ICostIntermediateDistributionAdminService.GetIntermediateDistribution
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim intermediateDistribution As CostIntermediateDistribution = Me._intermediateDistributionRepository.GetIntermediateDistribution(code.Trim())
            Return New ActionResult(Of CostIntermediateDistribution) With {.StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = intermediateDistribution}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CostIntermediateDistribution) With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una distribucion intermedia por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">Id</exception>
    Public Function GetIntermediateDistributionById(id As Integer) As CostIntermediateDistribution Implements ICostIntermediateDistributionAdminService.GetIntermediateDistributionById
        If id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Try
            Return Me._intermediateDistributionRepository.FindById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guarda una distribucion intermedia
    ''' </summary>
    Public Function SaveIntermediateDistribution(intermediateDistribution As CostIntermediateDistribution, audit As AuditMessage, Optional idSequence As Long = 0) As ActionResult(Of CostIntermediateDistribution) Implements ICostIntermediateDistributionAdminService.SaveIntermediateDistribution
        If intermediateDistribution Is Nothing Then
            Throw New ArgumentNullException("distributionSecondary")
        End If
        Dim unitOfWork As IUnitWork = Me._intermediateDistributionRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty
                If String.IsNullOrEmpty(intermediateDistribution.Code) Then
                    Dim seq As CostSecuenceDetail = _sequenceDRepository.GetSequenseDById(idSequence)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.CostSecuence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            intermediateDistribution.Code = res
                            seq.Next += 1
                            Me._sequenceDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of CostIntermediateDistribution) With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("SequenceNotFound")}
                        End If
                        MessageResult = If(seq.CostSecuence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), intermediateDistribution.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of CostIntermediateDistribution) With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("SequenceNotFound")}
                    End If
                End If

                Dim auxDistributionSecondary As CostIntermediateDistribution = Nothing
                Dim status As Integer
                If intermediateDistribution.ChangeTracker.State = ObjectState.Added Then
                    If String.IsNullOrEmpty(MessageResult) Then
                        MessageResult = String.Format(ResourceManager.GetString("SavedWithCode"), intermediateDistribution.Code)
                    End If
                    intermediateDistribution.CreationDate = Date.Now
                    intermediateDistribution.CreationUser = audit.CodeUser
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    intermediateDistribution.ModificationDate = Date.Now
                    intermediateDistribution.ModificationUser = audit.CodeUser
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                    auxDistributionSecondary = intermediateDistribution.OriginalValue
                End If

                Me._intermediateDistributionRepository.SaveEntity(intermediateDistribution)
                unitOfWork.Commit()
                Dim auditProcess As New IndigoAuditSimpleEntity(Of CostIntermediateDistribution)(intermediateDistribution, audit, status, auxDistributionSecondary)
                auditProcess.Execute()
                scope.Complete()
                Return New ActionResult(Of CostIntermediateDistribution) With {.StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = intermediateDistribution, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of CostIntermediateDistribution) With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CostIntermediateDistribution) With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Actualiza una distribucion intermedia
    ''' </summary>
    Public Function UpdateStateIntermediateDistribution(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of CostIntermediateDistribution) Implements ICostIntermediateDistributionAdminService.UpdateStateIntermediateDistribution
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
            Dim intermediateDistribution As CostIntermediateDistribution = Me._intermediateDistributionRepository.FirstOrDefault(Function(m) m.Code.Equals(code.Trim()))
            If intermediateDistribution IsNot Nothing AndAlso intermediateDistribution.Id > 0 Then
                intermediateDistribution.Status = state
            End If
            Return Me.SaveIntermediateDistribution(intermediateDistribution, audit)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CostIntermediateDistribution) With {.StatusCode = eStatusResult.EXCEPTION, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    ''' <summary>
    ''' Copiar pegar
    ''' </summary>
    ''' <param name="data"></param>
    ''' <param name="intermediateDistributionId"></param>
    ''' <param name="InitialDistribution"></param>
    ''' <returns></returns>
    Public Function SP_CopyPasteCostIntermediateDistribution(data As List(Of List(Of String)), intermediateDistributionId As Integer, initialDistribution As Decimal) As ActionResult(Of List(Of CostIntermediateDistributionBaseDetail), List(Of Tuple(Of String, Integer))) Implements ICostIntermediateDistributionAdminService.SP_CopyPasteCostIntermediateDistribution
        'If data Is Nothing OrElse data.Count = 0 Then
        '    Throw New ArgumentNullException("data")
        'End If
        ''Listado que se devuelve para pegar a la rejilla del form
        'Dim ListDirectDistributionSecondaryDetail As New List(Of CostDirectDistributionSecondaryDetail)
        ''Listado de errores
        'Dim listErrors As New List(Of Tuple(Of String, Integer))
        'Try
        '    'Objeto xml
        '    Dim xmlObject = ConvertToXml(data)
        '    'Se consume el procedimiento almacenado
        '    Dim resultStore = _intermediateDistributionRepository.SP_CopyAndPasteCostIntermediateDistribution(xmlObject, intermediateDistributionId)

        '    'Se crean los objetos para devolver y pegar en la rejilla
        '    If resultStore IsNot Nothing AndAlso resultStore.Count > 0 Then
        '        For Each itemXml In resultStore
        '            If itemXml.StatusField = 1 Then 'Si el estado del item es True y pasó todas las validaciones
        '                If ListDirectDistributionSecondaryDetail IsNot Nothing AndAlso ListDirectDistributionSecondaryDetail.Count > 0 Then
        '                    Dim itemAddeed = ListDirectDistributionSecondaryDetail.Find(Function(x) x.ProductionCenterId = itemXml.ProductionCenterId And x.MeasurementUnitId = itemXml.MeasurementUnitId)
        '                    If itemAddeed IsNot Nothing Then
        '                        Continue For
        '                    End If
        '                End If
        '                Dim DirectDistributionSecondaryDetail As New CostDirectDistributionSecondaryDetail
        '                With DirectDistributionSecondaryDetail
        '                    .ProductionCenterId = itemXml.ProductionCenterId
        '                    .CodeNameProductionCenter = itemXml.ProductionCenterDescription
        '                    .MeasurementUnitId = itemXml.MeasurementUnitId
        '                    .CodeNameMeasureUnit = itemXml.MeasurementUnitDescription
        '                    .Value = itemXml.Value
        '                    .Percentage = (.Value * 100) / initialDistribution
        '                End With
        '                ListDirectDistributionSecondaryDetail.Add(DirectDistributionSecondaryDetail)
        '            Else 'Si el estado del item es False y no pasó alguna validación
        '                listErrors.Add(New Tuple(Of String, Integer)(itemXml.MessageField, 2))
        '            End If
        '        Next
        '    End If

        '    'Se devuelve el mensaje
        '    Return New ActionResult(Of List(Of CostDirectDistributionSecondaryDetail), List(Of Tuple(Of String, Integer))) With {.ObjectEmbbeded = ListDirectDistributionSecondaryDetail, .ObjectEmbbededAux = listErrors, .StateResult = True}
        'Catch ex As SqlException
        '    If ex.ErrorCode = -2146232060 Then
        '        Return New ActionResult(Of List(Of CostDirectDistributionSecondaryDetail), List(Of Tuple(Of String, Integer))) With {.StateResult = False, .Message = "Los valores contienen decimales con un formato no valido, por favor corrija para poder continuar"}
        '    Else
        '        Return New ActionResult(Of List(Of CostDirectDistributionSecondaryDetail), List(Of Tuple(Of String, Integer))) With {.StateResult = False, .Message = ex.ToString}
        '    End If
        'Catch ex As Exception
        '    Return New ActionResult(Of List(Of CostDirectDistributionSecondaryDetail), List(Of Tuple(Of String, Integer))) With {.StateResult = False, .Message = ex.ToString}
        'End Try
    End Function

    Public Function ImportDetailsToCostIntermediateDistributionBase(DistributionType As Byte, MeasurementUnit As Byte, listIntermediateDistributionBaseDetail As List(Of CostIntermediateDistributionBaseDetail), Data As List(Of List(Of String))) As ActionResult(Of List(Of CostIntermediateDistributionBaseDetail)) Implements ICostIntermediateDistributionAdminService.ImportDetailsToCostIntermediateDistributionBase
        If Data Is Nothing OrElse Data.Count = 0 Then
            Throw New ArgumentNullException("Data")
        End If

        'Listado de errores
        Dim listErrors As New List(Of String)

        'Listado que se devuelve para pegar a la rejilla del form
        Dim ListImportData As New List(Of CostIntermediateDistributionBaseDetail)

        Try
            Dim xmlListIntermediateDistributionBaseDetail = ConvertListIntermediateDistributionBaseDetailToXml(listIntermediateDistributionBaseDetail)
            Dim xmlData = ConvertDataToXml(DistributionType, MeasurementUnit, Data)

            'Se consume el procedimiento almacenado
            Dim resultStore = _intermediateDistributionRepository.SP_ImportDetailsToCostIntermediateDistributionBase(DistributionType, MeasurementUnit, xmlListIntermediateDistributionBaseDetail, xmlData)

            'Se crean los objetos para devolver y pegar en la rejilla
            If resultStore IsNot Nothing AndAlso resultStore.Count > 0 Then
                For Each itemXml In resultStore
                    If itemXml.StatusField Then 'Si el estado del item es True y pasó todas las validaciones
                        'Se crea el nuevo objeto para agregarlo al listado
                        Dim costIntermediateDistributionBaseDetail As New CostIntermediateDistributionBaseDetail() With
                        {
                            .ProductionCenterId = itemXml.ProductionCenterId,
                            .Quantity = itemXml.Quantity
                        }

                        ListImportData.Add(costIntermediateDistributionBaseDetail)
                    Else 'Si el estado del item es False y no pasó alguna validación
                        listErrors.Add(itemXml.MessageField)
                    End If
                Next
            End If

            Return New ActionResult(Of List(Of CostIntermediateDistributionBaseDetail)) With {.StateResult = True, .ObjectEmbbeded = ListImportData, .MessageResult = listErrors}
        Catch ex As SqlException
            If ex.ErrorCode = -2146232060 Then
                Return New ActionResult(Of List(Of CostIntermediateDistributionBaseDetail)) With {.StateResult = False, .MessageResult = {"Los valores contienen decimales con un formato no valido, por favor corrija para poder continuar"}.ToList()}
            Else
                Return New ActionResult(Of List(Of CostIntermediateDistributionBaseDetail)) With {.StateResult = False, .MessageResult = {Utils.GetInnerExceptionMessageToString(ex)}.ToList()}
            End If
        Catch ex As Exception
            Return New ActionResult(Of List(Of CostIntermediateDistributionBaseDetail)) With {.StateResult = False, .MessageResult = {Utils.GetInnerExceptionMessageToString(ex)}.ToList()}
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

    Private Function ConvertListIntermediateDistributionBaseDetailToXml(ListIntermediateDistributionBaseDetail As List(Of CostIntermediateDistributionBaseDetail))
        Dim builder As StringBuilder = New StringBuilder()
        builder.Append("<ListIntermediateDistributionBaseDetail>")

        If ListIntermediateDistributionBaseDetail IsNot Nothing Then
            For Each intermediateDistributionBaseDetail In ListIntermediateDistributionBaseDetail
                builder.Append("<CostIntermediateDistributionBaseDetail>")
                builder.Append("<ProductionCenterId>" & intermediateDistributionBaseDetail.ProductionCenterId & "</ProductionCenterId>")
                builder.Append("<Quantity>" & intermediateDistributionBaseDetail.Quantity.ToString().Replace(",", ".") & "</Quantity>")
                builder.Append("</CostIntermediateDistributionBaseDetail>")
            Next
        End If

        builder.Append("</ListIntermediateDistributionBaseDetail>")
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
            _intermediateDistributionRepository = Nothing
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
