Imports Presentation.CloudAgent
Imports Infrastructure.CrossCutting.Base

Public NotInheritable Class CustomizacionFrontales

    Private _cargarDefinicionFrontales As Boolean

    Property CargarDefinicionFrontales(ByVal p1 As String) As Boolean
        Get
            Return _cargarDefinicionFrontales
        End Get
        Set(ByVal value As Boolean)
            _cargarDefinicionFrontales = value
        End Set
    End Property

    ''' <summary>
    ''' Guardars the definicion frontales.
    ''' </summary>
    Shared Function VerificaExisteDefinicionFrontal(ByVal RutaOrigenDefiniciones As String) As Boolean
        Try
            Dim Indigo As SessionValues = SessionValues.Instance
            If My.Computer.FileSystem.FileExists(RutaOrigenDefiniciones) = True Then
                Return True
            End If
        Catch ex As Exception
            Return False
        End Try
        Return False
    End Function

    ''' <summary>
    ''' Metodo que se utiliza para guardar las definiciones de los funcionales si en algun momento de la efecucion se modifico el layout
    ''' </summary>
    Shared Function VerificaCarpetaDefinicionFuncionales(ByVal RutaOrigenDefiniciones As String) As Boolean
        'Verificamos si existe la carpeta donde se van a guardar las definiciones de las rejillas
        If My.Computer.FileSystem.DirectoryExists(RutaOrigenDefiniciones) = False Then
            My.Computer.FileSystem.CreateDirectory(RutaOrigenDefiniciones)
            Return True
        End If
        Return True
    End Function

    Shared Function ConsultarCamposNull() As List(Of Object)

        'Dim Indigo As SessionValues = SessionValues.Instance
        'Return IndigoConecta .Instancia.CurrentCloud.

    End Function


End Class
