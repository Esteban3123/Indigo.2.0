'***********************************************************************
' Assembly         : Application.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/04/2014
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

Public Class BlockRecordPaymentsAdminService
    Implements IBlockRecordPaymentsAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de la entidad ciudades bancarias
    ''' </summary>
    Private _Repository As IBlockRecordPaymentsRepository

#End Region

#Region "Constructor"

    Public Sub New(ByVal Repository As IBlockRecordPaymentsRepository)
        If Repository Is Nothing Then
            Throw New ArgumentNullException("repository")
        End If
        _Repository = Repository
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    ''' <param name="blockRecordPayments"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteBlockRecordPayments(blockRecordPayments As BlockRecordPayments) As ActionResult Implements IBlockRecordPaymentsAdminService.DeleteBlockRecordPayments
        If blockRecordPayments Is Nothing Then
            Throw New ArgumentNullException("blockRecordPayments")
        End If
        Dim unitOfWork As IUnitWork = Me._Repository.UnitWork
        Try
            Me._Repository.DeleteEntity(blockRecordPayments)
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
    Public Function GetBlockRecordPaymentsByIdformAndIdRecord(IdForm As String, IdRecord As String) As BlockRecordPayments Implements IBlockRecordPaymentsAdminService.GetBlockRecordPaymentsByIdformAndIdRecord
        If String.IsNullOrEmpty(IdForm) Or String.IsNullOrEmpty(IdRecord) Then
            Throw New ArgumentNullException("IdForm IdRecord")
        End If
        Try
            Dim blockRecordPayments As BlockRecordPayments = Me._Repository.GetBlockRecordPaymentsByIdformAndIdRecord(IdForm, IdRecord)
            Return blockRecordPayments
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene el registro bloqueado
    ''' </summary>
    ''' <param name="IdForm"></param>
    ''' <param name="Consecutive"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBlockRecordPaymentsByIdformAndConsecutive(IdForm As String, Consecutive As String) As BlockRecordPayments Implements IBlockRecordPaymentsAdminService.GetBlockRecordPaymentsByIdformAndConsecutive
        If String.IsNullOrEmpty(IdForm) Or String.IsNullOrEmpty(Consecutive) Then
            Throw New ArgumentNullException("IdForm Consecutive")
        End If
        Try
            Dim blockRecordPayments As BlockRecordPayments = Me._Repository.GetBlockRecordPaymentsByIdformAndConsecutive(IdForm, Consecutive)
            Return blockRecordPayments
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guarda o actualiza el registro bloqueado
    ''' </summary>
    ''' <param name="blockRecordPayments"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveBlockRecordPayments(blockRecordPayments As BlockRecordPayments) As ActionResult(Of BlockRecordPayments) Implements IBlockRecordPaymentsAdminService.SaveBlockRecordPayments
        If blockRecordPayments Is Nothing Then
            Throw New ArgumentNullException("blockRecordPayments Vacio")
        End If
        Dim UnitOfWork As IUnitWork = _Repository.UnitWork
        Try
            'Valido si se va a guardar o actualizar
            _Repository.SaveEntity(blockRecordPayments)
            UnitOfWork.Commit()
            Return New ActionResult(Of BlockRecordPayments) With {.StateResult = True, .ObjectEmbbeded = blockRecordPayments}
        Catch ex As Exception
            UnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return New ActionResult(Of BlockRecordPayments) With {.StateResult = False}
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
