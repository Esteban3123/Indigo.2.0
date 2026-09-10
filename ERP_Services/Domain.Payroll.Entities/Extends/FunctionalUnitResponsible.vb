Imports System.Runtime.Serialization

Partial Public Class FunctionalUnitResponsible

    Private _user As String

    <DataMember()> _
    Public Property User() As String
        Get
            Return _user
        End Get
        Set(ByVal value As String)
            If Not Equals(_user, value) Then
                _user = value
                OnPropertyChanged("User")
            End If
        End Set
    End Property

    Private _fullname As String

    <DataMember()>
    Public Property Fullname() As String
        Get
            Return _fullname
        End Get
        Set(ByVal value As String)
            If Not Equals(_fullname, value) Then
                _fullname = value
                OnPropertyChanged("User")
            End If
        End Set
    End Property

    <DataMember()>
    Property CodeFunctionalUnit As String

    <DataMember()>
    Property NameFunctionalUnit As String

End Class
