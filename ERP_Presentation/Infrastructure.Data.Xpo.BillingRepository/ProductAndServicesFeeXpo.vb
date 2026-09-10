'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.CrystalRepository
' Author           : Andres Alarcon
' Created          : 2023-05-25
'
' Copyright        : (c) . All rights reserved.
'*************************************

Imports DevExpress.Xpo

<Persistent("Billing.ProductAndServiceFee")>
Public Class ProductAndServicesFeeXpo
    Inherits XPLiteObject

#Region "Members"

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
    <Size(20)>
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property

    Dim fStatus As Byte
    Public Property Status() As Byte
        Get
            Return fStatus
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Status", fStatus, value)
        End Set
    End Property

    <PersistentAlias("Iif(Status = 1, 'Activo', Iif(Status = 0, 'Inactivo', ''))")>
    Public ReadOnly Property StatusName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
    End Property

    <PersistentAlias("concat(Code,' - ',Name)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Me.EvaluateAlias("CodeName").ToString()
        End Get
    End Property

    <Association("Billing_ProductAndServicesFee_Users", GetType(ProductAndServiceFeeUserDetailXpo))>
    Public ReadOnly Property ProductAndServiceFeeUserDetailXpo() As XPCollection(Of ProductAndServiceFeeUserDetailXpo)
        Get
            Return GetCollection(Of ProductAndServiceFeeUserDetailXpo)("ProductAndServiceFeeUserDetailXpo")
        End Get
    End Property

#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

#End Region
End Class
