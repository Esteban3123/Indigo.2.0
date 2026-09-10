'Imports System
'Imports DevExpress.Xpo
'Imports DevExpress.Data.Filtering
'Imports System.Collections.Generic
'Imports System.ComponentModel

'<Persistent("Security.Person")> _
'Public Class SecurityPersonXpo
'    Inherits XPLiteObject
'    Dim fId As Integer
'    <Key(True)> _
'    Public Property Id() As Integer
'        Get
'            Return fId
'        End Get
'        Set(ByVal value As Integer)
'            SetPropertyValue(Of Integer)("Id", fId, value)
'        End Set
'    End Property
'    Dim fIdentification As String
'    '<Indexed(Name:="IX_Person", Unique:=True)> _
'    <Size(15)> _
'    Public Property Identification() As String
'        Get
'            Return fIdentification
'        End Get
'        Set(ByVal value As String)
'            SetPropertyValue(Of String)("Identification", fIdentification, value)
'        End Set
'    End Property
'    Dim fIdentificationType As Short
'    Public Property IdentificationType() As Short
'        Get
'            Return fIdentificationType
'        End Get
'        Set(ByVal value As Short)
'            SetPropertyValue(Of Short)("IdentificationType", fIdentificationType, value)
'        End Set
'    End Property
'    Dim fFirstName As String
'    <Size(50)> _
'    Public Property FirstName() As String
'        Get
'            Return fFirstName
'        End Get
'        Set(ByVal value As String)
'            SetPropertyValue(Of String)("FirstName", fFirstName, value)
'        End Set
'    End Property
'    Dim fSecondName As String
'    <Size(50)> _
'    Public Property SecondName() As String
'        Get
'            Return fSecondName
'        End Get
'        Set(ByVal value As String)
'            SetPropertyValue(Of String)("SecondName", fSecondName, value)
'        End Set
'    End Property
'    Dim fFirstLastName As String
'    <Size(50)> _
'    Public Property FirstLastName() As String
'        Get
'            Return fFirstLastName
'        End Get
'        Set(ByVal value As String)
'            SetPropertyValue(Of String)("FirstLastName", fFirstLastName, value)
'        End Set
'    End Property
'    Dim fSecondLastName As String
'    <Size(50)> _
'    Public Property SecondLastName() As String
'        Get
'            Return fSecondLastName
'        End Get
'        Set(ByVal value As String)
'            SetPropertyValue(Of String)("SecondLastName", fSecondLastName, value)
'        End Set
'    End Property
'    Dim fFullname As String
'    <Size(250)> _
'    Public Property Fullname() As String
'        Get
'            Return fFullname
'        End Get
'        Set(ByVal value As String)
'            SetPropertyValue(Of String)("Fullname", fFullname, value)
'        End Set
'    End Property
'    Dim fBirthDay As DateTime
'    Public Property BirthDay() As DateTime
'        Get
'            Return fBirthDay
'        End Get
'        Set(ByVal value As DateTime)
'            SetPropertyValue(Of DateTime)("BirthDay", fBirthDay, value)
'        End Set
'    End Property
'    Dim fFingerprint() As Byte
'    <Size(SizeAttribute.Unlimited)> _
'    Public Property Fingerprint() As Byte()
'        Get
'            Return fFingerprint
'        End Get
'        Set(ByVal value As Byte())
'            SetPropertyValue(Of Byte())("Fingerprint", fFingerprint, value)
'        End Set
'    End Property
'    Dim fGender As Short
'    Public Property Gender() As Short
'        Get
'            Return fGender
'        End Get
'        Set(ByVal value As Short)
'            SetPropertyValue(Of Short)("Gender", fGender, value)
'        End Set
'    End Property
'    Dim fState As Boolean
'    Public Property State() As Boolean
'        Get
'            Return fState
'        End Get
'        Set(ByVal value As Boolean)
'            SetPropertyValue(Of Boolean)("State", fState, value)
'        End Set
'    End Property
'    <Association("Security_UserReferencesSecurity_Person", GetType(SecurityUserXpo))> _
'    Public ReadOnly Property Security_Users() As XPCollection(Of SecurityUserXpo)
'        Get
'            Return GetCollection(Of SecurityUserXpo)("Security_Users")
'        End Get
'    End Property

'    Public Sub New(ByVal session As Session)
'        MyBase.New(session)
'    End Sub
'    Public Overrides Sub AfterConstruction()
'        MyBase.AfterConstruction()
'    End Sub

'End Class
