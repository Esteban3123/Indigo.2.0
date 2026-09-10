Imports Presentation.Base   

''' <summary>
''' Interfaz que utiliza la barra de botones para poder comunicarse con el presentador
''' </summary>
Public Interface IBarraBotones

#Region "propiedades"

    ''' <summary>
    ''' establece el valor del boton que se va a mostrar (bBotonTag -> configuracion del tag desde diseño)
    ''' </summary>
    WriteOnly Property Boton(ByVal bBotonTag As Integer) As Boolean
    ''' <summary>
    ''' obtiene o establece un valor que me especifica si el frontal permite o no customizar 
    ''' </summary>
    ''' <value>
    ''' <c>true</c> if [permitir customizar funcional]; otherwise, <c>false</c>.
    ''' </value>
    Property PermitirCustomizarFuncional As Boolean

    Property ClicBotonActualizar As Boolean
    ''' <summary>
    ''' establece el valor de la propiedad que almacena los permisos por formulario
    ''' </summary>
    Property PermissionsForm As Dictionary(Of Integer, String)

#End Region

End Interface
