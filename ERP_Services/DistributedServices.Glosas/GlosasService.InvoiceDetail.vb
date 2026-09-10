'***********************************************************************
' Assembly         : Application.Glosas
' Author           : RafaelPatiño
' Created          : 30-05-2013
'
' Last Modified By : 
' Last Modified On : 
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

    ''' <summary>
    ''' Funcion para cargar los detalle de cada factura por medio del numero de factura
    ''' </summary>
    ''' <param name="InvoiceNumber">numero de factura</param>
    ''' <returns>lista de detalles de factura</returns>
    ''' <remarks></remarks>
    Public Function ListGlosaInvoiceDetailByNumber(InvoiceNumber As String, ByVal Modulo As String, conciliationId As Integer, session As SessionValues) As List(Of Domain.Entities.GlosaInvoiceDetail) Implements IGlosasService.ListGlosaInvoiceDetailByInvoiceNumber
        Using InvoiceDetailAdminService As IInvoiceDetailAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IInvoiceDetailAdminService)()
            Return InvoiceDetailAdminService.ListGlosaInvoiceDetailByInvoiceNumber(InvoiceNumber, Modulo, conciliationId)
        End Using
    End Function
    ''' <summary>
    ''' Funcion para cargar los detalle quirurgicos
    ''' </summary>
    ''' <param name="InvoiceDetailId">Id del Detalle de Factura</param>
    ''' <returns>una lista de detalles de factura</returns>
    Public Function ListGlosaInvoiceDetailQX(InvoiceDetailId As String, session As SessionValues) As List(Of Domain.Entities.GlosaInvoiceDetailQX) Implements IGlosasService.ListGlosaInvoiceDetailQX
        Using InvoiceDetailAdminService As IInvoiceDetailAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IInvoiceDetailAdminService)()
            Return InvoiceDetailAdminService.ListGlosaInvoiceDetailQX(InvoiceDetailId)
        End Using
    End Function

    ''' <summary>
    ''' Guarda una lista de Detalles de Factura
    ''' </summary>
    ''' <param name="ListInvoiceDetail">Lista de Detalles de Factura</param>
    ''' <returns>ActionResult</returns>
    Public Function SaveGlosaInvoiceDetail(ListInvoiceDetail As List(Of GlosaInvoiceDetail), session As SessionValues) As Domain.Base.Entities.ActionResult Implements IGlosasInvoiceDetail.SaveGlosaInvoiceDetail
        Using InvoiceDetailAdminService As IInvoiceDetailAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IInvoiceDetailAdminService)()
            Return InvoiceDetailAdminService.SaveGlosaInvoiceDetail(ListInvoiceDetail, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Funcion para cargar un detalle de Factura
    ''' </summary>
    ''' <param name="Id">Codigo detalle de factura</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetGlosaInvoiceDetail(Id As String, session As SessionValues) As GlosaInvoiceDetail Implements IGlosasInvoiceDetail.GetGlosaInvoiceDetail
        Using InvoiceDetailAdminService As IInvoiceDetailAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IInvoiceDetailAdminService)()
            Return InvoiceDetailAdminService.GetGlosaInvoiceDetail(Id)
        End Using
    End Function

    ''' <summary>
    ''' Funcion que retorna un objeto detalle de factura tipo qx
    ''' </summary>
    ''' <param name="InvoiceDetailQXId">Codigo del detalle qx</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetGlosaInvoiceDetailQX(InvoiceDetailQXId As String, session As SessionValues) As GlosaInvoiceDetailQX Implements IGlosasInvoiceDetail.GetGlosaInvoiceDetailQX
        Using InvoiceDetailAdminService As IInvoiceDetailAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IInvoiceDetailAdminService)()
            Return InvoiceDetailAdminService.GetGlosaInvoiceDetailQX(InvoiceDetailQXId)
        End Using
    End Function

    ''' <summary>
    ''' Función para traer una lista de detalles que tiene cada factura, en el momento de reiteracion  
    ''' </summary>
    ''' <param name="InvoiceNumber">Numero de Factura</param>
    ''' <returns>Lista de Detalles Factura</returns>
    Public Function ListGlosaInvoiceDetailByInvoiceNumberReiteration(InvoiceNumber As String, Modulo As String, session As SessionValues) As Domain.Base.Entities.ActionResult(Of List(Of GlosaInvoiceDetail)) Implements IGlosasInvoiceDetail.ListGlosaInvoiceDetailByInvoiceNumberReiteration
        Using InvoiceDetailAdminService As IInvoiceDetailAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IInvoiceDetailAdminService)()
            Return InvoiceDetailAdminService.ListGlosaInvoiceDetailByInvoiceNumberReiteration(InvoiceNumber, Modulo)
        End Using
    End Function

    ''' <summary>
    ''' Función para traer una lista de detalles que tiene cada factura, en el momento de reiteracion  
    ''' </summary>
    ''' <param name="InvoiceNumber">Numero de Factura</param>
    ''' <returns>Lista de Detalles Factura</returns>
    Public Function ListGlosaInvoiceDetailByInvoiceNumberReiterationNoNormative(InvoiceNumber As String, Modulo As String, session As SessionValues) As Domain.Base.Entities.ActionResult(Of List(Of GlosaInvoiceDetail)) Implements IGlosasInvoiceDetail.ListGlosaInvoiceDetailByInvoiceNumberReiterationNoNormative
        Using InvoiceDetailAdminService As IInvoiceDetailAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IInvoiceDetailAdminService)()
            Return InvoiceDetailAdminService.ListGlosaInvoiceDetailByInvoiceNumberReiterationNoNormative(InvoiceNumber, Modulo)
        End Using
    End Function

    ''' <summary>
    ''' Función para traer una lista de detalles que tiene cada factura de una reiteración que no han sido glosadas previamente
    ''' </summary>
    ''' <param name="InvoiceNumber"></param>
    ''' <param name="Modulo"></param>
    ''' <returns></returns>
    Public Function ListGlosaInvoiceDetailByInvoiceNumberReiterationWithOutMovements(InvoiceNumber As String, Modulo As String, session As SessionValues) As ActionResult(Of List(Of GlosaInvoiceDetail)) Implements IGlosasInvoiceDetail.ListGlosaInvoiceDetailByInvoiceNumberReiterationWithOutMovements
        Using InvoiceDetailAdminService As IInvoiceDetailAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IInvoiceDetailAdminService)()
            Return InvoiceDetailAdminService.ListGlosaInvoiceDetailByInvoiceNumberReiterationWithOutMovements(InvoiceNumber, Modulo)
        End Using
    End Function

    ''' <summary>
    ''' lista de detalle de facturas
    ''' </summary>
    ''' <param name="listInvoice"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListsStructureDetailInvocie(listInvoice As List(Of String), session As SessionValues) As List(Of GlosaInvoiceDetail) Implements IGlosasInvoiceDetail.ListsStructureDetailInvocie
        Using InvoiceDetailAdminService As IInvoiceDetailAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IInvoiceDetailAdminService)()
            Return InvoiceDetailAdminService.ListsStructureDetailInvocie(listInvoice)
        End Using
    End Function

    ''' <summary>
    ''' Función para traer una lista de detalles que tiene cada factura 
    ''' </summary>
    ''' <param name="InvoiceNumber">Numero de Factura</param>
    ''' <returns>Lista de Detalles Factura</returns>
    Public Function ListGlosaInvoiceDetailForGeneralConciliation(InvoiceNumber As String, session As SessionValues) As List(Of GlosaInvoiceDetail) Implements IGlosasInvoiceDetail.ListGlosaInvoiceDetailForGeneralConciliation
        Using InvoiceDetailAdminService As IInvoiceDetailAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IInvoiceDetailAdminService)()
            Return InvoiceDetailAdminService.ListGlosaInvoiceDetailForGeneralConciliation(InvoiceNumber)
        End Using
    End Function
End Class
