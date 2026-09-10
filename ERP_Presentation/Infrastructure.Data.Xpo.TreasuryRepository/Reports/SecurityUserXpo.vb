'Imports System
'Imports DevExpress.Xpo
'Imports DevExpress.Data.Filtering
'Imports System.Collections.Generic
'Imports System.ComponentModel

'<Persistent("Security.User")> _
'Public Class SecurityUserXpo
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
'    Dim fIdPerson As SecurityPersonXpo
'    '<Indexed(Name:="IX_SEGUSUARU", Unique:=True)> _
'    <Association("Security_UserReferencesSecurity_Person")> _
'    Public Property IdPerson() As SecurityPersonXpo
'        Get
'            Return fIdPerson
'        End Get
'        Set(ByVal value As SecurityPersonXpo)
'            SetPropertyValue(Of SecurityPersonXpo)("IdPerson", fIdPerson, value)
'        End Set
'    End Property
'    Dim fUserCode As String
'    '<Indexed(Name:="IX_User", Unique:=True)> _
'    <Size(20)> _
'    Public Property UserCode() As String
'        Get
'            Return fUserCode
'        End Get
'        Set(ByVal value As String)
'            SetPropertyValue(Of String)("UserCode", fUserCode, value)
'        End Set
'    End Property
'    Dim fRollCode As Integer
'    Public Property RollCode() As Integer
'        Get
'            Return fRollCode
'        End Get
'        Set(ByVal value As Integer)
'            SetPropertyValue(Of Integer)("RollCode", fRollCode, value)
'        End Set
'    End Property
'    Dim fGroupCode As Integer
'    Public Property GroupCode() As Integer
'        Get
'            Return fGroupCode
'        End Get
'        Set(ByVal value As Integer)
'            SetPropertyValue(Of Integer)("GroupCode", fGroupCode, value)
'        End Set
'    End Property
'    Dim fPosition As String
'    <Size(30)> _
'    Public Property Position() As String
'        Get
'            Return fPosition
'        End Get
'        Set(ByVal value As String)
'            SetPropertyValue(Of String)("Position", fPosition, value)
'        End Set
'    End Property
'    Dim fUserType As Char
'    Public Property UserType() As Char
'        Get
'            Return fUserType
'        End Get
'        Set(ByVal value As Char)
'            SetPropertyValue(Of Char)("UserType", fUserType, value)
'        End Set
'    End Property
'    Dim fChangePassword As Boolean
'    Public Property ChangePassword() As Boolean
'        Get
'            Return fChangePassword
'        End Get
'        Set(ByVal value As Boolean)
'            SetPropertyValue(Of Boolean)("ChangePassword", fChangePassword, value)
'        End Set
'    End Property
'    Dim fDaysChangePassword As Integer
'    Public Property DaysChangePassword() As Integer
'        Get
'            Return fDaysChangePassword
'        End Get
'        Set(ByVal value As Integer)
'            SetPropertyValue(Of Integer)("DaysChangePassword", fDaysChangePassword, value)
'        End Set
'    End Property
'    Dim fDateLastChangePassword As DateTime
'    Public Property DateLastChangePassword() As DateTime
'        Get
'            Return fDateLastChangePassword
'        End Get
'        Set(ByVal value As DateTime)
'            SetPropertyValue(Of DateTime)("DateLastChangePassword", fDateLastChangePassword, value)
'        End Set
'    End Property
'    Dim fDateExpiryAccount As DateTime
'    Public Property DateExpiryAccount() As DateTime
'        Get
'            Return fDateExpiryAccount
'        End Get
'        Set(ByVal value As DateTime)
'            SetPropertyValue(Of DateTime)("DateExpiryAccount", fDateExpiryAccount, value)
'        End Set
'    End Property
'    Dim fPassword As String
'    <Size(50)> _
'    Public Property Password() As String
'        Get
'            Return fPassword
'        End Get
'        Set(ByVal value As String)
'            SetPropertyValue(Of String)("Password", fPassword, value)
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
'    Dim fUserNameLync As String
'    Public Property UserNameLync() As String
'        Get
'            Return fUserNameLync
'        End Get
'        Set(ByVal value As String)
'            SetPropertyValue(Of String)("UserNameLync", fUserNameLync, value)
'        End Set
'    End Property
'    Dim fAddressSingInLync As String
'    Public Property AddressSingInLync() As String
'        Get
'            Return fAddressSingInLync
'        End Get
'        Set(ByVal value As String)
'            SetPropertyValue(Of String)("AddressSingInLync", fAddressSingInLync, value)
'        End Set
'    End Property
'    Dim fPasswordLync As String
'    Public Property PasswordLync() As String
'        Get
'            Return fPasswordLync
'        End Get
'        Set(ByVal value As String)
'            SetPropertyValue(Of String)("PasswordLync", fPasswordLync, value)
'        End Set
'    End Property
'    Dim fPersonalNote As String
'    Public Property PersonalNote() As String
'        Get
'            Return fPersonalNote
'        End Get
'        Set(ByVal value As String)
'            SetPropertyValue(Of String)("PersonalNote", fPersonalNote, value)
'        End Set
'    End Property
'    Dim fViewForm As Boolean
'    Public Property ViewForm() As Boolean
'        Get
'            Return fViewForm
'        End Get
'        Set(ByVal value As Boolean)
'            SetPropertyValue(Of Boolean)("ViewForm", fViewForm, value)
'        End Set
'    End Property
'    Dim fCodeInterface As String
'    <Size(3)> _
'    Public Property CodeInterface() As String
'        Get
'            Return fCodeInterface
'        End Get
'        Set(ByVal value As String)
'            SetPropertyValue(Of String)("CodeInterface", fCodeInterface, value)
'        End Set
'    End Property
'    Dim fIsLockedOut As Boolean
'    Public Property IsLockedOut() As Boolean
'        Get
'            Return fIsLockedOut
'        End Get
'        Set(ByVal value As Boolean)
'            SetPropertyValue(Of Boolean)("IsLockedOut", fIsLockedOut, value)
'        End Set
'    End Property
'    Dim fFailedPasswordCount As Integer
'    Public Property FailedPasswordCount() As Integer
'        Get
'            Return fFailedPasswordCount
'        End Get
'        Set(ByVal value As Integer)
'            SetPropertyValue(Of Integer)("FailedPasswordCount", fFailedPasswordCount, value)
'        End Set
'    End Property

'    'Propiedad Añadida
'    Dim fSeleccionado As Boolean = False
'    <NonPersistent()> _
'    Public Property Seleccionado() As Boolean
'        Get
'            Return fSeleccionado
'        End Get
'        Set(ByVal value As Boolean)
'            Me.fSeleccionado = value
'        End Set
'    End Property

'    Public Sub New(ByVal session As Session)
'        MyBase.New(session)
'    End Sub
'    Public Overrides Sub AfterConstruction()
'        MyBase.AfterConstruction()
'    End Sub

'End Class
