'***********************************************************************
' Assembly         : Application.Portfolio
' Author           : Faiber Julian Mora D.
' Created          : 07-10-2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Core
Imports Application.Portfolio

#End Region

Public Class CircularZeroThirtyAdminService
    Implements ICircularZeroThirtyAdminService

#Region "Variables"

    Private _circularZeroThirtyRepository As ICircularZeroThirtyRepository

#End Region

#Region "Builder"

    Public Sub New(ByVal circularZeroThirtyRepository As ICircularZeroThirtyRepository)
        If circularZeroThirtyRepository Is Nothing Then
            Throw New ArgumentException("circularZeroThirtyRepository Nothing")
        End If
        _circularZeroThirtyRepository = circularZeroThirtyRepository
    End Sub

    Public Function GenerateDocument030(year As Integer, trimester As Integer, audit As AuditMessage) As ActionResult(Of List(Of GenerateDocument030_Result)) Implements ICircularZeroThirtyAdminService.GenerateDocument030
        Try
            Dim result = _circularZeroThirtyRepository.GenerateDocument030(year, trimester, audit.CodeUser)
            Return New ActionResult(Of List(Of GenerateDocument030_Result)) With {.StateResult = True, .ObjectEmbbeded = result}
        Catch ex As Exception
            Return New ActionResult(Of List(Of GenerateDocument030_Result)) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function ListTrimesters() As Dictionary(Of Integer, String) Implements ICircularZeroThirtyAdminService.ListTrimesters
        Return _circularZeroThirtyRepository.ListTrimesters()
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _circularZeroThirtyRepository = Nothing
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
