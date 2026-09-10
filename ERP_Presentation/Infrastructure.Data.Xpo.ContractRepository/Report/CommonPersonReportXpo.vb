Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Common.Person")> _
Public Class CommonPersonReportXpo
    Inherits XPLiteObject
    Dim fId As Integer
    <Key(True)> _
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property
    Dim fIdentificationNumber As String
    '<Indexed(Name:="IX_Person_IdentificationNumber", Unique:=True)> _
    <Size(15)> _
    Public Property IdentificationNumber() As String
        Get
            Return fIdentificationNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IdentificationNumber", fIdentificationNumber, value)
        End Set
    End Property
    Dim fIdentificationType As Integer
    Public Property IdentificationType() As Integer
        Get
            Return fIdentificationType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdentificationType", fIdentificationType, value)
        End Set
    End Property
    Dim fIdentificacionCityId As Integer
    Public Property IdentificacionCityId() As Integer
        Get
            Return fIdentificacionCityId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdentificacionCityId", fIdentificacionCityId, value)
        End Set
    End Property
    Dim fIdentificationExpeditionDate As DateTime
    Public Property IdentificationExpeditionDate() As DateTime
        Get
            Return fIdentificationExpeditionDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("IdentificationExpeditionDate", fIdentificationExpeditionDate, value)
        End Set
    End Property
    Dim fMilitaryCardId As Integer
    Public Property MilitaryCardId() As Integer
        Get
            Return fMilitaryCardId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("MilitaryCardId", fMilitaryCardId, value)
        End Set
    End Property
    Dim fMilitaryCardNumber As String
    <Size(15)> _
    Public Property MilitaryCardNumber() As String
        Get
            Return fMilitaryCardNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("MilitaryCardNumber", fMilitaryCardNumber, value)
        End Set
    End Property
    Dim fFirstName As String
    <Size(50)> _
    Public Property FirstName() As String
        Get
            Return fFirstName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FirstName", fFirstName, value)
        End Set
    End Property
    Dim fSecondName As String
    <Size(50)> _
    Public Property SecondName() As String
        Get
            Return fSecondName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SecondName", fSecondName, value)
        End Set
    End Property
    Dim fFirstLastName As String
    <Size(50)> _
    Public Property FirstLastName() As String
        Get
            Return fFirstLastName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FirstLastName", fFirstLastName, value)
        End Set
    End Property
    Dim fSecondLastName As String
    <Size(50)> _
    Public Property SecondLastName() As String
        Get
            Return fSecondLastName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SecondLastName", fSecondLastName, value)
        End Set
    End Property
    Dim fBirthDate As DateTime
    Public Property BirthDate() As DateTime
        Get
            Return fBirthDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("BirthDate", fBirthDate, value)
        End Set
    End Property
    Dim fBirthCityId As Integer
    Public Property BirthCityId() As Integer
        Get
            Return fBirthCityId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("BirthCityId", fBirthCityId, value)
        End Set
    End Property
    Dim fDeathDate As DateTime
    Public Property DeathDate() As DateTime
        Get
            Return fDeathDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("DeathDate", fDeathDate, value)
        End Set
    End Property
    Dim fGender As Byte
    Public Property Gender() As Byte
        Get
            Return fGender
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Gender", fGender, value)
        End Set
    End Property
    Dim fBloodGroup As String
    <Size(2)> _
    Public Property BloodGroup() As String
        Get
            Return fBloodGroup
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BloodGroup", fBloodGroup, value)
        End Set
    End Property
    Dim fRH As Char
    Public Property RH() As Char
        Get
            Return fRH
        End Get
        Set(ByVal value As Char)
            SetPropertyValue(Of Char)("RH", fRH, value)
        End Set
    End Property
    Dim fFingerprint() As Byte
    <Size(SizeAttribute.Unlimited)> _
    Public Property Fingerprint() As Byte()
        Get
            Return fFingerprint
        End Get
        Set(ByVal value As Byte())
            SetPropertyValue(Of Byte())("Fingerprint", fFingerprint, value)
        End Set
    End Property
    Dim fSonNumber As Byte
    Public Property SonNumber() As Byte
        Get
            Return fSonNumber
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("SonNumber", fSonNumber, value)
        End Set
    End Property
    Dim fDependents As Byte
    Public Property Dependents() As Byte
        Get
            Return fDependents
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Dependents", fDependents, value)
        End Set
    End Property
    Dim fMaritalStatus As Byte
    Public Property MaritalStatus() As Byte
        Get
            Return fMaritalStatus
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("MaritalStatus", fMaritalStatus, value)
        End Set
    End Property
    Dim fState As Boolean
    Public Property State() As Boolean
        Get
            Return fState
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("State", fState, value)
        End Set
    End Property
    <Association("Common_ThirdPartyReferencesCommon_Person", GetType(CommonThirdPartyReportXpo))> _
    Public ReadOnly Property Common_ThirdPartys() As XPCollection(Of CommonThirdPartyReportXpo)
        Get
            Return GetCollection(Of CommonThirdPartyReportXpo)("Common_ThirdPartys")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
