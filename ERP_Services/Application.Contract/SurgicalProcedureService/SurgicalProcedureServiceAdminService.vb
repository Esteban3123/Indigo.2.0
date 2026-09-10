'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 30/09/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Core
Imports Infrastructure.CrossCutting.Resources
Imports System.Transactions

Public Class SurgicalProcedureServiceAdminService
    Implements ISurgicalProcedureServiceAdminService

    Private _surgicalProcedureService As ISurgicalProcedureServiceRepository

    Public Sub New(surgicalProcedureService As ISurgicalProcedureServiceRepository)
        If surgicalProcedureService Is Nothing Then
            Throw New ArgumentNullException("surgicalProcedureService")
        End If
        _surgicalProcedureService = surgicalProcedureService
    End Sub

    ''' <summary>
    ''' obtiene procedimiento quirurgicos del servcio IPS
    ''' </summary>
    ''' <param name="idIPSService"></param>
    ''' <returns></returns>
    Public Function GetSurgicalProcedureServiceByIPSServiceId(idIPSService As Integer) As List(Of SurgicalProcedureService) Implements ISurgicalProcedureServiceAdminService.GetSurgicalProcedureServiceByIPSServiceId
        Try
            Return _surgicalProcedureService.GetSurgicalProcedureServiceByIPSServiceId(idIPSService)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of SurgicalProcedureService)
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _surgicalProcedureService = Nothing
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
