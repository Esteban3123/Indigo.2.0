'***********************************************************************
' Assembly         : Application.Crystal
' Author           : Diego Roldán Lozano
' Created          : 22-06-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Payroll
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Domain.Crystal
Imports Domain.Crystal.Service
Imports System.Data.Entity.Infrastructure
Imports Application.Billing
Imports System.Data.Entity.Core
Imports System.Transactions
Imports System.Dynamic
Imports Infrastructure.CrossCutting.Resources
Imports Domain.Crystal.Entities
Imports Application.Base

#End Region

Public Class HCUNITHISAdminService
    Implements IHCUNITHISAdminService

#Region "Fields"
    Private _iHCUNITHISRepository As IHCUNITHISRepository
#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New(iHCUNITHISRepository As IHCUNITHISRepository)
        _iHCUNITHISRepository = iHCUNITHISRepository
    End Sub

#End Region

#Region "Methods"
    Public Function GetHCUNITHISByUFUCODIGO(ufucodigo As String) As List(Of HCUNITHIS) Implements IHCUNITHISAdminService.GetHCUNITHISByUFUCODIGO
        Try
            Return _iHCUNITHISRepository.GetHCUNITHISByUFUCODIGO(ufucodigo)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function GetHCUNITHISByUFUCODIGOWithFACMECONINS(ufucodigo As String) As HCUNITHIS Implements IHCUNITHISAdminService.GetHCUNITHISByUFUCODIGOWithFACMECONINS
        Try
            Return _iHCUNITHISRepository.GetHCUNITHISByUFUCODIGOWithFACMECONINS(ufucodigo)
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
            _iHCUNITHISRepository = Nothing
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