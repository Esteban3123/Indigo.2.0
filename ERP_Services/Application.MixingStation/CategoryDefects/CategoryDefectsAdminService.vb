'***********************************************************************
' Assembly         : Application.MixingStation
' Author           : Andres Alarcon
' Created          : 26/08/2022
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.Transactions
Imports Application.Security
Imports Application.MixingStation

Public Class CategoryDefectsAdminService
    Implements ICategoryDefectsAdminService, Inject

    ''' <summary>
    ''' repositorio de las secuencias
    ''' </summary>
    Private _secuenseDRepository As IMixingStationSequenceDetailRepository
    Private _categoryDefectsRepository As ICategoryDefectsRepository

    Public Sub New(secuenceDRepository As IMixingStationSequenceDetailRepository, categoryDefectsRepository As ICategoryDefectsRepository)
        If secuenceDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenceDRepository")
        End If
        If categoryDefectsRepository Is Nothing Then
            Throw New ArgumentNullException("categoryDefectsRepository")
        End If
        _secuenseDRepository = secuenceDRepository
        _categoryDefectsRepository = categoryDefectsRepository
    End Sub

    Public Function SaveCategoryDefects(CategoryDefects As DefectClassificationGroup, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of DefectClassificationGroup) Implements ICategoryDefectsAdminService.SaveCategoryDefects
        If CategoryDefects Is Nothing Then
            Throw New ArgumentNullException("CategoryDefects")
        End If
        Dim unitOfWork As IUnitWork = Me._categoryDefectsRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try

            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})

                Dim seq As MixingStationSequenceDetail = Nothing
                If CategoryDefects.Code Is Nothing OrElse CategoryDefects.Code.Trim().Equals(String.Empty) Then
                    seq = Me._secuenseDRepository.GetSequenceDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.MixingStationSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            CategoryDefects.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of DefectClassificationGroup) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = ResourceManager.GetString("SequenceNotFound")}
                        End If
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of DefectClassificationGroup) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = ResourceManager.GetString("SequenceNotFound")}
                    End If
                End If

                Dim auxCategoryDefects As DefectClassificationGroup = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of DefectClassificationGroup)
                Dim status As Integer

                If CategoryDefects.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    CategoryDefects.CreationUser = audit.CodeUser
                    CategoryDefects.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    auxCategoryDefects = CategoryDefects.OriginalValue
                    CategoryDefects.ModificationUser = audit.CodeUser
                    CategoryDefects.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._categoryDefectsRepository.SaveEntity(CategoryDefects)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of DefectClassificationGroup)(CategoryDefects, audit, status, auxCategoryDefects)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                CategoryDefects.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of DefectClassificationGroup) With {.StateResult = True, .ObjectEmbbeded = CategoryDefects}
            End Using

        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of DefectClassificationGroup) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of DefectClassificationGroup) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    Public Function DeleteCategoryDefects(CategoryDefects As DefectClassificationGroup, audit As AuditMessage) As ActionResult Implements ICategoryDefectsAdminService.DeleteCategoryDefects
        If CategoryDefects Is Nothing Then
            Throw New ArgumentNullException("CategoryDefects")
        End If
        Dim unitOfWork As IUnitWork = Me._categoryDefectsRepository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of DefectClassificationGroup)
            auditProcess = New IndigoAuditSimpleEntity(Of DefectClassificationGroup)(CategoryDefects, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)

            CategoryDefects.MarkAsDeleted()

            Me._categoryDefectsRepository.SaveEntity(CategoryDefects)
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

    Public Function GetCategoryDefects(code As String, audit As AuditMessage) As DefectClassificationGroup Implements ICategoryDefectsAdminService.GetCategoryDefects
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim CategoryDefects As DefectClassificationGroup = Me._categoryDefectsRepository.GetCategoryDefectsByCode(code.Trim())
            If CategoryDefects IsNot Nothing AndAlso CategoryDefects.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of DefectClassificationGroup)(CategoryDefects, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If

            Return CategoryDefects
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New DefectClassificationGroup
        End Try
    End Function

    Public Function GetCategoryDefectsById(id As Integer) As DefectClassificationGroup Implements ICategoryDefectsAdminService.GetCategoryDefectsById
        Try
            Return _categoryDefectsRepository.GetCategoryDefectsById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New DefectClassificationGroup
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: elimine el estado administrado (objetos administrados).
            End If
            _secuenseDRepository = Nothing
            _categoryDefectsRepository = Nothing
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
