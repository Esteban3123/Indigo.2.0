Imports System.Security.Cryptography.X509Certificates

Public Class ConfigurationCertificate

    Public Shared Function ReadCertificateFromFile(certPath As String, certPass As String, ByRef x509Chain As X509Chain) As X509Certificate2
        Dim certificate As X509Certificate2 = Nothing
        x509Chain = new X509Chain()

        Try
            Dim certificates As X509Certificate2Collection = new X509Certificate2Collection()
            certificates.Import(certPath, certPass, X509KeyStorageFlags.PersistKeySet)
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
        Dim certificate As X509Certificate2 = Nothing
        x509Chain = new X509Chain()

        Try
            Dim certificates As X509Certificate2Collection = new X509Certificate2Collection()
            certificates.Import(digitalCertificate, digitalCertificateKey, X509KeyStorageFlags.PersistKeySet)
            For Each x509Certificate2 As X509Certificate2 In certificates
                if x509Certificate2.HasPrivateKey
                    certificate = x509Certificate2
                End If
            Next
            'x509Chain.ChainPolicy.ExtraStore.AddRange(certificates)
            x509Chain.Build(certificate)    
        Catch ex As Exception

        End Try

        return certificate
    End Function

End Class
