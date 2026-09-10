'***********************************************************************
' Assembly         : Presentation.Controls.MVP.IRejilla
' Author           : Sergio Fernandez
' Created          : 07-10-2011
'
' Last Modified By : 
' Last Modified On : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
'Imports SA.IndigoCrystal.Infraestructura.Base
''' <summary>
''' Esta interfaz contiene los metodos y propiedades para ser implementadas en el  control de usuario CtrRejilla
''' </summary>
Public Interface IRejilla(Of T)
#Region "Propiedades de la interfaz"

    ''' <summary>
    ''' Propiedad que obtiene el origen de datos de una enumaracion
    ''' </summary>
    ''' <value>The origende datos.</value>
    WriteOnly Property OrigendeDatos As List(Of T)

    ''' <summary>
    ''' Gets or sets the listado origen datos.
    ''' </summary>
    ''' <value>The listado origen datos.</value>
    Property ListadoOrigenDatos As Object

#End Region
#Region "Metodos"
    ''' <summary>
    ''' Metodo Para Cargar el DataSource
    ''' </summary>
    Sub cargarDataSource()

#End Region
End Interface

