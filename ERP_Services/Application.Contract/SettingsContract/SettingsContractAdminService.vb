'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 05/02/2020
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
Imports Application.Contract

Public Class SettingsContractAdminService
    Implements ISettingsContractAdminService

#Region "Variables"

    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _settingsContractRepository As ISettingsContractRepository
    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDRepository As ISequenseContractDRepository

#End Region

#Region "Builder"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal settingsContractRepository As ISettingsContractRepository, ByVal secuenseDRepository As ISequenseContractDRepository)
        If settingsContractRepository Is Nothing Then
            Throw New ArgumentNullException("settingsContractRepository Vacio")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        _settingsContractRepository = settingsContractRepository
        _secuenseDRepository = secuenseDRepository
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Guarda un registro
    ''' </summary>
    ''' <param name="SettingsContract"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    Public Function SaveSettingsContract(SettingsContract As SettingsContract, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of SettingsContract) Implements ISettingsContractAdminService.SaveSettingsContract
        If SettingsContract Is Nothing Then
            Throw New ArgumentNullException("SettingsContract")
        End If
        Dim unitOfWork As IUnitWork = Me._settingsContractRepository.UnitWork
        Try
            If SettingsContract.CUPSWithRelatedDescription = False Then
                If _settingsContractRepository.ValidationDescriptions() > 0 Then
                    Return New ActionResult(Of SettingsContract) With {.StateResult = False, .Message = "No se puede guardar el parámetro 'CUPS con descripción relacionada' en NO, ya que existen descripciones relacionadas a CUPS actualmente vigentes"}
                End If
            End If

            Dim auxSettingsContract As SettingsContract = Nothing
            Dim auditProcess As IndigoAuditSimpleEntity(Of SettingsContract)
            Dim status As Integer

            If SettingsContract.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                SettingsContract.CreationUser = audit.CodeUser
                SettingsContract.CreationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            Else
                auxSettingsContract = _settingsContractRepository.GetSettingsContractByOperatingUnitId(SettingsContract.OperatingUnitId, False)
                SettingsContract.ModificationUser = audit.CodeUser
                SettingsContract.ModificationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Update
            End If

            Me._settingsContractRepository.SaveEntity(SettingsContract)
            unitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of SettingsContract)(SettingsContract, audit, status, auxSettingsContract)
            auditProcess.Execute()

            SettingsContract.MarkAsUnchanged()

            Return New ActionResult(Of SettingsContract) With {.StateResult = True, .ObjectEmbbeded = SettingsContract}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of SettingsContract) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of SettingsContract) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Elimina un registro
    ''' </summary>
    ''' <param name="SettingsContract"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function DeleteSettingsContract(SettingsContract As SettingsContract, audit As AuditMessage) As ActionResult Implements ISettingsContractAdminService.DeleteSettingsContract
        If SettingsContract Is Nothing Then
            Throw New ArgumentNullException("SettingsContract")
        End If
        Dim unitOfWork As IUnitWork = Me._settingsContractRepository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of SettingsContract)
            auditProcess = New IndigoAuditSimpleEntity(Of SettingsContract)(SettingsContract, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            Me._settingsContractRepository.DeleteEntity(SettingsContract)
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

    ''' <summary>
    ''' Obtiene un registro por id unudad operativa
    ''' </summary>
    ''' <param name="operatingUnitId"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function GetSettingsContractByOperatingUnitId(operatingUnitId As Integer, audit As AuditMessage) As ActionResult(Of SettingsContract) Implements ISettingsContractAdminService.GetSettingsContractByOperatingUnitId
        If operatingUnitId = 0 Then
            Throw New ArgumentNullException("operatingUnitId")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim SettingsContract As SettingsContract = Me._settingsContractRepository.GetSettingsContractByOperatingUnitId(operatingUnitId)
            If SettingsContract IsNot Nothing AndAlso SettingsContract.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of SettingsContract)(SettingsContract, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of SettingsContract) With {.StateResult = True, .ObjectEmbbeded = SettingsContract}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of SettingsContract) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
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
            _settingsContractRepository = Nothing
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
