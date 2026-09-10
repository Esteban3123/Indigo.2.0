Imports System.IO
Imports System.IO.Compression
Imports System.Security.Cryptography.X509Certificates
Imports System.ServiceModel
Imports DistributedServices.DIAN.WcfDianCustomerServices
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.AzureBlobStorage
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Signature

Public Class ServiceClient
    Implements IDisposable

#Region "Properties"

    Private ReadOnly wsHttpBinding As New WSHttpBinding With
    {
        .Name = "WSHttpBinding_IWcfDianCustomerServices",
        .Security = New WSHttpSecurity With
        {
            .Mode = SecurityMode.TransportWithMessageCredential,
            .Transport = New HttpTransportSecurity With
            {
                .ClientCredentialType = HttpClientCredentialType.None
            },
            .Message = New NonDualMessageSecurityOverHttp With
            {
                .ClientCredentialType = MessageCredentialType.Certificate,
                .AlgorithmSuite = Security.SecurityAlgorithmSuite.Basic256Sha256Rsa15,
                .EstablishSecurityContext = False
            }
        },
        .OpenTimeout = TimeSpan.FromMinutes(5),
        .CloseTimeout = TimeSpan.FromMinutes(5),
        .SendTimeout = TimeSpan.FromMinutes(5),
        .ReceiveTimeout = TimeSpan.FromMinutes(5),
        .MaxReceivedMessageSize = 2147483647
    }

    Private _url As String

    Private _digitalCertificate As Byte()

    Private _digitalCertificateKey As String
    ''' <summary>
    ''' servicio de almacenamiento
    ''' </summary>
    Private ReadOnly _storateService As IStorage

#End Region

#Region "Builder"

    Public Sub New(url As String, digitalCertificate As Byte(), digitalCertificateKey As String, Optional storateService As IStorage = Nothing)
        Me._url = url
        Me._digitalCertificate = digitalCertificate
        Me._digitalCertificateKey = digitalCertificateKey
        Me._storateService = storateService
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene los datos de una autorización de FE
    ''' </summary>
    ''' <param name="supplierThirdPartyNit"></param>
    ''' <param name="softwareIdentifier"></param>
    ''' <returns></returns>
    Public Function GetNumberingRange(supplierThirdPartyNit As String, softwareIdentifier As String) As ActionResult(Of NumberRangeResponseList)
        Try
            Dim address As New EndpointAddress(Me._url)
            Using service As New WcfDianCustomerServicesClient(wsHttpBinding, address)
                Dim x509Chain As X509Chain = Nothing
                Dim certificate As X509Certificate2 = ConfigurationCertificate.ReadCertificate(Me._digitalCertificate, Me._digitalCertificateKey, x509Chain)
                If certificate Is Nothing Then
                    Return New ActionResult(Of NumberRangeResponseList) With {.StateResult = False, .Message = "No se encontro el certificado."}
                End If
                Dim expirationDate As DateTime = Convert.ToDateTime(certificate.GetExpirationDateString())
                Dim currentDate = Date.Now()
                If currentDate > expirationDate Then
                    Return New ActionResult(Of NumberRangeResponseList) With {.StateResult = False, .Message = "Certificado se encuentra expirado."}
                End If

                service.ClientCredentials.ClientCertificate.Certificate = certificate
                Dim response = service.GetNumberingRange(supplierThirdPartyNit, supplierThirdPartyNit, softwareIdentifier)
                Return New ActionResult(Of NumberRangeResponseList) With {.StateResult = True, .ObjectEmbbeded = response}
            End Using
        Catch ex As Exception
            Return New ActionResult(Of NumberRangeResponseList) With {.StateResult = False, .Message = Infrastructure.CrossCutting.Base.Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function SendTestSet(testSetId As String, filePath As String, fileName As String) As ActionResult(Of DianResponse)
        Try
            Dim fileWs = String.Concat("z", fileName.Substring(2, fileName.Length - 6), ".zip")
            Dim contentFile = CompressFile(filePath, fileName, fileWs)
            Dim address As New EndpointAddress(Me._url)
            Using service As New WcfDianCustomerServicesClient(wsHttpBinding, address)
                Dim x509Chain As X509Chain = Nothing
                service.ClientCredentials.ClientCertificate.Certificate = ConfigurationCertificate.ReadCertificate(Me._digitalCertificate, Me._digitalCertificateKey, x509Chain)

                Dim response = service.SendTestSetAsync(fileWs, contentFile, testSetId)

                Dim dianResponse As New DianResponse
                dianResponse.IsValid = If(response.ErrorMessageList Is Nothing, True, False)
                dianResponse.StatusCode = If(dianResponse.IsValid, Enums.ElectronicDocuments.StatusCode.OK, Enums.ElectronicDocuments.StatusCode.NotAcceptable)
                dianResponse.StatusDescription = If(dianResponse.IsValid, "El archivo fue recibido", "El archivo fue rechazado")
                dianResponse.StatusMessage = response.ZipKey

                Return New ActionResult(Of DianResponse) With {.StateResult = True, .ObjectEmbbeded = dianResponse}
            End Using
        Catch ex As Exception
            Return New ActionResult(Of DianResponse) With {.StateResult = False, .Message = Infrastructure.CrossCutting.Base.Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function SendBillAsync(filePath As String, fileName As String) As ActionResult(Of UploadDocumentResponse)
        Try
            Dim fileWs = String.Concat("z", fileName.Substring(2, fileName.Length - 6), ".zip")
            Dim contentFile = CompressFile(filePath, fileName, fileWs)

            Dim address As New EndpointAddress(Me._url)
            Using service As New WcfDianCustomerServicesClient(wsHttpBinding, address)
                Dim x509Chain As X509Chain = Nothing
                service.ClientCredentials.ClientCertificate.Certificate = ConfigurationCertificate.ReadCertificate(Me._digitalCertificate, Me._digitalCertificateKey, x509Chain)

                Dim response = service.SendBillAsync(fileWs, contentFile)
                Return New ActionResult(Of UploadDocumentResponse) With {.StateResult = True, .ObjectEmbbeded = response}
            End Using
        Catch ex As Exception
            Return New ActionResult(Of UploadDocumentResponse) With {.StateResult = False, .Message = Infrastructure.CrossCutting.Base.Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function SendBill(filePath As String, fileName As String) As ActionResult(Of DianResponse)
        Try
            Dim fileWs = String.Concat("z", fileName.Substring(2, fileName.Length - 6), ".zip")
            Dim contentFile = CompressFile(filePath, fileName, fileWs)

            Dim address As New EndpointAddress(Me._url)
            Using service As New WcfDianCustomerServicesClient(wsHttpBinding, address)
                Dim x509Chain As X509Chain = Nothing
                service.ClientCredentials.ClientCertificate.Certificate = ConfigurationCertificate.ReadCertificate(Me._digitalCertificate, Me._digitalCertificateKey, x509Chain)
                Dim response = service.SendBillSync(fileWs, contentFile)
                Return New ActionResult(Of DianResponse) With {.StateResult = True, .ObjectEmbbeded = response}
            End Using
        Catch ex As Exception
            Return New ActionResult(Of DianResponse) With {.StateResult = False, .Message = Infrastructure.CrossCutting.Base.Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' funcion que se encarga de hacer la compresion del archivo a zip 
    ''' </summary>
    ''' <param name="filePath"></param>
    ''' <param name="fileNameXml"></param>
    ''' <param name="fileNameZip"></param>
    ''' <returns></returns>
    Private Function CompressFile(filePath As String, fileNameXml As String, fileNameZip As String) As Byte()

        Dim zipBytes As Byte()
        Dim fileZip As String = Path.Combine(filePath, fileNameZip)
        Dim fileXml As String = Path.Combine(filePath, fileNameXml)

        If _storateService?.StorageType IsNot Nothing Then
            Dim fileXmlBytes = _storateService.ReadFile(filePath, fileNameXml)
            zipBytes = Utils.CompressFileStream(fileXmlBytes, fileNameXml)
            _storateService.WriteFile(filePath, fileNameZip, zipBytes)
        Else
            Infrastructure.CrossCutting.Base.Utils.CompressFile(filePath, fileNameXml, fileNameZip)
            zipBytes = Infrastructure.CrossCutting.Base.Utils.FileReadAllBytes(filePath, fileNameZip)
            File.WriteAllBytes(fileZip, zipBytes)
        End If

        Return zipBytes
    End Function

    Public Async Function SendNominaAsync(filePath As String, fileName As String) As Task(Of ActionResult(Of DianResponse))
        Try
            Dim fileWs = String.Concat("z", fileName.Substring(2, fileName.Length - 6), ".zip")
            Infrastructure.CrossCutting.Base.Utils.CompressFile(filePath, fileName, fileWs)
            Dim contentFile = Infrastructure.CrossCutting.Base.Utils.FileReadAllBytes(filePath, fileWs)

            Dim address As New EndpointAddress(Me._url)
            Using service As New WcfDianCustomerServicesClient(wsHttpBinding, address)
                Dim x509Chain As X509Chain = Nothing
                service.ClientCredentials.ClientCertificate.Certificate = ConfigurationCertificate.ReadCertificate(Me._digitalCertificate, Me._digitalCertificateKey, x509Chain)

                Dim response = Await service.SendNominaSyncAsync(contentFile)
                Return New ActionResult(Of DianResponse) With {.StateResult = True, .ObjectEmbbeded = response}
            End Using
        Catch ex As Exception
            Return New ActionResult(Of DianResponse) With {.StateResult = False, .Message = Infrastructure.CrossCutting.Base.Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function SendNomina(filePath As String, fileName As String) As ActionResult(Of DianResponse)
        Try
			Dim fileWs = String.Concat("z", fileName.Substring(2, fileName.Length - 6), ".zip")
			Dim contentFile = CompressFile(filePath, fileName, fileWs)

			Dim address As New EndpointAddress(Me._url)
            Using service As New WcfDianCustomerServicesClient(wsHttpBinding, address)
                Dim x509Chain As X509Chain = Nothing
                service.ClientCredentials.ClientCertificate.Certificate = ConfigurationCertificate.ReadCertificate(Me._digitalCertificate, Me._digitalCertificateKey, x509Chain)

                Dim response = service.SendNominaSync(contentFile)
                Return New ActionResult(Of DianResponse) With {.StateResult = True, .ObjectEmbbeded = response}
            End Using
        Catch ex As Exception
            Return New ActionResult(Of DianResponse) With {.StateResult = False, .Message = Infrastructure.CrossCutting.Base.Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function GetStatusZip(zipKey As String) As ActionResult(Of DianResponse)
        Try
            Dim address As New EndpointAddress(Me._url)
            Using service As New WcfDianCustomerServicesClient(wsHttpBinding, address)
                Dim x509Chain As X509Chain = Nothing
                service.ClientCredentials.ClientCertificate.Certificate = ConfigurationCertificate.ReadCertificate(Me._digitalCertificate, Me._digitalCertificateKey, x509Chain)

                Dim response = service.GetStatusZip(zipKey)
                Return New ActionResult(Of DianResponse) With {.StateResult = True, .ObjectEmbbeded = response.FirstOrDefault()}
            End Using
        Catch ex As Exception
            Return New ActionResult(Of DianResponse) With {.StateResult = False, .Message = Infrastructure.CrossCutting.Base.Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function GetStatus(cufe As String) As ActionResult(Of DianResponse)
        Try
            Dim address As New EndpointAddress(Me._url)
            Using service As New WcfDianCustomerServicesClient(wsHttpBinding, address)
                Dim x509Chain As X509Chain = Nothing
                service.ClientCredentials.ClientCertificate.Certificate = ConfigurationCertificate.ReadCertificate(Me._digitalCertificate, Me._digitalCertificateKey, x509Chain)

                Dim response = service.GetStatus(cufe)
                Return New ActionResult(Of DianResponse) With {.StateResult = True, .ObjectEmbbeded = response}
            End Using
        Catch ex As Exception
            Return New ActionResult(Of DianResponse) With {.StateResult = False, .Message = Infrastructure.CrossCutting.Base.Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function GetElectronicDocument(cufe As String, filePath As String, fileName As String) As Boolean
        Try
            Dim address As New EndpointAddress(Me._url)
            Using service As New WcfDianCustomerServicesClient(wsHttpBinding, address)
                Dim x509Chain As X509Chain = Nothing
                service.ClientCredentials.ClientCertificate.Certificate = ConfigurationCertificate.ReadCertificate(Me._digitalCertificate, Me._digitalCertificateKey, x509Chain)

                Dim response = service.GetXmlByDocumentKey(cufe)
                If response.Code = "100" Then
                    Dim xmlBytesBase64 = Convert.FromBase64String(response.XmlBytesBase64)
                    If _storateService IsNot Nothing Then
                        _storateService.WriteFile(filePath, fileName, xmlBytesBase64)
                    Else
                        File.WriteAllBytes(System.IO.Path.Combine(filePath, fileName), xmlBytesBase64)
                    End If
                    Return True
                End If

                Return False
            End Using
        Catch ex As Exception
            Return False
        End Try
    End Function

#End Region

#Region "IDisposable Support"

    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                'Others Services
            End If
            Infrastructure.CrossCutting.Base.IndigoGC.Execute()
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
