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
Imports System.Transactions
Imports Application.Cost
Imports System.Text
Imports System.Data.SqlClient

Public Class CostGeneralExpenseAdminService
    Implements ICostGeneralExpenseAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de gastos generales
    ''' </summary>
    Private _generalExpenseRepository As ICostGeneralExpensesRepository

    ''' <summary>
    ''' repositorio de secuencias numericas
    ''' </summary>
    Private _sequenceDRepository As ICostSequenceDetailRepository

#End Region

#Region "Methods"

    Public Sub New(ByVal generalExpenseRepository As ICostGeneralExpensesRepository, ByVal sequenceDRepository As ICostSequenceDetailRepository)
        If generalExpenseRepository Is Nothing Then
            Throw New ArgumentNullException("generalExpenseRepository")
        End If
        If sequenceDRepository Is Nothing Then
            Throw New ArgumentNullException("sequenceDRepository")
        End If
        _generalExpenseRepository = generalExpenseRepository
        _sequenceDRepository = sequenceDRepository
    End Sub

    ''' <summary>
    ''' Elimina un centro de produccion
    ''' </summary>
    Public Function DeleteGeneralExpense(generalExpense As CostGeneralExpense, audit As AuditMessage) As ActionResult Implements ICostGeneralExpenseAdminService.DeleteGeneralExpense
        If generalExpense Is Nothing Then
            Throw New ArgumentNullException("productionCenter")
        End If
        Dim unitOfWork As IUnitWork = Me._generalExpenseRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                While generalExpense.CostDistributionBase.Count > 0
                    Dim _distribBase As CostDistributionBase = generalExpense.CostDistributionBase.Item(generalExpense.CostDistributionBase.Count() - 1)
                    If _distribBase.CostDistributionBaseDetail IsNot Nothing AndAlso _distribBase.CostDistributionBaseDetail.Count > 0 Then
                        While _distribBase.CostDistributionBaseDetail.Count > 0
                            _distribBase.CostDistributionBaseDetail.Item(_distribBase.CostDistributionBaseDetail.Count() - 1).MarkAsDeleted()
                        End While
                    End If
                    If _distribBase.CostDistributionBaseMeasurementUnit IsNot Nothing AndAlso _distribBase.CostDistributionBaseMeasurementUnit.Count > 0 Then
                        While _distribBase.CostDistributionBaseMeasurementUnit.Count > 0
                            _distribBase.CostDistributionBaseMeasurementUnit(_distribBase.CostDistributionBaseMeasurementUnit.Count - 1).MarkAsDeleted()
                        End While
                    End If
                    _distribBase.MarkAsDeleted()
                End While

                generalExpense.MarkAsDeleted()
                generalExpense.ModificationUser = audit.CodeUser
                generalExpense.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of CostGeneralExpense)(generalExpense, audit, status)

                Me._generalExpenseRepository.SaveEntity(generalExpense)
                unitOfWork.Commit()
                auditProcess.Execute()
                scope.Complete()
                Return New ActionResult With {.StatusCode = eStatusResult.SUCCESS, .Message = ResourceManager.GetString("RecordDeleted")}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Guarda un centro de produccion
    ''' </summary>
    Public Function SaveGeneralExpense(generalExpense As CostGeneralExpense, audit As AuditMessage, Optional idSequence As Long = 0) As ActionResult(Of CostGeneralExpense) Implements ICostGeneralExpenseAdminService.SaveGeneralExpense
        If generalExpense Is Nothing Then
            Throw New ArgumentNullException("generalExpense")
        End If
        Dim unitOfWork As IUnitWork = Me._generalExpenseRepository.UnitWork
        Dim sequenceUnitOfWork As IUnitWork = Me._sequenceDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty
                If String.IsNullOrEmpty(generalExpense.Code) Then
                    Dim seq As CostSecuenceDetail = _sequenceDRepository.GetSequenseDById(idSequence)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.CostSecuence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            generalExpense.Code = res
                            seq.Next += 1
                            Me._sequenceDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of CostGeneralExpense) With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("SequenceNotFound")}
                        End If
                        MessageResult = If(seq.CostSecuence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), generalExpense.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of CostGeneralExpense) With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("SequenceNotFound")}
                    End If
                End If

                Dim auxExpenseConcept As CostGeneralExpense = Nothing
                Dim status As Integer
                If generalExpense.ChangeTracker.State = ObjectState.Added Then
                    If String.IsNullOrEmpty(MessageResult) Then
                        MessageResult = String.Format(ResourceManager.GetString("SavedWithCode"), generalExpense.Code)
                    End If
                    generalExpense.CreationDate = Date.Now
                    generalExpense.CreationUser = audit.CodeUser
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    generalExpense.ModificationDate = Date.Now
                    generalExpense.ModificationUser = audit.CodeUser
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                    auxExpenseConcept = generalExpense.OriginalValue
                End If

                Me._generalExpenseRepository.SaveEntity(generalExpense)
                unitOfWork.Commit()
                Dim auditProcess As New IndigoAuditSimpleEntity(Of CostGeneralExpense)(generalExpense, audit, status, auxExpenseConcept)
                auditProcess.Execute()
                scope.Complete()
                Return New ActionResult(Of CostGeneralExpense) With {.StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = generalExpense, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of CostGeneralExpense) With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CostGeneralExpense) With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un gasto general por id
    ''' </summary>
    Public Function GetGeneralExpenseById(id As Integer) As CostGeneralExpense Implements ICostGeneralExpenseAdminService.GetGeneralExpenseById
        If id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Try
            Return Me._generalExpenseRepository.GetGeneralExpenseById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Lista los gastos generales por id de cuenta contable
    ''' </summary>
    Public Function GetGeneralExpenseByMainAccountId(MainAccountId As Integer) As CostGeneralExpense Implements ICostGeneralExpenseAdminService.GetGeneralExpenseByMainAccountId
        If MainAccountId = 0 Then
            Throw New ArgumentNullException("MainAccountId")
        End If
        Try
            Return Me._generalExpenseRepository.GetGeneralExpenseByMainAccountId(MainAccountId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un gasto general
    ''' </summary>
    Public Function GetGeneralExpense(code As String, audit As AuditMessage) As ActionResult(Of CostGeneralExpense) Implements ICostGeneralExpenseAdminService.GetGeneralExpense
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim generalExpense As CostGeneralExpense = Me._generalExpenseRepository.GetGeneralExpense(code.Trim())
            If generalExpense IsNot Nothing AndAlso generalExpense.Id > 0 Then
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Print
                Dim auditProcess As New IndigoAuditSimpleEntity(Of CostGeneralExpense)(generalExpense, audit, status)
                auditProcess.Execute()
            End If
            Return New ActionResult(Of CostGeneralExpense) With {.StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = generalExpense}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CostGeneralExpense) With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Actualiza el estado
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateStateCostGeneralExpense(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of CostGeneralExpense) Implements ICostGeneralExpenseAdminService.UpdateStateCostGeneralExpense
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
            Dim generalExpense = Me._generalExpenseRepository.GetGeneralExpense(code.Trim())
            If generalExpense IsNot Nothing AndAlso generalExpense.Id > 0 Then
                generalExpense.Status = state
            End If
            Return Me.SaveGeneralExpense(generalExpense, audit)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CostGeneralExpense) With {.StateResult = False}
        End Try
    End Function

    Public Function ImportDetailsToCostDistributionBase(DistributionType As Byte, MeasurementUnit As Byte, ListDistributionBaseDetail As List(Of CostDistributionBaseDetail), Data As List(Of List(Of String))) As ActionResult(Of List(Of CostDistributionBaseDetail)) Implements ICostGeneralExpenseAdminService.ImportDetailsToCostDistributionBase
        If Data Is Nothing OrElse Data.Count = 0 Then
            Throw New ArgumentNullException("Data")
        End If

        'Listado de errores
        Dim listErrors As New List(Of String)

        'Listado que se devuelve para pegar a la rejilla del form
        Dim ListImportData As New List(Of CostDistributionBaseDetail)

        Try
            Dim xmlListDistributionBaseDetail = ConvertListDistributionBaseDetailToXml(ListDistributionBaseDetail)
            Dim xmlData = ConvertDataToXml(DistributionType, MeasurementUnit, Data)

            'Se consume el procedimiento almacenado
            Dim resultStore = _generalExpenseRepository.SP_ImportDetailsToCostDistributionBase(DistributionType, MeasurementUnit, xmlListDistributionBaseDetail, xmlData)

            'Se crean los objetos para devolver y pegar en la rejilla
            If resultStore IsNot Nothing AndAlso resultStore.Count > 0 Then
                For Each itemXml In resultStore
                    If itemXml.StatusField Then 'Si el estado del item es True y pasó todas las validaciones
                        'Se crea el nuevo objeto para agregarlo al listado
                        Dim costDistributionBaseDetail As New CostDistributionBaseDetail() With
                        {
                            .ProductionCenterId = itemXml.ProductionCenterId,
                            .MainAccountId = itemXml.MainAccountId,
                            .CodeNameMainAccount = itemXml.MainAccountNumberName,
                            .CostCenterId = itemXml.CostCenterId,
                            .CodeNameCostCenter = itemXml.CostCenterCodeName,
                            .Quantity = itemXml.Quantity
                        }

                        ListImportData.Add(costDistributionBaseDetail)
                    Else 'Si el estado del item es False y no pasó alguna validación
                        listErrors.Add(itemXml.MessageField)
                    End If
                Next
            End If

            Return New ActionResult(Of List(Of CostDistributionBaseDetail)) With {.StateResult = True, .ObjectEmbbeded = ListImportData, .MessageResult = listErrors}
        Catch ex As SqlException
            If ex.ErrorCode = -2146232060 Then
                Return New ActionResult(Of List(Of CostDistributionBaseDetail)) With {.StateResult = False, .MessageResult = {"Los valores contienen decimales con un formato no valido, por favor corrija para poder continuar"}.ToList()}
            Else
                Return New ActionResult(Of List(Of CostDistributionBaseDetail)) With {.StateResult = False, .MessageResult = {Utils.GetInnerExceptionMessageToString(ex)}.ToList()}
            End If
        Catch ex As Exception
            Return New ActionResult(Of List(Of CostDistributionBaseDetail)) With {.StateResult = False, .MessageResult = {Utils.GetInnerExceptionMessageToString(ex)}.ToList()}
        End Try
    End Function

#End Region

#Region "Private Methods"

    Private Function ConvertListDistributionBaseDetailToXml(ListDistributionBaseDetail As List(Of CostDistributionBaseDetail))
        Dim builder As StringBuilder = New StringBuilder()
        builder.Append("<ListDistributionBaseDetail>")

        If ListDistributionBaseDetail IsNot Nothing Then
            For Each distributionBaseDetail In ListDistributionBaseDetail
                builder.Append("<CostDistributionBaseDetail>")

                builder.Append("<ProductionCenterId>" & distributionBaseDetail.ProductionCenterId & "</ProductionCenterId>")
                builder.Append("<MainAccountId>" & distributionBaseDetail.MainAccountId & "</MainAccountId>")
                builder.Append("<CostCenterId>" & distributionBaseDetail.CostCenterId & "</CostCenterId>")
                builder.Append("<Quantity>" & distributionBaseDetail.Quantity.ToString().Replace(",", ".") & "</Quantity>")

                builder.Append("</CostDistributionBaseDetail>")
            Next
        End If

        builder.Append("</ListDistributionBaseDetail>")
        Return builder.ToString
    End Function

    Private Function ConvertDataToXml(DistributionType As Byte, MeasurementUnit As Byte, data As List(Of List(Of String)))
        Dim builder As StringBuilder = New StringBuilder()
        builder.Append("<Data>")

        Dim validColumns As Integer = IIf(DistributionType = 2, 4, 3)
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
            builder.Append("<MainAccountNumber>" & item(1) & "</MainAccountNumber>")
            builder.Append("<CostCenterCode>" & item(2) & "</CostCenterCode>")
            If item.Count > 3 Then
                builder.Append("<Quantity>" & item(3).ToString().Replace(",", ".") & "</Quantity>")
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
            _generalExpenseRepository = Nothing
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