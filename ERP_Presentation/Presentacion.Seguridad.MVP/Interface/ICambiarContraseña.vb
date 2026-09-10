'***********************************************************************
' Assembly         : Presentacion.Seguridad.MVP
' Author           : Andres Bonilla
' Created          : 03-03-2011
'
' Last Modified By : Andres Bonilla
' Last Modified On : 30-03-2011
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Libererias Importadas"
Imports Presentation.Base
Imports Presentation.Controls
Imports Domain.Security.Entities

#End Region

''' <summary>
''' Esta interfaz contiene los campos de nuestra vista y los que implementa nuestro presentador
''' </summary>
''' <remarks></remarks>
Public Interface ICambiarContraseña
    Inherits IcrudBase
#Region "Metodos"

    ''' <summary>
    ''' Metodo que sirve para guardar los cambios realizados en la contraseña
    ''' </summary>
    Sub Guardar()
    ''' <summary>
    ''' Metodo que Deshace todas las operaciones realizadas y Limpia los controles utilizados
    ''' </summary>
    Sub Deshacer()
    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -> Muestra Guardar | False -> Muestra Actualizar
    ''' </summary>
    ''' <remarks></remarks>
    Sub LogicaBotonActualizar(ByVal existeDatos As Boolean)

    Sub AsyncLoader(ByVal valor As Boolean)
#End Region

#Region "Propiedades"

    ''' <summary>
    ''' esta propiedad contiene la contraseña anterior del usuario
    ''' </summary>
    Property ContraseñaAnterior As String
    ''' <summary>
    ''' esta propiedad contiene la nueva contraseña del usuario
    ''' </summary>
    Property NuevaContraseña As String
    ''' <summary>
    ''' esta propiedad contiene la confirmacion de la contraseña del usuario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ConfirmarContraseña As String
    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    WriteOnly Property Mensaje(ByVal Icono As EeventViewerImages) As String
    ''' <summary>
    ''' Esta Propiedad establece el foco en el control que mande en el parametro
    ''' </summary>
    WriteOnly Property EstablecerFoco(ByVal NombreControl As String) As Boolean
    ''' <summary>
    ''' Esta propiedad se utiliza para Habilitar los controles despues de la contraseña estar correcta
    ''' </summary>
    WriteOnly Property HabilitarControles As Boolean
    ''' <summary>
    ''' propiedad que obtiene o establece el usuario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property User As User

#End Region
End Interface
