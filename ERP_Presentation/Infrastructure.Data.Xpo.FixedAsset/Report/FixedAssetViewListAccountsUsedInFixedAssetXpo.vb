Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.ViewListAccountsUsedInFixedAsset")>
Public Class FixedAssetViewListAccountsUsedInFixedAssetXpo
    Inherits XPLiteObject
    Dim fid As Integer
    <Key(True)>
    Public Property id() As Integer
        Get
            Return fid
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("id", fid, value)
        End Set
    End Property
    Dim fNumber As String
    <Size(50)>
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
    Dim fStatus As Boolean
    Public Property Status() As Boolean
        Get
            Return fStatus
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Status", fStatus, value)
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
    Dim fType As Integer
    Public Property Type() As Integer
        Get
            Return fType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Type", fType, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
