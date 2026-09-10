Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Common.Supplier")> _
Public Class CommonSupplierXpo
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
    Dim fIdThirdParty As CommonThirdsPartyXpo
    '<Indexed(Name:="IX_Supplier", Unique:=True)> _
    <Association("Common_SupplierReferencesCommon_ThirdParty")> _
    Public Property IdThirdParty() As CommonThirdsPartyXpo
        Get
            Return fIdThirdParty
        End Get
        Set(ByVal value As CommonThirdsPartyXpo)
            SetPropertyValue(Of CommonThirdsPartyXpo)("IdThirdParty", fIdThirdParty, value)
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
    Dim fCodeCMMS As String
    <Size(20)> _
    Public Property CodeCMMS() As String
        Get
            Return fCodeCMMS
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeCMMS", fCodeCMMS, value)
        End Set
    End Property
    Dim fWebSite As String
    <Size(80)> _
    Public Property WebSite() As String
        Get
            Return fWebSite
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("WebSite", fWebSite, value)
        End Set
    End Property
    Dim fIdCity As Integer
    Public Property IdCity() As Integer
        Get
            Return fIdCity
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdCity", fIdCity, value)
        End Set
    End Property
    Dim fIdManufacturer As Integer
    Public Property IdManufacturer() As Integer
        Get
            Return fIdManufacturer
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdManufacturer", fIdManufacturer, value)
        End Set
    End Property
    Dim fPermanentRetention As Boolean
    Public Property PermanentRetention() As Boolean
        Get
            Return fPermanentRetention
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("PermanentRetention", fPermanentRetention, value)
        End Set
    End Property
    Dim fTimeLimitDays As Integer
    Public Property TimeLimitDays() As Integer
        Get
            Return fTimeLimitDays
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("TimeLimitDays", fTimeLimitDays, value)
        End Set
    End Property
    Dim fNotIva As Boolean
    Public Property NotIva() As Boolean
        Get
            Return fNotIva
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("NotIva", fNotIva, value)
        End Set
    End Property
    Dim fStatus As Boolean
    '<Indexed(Name:="IX_Manufacturer_State")> _
    Public Property Status() As Boolean
        Get
            Return fStatus
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Status", fStatus, value)
        End Set
    End Property
    Dim fCreationUser As String
    <Size(20)> _
    Public Property CreationUser() As String
        Get
            Return fCreationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CreationUser", fCreationUser, value)
        End Set
    End Property
    Dim fCreationDate As DateTime
    Public Property CreationDate() As DateTime
        Get
            Return fCreationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("CreationDate", fCreationDate, value)
        End Set
    End Property
    Dim fModificationUser As String
    <Size(20)> _
    Public Property ModificationUser() As String
        Get
            Return fModificationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ModificationUser", fModificationUser, value)
        End Set
    End Property
    Dim fModificationDate As DateTime
    Public Property ModificationDate() As DateTime
        Get
            Return fModificationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ModificationDate", fModificationDate, value)
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

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
