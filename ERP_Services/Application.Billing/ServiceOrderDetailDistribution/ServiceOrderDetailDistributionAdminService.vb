'***********************************************************************
' Assembly         : Application.Billing
' Author           : Carlos Ernesto Cordoba
' Created          : 02-04-2014
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
Imports System.Data.Entity.Core
Imports System.Transactions
Imports Infrastructure.CrossCutting.Resources
Imports Domain.Crystal
Imports System.Text

#End Region

Public Class ServiceOrderDetailDistributionAdminService
    Implements IServiceOrderDetailDistributionAdminService

    Private _serviceOrderDetailDistributionRepository As IServiceOrderDetailDistributionRepository


    Public Sub New(serviceOrderDetailDistributionRepository As IServiceOrderDetailDistributionRepository)
        If serviceOrderDetailDistributionRepository Is Nothing Then
            Throw New ArgumentNullException("serviceOrderDetailDistributionRepository")
        End If
        _serviceOrderDetailDistributionRepository = serviceOrderDetailDistributionRepository
    End Sub
    Public Function GetServiceOrderDetailDistributionByServideOrderDetailId(ServiceOrderDetailId As Integer) As List(Of ServiceOrderDetailDistribution) Implements IServiceOrderDetailDistributionAdminService.GetServiceOrderDetailDistributionByServideOrderDetailId
        Try
            Return _serviceOrderDetailDistributionRepository.GetServiceOrderDetailDistributionByServideOrderDetailId(ServiceOrderDetailId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of ServiceOrderDetailDistribution)
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: elimine el estado administrado (objetos administrados).
            End If
            _serviceOrderDetailDistributionRepository = Nothing
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
