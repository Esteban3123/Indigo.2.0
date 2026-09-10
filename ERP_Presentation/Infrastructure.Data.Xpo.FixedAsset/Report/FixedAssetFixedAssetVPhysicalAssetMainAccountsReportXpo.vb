Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.VReportFixedAssetPhysicalMainAccounts")> _
Public Class FixedAssetFixedAssetVPhysicalAssetMainAccountsReportXpo
    Inherits XPLiteObject
    Dim fid As Integer
    <Key(True)> _
    Public Property id() As Integer
        Get
            Return fid
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("id", fid, value)
        End Set
    End Property
    Dim fLegalBookId As Integer
    Public Property LegalBookId() As Integer
        Get
            Return fLegalBookId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("LegalBookId", fLegalBookId, value)
        End Set
    End Property
    Dim fNumber As String
    <Size(50)> _
    Public Property Number() As String
        Get
            Return fNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Number", fNumber, value)
        End Set
    End Property
    Dim fName As String
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property
    Dim fHandlesThirdParty As Boolean
    Public Property HandlesThirdParty() As Boolean
        Get
            Return fHandlesThirdParty
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("HandlesThirdParty", fHandlesThirdParty, value)
        End Set
    End Property
    Dim fHandlesCostCenter As Boolean
    Public Property HandlesCostCenter() As Boolean
        Get
            Return fHandlesCostCenter
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("HandlesCostCenter", fHandlesCostCenter, value)
        End Set
    End Property
    Dim fStatus As Boolean
    Public Property Status() As Boolean
        Get
            Return fStatus
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Status", fStatus, value)
        End Set
    End Property
    Dim fRetencionType As Byte
    Public Property RetencionType() As Byte
        Get
            Return fRetencionType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("RetencionType", fRetencionType, value)
        End Set
    End Property
    Dim fAllowsMovement As Boolean
    Public Property AllowsMovement() As Boolean
        Get
            Return fAllowsMovement
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("AllowsMovement", fAllowsMovement, value)
        End Set
    End Property
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
