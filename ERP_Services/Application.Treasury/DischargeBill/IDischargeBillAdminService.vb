'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 04-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IDischargeBillAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda una factura de egreso
    ''' </summary>
    ''' <param name="dischargeBill">The discharge bill.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveDischargeBill(ByVal dischargeBill As DischargeBill, ByVal audit As AuditMessage) As ActionResult(Of DischargeBill)

    ''' <summary>
    ''' Elimina una factura de egreso
    ''' </summary>
    ''' <param name="dischargeBill">The discharge bill.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteDischargeBill(ByVal dischargeBill As DischargeBill, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene o establece una factura de egresos por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Function GetDischargeBillById(ByVal Id As Integer, ByVal audit As AuditMessage) As DischargeBill

    ''' <summary>
    ''' Obtiene una factura de egreso por id del detalle del comprobante de egreso
    ''' </summary>
    ''' <param name="IdVoucherTransactionD">The identifier voucher transaction d.</param>
    ''' <returns></returns>
    Function ListDischargeBillByIdVoucherTransactionD(ByVal IdVoucherTransactionD As Integer, ByVal audit As AuditMessage) As ActionResult(Of List(Of DischargeBill))

    ''' <summary>
    ''' Obtiene una factura de egreso por id de la cuota de la factura
    ''' </summary>
    ''' <param name="IdAccountPayableShare">The identifier account payable share.</param>
    ''' <returns></returns>
    Function GetDischargeBillByIdAccountPayableShare(ByVal IdAccountPayableShare As Integer, ByVal audit As AuditMessage) As ActionResult(Of DischargeBill)

End Interface

