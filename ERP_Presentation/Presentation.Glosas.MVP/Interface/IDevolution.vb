'***********************************************************************
' Assembly         : Presentacion.Glosas.MVP
' Author           : Juan Diego Diaz
' Created          : 2013-06-07
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.Base
Imports  Domain.Entities

#End Region

''' <summary>
''' Interface que declara las propiedades y metodos que debe implementar el frontal de devoluciones
''' </summary>
Public Interface IDevolution
    Inherits IcrudBase

    ''' <summary>
    ''' Asigna el valor de activo o inactivo a los controles
    ''' </summary>
    WriteOnly Property ActionsOnControls As ActionOnControlsTypeDevolutions

    ''' <summary>
    ''' Propiedad que contiene el listado de Sedes o Sucursales
    ''' </summary>
    Property DataSourceBranch As List(Of GlosasParametersInterface)

    ''' <summary>
    ''' Propiedad que contine los datos de la rejilla devolución detalles
    ''' </summary>
    Property SelectedInvoices As Object

    ''' <summary>
    ''' Propiedad que contiene una lista de facturas de devolución
    ''' </summary>
    Property DataSourceInvoices As List(Of SP_invoiceList_Result)
    ''' <summary>
    ''' Propiedad que contiene el listado de conceptos especificos
    ''' </summary>
    Property DataSourceSpecificConcepts As List(Of Domain.Entities.ConceptGlosas)
    ''' <summary>
    ''' Propiedad que contiene el tag del formulario
    ''' </summary>
    ReadOnly Property TagForm As String
    ''' <summary>
    ''' Secuencia numericas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Sequense As Domain.Entities.PortfolioSequence


End Interface

''' <summary>
''' Tipos de accion a ejecutar sobre los controles
''' </summary>
Public Enum ActionOnControlsTypeDevolutions

    ''' <summary>
    ''' Listo para consultar
    ''' </summary>
    WaitingQuery
    ''' <summary>
    ''' Nueva devolución
    ''' </summary>
    NewDevolution
    ''' <summary>
    ''' Devolución sin confirmar
    ''' </summary>
    UnconfirmedDevolution
    ''' <summary>
    ''' Devolución confirmada
    ''' </summary>
    ConfirmedDevolution

    ''' <summary>
    ''' devolucion anulada
    ''' </summary>
    ''' <remarks></remarks>
    invalidateDevolution

End Enum
