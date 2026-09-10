'***********************************************************************
' Assembly         : Application.Budget
' Author           : Carlos Mario Arias Rubiano
' Created          : 29/01/2018
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
Imports Application.Budget

Public Class PrivateBudgetItemsStructureAdminService
    Implements IPrivateBudgetItemsStructureAdminService

#Region "Variables"

    ''' <summary>
    ''' Variable tipo repositorio 
    ''' </summary>
    ''' <remarks></remarks>
    Private _privateBudgetItemsStructureRepository As IPrivateBudgetItemsStructureRepository

    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDRepository As ISequenseBudgetDRepository

#End Region

#Region "Constructor"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal privateBudgetItemsStructureRepository As IPrivateBudgetItemsStructureRepository, ByVal secuenseDRepository As ISequenseBudgetDRepository)
        If privateBudgetItemsStructureRepository Is Nothing Then
            Throw New ArgumentNullException("privateBudgetItemsStructureRepository Vacio")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        _privateBudgetItemsStructureRepository = privateBudgetItemsStructureRepository
        _secuenseDRepository = secuenseDRepository
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Elimina un registro
    ''' </summary>
    ''' <param name="PrivateBudgetItemsStructure"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function DeletePrivateBudgetItemsStructure(PrivateBudgetItemsStructure As PrivateBudgetItemsStructure, audit As AuditMessage) As ActionResult Implements IPrivateBudgetItemsStructureAdminService.DeletePrivateBudgetItemsStructure
        If PrivateBudgetItemsStructure Is Nothing Then
            Throw New ArgumentNullException("PrivateBudgetItemsStructure")
        End If
        Dim unitOfWork As IUnitWork = Me._privateBudgetItemsStructureRepository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of PrivateBudgetItemsStructure)
            auditProcess = New IndigoAuditSimpleEntity(Of PrivateBudgetItemsStructure)(PrivateBudgetItemsStructure, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            Me._privateBudgetItemsStructureRepository.DeleteEntity(PrivateBudgetItemsStructure)
            unitOfWork.Commit()
            auditProcess.Execute()
            Return New ActionResult With {.StateResult = True}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"}), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un registro por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function GetPrivateBudgetItemsStructure(code As String, audit As AuditMessage) As ActionResult(Of PrivateBudgetItemsStructure) Implements IPrivateBudgetItemsStructureAdminService.GetPrivateBudgetItemsStructure
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim PrivateBudgetItemsStructure As PrivateBudgetItemsStructure = Me._privateBudgetItemsStructureRepository.GetPrivateBudgetItemsStructure(code.Trim())
            If PrivateBudgetItemsStructure IsNot Nothing AndAlso PrivateBudgetItemsStructure.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of PrivateBudgetItemsStructure)(PrivateBudgetItemsStructure, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of PrivateBudgetItemsStructure) With {.StateResult = True, .ObjectEmbbeded = PrivateBudgetItemsStructure}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of PrivateBudgetItemsStructure) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un registro por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function GetPrivateBudgetItemsStructureById(id As String, audit As AuditMessage) As ActionResult(Of PrivateBudgetItemsStructure) Implements IPrivateBudgetItemsStructureAdminService.GetPrivateBudgetItemsStructureById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim PrivateBudgetItemsStructure As PrivateBudgetItemsStructure = Me._privateBudgetItemsStructureRepository.GetPrivateBudgetItemsStructureById(id)
            If PrivateBudgetItemsStructure IsNot Nothing AndAlso PrivateBudgetItemsStructure.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of PrivateBudgetItemsStructure)(PrivateBudgetItemsStructure, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of PrivateBudgetItemsStructure) With {.StateResult = True, .ObjectEmbbeded = PrivateBudgetItemsStructure}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of PrivateBudgetItemsStructure) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Guarda un registro
    ''' </summary>
    ''' <param name="PrivateBudgetItemsStructure"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    Public Function SavePrivateBudgetItemsStructure(PrivateBudgetItemsStructure As PrivateBudgetItemsStructure, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of PrivateBudgetItemsStructure) Implements IPrivateBudgetItemsStructureAdminService.SavePrivateBudgetItemsStructure
        If PrivateBudgetItemsStructure Is Nothing Then
            Throw New ArgumentNullException("PrivateBudgetItemsStructure")
        End If
        Dim unitOfWork As IUnitWork = Me._privateBudgetItemsStructureRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Dim seq As BudgetSequenceDetail = Nothing
            If PrivateBudgetItemsStructure.Code Is Nothing OrElse PrivateBudgetItemsStructure.Code.Trim().Equals(String.Empty) Then
                seq = Me._secuenseDRepository.GetSequenseDById(idSequense)
                If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.BudgetSequence.Sequential Then
                    Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                    If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                        PrivateBudgetItemsStructure.Code = res
                        seq.Next += 1
                        Me._secuenseDRepository.SaveEntity(seq)
                    Else
                        Return New ActionResult(Of PrivateBudgetItemsStructure) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
                    End If
                Else
                    Return New ActionResult(Of PrivateBudgetItemsStructure) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
                End If
            End If

            Dim auxPrivateBudgetItemsStructure As PrivateBudgetItemsStructure = Nothing
            Dim auditProcess As IndigoAuditSimpleEntity(Of PrivateBudgetItemsStructure)
            Dim status As Integer

            If PrivateBudgetItemsStructure.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                PrivateBudgetItemsStructure.CreationUser = audit.CodeUser
                PrivateBudgetItemsStructure.CreationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            Else
                auxPrivateBudgetItemsStructure = PrivateBudgetItemsStructure.OriginalValue
                PrivateBudgetItemsStructure.ModificationUser = audit.CodeUser
                PrivateBudgetItemsStructure.ModificationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Update
            End If

            Me._privateBudgetItemsStructureRepository.SaveEntity(PrivateBudgetItemsStructure)
            unitOfWork.Commit()
            sequenseUnitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of PrivateBudgetItemsStructure)(PrivateBudgetItemsStructure, audit, status, auxPrivateBudgetItemsStructure)
            auditProcess.Execute()

            'Se marca la entidad como sin cambios
            PrivateBudgetItemsStructure.MarkAsUnchanged()

            Return New ActionResult(Of PrivateBudgetItemsStructure) With {.StateResult = True, .ObjectEmbbeded = PrivateBudgetItemsStructure}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of PrivateBudgetItemsStructure) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of PrivateBudgetItemsStructure) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
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
            _privateBudgetItemsStructureRepository = Nothing
            _secuenseDRepository = Nothing
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
