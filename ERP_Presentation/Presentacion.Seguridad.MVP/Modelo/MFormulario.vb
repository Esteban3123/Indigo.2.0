Imports Domain.Security.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent

Public Class MFormulario
    Implements IDisposable

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    Private disposedValue As Boolean ' To detect redundant calls

    ''' <summary>
    ''' Funcion para consultar modulo por codigo
    ''' </summary>
    Public Async Function ConsultarNombreFormulario(ByVal codigo As String) As Threading.Tasks.Task(Of VieDBForm)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.GetFormAsync(codigo, Me.Indigo)
    End Function

    ''' <summary>
    ''' Guardar el listado de objetos de VieDBForm
    ''' </summary>
    Public Async Function GuardarFormulario(ByVal vieDBForm As VieDBForm) As Threading.Tasks.Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.SaveFormAsync(vieDBForm, Me.Indigo)
    End Function

    ''' <summary>
    ''' Funcion para Eliminar modulo Seleccionado .
    ''' </summary>
    Friend Async Function EliminarFormulario(ByVal id As String) As Threading.Tasks.Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.DeleteFormAsync(id, Me.Indigo)
    End Function

    ''' <summary>
    ''' Funcion para Cambiar estado
    ''' </summary>
    Friend Async Function CambiarEstado(ByVal id As Integer, ByVal estado As Byte) As Threading.Tasks.Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.ChangeStateFormAsync(id, estado, Me.Indigo)
    End Function

    Public Async Function ConsultarFormActionFormulario(ByVal codigo As String) As Threading.Tasks.Task(Of List(Of FormAction))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.ListFormActionByFormAsync(codigo, Me.Indigo)
    End Function

    ''' <summary>
    ''' Consulta lista de formularios con acciones del ambiente seleccionado por el usuario
    ''' </summary>
    ''' <param name="Uri"></param>
    ''' <returns></returns>
    Public Async Function ConsultarFormulariosDesarrollo(ByVal Uri As String) As Threading.Tasks.Task(Of List(Of VieDBForm))
        Dim endpointConfigurationName As String = GetEndPoint(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Security)
        Dim remoteAddress As String = GetRemoteAddress(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Security, Uri)

        Dim IndigoSeguridad_New = New IndigoReference.Security.SecurityServiceClient(endpointConfigurationName, remoteAddress)
        IndigoSeguridad_New.Endpoint.EndpointBehaviors.Add(New UnityMessageBehavior())
        Return Await IndigoSeguridad_New.ListVieDBFormsImportAsync(Me.Indigo)
    End Function

    ''' <summary>
    ''' Consulta lista de formularios con acciones
    ''' </summary>
    ''' <param name="Uri"></param>
    ''' <returns></returns>
    Public Async Function ConsultarFormularios() As Threading.Tasks.Task(Of List(Of VieDBForm))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.ListVieDBFormsImportAsync(Me.Indigo)
    End Function

    ''' <summary>
    ''' Guardar el formulario a importar
    ''' </summary>
    Public Async Function GuardarFormulariosImportar(ByVal vieDBForm As List(Of VieDBForm)) As Threading.Tasks.Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.SaveFormImportAsync(vieDBForm, Me.Indigo)
    End Function

#Region "Utilidades"
    '''Metodos copia de IndigoConect para crear url de web service para el proceso de importar forms

    Private Function GetEndPoint(ByVal Protocolo As Protocol, ByVal Servicio As eServicios) As String
        Return String.Format("{0}_Endpoint_{1}", [Enum].GetName(GetType(Protocol), Protocolo), [Enum].GetName(GetType(eServicios), Servicio))
    End Function

    Private Function GetRemoteAddress(ByVal Protocolo As Protocol, ByVal Servicio As eServicios, Optional ByVal Uri As String = "") As String
        Return String.Format("{0}{1}.svc/{2}{3}", Uri.Trim(), [Enum].GetName(GetType(eServicios), Servicio), [Enum].GetName(GetType(Protocol), Protocolo), [Enum].GetName(GetType(eServicios), Servicio))
    End Function
#End Region

#Region "IDisposable"
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

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
