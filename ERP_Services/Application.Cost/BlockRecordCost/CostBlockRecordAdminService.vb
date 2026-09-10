'***********************************************************************
' Assembly         : Application.Cost
' Author           : Diego Andrés Roldán Lozano
' Created          : 26-02-2016
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

Public Class CostBlockRecordAdminService
    Implements ICostBlockRecordAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de la bloqueo de registros
    ''' </summary>
    Private _repositoryCostBlockRecord As IBlockRecordCostRepository

#End Region

    Public Sub New(ByVal repository As IBlockRecordCostRepository)
        If repository Is Nothing Then
            Throw New ArgumentNullException("repository")
        End If
        _repositoryCostBlockRecord = repository
    End Sub

    ''' <summary>
    ''' Elimina un registro bloqueado
    ''' </summary>
    ''' <param name="blockRecordCost"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">blockRecordCost</exception>
    Public Function DeleteBlockRecordCost(blockRecordCost As BlockRecordCost) As ActionResult Implements ICostBlockRecordAdminService.DeleteBlockRecordCost
        If blockRecordCost Is Nothing Then
            Throw New ArgumentNullException("blockRecordCost")
        End If
        Dim unitOfWork As IUnitWork = Me._repositoryCostBlockRecord.UnitWork
        Try
            Me._repositoryCostBlockRecord.DeleteEntity(blockRecordCost)
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
    ''' <param name="IdForm"></param>
    ''' <param name="IdRecord"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">IdForm IdRecord</exception>
    Public Function GetBlockRecordCostByIdformAndIdRecord(IdForm As String, IdRecord As String, Optional tracking As Boolean = True) As BlockRecordCost Implements ICostBlockRecordAdminService.GetBlockRecordCostByIdformAndIdRecord
        If String.IsNullOrEmpty(IdForm) Or String.IsNullOrEmpty(IdRecord) Then
            Throw New ArgumentNullException("IdForm IdRecord")
        End If
        Try
            Return Me._repositoryCostBlockRecord.GetBlockRecordCostByIdformAndIdRecord(IdForm, IdRecord)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Bloquea un registro
    ''' </summary>
    ''' <param name="blockRecordCost"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">blockRecordCost Vacio</exception>
    Public Function SaveBlockRecordCost(blockRecordCost As BlockRecordCost) As ActionResult(Of BlockRecordCost) Implements ICostBlockRecordAdminService.SaveBlockRecordCost
        If blockRecordCost Is Nothing Then
            Throw New ArgumentNullException("blockRecordCost Vacio")
        End If
        Dim UnitOfWork As IUnitWork = _repositoryCostBlockRecord.UnitWork
        Try
            'Valido si se va a guardar o actualizar
            _repositoryCostBlockRecord.SaveEntity(blockRecordCost)
            UnitOfWork.Commit()
            'Agrego la auditoria
            Return New ActionResult(Of BlockRecordCost) With {.StateResult = True, .ObjectEmbbeded = blockRecordCost}
        Catch ex As Exception
            UnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return New ActionResult(Of BlockRecordCost) With {.StateResult = False}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _repositoryCostBlockRecord = Nothing
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