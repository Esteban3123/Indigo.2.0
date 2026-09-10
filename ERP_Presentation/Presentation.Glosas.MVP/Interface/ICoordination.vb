'***********************************************************************
' Assembly         : Presentacion.Glosas.MVP
' Author           : Juan F. Tamayo
' Created          : 2013-07-22
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-07-22
' Description      : Interface del frontal de coordinación
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.Base
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.GlosasRepository

#End Region

''' <summary>
''' Interface del frontal de coordinación
''' </summary>
Public Interface ICoordination
    Inherits ICrudBase

#Region "Properties"

    ''' <summary>
    ''' Asigna un valor que indica que se esta realizacion una operacion asincrona
    ''' </summary>
    ''' <value>Valor</value>
    WriteOnly Property AsyncOperation As Boolean
    ''' <summary>
    ''' Obtiene o asigna la lista de oficios
    ''' </summary>
    ''' <value>Lista de oficios</value>
    ''' <returns>La lista de oficios</returns>
    Property Documents As XPInstantFeedbackSource
    ''' <summary>
    ''' Obtiene o asigna el oficio seleccionado de la rejilla
    ''' </summary>
    ''' <value>Oficio seleccionado de la rejilla</value>
    ''' <returns>El oficio seleccionado de la rejilla</returns>
    Property SelectedDocument As Object
    ''' <summary>
    ''' Obtiene o asigna la lista de detalles de factura
    ''' </summary>
    ''' <value>Lista de detalles de factura</value>
    ''' <returns>La lista de detalles de factura</returns>
    Property InvoiceDetails As Object
    ''' <summary>
    ''' Propiedad que contiene el nombre del usuario que bloqueo la factura.
    ''' </summary>
    Property NameUserWithInvoice As String
    ''' <summary>
    ''' Propiedad que contiene el código del usuario que bloqueo la factura.
    ''' </summary>
    Property CodeUserWithInvoice As String

#End Region

End Interface
