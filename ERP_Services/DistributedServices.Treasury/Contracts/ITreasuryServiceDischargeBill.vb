'***********************************************************************
' Assembly         : DistributedServices.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 04-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()>
Public Interface ITreasuryServiceDischargeBill

    ''' <summary>
    ''' Guarda una factura de egreso
    ''' </summary>
    ''' <param name="dischargeBill">The discharge bill.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveDischargeBill(dischargeBill As Domain.Entities.DischargeBill, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.DischargeBill)

    ''' <summary>
    ''' Elimina una factura de egreso
    ''' </summary>
    ''' <param name="dischargeBill">The discharge bill.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteDischargeBill(dischargeBill As Domain.Entities.DischargeBill, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene o establece una factura de egresos por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetDischargeBillById(Id As Integer, audit As AuditMessage) As Domain.Entities.DischargeBill

    ''' <summary>
    ''' Obtiene una factura de egreso por id del detalle del comprobante de egreso
    ''' </summary>
    ''' <param name="IdVoucherTransactionD">The identifier voucher transaction d.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function ListDischargeBillByIdVoucherTransactionD(IdVoucherTransactionD As Integer, audit As AuditMessage) As ActionResult(Of List(Of DischargeBill))

    ''' <summary>
    ''' Obtiene una factura de egreso por id de la cuota de la factura
    ''' </summary>
    ''' <param name="IdAccountPayableShare">The identifier account payable share.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetDischargeBillByIdAccountPayableShare(IdAccountPayableShare As Integer, audit As AuditMessage) As ActionResult(Of DischargeBill)

End Interface

