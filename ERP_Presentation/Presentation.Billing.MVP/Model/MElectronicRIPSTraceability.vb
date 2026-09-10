'***********************************************************************
' Assembly         : Presentacion.Billing.MVP
' Author           : Cristian Camilo Bahamon Castaño
' Created          : 01-03-2024
' Copyright        : (c) . All rights reserved.
'***********************************************************************


#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Base.Security
Imports Domain.Base.Entities
Imports Presentation.Base
Imports RestSharp

#End Region


Public Class MElectronicRIPSTraceability
    Implements IDisposable

#Region "Fields"

    ''' <summary>
    ''' variable que obtiene los valores de la sesion
    ''' </summary>
    Dim Indigo As SessionValues

    ''' <summary>
    ''' Id del frontal
    ''' </summary>
    Private _tagForm As String

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New(ByVal tag As String)
        Me._tagForm = tag
        Me.Indigo = SessionValues.Instance
    End Sub

    Public Sub New()
    End Sub

#End Region

#Region "Methods"
    ''' <summary>
    ''' Funcion para el reenvio de los RIPS electronicos
    ''' </summary>
    ''' <param name="EntityName"></param>
    ''' <param name="ListResendInvoice"></param>
    ''' <returns></returns>
    Public Async Function ReSendElectronicRIPS(EntityName As String, ListResendInvoice As List(Of String)) As Task(Of ActionResult(Of String))
        Try
            Dim endpoint = SessionValues.Instance.GetEndpointByCode(EndpointCodes.Revenue_Cycle)
            Dim client = New RestClient($"{endpoint.UrlBase}/electronicRIPS/ReSendRIPS/{EntityName}")
            client.Authenticator = New BearerTokenAuthenticator()

            Dim req = New RestRequest(Method.POST)
            req.AddHeader("Content-Type", "application/json")
            req.AddHeader("_ContainerName_", Indigo.TransactionalContainer)
            req.AddHeader("_ContainerHisName_", Indigo.HisContainer)
            req.AddHeader("CodeUser", Indigo.AuditMessageWcf.CodeUser)
            req.AddJsonBody(ListResendInvoice)

            Dim response = Await client.ExecuteAsync(Of ServiceResponse(Of String))(req)

            If response?.Data Is Nothing Then
                Return New ActionResult(Of String) With {.StateResult = False, .Message = response?.StatusDescription}
            End If

            Return New ActionResult(Of String) With {.StateResult = response.Data.Status, .Message = response.Data.Message}

        Catch ex As Exception
            Return New ActionResult(Of String) With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' consultar json rips por el id de cosmodbid
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Async Function GetJsonRIPSById(id As String) As Task(Of ActionResult(Of String))
        If String.IsNullOrEmpty(id) Then
            Return New ActionResult(Of String) With {.StateResult = False, .Message = $"Parámetro {NameOf(id)} vacío"}
        End If

        Return Await ExecuteJsonRIPSRequest($"GetJsonRIPSById/{id}")
    End Function

    ''' <summary>
    ''' consultar json rips por numero de factura
    ''' </summary>
    ''' <param name="docNumber"></param>
    ''' <returns></returns>
    Public Async Function GetJsonRIPSByDocNumber(docNumber As String) As Task(Of ActionResult(Of String))
        If String.IsNullOrEmpty(docNumber) Then
            Return New ActionResult(Of String) With {.StateResult = False, .Message = $"Parámetro {NameOf(docNumber)} vacío"}
        End If

        Return Await ExecuteJsonRIPSRequest($"GetJsonRIPSByDocNumber/{docNumber}")
    End Function

    ''' <summary>
    ''' ejecutar la peticion al servicio de rips a traves del CosmoDBId o docNumber
    ''' </summary>
    ''' <param name="resourcePath"></param>
    ''' <returns></returns>
    Private Async Function ExecuteJsonRIPSRequest(resourcePath As String) As Task(Of ActionResult(Of String))
        Try
            Dim endpoint = SessionValues.Instance.GetEndpointByCode(EndpointCodes.Revenue_Cycle)
            Dim client = New RestClient($"{endpoint.UrlBase}/electronicRIPS/{resourcePath}")
            client.Authenticator = New BearerTokenAuthenticator()


            Dim req = New RestRequest(Method.GET)
            req.AddHeader("Content-Type", "application/json")
            req.AddHeader("_ContainerName_", SessionValues.Instance.TransactionalContainer)
            req.AddHeader("_ContainerHisName_", SessionValues.Instance.HisContainer)

            Dim response = Await client.ExecuteGetAsync(Of ServiceResponse(Of String))(req)

            If response?.Data?.Status Is Nothing Then
                Return New ActionResult(Of String) With {.StateResult = False, .Message = "Error Servicio de descarga JSON"}
            End If

            If Not response.Data.Status Then
                Return New ActionResult(Of String) With {.StateResult = False, .Message = response.Data.Message}
            End If

            Return New ActionResult(Of String) With {.StateResult = response.Data.Status, .Message = response.Data.Message, .ObjectEmbbeded = response?.Data?.Data}

        Catch ex As Exception
            Return New ActionResult(Of String) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function
#End Region

#Region "IDisposable Support"

    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: desechar estado administrado (objetos administrados).
            End If

            ' TODO: liberar recursos no administrados (objetos no administrados) e invalidar Finalize() below.
            ' TODO: Establecer campos grandes como Null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: invalidar Finalize() sólo si la instrucción Dispose(ByVal disposing As Boolean) anterior tiene código para liberar recursos no administrados.
    'Protected Overrides Sub Finalize()
    '    ' No cambie este código. Ponga el código de limpieza en la instrucción Dispose(ByVal disposing As Boolean) anterior.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' Visual Basic agregó este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en Dispose(disposing As Boolean).
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

#End Region

End Class
