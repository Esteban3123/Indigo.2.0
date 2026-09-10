#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Entities
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports Domain.Base
Imports Application.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Core
Imports Infrastructure.CrossCutting.Audit

#End Region

Public Class OperatingUnitAdminService
    Implements IOperatingUnitAdminService

#Region "Variables"
    Private _operatingRepository As IOperatingUnitRepository
#End Region

#Region "Build"

    ''' <summary>
    ''' inicia el repositorio de unidad operativa
    ''' </summary>
    ''' <param name="operatingUnitRepository">repositorio de unidad operativa</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal operatingUnitRepository As IOperatingUnitRepository)
        If (operatingUnitRepository Is Nothing) Then
            Throw New ArgumentNullException("OperatingUnitRepositorty es vacio")
        End If
        _operatingRepository = operatingUnitRepository
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' obtiene una unidad operativa por codigo
    ''' </summary>
    ''' <param name="code">codigo de la unidad operativa</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetOpertatingUnitByCode(code As String, audit As AuditMessage) As ActionResult(Of OperatingUnit) Implements IOperatingUnitAdminService.GetOperatingUnitByCode
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim operatingUnit As OperatingUnit = _operatingRepository.GetOpertatingUnitByCode(code.Trim())
            Return New ActionResult(Of OperatingUnit) With {.StateResult = True, .ObjectEmbbeded = operatingUnit}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of OperatingUnit) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' obtiene una unidad operativa por id
    ''' </summary>
    ''' <param name="id">id de la unidad operativa</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetOperatingUnitById(id As Integer, audit As AuditMessage) As ActionResult(Of OperatingUnit) Implements IOperatingUnitAdminService.GetOperatingUnitById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentException("audit")
        End If
        Try
            Dim operatingUnit As OperatingUnit = _operatingRepository.GetOperatingUnitById(id)
            Return New ActionResult(Of OperatingUnit) With {.StateResult = True, .ObjectEmbbeded = operatingUnit}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of OperatingUnit) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try

    End Function

    ''' <summary>
    ''' guarda o actualiza una unidad operativa
    ''' </summary>
    ''' <param name="operatingUnit"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveOperatingUnit(operatingUnit As OperatingUnit, audit As AuditMessage) As ActionResult(Of OperatingUnit) Implements IOperatingUnitAdminService.SaveOperatingUnit
        If operatingUnit Is Nothing Then
            Throw New ArgumentNullException("operatingUnit")
        End If
        Dim unitOfWork As IUnitWork = _operatingRepository.UnitWork

        Try
            Dim auxOperatingUnit As OperatingUnit = Nothing
            Dim auditProcess As IndigoAuditSimpleEntity(Of OperatingUnit)
            Dim status As Actions

            If (operatingUnit.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added) Then
                operatingUnit.CreationUser = audit.CodeUser
                operatingUnit.CreationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            Else
                auxOperatingUnit = operatingUnit.OriginalValue
                operatingUnit.ModificationUser = audit.CodeUser
                operatingUnit.ModificationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Update
            End If

            _operatingRepository.SaveEntity(operatingUnit)
            unitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of OperatingUnit)(operatingUnit, audit, status, auxOperatingUnit)
            auditProcess.Execute()

            Return New ActionResult(Of OperatingUnit) With {.StateResult = True, .ObjectEmbbeded = operatingUnit}

        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of OperatingUnit) With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of OperatingUnit) With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of OperatingUnit) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try

    End Function

    ''' <summary>
    ''' elimina una unidad operativa
    ''' </summary>
    ''' <param name="operatingUnit"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteOperatingUnit(operatingUnit As OperatingUnit, audit As AuditMessage) As ActionResult Implements IOperatingUnitAdminService.DeleteOperatingUnit
        If operatingUnit Is Nothing Then
            Throw New ArgumentNullException("operatingUnit")
        End If

        Dim unitOfWork As IUnitWork = _operatingRepository.UnitWork

        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of OperatingUnit)
            auditProcess = New IndigoAuditSimpleEntity(Of OperatingUnit)(operatingUnit, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            _operatingRepository.DeleteEntity(operatingUnit)
            unitOfWork.Commit()
            auditProcess.Execute()

            Return New ActionResult() With {.StateResult = True}
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
    ''' Funcion que lista todas las unidades operativas
    ''' </summary>
    ''' <returns>Lista de unidades operativos</returns>
    ''' <remarks></remarks>
    Public Function ListAllOperatingUnit() As List(Of OperatingUnit) Implements IOperatingUnitAdminService.ListAllOperatingUnit
        Try
            Return _operatingRepository.ListAllOperatingUnit()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of OperatingUnit)
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
            _operatingRepository = Nothing
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
