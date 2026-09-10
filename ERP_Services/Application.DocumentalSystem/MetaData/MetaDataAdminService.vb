'***********************************************************************
' Assembly         : Application.DocumentalSystem
' Author           : Juan Diego Diaz
' Created          : 22-09-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Transactions
Imports Domain.DocumentalSystem
Imports Domain.Base
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.DocumentalSystem.Entities
Imports Application.Base
Imports System.Data.SqlTypes

#End Region

''' <summary>
''' Servicio archivadores.
''' </summary>
''' <remarks></remarks>
Public Class MetaDataAdminService
    Implements IMetaDataAdminService


    Private _MetaDataRepository As IMetaDataRepository

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase <see cref="MetaDataAdminService" />.
    ''' </summary>
    ''' <param name="MetaDataRepository">el repositorio para el manejo de la metadata.</param>
    Public Sub New(ByVal MetaDataRepository As IMetaDataRepository)
        If MetaDataRepository Is Nothing Then
            Throw New ArgumentNullException("MetaDataRepository Vacio")
        End If
        _MetaDataRepository = MetaDataRepository
    End Sub

    ''' <summary>
    ''' Funcion para obtener metadata según archivador
    ''' </summary>
    ''' <param name="IdFileContainer">Id del Archivador</param>
    ''' <returns>Lista de Metadata</returns>
    Public Function GetMetadataByFileContainer(IdFileContainer As String) As List(Of Metadata) Implements IMetaDataAdminService.GetMetadataByFileContainer
        If String.IsNullOrEmpty(IdFileContainer) = True Then
            Throw New ArgumentNullException("IdFileContainer vacío")
        End If
        Try
            Return _MetaDataRepository.GetMetadataByFileContainer(IdFileContainer)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Función que obtiene una lista de metadata.
    ''' </summary>
    ''' <returns>Lista de Metadata</returns>
    Public Function ListAllMetadata() As List(Of Metadata) Implements IMetaDataAdminService.ListAllMetadata
        Try
            Return _MetaDataRepository.ListAllMetadata()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Funcion para guardar metadata
    ''' </summary>
    ''' <param name="_listMetadata"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function saveListMetata(ByVal _listMetadata As List(Of Metadata)) As ActionResult Implements IMetaDataAdminService.saveListMetata
        If _listMetadata Is Nothing AndAlso _listMetadata.Count = 0 Then
            Throw New ArgumentNullException("Lista de Metadata vacia")
        End If
        Dim unitOfWork As IUnitWork = _MetaDataRepository.UnitWork
        Try
            For i As Integer = 0 To _listMetadata.Count - 1
                _MetaDataRepository.SaveEntity(_listMetadata(i))
                unitOfWork.Commit()
            Next
            Return New ActionResult With {.StateResult = True}
        Catch ex As OperationAbortedException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False}
        End Try
    End Function
    ''' <summary>
    ''' Funcion para eliminar metadata
    ''' </summary>
    ''' <param name="_objMetadata"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteMetadata(ByVal _objMetadata As Metadata) As ActionResult Implements IMetaDataAdminService.DeleteMetadata
        If _objMetadata Is Nothing Then
            Throw New ArgumentNullException("Metadata vacia")
        End If
        Dim unitOfWork As IUnitWork = _MetaDataRepository.UnitWork
        Try
            _MetaDataRepository.DeleteEntity(_objMetadata)
            unitOfWork.Commit()
            Return New ActionResult With {.StateResult = True}
        Catch ex As OperationAbortedException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False}
        End Try
    End Function

End Class

