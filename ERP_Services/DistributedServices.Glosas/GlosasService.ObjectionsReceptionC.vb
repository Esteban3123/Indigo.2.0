'***********************************************************************
' Assembly         : Application.Glosas
' Author           : RafaelPatiño
' Created          : 12-04-2013
'
' Last Modified By : RafaelPatiño
' Last Modified On : 2013-04-21
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

#Region "ObjectionsReception"

    ''' <summary>
    ''' Guarda el oficio actualizado por el proceso de coordinacion
    ''' </summary>
    ''' <param name="ObjectionsReceptionC">Oficio a actualizar</param>
    ''' <param name="session">session</param>
    ''' <returns>Resultado de la accion</returns>
    Public Function SaveObjectionsReceptionCInCoordication(ObjectionsReceptionC As GlosaObjectionsReceptionC, ListInvocie As List(Of String), ByVal Session As SessionValues) As ActionResult Implements IGlosasObjectionsReceptionC.SaveObjectionsReceptionCInCoordication
        Using ObjectionsReceptionAdmin As IObjectionsReceptionCAdminService = IocFactory.Instance(Session.TransactionalContainer).CurrentContainer.Resolve(Of IObjectionsReceptionCAdminService)()
            Return ObjectionsReceptionAdmin.SaveObjectionsReceptionCInCoordication(ObjectionsReceptionC, ListInvocie, Session)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene una objecion completa con sus agregados por su numero de Id
    ''' </summary>
    ''' <param name="id">Id de la objecion</param>
    ''' <returns>La objecion</returns>
    Public Function GetObjectionCWithAgregatesById(id As String, session As SessionValues, ByVal _IdIOperatingUnit As Integer) As GlosaObjectionsReceptionC Implements IGlosasObjectionsReceptionC.GetObjectionCWithAgregatesById
        Using ObjectionsReceptionAdmin As IObjectionsReceptionCAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IObjectionsReceptionCAdminService)()
            Using timeParameterAdmin As ITimeParametersAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ITimeParametersAdminService)()
                Return ObjectionsReceptionAdmin.GetObjectionCWithAgregatesById(id, timeParameterAdmin, _IdIOperatingUnit)
            End Using
        End Using
    End Function

    ''' <summary>
    ''' Obtiene una objecion completa con sus agregados por su numero de Id
    ''' </summary>
    ''' <param name="id">Id de la objecion</param>
    ''' <returns>La objecion</returns>
    Public Function GetObjectionCWithoutAgregatesById(id As Integer, ByVal _IdIOperatingUnit As Integer, session As SessionValues) As GlosaObjectionsReceptionC Implements IGlosasObjectionsReceptionC.GetObjectionCWithoutAgregatesById
        Using ObjectionsReceptionAdmin As IObjectionsReceptionCAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IObjectionsReceptionCAdminService)()
            Return ObjectionsReceptionAdmin.GetObjectionCWithoutAgregatesById(id, _IdIOperatingUnit)
        End Using
    End Function

    ''' <summary>
    ''' Lista las recepciones de objeciones por su estado
    ''' </summary>
    ''' <param name="status">Estado de las objeciones a listar</param>
    ''' <returns>Lista de objeciones</returns>
    Public Function ListObjectionsReceptionCByStatus(status As String, session As SessionValues) As List(Of GlosaObjectionsReceptionC) Implements IGlosasObjectionsReceptionC.ListObjectionsReceptionCByStatus
        Using ObjectionsReceptionAdmin As IObjectionsReceptionCAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IObjectionsReceptionCAdminService)()
            Return ObjectionsReceptionAdmin.ListObjectionsReceptionCByStatus(status.Trim())
        End Using
    End Function

    ''' <summary>
    ''' guarda una objecion con su detalle
    ''' </summary>
    ''' <param name="ObjectionsReceptionC">la objecion de recepcion</param>
    ''' <param name="session">session</param>
    ''' <returns>valor de si guardo o no</returns>
    Public Function SaveObjectionsReceptionC(ObjectionsReceptionC As GlosaObjectionsReceptionC, session As SessionValues) As ActionResult Implements IGlosasService.SaveObjectionsReceptionC
        Using ObjectionsReceptionAdmin As IObjectionsReceptionCAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IObjectionsReceptionCAdminService)()
            Return ObjectionsReceptionAdmin.SaveObjectionsReceptionC(ObjectionsReceptionC, session.IndigoCompany, session.AuditMessageWcf)
        End Using
    End Function
    ''' <summary>
    ''' guardar los detalles de una recepcion de objeciones y persistir los detalles de las facturas
    ''' </summary>
    ''' <param name="RadicatedConsecutive">numero consecutivo de radicado</param>
    ''' <param name="ContainerName">nombre del contenedor</param>
    ''' <param name="GlosaObjectionsReceptionD">objeto factura a guardar</param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Function SaveObjectionsReceptionDAndPersist(RadicatedConsecutive As String, ContainerName As String, GlosaObjectionsReceptionD As GlosaObjectionsReceptionD, session As SessionValues) As ActionResult Implements IGlosasService.SaveObjectionsReceptionDAndPersist
        Using ObjectionsReceptionAdmin As IObjectionsReceptionCAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IObjectionsReceptionCAdminService)()
            Return ObjectionsReceptionAdmin.SaveObjectionsReceptionDAndPersist(RadicatedConsecutive, ContainerName, GlosaObjectionsReceptionD, session)
        End Using
    End Function
    ''' <summary>
    ''' Lista las facturas por contenedor y nit de la entidad ademas del total de registro
    ''' </summary>
    ''' <param name="nameContainer">recibe el nombre del contenedor</param>
    ''' <param name="nit">nit del tercro a buscar facturas</param>
    ''' <param name="InvoiceNumber">Numero de factura</param>
    ''' <param name="session">variable de sesion</param>
    '''  <param name="stringSQl">cadena Sql de Filtro avanzados</param>
    ''' <param name="TopQuery">Cantidad de Registro a retornar</param>
    ''' <returns>Un objeto result que tiene una lista de facturas y el conteo de las misma</returns>
    Public Function ListAllInvoce(nameContainer As String, nit As String, InvoiceNumber As String, session As SessionValues, stringSQl As String, TopQuery As String, ByVal FlagNotConfirmInvoice As String) As ActionResult(Of List(Of SP_invoiceList_Result)) Implements IGlosasObjectionsReceptionC.ListAllInvoce
        Using ObjectionsReceptionAdmin As IObjectionsReceptionCAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IObjectionsReceptionCAdminService)()
            Return ObjectionsReceptionAdmin.ListAllInvoice(nameContainer, nit, InvoiceNumber, session.TransactionalContainer, stringSQl, TopQuery, FlagNotConfirmInvoice, session)
        End Using
    End Function

    ''' <summary>
    ''' carga una factura 
    ''' </summary>
    ''' <param name="nameContainer">recibe el nombre del contenedor</param>
    ''' <param name="nit">nit del tercro a buscar facturas</param>
    ''' <param name="InvoiceNumber">numero de factura</param>
    ''' <returns>una factura</returns>
    Public Function GetInvoice(nameContainer As String, nit As String, InvoiceNumber As String, session As SessionValues, ByVal stringSQl As String, ByVal FlagNotConfirmInvoice As String) As SP_invoiceList_Result Implements IGlosasService.GetInvoice
        Using ObjectionsReceptionAdmin As IObjectionsReceptionCAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IObjectionsReceptionCAdminService)()
            Return ObjectionsReceptionAdmin.GetInvoce(nameContainer, nit, InvoiceNumber, session.TransactionalContainer, stringSQl, FlagNotConfirmInvoice, session)
        End Using
    End Function
    ''' <summary>
    ''' obtiene una recepcion de objeciones
    ''' </summary>
    ''' <param name="codeObjectionReceptionC">codigo de la recepcion </param>
    ''' <returns>una recepcion de objecion </returns>
    Public Function GetObjection(codeObjectionReceptionC As String, session As SessionValues) As GlosaObjectionsReceptionC Implements IGlosasService.GetObjection
        Using ObjectionsReceptionAdmin As IObjectionsReceptionCAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IObjectionsReceptionCAdminService)()
            Return ObjectionsReceptionAdmin.GetObjection(codeObjectionReceptionC)
        End Using
    End Function

    ''' <summary>
    ''' confirma  una objecion
    ''' </summary>
    ''' <param name="ObjectionsReceptionC">objeto objecion</param>
    ''' <param name="session">session</param>
    ''' <returns>un action resul</returns>
    Public Function ConfirmObjectionsReceptionC(ObjectionsReceptionC As GlosaObjectionsReceptionC, session As SessionValues) As Domain.Base.Entities.ActionResult Implements IGlosasService.ConfirmObjectionsReceptionC
        Using ObjectionsReceptionAdmin As IObjectionsReceptionCAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IObjectionsReceptionCAdminService)()
            Return ObjectionsReceptionAdmin.ConfirmObjectionsReceptionC(ObjectionsReceptionC, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    '''  anula una objecion
    ''' </summary>
    ''' <param name="ObjectionsReceptionC">objeto objecion</param>
    ''' <param name="session">session</param>
    ''' <returns>un action resul</returns>
    Public Function InvalidateObjectionsReceptionC(ObjectionsReceptionC As GlosaObjectionsReceptionC, session As SessionValues) As Domain.Base.Entities.ActionResult Implements IGlosasService.InvalidateObjectionsReceptionC
        Using ObjectionsReceptionAdmin As IObjectionsReceptionCAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IObjectionsReceptionCAdminService)()
            Return ObjectionsReceptionAdmin.InvalidateObjectionsReceptionC(ObjectionsReceptionC, session.AuditMessageWcf)
        End Using
    End Function


    ''' <summary>
    ''' funcion que retorna la observacion y/o estado actual de como viene la factura a persistir
    ''' </summary>
    ''' <param name="code">codigo estado</param>
    ''' <returns>uan Observacion de fatura</returns>
    'Public Function GetObservationInvoice(code As String, session As SessionValues) As ObservationInvoice Implements IGlosasObjectionsReceptionC.GetObservationInvoice
    '    Using ObjectionsReceptionCAdmin As IObjectionsReceptionCAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IObjectionsReceptionCAdminService)()
    '    Return ObjectionsReceptionCAdmin.GetObservationInvoice(code)
    'End Function


    ''' <summary>
    ''' Función para validar y agregar facturas desde el sp
    ''' </summary>
    ''' <param name="ListInvoices">Lista de facturas</param>
    ''' <param name="Nit">Numero de Nit</param>
    ''' <param name="container">nombre Contenedor</param>
    ''' <returns>Lista de Factura </returns>
    Public Function ValidateListInvoiceSp(ListInvoices As List(Of String), Nit As String, container As String, session As SessionValues) As ActionResult(Of List(Of GlosaObjectionsReceptionD)) Implements IGlosasObjectionsReceptionC.ValidateListInvoiceSp
        Using ObjectionsReceptionAdmin As IObjectionsReceptionCAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IObjectionsReceptionCAdminService)()
            Return ObjectionsReceptionAdmin.ValidateListInvoiceSp(ListInvoices, Nit, container, session)
        End Using
    End Function

    ''' <summary>
    ''' Funcion para cargar dataset de datos de oficio de respuesta
    ''' </summary>
    ''' <param name="Filter">filtro aplicar</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function OfficeReponse(Filter As String, ByVal LevelInvoice As Boolean, session As SessionValues) As DataSet Implements IGlosasObjectionsReceptionC.OfficeReponse
        Using ObjectionsReceptionAdmin As IObjectionsReceptionCAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IObjectionsReceptionCAdminService)()
            Return ObjectionsReceptionAdmin.OfficeReponse(Filter, LevelInvoice, session)
        End Using
    End Function

    ''' <summary>
    ''' Funcion para realizar la exportacion de una recepcion de objeciones a excel
    ''' </summary>
    ''' <param name="IdRecepcion">Id de la recepcion</param>
    ''' <param name="session">valroe de sesion</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ReceptionExcelExport(IdRecepcion As String, session As SessionValues) As DataSet Implements IGlosasObjectionsReceptionC.ReceptionExcelExport
        Using ObjectionsReceptionAdmin As IObjectionsReceptionCAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IObjectionsReceptionCAdminService)()
            Return ObjectionsReceptionAdmin.ReceptionExcelExport(IdRecepcion, session)
        End Using
    End Function
    ''' <summary>
    ''' Funcion para realizar la exportacion de una recepcion de objeciones a excel completo.
    ''' </summary>
    ''' <param name="IdRecepcion">Id de la recepcion</param>
    ''' <param name="session">valroe de sesion</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ReceptionExcelExportFull(IdRecepcion As String, session As SessionValues) As DataTable Implements IGlosasObjectionsReceptionC.ReceptionExcelExportFull
        Using ObjectionsReceptionAdmin As IObjectionsReceptionCAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IObjectionsReceptionCAdminService)()
            Return ObjectionsReceptionAdmin.ReceptionExcelExportFull(IdRecepcion, session)
        End Using
    End Function

#End Region

End Class
