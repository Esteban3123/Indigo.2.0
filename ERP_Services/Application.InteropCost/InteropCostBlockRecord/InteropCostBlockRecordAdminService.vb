'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 31-03-2014
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

Public Class InteropCostBlockRecordAdminService
    Implements IInteropCostBlockRecordAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de la bloqueo de registros
    ''' </summary>
    Private _Repository As IBlockRecordInteropCostRepository

#End Region

    Public Sub New(ByVal Repository As IBlockRecordInteropCostRepository)
        If Repository Is Nothing Then
            Throw New ArgumentNullException("repository")
        End If
        _Repository = Repository
    End Sub

    ''' <summary>
    ''' Elimina un registro bloqueado
    ''' </summary>
    ''' <param name="blockRecordInteropCost"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">blockRecordInteropCost</exception>
    Public Function DeleteBlockRecordInteropCost(blockRecordInteropCost As BlockRecordInteropCost) As ActionResult Implements IInteropCostBlockRecordAdminService.DeleteBlockRecordInteropCost
        If blockRecordInteropCost Is Nothing Then
            Throw New ArgumentNullException("blockRecordInteropCost")
        End If
        Dim unitOfWork As IUnitWork = Me._Repository.UnitWork
        Try
            Me._Repository.DeleteEntity(blockRecordInteropCost)
            unitOfWork.Commit()
            Return New ActionResult With {.StateResult = True}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un registro bloqueado por id del registro y formulario
    ''' </summary>
    Public Function GetBlockRecordInteropCostByIdformAndIdRecord(IdForm As String, IdRecord As String, Optional tracking As Boolean = True) As BlockRecordInteropCost Implements IInteropCostBlockRecordAdminService.GetBlockRecordInteropCostByIdformAndIdRecord
        If String.IsNullOrEmpty(IdForm) Or String.IsNullOrEmpty(IdRecord) Then
            Throw New ArgumentNullException("IdForm IdRecord")
        End If
        Try
            Dim blockRecordInteropCost As BlockRecordInteropCost = Me._Repository.GetBlockRecordInteropCostByIdformAndIdRecord(IdForm, IdRecord)
            Return blockRecordInteropCost
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Bloquea un registro
    ''' </summary>
    ''' <param name="blockRecordInteropCost"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">blockRecordInteropCost Vacio</exception>
    Public Function SaveBlockRecordInteropCost(blockRecordInteropCost As BlockRecordInteropCost) As ActionResult(Of BlockRecordInteropCost) Implements IInteropCostBlockRecordAdminService.SaveBlockRecordInteropCost
        If blockRecordInteropCost Is Nothing Then
            Throw New ArgumentNullException("blockRecordInteropCost Vacio")
        End If
        Dim UnitOfWork As IUnitWork = _Repository.UnitWork
        Try
            'Valido si se va a guardar o actualizar
            _Repository.SaveEntity(blockRecordInteropCost)
            UnitOfWork.Commit()
            'Agrego la auditoria
            Return New ActionResult(Of BlockRecordInteropCost) With {.StateResult = True, .ObjectEmbbeded = blockRecordInteropCost}
        Catch ex As Exception
            UnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return New ActionResult(Of BlockRecordInteropCost) With {.StateResult = False}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _Repository = Nothing
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