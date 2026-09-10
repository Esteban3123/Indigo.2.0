Imports System.Runtime.Serialization

Partial Public Class AuthorizationConcept

    Private _apply As Boolean = False

    <DataMember()>
    Public Property Apply() As Boolean
        Get
            Return _apply
        End Get
        Set(ByVal value As Boolean)
            If Not Equals(_apply, value) Then
                _apply = value
                OnPropertyChanged("Apply")
            End If
        End Set
    End Property

    <DataMember>
    Public Property ConceptValue As Double

End Class
