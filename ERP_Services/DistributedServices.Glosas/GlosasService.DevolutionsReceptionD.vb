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
    ''' Elimina detalles de devolución.
    ''' </summary>
    ''' <param name="DevolucionD">Lista devolución Detalle</param>
    ''' <returns>ActionResult</returns>
    Public Function DeleteDevolutionD(DevolucionD As List(Of GlosaDevolutionsReceptionD), session As SessionValues) As Domain.Base.Entities.ActionResult Implements IGlosasDevolutionsReceptionD.DeleteDevolutionD
        Using devolutionD As IDevolutionsReceptionDAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IDevolutionsReceptionDAdminService)()
            Return devolutionD.DeleteDevolutionD(DevolucionD, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un detalle de devolución especifico.
    ''' </summary>
    ''' <param name="Id">Id de devolución detalle</param>
    ''' <returns>Objeto devolución Detalle</returns>
    Public Function GetDevolutionDByIdDevolutionD(Id As String, session As SessionValues) As GlosaDevolutionsReceptionD Implements IGlosasDevolutionsReceptionD.GetDevolutionDByIdDevolutionD
        Using devolutionD As IDevolutionsReceptionDAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IDevolutionsReceptionDAdminService)()
            Return devolutionD.GetDevolutionDByIdDevolutionD(Id)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene todos los detalles de conciliacion.
    ''' </summary>
    ''' <returns>ActionResult</returns>
    Public Function ListAllDevolutionD(session As SessionValues) As List(Of GlosaDevolutionsReceptionD) Implements IGlosasDevolutionsReceptionD.ListAllDevolutionD
        Using devolutionD As IDevolutionsReceptionDAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IDevolutionsReceptionDAdminService)()
            Return devolutionD.ListAllDevolutionD
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un detalle de devolución especifico.
    ''' </summary>
    ''' <param name="Id">Id de devolución cabecera</param>
    ''' <returns>Lista devolución Detalle</returns>
    Public Function ListDevolutionDByIdDevolutionnC(Id As String, session As SessionValues) As List(Of GlosaDevolutionsReceptionD) Implements IGlosasDevolutionsReceptionD.ListDevolutionDByIdDevolutionnC
        Using devolutionD As IDevolutionsReceptionDAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IDevolutionsReceptionDAdminService)()
            Return devolutionD.ListDevolutionDByIdDevolutionnC(Id)
        End Using
    End Function

    ''' <summary>
    ''' guarda detalles de devolución.
    ''' </summary>
    ''' <param name="DevolucionD">Lista devolución Detalle</param>
    ''' <returns>ActionResult</returns>
    Public Function SaveDevolutionD(DevolucionD As List(Of GlosaDevolutionsReceptionD), session As SessionValues) As Domain.Base.Entities.ActionResult Implements IGlosasDevolutionsReceptionD.SaveDevolutionD
        Using devolutionD As IDevolutionsReceptionDAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IDevolutionsReceptionDAdminService)()
            Return devolutionD.SaveDevolutionD(DevolucionD, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' eliminar factura devolucion
    ''' </summary>
    ''' <param name="GlosaDevolutionsReceptionD"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteLDevolution(GlosaDevolutionsReceptionD As GlosaDevolutionsReceptionD, session As SessionValues) As Domain.Base.Entities.ActionResult Implements IGlosasDevolutionsReceptionD.DeleteDevolution
        Using devolutionD As IDevolutionsReceptionDAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IDevolutionsReceptionDAdminService)()
            Return devolutionD.DeleteDevolution(GlosaDevolutionsReceptionD, session)
        End Using
    End Function

    ''' <summary>
    ''' Eliminar lista de eliminacion de devolucion
    ''' </summary>
    ''' <param name="tmpList"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteListDevolution(tmpList As List(Of GlosaDevolutionsReceptionD), session As SessionValues) As Domain.Base.Entities.ActionResult Implements IGlosasDevolutionsReceptionD.DeleteListDevolution
        Using devolutionD As IDevolutionsReceptionDAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IDevolutionsReceptionDAdminService)()
            Return devolutionD.DeleteListDevolution(tmpList, session)
        End Using
    End Function


    ''' <summary>
    ''' Función para validar y agregar facturas desde el sp
    ''' </summary>
    ''' <param name="ListInvoices">Lista de facturas</param>
    ''' <param name="Nit">Numero de Nit</param>
    ''' <param name="container">nombre Contenedor</param>
    ''' <returns>Lista de Factura </returns>
    Public Function ValidateListInvoiceDevolutionSp(ListInvoices As List(Of String), Nit As String, container As String, session As SessionValues) As Domain.Base.Entities.ActionResult(Of List(Of GlosaDevolutionsReceptionD)) Implements IGlosasDevolutionsReceptionD.ValidateListInvoiceDevolutionSp
        Using ObjectionsReceptionAdmin As IMovementDevolutionsAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IMovementDevolutionsAdminService)()
            Return ObjectionsReceptionAdmin.ValidateListInvoiceDevolutionSp(ListInvoices, Nit, container, session.TransactionalContainer, session)
        End Using
    End Function

End Class
