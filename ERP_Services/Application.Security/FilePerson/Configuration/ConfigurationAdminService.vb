'***********************************************************************
' Assembly         : Application.Security
' Author           : Juan F. Tamayo
' Created          : 2013-12-11
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-12-11
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Data.SqlClient
Imports System.Configuration
Imports Infrastructure.CrossCutting.Base
Imports Domain.Security.Entities

#End Region

Public Class ConfigurationAdminService
    Implements IConfigurationAdminService

    ''' <summary>
    ''' Obtiene la configuración por defecto configurada para todos los clientes
    ''' de la organisación
    ''' </summary>
    ''' <returns></returns>
    Public Function GetGenesisConfiguration() As DataSet Implements IConfigurationAdminService.GetGenesisConfiguration
        Using conexion As New SqlConnection(String.Format(ConfigurationManager.ConnectionStrings("EFConnection").ConnectionString, ConfigurationFile.SECURITY_CONTAINER_PARAMETER_NAME))
            Dim query As String = "SELECT * FROM Confg.GenesisConfiguration WHERE Id=1"
            Dim da As SqlDataAdapter = New SqlDataAdapter(query, conexion)
            da.SelectCommand.CommandTimeout = 90
            Dim ds As New DataSet
            da.Fill(ds)
            conexion.Close()
            Return ds
        End Using
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            IndigoGC.Execute()
        End If
        disposedValue = True
    End Sub

    ' Visual Basic agrega este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
