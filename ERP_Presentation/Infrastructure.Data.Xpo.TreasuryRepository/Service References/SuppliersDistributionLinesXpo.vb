'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.TreasuryRepository
' Author           : Diego Andrés Roldán Lozano
' Created          : 19-08-2014
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"
Imports System
Imports DevExpress.Xpo
#End Region

<Persistent("Common.SuppliersDistributionLines")> _
Public Class SuppliersDistributionLinesXpo
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
    Dim fIdSupplier As Integer
    <Persistent("IdSupplier")> _
    Public Property IdSupplier() As Integer
        Get
            Return fIdSupplier
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdSupplier", fIdSupplier, value)
        End Set
    End Property
    Dim fIdDistributionLine As Integer
    <Persistent("IdDistributionLine")> _
    Public Property IdDistributionLine() As Integer
        Get
            Return fIdDistributionLine
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdDistributionLine", fIdDistributionLine, value)
        End Set
    End Property
    Dim fStatus As Boolean
    <Persistent("Status")> _
    Public Property Status() As Boolean
        Get
            Return fStatus
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Status", fStatus, value)
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
