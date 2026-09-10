'***********************************************************************
' Assembly         : DistributedService.Glosas
' Author           : Rafael Eduardo Patiño
' Created          : 17-06-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.ServiceModel
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base

<ServiceContract()> _
Public Interface IGlosaMovementGlosa

    ''' <summary>
    ''' Funcion para Guardar un Moviemineto glosa
    ''' </summary>
    ''' <param name="ListMovementGlosa">movimineto glosa</param>
    ''' <param name="session"></param>
    <OperationContract()>
    Function SaveMovementGlosa(ListMovementGlosa As List(Of GlosaMovementGlosa), session As SessionValues) As ActionResult

    ''' <summary>
    ''' Funcion que carga una lista de movimientos de glosa
    ''' </summary>
    ''' <param name="InvoiceDetailId">codigo del detalle de factura</param>
    ''' <returns>lista de moviminetos de glosa</returns>
    <OperationContract>
    Function ListMovementGlosa(InvoiceDetailId As String, session As SessionValues) As List(Of GlosaMovementGlosa)

    ''' <summary>
    ''' Funcion para eliminar un Movimineto Glosa en este caso un Registro de Objecion
    ''' </summary>
    ''' <param name="MovementGlosa">MovimientoGLosa</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract>
    Function DeleteMovementGlosa(ByVal MovementGlosa As GlosaMovementGlosa, session As SessionValues) As Boolean


    ''' <summary>
    ''' Funcion que lista los movimineto glosa por el codigo de detalle de factura
    ''' </summary>
    ''' <param name="InvoiceDetailId">codigo detalle de factura</param>
    '''   <param name="InvoiceDetailQXId">codigo detalle de factura QX</param>
    ''' <returns>lista de moviminetos</returns>
    <OperationContract>
    Function ListMovementGlosaQx(InvoiceDetailId As String, InvoiceDetailQXId As String, session As SessionValues) As List(Of GlosaMovementGlosa)

    ''' <summary>
    ''' Funcion que lista los movimiento glosa por el codigo de detalle de factura
    ''' </summary>
    ''' <param name="InvoiceDetailId">codigo detalle de factura</param>
    '''   <param name="InvoiceDetailQXId">codigo detalle de factura QX</param>
    ''' <returns>lista de movimientos</returns>
    <OperationContract>
    Function ListMovementGlosaByCodes(InvoiceDetailId As String, InvoiceDetailQXId As String, session As SessionValues) As List(Of GlosaMovementGlosa)

    ''' <summary>
    ''' Funcion para actualizar un movimiento glosa
    ''' </summary>
    ''' <param name="MovementGlosa">MovimientoGLosa</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract>
    Function UpdateMovementReiteration(ByVal MovementGlosa As GlosaMovementGlosa, session As SessionValues) As ActionResult

    ''' <summary>
    ''' Obtiene una lista de movimientos según Numero de factura y Código Responsable
    ''' </summary>
    ''' <param name="InvoiceNumber">Numero Factura</param>
    ''' <param name="CodeResponsible">Código Responsable</param>
    ''' <returns>Lista de Movimientos</returns>
    <OperationContract>
    Function ListMovementsByInvoiceAndResponsible(InvoiceNumber As String, session As SessionValues, Optional CodeResponsible As String = "") As List(Of GlosaMovementGlosa)

    ''' <summary>
    ''' Guarda los datos de la evaluación
    ''' </summary>
    ''' <param name="ListMov">Lista de movimientos</param>
    ''' <param name="session">Objeto session</param>
    <OperationContract>
    Function SaveMovEvaluation(ListMov As List(Of GlosaMovementGlosa), session As SessionValues, Optional ByVal _IdUnitoperating As Integer = 0) As ActionResult

    ''' <summary>
    ''' Función para transferir responsables en los movimientos de glosas
    ''' </summary>
    ''' <param name="listResponsiblesMovements">Lista de Movimientos por Responsables</param>
    ''' <param name="session">Objeto Session</param>
    ''' <returns>ActionResult</returns>
    <OperationContract>
    Function TransferResponsibleMovements(listResponsiblesMovements As List(Of ResponsibleMovements), session As SessionValues, opt As Integer) As ActionResult

    ''' <summary>
    ''' Funcion para listar movimientos de glosas para las evaluaciones masivas a nivel de facturas
    ''' </summary>
    ''' <param name="ListInvoice">Lista de Facturas</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract>
    Function ListAllMovementGlosabymultipleInvoice(ListInvoice As List(Of String), session As SessionValues, Optional ByVal codeUser As String = "") As List(Of GlosaMovementGlosa)


    ''' <summary>
    ''' Guarda de una lista de movieminetos, las reiteraciones
    ''' </summary>
    ''' <param name="ObjSaveListMovementGlosa"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract>
    Function SaveReiterationMovementGlosa(ObjSaveListMovementGlosa As List(Of GlosaMovementGlosa), session As SessionValues) As ActionResult

    ''' <summary>
    ''' Genera la estructura de un oficio desde coordinación
    ''' </summary>
    ''' <param name="IdReciptionObjection"></param>
    ''' <returns></returns>
    <OperationContract>
    Function ExportCoordinationGlosa(IdReciptionObjection As Integer, session As SessionValues) As DataSet

    ''' <summary>
    ''' Carga masiva desde coordinación
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    <OperationContract>
    Function ChargueExcelDataCoordination(dt As DataSet, session As SessionValues) As ActionResult

End Interface
