
'***********************************************************************
' Assembly         : Presentation.Controls.MVP
' Author           : JulianCardozo
' Created          : 12-04-2011
'
' Last Modified By : JulianCardozo
' Last Modified On : 13-04-2011
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Librerias Importadas"

Imports System.ComponentModel.Composition
Imports System.ComponentModel.Composition.Hosting
Imports System.Reflection
Imports Presentation.Controls.MVP


#End Region

''' <summary>
''' Clase que se utiliza para formar el catalogo de plugins y agregarlos al contenedor 
''' </summary>
Public Class EjecucionPlugins

    ''' <summary>
    ''' Esta propiedad obtiene el listado de plugins con sus respectivas propiedades.
    ''' </summary>
    ''' <value>The plugins aplicacion.</value>
    <ImportMany()>
    Public Property PluginsAplicacion As List(Of IPlugins)

    ''' <summary>
    ''' Inicializa y carga los plugins en el contenedor
    ''' </summary>
    Public Sub CargarPlugins()
        Dim CatalogoPlugins As New AssemblyCatalog(Assembly.GetExecutingAssembly)
        Dim Contenedor As New CompositionContainer(CatalogoPlugins)
        Contenedor.ComposeParts(Me)
    End Sub


    ''' <summary>
    ''' Ejecutar Plugins recibiendo datos del funional.
    ''' </summary>
    ''' <param name="DatosCompartidos">son las diferentes variables que se traen desde el frontal.</param>
    ''' <param name="Indice">el indice del Plugins dentro de la lista para saber cual ejecutar.</param>
    ''' <returns></returns>
    Public Function EjecutarPlugins(ByVal DatosCompartidos As Object, ByVal Indice As Integer) As String
        Return PluginsAplicacion(Indice).EjecutarPlugins(DatosCompartidos)
    End Function


End Class
