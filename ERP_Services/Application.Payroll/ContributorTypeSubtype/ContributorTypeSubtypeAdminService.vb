'***********************************************************************
' Assembly         : Application.Payroll
' Author           :
' Created          : 05-06-2026
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Domain.Payroll
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions

Public Class ContributorTypeSubtypeAdminService
    Implements IContributorTypeSubtypeAdminService

    Private _repository As IContributorTypeSubtypeRepository

    Public Sub New(ByVal repository As IContributorTypeSubtypeRepository)
        If repository Is Nothing Then
            Throw New ArgumentNullException("repository")
        End If
        _repository = repository
    End Sub

    ''' <summary>
    ''' Lista todas las combinaciones Tipo+Subtipo de cotizante
    ''' </summary>
    Public Function ListAllContributorTypeSubtype() As List(Of ContributorTypeSubtype) Implements IContributorTypeSubtypeAdminService.ListAllContributorTypeSubtype
        Try
            Return _repository.ListAllContributorTypeSubtype()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of ContributorTypeSubtype)()
        End Try
    End Function

    ''' <summary>
    ''' Lista los subtipos válidos para un tipo de cotizante
    ''' </summary>
    Public Function ListByContributorTypeId(contributorTypeId As Integer) As List(Of ContributorTypeSubtype) Implements IContributorTypeSubtypeAdminService.ListByContributorTypeId
        Try
            Return _repository.ListByContributorTypeId(contributorTypeId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of ContributorTypeSubtype)()
        End Try
    End Function

    ''' <summary>
    ''' Obtiene el Id del pivote dado tipo + subtipo
    ''' </summary>
    Public Function GetIdByTypeAndSubtype(contributorTypeId As Integer, contributorSubtypeId As Integer) As Integer Implements IContributorTypeSubtypeAdminService.GetIdByTypeAndSubtype
        Try
            Return _repository.GetIdByTypeAndSubtype(contributorTypeId, contributorSubtypeId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return 0
        End Try
    End Function

    ''' <summary>
    ''' Guarda una nueva combinacion Tipo+Subtipo de cotizante
    ''' </summary>
    Public Function SaveContributorTypeSubtype(entity As ContributorTypeSubtype) As Boolean Implements IContributorTypeSubtypeAdminService.SaveContributorTypeSubtype
        Try
            Return _repository.SaveContributorTypeSubtype(entity)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Elimina una combinacion Tipo+Subtipo de cotizante por Id
    ''' </summary>
    Public Function DeleteContributorTypeSubtype(id As Integer) As Boolean Implements IContributorTypeSubtypeAdminService.DeleteContributorTypeSubtype
        Try
            Return _repository.DeleteContributorTypeSubtype(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean

    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
            End If
            _repository = Nothing
        End If
        disposedValue = True
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
