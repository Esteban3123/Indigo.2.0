'***********************************************************************
' Assembly         : DistributedService.Glosas
' Author           : Rafael Eduardo Patiño
' Created          : 17-06-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Domain.Entities

Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.IOC
Imports Application.Glosas
Imports Infrastructure.CrossCutting.Base

#End Region

Partial Class GlosasService

#Region "GlosaMovementGlosa"

    ''' <summary>
    ''' Funcion para Guardar un Moviemineto glosa
    ''' </summary>
    ''' <param name="ListMovementGlosa">movimineto glosa</param>
    ''' <param name="session">Objeto session</param>
    Public Function SaveMovementGlosa(ListMovementGlosa As List(Of GlosaMovementGlosa), session As SessionValues) As Domain.Base.Entities.ActionResult Implements IGlosaMovementGlosa.SaveMovementGlosa
        Using MovementGlosaAdminservice As IMovementGlosaAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IMovementGlosaAdminService)()
            Return MovementGlosaAdminservice.SaveMovementGlosa(ListMovementGlosa, session.AuditMessageWcf)
        End Using
    End Function


    ''' <summary>
    ''' Funcion que carga una lista de movimientos de glosa
    ''' </summary>
    ''' <param name="InvoiceDetailId">codigo del detalle de factura</param>
    ''' <returns>lista de moviminetos de glosa</returns>
    Public Function ListMovementGlosa(InvoiceDetailId As String, session As SessionValues) As List(Of GlosaMovementGlosa) Implements IGlosaMovementGlosa.ListMovementGlosa
        Using MovementGlosaAdminservice As IMovementGlosaAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IMovementGlosaAdminService)()
            Return MovementGlosaAdminservice.ListMovementGlosa(InvoiceDetailId)
        End Using
    End Function

    ''' <summary>
    ''' Funcion para eliminar un Movimineto Glosa en este caso un Registro de Objecion
    ''' </summary>
    ''' <param name="MovementGlosa">MovimientoGLosa</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteMovementGlosa(MovementGlosa As GlosaMovementGlosa, session As SessionValues) As Boolean Implements IGlosaMovementGlosa.DeleteMovementGlosa
        Using MovementGlosaAdminservice As IMovementGlosaAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IMovementGlosaAdminService)()
            Return MovementGlosaAdminservice.DeleteMovementGlosa(MovementGlosa, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Funcion que lista los movimineto glosa por el codigo de detalle de factura
    ''' </summary>
    ''' <param name="InvoiceDetailId">codigo detalle de factura</param>
    '''   <param name="InvoiceDetailQXId">codigo detalle de factura QX</param>
    ''' <returns>lista de moviminetos</returns>
    Public Function ListMovementGlosaQx(InvoiceDetailId As String, InvoiceDetailQXId As String, session As SessionValues) As List(Of GlosaMovementGlosa) Implements IGlosaMovementGlosa.ListMovementGlosaQx
        Using MovementGlosaAdminservice As IMovementGlosaAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IMovementGlosaAdminService)()
            Return MovementGlosaAdminservice.ListMovementGlosaQx(InvoiceDetailId, InvoiceDetailQXId)
        End Using
    End Function

    ''' <summary>
    ''' Funcion que lista los movimiento glosa por el codigo de detalle de factura
    ''' </summary>
    ''' <param name="InvoiceDetailId">codigo detalle de factura</param>
    '''   <param name="InvoiceDetailQXId">codigo detalle de factura QX</param>
    ''' <returns>lista de movimientos</returns>
    Public Function ListMovementGlosaByCodes(InvoiceDetailId As String, InvoiceDetailQXId As String, session As SessionValues) As List(Of GlosaMovementGlosa) Implements IGlosaMovementGlosa.ListMovementGlosaByCodes
        Using MovementGlosaAdminservice As IMovementGlosaAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IMovementGlosaAdminService)()
            Return MovementGlosaAdminservice.ListMovementGlosaByCodes(InvoiceDetailId, InvoiceDetailQXId)
        End Using
    End Function

    ''' <summary>
    ''' Funcion para actualizar un movimiento glosa
    ''' </summary>
    ''' <param name="MovementGlosa">MovimientoGLosa</param>
    ''' <returns></returns>
    Public Function UpdateMovementReiteration(ByVal MovementGlosa As GlosaMovementGlosa, session As SessionValues) As ActionResult Implements IGlosaMovementGlosa.UpdateMovementReiteration
        Using MovementGlosaAdminservice As IMovementGlosaAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IMovementGlosaAdminService)()
            Return MovementGlosaAdminservice.UpdateMovementReiteration(MovementGlosa, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene una lista de movimientos según Numero de factura y Código Responsable
    ''' </summary>
    ''' <param name="InvoiceNumber">Numero Factura</param>
    ''' <param name="CodeResponsible">Código Responsable</param>
    ''' <returns>Lista de Movimientos</returns>
    Public Function ListMovementsByInvoiceAndResponsible(InvoiceNumber As String, session As SessionValues, Optional CodeResponsible As String = "") As List(Of GlosaMovementGlosa) Implements IGlosaMovementGlosa.ListMovementsByInvoiceAndResponsible
        Using MovementGlosaAdminservice As IMovementGlosaAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IMovementGlosaAdminService)()
            Return MovementGlosaAdminservice.ListMovementsByInvoiceAndResponsible(InvoiceNumber, CodeResponsible)
        End Using
    End Function

    ''' <summary>
    ''' Funcion para guardar datos de la evaluación
    ''' </summary>
    ''' <param name="ListMov">Lista de movimientos</param>
    ''' <param name="session">Objeto session</param>
    Public Function SaveMovEvaluation(ListMov As List(Of GlosaMovementGlosa), session As SessionValues, Optional ByVal _IdUnitoperating As Integer = 0) As Domain.Base.Entities.ActionResult Implements IGlosaMovementGlosa.SaveMovEvaluation
        Using MovementGlosaAdminservice As IMovementGlosaAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IMovementGlosaAdminService)()
            Return MovementGlosaAdminservice.SaveMovEvaluation(ListMov, session, _IdUnitoperating)
        End Using
    End Function

    ''' <summary>
    ''' Función para transferir responsables en los movimientos de glosas
    ''' </summary>
    ''' <param name="listResponsiblesMovements">Lista de Movimientos por Responsables</param>
    ''' <param name="session">Objeto Session</param>
    ''' <param name="opt">Opción 1:Responsables 2:Conceptos</param>
    ''' <returns>ActionResult</returns>
    Public Function TransferResponsibleMovements(listResponsiblesMovements As List(Of ResponsibleMovements), session As SessionValues, opt As Integer) As Domain.Base.Entities.ActionResult Implements IGlosaMovementGlosa.TransferResponsibleMovements
        Using MovementGlosaAdminservice As IMovementGlosaAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IMovementGlosaAdminService)()
            Return MovementGlosaAdminservice.TransferResponsibleMovements(listResponsiblesMovements, session.AuditMessageWcf, opt)
        End Using
    End Function


    ''' <summary>
    ''' Funcion para listar movimientos de glosas para las evaluaciones masivas a nivel de facturas
    ''' </summary>
    ''' <param name="ListInvoice">Lista de Facturas</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllMovementGlosabymultipleInvoice(ListInvoice As List(Of String), session As SessionValues, Optional ByVal codeUser As String = "") As List(Of GlosaMovementGlosa) Implements IGlosaMovementGlosa.ListAllMovementGlosabymultipleInvoice
        Using MovementGlosaAdminservice As IMovementGlosaAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IMovementGlosaAdminService)()
            Return MovementGlosaAdminservice.ListAllMovementGlosabymultipleInvoice(ListInvoice, codeUser)
        End Using
    End Function

    ''' <summary>
    ''' Guardar movimientos de tipo reiteracion
    ''' </summary>
    ''' <param name="ObjSaveListMovementGlosa"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveReiterationMovementGlosa(ObjSaveListMovementGlosa As List(Of GlosaMovementGlosa), session As SessionValues) As Domain.Base.Entities.ActionResult Implements IGlosaMovementGlosa.SaveReiterationMovementGlosa
        Using MovementGlosaAdminservice As IMovementGlosaAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IMovementGlosaAdminService)()
            Return MovementGlosaAdminservice.SaveReiterationMovementGlosa(ObjSaveListMovementGlosa, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Genera la estructura de un oficio desde coordinación
    ''' </summary>
    ''' <param name="IdReciptionObjection"></param>
    ''' <returns></returns>
    Public Function ExportCoordinationGlosa(IdReciptionObjection As Integer, session As SessionValues) As DataSet Implements IGlosaMovementGlosa.ExportCoordinationGlosa
        Using MovementGlosaAdminService As IMovementGlosaAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IMovementGlosaAdminService)()
            Return MovementGlosaAdminService.ExportCoordinationGlosa(IdReciptionObjection, session)
        End Using
    End Function

    ''' <summary>
    ''' Carga masiva desde coordinación
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Function ChargueExcelDataCoordination(dt As DataSet, session As SessionValues) As ActionResult Implements IGlosaMovementGlosa.ChargueExcelDataCoordination
        Using service As IMovementGlosaAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IMovementGlosaAdminService)()
            Return service.ChargueExcelDataCoordination(dt, session)
        End Using
    End Function

#End Region

End Class
