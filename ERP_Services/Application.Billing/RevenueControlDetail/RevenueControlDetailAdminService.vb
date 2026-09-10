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

Public Class RevenueControlDetailAdminService
    Implements IRevenueControlDetailAdminService

    Private _revenueControlDetailRepository As IRevenueControlDetailRepository

    Public Sub New(revenueControlDetailRepository As IRevenueControlDetailRepository)
        If revenueControlDetailRepository Is Nothing Then
            Throw New ArgumentNullException("revenueControlDetailRepository")
        End If
        _revenueControlDetailRepository = revenueControlDetailRepository
    End Sub

    ''' <summary>
    ''' obtiene un folio por id del detalle de la orden de servicio
    ''' </summary>
    ''' <param name="serviceOrderDetailId"></param>
    ''' <returns></returns>
    Public Function GetRevenueControlDetailByServiceOrderDetailId(serviceOrderDetailId As Integer) As RevenueControlDetail Implements IRevenueControlDetailAdminService.GetRevenueControlDetailByServiceOrderDetailId
        Try
            Return _revenueControlDetailRepository.GetRevenueControlDetailByServiceOrderDetailId(serviceOrderDetailId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New RevenueControlDetail
        End Try
    End Function

    ''' <summary>
    ''' obtiene el modo de impresión de contrato por el id del detalle del folio
    ''' </summary>
    ''' <param name="idRevenueControl"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPrintgModeByIdRevenueControl(idRevenueControl As Integer) As Byte Implements IRevenueControlDetailAdminService.GetPrintgModeByIdRevenueControl
        Try
            Return _revenueControlDetailRepository.GetPrintgModeByIdRevenueControl(idRevenueControl)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return 0
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
            _revenueControlDetailRepository = Nothing
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
