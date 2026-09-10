'***********************************************************************
' Assembly         : Presentacion.Seguridad.MVP
' Author           : Andres Bonilla
' Created          : 03-03-2011
'
' Last Modified By : Andres Bonilla
' Last Modified On : 24-03-2011
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Librerias Importadas"
Imports Presentation.Controls
Imports Presentation.Base   
#End Region

''' <summary>
''' esta interfaz contiene las propiedades y metodos que va implemenmtar nuestra vista y va a controlar nuestro presenter
''' </summary>
''' <remarks></remarks>
Public Interface IDesbloquearUsuario

#Region "Propiedades"

    ''' <summary>
    ''' Esta propiedad contiene el codigo del usuario seleccionado
    ''' </summary>
    Property CodigoDelUsuario As String
    ''' <summary>
    ''' Esta propiedad contiene el nombre del usuario
    ''' </summary>
    Property NombreDelUsuario As String
    ''' <summary>
    ''' Esta propiedad sirve para permitir o no permitir algunos controles de la vista
    ''' </summary>
    WriteOnly Property HabilitarControles As Boolean
    ''' <summary>
    ''' Establece el valor de un mensaje que se va a mostrar en el visor de eventos
    ''' </summary>
    WriteOnly Property Mensaje(ByVal Icono As EeventViewerImages) As String

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Este metodo sirve para abrir el frontal lista de usuarios
    ''' </summary>
    Sub AbrirFormularioBusqueda()

    Sub AsyncLoader(ByVal Value As Boolean)

#End Region

End Interface
