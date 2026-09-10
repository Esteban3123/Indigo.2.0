'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 11/12/2014
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

Public Class BlockRecordMedicalFeesAdminService
    Implements IBlockRecordMedicalFeesAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de la entidad de bloqueo de registro
    ''' </summary>
    Private _Repository As IBlockRecordMedicalFeesRepository

#End Region

#Region "Constructor"

    Public Sub New(ByVal Repository As IBlockRecordMedicalFeesRepository)
        If Repository Is Nothing Then
            Throw New ArgumentNullException("repository")
        End If
        _Repository = Repository
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Elimina un registro bloqueado
    ''' </summary>
    ''' <param name="BlockRecordMedicalFees"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteBlockRecordMedicalFees(BlockRecordMedicalFees As BlockRecordMedicalFees) As ActionResult Implements IBlockRecordMedicalFeesAdminService.DeleteBlockRecordMedicalFees
        If BlockRecordMedicalFees Is Nothing Then
            Throw New ArgumentNullException("BlockRecordMedicalFees")
        End If
        Dim unitOfWork As IUnitWork = Me._Repository.UnitWork
        Try
            Me._Repository.DeleteEntity(BlockRecordMedicalFees)
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
    ''' Obtiene un registro bloqueado
    ''' </summary>
    ''' <param name="IdForm"></param>
    ''' <param name="IdRecord"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBlockRecordMedicalFeesByIdformAndIdRecord(IdForm As String, IdRecord As String) As BlockRecordMedicalFees Implements IBlockRecordMedicalFeesAdminService.GetBlockRecordMedicalFeesByIdformAndIdRecord
        If String.IsNullOrEmpty(IdForm) Or String.IsNullOrEmpty(IdRecord) Then
            Throw New ArgumentNullException("IdForm IdRecord")
        End If
        Try
            Dim BlockRecordMedicalFees As BlockRecordMedicalFees = Me._Repository.GetBlockRecordMedicalFeesByIdformAndIdRecord(IdForm, IdRecord)
            Return BlockRecordMedicalFees
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guarda el registro bloqueado
    ''' </summary>
    ''' <param name="BlockRecordMedicalFees"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveBlockRecordMedicalFees(BlockRecordMedicalFees As BlockRecordMedicalFees) As ActionResult(Of BlockRecordMedicalFees) Implements IBlockRecordMedicalFeesAdminService.SaveBlockRecordMedicalFees
        If BlockRecordMedicalFees Is Nothing Then
            Throw New ArgumentNullException("BlockRecordMedicalFees Vacio")
        End If
        Dim UnitOfWork As IUnitWork = _Repository.UnitWork
        Try
            'Valido si se va a guardar o actualizar
            _Repository.SaveEntity(BlockRecordMedicalFees)
            UnitOfWork.Commit()
            Return New ActionResult(Of BlockRecordMedicalFees) With {.StateResult = True, .ObjectEmbbeded = BlockRecordMedicalFees}
        Catch ex As Exception
            UnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return New ActionResult(Of BlockRecordMedicalFees) With {.StateResult = False}
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
