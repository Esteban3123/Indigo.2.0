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
<Persistent("Contract.CUPSEntity")> _
Public Class CupsEntityXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fId As Integer
    <Key(True)> _
    <Persistent("Id")> _
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property

    Dim fCode As String
    <Size(20)> _
    <Persistent("Code")> _
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property

    Dim fDescription As String
    <Size(300)> _
    <Persistent("Description")> _
    Public Property Description() As String
        Get
            Return fDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Description", fDescription, value)
        End Set
    End Property

    'columna que devuelve el nit y el nombre concatenado
    <Size(50)> _
    <PersistentAlias("concat(concat(Code,' - '),Description)")>
    Public ReadOnly Property CodeDescription() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeDescription"))
        End Get
    End Property

    <Association("CUPSReferencesCUPS", GetType(AuthorizationPortfolioCUPSEntityXpo))>
    Public ReadOnly Property AuthorizationPortfolioCUPSEntityXpo() As XPCollection(Of AuthorizationPortfolioCUPSEntityXpo)
        Get
            Return GetCollection(Of AuthorizationPortfolioCUPSEntityXpo)("AuthorizationPortfolioCUPSEntityXpo")
        End Get
    End Property

    <Association("AuthorizationOutsourcedServicesServiceOrderDetailReferencesCUPS", GetType(AuthorizationOutsourcedServicesServiceOrderDetailXpo))>
    Public ReadOnly Property AuthorizationOutsourcedServicesServiceOrderDetailXpo() As XPCollection(Of AuthorizationOutsourcedServicesServiceOrderDetailXpo)
        Get
            Return GetCollection(Of AuthorizationOutsourcedServicesServiceOrderDetailXpo)("AuthorizationOutsourcedServicesServiceOrderDetailXpo")
        End Get
    End Property

    <Association("AuthorizationOutsourcedServicesPharmaceuticalDispensingDetailReferencesCUPS", GetType(AuthorizationOutsourcedServicesPharmaceuticalDispensingDetailXpo))>
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
