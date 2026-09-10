Imports Infrastructure.CrossCutting.Signature.Crypto

Namespace Signature.Parameters

    Public Class SignatureParameters

#Region "Properties"

        Private _defaultSignatureMethod As SignatureMethod = SignatureMethod.RSAwithSHA1

        Private _defaultDigestMethod As DigestMethod = DigestMethod.SHA1

        Public Property Signer As Signer

        Public SignatureMethod As SignatureMethod

        Public DigestMethod As DigestMethod

        Public SigningDate As DateTime?

        Public SignerRole As SignerRole

        Public ReadOnly SignatureCommitments As List(Of SignatureCommitment)

        Public SignatureProductionPlace As SignatureProductionPlace

        Public ReadOnly XPathTransformations As List(Of SignatureXPathExpression)

        Public SignaturePolicyInfo As SignaturePolicyInfo

        Public SignatureDestination As SignatureXPathExpression

        Public SignaturePackaging As SignaturePackaging

        Public DataFormat As DataFormat

        Public ElementIdToSign As String

        Public ExternalContentUri As String

#End Region

#Region "Builder"

        Public Sub New()
            Me.XPathTransformations = New List(Of SignatureXPathExpression)
            Me.SignatureCommitments = New List(Of SignatureCommitment)
            Me.SignatureMethod = _defaultSignatureMethod
            Me.DigestMethod = _defaultDigestMethod
        End Sub

#End Region

    End Class

End NameSpace