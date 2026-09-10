'***********************************************************************
' Assembly         : Presentacion.Glosas.MVP
' Author           : Juan Diego Diaz Mosquera
' Created          : 27-01-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Librerias Importadas"
Imports Presentation.Base
#End Region

''' <summary>
''' Esta interfaz contiene las propiedades y metodos que va implemenmtar nuestra vista y va a controlar nuestro presenter
''' </summary>
''' <remarks></remarks>
Public Interface IResponsibleTransfer
    Inherits IcrudBase

    Property DataSourceResponsibleGle As Object

End Interface