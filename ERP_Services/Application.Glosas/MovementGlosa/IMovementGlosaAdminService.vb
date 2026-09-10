'***********************************************************************
' Assembly         : Application.Glosas
' Author           : RafaelPatiño
' Created          : 17-06-2013
'
' Last Modified By : RafaelPatiño
' Last Modified On : 2013-06-17
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface IMovementGlosaAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Funcion para Guardar una lista de  Moviemineto glosa
    ''' </summary>
    ''' <param name="ListMovementGlosa">movimineto glosa</param>
    ''' <param name="audit"></param>
    Function SaveMovementGlosa(ListMovementGlosa As List(Of GlosaMovementGlosa), audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Funcion que carga una lista de movimientos de glosa
    ''' </summary>
    ''' <param name="InvoiceDetailId">codigo del detalle de factura</param>
    ''' <returns>lista de moviminetos de glosa</returns>
    Function ListMovementGlosa(InvoiceDetailId As String) As List(Of GlosaMovementGlosa)
    ''' <summary>
    ''' Funcion para eliminar un Movimineto Glosa en este caso un Registro de Objecion
    ''' </summary>
    ''' <param name="MovementGlosa">MovimientoGLosa</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DeleteMovementGlosa(ByVal MovementGlosa As GlosaMovementGlosa, audit As AuditMessage) As Boolean

    ''' <summary>
    ''' Funcion que lista los movimineto glosa por el codigo de detalle de factura
    ''' </summary>
    ''' <param name="InvoiceDetailId">codigo detalle de factura</param>
    '''   <param name="InvoiceDetailQXId">codigo detalle de factura QX</param>
    ''' <returns>lista de moviminetos</returns>
    Function ListMovementGlosaQx(InvoiceDetailId As String, InvoiceDetailQXId As String) As List(Of GlosaMovementGlosa)


    ''' <summary>
    ''' Funcion que lista los movimiento glosa por el codigo de detalle de factura
    ''' </summary>
    ''' <param name="InvoiceDetailId">codigo detalle de factura</param>
    '''   <param name="InvoiceDetailQXId">codigo detalle de factura QX</param>
    ''' <returns>lista de movimientos</returns>
    Function ListMovementGlosaByCodes(InvoiceDetailId As String, InvoiceDetailQXId As String) As List(Of GlosaMovementGlosa)

    ''' <summary>
    ''' Funcion para actualizar un movimiento glosa
    ''' </summary>
    ''' <param name="MovementGlosa">MovimientoGLosa</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function UpdateMovementReiteration(ByVal MovementGlosa As GlosaMovementGlosa, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene una lista de movimientos según Numero de factura y Código Responsable
    ''' </summary>
    ''' <param name="InvoiceNumber">Numero Factura</param>
    ''' <param name="CodeResponsible">Código Responsable</param>
    ''' <returns>Lista de Movimientos</returns>
    Function ListMovementsByInvoiceAndResponsible(InvoiceNumber As String, Optional CodeResponsible As String = "") As List(Of GlosaMovementGlosa)

    ''' <summary>
    ''' Guardar lista de movimientos de evaluación
    ''' </summary>
    ''' <param name="ListMov">lista de movimientos</param>
    ''' <param name="IndigoSessionValues">valores de sesion</param> 
    ''' <returns>Boolean</returns>
    Function SaveMovEvaluation(ListMov As List(Of GlosaMovementGlosa), IndigoSessionValues As SessionValues, Optional ByVal _IdUnitoperating As Integer = 0) As ActionResult

    ''' <summary>
    ''' Función para transferir responsables en los movimientos de glosas
    ''' </summary>
    ''' <param name="listResponsiblesMovements">Lista de Movimientos por Responsables</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>ActionResult</returns>
    Function TransferResponsibleMovements(listResponsiblesMovements As List(Of ResponsibleMovements), audit As AuditMessage, opt As Integer) As ActionResult

    ''' <summary>
    ''' Funcion para listar movimientos de glosas para las evaluaciones masivas a nivel de facturas
    ''' </summary>
    ''' <param name="ListInvoice">Lista de Facturas</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllMovementGlosabymultipleInvoice(ByVal ListInvoice As List(Of String), Optional ByVal codeUser As String = "") As List(Of GlosaMovementGlosa)

    ''' <summary>
    ''' Guarda de una lista de movieminetos, las reiteraciones
    ''' </summary>
    ''' <param name="ObjSaveListMovementGlosa"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveReiterationMovementGlosa(ObjSaveListMovementGlosa As List(Of GlosaMovementGlosa), audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Genera la estructura de un oficio desde coordinación
    ''' </summary>
    ''' <param name="IdReciptionObjection"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Function ExportCoordinationGlosa(IdReciptionObjection As Integer, session As SessionValues) As DataSet

    ''' <summary>
    ''' Carga masiva desde coordinación
    ''' </summary>
    ''' <returns></returns>
    Function ChargueExcelDataCoordination(dt As DataSet, session As SessionValues) As ActionResult

End Interface
