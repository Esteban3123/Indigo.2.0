'***********************************************************************
' Assembly         : Domain.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 04-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface IDischargeBillRepository
    Inherits IRepository(Of DischargeBill)

    ''' <summary>
    ''' Obtiene o establece una factura de egresos por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Function GetDischargeBillById(ByVal Id As Integer) As DischargeBill

    ''' <summary>
    ''' Obtiene una factura de egreso por id del detalle del comprobante de egreso
    ''' </summary>
    ''' <param name="IdVoucherTransactionD">The identifier voucher transaction d.</param>
    ''' <returns></returns>
    Function ListDischargeBillByIdVoucherTransactionD(ByVal IdVoucherTransactionD As Integer) As List(Of DischargeBill)

    ''' <summary>
    ''' Obtiene una factura de egreso por id de la cuota de la factura
    ''' </summary>
    ''' <param name="IdAccountPayableShare">The identifier account payable share.</param>
    ''' <returns></returns>
    Function GetDischargeBillByIdAccountPayableShare(ByVal IdAccountPayableShare As Integer) As DischargeBill

End Interface
