Imports System.ServiceModel
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()> _
Public Interface IGlosasMovementDevolutions

    ''' <summary>
    ''' Funcion para Guardar un Movimiento de Devolución
    ''' </summary>
    ''' <param name="MovementDevolution">Movimineto Devolución</param>
    ''' <param name="session">Objeto session</param>
    <OperationContract>
    Function SaveMovementDevolution(MovementDevolution As GlosaMovementDevolutions, ByVal session As SessionValues) As ActionResult(Of GlosaMovementDevolutions)

    ''' <summary>
    ''' Funcion para eliminar un Movimiento de Devolución
    ''' </summary>
    ''' <param name="MovementDevolution">Movimiento Devolución</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract>
    Function DeleteMovementDevolution(ByVal MovementDevolution As GlosaMovementDevolutions, ByVal session As SessionValues) As ActionResult

    ''' <summary>
    ''' Lista todos los Movimientos de Devoluciones.
    ''' </summary>
    ''' <returns>Lista de Movimiento de Devoluciones</returns>
    <OperationContract>
    Function ListAllMovementGlosaDevolutions(ByVal session As SessionValues) As List(Of GlosaMovementDevolutions)

    ''' <summary>
    ''' Obtiene un Movimiento Devolucion especifico.
    ''' </summary>
    ''' <param name="Invoice">Numero de factura</param>
    ''' <param name="IdDevolutionD">Id Devolución Detalle</param>
    ''' <returns>Objeto Movimiento Devolucion</returns>
    <OperationContract>
    Function GetMovementDevolutionByInvoiceNumber(ByVal Invoice As String, ByVal IdDevolutionD As String, ByVal session As SessionValues) As GlosaMovementDevolutions

    ''' <summary>
    ''' Funcion para Confirmar un Movimiento de Devolución
    ''' </summary>
    ''' <param name="listDevolutionD">lista de facturas de  Devolución</param>
    ''' <param name="IndigoSessionValues">Objeto session</param>
    <OperationContract>
    Function ConfirmDevolution(ByVal listDevolutionD As List(Of GlosaDevolutionsReceptionD), ByVal idSequence As Integer, ByVal Injustificate As Boolean, ByVal UserFreeInvoice As Boolean, IndigoSessionValues As SessionValues) As ActionResult

End Interface
