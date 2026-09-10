Namespace Signature.Parameters

    Public Class SignatureCommitmentType

#Region "Properties"

        Public Shared ProofOfOrigin As SignatureCommitmentType = New SignatureCommitmentType("http://uri.etsi.org/01903/v1.2.2#ProofOfOrigin")

        Public Shared ProofOfReceipt As SignatureCommitmentType = New SignatureCommitmentType("http://uri.etsi.org/01903/v1.2.2#ProofOfReceipt")

        Public Shared ProofOfDelivery As SignatureCommitmentType = New SignatureCommitmentType("http://uri.etsi.org/01903/v1.2.2#ProofOfDelivery")

        Public Shared ProofOfSender As SignatureCommitmentType = New SignatureCommitmentType("http://uri.etsi.org/01903/v1.2.2#ProofOfSender")

        Public Shared ProofOfApproval As SignatureCommitmentType = New SignatureCommitmentType("http://uri.etsi.org/01903/v1.2.2#ProofOfApproval")

        Public Shared ProofOfCreation As SignatureCommitmentType = New SignatureCommitmentType("http://uri.etsi.org/01903/v1.2.2#ProofOfCreation")

        Private ReadOnly _uri As String

        Public ReadOnly Property URI() As String
            Get
                Return Me._uri
            End Get
        End Property

#End Region

#Region "Builder"

        Public Sub New(uri As String)
            Me._uri = uri
        End Sub

#End Region

    End Class

End NameSpace