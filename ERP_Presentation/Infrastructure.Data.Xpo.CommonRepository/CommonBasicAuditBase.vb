Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

Public Class CommonBasicAuditBase
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
    Dim fTag As Integer
    Public Property Tag() As Integer
        Get
            Return fTag
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Tag", fTag, value)
        End Set
    End Property
    Dim fEntity As String
    Public Property Entity() As String
        Get
            Return fEntity
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Entity", fEntity, value)
        End Set
    End Property
    Dim fRegisterId As String
    <Size(20)> _
    Public Property RegisterId() As String
        Get
            Return fRegisterId
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RegisterId", fRegisterId, value)
        End Set
    End Property
    Dim fUserCode As String
    <Size(20)> _
    Public Property UserCode() As String
        Get
            Return fUserCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UserCode", fUserCode, value)
        End Set
    End Property
    Dim fUserName As String
    <Size(200)> _
    Public Property UserName() As String
        Get
            Return fUserName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UserName", fUserName, value)
        End Set
    End Property
    Dim fTransactionDate As DateTime
    Public Property TransactionDate() As DateTime
        Get
            Return fTransactionDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("TransactionDate", fTransactionDate, value)
        End Set
    End Property
    Dim fOperation As Byte
    Public Property Operation() As Byte
        Get
            Return fOperation
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Operation", fOperation, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
