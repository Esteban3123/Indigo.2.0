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


Public Class ObligationDetailAdminService
    Implements IObligationDetailAdminService

    Private _ObligationDetailRepository As IObligationDetailRepository

    Public Sub New(ObligationDetailRepository As IObligationDetailRepository)
        If ObligationDetailRepository Is Nothing Then
            Throw New ArgumentNullException("ObligationDetailRepository")
        End If
        _ObligationDetailRepository = ObligationDetailRepository
    End Sub

    Public Function GetObligationDetailByObligationId(ObligationId As Integer) As List(Of ObligationDetail) Implements IObligationDetailAdminService.GetObligationDetailByObligationId
        Try
            Return _ObligationDetailRepository.GetObligationDetailByObligationId(ObligationId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of ObligationDetail)
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _ObligationDetailRepository = Nothing
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
