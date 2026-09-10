#Region "Imports"
Imports System.Text
Imports System.Net.Http
Imports System.Threading.Tasks
Imports Newtonsoft.Json
Imports Domain.Base.Entities
Imports Domain.Billing.Repositories
Imports Infrastructure.CrossCutting.Base
#End Region

''' <summary>
''' Clase para validar derechos del FOMAG desde el ERP
''' </summary>
Public Class ValidationFomagAdminService
    Implements IValidationFomagAdminService

#Region "Init"
    Private Const JersaludNit = "900622551"
    Private _sessionValues As SessionValues
    Private _fomagRepository As IFomagRepository

    Public Sub New(fomagRepository As IFomagRepository)
        _fomagRepository = fomagRepository
        _sessionValues = SessionValues.Instance
    End Sub
#End Region

#Region "Metodos"
    ''' <summary>
    ''' Ejecuta la validación si el paciente aplica a FOMAG
    ''' </summary>
    Public Async Function ValidatePatientAsync(codigoPaciente As String, companyNit As String) As Task(Of ActionResult) Implements IValidationFomagAdminService.ValidatePatientAsync

        Dim resValidated As New ActionResult

        'Proceso si el cliente es Jersalud
        If companyNit = JersaludNit Then
            Dim patientInfo = _fomagRepository.GetPatientInfo(codigoPaciente)
            Dim inteInfo = _fomagRepository.GetIntegrationFomag(1)

            'Validación edad mínima
            If Not patientInfo.BornDays > 28 Then Return New ActionResult With {.StateResult = False, .Message = "El paciente no tiene más de 28 días de nacido"}

            'Se llama la validación remota
            resValidated = Await ValidationRequestFomag(inteInfo.URLAz, patientInfo.DocumentType, patientInfo.DocumentNumber)

            If resValidated.StateResult = False Then
                Return New ActionResult With {.StateResult = False, .Message = "Fomag: no se puede crear ingreso porque: " + resValidated.Message}
            End If
        End If

        'Respuesta exitosa independiente al cliente
        Return New ActionResult With {.StateResult = True, .Message = resValidated.Message}
    End Function
#End Region

#Region "Auxiliar"
    ''' <summary>
    ''' Valida el derecho FOMAG en Azure Function
    ''' </summary>
    Public Async Function ValidationRequestFomag(baseUrl As String, tipoDocumento As Integer, numeroDocumento As String) As Task(Of ActionResult)

        Net.ServicePointManager.SecurityProtocol = Net.SecurityProtocolType.Tls12

        Using client As New HttpClient()
            Try
                If String.IsNullOrWhiteSpace(baseUrl) OrElse String.IsNullOrWhiteSpace(numeroDocumento) Then
                    Return New ActionResult With {.StateResult = False, .Message = "URL o documento inválido."}
                End If

                Dim requestBody = New With {
                    .tipo_documento = tipoDocumento,
                    .numero_documento = numeroDocumento
                }

                Dim jsonContent As String = JsonConvert.SerializeObject(requestBody)
                Dim content = New StringContent(jsonContent, Encoding.UTF8, "application/json")

                Dim response = Await client.PostAsync(baseUrl, content)

                If Not response.IsSuccessStatusCode Then Return New ActionResult With {.StateResult = False, .Message = String.Format("{0} {1}", response.StatusCode, CInt(response.StatusCode))}

                Dim responseBody = Await response.Content.ReadAsStringAsync()
                Dim result As ResponseFomag = JsonConvert.DeserializeObject(Of ResponseFomag)(responseBody)

                If result Is Nothing OrElse result.res = False Then
                    Return New ActionResult With {.StateResult = False, .Message = result?.error}
                Else
                    Return New ActionResult With {.StateResult = True, .Message = result?.error}
                End If

            Catch ex As HttpRequestException
                Return New ActionResult With {.StateResult = False, .Message = ex.Message}
            End Try
        End Using
    End Function
#End Region

#Region "Modelo de respuesta"
    ''' <summary>
    ''' Modelo de respuesta FOMAG
    ''' </summary>
    Private Class ResponseFomag
        Public Property res As Boolean
        Public Property [error] As String
    End Class
#End Region

#Region "IDisposable Support"
    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        _fomagRepository = Nothing
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class
