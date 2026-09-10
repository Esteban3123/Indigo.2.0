'***********************************************************************
' Assembly         : Presentacion.Glosas.MVP
' Author           : Rafael Eduardo Patiño
' Created          : 10-06-2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Librerias Importadas"
Imports Presentation.Base
#End Region

''' <summary>
''' esta interfaz contiene las propiedades y metodos que va implemenmtar nuestra vista y va a controlar nuestro presenter
''' </summary>
''' <remarks></remarks>
Public Interface IResponseHierarchy
    Inherits IcrudBase

#Region "Propiedades"
    ''' <summary>
    ''' Esta propiedad contiene el codigo de la jerarquía
    ''' </summary>
    Property CodeHierarchy As String
    ''' <summary>
    ''' Esta propiedad contiene el nombre de la jerarquía
    ''' </summary>
    Property NameHierarchy As String
    ''' <summary>
    ''' propiedad que contiene la aplicacion del Responsable
    ''' </summary>
    Property Status As Boolean
    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean


#End Region


End Interface