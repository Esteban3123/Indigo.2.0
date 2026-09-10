Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Treasury.SettingsTreasury")>
Public Class TreasurySettingsTreasuryXpo
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
    Dim fIdOperatingUnit As Integer
    Public Property IdOperatingUnit() As Integer
        Get
            Return fIdOperatingUnit
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdOperatingUnit", fIdOperatingUnit, value)
        End Set
    End Property
    Dim fGetThirdPartyCashRegister As Byte
    Public Property GetThirdPartyCashRegister() As Byte
        Get
            Return fGetThirdPartyCashRegister
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("GetThirdPartyCashRegister", fGetThirdPartyCashRegister, value)
        End Set
    End Property
    Dim fGetThirdPartyBank As Byte
    Public Property GetThirdPartyBank() As Byte
        Get
            Return fGetThirdPartyBank
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("GetThirdPartyBank", fGetThirdPartyBank, value)
        End Set
    End Property
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class

