Imports System.Xml

Namespace Signature.Parameters

    Public Class SignatureCommitment

#Region "Properties"

        Public CommitmentType As SignatureCommitmentType

        Public CommitmentTypeQualifiers As List(Of XmlElement)

#End Region

#Region "Builder"

        Public Sub New (commitmentType As SignatureCommitmentType)
            Me.CommitmentType = commitmentType
            Me.CommitmentTypeQualifiers = New List(Of XmlElement)()
        End Sub

#End Region

#Region "Methods"

        Public Sub AddQualifierFromXml(xml As String)
            Dim xmlDocument As XmlDocument = new XmlDocument()
            xmlDocument.LoadXml(xml)
            Me.CommitmentTypeQualifiers.Add(xmlDocument.DocumentElement)
        End Sub

#End Region

    End Class

End NameSpace