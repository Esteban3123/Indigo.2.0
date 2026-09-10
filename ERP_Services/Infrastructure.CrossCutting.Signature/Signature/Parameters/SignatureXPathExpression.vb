Namespace Signature.Parameters

    Public Class SignatureXPathExpression

#Region "Properties"

        Private _namespaces As Dictionary(Of String, string)

        Public XPathExpression As String

        Public Property Namespaces As Dictionary(Of String, string)
            Get
                Return _namespaces
            End Get
            Set(value As Dictionary(Of String, string))
                Me._namespaces = value
            End Set
        End Property

#End Region

#Region "Builder"

        Public Sub New ()
            _namespaces = New Dictionary(Of String,String)()
        End Sub

#End Region

    End Class

End NameSpace