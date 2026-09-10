'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 29-09-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Libraries"
Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Core
Imports System.Transactions
Imports Infrastructure.CrossCutting.Resources
#End Region

Public Class CrossingAccountDetailCxCAdminService
    Implements ICrossingAccountDetailCxCAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de cruce de cuentas detalle cxc
    ''' </summary>
    Private _crossingAccountDetailCxCRepository As ICrossingAccountDetailCxCRepository

#End Region

#Region "Methods"
    Public Sub New(ByVal crossingAccountDetailCxCRepository As ICrossingAccountDetailCxCRepository)
        If crossingAccountDetailCxCRepository Is Nothing Then
            Throw New ArgumentNullException("crossingAccountDetailCxCRepository")
        End If
        _crossingAccountDetailCxCRepository = crossingAccountDetailCxCRepository
    End Sub

    ''' <summary>
    ''' Obtiene el detalle del cruce de cuentas de CxC por id
    ''' </summary>
    Public Function GetCrossingAccountDetailCxCById(Id As Integer, Optional tracking As Boolean = False) As CrossingAccountDetailCxC Implements ICrossingAccountDetailCxCAdminService.GetCrossingAccountDetailCxCById
        Try
            Return _crossingAccountDetailCxCRepository.GetCrossingAccountDetailCxCById(Id, tracking)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Lista los detalles de cruces de cuentas de CxC por el id de cruce de cuenta
    ''' </summary>
    Public Function ListCrossingAccountDetailCxCByCrossingAccountId(crossingAccountId As Integer, Optional tracking As Boolean = False) As List(Of CrossingAccountDetailCxC) Implements ICrossingAccountDetailCxCAdminService.ListCrossingAccountDetailCxCByCrossingAccountId
        Try
            Return _crossingAccountDetailCxCRepository.ListCrossingAccountDetailCxCByCrossingAccountId(crossingAccountId, tracking)
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
            _crossingAccountDetailCxCRepository = Nothing
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
