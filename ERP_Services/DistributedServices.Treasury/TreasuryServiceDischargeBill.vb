'***********************************************************************
' Assembly         : DistributedServices.Treasury
' Author           : Diego Andrés Roldán lozano
' Created          : 04-07-2014
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
    ''' Elimina una factura de egreso
    ''' </summary>
    ''' <param name="dischargeBill">The discharge bill.</param>
    ''' <returns></returns>
    Public Function DeleteDischargeBill(dischargeBill As Domain.Entities.DischargeBill, audit As AuditMessage) As ActionResult Implements ITreasuryServiceDischargeBill.DeleteDischargeBill
        Using service As IDischargeBillAdminService = Container.Current.Resolve(Of IDischargeBillAdminService)()
            Return service.DeleteDischargeBill(dischargeBill, audit)
        End Using
        'Return Me._dischargeAdminService.DeleteDischargeBill(dischargeBill, audit)
    End Function

    ''' <summary>
    ''' Obtiene o establece una factura de egresos por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetDischargeBillById(Id As Integer, audit As AuditMessage) As Domain.Entities.DischargeBill Implements ITreasuryServiceDischargeBill.GetDischargeBillById
        Using service As IDischargeBillAdminService = Container.Current.Resolve(Of IDischargeBillAdminService)()
            Return service.GetDischargeBillById(Id, audit)
        End Using
        'Return Me._dischargeAdminService.GetDischargeBillById(Id, audit)
    End Function

    ''' <summary>
    ''' Gets the discharge bill by identifier account payable share1.
    ''' </summary>
    ''' <param name="IdAccountPayableShare">The identifier account payable share.</param>
    ''' <returns></returns>
    Public Function GetDischargeBillByIdAccountPayableShare(IdAccountPayableShare As Integer, audit As AuditMessage) As ActionResult(Of DischargeBill) Implements ITreasuryServiceDischargeBill.GetDischargeBillByIdAccountPayableShare
        Using service As IDischargeBillAdminService = Container.Current.Resolve(Of IDischargeBillAdminService)()
            Return service.GetDischargeBillByIdAccountPayableShare(IdAccountPayableShare, audit)
        End Using
        'Return Me._dischargeAdminService.GetDischargeBillByIdAccountPayableShare(IdAccountPayableShare, audit)
    End Function

    ''' <summary>
    ''' Obtiene una factura de egreso por id del detalle del comprobante de egreso
    ''' </summary>
    ''' <param name="IdVoucherTransactionD">The identifier voucher transaction d.</param>
    ''' <returns></returns>
    Public Function ListDischargeBillByIdVoucherTransactionD(IdVoucherTransactionD As Integer, audit As AuditMessage) As ActionResult(Of List(Of DischargeBill)) Implements ITreasuryServiceDischargeBill.ListDischargeBillByIdVoucherTransactionD
        Using service As IDischargeBillAdminService = Container.Current.Resolve(Of IDischargeBillAdminService)()
            Return service.ListDischargeBillByIdVoucherTransactionD(IdVoucherTransactionD, audit)
        End Using
        'Return Me._dischargeAdminService.ListDischargeBillByIdVoucherTransactionD(IdVoucherTransactionD, audit)
    End Function

    ''' <summary>
    ''' Guarda una factura de egreso
    ''' </summary>
    ''' <param name="dischargeBill">The discharge bill.</param>
    ''' <returns></returns>
    Public Function SaveDischargeBill(dischargeBill As Domain.Entities.DischargeBill, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.DischargeBill) Implements ITreasuryServiceDischargeBill.SaveDischargeBill
        Using service As IDischargeBillAdminService = Container.Current.Resolve(Of IDischargeBillAdminService)()
            Return service.SaveDischargeBill(dischargeBill, audit)
        End Using
        'Return Me._dischargeAdminService.SaveDischargeBill(dischargeBill, audit)
    End Function

End Class
