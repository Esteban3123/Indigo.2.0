Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Contract.Groupers")>
Public Class GroupersXpo
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

    <Association("Contract_GroupersCareGroupReferencesContract_Group", GetType(GroupersCareGroupXpo))>
    Public ReadOnly Property Contract_GroupersCareGroups() As XPCollection(Of GroupersCareGroupXpo)
        Get
            Return GetCollection(Of GroupersCareGroupXpo)("Contract_GroupersCareGroups")
        End Get
    End Property

    Dim fCode As String
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property

    Dim fDescription As String
    Public Property Description() As String
        Get
            Return fDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Description", fDescription, value)
        End Set
    End Property

    Dim fUserMin As Integer
    Public Property UserMin() As Integer
        Get
            Return fUserMin
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("UserMin", fUserMin, value)
        End Set
    End Property

    Dim fUserMax As Integer
    Public Property UserMax() As Integer
        Get
            Return fUserMax
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("UserMax", fUserMax, value)
        End Set
    End Property

    Dim fProjectCME As Decimal
    Public Property ProjectCME() As Decimal
        Get
            Return fProjectCME
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ProjectCME", fProjectCME, value)
        End Set
    End Property

    Dim fTotalContract As Decimal
    Public Property TotalContract() As Decimal
        Get
            Return fTotalContract
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalContract", fTotalContract, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
