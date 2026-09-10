'***********************************************************************
' Assembly         : DistributedServices.Treasury
' Author           : Diego Andrés Roldán lozano
' Created          : 25-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Application.Treasury
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Microsoft.Practices.Unity

Partial Class TreasuryService

    ''' <summary>
    ''' obtiene un detalle de comprobante de egreso por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetVoucherTransactionDetailById(Id As Integer) As Domain.Entities.VoucherTransactionDetails Implements ITreasuryServiceVoucherTransactionDetail.GetVoucherTransactionDetailById
        Using service As IVoucherTransactionDetailAdminService = Container.Current.Resolve(Of IVoucherTransactionDetailAdminService)()
            Return service.GetVoucherTransactionDetailById(Id)
        End Using
        'Return Me._vouDetailAdminService.GetVoucherTransactionDetailById(Id)
    End Function

    ''' <summary>
    ''' Lista todos los detalles de la cabecera de un comprobante de egreso
    ''' </summary>
    ''' <param name="IdVoucherTransaction">The identifier voucher transaction.</param>
    ''' <returns></returns>
    Public Function ListVoucherTransactionDetailByIdVoucherTransaction(IdVoucherTransaction As Integer) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.VoucherTransactionDetails)) Implements ITreasuryServiceVoucherTransactionDetail.ListVoucherTransactionDetailByIdVoucherTransaction
        Using service As IVoucherTransactionDetailAdminService = Container.Current.Resolve(Of IVoucherTransactionDetailAdminService)()
            Return service.ListVoucherTransactionDetailByIdVoucherTransaction(IdVoucherTransaction)
        End Using
        'Return Me._vouDetailAdminService.ListVoucherTransactionDetailByIdVoucherTransaction(IdVoucherTransaction)
    End Function

    ''' <summary>
    ''' Confirma si existen movimientos contables asociados a una cuenta bancaria de un proveedor
    ''' </summary>
    ''' <param name="supplierBankAccountId"></param>
    ''' <returns></returns>
    Public Function HasAccountingMovementsForSupplierBankAccount(supplierBankAccountId As Integer) As Boolean Implements ITreasuryServiceVoucherTransactionDetail.HasAccountingMovementsForSupplierBankAccount
        Using service As IVoucherTransactionDetailAdminService = Container.Current.Resolve(Of IVoucherTransactionDetailAdminService)()
            Return service.HasAccountingMovementsForSupplierBankAccount(supplierBankAccountId)
        End Using
    End Function
End Class