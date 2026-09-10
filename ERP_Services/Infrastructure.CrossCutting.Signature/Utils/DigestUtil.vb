Imports System.Security.Cryptography
Imports Microsoft.Xades

Namespace Utils

    Public Class DigestUtil

#Region "Builder"

        Public Shared Sub SetCertDigest(rawCert As Byte(), digestMethod As Crypto.DigestMethod, destination As DigestAlgAndValueType)
            Using hashAlgorithm As System.Security.Cryptography.HashAlgorithm = digestMethod.GetHashAlgorithm()
                destination.DigestMethod.Algorithm = digestMethod.URI
                destination.DigestValue = hashAlgorithm.ComputeHash(rawCert)
            End Using
        End Sub

        Public Shared Function ComputeHashValue(value As Byte(), digestMethod As Crypto.DigestMethod) As Byte()
            Using hashAlgorithm As HashAlgorithm = digestMethod.GetHashAlgorithm()
                Return hashAlgorithm.ComputeHash(value)
            End Using
        End Function

#End Region

    End Class

End Namespace