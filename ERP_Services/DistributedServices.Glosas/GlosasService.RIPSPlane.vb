'***********************************************************************
' Assembly         : DistributedService.Glosas
' Author           : Daniel Eduardo Arévalo
' Created          : 27-04-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Domain.Entities

Imports Infrastructure.CrossCutting.IOC
Imports Application.Glosas
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Text

#End Region
Partial Class GlosasService
    Implements IGlosasRIPSPlane

    ''' <summary>
    ''' Función para Generar Archivos Planos de RIPS
    ''' </summary>
    ''' <param name="IdRadicateInvoice">Id de Radicación de Cuentas</param>
    ''' <param name="Session">Variable de Sesión</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GenerateRIPSPlane(IdRadicateInvoice As Integer, ConsecutiveRadicateInvoice As String, CodificationType As String, ServiceCode As String, Session As SessionValues, DetailPackage As Boolean, GenerateADPlane As Boolean, Optional InvoicesList As List(Of Integer) = Nothing) As List(Of ActionMessageResult(Of StringBuilder)) Implements IGlosasRIPSPlane.GenerateRIPSPlane
        Using RIPSPlaneAdmin As IRIPSPlaneAdminService = IocFactory.Instance(Session.TransactionalContainer).CurrentContainer.Resolve(Of IRIPSPlaneAdminService)()
            Return RIPSPlaneAdmin.GenerateRIPSPlane(IdRadicateInvoice, ConsecutiveRadicateInvoice, CodificationType, ServiceCode, Session, DetailPackage, GenerateADPlane, InvoicesList)
        End Using
    End Function

    ''' <summary>
    ''' Función para Generar los ARchivos FURIPS
    ''' </summary>
    ''' <param name="IdRadicateInvoice">IdRadicateInvoice</param>
    ''' <param name="Session">Variable de Sesión</param>
    ''' <returns>List(Of ActionMessageResult(Of StringBuilder))</returns>
    ''' <remarks></remarks>
    Public Function GenerateRIPSPlane(IdRadicateInvoice As Integer, Session As SessionValues, Optional InvoicesList As List(Of RIPSBilling) = Nothing) As List(Of ActionMessageResult(Of StringBuilder)) Implements IGlosasRIPSPlane.GenerateFURIPSPlane
        Using RIPSPlaneAdmin As IRIPSPlaneAdminService = IocFactory.Instance(Session.TransactionalContainer).CurrentContainer.Resolve(Of IRIPSPlaneAdminService)()
            Return RIPSPlaneAdmin.GenerateFURIPSPlane(IdRadicateInvoice, Session, InvoicesList)
        End Using
    End Function

    ''' <summary>
    ''' Función para Generar los Archivos FURTRAN
    ''' </summary>
    ''' <param name="Session">Variable de Sesión</param>
    ''' <returns>List(Of ActionMessageResult(Of StringBuilder))</returns>
    ''' <remarks></remarks>
    Public Function GenerateFURTRANPlane(InvoicesList As List(Of RIPSBilling), Session As SessionValues) As List(Of ActionMessageResult(Of StringBuilder)) Implements IGlosasRIPSPlane.GenerateFURTRANPlane
        Using RIPSPlaneAdmin As IRIPSPlaneAdminService = IocFactory.Instance(Session.TransactionalContainer).CurrentContainer.Resolve(Of IRIPSPlaneAdminService)()
            Return RIPSPlaneAdmin.GenerateFURTRANPlane(InvoicesList, Session)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene los registros para generar el archivo plano MegaRIPS
    ''' </summary>
    ''' <param name="radicateInvoiceId">Id del radicado de la factura</param>
    ''' <returns>Lista de registros</returns>
    Public Function GetMegaRIPSByRadicateInvoiceId(radicateInvoiceId As Integer, Session As SessionValues, Optional InvoicesList As List(Of RIPSBilling) = Nothing) As ActionMessageResult(Of String) Implements IGlosasRIPSPlane.GetMegaRIPSByRadicateInvoiceId
        Using RIPSPlaneAdmin As IRIPSPlaneAdminService = IocFactory.Instance(Session.TransactionalContainer).CurrentContainer.Resolve(Of IRIPSPlaneAdminService)()
            Return RIPSPlaneAdmin.GetMegaRIPSByRadicateInvoiceId(radicateInvoiceId, Session.IndigoCompany, InvoicesList)
        End Using
    End Function

    ''' <summary>
    ''' Función que Genera los Archivos de Mega Plano
    ''' </summary>
    ''' <param name="IdRadicateInvoice">Id de Radicación de Cuentas</param>
    ''' <param name="Session">Variable de Sesión</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GenerateRIPSMegaPlane(IdRadicateInvoice As Integer, ConsecutiveRadicateInvoice As String, CodificationType As String, ServiceCode As String, Session As SessionValues, Optional InvoicesList As List(Of Integer) = Nothing) As List(Of ActionMessageResult(Of StringBuilder)) Implements IGlosasRIPSPlane.GenerateRIPSMegaPlane
        Using RIPSPlaneAdmin As IRIPSPlaneAdminService = IocFactory.Instance(Session.TransactionalContainer).CurrentContainer.Resolve(Of IRIPSPlaneAdminService)()
            Return RIPSPlaneAdmin.GenerateRIPSMegaPlane(IdRadicateInvoice, ConsecutiveRadicateInvoice, CodificationType, ServiceCode, Session, InvoicesList)
        End Using
    End Function

End Class
