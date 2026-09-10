Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Common.ThirdPartyFiscalResponsibility")>
Public Class CommonThirdPartyFiscalResponsibilityXpo
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

    Dim fThirdPartyId As CommonThirdPartyXpo
    <Association("FiscalResponsibilityReferencesThirdParty")>
    Public Property ThirdPartyId() As CommonThirdPartyXpo
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As CommonThirdPartyXpo)
            SetPropertyValue(Of CommonThirdPartyXpo)("ThirdPartyId", fThirdPartyId, value)
        End Set
    End Property

    Dim fFiscalResponsibilityId As CommonFiscalResponsibilityXpo
    <Association("ThirdPartyFiscalResponsibilityReferencesFiscalResponsibility")>
    Public Property FiscalResponsibilityId() As CommonFiscalResponsibilityXpo
        Get
            Return fFiscalResponsibilityId
        End Get
        Set(ByVal value As CommonFiscalResponsibilityXpo)
            SetPropertyValue(Of CommonFiscalResponsibilityXpo)("FiscalResponsibilityId", fFiscalResponsibilityId, value)
        End Set
    End Property

#Region "Constructors"
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
