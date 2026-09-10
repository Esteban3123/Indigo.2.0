'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.ContractRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 25/09/2014
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports System
Imports DevExpress.Xpo

#End Region

''' <summary>
''' conceptos de nota usado en los servicios Xpo
''' </summary>
<Persistent("Inventory.InventoryProduct")>
Public Class InventoryProductXpo
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
    <Size(20)>
    <Persistent("Code")>
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property

    Dim fName As String
    <Size(300)>
    <Persistent("Name")>
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property

    'columna que devuelve el nit y el nombre concatenado
    <Size(50)>
    <PersistentAlias("concat(concat(Code,' - '),Name)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property

    <Association("ProductReferencesProduct", GetType(AuthorizationPortfolioInventoryProductXpo))>
    Public ReadOnly Property AuthorizationPortfolioInventoryProductXpo() As XPCollection(Of AuthorizationPortfolioInventoryProductXpo)
        Get
            Return GetCollection(Of AuthorizationPortfolioInventoryProductXpo)("AuthorizationPortfolioInventoryProductXpo")
        End Get
    End Property

    <Association("AuthorizationOutsourcedServicesServiceOrderDetailReferencesProduct", GetType(AuthorizationOutsourcedServicesServiceOrderDetailXpo))>
    Public ReadOnly Property AuthorizationOutsourcedServicesServiceOrderDetailXpo() As XPCollection(Of AuthorizationOutsourcedServicesServiceOrderDetailXpo)
        Get
            Return GetCollection(Of AuthorizationOutsourcedServicesServiceOrderDetailXpo)("AuthorizationOutsourcedServicesServiceOrderDetailXpo")
        End Get
    End Property

    <Association("AuthorizationOutsourcedServicesPharmaceuticalDispensingDetailReferencesProduct", GetType(AuthorizationOutsourcedServicesPharmaceuticalDispensingDetailXpo))>
    Public ReadOnly Property AuthorizationOutsourcedServicesPharmaceuticalDispensingDetailXpo() As XPCollection(Of AuthorizationOutsourcedServicesPharmaceuticalDispensingDetailXpo)
        Get
            Return GetCollection(Of AuthorizationOutsourcedServicesPharmaceuticalDispensingDetailXpo)("AuthorizationOutsourcedServicesPharmaceuticalDispensingDetailXpo")
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
