'***********************************************************************
' Assembly         : Presentacion.Glosas.MVP
' Author           : Cristhian Mauricio Salazar
' Created          : 21/09/2014
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Presentation.Base
Imports Domain.Entities
Imports DevExpress.Xpo

Public Interface IInvoiceTraceability
    Inherits ICrudBase

    ''' <summary>
    ''' Propiedad que contiene el listado de empresa de parametros interfaces
    ''' </summary>
    Property DataSourceBranch As List(Of GlosasParametersInterface)

    ''' <summary>
    ''' Propiedad que contiene el listado de empresa de parametros interfaces
    ''' </summary>
    Property InvoiceTraceability As SP_InvoiceTraceability_Result

    ''' <summary>
    ''' Propiedad que contiene el listado de empresa de parametros interfaces
    ''' </summary>
    WriteOnly Property InvoiceTraceabilityConciliation As List(Of SP_InvoiceTraceabilityConciliation_Result)

    ''' <summary>
    ''' Propiedad que contiene el listado de empresa de parametros interfaces
    ''' </summary>
    WriteOnly Property InvoiceTraceabilityDevolution As List(Of SP_InvoiceTraceabilityDevolution_Result)

    ''' <summary>
    ''' Propiedad que contiene el listado de empresa de parametros interfaces
    ''' </summary>
    WriteOnly Property InvoiceTraceabilityRadication As List(Of SP_InvoiceTraceabilityRadication_Result)

    ''' <summary>
    ''' propiedad para activar e inactivar controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' propiedad que obtiene o establece el id de la moneda, ademas establece la abreviacion
    ''' </summary>
    ''' <param name="currencyAbbreviation"></param>
    ''' <returns></returns>
    Property CurrencyId(Optional currencyAbbreviation As String = Nothing) As Integer

#Region "XPO"

    ''' <summary>
    ''' datasource de la moneda
    ''' </summary>
    ''' <returns></returns>
    Property CurrencyXpo As XPInstantFeedbackSource

#End Region
End Interface
