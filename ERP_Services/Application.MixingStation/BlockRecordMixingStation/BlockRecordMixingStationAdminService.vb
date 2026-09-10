'***********************************************************************
' Assembly         : Application.MixingStation
' Author           : Yoe Andres Cardenas
' Created          : 26-04-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Class BlockRecordMixingStationAdminService
    Implements IBlockRecordMixingStationAdminService, Inject

#Region "Fields"

    ''' <summary>
    ''' Repositorio de la entidad ciudades bancarias
    ''' </summary>
    Private _Repository As IBlockRecordMixingStationRepository

#End Region

#Region "Constructor"

    Public Sub New(ByVal repository As IBlockRecordMixingStationRepository)
        If repository Is Nothing Then
            Throw New ArgumentNullException("repository")
        End If
        _Repository = repository
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    ''' <param name="blockRecordMixingStation"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteBlockRecordMixingStation(blockRecordMixingStation As BlockRecordMixingStation) As ActionResult Implements IBlockRecordMixingStationAdminService.DeleteBlockRecordMixingStation
        If blockRecordMixingStation Is Nothing Then
            Throw New ArgumentNullException("blockRecordMixingStation")
        End If
        Dim unitOfWork As IUnitWork = Me._Repository.UnitWork
        Try
            Me._Repository.DeleteEntity(blockRecordMixingStation)
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
    Public Function GetBlockRecordMixingStationByIdformAndIdRecord(IdForm As String, IdRecord As String) As BlockRecordMixingStation Implements IBlockRecordMixingStationAdminService.GetBlockRecordMixingStationByIdformAndIdRecord
        If String.IsNullOrEmpty(IdForm) Or String.IsNullOrEmpty(IdRecord) Then
            Throw New ArgumentNullException("IdForm IdRecord")
        End If
        Try
            Dim blockRecordMixingStation As BlockRecordMixingStation = Me._Repository.GetBlockRecordMixingStationByIdformAndIdRecord(IdForm, IdRecord)
            Return blockRecordMixingStation
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guarda o actualiza el registro bloqueado
    ''' </summary>
    ''' <param name="blockRecordMixingStation"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveBlockRecordMixingStation(blockRecordMixingStation As BlockRecordMixingStation) As ActionResult(Of BlockRecordMixingStation) Implements IBlockRecordMixingStationAdminService.SaveBlockRecordMixingStation
        If blockRecordMixingStation Is Nothing Then
            Throw New ArgumentNullException("blockRecordMixingStation Vacio")
        End If
        Dim UnitOfWork As IUnitWork = _Repository.UnitWork
        Try
            'Valido si se va a guardar o actualizar
            _Repository.SaveEntity(blockRecordMixingStation)
            UnitOfWork.Commit()
            Return New ActionResult(Of BlockRecordMixingStation) With {.StateResult = True, .ObjectEmbbeded = blockRecordMixingStation}
        Catch ex As Exception
            UnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return New ActionResult(Of BlockRecordMixingStation) With {.StateResult = False}
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
