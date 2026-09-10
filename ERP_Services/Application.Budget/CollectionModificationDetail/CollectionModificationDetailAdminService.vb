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

Public Class CollectionModificationDetailAdminService
    Implements ICollectionModificationDetailAdminService

    Private _collectionModificatioDetailRepository As ICollectionModificationDetailRepository

    Public Sub New(collectionModificatioDetailRepository As ICollectionModificationDetailRepository)
        _collectionModificatioDetailRepository = collectionModificatioDetailRepository
    End Sub

    ''' <summary>
    ''' obtiene le detalle de la modificacion por id de la cabecera
    ''' </summary>
    ''' <param name="CollectionModificationId"></param>
    ''' <returns></returns>
    Public Function GetCollectionModificationDetailByCollectionId(CollectionModificationId As Integer) As List(Of CollectionModificationDetail) Implements ICollectionModificationDetailAdminService.GetCollectionModificationDetailByCollectionId
        Try
            Return _collectionModificatioDetailRepository.GetCollectionModificationDetailByCollectionId(CollectionModificationId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of CollectionModificationDetail)
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _collectionModificatioDetailRepository = Nothing
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
