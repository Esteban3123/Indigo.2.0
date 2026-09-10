'************************************************************
' Assembly         : Domain.Billing
' Author           : Diego Andrés Roldán Lozano
' Created          : 18-06-2015
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports System.Threading.Tasks
Imports Domain.Base
Imports Domain.Entities

#End Region

Public Interface IInvoiceEntityCapitedRepository
    Inherits IRepository(Of InvoiceEntityCapitated)

    ''' <summary>
    ''' Obtiene una factura por id
    ''' </summary>
    Function GetInvoiceEntityCapitatedById(ByVal Id As Integer) As InvoiceEntityCapitated

    ''' <summary>
    ''' Obtiene una factura de entidad capitada por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetInvoiceEntityCapitated(code As String) As InvoiceEntityCapitated

    ''' <summary>
    ''' Obtiene los registros de control de facturas capitadas
    ''' </summary>
    ''' <param name="initialDate">Fecha inicial de busqueda</param>
    ''' <param name="finalDate">Fecha final de busqueda</param>
    ''' <param name="careGroupId">Id del grupo de atención</param>
    ''' <returns></returns>
    Function GetCapitationControlRegistry(initialDate As DateTime, finalDate As DateTime, careGroupId As Integer) As Task(Of List(Of InvoiceDetail))

    ''' <summary>
    ''' Guarda, Actualiza o Confirma una factura de monto fijo
    ''' </summary>
    ''' <param name="invoiceEntityCapitatedXml"></param>
    ''' <param name="userCode"></param>
    ''' <returns></returns>
    Function SP_SaveInvoiceEntityCapitated(invoiceEntityCapitatedXml As String, userCode As String) As SP_SaveInvoiceEntityCapitated_Result

End Interface