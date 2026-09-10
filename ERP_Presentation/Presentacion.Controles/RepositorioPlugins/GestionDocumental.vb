#Region "Librerias Importadas"
Imports System.ComponentModel.Composition
Imports Presentation.Controls.MVP
Imports Presentation.Base   

#End Region

<Export(GetType(IPlugins))>
Public Class GestionDocumental

    Implements IPlugins

    ''' <summary>
    ''' Esta Funcion se utiliza para ejecutar el plugins de redes sociales.
    ''' </summary>
    ''' <param name="DatosCompartidos"></param>
    ''' <returns>
    ''' Retorna un strin para pintar mensaje en el visor de eventos
    ''' </returns>
    Public Function EjecutarPlugins(ByVal DatosCompartidos As Object) As String Implements MVP.IPlugins.EjecutarPlugins
        Return Nothing
    End Function

    ''' <summary>
    ''' Esta propiedad Obtiene el Icono del Plugins.
    ''' </summary>
    ''' <value>icono plugins.</value>
    Public Property IconoPlugins As EpluginsIcons Implements MVP.IPlugins.IconoPlugins
        Get
            Return Nothing
        End Get
        Set(ByVal value As EpluginsIcons)

        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad Obtiene o establece el nombre de la red social.
    ''' </summary>
    ''' <value>Nombre Red Social</value>
    Public ReadOnly Property NombrePlugins As String Implements MVP.IPlugins.NombrePlugins
        Get
            Return "Gestion Documental"
        End Get
    End Property

    ''' <summary>
    ''' Esta propiedad Obtiene el tipo de plugins para listarlo.
    ''' </summary>
    ''' <value>Tipo Plugins</value>
    Public ReadOnly Property TipoPlugins As EPluginsTypes Implements MVP.IPlugins.TipoPlugins
        Get
            Return EPluginsTypes.GestionDocumental
        End Get
    End Property
End Class
