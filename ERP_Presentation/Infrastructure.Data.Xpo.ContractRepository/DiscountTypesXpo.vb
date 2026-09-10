'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.ContractRepository
' Author           : Angi Camila Durán Vargas
' Created          : 16/03/2023
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"
Imports DevExpress.Xpo
#End Region

''' <summary>
''' conceptos de nota usado en los servicios Xpo
''' </summary>
<Persistent("Contract.DiscountTypes")>
Public Class DiscountTypesXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fId As Integer
    <Key(True)>
    <Persistent("Id")>
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property

    Dim fCode As String
    <Persistent("Code")>
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property

    Dim fDiscountType As String
    <Persistent("DiscountType")>
    Public Property DiscountType() As String
        Get
            Return fDiscountType
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DiscountType", fDiscountType, value)
        End Set
    End Property

    Dim fDiscountPercentage As Decimal
    <Persistent("DiscountPercentage")>
    Public Property DiscountPercentage() As Decimal
        Get
            Return fDiscountPercentage
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("DiscountPercentage", fDiscountPercentage, value)
        End Set
    End Property


    'columna que devuelve el nit y el nombre concatenado
    <PersistentAlias("concat(Code,' - ',Name)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property

#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

#End Region


End Class
