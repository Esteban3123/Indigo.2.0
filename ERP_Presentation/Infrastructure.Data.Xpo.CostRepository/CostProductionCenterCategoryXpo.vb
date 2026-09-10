Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Cost.CostProductionCenterCategory")>
Partial Public Class CostProductionCenterCategoryXpo
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
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
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

    <PersistentAlias("concat(Code, ' - ', Name)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Me.EvaluateAlias("CodeName")
        End Get
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

    <PersistentAlias("Iif(Status = 1, 'Activo', 'Inactivo')")>
    Public ReadOnly Property StatusName() As String
        Get
            Return Me.EvaluateAlias("StatusName")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
End Class
