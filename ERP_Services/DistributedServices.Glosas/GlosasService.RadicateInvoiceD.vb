'***********************************************************************
' Assembly         : DistributedService.Glosas
' Author           : Rafael Patiño
' Created          : 11-04-2014
'
' Last Modified By : 
' Last Modified On : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports Domain.Entities

Imports Infrastructure.CrossCutting.IOC
Imports Application.Glosas
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region

Partial Public Class GlosasService

    ''' <summary>
    ''' Elimina  una factura de un oficio de radicacion de cuentas
    ''' </summary>
    ''' <param name="RadicateInvoiceD">objeto radicacion de cuentas</param>
    ''' <returns></returns>
    Public Function DeleteInvoiveD(RadicateInvoiceD As RadicateInvoiceD, session As SessionValues) As Domain.Base.Entities.ActionResult Implements IGlosasRadicateInvoiceD.DeleteInvoiveD
        Using RadicateInvoiceDAdmin As IRadicateInvoiceDAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IRadicateInvoiceDAdminService)()
            Return RadicateInvoiceDAdmin.DeleteInvoiveD(RadicateInvoiceD, session)
        End Using
    End Function
    ''' <summary>
    ''' Función para validar y agregar facturas desde el sp
    ''' </summary>
    ''' <param name="ListInvoices">Lista de facturas</param>
    ''' <param name="Nit">Numero de Nit</param>
    ''' <param name="container">nombre Contenedor</param>
    ''' <returns>Lista de Factura </returns>
    Public Function ValidateListInvoiceRadicateDSp(ListInvoices As List(Of String), Nit As String, container As String, IndigoCompany As String, session As SessionValues) As Domain.Base.Entities.ActionResult(Of List(Of RadicateInvoiceD)) Implements IGlosasRadicateInvoiceD.ValidateListInvoiceRadicateDSp
        Using RadicateInvoiceCAdmin As IRadicateInvoiceAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IRadicateInvoiceAdminService)()
            Return RadicateInvoiceCAdmin.ValidateListInvoiceRadicateDSp(ListInvoices, Nit, container, session.TransactionalContainer, session)
        End Using
    End Function
    ''' <summary>
    ''' Funcion para eliminacion masiva
    ''' </summary>
    ''' <param name="tmpList">lista de item a eliminar</param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteListInvoiceD(tmpList As List(Of String), session As SessionValues) As Domain.Base.Entities.ActionResult Implements IGlosasRadicateInvoiceD.DeleteListInvoiceD
        Using RadicateInvoiceDAdmin As IRadicateInvoiceDAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IRadicateInvoiceDAdminService)()
            Return RadicateInvoiceDAdmin.DeleteListInvoiceD(tmpList, session)
        End Using
    End Function
    ''' <summary>
    ''' Lista de factura de radicacion con oficio
    ''' </summary>
    ''' <param name="consecutive">numero de radicado del oficio</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListRadicateD(consecutive As String, session As SessionValues) As List(Of RadicateInvoiceD) Implements IGlosasRadicateInvoiceD.GetListRadicateD
        Using RadicateInvoiceDAdmin As IRadicateInvoiceDAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IRadicateInvoiceDAdminService)()
            Return RadicateInvoiceDAdmin.GetListRadicateD(consecutive)
        End Using
    End Function

End Class
