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

Public Class CrossingAccountDetailCxPAdminService
    Implements ICrossingAccountDetailCxPAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de cruce de cuentas detalle cxp
    ''' </summary>
    Private _crossingAccountDetailCxPRepository As ICrossingAccountDetailCxPRepository

#End Region

#Region "Methods"
    Public Sub New(ByVal crossingAccountDetailCxPRepository As ICrossingAccountDetailCxPRepository)
        If crossingAccountDetailCxPRepository Is Nothing Then
            Throw New ArgumentNullException("crossingAccountDetailCxPRepository")
        End If
        _crossingAccountDetailCxPRepository = crossingAccountDetailCxPRepository
    End Sub

    ''' <summary>
    ''' Obtiene el detalle del cruce de cuentas de CxP por id
    ''' </summary>
    Public Function GetCrossingAccountDetailCxPById(Id As Integer, Optional tracking As Boolean = False) As CrossingAccountDetailCxP Implements ICrossingAccountDetailCxPAdminService.GetCrossingAccountDetailCxPById
        Try
            Return _crossingAccountDetailCxPRepository.GetCrossingAccountDetailCxPById(Id, tracking)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Lista los detalles de cruces de cuentas de CxP por el id de cruce de cuenta
    ''' </summary>
    Public Function ListCrossingAccountDetailCxPByCrossingAccountId(crossingAccountId As Integer, Optional tracking As Boolean = False) As List(Of CrossingAccountDetailCxP) Implements ICrossingAccountDetailCxPAdminService.ListCrossingAccountDetailCxPByCrossingAccountId
        Try
            Return _crossingAccountDetailCxPRepository.ListCrossingAccountDetailCxPByCrossingAccountId(crossingAccountId, tracking)
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
            _crossingAccountDetailCxPRepository = Nothing
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