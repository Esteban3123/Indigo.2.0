Imports System.Runtime.Serialization
Imports Domain.Base.Entities

Partial Class INPACIENT
    Inherits Entity(Of INPACIENT)

    <DataMember()>
    Public Property CompanyDesc As String

    <DataMember()>
    Public Property LocationDesc As String

    <DataMember()>
    Public Property ActivityDesc As String

    <DataMember()>
    Public Property EtGroupDesc As String

    <DataMember()>
    Public Property BeliefDesc As String

    <DataMember()>
    Public Property DisabilityDesc As String

    <DataMember()>
    Public Property LanguageDesc As String

    <DataMember()>
    Public Property LevelDescription As String


    <DataMember()>
    Public Property EducationLevelDesc As String

    <DataMember()>
    Public Property SpecialGroupDesc As String

    <DataMember()>
    Public Property CareGroupDesc As String

    <DataMember()>
    Public Property SonNumber As Integer

    <DataMember()>
    Public Property AnonymousType As String

    <DataMember()>
    Public Property AnonimusAdult As Boolean

    <DataMember()>
    Public Property AnonimusRegBaby As Boolean

    <DataMember()>
    Public Property CodAnonimusAdult As String
    <DataMember()>
    Public Property EntityDescription As String

    <DataMember()>
    Public ExpeditionCityDescription As String



End Class


