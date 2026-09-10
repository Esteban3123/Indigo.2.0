Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Glosas.GlosasParametersInterface")> _
Public Class GlosasParametersInterfaceXpo
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
    Dim fContainerName As String
    <Size(50)> _
    Public Property ContainerName() As String
        Get
            Return fContainerName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ContainerName", fContainerName, value)
        End Set
    End Property
    Dim fCompanyName As String
    Public Property CompanyName() As String
        Get
            Return fCompanyName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CompanyName", fCompanyName, value)
        End Set
    End Property
    Dim fInterface1 As Boolean
    <Persistent("Interface")> _
    Public Property Interface1() As Boolean
        Get
            Return fInterface1
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Interface1", fInterface1, value)
        End Set
    End Property
    Dim fAccountingMethod As Integer
    Public Property AccountingMethod() As Integer
        Get
            Return fAccountingMethod
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AccountingMethod", fAccountingMethod, value)
        End Set
    End Property
    'Dim fAffectsPortfolio As Boolean
    'Public Property AffectsPortfolio() As Boolean
    '    Get
    '        Return fAffectsPortfolio
    '    End Get
    '    Set(ByVal value As Boolean)
    '        SetPropertyValue(Of Boolean)("AffectsPortfolio", fAffectsPortfolio, value)
    '    End Set
    'End Property
    Dim fAffectsService As Boolean
    Public Property AffectsService() As Boolean
        Get
            Return fAffectsService
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("AffectsService", fAffectsService, value)
        End Set
    End Property

    Dim fDateConfiguration As DateTime
    Public Property DateConfiguration() As DateTime
        Get
            Return fDateConfiguration
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("DateConfiguration", fDateConfiguration, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class




