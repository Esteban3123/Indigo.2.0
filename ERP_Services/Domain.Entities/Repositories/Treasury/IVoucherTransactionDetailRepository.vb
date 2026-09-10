'***********************************************************************
' Assembly         : Domain.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 27-06-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface IVoucherTransactionDetailRepository
    Inherits IRepository(Of VoucherTransactionDetails)

    ''' <summary>
    ''' Lista todos los detalles de la cabecera de un comprobante de egreso
    ''' </summary>
    ''' <param name="IdVoucherTransaction">The identifier voucher transaction.</param>
    ''' <returns></returns>
    Function ListVoucherTransactionDetailByIdVoucherTransaction(ByVal IdVoucherTransaction As Integer) As List(Of VoucherTransactionDetails)

    ''' <summary>
    ''' obtiene un detalle de comprobante de egreso por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Function GetVoucherTransactionDetailById(ByVal Id As Integer) As VoucherTransactionDetails

    ''' <summary>
    ''' Obtiene todos los movimientos contables asociado a una cuenta bancaria de un proveedor
    ''' </summary>
    ''' <param name="supplierBankAccountId"></param>
    ''' <returns></returns>
    Function GetVoucherTransactionDetailsBySupplierBankAccountId(ByVal supplierBankAccountId As Integer) As List(Of VoucherTransactionDetails)

End Interface