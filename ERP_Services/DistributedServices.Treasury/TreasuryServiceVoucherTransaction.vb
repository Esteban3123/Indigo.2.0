'***********************************************************************
' Assembly         : DistributedServices.Treasury
' Author           : Diego Andrés Roldán lozano
' Created          : 02-04-2014
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
    Implements ITreasuryServiceVoucherTransaction

    ''' <summary>
    ''' Obtiene un comprobante de egreso por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetVoucherTransaction(code As String, audit As AuditMessage) As ActionResult(Of VoucherTransaction) Implements ITreasuryServiceVoucherTransaction.GetVoucherTransaction
        Using service As IVoucherTransactionAdminService = Container.Current.Resolve(Of IVoucherTransactionAdminService)()
            Return service.GetVoucherTransaction(code, audit)
        End Using
        'Return Me._voucherTransactionAdminService.GetVoucherTransaction(code, audit)
    End Function

    ''' <summary>
    ''' Guarda un comprobante de egreso
    ''' </summary>
    ''' <param name="voucherTransaction"></param>
    ''' <returns></returns>
    Public Function SaveVoucherTransaction(voucherTransaction As Domain.Entities.VoucherTransaction, withConfirm As Boolean, idSequence As Int64, sequenceC As Domain.Entities.TreasurySequence, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.VoucherTransaction) Implements ITreasuryServiceVoucherTransaction.SaveVoucherTransaction
        Using service As IVoucherTransactionAdminService = Container.Current.Resolve(Of IVoucherTransactionAdminService)()
            Return service.SaveVoucherTransaction(voucherTransaction, audit, withConfirm, idSequence, sequenceC)
        End Using
        'Return Me._voucherTransactionAdminService.SaveVoucherTransaction(voucherTransaction, audit, withConfirm, idSequence, sequenceC)
    End Function

    ''' <summary>
    ''' Confirma un comprobante de egreso
    ''' </summary>
    ''' <returns></returns>
    Public Function ConfirmVoucherTransaction(idVoucherTransaction As Integer, idSequence As Int64, audit As AuditMessage) As ActionResult(Of String) Implements ITreasuryServiceVoucherTransaction.ConfirmVoucherTransaction
        Using service As IVoucherTransactionAdminService = Container.Current.Resolve(Of IVoucherTransactionAdminService)()
            Return service.ConfirmVoucherTransaction(idVoucherTransaction, audit, Nothing, idSequence)
        End Using
        'Return Me._voucherTransactionAdminService.ConfirmVoucherTransaction(idVoucherTransaction, audit, Nothing, idSequence)
    End Function

    ''' <summary>
    ''' Obtiene un avance de tesoreria por Id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetTreasuryAdvanceById(Id As Integer) As Domain.Base.Entities.ActionResult(Of Domain.Entities.TreasuryAdvances) Implements ITreasuryServiceVoucherTransaction.GetTreasuryAdvanceById
        Using service As IVoucherTransactionAdminService = Container.Current.Resolve(Of IVoucherTransactionAdminService)()
            Return service.GetTreasuryAdvanceById(Id)
        End Using
        'Return Me._voucherTransactionAdminService.GetTreasuryAdvanceById(Id)
    End Function

    ''' <summary>
    ''' Obtiene un avance de tesoreria por IdVoucherTransactionDetail
    ''' </summary>
    ''' <param name="IdVoucherTransactionDetail">The identifier voucher transaction detail.</param>
    ''' <returns></returns>
    Public Function GetTreasuryAdvanceByIdVoucherTransactionDetail(IdVoucherTransactionDetail As Integer) As Domain.Base.Entities.ActionResult(Of Domain.Entities.TreasuryAdvances) Implements ITreasuryServiceVoucherTransaction.GetTreasuryAdvanceByIdVoucherTransactionDetail
        Using service As IVoucherTransactionAdminService = Container.Current.Resolve(Of IVoucherTransactionAdminService)()
            Return service.GetTreasuryAdvanceByIdVoucherTransactionDetail(IdVoucherTransactionDetail)
        End Using
        'Return Me._voucherTransactionAdminService.GetTreasuryAdvanceByIdVoucherTransactionDetail(IdVoucherTransactionDetail)
    End Function

    ''' <summary>
    ''' Obtiene un comprobante de egreso por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetVoucherTransactionById(Id As Integer) As ActionResult(Of VoucherTransaction) Implements ITreasuryServiceVoucherTransaction.GetVoucherTransactionById
        Using service As IVoucherTransactionAdminService = Container.Current.Resolve(Of IVoucherTransactionAdminService)()
            Return service.GetVoucherTransactionById(Id)
        End Using
        'Return Me._voucherTransactionAdminService.GetVoucherTransactionById(Id)
    End Function

    ''' <summary>
    ''' Obtiene un listado de comprobantes de egreso que esten dentro de un rango de fecha y que no esten reembolsados
    ''' </summary>
    ''' <param name="InitialDate"></param>
    ''' <param name="FinalDate"></param>
    ''' <param name="ExpenseType"></param>
    ''' <returns></returns>
    Public Function ListVoucherTransactionBetweenDateNotRefundExpenseType(CashRegisterId As Integer, InitialDate As Date, FinalDate As Date, ExpenseType As Byte, audit As AuditMessage) As ActionResult(Of List(Of VoucherTransaction)) Implements ITreasuryServiceVoucherTransaction.ListVoucherTransactionBetweenDateNotRefundExpenseType
        Using service As IVoucherTransactionAdminService = Container.Current.Resolve(Of IVoucherTransactionAdminService)()
            Return service.ListVoucherTransactionBetweenDateNotRefundExpenseType(CashRegisterId, InitialDate, FinalDate, ExpenseType, audit)
        End Using
        'Return Me._voucherTransactionAdminService.ListVoucherTransactionBetweenDateNotRefundExpenseType(CashRegisterId, InitialDate, FinalDate, ExpenseType, audit)
    End Function

    ''' <summary>
    ''' Obtiene un listado de comprobantes de egreso que esten dentro de un rango de fecha y que no esten reembolsados
    ''' </summary>
    ''' <param name="InitialDate"></param>
    ''' <param name="FinalDate"></param>
    ''' <param name="ExpenseType"></param>
    ''' <returns></returns>
    Public Function ListVoucherTransactionFinalDateNotRefundExpenseType(CashRegisterId As Integer, FinalDate As Date, ExpenseType As Byte, audit As AuditMessage) As ActionResult(Of List(Of VoucherTransaction)) Implements ITreasuryServiceVoucherTransaction.ListVoucherTransactionFinalDateNotRefundExpenseType
        Using service As IVoucherTransactionAdminService = Container.Current.Resolve(Of IVoucherTransactionAdminService)()
            Return service.ListVoucherTransactionFinalDateNotRefundExpenseType(CashRegisterId, FinalDate, ExpenseType, audit)
        End Using
        'Return Me._voucherTransactionAdminService.ListVoucherTransactionFinalDateNotRefundExpenseType(CashRegisterId, FinalDate, ExpenseType, audit)
    End Function

    ''' <summary>
    ''' Obtiene un comprobante de egreso por número de cheque
    ''' </summary>
    ''' <param name="checkNumber"></param>
    ''' <returns></returns>
    Public Function GetVoucherTransactionByCheckNumber(checkNumber As Long) As ActionResult(Of VoucherTransaction) Implements ITreasuryServiceVoucherTransaction.GetVoucherTransactionByCheckNumber
        Using service As IVoucherTransactionAdminService = Container.Current.Resolve(Of IVoucherTransactionAdminService)()
            Return service.GetVoucherTransactionByCheckNumber(checkNumber)
        End Using
        'Return Me._voucherTransactionAdminService.GetVoucherTransactionByCheckNumber(checkNumber)
    End Function

    ''' <summary>
    ''' Gets the account payable in note and advance.
    ''' </summary>
    ''' <param name="listAccountPayableShareId">The list account payable share identifier.</param>
    ''' <returns></returns>
    Public Function GetAccountPayableInNoteAndAdvance(listAccountPayableShareId As List(Of Integer), voucherTransactionCode As String) As ActionResult(Of List(Of String)) Implements ITreasuryServiceVoucherTransaction.GetAccountPayableInNoteAndAdvance
        Using service As IVoucherTransactionAdminService = Container.Current.Resolve(Of IVoucherTransactionAdminService)()
            Return service.GetAccountPayableInNoteAndAdvance(listAccountPayableShareId, voucherTransactionCode)
        End Using
        'Return Me._voucherTransactionAdminService.GetAccountPayableInNoteAndAdvance(listAccountPayableShareId, voucherTransactionCode)
    End Function

    Public Function GetCheckNumber(entitybanckAccountId As Integer, OperatingUnitId As Integer, UserCode As String) As SP_GetCheckNumber_Result Implements ITreasuryServiceVoucherTransaction.GetCheckNumber
        Using service As IVoucherTransactionAdminService = Container.Current.Resolve(Of IVoucherTransactionAdminService)()
            Return service.GetCheckNumber(entitybanckAccountId, OperatingUnitId, UserCode)
        End Using
        'Return _voucherTransactionAdminService.GetCheckNumber(entitybanckAccountId, OperatingUnitId, UserCode)
    End Function
End Class
