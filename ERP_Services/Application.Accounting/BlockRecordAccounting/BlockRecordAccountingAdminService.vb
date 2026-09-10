'***********************************************************************
' Assembly         : Application.Accounting
' Author           : Diego Andrés Roldán Lozano
' Created          : 11-04-2014
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

Public Class BlockRecordAccountingAdminService
    Implements IBlockRecordAccountingAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de la entidad ciudades bancarias
    ''' </summary>
    Private _Repository As IBlockRecordAccountingRepository

#End Region

    Public Sub New(ByVal Repository As IBlockRecordAccountingRepository)
        If Repository Is Nothing Then
            Throw New ArgumentNullException("repository")
        End If
        _Repository = Repository
    End Sub

    ''' <summary>
    ''' Deletes the block record accounting.
    ''' </summary>
    ''' <param name="blockRecordAccounting">The block record accounting.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">blockRecordTreasury</exception>
    Public Function DeleteBlockRecordAccounting(blockRecordAccounting As BlockRecordGeneralLedger) As ActionResult Implements IBlockRecordAccountingAdminService.DeleteBlockRecordAccounting
        If blockRecordAccounting Is Nothing Then
            Throw New ArgumentNullException("blockRecordTreasury")
        End If
        Dim unitOfWork As IUnitWork = Me._Repository.UnitWork
        Try
            Me._Repository.DeleteEntity(blockRecordAccounting)
            unitOfWork.Commit()
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
            Return New ActionResult With {.StateResult = False}
        End Try
    End Function


    ''' <summary>
    ''' Gets the block record accounting by idform and identifier record.
    ''' </summary>
    ''' <param name="IdForm">The identifier form.</param>
    ''' <param name="IdRecord">The identifier record.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">IdForm IdRecord</exception>
    Public Function GetBlockRecordAccountingByIdformAndIdRecord(IdForm As String, IdRecord As String) As BlockRecordGeneralLedger Implements IBlockRecordAccountingAdminService.GetBlockRecordAccountingByIdformAndIdRecord
        If String.IsNullOrEmpty(IdForm) Or String.IsNullOrEmpty(IdRecord) Then
            Throw New ArgumentNullException("IdForm IdRecord")
        End If
        Try
            Dim blockRecordAccounting As BlockRecordGeneralLedger = Me._Repository.GetBlockRecordByIdformAndIdRecord(IdForm, IdRecord)
            Return blockRecordAccounting
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Saves the block record accounting.
    ''' </summary>
    ''' <param name="blockRecordAccounting">The block record accounting.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">blockRecordTreasury Vacio</exception>
    Public Function SaveBlockRecordAccounting(blockRecordAccounting As BlockRecordGeneralLedger) As ActionResult(Of BlockRecordGeneralLedger) Implements IBlockRecordAccountingAdminService.SaveBlockRecordAccounting
        If blockRecordAccounting Is Nothing Then
            Throw New ArgumentNullException("blockRecordTreasury Vacio")
        End If
        Dim UnitOfWork As IUnitWork = _Repository.UnitWork
        Try
            'Valido si se va a guardar o actualizar
            _Repository.SaveEntity(blockRecordAccounting)
            UnitOfWork.Commit()
            Return New ActionResult(Of BlockRecordGeneralLedger) With {.StateResult = True, .ObjectEmbbeded = blockRecordAccounting}
        Catch ex As Exception
            UnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return New ActionResult(Of BlockRecordGeneralLedger) With {.StateResult = False}
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
