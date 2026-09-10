'***********************************************************************
' Assembly         : Presentacion.Glosas.MVP
' Author           : Juan Diego Diaz M.
' Created          : 01-08-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Librerias Importadas"
Imports Presentation.Base
Imports Domain.Entities
#End Region

''' <summary>
''' esta interfaz contiene las propiedades y metodos que va implemenmtar nuestra vista y va a controlar nuestro presenter
''' </summary>
''' <remarks></remarks>
Public Interface IJustificationTemplate
    Inherits IcrudBase
#Region "Propiedades"

    ''' <summary>
    ''' Propiedad que contiene el listado de Conceptos
    ''' </summary>
    Property DataSourceConcepts As List(Of ConceptGlosas)
    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

#End Region

End Interface