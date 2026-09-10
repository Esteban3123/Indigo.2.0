'***********************************************************************
' Assembly         : Application.Payments
' Author           : Juan Carlos Bermudez
' Created          : 16/07/2015
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

Public Class BlockRecordPayrollAdminService
    Implements IBlockRecordPayrollAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de la entidad ciudades bancarias
    ''' </summary>
    Private _Repository As IBlockRecordPayrollRepository

#End Region

#Region "Constructor"

    Public Sub New(ByVal Repository As IBlockRecordPayrollRepository)
        If Repository Is Nothing Then
            Throw New ArgumentNullException("repository")
        End If
        _Repository = Repository
    End Sub

#End Region

#Region "methods"

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    ''' <param name="blockRecordPayroll"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteBlockRecordPayroll(blockRecordPayroll As BlockRecordPayroll) As ActionResult Implements IBlockRecordPayrollAdminService.DeleteBlockRecordPayroll
        If blockRecordPayroll Is Nothing Then
            Throw New ArgumentNullException("blockRecordPayroll")
        End If
        Dim unitOfWork As IUnitWork = Me._Repository.UnitWork
        Try
            Me._Repository.DeleteEntity(blockRecordPayroll)
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
    ''' Obtiene el registro bloqueado
    ''' </summary>
    ''' <param name="IdForm"></param>
    ''' <param name="IdRecord"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBlockRecordPayrollByIdformAndIdRecord(IdForm As String, IdRecord As String) As BlockRecordPayroll Implements IBlockRecordPayrollAdminService.GetBlockRecordPayrollByIdformAndIdRecord
        If String.IsNullOrEmpty(IdForm) Or String.IsNullOrEmpty(IdRecord) Then
            Throw New ArgumentNullException("IdForm IdRecord")
        End If
        Try
            Dim blockRecordPayroll As BlockRecordPayroll = Me._Repository.GetBlockRecordPayrollByIdformAndIdRecord(IdForm, IdRecord)
            Return blockRecordPayroll
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guarda o actualiza el registro bloqueado
    ''' </summary>
    ''' <param name="blockRecordPayroll"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveBlockRecordPayroll(blockRecordPayroll As BlockRecordPayroll) As ActionResult(Of BlockRecordPayroll) Implements IBlockRecordPayrollAdminService.SaveBlockRecordPayroll
        If blockRecordPayroll Is Nothing Then
            Throw New ArgumentNullException("blockRecordPayroll Vacio")
        End If
        Dim UnitOfWork As IUnitWork = _Repository.UnitWork
        Try
            'Valido si se va a guardar o actualizar
            _Repository.SaveEntity(blockRecordPayroll)
            UnitOfWork.Commit()
            Return New ActionResult(Of BlockRecordPayroll) With {.StateResult = True, .ObjectEmbbeded = blockRecordPayroll}
        Catch ex As Exception
            UnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return New ActionResult(Of BlockRecordPayroll) With {.StateResult = False}
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
