'***********************************************************************
' Assembly         : Application.Glosas
' Author           : JuanDiegoDiaz
' Created          : 08-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface IMovementDevolutionsAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Funcion para Guardar un Movimiento de Devolución
    ''' </summary>
    ''' <param name="MovementDevolution">Movimineto Devolución</param>
    ''' <param name="audit"></param>
    Function SaveMovementDevolution(MovementDevolution As GlosaMovementDevolutions, audit As AuditMessage) As ActionResult(Of GlosaMovementDevolutions)

    ''' <summary>
    ''' Funcion para eliminar un Movimiento de Devolución
    ''' </summary>
    ''' <param name="MovementDevolution">Movimiento Devolución</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DeleteMovementDevolution(ByVal MovementDevolution As GlosaMovementDevolutions, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Lista todos los Movimientos de Devoluciones.
    ''' </summary>
    ''' <returns>Lista de Movimiento de Devoluciones</returns>
    Function ListAllMovementGlosaDevolutions() As List(Of GlosaMovementDevolutions)

    ''' <summary>
    ''' Obtiene un Movimiento Devolucion especifico.
    ''' </summary>
    ''' <param name="Invoice">Numero de factura</param>
    ''' <param name="IdDevolutionD">Id Devolución Detalle</param>
    ''' <returns>Objeto Movimiento Devolucion</returns>
    Function GetMovementDevolutionByInvoiceNumber(ByVal Invoice As String, IdDevolutionD As String) As GlosaMovementDevolutions

    ''' <summary>
    ''' Funcion para Confirmar un Movimiento de Devolución
    ''' </summary>
    ''' <param name="listDevolutionD">Movimineto Devolución</param>
    ''' <param name="IndigoSessionValues"></param>
    Function ConfirmDevolution(ByVal listDevolutionD As List(Of GlosaDevolutionsReceptionD), ByVal idSequence As Integer, ByVal Injustificate As Boolean, ByVal UserFreeInvoice As Boolean, IndigoSessionValues As SessionValues) As ActionResult

    ''' <summary>
    ''' Función para validar y agregar facturas desde el sp
    ''' </summary>
    ''' <param name="ListInvoices">Lista de facturas</param>
    ''' <param name="Nit">Numero de Nit</param>
    ''' <param name="container">nombre Contenedor</param>
    ''' <returns>Lista de Factura </returns>
    Function ValidateListInvoiceDevolutionSp(ListInvoices As List(Of String), Nit As String, container As String, IndigoCompany As String, Session As SessionValues) As ActionResult(Of List(Of GlosaDevolutionsReceptionD))

End Interface