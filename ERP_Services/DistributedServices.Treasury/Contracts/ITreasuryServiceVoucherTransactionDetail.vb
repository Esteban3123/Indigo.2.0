'***********************************************************************
' Assembly         : DistributedServices.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 25-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()>
Public Interface ITreasuryServiceVoucherTransactionDetail

    ''' <summary>
    ''' Lista todos los detalles de la cabecera de un comprobante de egreso
    ''' </summary>
    ''' <param name="IdVoucherTransaction">The identifier voucher transaction.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function ListVoucherTransactionDetailByIdVoucherTransaction(ByVal IdVoucherTransaction As Integer) As ActionResult(Of List(Of VoucherTransactionDetails))

    ''' <summary>
    ''' obtiene un detalle de comprobante de egreso por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetVoucherTransactionDetailById(ByVal Id As Integer) As VoucherTransactionDetails

    ''' <summary>
    ''' Confirma si existen movimientos contables asociados a una cuenta bancaria de un proveedor
    ''' </summary>
    ''' <param name="supplierBankAccountId"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function HasAccountingMovementsForSupplierBankAccount(ByVal supplierBankAccountId As Integer) As Boolean

End Interface