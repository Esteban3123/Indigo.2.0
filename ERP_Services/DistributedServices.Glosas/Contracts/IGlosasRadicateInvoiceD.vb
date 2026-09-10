Imports System.ServiceModel
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()> _
Public Interface IGlosasRadicateInvoiceD

    ''' <summary>
    ''' Elimina  una factura de un oficio de radicacion de cuentas
    ''' </summary>
    ''' <param name="RadicateInvoiceD">objeto radicacion de cuentas</param>
    ''' <returns></returns>
    <OperationContract>
    Function DeleteInvoiveD(ByVal RadicateInvoiceD As RadicateInvoiceD, ByVal session As SessionValues) As ActionResult

    ''' <summary>
    ''' Funcion para eliminacion masiva
    ''' </summary>
    ''' <param name="tmpList">lista de item a eliminar</param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract>
    Function DeleteListInvoiceD(ByVal tmpList As List(Of String), ByVal session As SessionValues) As ActionResult
    ''' <summary>
    ''' Función para validar y agregar facturas desde el sp
    ''' </summary>
    ''' <param name="ListInvoices">Lista de facturas</param>
    ''' <param name="Nit">Numero de Nit</param>
    ''' <param name="container">nombre Contenedor</param>
    ''' <returns>Lista de Factura </returns>
    <OperationContract>
    Function ValidateListInvoiceRadicateDSp(ListInvoices As List(Of String), Nit As String, container As String, IndigoCompany As String, ByVal session As SessionValues) As ActionResult(Of List(Of RadicateInvoiceD))

    ''' <summary>
    ''' Lista de factura de radicacion con oficio
    ''' </summary>
    ''' <param name="consecutive">numero de radicado del oficio</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract>
    Function GetListRadicateD(consecutive As String, session As SessionValues) As List(Of RadicateInvoiceD)

End Interface
