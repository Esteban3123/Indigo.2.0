'***********************************************************************
' Assembly         : Application.Budget
' Author           : Carlos Ernesto Cordoba
' Created          : 19-08-2015
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
Imports System.Transactions

#End Region

Public Class CollectionDetailAdminService
    Implements ICollectionDetailAdminService

    Private _collectionDetailRepository As ICollectionDetailRepository

    Public Sub New(collectionDetailRepository As ICollectionDetailRepository)
        If collectionDetailRepository Is Nothing Then
            Throw New ArgumentNullException("collectionDetailRepository")
        End If
        _collectionDetailRepository = collectionDetailRepository
    End Sub

    Public Function GetCollectionDetailByCollectionId(CollectionId As Integer) As List(Of CollectionDetail) Implements ICollectionDetailAdminService.GetCollectionDetailByCollectionId
        Try
            Return _collectionDetailRepository.GetCollectionDetailByCollectionId(CollectionId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of CollectionDetail)
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _collectionDetailRepository = Nothing
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
