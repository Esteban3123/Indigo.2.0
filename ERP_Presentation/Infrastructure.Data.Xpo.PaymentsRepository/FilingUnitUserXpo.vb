'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.PaymentsRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 05/03/2015
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports System
Imports DevExpress.Xpo

#End Region


<Persistent("Payments.FilingUnitUser")> _
Public Class FilingUnitUserXpo
    Inherits XPLiteObject

#Region "Members"

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

    Dim fFillingUnitId As FilingUnitXpo
    <Association("FilingUnitUserReferencesFilingUnit")> _
    Public Property FillingUnitId() As FilingUnitXpo
        Get
            Return fFillingUnitId
        End Get
        Set(ByVal value As FilingUnitXpo)
            SetPropertyValue(Of FilingUnitXpo)("FillingUnitId", fFillingUnitId, value)
        End Set
    End Property

    Dim fUserId As Integer
    Public Property UserId() As Integer
        Get
            Return fUserId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("UserId", fUserId, value)
        End Set
    End Property

    Dim fUserCode As String
    <Size(50)> _
    Public Property UserCode() As String
        Get
            Return fUserCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UserCode", fUserCode, value)
        End Set
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
