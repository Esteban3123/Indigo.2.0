'***********************************************************************
' Assembly         : Domain.Glosas
' Author           : RafaelPatiño
' Created          : 14-06-2013
'
' Last Modified By : 
' Last Modified On :
'
' Copyright        : (c) . All rights reserved.
'*********************************************************************** 



Imports Domain.Entities
Imports Domain.Base
Imports System.Data
Public Interface IRadicateInvoiceDRepository
    Inherits IRepository(Of RadicateInvoiceD)
    ''' <summary>
    ''' valida si las facturas son capitadas capitadas o de ventas
    ''' </summary>
    ''' <param name="radicateId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ValidateInvoiceCapitated(radicateId As Integer) As Integer
    ''' <summary>
    ''' lista las facturas capitadas
    ''' </summary>
    ''' <param name="radicateId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetRadidicateDetailInvoiceCapited(radicateId As Integer) As List(Of ViewRadicateDetailInvoiceCapitated)

    ''' <summary>
    ''' Obtiene factura del detalle de un radicado 
    ''' </summary>
    ''' <param name="invoiceNumber">numero de factura</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetRadicateD(ByVal invoiceNumber As String) As RadicateInvoiceD
    ''' <summary>
    ''' Lista de factura de radicacion con oficio
    ''' </summary>
    ''' <param name="consecutive">numero de radicado del oficio</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetListRadicateD(consecutive As String) As List(Of RadicateInvoiceD)
    ''' <summary>
    ''' Lista de RadicateD a eliminar
    ''' </summary>
    ''' <param name="ListD"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetListDeleteRadicateD(ListD As List(Of String)) As List(Of RadicateInvoiceD)
    ''' <summary>
    ''' Lista de RadicateD para actualizar estado.
    ''' </summary>
    ''' <param name="RadicateCId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetListRadicateDByRadicateCId(RadicateCId As Integer) As List(Of RadicateInvoiceD)
    ''' <summary>
    ''' Eliminacion masiva de RadicateD
    ''' </summary>
    ''' <param name="list"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DeleteMasivo(list As List(Of RadicateInvoiceD)) As List(Of RadicateInvoiceD)
    ''' <summary>
    ''' Funcion para validar que las facturas que estan radicando NO esten ya ingresadas a otro oficio de radicacion
    ''' </summary>
    ''' <param name="list"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListValidateRadicateD(ListStrInvoice As List(Of String)) As List(Of String)

    ''' <summary>
    ''' Obtiene los registros para generar el archivo plano MegaRIPS
    ''' </summary>
    ''' <param name="radicateInvoiceId">Id del radicado de la factura</param>
    ''' <returns>Lista de registros</returns>
    Function GetMegaRIPSByRadicateInvoiceId(ByVal radicateInvoiceId As Integer, companyCode As String) As List(Of SP_GenerateMegaRIPS_Result)

End Interface
