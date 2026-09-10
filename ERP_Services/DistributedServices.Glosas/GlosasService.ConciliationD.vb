'***********************************************************************
' Assembly         : DistributedService.Glosas
' Author           : Juan Diego Diaz
' Created          : 23-05-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports Domain.Entities

Imports Infrastructure.CrossCutting.IOC
Imports Application.Glosas
Imports Infrastructure.CrossCutting.Base

#End Region

Partial Class GlosasService

    ''' <summary>
    ''' Elimina detalles de conciliación.
    ''' </summary>
    ''' <param name="ConciliacionD">Lista Conciliación Detalle</param>
    ''' <returns>ActionResult</returns>
    Public Function DeleteConciliationD(ConciliacionD As List(Of ConciliationD), session As SessionValues) As Domain.Base.Entities.ActionResult Implements IGlosasConciliationD.DeleteConciliationD
        Using conciliationD As IConciliationDAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IConciliationDAdminService)()
            Return conciliationD.DeleteConciliationD(ConciliacionD, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un detalle de conciliación especifico.
    ''' </summary>
    ''' <param name="Id">Id de objecion detalle</param>
    ''' <returns>Objeto Conciliación Detalle</returns>
    Public Function GetConciliationDByIdObjectionD(Id As String, session As SessionValues) As ConciliationD Implements IGlosasConciliationD.GetConciliationDByIdObjectionD
        Using conciliationD As IConciliationDAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IConciliationDAdminService)()
            Return conciliationD.GetConciliationDByIdObjectionD(Id)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene todos los detalles de conciliacion.
    ''' </summary>
    ''' <returns>ActionResult</returns>
    Public Function ListAllConciliationD(session As SessionValues) As List(Of ConciliationD) Implements IGlosasConciliationD.ListAllConciliationD
        Using conciliationD As IConciliationDAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IConciliationDAdminService)()
            Return conciliationD.ListAllConciliationD
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un detalle de conciliación especifico.
    ''' </summary>
    ''' <param name="Id">Id de conciliacion cabecera</param>
    ''' <returns>Lista Conciliación Detalle</returns>
    Public Function ListConciliationDByIdConciliationC(Id As String, session As SessionValues) As List(Of ConciliationD) Implements IGlosasConciliationD.ListConciliationDByIdConciliationC
        Using conciliationD As IConciliationDAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IConciliationDAdminService)()
            Return conciliationD.ListConciliationDByIdConciliationC(Id)
        End Using
    End Function

    ''' <summary>
    ''' guarda detalles de conciliación.
    ''' </summary>
    ''' <param name="ConciliacionD">Lista Conciliación Detalle</param>
    ''' <returns>ActionResult</returns>
    Public Function SaveConciliationD(ConciliacionD As List(Of ConciliationD), session As SessionValues) As Domain.Base.Entities.ActionResult Implements IGlosasConciliationD.SaveConciliationD
        Using conciliationD As IConciliationDAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IConciliationDAdminService)()
            Return conciliationD.SaveConciliationD(ConciliacionD, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Función que obtiene registros de facturas.
    ''' </summary>
    ''' <param name="Factura">Numero Factura</param>
    ''' <param name="Nit"></param>
    ''' <param name="session"></param>
    ''' <param name="listStatus"></param>
    ''' <returns>Lista Cartera Glosa</returns>
    Public Function ListInvoicesByNumber(Factura As String, Nit As String, session As SessionValues, ByVal listStatus As List(Of String)) As GlosaPortfolioGlosada Implements IGlosasConciliationD.ListInvoicesByNumber
        Using conciliationD As IConciliationDAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IConciliationDAdminService)()
            Return conciliationD.ListInvoicesByNumber(Factura, Nit, listStatus)
        End Using
    End Function

    ''' <summary>
    ''' obtiene una cartera glosada por Nit
    ''' </summary>
    ''' <param name="Nit">Nit de la objecion</param>
    ''' <returns>Objeto Cartera Glosa</returns>
    Public Function ListConfirmGlosaPortfolio(Nit As String, session As SessionValues) As List(Of GlosaPortfolioGlosada) Implements IGlosasConciliationD.ListConfirmGlosaPortfolio
        Using conciliationD As IConciliationDAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IConciliationDAdminService)()
            Return conciliationD.ListConfirmGlosaPortfolio(Nit)
        End Using
    End Function

    ''' <summary>
    ''' Guardar Objecion en el detalle de factura
    ''' </summary>
    ''' <param name="Movimientos">Objeto Movimiento</param>
    ''' <param name="session">Objeto session</param>
    ''' <returns>Action Result</returns>
    Function SaveConciliationInvoiceDetail(Movimientos As List(Of GlosaMovementGlosa), session As SessionValues) As Domain.Base.Entities.ActionResult Implements IGlosasConciliationD.SaveConciliationInvoiceDetail
        Using conciliationD As IConciliationDAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IConciliationDAdminService)()
            Return conciliationD.SaveConciliationInvoiceDetail(Movimientos, session.AuditMessageWcf)
        End Using
    End Function
    ''' <summary>
    ''' Confirmar Objecion en el detalle de factura
    ''' </summary>
    ''' <param name="Movimientos">Objeto Movimiento</param>
    ''' <param name="session">Objeto Auditoria</param>
    ''' <returns>Action Result</returns>
    Function ConfirmConciliationInvoiceDetail(Movimientos As List(Of GlosaMovementGlosa), session As SessionValues) As Domain.Base.Entities.ActionResult Implements IGlosasConciliationD.ConfirmConciliationInvoiceDetail
        Using conciliationD As IConciliationDAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IConciliationDAdminService)()
            Return conciliationD.ConfirmConciliationInvoiceDetail(Movimientos, session.AuditMessageWcf)
        End Using
    End Function
    ''' <summary>
    ''' Confirmar factura
    ''' </summary>
    ''' <param name="InvoiceNumber">Numero Factura</param>
    ''' <param name="session">Objeto Auditoria</param>
    ''' <returns>Action Result</returns>
    Function ConfirmConciliationInvoice(conciliationId As Integer, InvoiceNumber As String, _IdUnitoperating As Integer, session As SessionValues) As Domain.Base.Entities.ActionResult Implements IGlosasConciliationD.ConfirmConciliationInvoice
        Using conciliationD As IConciliationDAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IConciliationDAdminService)()
            Return conciliationD.ConfirmConciliationInvoice(conciliationId, InvoiceNumber, _IdUnitoperating, session)
        End Using
    End Function

    ''' <summary>
    ''' 'Funcion para validar y subir conciliaciones desde excel
    ''' </summary>
    ''' <param name="dtSet"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateExcelDataConciliation(dtSet As DataSet, ByVal ConciliationC As ConciliationC, Session As SessionValues) As Domain.Base.Entities.ActionResult(Of ConciliationC) Implements IGlosasConciliationD.ValidateExcelDataConciliation
        Using conciliationD As IConciliationDAdminService = IocFactory.Instance(Session.TransactionalContainer).CurrentContainer.Resolve(Of IConciliationDAdminService)()
            Return conciliationD.ValidateExcelDataConciliation(dtSet, ConciliationC, Session.AuditMessageWcf)
        End Using
    End Function
End Class
