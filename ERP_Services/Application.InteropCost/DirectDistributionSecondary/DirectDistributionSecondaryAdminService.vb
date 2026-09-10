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
Imports System.Transactions
Imports System.Text

Public Class DirectDistributionSecondaryAdminService
    Implements IDirectDistributionSecondaryAdminService

    ''' <summary>
    ''' Repositorio de gastos generales
    ''' </summary>
    Private _directDistributionSecondaryRepository As IDirectDistributionSecondaryRepository

    ''' <summary>
    ''' repositorio de secuencias numericas
    ''' </summary>
    Private _sequenceDRepository As IInteropCostSequenceDetailRepository

    Public Sub New(directDistributionSecondaryRepository As IDirectDistributionSecondaryRepository, sequenceDRepository As IInteropCostSequenceDetailRepository)
        _directDistributionSecondaryRepository = directDistributionSecondaryRepository
        _sequenceDRepository = sequenceDRepository
    End Sub

    Public Function GetDirectDistributionSecondary(code As String, ByVal audit As AuditMessage) As DirectDistributionSecondary Implements IDirectDistributionSecondaryAdminService.GetDirectDistributionSecondary
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim DirectDistributionSecondary As DirectDistributionSecondary = Me._directDistributionSecondaryRepository.GetDirectDistributionSecondary(code.Trim())
            If DirectDistributionSecondary IsNot Nothing AndAlso DirectDistributionSecondary.Id > 0 Then
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Print
                Dim auditProcess As New IndigoAuditSimpleEntity(Of DirectDistributionSecondary)(DirectDistributionSecondary, audit, status)
                auditProcess.Execute()
            End If
            Return DirectDistributionSecondary
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New DirectDistributionSecondary
        End Try
    End Function

    Public Function GetDirectDistributionSecondaryById(id As Integer) As DirectDistributionSecondary Implements IDirectDistributionSecondaryAdminService.GetDirectDistributionSecondaryById
        If id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Try
            Return Me._directDistributionSecondaryRepository.GetDirectDistributionSecondaryById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function SaveDirectDistributionSecondary(DirectDistributionSecondary As DirectDistributionSecondary, ListUpdateIds As List(Of Integer), audit As AuditMessage, Optional idSequence As Long = 0) As ActionResult(Of DirectDistributionSecondary) Implements IDirectDistributionSecondaryAdminService.SaveDirectDistributionSecondary
        If DirectDistributionSecondary Is Nothing Then
            Throw New ArgumentNullException("DirectDistributionSecondary")
        End If
        Dim unitOfWork As IUnitWork = Me._directDistributionSecondaryRepository.UnitWork
        Dim sequenceUnitOfWork As IUnitWork = Me._sequenceDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                'Mensaje que se devuelve al usuario
                Dim MessageResult As String = String.Empty

                If DirectDistributionSecondary.Status <> 3 Then 'Si es diferente a anular
                    Dim quantity = _directDistributionSecondaryRepository.GetCountByDistributionSecondaryId(DirectDistributionSecondary.DistributionSecondaryId, DirectDistributionSecondary.Month, DirectDistributionSecondary.Year)
                    If DirectDistributionSecondary.ChangeTracker.State = ObjectState.Added And quantity > 0 Then
                        Throw New Exception("El elemento de distribucion secundaria ya fue creado para este periodo")
                    ElseIf DirectDistributionSecondary.ChangeTracker.State = ObjectState.Modified And quantity > 1 Then
                        Throw New Exception("El elemento de distribucion secundaria ya fue creado para este periodo")
                    End If
                    If String.IsNullOrEmpty(DirectDistributionSecondary.Code) Then
                        Dim seq As InteropCostSecuenceDetail = _sequenceDRepository.GetSequenseDById(idSequence)
                        If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.InteropCostSecuence.Sequential Then
                            Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                            If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                                DirectDistributionSecondary.Code = res
                                seq.Next += 1
                                Me._sequenceDRepository.SaveEntity(seq)
                            Else
                                scope.Dispose()
                                Return New ActionResult(Of DirectDistributionSecondary) With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("SequenceNotFound")}
                            End If
                            MessageResult = If(seq.InteropCostSecuence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), DirectDistributionSecondary.Code), ResourceManager.GetString("SaveMessage"))
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of DirectDistributionSecondary) With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("SequenceNotFound")}
                        End If
                    End If
                End If

                Dim auxExpenseConcept As DirectDistributionSecondary = Nothing
                Dim status As Integer

                If DirectDistributionSecondary.ChangeTracker.State = ObjectState.Added Then
                    If String.IsNullOrEmpty(MessageResult) Then
                        MessageResult = String.Format(ResourceManager.GetString("SavedWithCode"), DirectDistributionSecondary.Code)
                    End If
                    DirectDistributionSecondary.CreationUser = audit.CodeUser
                    DirectDistributionSecondary.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                ElseIf DirectDistributionSecondary.ChangeTracker.State = ObjectState.Modified Then
                    auxExpenseConcept = DirectDistributionSecondary.OriginalValue
                    If DirectDistributionSecondary.Status = 1 Then
                        MessageResult = ResourceManager.GetString("UpdateMessage")
                        DirectDistributionSecondary.ModificationUser = audit.CodeUser
                        DirectDistributionSecondary.ModificationDate = DateTime.Now
                        status = Infrastructure.CrossCutting.Audit.Actions.Update
                    End If
                    If DirectDistributionSecondary.Status = 2 Then
                        MessageResult = ResourceManager.GetString("ConfirmationMessage")
                        DirectDistributionSecondary.ModificationUser = audit.CodeUser
                        DirectDistributionSecondary.ModificationDate = DateTime.Now
                        DirectDistributionSecondary.ConfirmUser = audit.CodeUser
                        DirectDistributionSecondary.ConfirmDate = DateTime.Now
                        status = Infrastructure.CrossCutting.Audit.Actions.Confirm
                    End If
                    If DirectDistributionSecondary.Status = 3 Then
                        MessageResult = ResourceManager.GetString("AnnularCorrect")
                        DirectDistributionSecondary.ModificationUser = audit.CodeUser
                        DirectDistributionSecondary.ModificationDate = DateTime.Now
                        DirectDistributionSecondary.AnnulmentUser = audit.CodeUser
                        DirectDistributionSecondary.AnnulmentDate = DateTime.Now
                        status = Infrastructure.CrossCutting.Audit.Actions.Annular
                    End If
                End If

                'Se actualiza el campo import en la tabla LogisticProductionCenterRecordDetail
                If ListUpdateIds IsNot Nothing AndAlso ListUpdateIds.Count > 0 Then
                    Dim ObjectXml As String = ConvertToXml(ListUpdateIds)
                    Dim resultStore = _directDistributionSecondaryRepository.SP_UpdateFieldImport(ObjectXml, DirectDistributionSecondary.Status)
                    If resultStore.CodeMessage <> 0 Then
                        unitOfWork.RollbackChanges()
                        scope.Dispose()
                        Return New ActionResult(Of DirectDistributionSecondary) With {.StatusCode = eStatusResult.WARNING, .Message = resultStore.Message}
                    End If
                End If

                Me._directDistributionSecondaryRepository.SaveEntity(DirectDistributionSecondary)
                unitOfWork.Commit()
                Dim auditProcess As New IndigoAuditSimpleEntity(Of DirectDistributionSecondary)(DirectDistributionSecondary, audit, status, auxExpenseConcept)
                auditProcess.Execute()
                scope.Complete()
                Return New ActionResult(Of DirectDistributionSecondary) With {.StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = DirectDistributionSecondary, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of DirectDistributionSecondary) With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of DirectDistributionSecondary) With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Private Function ConvertToXml(ListIds As List(Of Integer))
        Dim builder As StringBuilder = New StringBuilder()
        For Each item In ListIds
            builder.Append("<Data>")
            builder.Append("<Id>" & item & "</Id>")
            builder.Append("</Data>")
        Next
        Return builder.ToString
    End Function

    ''' <summary>
    ''' Obtiene el producido de los centros de produccion logisticos
    ''' </summary>
    ''' <param name="ProductionCenterId">Id del centro de Produccion</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDataImportLogisticProductionCenterById(ProductionCenterId As Integer) As ActionResult(Of List(Of LogisticsProductionCenterRecordDetail)) Implements IDirectDistributionSecondaryAdminService.GetDataImportLogisticProductionCenterById

    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _directDistributionSecondaryRepository = Nothing
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
