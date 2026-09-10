'***********************************************************************
' Assembly         : Application.Glosas
' Author           : RafaelPatiño
' Created          : 09-04-2013
'
' Last Modified By : RafaelPatiño
' Last Modified On : 2013-09-04
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface IPartialPaymentsCAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtener un oficio de pagos parciales por medio del consecutivo
    ''' </summary>
    ''' <param name="consecutive">consecutivo del oficio</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPartialPaymentsC(ByVal consecutive As String, ByVal audit As AuditMessage) As ActionResult(Of PartialPaymentsC)

    ''' <summary>
    ''' Confirmar un oficio de pagos parciales
    ''' </summary>
    ''' <param name="PartialPaymentsC"></param>
    ''' <param name="IndigoSessionValues"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ConfirmPartialPaymentsC(ByVal PartialPaymentsC As PartialPaymentsC, IndigoSessionValues As SessionValues) As ActionResult(Of PartialPaymentsC)

    ''' <summary>
    ''' guardar un oficio de radicacion de cuentas
    ''' </summary>
    ''' <param name="PartialPaymentsC">objeto radicacion de cuentas</param>
    ''' <param name="audit">mensaje de auditoria</param>
    ''' <returns>valor de guardado</returns>
    Function SavePartialPaymentsC(ByVal PartialPaymentsC As PartialPaymentsC, ByVal audit As AuditMessage) As ActionResult(Of PartialPaymentsC)

    ''' <summary>
    ''' Funcion para Anular un oficio
    ''' </summary>
    ''' <param name="PartialPaymentsC"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function InvalidatePaymentsC(PartialPaymentsC As PartialPaymentsC, audit As AuditMessage) As ActionResult(Of PartialPaymentsC)

    ''' <summary>
    ''' Metodo para Guardar y Confirmar un pago parcial generado desde notas de tesoreria, esto cuando esta en modo NATIVO
    ''' </summary>
    ''' <param name="PartialPaymentsC">Objeto de Pago Parcial - Cabecera y detalle (Lista de Facturas) del pago</param>
    ''' <param name="IndigoSessionValues">Variable de Sesión</param>
    ''' <returns>ActionResult</returns>
    ''' <remarks></remarks>
    Function SaveAndConfirmGlossPaymentsC(PartialPaymentsC As PartialPaymentsC, IndigoSessionValues As SessionValues) As ActionResult(Of String)

End Interface
