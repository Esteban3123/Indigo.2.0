'***********************************************************************
' Assembly         : Presentacion.Security.MVP
' Author           : Jorge Leonardo Vernaza
' Created          : 21/10/2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Librerias Importadas"
Imports Domain.Security.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent

#End Region

''' <summary>
''' Clase Modelo de acceso a datos para el formulario configurar conexion
''' </summary>
Public Class MConfigurarConexion
    Implements IDisposable
#Region "Variables"
    Dim Indigo As SessionValues = SessionValues.Instance
#End Region

#Region "Funciones"

    ''' <summary>
    ''' Funcion para consultar contenedores.
    ''' </summary>
    Public Async Function GetContainersAsync() As Threading.Tasks.Task(Of List(Of Containers))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.getContainersAsync(Indigo)
    End Function

    ''' <summary>
    ''' Funcion para consultar las zonas horarias.
    ''' </summary>
    Public Async Function GetTimezoneAsync() As Threading.Tasks.Task(Of List(Of Timezone))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.getTimezoneAsync(Indigo)
    End Function
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
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
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
