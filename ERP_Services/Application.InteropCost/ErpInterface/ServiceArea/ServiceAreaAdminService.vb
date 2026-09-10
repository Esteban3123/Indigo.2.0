'***********************************************************************
' Assembly         : Application.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 15-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Core
Imports Infrastructure.CrossCutting.Resources
Imports Domain.InteropCost
Imports Domain.InteropCost.Entities

Public Class ServiceAreaAdminService
    Implements IServiceAreaAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de areas de servicio
    ''' </summary>
    Private _serviceAreaRepository As IServiceAreaRepository

#End Region

#Region "Methods"

    Public Sub New(ByVal serviceAreaRepository As IServiceAreaRepository)
        If serviceAreaRepository Is Nothing Then
            Throw New ArgumentNullException("serviceAreaRepository")
        End If
        _serviceAreaRepository = serviceAreaRepository
    End Sub

    ''' <summary>
    ''' Obtiene un area de servicio por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">code</exception>
    Public Function GetServiceArea(code As String) As ActionResult(Of Domain.InteropCost.Entities.GENARESER) Implements IServiceAreaAdminService.GetServiceArea
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        Try
            Dim serviceArea As GENARESER = Me._serviceAreaRepository.GetServiceArea(code.Trim())
            Return New ActionResult(Of GENARESER) With {.StateResult = True, .ObjectEmbbeded = serviceArea}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of GENARESER) With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un area de servicio por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">Id</exception>
    Public Function GetServiceAreaById(id As Integer) As ActionResult(Of Domain.InteropCost.Entities.GENARESER) Implements IServiceAreaAdminService.GetServiceAreaById
        If id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Try
            Dim serviceArea As GENARESER = Me._serviceAreaRepository.GetServiceAreaById(id)
            Return New ActionResult(Of GENARESER) With {.StateResult = True, .ObjectEmbbeded = serviceArea}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
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
            _serviceAreaRepository = Nothing
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