Imports Infrastructure.CrossCutting.Signature.Crypto

Namespace Signature.Parameters

    Public Class SignaturePolicyInfo

#Region "Properties"

        Private _defaultPolicyDigestAlgorithm As DigestMethod = DigestMethod.SHA1

        Public PolicyIdentifier As String

        Public PolicyDescription As String

        Public PolicyHash As String

        Public PolicyDigestAlgorithm As DigestMethod

        Public PolicyUri As String

#End Region

#Region "Builder"

        Public Sub New ()
            PolicyDigestAlgorithm = _defaultPolicyDigestAlgorithm
        End Sub

#End Region

    End Class

End NameSpace