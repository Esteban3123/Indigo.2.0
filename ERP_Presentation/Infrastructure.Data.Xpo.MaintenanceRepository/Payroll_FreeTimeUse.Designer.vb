Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Payroll.FreeTimeUse")>
Partial Public Class Payroll_FreeTimeUse
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
        Dim fCode As String
        <Indexed(Name:="IX_FreeTimeUse", Unique:=True)>
        <Size(20)>
        Public Property Code() As String
            Get
                Return fCode
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("Code", fCode, value)
            End Set
        End Property
        Dim fName As String
        <Size(30)>
        Public Property Name() As String
            Get
                Return fName
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("Name", fName, value)
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
        Dim fCreationUser As String
        <Size(20)>
        Public Property CreationUser() As String
            Get
                Return fCreationUser
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("CreationUser", fCreationUser, value)
            End Set
        End Property
        Dim fCreationDate As DateTime
        Public Property CreationDate() As DateTime
            Get
                Return fCreationDate
            End Get
            Set(ByVal value As DateTime)
                SetPropertyValue(Of DateTime)("CreationDate", fCreationDate, value)
            End Set
        End Property
        Dim fModificationUser As String
        <Size(20)>
        Public Property ModificationUser() As String
            Get
                Return fModificationUser
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("ModificationUser", fModificationUser, value)
            End Set
        End Property
        Dim fModificationDate As DateTime
        Public Property ModificationDate() As DateTime
            Get
                Return fModificationDate
            End Get
            Set(ByVal value As DateTime)
                SetPropertyValue(Of DateTime)("ModificationDate", fModificationDate, value)
            End Set
        End Property
        <Association("Common_PersonFreeTimeUseReferencesPayroll_FreeTimeUse")>
        Public ReadOnly Property Common_PersonFreeTimeUses() As XPCollection(Of Common_PersonFreeTimeUse)
            Get
                Return GetCollection(Of Common_PersonFreeTimeUse)("Common_PersonFreeTimeUses")
            End Get
        End Property
End Class