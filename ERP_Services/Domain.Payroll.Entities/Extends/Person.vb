Imports System.Runtime.Serialization

Partial Public Class Person

    Private _identificationCityName As String

    <DataMember()>
    Public Property IdentificationCityName As String
        Get
            Return Me._identificationCityName
        End Get
        Set(value As String)
            Me._identificationCityName = value
        End Set
    End Property

    Private _birthCityName As String

    <DataMember()>
    Public Property BirthCityName As String
        Get
            Return Me._birthCityName
        End Get
        Set(value As String)
            Me._birthCityName = value
        End Set
    End Property

    Private _ReligiousBeliefsDescription As String

     <DataMember()>
    Public Property ReligiousBeliefsDescription() As String
        Get
            Return Me._ReligiousBeliefsDescription
        End Get
        Set(ByVal value As String)
            _ReligiousBeliefsDescription = value
        End Set
    End Property

    Private _EthnicGroupsDescription As String

    <DataMember()>
    Public Property EthnicGroupsDescription() As String
        Get
            Return Me._EthnicGroupsDescription
        End Get
        Set(ByVal value As String)
            Me._EthnicGroupsDescription = value
        End Set
    End Property

    Private _DocumentTypeAbbreviation As String
    <DataMember()>
    Public Property DocumentTypeAbbreviation() As String
        Get
            Return Me._DocumentTypeAbbreviation
        End Get
        Set(ByVal value As String)
            Me._DocumentTypeAbbreviation = value
        End Set
    End Property

End Class
