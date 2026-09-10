'***********************************************************************
' Assembly         : Presentation.Security
' Author           : OscarSierra
' Created          : 31-10-2011
'
' Last Modified By : Oscar Sierra
' Last Modified On : 31-10-2011
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Liberias Importadas"
Imports System.IO
Imports System.Data
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base

#End Region


''' <summary>
''' clase que contiene el metodo para leer los parametros del archivo de configuracion
''' </summary>
Public Class GenesisLogin

    ''' <summary>
    ''' metodo para leer los parametros del archivo de configuracion
    ''' </summary>
    Public Shared Sub LeerConfiguracion()
        Try
            Dim ValoresSesion As SessionValues = SessionValues.Instance
            'verifico que exista el archivo
            'If File.Exists(String.Concat(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "\", "Indigo Technologies", "\", "Indigo Crystal", "\", "Configuracion.IndigoCrystal")) = False Then
            If File.Exists(String.Concat(Window.Utils.LocalFolder(), "\", "Vie HealtTech", "\", "Indigo Vie EHR", "\", "Configuracion.IndigoCrystal")) = False Then
                Throw New FileNotFoundException(String.Concat(BaseClass.obtenerExcepcion(EexceptionsResources.ArchivoNoExiste), " '", "Configuracion.IndigoCrystal", "'"), "Configuracion.IndigoCrystal")
            End If
            'leo el archivo
            Dim ConfiguracionXML As New DataTable("ConfiguracionIndigoCrystal")
            With ConfiguracionXML
                .Columns.Add("CacheLocal")
                'ConfiguracionXML.ReadXml(String.Concat(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "\", "Indigo Technologies", "\", "Indigo Crystal", "\", "Configuracion.IndigoCrystal"))
                ConfiguracionXML.ReadXml(String.Concat(Window.Utils.LocalFolder(), "\", "Vie HealtTech", "\", "Indigo Vie EHR", "\", "Configuracion.IndigoCrystal"))
            End With
            'cargo la cache local
            If ConfiguracionXML.Rows(0).Item("CacheLocal").ToString <> String.Empty Then
                ValoresSesion.LocalCache = CBool(ConfiguracionXML.Rows(0).Item("CacheLocal").ToString)
            End If
        Catch ex As Exception
            Dim mensajes = ex
        End Try
    End Sub
End Class
