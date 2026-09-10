'***********************************************************************
' Assembly         : Application.Glosas
' Author           : RafaelPatiño
' Created          : 11-04-2014
'
' Last Modified By : 
' Last Modified On : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface IRadicateInvoiceAdminService
    Inherits IDisposable
    ''' <summary>
    ''' Obtener un oficio de facturas radicadas por el id sin agregados
    ''' </summary>
    Function GetInvocieRadicateByIdSimple(ByVal Id As Integer) As RadicateInvoiceC
    ''' <summary>
    ''' Listar Oficio de Factura Radicadas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListInvocieRadicate(ByVal audit As AuditMessage) As List(Of RadicateInvoiceC)

    ''' <summary>
    ''' Obtener un oficio de facturas radicadas por medio del consecutivo
    ''' </summary>
    ''' <param name="consecutive">consecutivo del oficio</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetInvocieRadicate(ByVal consecutive As String, ByVal audit As AuditMessage) As RadicateInvoiceC

    ''' <summary>
    ''' guardar un oficio de radicacion de cuentas
    ''' </summary>
    ''' <param name="RadicateInvoiceC">objeto radicacion de cuentas</param>
    ''' <param name="IndigoSessionValues">mensaje de auditoria</param>
    ''' <returns>valor de guardado</returns>
    Function SaveRadicateInvoiveC(ByVal RadicateInvoiceC As RadicateInvoiceC, IndigoSessionValues As SessionValues) As ActionResult(Of RadicateInvoiceC)

    ''' <summary>
    ''' Funcion para guardar y confirmar
    ''' </summary>
    ''' <param name="ObjRadicateInvoiceC"></param>
    ''' <param name="idSequence"></param>
    ''' <param name="IndigoSessionValues"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveAndConfirm(ObjRadicateInvoiceC As RadicateInvoiceC, idSequence As Integer, IndigoSessionValues As SessionValues) As ActionResult(Of RadicateInvoiceC)

    ''' <summary>
    ''' Elimina  un oficio de radicacion de cuentas
    ''' </summary>
    ''' <param name="RadicateInvoiceC">objeto radicacion de cuentas</param>
    ''' <returns></returns>
    Function DeleteSaveRadicateInvoiveC(ByVal RadicateInvoiceC As RadicateInvoiceC, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Función para validar y agregar facturas desde el sp
    ''' </summary>
    ''' <param name="ListInvoices">Lista de facturas</param>
    ''' <param name="Nit">Numero de Nit</param>
    ''' <param name="container">nombre Contenedor</param>
    ''' <returns>Lista de Factura </returns>
    Function ValidateListInvoiceRadicateDSp(ListInvoices As List(Of String), Nit As String, container As String, IndigoCompany As String, session As SessionValues) As ActionResult(Of List(Of RadicateInvoiceD))

    ''' <summary>
    ''' Confirmar un oficio de radicado
    ''' </summary>
    ''' <param name="InvoiceRadicateC"></param>
    ''' <param name="IndigoSessionValues"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ConfirmInvoiceRadicateC(InvoiceRadicateC As RadicateInvoiceC, idSequence As Integer, IndigoSessionValues As SessionValues) As ActionResult

    ''' <summary>
    ''' Actualizar fecha de confirmacion de cartera
    ''' </summary>
    ''' <param name="NumberRadicate"></param>
    ''' <param name="NewDate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function UpdateDateConfirm(NumberRadicate As String, NewDate As Date, IndigoSessionValues As SessionValues) As ActionResult

    ''' <summary>
    ''' Obtener la trazabilidad de una factura
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_InvoiceTraceability(container As String, invoice As String, IndigoSessionValues As SessionValues) As SP_InvoiceTraceability_Result

    ''' <summary>
    ''' ''' Obtener la trazabilidad de una factura
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_InvoiceTraceabilityConciliation(invoiceNumber As String, IndigoSessionValues As SessionValues) As List(Of SP_InvoiceTraceabilityConciliation_Result)

    ''' <summary>
    ''' ''' Obtener la trazabilidad de una factura
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_InvoiceTraceabilityDevolution(invoiceNumber As String, IndigoSessionValues As SessionValues) As List(Of SP_InvoiceTraceabilityDevolution_Result)

    ''' <summary>
    ''' ''' Obtener la trazabilidad de una factura
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_InvoiceTraceabilityRadication(invoice As String, IndigoSessionValues As SessionValues) As List(Of SP_InvoiceTraceabilityRadication_Result)


    ''' <summary>
    ''' Carga info para la generación del reporte de trazabilidad
    ''' </summary>
    ''' <param name="container"></param>
    ''' <param name="invoice"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function dtsTrazabilityReports(container As String, invoice As String, session As SessionValues) As DataSet



End Interface
