Imports System.Security.Cryptography.X509Certificates

Namespace Signature.Parameters

    Public Class SignerRole

#Region "Properties"

        Private _certifiedRoles As List(Of X509Certificate)

        Private _claimedRoles As List(Of String)

        Public ReadOnly Property CertifiedRoles As List(Of X509Certificate)
            Get
                Return Me._certifiedRoles
            End Get
        End Property

        Public ReadOnly Property ClaimedRoles As List(Of String)
            Get
                Return Me._claimedRoles
            End Get
        End Property

#End Region

#Region "Builder"

        Public Sub New()
            Me._certifiedRoles = New List(Of X509Certificate)
            Me._claimedRoles = New List(Of String)
        End Sub

#End Region

    End Class

End Namespace