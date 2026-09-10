'***********************************************************************
' Assembly         : Application.Common
' Author           : Juan Diego Diaz
' Created          : 09-08-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Common
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base
Imports Domain.Base.Entities
Imports Application.Base

Public Class BlockRecordAdminService

    Implements IBlockRecordAdminService

    ' Repositorio de registro bloqueado
    Private _blockRecordRepository As IBlockRecordRepository

    Public Sub New(ByVal blockRecordRepository As IBlockRecordRepository)
        If blockRecordRepository Is Nothing Then
            Throw New ArgumentNullException("blockRecordRepository vacío")
        End If
        _blockRecordRepository = blockRecordRepository
    End Sub

    ''' <summary>
    ''' Elimina una registro bloqueado
    ''' </summary>
    ''' <param name="blockRecord">Registro bloqueado</param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>ActionResult</returns>
    Public Function DeleteBlockRecord(blockRecord As BlockRecord, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IBlockRecordAdminService.DeleteBlockRecord
        If blockRecord Is Nothing Then
            Throw New ArgumentNullException("blockRecord Vacio")
        End If
        Dim UnitOfWork As IUnitWork = _blockRecordRepository.UnitWork
        Try
            _blockRecordRepository.DeleteEntity(blockRecord)
            UnitOfWork.Commit()
            'IndigoAuditSimpleEntity(Of BlockRecord).Execute(blockRecord, audit, Infrastructure.CrossCutting.Audit.Actions.Delete, blockRecord)
            Return New ActionResult With {.StateResult = True}
        Catch ex As Exception
            UnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return New ActionResult With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una registro de bloqueo según parametros
    ''' </summary>
    ''' <param name="IdForm">Id del formulario</param>
    ''' <param name="IdRecord">Id del registro</param>
    ''' <returns>Registro bloqueado</returns>
    Public Function GetBlockRecordByIdformAndIdRecord(IdForm As String, IdRecord As String) As BlockRecord Implements IBlockRecordAdminService.GetBlockRecordByIdformAndIdRecord
        If String.IsNullOrEmpty(IdForm) Then
            Throw New ArgumentNullException("IdForm Vacio")
        End If
        If String.IsNullOrEmpty(IdRecord) Then
            Throw New ArgumentNullException("IdRecord Vacio")
        End If
        Try
            Return _blockRecordRepository.GetBlockRecordByIdformAndIdRecord(IdForm, IdRecord)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Lista Todos los registros bloqueados
    ''' </summary>
    ''' <returns>Registros bloqueados</returns>
    Public Function ListAllBlockRecord() As List(Of BlockRecord) Implements IBlockRecordAdminService.ListAllBlockRecord
        Try
            Return _blockRecordRepository.ListAllBlockRecord
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Almacena o Actualiza registro bloqueado
    ''' </summary>
    ''' <param name="blockRecord">Registro bloqueado</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>ActionResult</returns>
    Public Function SaveBlockRecord(blockRecord As BlockRecord, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of BlockRecord) Implements IBlockRecordAdminService.SaveBlockRecord
        If blockRecord Is Nothing Then
            Throw New ArgumentNullException("blockRecord Vacio")
        End If
        Dim AuxBlockRecord As BlockRecord = Nothing

        Dim UnitOfWork As IUnitWork = _blockRecordRepository.UnitWork
        Try
            'Valido si se va a guardar o actualizar
            _blockRecordRepository.SaveEntity(blockRecord)
            UnitOfWork.Commit()
            'Agrego la auditoria

            Return New ActionResult(Of BlockRecord) With {.StateResult = True, .ObjectEmbbeded = blockRecord}
        Catch ex As Exception
            UnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return New ActionResult(Of BlockRecord) With {.StateResult = False}
        End Try
    End Function


    ''' <summary>
    ''' Limpiar regsitro de bloqueo
    ''' </summary>
    ''' <param name="CodUser"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SP_UnlockBlockRecord(CodUser As String, session As Infrastructure.CrossCutting.Base.SessionValues) As Boolean Implements IBlockRecordAdminService.SP_UnlockBlockRecord
        If CodUser Is Nothing Then
            Throw New ArgumentNullException("codigo usario Vacio")
        End If
        Dim UnitOfWork As IUnitWork = _blockRecordRepository.UnitWork
        Try
            Dim ObjSP_UnlockBlockRecord As SP_UnlockBlockRecord_Result
            ObjSP_UnlockBlockRecord = _blockRecordRepository.SP_UnlockBlockRecord(CodUser)

            If ObjSP_UnlockBlockRecord.Resultado = "1" Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            UnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return False
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

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
