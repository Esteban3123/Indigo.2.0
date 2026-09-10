'***********************************************************************
' Assembly         : Presentacion.Glosas.MVP
' Author           : Juan F. Tamayo
' Created          : 2013-04-19
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-04-19
' Description      : Interface del frontal de conciliación
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.Base

#End Region

''' <summary>
''' Interface que declara las propiedades y metodos que debe implementar el frontal de conciliación
''' </summary>
Public Interface IConciliation
    Inherits IcrudBase

#Region "Properties"

    ''' <summary>
    ''' Obtiene o asigna el numero consecutivo de la conciliacion
    ''' </summary>
    ''' <value>Numero de la conciliacion</value>
    ''' <returns>Numero de la conciliacion</returns>
    Property Consecutive As Long
    ''' <summary>
    ''' Obtiene o asigna el nit de la entidad
    ''' </summary>
    ''' <value>Nit de la entidad</value>
    ''' <returns>Nit de la entidad</returns>
    Property Nit As String
    ''' <summary>
    ''' Obtiene o asigna el codigo o numero del oficio de la cociliacion
    ''' </summary>
    ''' <value>Codigo o numero del oficio</value>
    ''' <returns>Codigo o numero del oficio</returns>
    Property Document As String
    ''' <summary>
    ''' Obtiene o asigna la fecha en que se realiza la conciliacion
    ''' </summary>
    ''' <value>Fecha de la conciliacion</value>
    ''' <returns>Fecha de la conciliacion</returns>
    Property DateConciliation As DateTime
    ''' <summary>
    ''' Obtiene o asigna la fecha del oficio
    ''' </summary>
    ''' <value>Fecha del oficio</value>
    ''' <returns>Fecha del oficio</returns>
    Property DateDocument As DateTime
    ''' <summary>
    ''' Obtiene o asigna el comentario de observacion en la conciliacion
    ''' </summary>
    ''' <value>Comentario de observacion</value>
    ''' <returns>Comentario de observacion</returns>
    Property Comment As String
    ''' <summary>
    ''' Asigna el valor de activo o inactivo a los controles
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean
    ''' <summary>
    ''' Obtiene o asigna la lista de facturas seleccionadas
    ''' </summary>
    ''' <value>Lista de facturas seleccionadas</value>
    ''' <returns>Lista de facturas seleccionadas</returns>
    Property SelectedInvoices As Object
    ''' <summary>
    ''' Obtiene o asigna los participantes
    ''' </summary>
    ''' <value>Lista de participantes</value>
    ''' <returns>La lista de participantes</returns>
    Property Participants As Object

#End Region

End Interface