'***********************************************************************
' Assembly         : Presentacion.Glosas.MVP
' Author           : Rafael Patiño
' Created          : 10-04-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Librerias Importadas"
Imports Presentation.Base
Imports  Domain.Entities

#End Region

Public Interface IRadicateInvoice
    Inherits IcrudBase


    ''' <summary>
    ''' Propiedad que contiene el listado de empresa de parametros interfaces
    ''' </summary>
    Property DataSourceBranch As List(Of GlosasParametersInterface)

    ''' <summary>
    ''' Datasource de informacion de factura
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DataSourceInvoices As List(Of SP_invoiceList_Result)
    ''' <summary>
    ''' Activar o inactivar controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    WriteOnly Property ActionsOnControls As Boolean
    ''' <summary>
    ''' Lista de facturas del oficio
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DataSourceObjectsInvoices As List(Of RadicateInvoiceD)
    ''' <summary>
    ''' Dtermina el estado del oficio
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property StatusDocument As String
    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Property Sequense As Domain.Entities.PortfolioSequence

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    ReadOnly Property MyTag As Object

    WriteOnly Property IdSequence As Int64

End Interface
