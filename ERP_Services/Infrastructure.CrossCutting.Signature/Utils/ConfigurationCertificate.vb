Imports System.Collections.Concurrent
Imports System.Security.Cryptography
Imports System.Security.Cryptography.X509Certificates
Imports System.Threading

Public Class ConfigurationCertificate

    Private Shared ReadOnly CertificateStorageFlags As X509KeyStorageFlags =
        X509KeyStorageFlags.MachineKeySet Or X509KeyStorageFlags.PersistKeySet

    ''' <summary>
    ''' Cada Import con PersistKeySet escribe un contenedor de claves nuevo en disco que el sistema
    ''' no elimina. Importar una vez por certificado y reutilizar es lo que evita agotar el almacén
    ''' de claves del host durante procesos masivos (nómina electrónica, facturación).
    ''' </summary>
    Private Shared ReadOnly _importedCertificates As New ConcurrentDictionary(Of String, Lazy(Of ImportedCertificate))()

    Private NotInheritable Class ImportedCertificate

        Public ReadOnly Certificate As X509Certificate2

        Public ReadOnly Chain As X509Chain

        Public Sub New(certificate As X509Certificate2, chain As X509Chain)
            Me.Certificate = certificate
            Me.Chain = chain
        End Sub

    End Class

    Public Shared Function ReadCertificateFromFile(certPath As String, certPass As String, ByRef x509Chain As X509Chain) As X509Certificate2
        Dim certificate As X509Certificate2 = Nothing
        x509Chain = new X509Chain()

        Try
            Dim certificates As X509Certificate2Collection = new X509Certificate2Collection()
            certificates.Import(certPath, certPass, CertificateStorageFlags)
            For Each x509Certificate2 As X509Certificate2 In certificates
                if x509Certificate2.HasPrivateKey
                    certificate = x509Certificate2
                End If
            Next
            x509Chain.ChainPolicy.ExtraStore.AddRange(certificates)
            x509Chain.Build(certificate)
        Catch ex As Exception

        End Try

        return certificate
    End Function

    Public Shared Function ReadCertificate(digitalCertificate As Byte(), digitalCertificateKey As String, ByRef x509Chain As X509Chain) As X509Certificate2
        If digitalCertificate Is Nothing OrElse digitalCertificate.Length = 0 Then
            x509Chain = New X509Chain()
            Return Nothing
        End If

        Dim cacheKey = BuildCacheKey(digitalCertificate, digitalCertificateKey)
        Dim pendingImport As New Lazy(Of ImportedCertificate)(
            Function() Import(digitalCertificate, digitalCertificateKey),
            LazyThreadSafetyMode.ExecutionAndPublication)

        Dim imported = _importedCertificates.GetOrAdd(cacheKey, pendingImport).Value

        If imported.Certificate Is Nothing Then
            Dim discarded As Lazy(Of ImportedCertificate) = Nothing
            _importedCertificates.TryRemove(cacheKey, discarded)
        End If

        x509Chain = imported.Chain
        Return imported.Certificate
    End Function

    Private Shared Function Import(digitalCertificate As Byte(), digitalCertificateKey As String) As ImportedCertificate
        Dim certificate As X509Certificate2 = Nothing
        Dim x509Chain As New X509Chain()

        Dim certificates As New X509Certificate2Collection()
        certificates.Import(digitalCertificate, digitalCertificateKey, CertificateStorageFlags)
        For Each x509Certificate2 As X509Certificate2 In certificates
            If x509Certificate2.HasPrivateKey Then
                certificate = x509Certificate2
            End If
        Next

        If certificate IsNot Nothing Then
            x509Chain.Build(certificate)
        End If

        Return New ImportedCertificate(certificate, x509Chain)
    End Function

    Private Shared Function BuildCacheKey(digitalCertificate As Byte(), digitalCertificateKey As String) As String
        Using sha256 As SHA256 = SHA256.Create()
            Dim contentHash = BitConverter.ToString(sha256.ComputeHash(digitalCertificate)).Replace("-", String.Empty)
            Return String.Concat(contentHash, "|", digitalCertificateKey)
        End Using
    End Function

End Class
