Imports System.Security.Cryptography

Namespace Crypto

    Public Class DigestMethod

#Region "Properties"

        Private _name As String

        Private _uri As String

        Private _oid As String

        Public Shared SHA1 As DigestMethod = New DigestMethod("SHA1", "http://www.w3.org/2000/09/xmldsig#sha1", "1.3.14.3.2.26")

        Public Shared SHA256 As DigestMethod = new DigestMethod("SHA256", "http://www.w3.org/2001/04/xmlenc#sha256", "2.16.840.1.101.3.4.2.1")

        Public Shared SHA512 As DigestMethod = new DigestMethod("SHA512", "http://www.w3.org/2001/04/xmlenc#sha512", "2.16.840.1.101.3.4.2.3")

        Public Property Name As String
            Get
                Return _name
            End Get
            Set(value As String)
                Me._name = value
            End Set
        End Property

        Public Property URI As String
            Get
                Return _uri
            End Get
            Set(value As String)
                Me._uri = value
            End Set
        End Property

        Public Property Oid() As String
            Get
                Return oid
            End Get
            Set(value As String)
                Me._oid = value
            End Set
        End Property

#End Region

#Region "Builder"

        Private Sub New (name As String, uri As String, oid As String)
            Me._name = name
            Me._uri = uri
            Me._oid = oid
        End Sub

#End Region

#Region "Methods"

        Public Shared Function GetByOid(oid As String) As DigestMethod
            If Not(oid = DigestMethod.SHA1.Oid) Then
                If Not(oid = DigestMethod.SHA256.Oid) Then
                    If Not(oid = DigestMethod.SHA512.Oid) Then
                        Throw New Exception("Unsupported digest method")
                    End If
                    Return DigestMethod.SHA512
                End If
                Return DigestMethod.SHA256
            End If
            Return DigestMethod.SHA1
        End Function

        Public Function GetHashAlgorithm() As HashAlgorithm
            If Not (Name = "SHA1") Then
                If Not (Name = "SHA256") Then
                    If Not (Name = "SHA512") Then
                        Throw New Exception("Unsupported digest method")
                    End If
                    Return System.Security.Cryptography.SHA512.Create()
                End If
                Return System.Security.Cryptography.SHA256.Create()
            End If
            Return System.Security.Cryptography.SHA1.Create()
        End Function

#End Region

    End Class

End NameSpace