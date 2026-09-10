Imports System.IO
Imports System.Security.Cryptography.X509Certificates
Imports System.Text
Imports Infrastructure.CrossCutting.Root
Imports Infrastructure.CrossCutting.Signature.Crypto
Imports Infrastructure.CrossCutting.Signature.Signature
Imports Infrastructure.CrossCutting.Signature.Signature.Parameters

Public Class ElectronicSignature

#Region "Properties"

    Public _dianVersion As Decimal

    Public _ublExtensions As Integer

    Public _signatoryRole As SignatoryRole

    Public _digitalCertificate As Byte()

    Public _digitalCertificateKey As String

    Public _certificateIssuer As String

    Public _namespaces As Dictionary(Of String, String)

#End Region

#Region "Builders"

    Public Sub New(dianVersion As Decimal, signatoryRole As SignatoryRole, digitalCertificate As Byte(), digitalCertificateKey As String, Optional certificateIssuer As String = Nothing, Optional namespaces As Dictionary(Of String, String) = Nothing)
        Me._dianVersion = dianVersion
        Me._signatoryRole = signatoryRole
        Me._digitalCertificate = digitalCertificate
        Me._digitalCertificateKey = digitalCertificateKey
        Me._certificateIssuer = certificateIssuer
        Me._namespaces = namespaces
    End Sub

#End Region

#Region "Methods"

    Public Function SignatureFile(fileXml As FileInfo, typeElectronicDocument As TypeElectronicDocument, currentDate As DateTime) As Byte()
        Dim bytesXml = File.ReadAllBytes(fileXml.FullName)
        Return Me.Signature(bytesXml, typeElectronicDocument, currentDate)
    End Function

    Public Function SignatureFile(fileXml As MemoryStream, typeElectronicDocument As TypeElectronicDocument, currentDate As DateTime) As Byte()
        Dim bytesXml = fileXml.ToArray()
        Return Me.Signature(bytesXml, typeElectronicDocument, currentDate)
    End Function

    Public Function SignatureString(stringXml As String, typeElectronicDocument As TypeElectronicDocument, currentDate As DateTime) As Byte()
        Dim bytesXml = Encoding.UTF8.GetBytes(stringXml)
        Return Me.Signature(bytesXml, typeElectronicDocument, currentDate)
    End Function

    Public Function Signature(bytesXml As Byte(), typeElectronicDocument As TypeElectronicDocument, currentDate As DateTime) As Byte()
        Dim parameters As New SignatureParameters()

        If Me._dianVersion = 2.1 Then
            parameters.SignatureMethod = SignatureMethod.RSAwithSHA256
            parameters.DigestMethod = DigestMethod.SHA256
            parameters.SigningDate = currentDate
            parameters.SignaturePolicyInfo = New SignaturePolicyInfo()
            parameters.SignaturePolicyInfo.PolicyIdentifier = "https://facturaelectronica.dian.gov.co/politicadefirma/v2/politicadefirmav2.pdf"
            parameters.SignaturePolicyInfo.PolicyDigestAlgorithm = DigestMethod.SHA256
            parameters.SignaturePolicyInfo.PolicyHash = "dMoMvtcG5aIzgYo0tIsSQeVJBDnUnfSOfBpxXrmor0Y="
            parameters.SignaturePackaging = SignaturePackaging.ENVELOPED
            parameters.SignerRole = New Signature.Parameters.SignerRole()
            parameters.SignerRole.ClaimedRoles.Add(IIf(Me._signatoryRole = SignatoryRole.ElectronicBiller, "supplier", "third party"))
            parameters.SignatureDestination = New SignatureXPathExpression()
            parameters.SignatureDestination.Namespaces = Me._namespaces
            parameters.SignatureDestination.XPathExpression = String.Format("//ext:UBLExtensions/ext:UBLExtension[{0}]/ext:ExtensionContent", _ublExtensions)
        End If

        Dim x509Chain As X509Chain = Nothing
        Dim certificate As X509Certificate2 = ConfigurationCertificate.ReadCertificate(Me._digitalCertificate, Me._digitalCertificateKey, x509Chain)
        If certificate Is Nothing OrElse x509Chain Is Nothing Then
            Throw New Exception("Error Firmando: No se encontro el certificado.")
        End If

        Dim dateTime1 As DateTime = Convert.ToDateTime(certificate.GetEffectiveDateString())
        Dim dateTime2 As DateTime = Convert.ToDateTime(certificate.GetExpirationDateString())
        If currentDate < dateTime1 OrElse currentDate > dateTime2 Then
            Throw New Exception("Error Firmando: El certificado no esta vigente.")
        End If

        parameters.Signer = New Signer(certificate, x509Chain)
        Using input = New MemoryStream(bytesXml)
            Dim xades = New XadesService()
            Dim signatureDocument = xades.Sign(input, parameters)

            Dim output = New MemoryStream()
            signatureDocument.Save(output)

            Return output.ToArray()
        End Using
    End Function

#End Region

End Class
