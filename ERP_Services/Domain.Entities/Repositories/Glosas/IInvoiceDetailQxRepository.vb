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

Public Interface IInvoiceDetailQxRepository
    Inherits IRepository(Of GlosaInvoiceDetailQX)

    ''' <summary>
    ''' Funcion que retorna una lista de detalle de factura QX 
    ''' </summary>
    ''' <param name="InvoiceDetailId">el Codigo del detalle de factura</param>
    ''' <returns>lista de detallle factura QX</returns>
    Function ListGlosaInvoiceDetailQX(InvoiceDetailId As String) As List(Of GlosaInvoiceDetailQX)

    ''' <summary>
    ''' Funcion que retorna un objeto detalle de factura tipo qx
    ''' </summary>
    ''' <param name="InvoiceDetailQXId">Codigo del detalle qx</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetGlosaInvoiceDetailQX(InvoiceDetailQXId As String) As GlosaInvoiceDetailQX

End Interface
