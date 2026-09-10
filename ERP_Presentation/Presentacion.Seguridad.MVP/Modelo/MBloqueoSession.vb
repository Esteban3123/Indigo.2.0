'***********************************************************************
' Assembly         : Presentacion.Seguridad.MVP
' Author           : Jorge Leonardo Vernaza
' Created          : 21-12-2011
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Librerias Importadas"
Imports Presentation.CloudAgent
Imports Presentation.CloudAgent.IndigoReference.Security
Imports Microsoft.VisualBasic
Imports Domain.Security.Entities
Imports Infrastructure.CrossCutting.Base
#End Region
''' <summary>
''' Clase del Modelo del Loginda
''' 
''' </summary>
Public Class MBloqueoSession
    Implements IDisposable
#Region "Variables"
    ''' <summary>
    ''' Variable de session
    ''' </summary>
    Private _indigoSession As SessionValues = SessionValues.Instance
#End Region
#Region "Funciones"
    ''' <summary>
    ''' Funcion que retorna true si el usuario es validado correctamente.
    ''' </summary>
    Public Function ValidarUsuario(ByVal idUsuario As String, ByVal contraseña As String) As Boolean
        Return IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.ValidateUser(idUsuario, contraseña, Me._indigoSession)
    End Function

    '''' <summary>
    '''' Funcion que retorna el objeto [Entidad] SeguridadUsuario
    '''' </summary>
    'Public Function ConsultarUsuario(ByVal codigoUsuario As String) As User
    '    Dim objetoSeguridadUsuario As User
    '    objetoSeguridadUsuario = IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.GetUserByCodeUser(codigoUsuario, Me._indigoSession)
    '    If Not objetoSeguridadUsuario Is Nothing Then
    '        'Elimino los espacion del objeto devuelto por el serivico
    '        objetoSeguridadUsuario.Position = objetoSeguridadUsuario.Position.Trim
    '        objetoSeguridadUsuario.Person.Fullname = objetoSeguridadUsuario.Person.Fullname.Trim
    '        Return objetoSeguridadUsuario
    '    End If
    '    Return Nothing
    'End Function

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
