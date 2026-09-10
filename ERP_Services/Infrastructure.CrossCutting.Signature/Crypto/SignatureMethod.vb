Namespace Crypto

    Public Class SignatureMethod

#Region "Properties"

        Dim _name As String

        Dim _uri As String

        Public Shared RSAwithSHA1 As SignatureMethod = new SignatureMethod("RSAwithSHA1", "http://www.w3.org/2000/09/xmldsig#rsa-sha1")

        Public Shared RSAwithSHA256 As SignatureMethod = new SignatureMethod("RSAwithSHA256", "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256")

        Public Shared RSAwithSHA512 As SignatureMethod = new SignatureMethod("RSAwithSHA512", "http://www.w3.org/2001/04/xmldsig-more#rsa-sha512")

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

#End Region

#Region "Builder"

        Private Sub New (name As String, uri As String)
            Me._name = name
            Me._uri = uri
        End Sub

#End Region

    End Class

End NameSpace