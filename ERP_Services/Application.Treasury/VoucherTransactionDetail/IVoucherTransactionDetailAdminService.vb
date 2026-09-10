'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 13-06-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IVoucherTransactionDetailAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista todos los detalles de la cabecera de un comprobante de egreso
    ''' </summary>
    ''' <param name="IdVoucherTransaction">The identifier voucher transaction.</param>
    ''' <returns></returns>
    Function ListVoucherTransactionDetailByIdVoucherTransaction(ByVal IdVoucherTransaction As Integer) As ActionResult(Of List(Of VoucherTransactionDetails))

    ''' <summary>
    ''' obtiene un detalle de comprobante de egreso por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Function GetVoucherTransactionDetailById(ByVal Id As Integer) As VoucherTransactionDetails

    ''' <summary>
    ''' Verifica si existe al menos un movimiento contable asociado a una cuenta bancaria de un proveedor
    ''' </summary>
    ''' <param name="supplierBankAccountId"></param>
    ''' <returns></returns>
    Function HasAccountingMovementsForSupplierBankAccount(ByVal supplierBankAccountId As Integer) As Boolean
End Interface