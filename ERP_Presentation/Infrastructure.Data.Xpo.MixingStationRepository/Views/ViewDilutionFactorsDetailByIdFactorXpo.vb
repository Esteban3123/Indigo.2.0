'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Andres Alarcon
' Created          : 23/11/2022
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("MixingStation.ViewDilutionFactorsDetailByIdFactor")>
Partial Public Class ViewDilutionFactorsDetailByIdFactorXpo
    Inherits XPLiteObject

#Region "Builder"
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

    Dim fDilutionFactorId As Integer
    Public Property DilutionFactorId() As Integer
        Get
            Return fDilutionFactorId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("DilutionFactorId", fDilutionFactorId, value)
        End Set
    End Property

    Dim fDilutionFactorDetailId As Integer
    Public Property DilutionFactorDetailId() As Integer
        Get
            Return fDilutionFactorDetailId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("DilutionFactorDetailId", fDilutionFactorDetailId, value)
        End Set
    End Property

    Dim fATCDilutionFactor As Integer
    Public Property ATCDilutionFactor() As Integer
        Get
            Return fATCDilutionFactor
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ATCDilutionFactor", fATCDilutionFactor, value)
        End Set
    End Property

    Dim fATCDilutionFactorDetail As Integer
    Public Property ATCDilutionFactorDetail() As Integer
        Get
            Return fATCDilutionFactorDetail
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ATCDilutionFactorDetail", fATCDilutionFactorDetail, value)
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

    Dim fCodeFactor As String
    Public Property CodeFactor() As String
        Get
            Return fCodeFactor
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeFactor", fCodeFactor, value)
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

#End Region
#Region "PersistentAlias"

    <PersistentAlias("concat(concat(Code,' - '),Name)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property
#End Region
End Class