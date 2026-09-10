'***********************************************************************
' Assembly         : Presentacion.Glosas.MVP
' Author           : rafael Patiño
' Created          : 17-049-2014
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
Public Interface IenlistmentConciliation
    Inherits IcrudBase
#Region "Propiedades"

    WriteOnly Property ActionsOnControls As Boolean


#End Region


End Interface