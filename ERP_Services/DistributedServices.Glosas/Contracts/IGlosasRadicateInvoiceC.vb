Imports System.ServiceModel
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()> _
Public Interface IGlosasRadicateInvoiceC

    ''' <summary>
    ''' Obtener un oficio de facturas radicadas por el id sin agregados
    ''' </summary>
    <OperationContract>
    Function GetInvocieRadicateByIdSimple(ByVal Id As Integer, session As SessionValues) As RadicateInvoiceC
    ''' <summary>
    ''' Listar Oficio de Factura Radicadas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract>
    Function ListInvocieRadicate(ByVal session As SessionValues) As List(Of RadicateInvoiceC)

    ''' <summary>
    ''' Obtener un oficio de facturas radicadas por medio del consecutivo
    ''' </summary>
    ''' <param name="consecutive">consecutivo del oficio</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract>
    Function GetInvocieRadicate(ByVal consecutive As String, ByVal session As SessionValues) As RadicateInvoiceC

    ''' <summary>
    ''' guardar un oficio de radicacion de cuentas
    ''' </summary>
    ''' <param name="RadicateInvoiceC">objeto radicacion de cuentas</param>
    ''' <param name="session">mensaje de auditoria</param>
    ''' <returns>valor de guardado</returns>
    <OperationContract>
    Function SaveRadicateInvoiveC(ByVal RadicateInvoiceC As RadicateInvoiceC, ByVal session As SessionValues) As ActionResult(Of RadicateInvoiceC)

    ''' <summary>
    ''' Funcion para guardar y confirmar
    ''' </summary>
    ''' <param name="ObjRadicateInvoiceC"></param>
    ''' <param name="idSequence"></param>
    ''' <param name="IndigoSessionValues"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract>
    Function SaveAndConfirmRadicateC(ObjRadicateInvoiceC As RadicateInvoiceC, idSequence As Integer, IndigoSessionValues As SessionValues) As ActionResult(Of RadicateInvoiceC)

    ''' <summary>
    ''' Elimina  un oficio de radicacion de cuentas
    ''' </summary>
    ''' <param name="RadicateInvoiceC">objeto radicacion de cuentas</param>
    ''' <returns></returns>
    <OperationContract>
    Function DeleteSaveRadicateInvoiveC(ByVal RadicateInvoiceC As RadicateInvoiceC, ByVal session As SessionValues) As ActionResult

    ''' <summary>
    ''' Confirmar un oficio de radicado
    ''' </summary>
    ''' <param name="InvoiceRadicateC"></param>
    ''' <param name="IndigoSessionValues"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract>
    Function ConfirmInvoiceRadicateC(InvoiceRadicateC As RadicateInvoiceC, idSequence As Integer, IndigoSessionValues As SessionValues) As ActionResult


    ''' <summary>
    ''' Actualizar fecha de confirmacion de cartera
    ''' </summary>
    ''' <param name="NumberRadicate"></param>
    ''' <param name="NewDate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract>
    Function UpdateDateConfirm(NumberRadicate As String, NewDate As Date, IndigoSessionValues As SessionValues) As actionresult

    ''' <summary>
    ''' Obtener la trazabilidad de una factura
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract>
    Function SP_InvoiceTraceability(container As String, invoice As String, IndigoSessionValues As SessionValues) As SP_InvoiceTraceability_Result

    ''' <summary>
    ''' ''' Obtener la trazabilidad de una factura
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract>
    Function SP_InvoiceTraceabilityConciliation(invoiceNumber As String, IndigoSessionValues As SessionValues) As List(Of SP_InvoiceTraceabilityConciliation_Result)

    ''' <summary>
    ''' ''' Obtener la trazabilidad de una factura
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract>
    Function SP_InvoiceTraceabilityDevolution(invoiceNumber As String, IndigoSessionValues As SessionValues) As List(Of SP_InvoiceTraceabilityDevolution_Result)

    ''' <summary>
    ''' ''' Obtener la trazabilidad de una factura
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract>
    Function SP_InvoiceTraceabilityRadication(invoice As String, IndigoSessionValues As SessionValues) As List(Of SP_InvoiceTraceabilityRadication_Result)

    ''' <summary>
    ''' Carga info para la generación del reporte de trazabilidad
    ''' </summary>
    ''' <param name="container"></param>
    ''' <param name="invoice"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract>
    Function dtsTrazabilityReports(container As String, invoice As String, session As SessionValues) As DataSet

End Interface
