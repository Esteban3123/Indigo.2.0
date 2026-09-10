'***********************************************************************
' Assembly         : Application.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 18/12/2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports System.Data.Entity.Infrastructure
Imports System.Transactions
Imports Application.Base
Imports Application.MixingStation
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Resources
Imports System.Text
Imports System.Data.SqlClient


#End Region
Public Class ContractExternalClientsAdminService
    Implements IContractExternalClientsAdminService, Inject

#Region "Properties"

    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _contractExternalClientsRepository As IContractExternalClientsRepository

    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDetailRepository As IMixingStationSequenceDetailRepository

#End Region

#Region "Methods"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(contractExternalClientsRepository As IContractExternalClientsRepository, ByVal secuenseDetailRepository As IMixingStationSequenceDetailRepository)
        If contractExternalClientsRepository Is Nothing Then
            Throw New ArgumentNullException("contractExternalClientsRepository Vacio")
        End If
        If secuenseDetailRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDetailRepository")
        End If
        _contractExternalClientsRepository = contractExternalClientsRepository
        _secuenseDetailRepository = secuenseDetailRepository
    End Sub

    Public Function SaveContractExternalClients(ContractExternalClients As ContractExternalClients, audit As AuditMessage, operatingUnitId As Integer, Optional idSequense As Long = 0) As ActionResult(Of ContractExternalClients) Implements IContractExternalClientsAdminService.SaveContractExternalClients
        If ContractExternalClients Is Nothing Then
            Throw New ArgumentNullException("ContractExternalClients")
        End If
        Dim unitOfWork As IUnitWork = Me._contractExternalClientsRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDetailRepository.UnitWork
        Try

            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})

                Dim seq As MixingStationSequenceDetail = Nothing
                If ContractExternalClients.Code Is Nothing OrElse ContractExternalClients.Code.Trim().Equals(String.Empty) Then
                    seq = Me._secuenseDetailRepository.GetSequenceDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.MixingStationSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            ContractExternalClients.Code = res
                            seq.Next += 1
                            Me._secuenseDetailRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of ContractExternalClients) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = ResourceManager.GetString("SequenceNotFound")}
                        End If
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of ContractExternalClients) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = ResourceManager.GetString("SequenceNotFound")}
                    End If
                End If

                Dim auxContractExternalClients As ContractExternalClients = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of ContractExternalClients)
                Dim status As Integer

                If ContractExternalClients.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    ContractExternalClients.CreationUser = audit.CodeUser
                    ContractExternalClients.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    auxContractExternalClients = ContractExternalClients.OriginalValue
                    ContractExternalClients.ModificationUser = audit.CodeUser
                    ContractExternalClients.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._contractExternalClientsRepository.SaveEntity(ContractExternalClients)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of ContractExternalClients)(ContractExternalClients, audit, status, auxContractExternalClients)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                ContractExternalClients.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of ContractExternalClients) With {.StateResult = True, .ObjectEmbbeded = ContractExternalClients}
            End Using

        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of ContractExternalClients) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ContractExternalClients) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    Public Function DeleteContractExternalClients(ContractExternalClients As ContractExternalClients, audit As AuditMessage, TransactionalContainer As String) As ActionResult Implements IContractExternalClientsAdminService.DeleteContractExternalClients
        If ContractExternalClients Is Nothing Then
            Throw New ArgumentNullException("ContractExternalClients")
        End If
        Dim unitOfWork As IUnitWork = Me._contractExternalClientsRepository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of ContractExternalClients)
            auditProcess = New IndigoAuditSimpleEntity(Of ContractExternalClients)(ContractExternalClients, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)

            While ContractExternalClients.ContractExternalClientsDefinitionRate.Count > 0
                ContractExternalClients.ContractExternalClientsDefinitionRate(ContractExternalClients.ContractExternalClientsDefinitionRate.Count - 1).MarkAsDeleted()
            End While

            While ContractExternalClients.ContractExternalClientsDetail.Count > 0
                ContractExternalClients.ContractExternalClientsDetail(ContractExternalClients.ContractExternalClientsDetail.Count - 1).MarkAsDeleted()
            End While

            ContractExternalClients.MarkAsDeleted()

            Me._contractExternalClientsRepository.SaveEntity(ContractExternalClients)
            unitOfWork.Commit()
            auditProcess.Execute()
            Return New ActionResult With {.StateResult = True}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    Public Function GetContractExternalClients(code As String, audit As AuditMessage) As ActionResult(Of ContractExternalClients) Implements IContractExternalClientsAdminService.GetContractExternalClients
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim ContractExternalClients As ContractExternalClients = Me._contractExternalClientsRepository.GetContractExternalClients(code)
            If ContractExternalClients IsNot Nothing AndAlso ContractExternalClients.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of ContractExternalClients)(ContractExternalClients, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of ContractExternalClients) With {.StateResult = True, .ObjectEmbbeded = ContractExternalClients}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ContractExternalClients) With {.StateResult = False, .MessageResult = {ex.Message}.ToList, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function GetContractExternalClientsById(id As Integer) As ActionResult(Of ContractExternalClients) Implements IContractExternalClientsAdminService.GetContractExternalClientsById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Try
            Dim ContractExternalClients As ContractExternalClients = Me._contractExternalClientsRepository.GetContractExternalClientsById(id)
            Return New ActionResult(Of ContractExternalClients) With {.StateResult = True, .ObjectEmbbeded = ContractExternalClients}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ContractExternalClients) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function ChangeStateContractExternalClients(code As String, state As Boolean, audit As AuditMessage, operatingUnitId As Integer) As ActionResult(Of ContractExternalClients) Implements IContractExternalClientsAdminService.ChangeStateContractExternalClients
        Dim ContractExternalClients As ContractExternalClients = _contractExternalClientsRepository.GetContractExternalClients(code)
        ContractExternalClients.Status = state
        Return SaveContractExternalClients(ContractExternalClients, audit, operatingUnitId)
    End Function

    Public Function ImportExceptionsRawMaterial(data As List(Of List(Of String))) As ActionResult(Of List(Of ContractExternalClientsDetail)) Implements IContractExternalClientsAdminService.ImportExceptionsRawMaterial
        If data Is Nothing OrElse data.Count = 0 Then
            Throw New ArgumentNullException("Data")
        End If

        'Listado de errores
        Dim listErrors As New List(Of String)

        'Listado que se devuelve para pegar a la rejilla del form
        Dim ListContractExternalClientsDetail As New List(Of ContractExternalClientsDetail)

        Try
            Dim xmlObject = ConvertBillsToXml(data)

            'Se consume el procedimiento almacenado
            Dim resultStore = _contractExternalClientsRepository.SP_ImportExceptionsRawMaterial(xmlObject)

            'Se crean los objetos para devolver y pegar en la rejilla
            If resultStore IsNot Nothing AndAlso resultStore.Count > 0 Then
                For Each itemXml In resultStore
                    If itemXml.StatusField = 1 Then 'Si el estado del item es True y pasó todas las validaciones
                        'Se crea el nuevo objeto para agregarlo al listado
                        Dim ContractExternalClientsDetail As ContractExternalClientsDetail

                        If itemXml.TypeDetail = 1 Then 'Medicamntos
                            ContractExternalClientsDetail = New ContractExternalClientsDetail() With
                        {
                            .Id = itemXml.Id,
                            .Type = itemXml.TypeDetail,
                            .TypeName = "Medicamento",
                            .SourceCodeName = itemXml.ItemName,
                            .SuppliedBy = itemXml.SupplieBy,
                            .SuppliedByName = If(itemXml.SupplieBy = 1, "Cliente", "Cliente y Central Mezclas"),
                            .AtcId = itemXml.ItemId
                        }

                        ElseIf itemXml.TypeDetail = 2 Then 'Insumo
                            ContractExternalClientsDetail = New ContractExternalClientsDetail() With
                        {
                            .Id = itemXml.Id,
                            .Type = itemXml.TypeDetail,
                            .TypeName = "Insumo",
                            .SourceCodeName = itemXml.ItemName,
                            .SuppliedBy = itemXml.SupplieBy,
                            .SuppliedByName = If(itemXml.SupplieBy = 1, "Cliente", "Cliente y Central Mezclas"),
                            .SupplieId = itemXml.ItemId
                        }
                        ElseIf itemXml.TypeDetail = 3 Then 'Producto
                            ContractExternalClientsDetail = New ContractExternalClientsDetail() With
                        {
                            .Id = itemXml.Id,
                            .Type = itemXml.TypeDetail,
                            .TypeName = "Producto",
                            .SourceCodeName = itemXml.ItemName,
                            .SuppliedBy = itemXml.SupplieBy,
                            .SuppliedByName = If(itemXml.SupplieBy = 1, "Cliente", "Cliente y Central Mezclas"),
                            .ProductId = itemXml.ItemId
                        }
                        ElseIf itemXml.TypeDetail = 4 Then 'Servicio
                            ContractExternalClientsDetail = New ContractExternalClientsDetail() With
                        {
                            .Id = itemXml.Id,
                            .Type = itemXml.TypeDetail,
                            .TypeName = "Servicio",
                            .SourceCodeName = itemXml.ItemName,
                            .SuppliedBy = itemXml.SupplieBy,
                            .SuppliedByName = If(itemXml.SupplieBy = 1, "Cliente", "Cliente y Central Mezclas"),
                            .CUPSEntityId = itemXml.ItemId
                        }
                        End If

                        ListContractExternalClientsDetail.Add(ContractExternalClientsDetail)
                        'End If

                    Else 'Si el estado del item es False y no pasó alguna validación
                        listErrors.Add(itemXml.MessageField)
                    End If
                Next
            End If

            Return New ActionResult(Of List(Of ContractExternalClientsDetail)) With {.StateResult = True, .ObjectEmbbeded = ListContractExternalClientsDetail, .MessageResult = listErrors}
        Catch ex As SqlException
            Return New ActionResult(Of List(Of ContractExternalClientsDetail)) With {.StateResult = False, .MessageResult = {Utils.GetInnerExceptionMessageToString(ex)}.ToList()}
        Catch ex As Exception
            Return New ActionResult(Of List(Of ContractExternalClientsDetail)) With {.StateResult = False, .MessageResult = {Utils.GetInnerExceptionMessageToString(ex)}.ToList()}
        End Try
    End Function

    Private Function ConvertBillsToXml(data As List(Of List(Of String)))
        Dim builder As StringBuilder = New StringBuilder()
        builder.Append("<Data>")

        Dim position As Integer = 0
        For Each item In data
            position = position + 1

            If item.Count <> 3 Then
                builder.Append("<Row>")

                builder.Append("<StatusField>" & 0 & "</StatusField>")
                builder.Append("<MessageField>" & String.Format("El registro {0} no tiene la estructura requerida", position) & "</MessageField>")

                builder.Append("</Row>")
                Continue For
            End If

            builder.Append("<Row>")

            builder.Append("<CountFields>" & item.Count & "</CountFields>")
            builder.Append("<StatusField>" & 1 & "</StatusField>")
            builder.Append("<MessageField>" & "Ok" & "</MessageField>")
            builder.Append("<TypeDetail>" & item(0) & "</TypeDetail>")
            builder.Append("<Codigo>" & item(1) & "</Codigo>")
            builder.Append("<SupplieBy>" & item(2) & "</SupplieBy>")

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

            _contractExternalClientsRepository = Nothing
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
