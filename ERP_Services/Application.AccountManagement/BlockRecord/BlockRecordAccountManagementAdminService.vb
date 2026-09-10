'***********************************************************************
' Assembly         : Application.AccountManagment
' Author           : Felix Camilo Salazar Roldal
' Created          : 12-13-2024
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "imports"
Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region

Public Class BlockRecordAccountManagementAdminService
    Implements IBlockRecordAccountManagementAdminService, IDisposable

    ' Repositorio de registro bloqueado
    Private _blockRecordRepository As IBlockRecordAccountManagementRepository

    Public Sub New(ByVal blockRecordRepository As IBlockRecordAccountManagementRepository)
        If blockRecordRepository Is Nothing Then
            Throw New ArgumentNullException("blockRecordRepository vacío")
        End If
        _blockRecordRepository = blockRecordRepository
    End Sub

    ''' <summary>
    ''' Elimina una registro bloqueado
    ''' </summary>
    ''' <param name="blockRecord">Registro bloqueado</param>
    ''' <returns>ActionResult</returns>
    Public Function DeleteBlockRecord(blockRecord As BlockRecordAccountManagement) As Domain.Base.Entities.ActionResult Implements IBlockRecordAccountManagementAdminService.DeleteBlockRecord
        If blockRecord Is Nothing Then
            Throw New ArgumentNullException("blockRecord Vacio")
        End If
        Dim UnitOfWork As IUnitWork = _blockRecordRepository.UnitWork
        Try
            _blockRecordRepository.DeleteEntity(blockRecord)
            UnitOfWork.Commit()
            Return New ActionResult With {.StateResult = True}
        Catch ex As Exception
            UnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una registro de bloqueo según parametros
    ''' </summary>
    ''' <param name="IdForm">Id del formulario</param>
    ''' <param name="IdRecord">Id del registro</param>
    ''' <returns>Registro bloqueado</returns>
    Public Function GetBlockRecordByIdformAndIdRecord(IdForm As String, IdRecord As String) As BlockRecordAccountManagement Implements IBlockRecordAccountManagementAdminService.GetBlockRecordByIdformAndIdRecord
        If String.IsNullOrEmpty(IdForm) Then
            Throw New ArgumentNullException("IdForm Vacio")
        End If
        If String.IsNullOrEmpty(IdRecord) Then
            Throw New ArgumentNullException("IdRecord Vacio")
        End If
        Try
            Return _blockRecordRepository.GetBlockRecordByIdformAndIdRecord(IdForm, IdRecord)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Lista Todos los registros bloqueados
    ''' </summary>
    ''' <returns>Registros bloqueados</returns>
    Public Function ListAllBlockRecord() As List(Of BlockRecordAccountManagement) Implements IBlockRecordAccountManagementAdminService.ListAllBlockRecord
        Try
            Return _blockRecordRepository.ListAllBlockRecord
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Almacena o Actualiza registro bloqueado
    ''' </summary>
    ''' <param name="blockRecord">Registro bloqueado</param>
    ''' <returns>ActionResult</returns>
    Public Function SaveBlockRecord(blockRecord As BlockRecordAccountManagement) As Domain.Base.Entities.ActionResult(Of BlockRecordAccountManagement) Implements IBlockRecordAccountManagementAdminService.SaveBlockRecord
        If blockRecord Is Nothing Then
            Throw New ArgumentNullException("blockRecord Vacio")
        End If
        Dim AuxBlockRecord As BlockRecordAccountManagement = Nothing
        Dim UnitOfWork As IUnitWork = _blockRecordRepository.UnitWork
        Try
            _blockRecordRepository.SaveEntity(blockRecord)
            UnitOfWork.Commit()
            Return New ActionResult(Of BlockRecordAccountManagement) With {.StateResult = True, .ObjectEmbbeded = blockRecord}
        Catch ex As Exception
            UnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of BlockRecordAccountManagement) With {.StateResult = False}
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
            _blockRecordRepository = Nothing
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

