'***********************************************************************
' Assembly         : Application.Glosas
' Author           : RafaelPatiño
' Created          : 11-04-2014
'
' Last Modified By : 
' Last Modified On : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Public Interface IRadicateInvoiceDAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Elimina  un factura del oficio de radicacion de cuentas
    ''' </summary>
    ''' <param name="RadicateInvoiceD">objeto factura de radicacion de cuentas</param>
    ''' <returns></returns>
    Function DeleteInvoiveD(ByVal RadicateInvoiceD As RadicateInvoiceD, ByVal SessionValues As SessionValues) As ActionResult
    ''' <summary>
    ''' Funcion para eliminacion masiva
    ''' </summary>
    ''' <param name="tmpList">lista de item a eliminar</param>
    ''' <param name="SessionValues"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DeleteListInvoiceD(ByVal tmpList As List(Of String), ByVal SessionValues As SessionValues) As ActionResult
    ''' <summary>
    ''' Lista de factura de radicacion con oficio
    ''' </summary>
    ''' <param name="consecutive">numero de radicado del oficio</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetListRadicateD(consecutive As String) As List(Of RadicateInvoiceD)

End Interface
