'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 02-04-2014
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
Imports Infrastructure.CrossCutting.Resources

#End Region

Public Class VoucherTransactionDetailAdminService
    Implements IVoucherTransactionDetailAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de detalle de comprobante egreso
    ''' </summary>
    Private _vouDetailRepository As IVoucherTransactionDetailRepository

#End Region

#Region "Methods"

    Public Sub New(ByVal vouDetailRepository As IVoucherTransactionDetailRepository)
        If vouDetailRepository Is Nothing Then
            Throw New ArgumentNullException("vouDetailRepository")
        End If
        _vouDetailRepository = vouDetailRepository
    End Sub

    ''' <summary>
    ''' obtiene un detalle de comprobante de egreso por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetVoucherTransactionDetailById(Id As Integer) As VoucherTransactionDetails Implements IVoucherTransactionDetailAdminService.GetVoucherTransactionDetailById
        Try
            Return _vouDetailRepository.GetVoucherTransactionDetailById(Id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Lista todos los detalles de la cabecera de un comprobante de egreso
    ''' </summary>
    ''' <param name="IdVoucherTransaction">The identifier voucher transaction.</param>
    ''' <returns></returns>
    Public Function ListVoucherTransactionDetailByIdVoucherTransaction(IdVoucherTransaction As Integer) As ActionResult(Of List(Of VoucherTransactionDetails)) Implements IVoucherTransactionDetailAdminService.ListVoucherTransactionDetailByIdVoucherTransaction
        Try
            Dim result As List(Of VoucherTransactionDetails) = _vouDetailRepository.ListVoucherTransactionDetailByIdVoucherTransaction(IdVoucherTransaction)
            Return New ActionResult(Of List(Of VoucherTransactionDetails)) With {.StateResult = True, .ObjectEmbbeded = result}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of VoucherTransactionDetails)) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    ''' <summary>
    ''' Confirma si existe o no al menos un movimiento contable asociado a una cuenta bancaria de un proveedor
    ''' </summary>
    ''' <param name="supplierBankAccountId"></param>
    ''' <returns></returns>
    Public Function HasAccountingMovementsForSupplierBankAccount(supplierBankAccountId As Integer) As Boolean Implements IVoucherTransactionDetailAdminService.HasAccountingMovementsForSupplierBankAccount
        Dim result As Boolean = _vouDetailRepository.GetVoucherTransactionDetailsBySupplierBankAccountId(supplierBankAccountId).Any()
        Return result
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _vouDetailRepository = Nothing
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