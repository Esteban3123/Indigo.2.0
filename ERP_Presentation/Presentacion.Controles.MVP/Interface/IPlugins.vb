'***********************************************************************
' Assembly         : Presentation.Controls.MVP
' Author           : Julian Cardozo
' Created          : 12-04-2011
'
' Last Modified By : Julian Cardozo
' Last Modified On : 13-04-2011
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Librerias Importadas"

Imports Presentation.Base   
#End Region

Public Interface IPlugins

    ''' <summary>
    ''' Esta propiedad Obtiene o establece el nombre  del plugins.
    ''' </summary>
    ''' <value>Nombre Red Social</value>
    ReadOnly Property NombrePlugins As String

    ''' <summary>
    ''' Esta Funcion se utiliza para ejecutar el plugins .
    ''' </summary>
    ''' <returns>Retorna un strin para pintar mensaje en el visor de eventos </returns>
    Function EjecutarPlugins(ByVal DatosCompartidos As Object) As String

    ''' <summary>
    ''' Esta propiedad Obtiene el tipo de plugins para listarlo.
    ''' </summary>
    ''' <value>Tipo Plugins</value>
    ReadOnly Property TipoPlugins As EPluginsTypes

    ''' <summary> 
    ''' Esta propiedad Obtiene el Icono del Plugins.
    ''' </summary>
    ''' <value> icono plugins.</value>
    Property IconoPlugins As EpluginsIcons


End Interface
