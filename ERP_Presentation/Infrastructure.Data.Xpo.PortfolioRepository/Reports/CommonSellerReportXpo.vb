Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Common.Seller")> _
Public Class CommonSellerReportXpo
    Inherits XPLiteObject
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
    Dim fCode As String
    <Size(20)> _
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property
    Dim fThirdPartyId As CommonThirdPartyXpo
    <Association("CommonSellerReportXpoReferencesCommonThirdPartyXpo")> _
    Public Property ThirdPartyId() As CommonThirdPartyXpo
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As CommonThirdPartyXpo)
            SetPropertyValue(Of CommonThirdPartyXpo)("ThirdPartyId", fThirdPartyId, value)
        End Set
    End Property
    Dim fClassification As Byte
    Public Property Classification() As Byte
        Get
            Return fClassification
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Classification", fClassification, value)
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
    'Propiedad Añadida
    Dim fSeleccionado As Boolean = False
    <NonPersistent()> _
    Public Property Seleccionado() As Boolean
        Get
            Return fSeleccionado
        End Get
        Set(ByVal value As Boolean)
            Me.fSeleccionado = value
        End Set
    End Property
    <Association("PortfolioAccountReceivableReportXpoReferencesCommonSellerReportXpo", GetType(PortfolioAccountReceivableReportXpo))> _
    Public ReadOnly Property PortfolioAccountReceivableReportXpo() As XPCollection(Of PortfolioAccountReceivableReportXpo)
        Get
            Return GetCollection(Of PortfolioAccountReceivableReportXpo)("PortfolioAccountReceivableReportXpo")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
