'***********************************************************************
' Assembly         : Presentacion.Glosas.MVP
' Author           : Jorge Leonardo Vernaza
' Created          : 06-04-2013
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
Public Interface IResponsible
    Inherits IcrudBase
#Region "Propiedades"
    ''' <summary>
    ''' Esta propiedad contiene el codigo del Responsable
    ''' </summary>
    Property CodeResponsible As String
    ''' <summary>
    ''' Esta propiedad contiene el nombre del Responsable
    ''' </summary>
    Property NameResponsible As String
    ''' <summary>
    ''' Propiedad que contiene el tipo
    ''' </summary>
    Property ERPCodeResponsible As String
    ''' <summary>
    ''' propiedad que contiene el tipo del Responsable
    ''' </summary>
    Property ChargeResponsible As String
    ''' <summary>
    ''' propiedad que contiene la aplicacion del Responsable
    ''' </summary>
    Property StatusResponsible As Boolean
    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean


#End Region


End Interface