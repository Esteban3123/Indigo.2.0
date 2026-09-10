'***********************************************************************
' Assembly         : Presentacion.Seguridad.MVP
' Author           : Julian Cardozo
' Created          : 03-03-2011
'
' Last Modified By : Andres Bonilla
' Last Modified On : 27-03-2011
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Importadas"

Imports Presentation.CloudAgent
Imports Presentation.CloudAgent.IndigoReference.Security
Imports Domain.Security.Entities
Imports Infrastructure.CrossCutting.Base
#End Region

''' <summary>
''' Modelo que se comunica con los servicios
''' </summary>
Public Class MCambiarContraseña
    Implements IDisposable

#Region "Fields"

    ''' <summary>
    ''' Variable de session
    ''' </summary>
    Private _indigoSession As SessionValues = SessionValues.Instance

#End Region


#Region "Funciones"

    ''' <summary>
    ''' Funcion que se utiliza para cambiar la contraseña del usuario.
    ''' </summary>
    Friend Async Function CambiarContraseña(ByVal idUsuario As String, ByVal contraseñaAnterior As String, ByVal contraseñaNueva As String) As Threading.Tasks.Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.ChangePasswordAsync(idUsuario, contraseñaAnterior, contraseñaNueva, Me._indigoSession)
    End Function

    ''' <summary>
    ''' Metodo que obtiene la contraseña del usuario
    ''' </summary>
    ''' <param name="userId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetPasswordAsync(ByVal userId As String) As Threading.Tasks.Task(Of String)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.GetPasswordUserAsync(userId, _indigoSession)
    End Function

    '''' <summary>
    '''' Funcion que se utiliza par consultar La hora del servidor
    '''' </summary>
    'Friend Async Function ConsultarFechaServidor() As Threading.Tasks.Task(Of Date)
    '    Return Await IndigoConecta.Instancia.CurrentCloud.IndigoComunes.GetServerDateAsync
    'End Function

    '''' <summary>
    '''' Funcion que se utiliza para consultar el usuario.
    '''' </summary>
    'Friend Async Function ConsultarUsuario(ByVal codigoUsuario As Integer) As Threading.Tasks.Task(Of User)
    '    Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.GetUserByIdentificationAsync(codigoUsuario, Me._indigoSession)
    'End Function

    '''' <summary>
    '''' Funcion que se utiliza para cambiar la contraseña del usuario.
    '''' </summary>
    'Friend Async Function ConsultarContrasenaUsuario(ByVal codigoUsuario As String) As Threading.Tasks.Task(Of String)
    '    Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.GetPasswordUserAsync(codigoUsuario, Me._indigoSession)
    'End Function


    ''' <summary>
    ''' Funcion que se utiliza para verificar el usuario.
    ''' </summary>
    Friend Async Function ValidarUsuario(ByVal codigoUsuario As String, ByVal contrasena As String) As Threading.Tasks.Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.ValidateUserAsync(codigoUsuario, contrasena, Me._indigoSession)
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
