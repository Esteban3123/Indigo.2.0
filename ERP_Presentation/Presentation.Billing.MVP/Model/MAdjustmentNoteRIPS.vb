'***********************************************************************
' Assembly         : Presentacion.Billing.MVP
' Author           : Oscar stiven astudillo 
' Created          : 2024-14-11
' Copyright        : (c) . All rights reserved.
'***********************************************************************


#Region "Imports"
Imports System.Dynamic
Imports Domain.Base.Entities
Imports Domain.Billing.POCO
Imports Infrastructure.Base.Security
Imports Infrastructure.CrossCutting.Base
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports Presentation.Base
Imports RestSharp
Imports RestSharp.Serialization.Json

#End Region

Public Class MAdjustmentNoteRIPS
    Implements IDisposable

#Region "Fields"

    ''' <summary>
    ''' variable que obtiene los valores de la sesion
    ''' </summary>
    Private _indigo As SessionValues

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New()
        Me._indigo = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"
    ''' <summary>
    ''' Obtiene objeto Json RIPS
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Async Function GetObjectJsonRIPSbyId(id As String) As Task(Of ActionResult(Of ElectronicRIPSModel))
        If String.IsNullOrEmpty(id) Then
            Return New ActionResult(Of ElectronicRIPSModel) With {
            .StateResult = False,
            .Message = $"Parámetro {NameOf(id)} vacío"
        }
        End If
        Dim endpoint = SessionValues.Instance.GetEndpointByCode(EndpointCodes.Revenue_Cycle)
        Dim url As String = $"{endpoint.UrlBase}/electronicRIPS/GetObjectJsonRIPSbyId/{id}"
        Dim client As New RestClient(url)
        client.Authenticator = New BearerTokenAuthenticator()

        Dim req As New RestRequest(Method.GET)
        req.AddHeader("Content-Type", "application/json")
        req.AddHeader("_ContainerName_", SessionValues.Instance.TransactionalContainer)
        req.AddHeader("_ContainerHisName_", SessionValues.Instance.HisContainer)

        Try
            Dim response = Await client.ExecuteGetAsync(Of ServiceResponse(Of ElectronicRIPSModel))(req)
            If response?.Data?.Status Is Nothing Then
                Return New ActionResult(Of ElectronicRIPSModel) With {
               .StateResult = False,
               .Message = "Error Servicio de obtener objeto JSON"
           }
            End If
            If Not response.Data.Status Then
                Return New ActionResult(Of ElectronicRIPSModel) With {
               .StateResult = False,
               .Message = response.Data.Message
           }
            End If
            Return New ActionResult(Of ElectronicRIPSModel) With {
            .StateResult = response.Data.Status,
            .Message = response.Data.Message,
            .ObjectEmbbeded = response?.Data?.Data
        }
        Catch ex As Exception
            Return New ActionResult(Of ElectronicRIPSModel) With {
               .StateResult = False,
               .Message = Utils.GetInnerExceptionMessageToString(ex)
           }
        End Try
    End Function


#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
            End If
        End If
        Me.disposedValue = True
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region


End Class
