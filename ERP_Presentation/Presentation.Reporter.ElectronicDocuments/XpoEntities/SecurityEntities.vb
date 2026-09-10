'***********************************************************************
' Assembly         : Presentation.Reporter.Net8
' Entidades XPO para Security
' Adaptado para .NET 8
'***********************************************************************

Imports DevExpress.Xpo

Namespace XpoEntities

#Region "UserXpo"

    <Persistent("Security.User")>
    Public Class UserXpo
        Inherits XPLiteObject

        Dim fId As Integer
        <Key(True)>
        Public Property Id() As Integer
            Get
                Return fId
            End Get
            Set(ByVal value As Integer)
                SetPropertyValue(Of Integer)("Id", fId, value)
            End Set
        End Property

        Dim fIdPerson As PersonXpo
        <Association("SecurityUserReferencesSecurityPerson")>
        Public Property IdPerson() As PersonXpo
            Get
                Return fIdPerson
            End Get
            Set(ByVal value As PersonXpo)
                SetPropertyValue(Of PersonXpo)("IdPerson", fIdPerson, value)
            End Set
        End Property

        Public Property UserCode() As String
        Public Property RollCode() As Integer
        Public Property GroupCode() As Integer
        Public Property Position() As String
        Public Property UserType() As Char
        Public Property ChangePassword() As Boolean
        Public Property DaysChangePassword() As Integer
        Public Property DateLastChangePassword() As DateTime
        Public Property DateExpiryAccount() As DateTime
        Public Property Password() As String
        Public Property State() As Boolean
        Public Property Email() As String
        Public Property ProfileType() As Char

        <PersistentAlias("concat(concat(UserCode,' - '),IdPerson.Fullname)")>
        Public ReadOnly Property CodeName() As String
            Get
                Return Convert.ToString(Me.EvaluateAlias("CodeName"))
            End Get
        End Property

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub

        Public Sub New()
            MyBase.New(Session.DefaultSession)
        End Sub

    End Class

#End Region

#Region "PersonXpo"

    <Persistent("Security.Person")>
    Public Class PersonXpo
        Inherits XPLiteObject

        Dim fId As Integer
        <Key(True)>
        Public Property Id() As Integer
            Get
                Return fId
            End Get
            Set(ByVal value As Integer)
                SetPropertyValue(Of Integer)("Id", fId, value)
            End Set
        End Property

        Public Property Identification() As String
        Public Property FirstName() As String
        Public Property SecondName() As String
        Public Property FirstLastName() As String
        Public Property SecondLastName() As String
        Public Property Fullname() As String

        <Association("SecurityUserReferencesSecurityPerson", GetType(UserXpo))>
        Public ReadOnly Property Users() As XPCollection(Of UserXpo)
            Get
                Return GetCollection(Of UserXpo)("Users")
            End Get
        End Property

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub

        Public Sub New()
            MyBase.New(Session.DefaultSession)
        End Sub

    End Class

#End Region

#Region "ThirdPartyXpo"

    <Persistent("Common.ThirdParty")>
    Public Class ThirdPartyXpo
        Inherits XPLiteObject

        Dim fId As Integer
        <Key(True)>
        Public Property Id() As Integer
            Get
                Return fId
            End Get
            Set(ByVal value As Integer)
                SetPropertyValue(Of Integer)("Id", fId, value)
            End Set
        End Property

        Public Property Nit() As String
        Public Property Name() As String
        Public Property Address() As String
        Public Property Phone() As String
        Public Property DigitalSignature() As Byte()

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub

        Public Sub New()
            MyBase.New(Session.DefaultSession)
        End Sub

    End Class

#End Region

End Namespace
