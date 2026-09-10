'***********************************************************************
' Assembly         : Presentacion.Glosas.MVP
' Author           : Juan Diego Diaz
' Created          : 2013-07-03
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.Base
Imports  Domain.Entities

#End Region

''' <summary>
''' Interface que declara las propiedades y metodos que debe implementar el frontal de evaluaciones
''' </summary>
Public Interface IEvaluation
    Inherits IcrudBase

    ''' <summary>
    ''' Propiedad que contiene el listado de detalles de facturas.
    ''' </summary>
    Property DataSourceDetail As List(Of GlosaInvoiceDetail)
    ''' <summary>
    ''' Propiedad que contiene una lista de facturas.
    ''' </summary>
    Property DataSourceInvoices As Object
    ''' <summary>
    ''' Propiedad que contiene el nombre del usuario que bloqueo la factura.
    ''' </summary>
    Property NameUserWithInvoice As String
    ''' <summary>
    ''' Propiedad que contiene el código del usuario que bloqueo la factura.
    ''' </summary>
    Property CodeUserWithInvoice As String

End Interface

