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
    ''' Obtener un oficio de facturas radicadas por el id sin agregados
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Function GetInvocieRadicateByIdSimple(Id As Integer, session As SessionValues) As RadicateInvoiceC Implements IGlosasRadicateInvoiceC.GetInvocieRadicateByIdSimple
        Using RadicateInvoiceCAdmin As IRadicateInvoiceAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IRadicateInvoiceAdminService)()
            Return RadicateInvoiceCAdmin.GetInvocieRadicateByIdSimple(Id)
        End Using
    End Function
    ''' <summary>
    ''' Elimina  un oficio de radicacion de cuentas
    ''' </summary>
    ''' <param name="RadicateInvoiceC">objeto radicacion de cuentas</param>
    ''' <returns></returns>
    Public Function DeleteSaveRadicateInvoiveC(RadicateInvoiceC As RadicateInvoiceC, session As SessionValues) As Domain.Base.Entities.ActionResult Implements IGlosasRadicateInvoiceC.DeleteSaveRadicateInvoiveC
        Using RadicateInvoiceCAdmin As IRadicateInvoiceAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IRadicateInvoiceAdminService)()
            Return RadicateInvoiceCAdmin.DeleteSaveRadicateInvoiveC(RadicateInvoiceC, session.AuditMessageWcf)
        End Using
    End Function
    ''' <summary>
    ''' Obtener un oficio de facturas radicadas por medio del consecutivo
    ''' </summary>
    ''' <param name="consecutive">consecutivo del oficio</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetInvocieRadicate(consecutive As String, session As SessionValues) As RadicateInvoiceC Implements IGlosasRadicateInvoiceC.GetInvocieRadicate
        Using RadicateInvoiceCAdmin As IRadicateInvoiceAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IRadicateInvoiceAdminService)()
            Return RadicateInvoiceCAdmin.GetInvocieRadicate(consecutive, session.AuditMessageWcf)
        End Using
    End Function
    ''' <summary>
    ''' Listar Oficio de Factura Radicadas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListInvocieRadicate(session As SessionValues) As List(Of RadicateInvoiceC) Implements IGlosasRadicateInvoiceC.ListInvocieRadicate
        Using RadicateInvoiceCAdmin As IRadicateInvoiceAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IRadicateInvoiceAdminService)()
            Return RadicateInvoiceCAdmin.ListInvocieRadicate(session.AuditMessageWcf)
        End Using
    End Function
    ''' <summary>
    ''' guardar un oficio de radicacion de cuentas
    ''' </summary>
    ''' <param name="RadicateInvoiceC">objeto radicacion de cuentas</param>
    ''' <param name="session">mensaje de auditoria</param>
    ''' <returns>valor de guardado</returns>
    Public Function SaveRadicateInvoiveC(RadicateInvoiceC As RadicateInvoiceC, session As SessionValues) As Domain.Base.Entities.ActionResult(Of RadicateInvoiceC) Implements IGlosasRadicateInvoiceC.SaveRadicateInvoiveC
        Using RadicateInvoiceCAdmin As IRadicateInvoiceAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IRadicateInvoiceAdminService)()
            Return RadicateInvoiceCAdmin.SaveRadicateInvoiveC(RadicateInvoiceC, session)
        End Using
    End Function

    ''' <summary>
    ''' Funcion para guardar y confirmar
    ''' </summary>
    ''' <param name="ObjRadicateInvoiceC"></param>
    ''' <param name="idSequence"></param>
    ''' <param name="IndigoSessionValues"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveAndConfirmRadicateC(ObjRadicateInvoiceC As RadicateInvoiceC, idSequence As Integer, IndigoSessionValues As SessionValues) As Domain.Base.Entities.ActionResult(Of RadicateInvoiceC) Implements IGlosasRadicateInvoiceC.SaveAndConfirmRadicateC
        Using RadicateInvoiceCAdmin As IRadicateInvoiceAdminService = IocFactory.Instance(IndigoSessionValues.TransactionalContainer).CurrentContainer.Resolve(Of IRadicateInvoiceAdminService)()
            Return RadicateInvoiceCAdmin.SaveAndConfirm(ObjRadicateInvoiceC, idSequence, IndigoSessionValues)
        End Using
    End Function
    ''' <summary>
    ''' Confirmar Oficio y factura de Radicacion de facturas
    ''' </summary>
    ''' <param name="InvoiceRadicateC"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ConfirmInvoiceRadicateC(InvoiceRadicateC As RadicateInvoiceC, idSequence As Integer, session As SessionValues) As Domain.Base.Entities.ActionResult Implements IGlosasRadicateInvoiceC.ConfirmInvoiceRadicateC
        Using RadicateInvoiceCAdmin As IRadicateInvoiceAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IRadicateInvoiceAdminService)()
            Return RadicateInvoiceCAdmin.ConfirmInvoiceRadicateC(InvoiceRadicateC, idSequence, session)
        End Using
    End Function


    ''' <summary>
    ''' Actualizar fecha de confirmacion
    ''' </summary>
    ''' <param name="NumberRadicate"></param>
    ''' <param name="NewDate"></param>
    ''' <param name="IndigoSessionValues"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateDateConfirm(NumberRadicate As String, NewDate As Date, IndigoSessionValues As SessionValues) As Domain.Base.Entities.ActionResult Implements IGlosasRadicateInvoiceC.UpdateDateConfirm
        Using RadicateInvoiceCAdmin As IRadicateInvoiceAdminService = IocFactory.Instance(IndigoSessionValues.TransactionalContainer).CurrentContainer.Resolve(Of IRadicateInvoiceAdminService)()
            Return RadicateInvoiceCAdmin.UpdateDateConfirm(NumberRadicate, NewDate, IndigoSessionValues)
        End Using
    End Function

    ''' <summary>
    ''' ''' Obtener la trazabilidad de una factura
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SP_InvoiceTraceability(container As String, invoice As String, IndigoSessionValues As SessionValues) As SP_InvoiceTraceability_Result Implements IGlosasRadicateInvoiceC.SP_InvoiceTraceability
        Using RadicateInvoiceCAdmin As IRadicateInvoiceAdminService = IocFactory.Instance(IndigoSessionValues.TransactionalContainer).CurrentContainer.Resolve(Of IRadicateInvoiceAdminService)()
            Return RadicateInvoiceCAdmin.SP_InvoiceTraceability(container, invoice, IndigoSessionValues)
        End Using
    End Function

    ''' <summary>
    ''' ''' Obtener la trazabilidad de una factura
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SP_InvoiceTraceabilityConciliation(invoiceNumber As String, IndigoSessionValues As SessionValues) As List(Of SP_InvoiceTraceabilityConciliation_Result) Implements IGlosasRadicateInvoiceC.SP_InvoiceTraceabilityConciliation
        Using RadicateInvoiceCAdmin As IRadicateInvoiceAdminService = IocFactory.Instance(IndigoSessionValues.TransactionalContainer).CurrentContainer.Resolve(Of IRadicateInvoiceAdminService)()
            Return RadicateInvoiceCAdmin.SP_InvoiceTraceabilityConciliation(invoiceNumber, IndigoSessionValues)
        End Using
    End Function

    ''' <summary>
    ''' ''' Obtener la trazabilidad de una factura
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SP_InvoiceTraceabilityDevolution(invoiceNumber As String, IndigoSessionValues As SessionValues) As List(Of SP_InvoiceTraceabilityDevolution_Result) Implements IGlosasRadicateInvoiceC.SP_InvoiceTraceabilityDevolution
        Using RadicateInvoiceCAdmin As IRadicateInvoiceAdminService = IocFactory.Instance(IndigoSessionValues.TransactionalContainer).CurrentContainer.Resolve(Of IRadicateInvoiceAdminService)()
            Return RadicateInvoiceCAdmin.SP_InvoiceTraceabilityDevolution(invoiceNumber, IndigoSessionValues)
        End Using
    End Function

    ''' <summary>
    ''' ''' Obtener la trazabilidad de una factura
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SP_InvoiceTraceabilityRadication(invoice As String, IndigoSessionValues As SessionValues) As List(Of SP_InvoiceTraceabilityRadication_Result) Implements IGlosasRadicateInvoiceC.SP_InvoiceTraceabilityRadication
        Using RadicateInvoiceCAdmin As IRadicateInvoiceAdminService = IocFactory.Instance(IndigoSessionValues.TransactionalContainer).CurrentContainer.Resolve(Of IRadicateInvoiceAdminService)()
            Return RadicateInvoiceCAdmin.SP_InvoiceTraceabilityRadication(invoice, IndigoSessionValues)
        End Using
    End Function

    ''' <summary>
    ''' Carga info para la generación del reporte de trazabilidad
    ''' </summary>
    ''' <param name="container"></param>
    ''' <param name="invoice"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function dtsTrazabilityReports(container As String, invoice As String, session As SessionValues) As DataSet Implements IGlosasRadicateInvoiceC.dtsTrazabilityReports
        Using RadicateInvoiceCAdmin As IRadicateInvoiceAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IRadicateInvoiceAdminService)()
            Return RadicateInvoiceCAdmin.dtsTrazabilityReports(container, invoice, session)
        End Using
    End Function

End Class
