Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports Infrastructure.CrossCutting.Resources

<Persistent("Treasury.CrossingAccount")> _
Public Class TreasuryCrossingAccountXpo
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
    Dim fDescription As String
    <Size(300)> _
    Public Property Description() As String
        Get
            Return fDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Description", fDescription, value)
        End Set
    End Property
    Dim fThirdPartyId As CommonThirdPartyReportXpo
    <Association("TreasuryCrossingAccountXpoReferencesCommonThirdPartyReportXpo")> _
    Public Property ThirdPartyId() As CommonThirdPartyReportXpo
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As CommonThirdPartyReportXpo)
            SetPropertyValue(Of CommonThirdPartyReportXpo)("ThirdPartyId", fThirdPartyId, value)
        End Set
    End Property
    Dim fDocumentDate As DateTime
    Public Property DocumentDate() As DateTime
        Get
            Return fDocumentDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("DocumentDate", fDocumentDate, value)
        End Set
    End Property
    Dim fOperatingUnitId As Integer
    Public Property OperatingUnitId() As Integer
        Get
            Return fOperatingUnitId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("OperatingUnitId", fOperatingUnitId, value)
        End Set
    End Property
    Dim fStatus As String
    <Persistent("Status")> _
    Public Property Status() As String
        Get
            Select Case fStatus
                Case 1
                    fStatus = ResourceManager.GetString("StateUnconfirmed")
                Case 2
                    fStatus = ResourceManager.GetString("StateConfirmed")
                Case 3
                    fStatus = ResourceManager.GetString("StatusCanceled")
            End Select
            Return fStatus
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Status", fStatus, value)
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
    Dim fConfirmationUser As String
    <Size(20)> _
    Public Property ConfirmationUser() As String
        Get
            Return fConfirmationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ConfirmationUser", fConfirmationUser, value)
        End Set
    End Property
    Dim fConfirmationDate As DateTime
    Public Property ConfirmationDate() As DateTime
        Get
            Return fConfirmationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ConfirmationDate", fConfirmationDate, value)
        End Set
    End Property
    Dim fAnnulmentUser As String
    <Size(20)> _
    Public Property AnnulmentUser() As String
        Get
            Return fAnnulmentUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AnnulmentUser", fAnnulmentUser, value)
        End Set
    End Property
    Dim fAnnulmentDate As DateTime
    Public Property AnnulmentDate() As DateTime
        Get
            Return fAnnulmentDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("AnnulmentDate", fAnnulmentDate, value)
        End Set
    End Property

    Dim fCommonCurrency As CommonCurrencyXpo
    <Persistent("CurrencyId")>
    <Association("CurrencyReferenceCrossingAccountXpo")>
    Public Property CommonCurrency() As CommonCurrencyXpo
        Get
            Return fCommonCurrency
        End Get
        Set(ByVal value As CommonCurrencyXpo)
            SetPropertyValue("CommonCurrency", fCommonCurrency, value)
        End Set
    End Property

    <PersistentAlias("CommonCurrency.Abbreviation")>
    Public ReadOnly Property CurrencyAbbreviation() As String
        Get
            Return Convert.ToString(EvaluateAlias("CurrencyAbbreviation"))
        End Get
    End Property

    <PersistentAlias("CommonCurrency.Id")>
    Public ReadOnly Property CurrencyId() As Integer?
        Get
            Return Convert.ToInt32(EvaluateAlias("CurrencyId"))
        End Get
    End Property

    <PersistentAlias("CommonCurrency.ISO4217Xpo.CurrencyName")>
    Public ReadOnly Property CurrencyNameISO() As String
        Get
            Return Convert.ToString(EvaluateAlias("CurrencyNameISO"))
        End Get
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

    <Association("TreasuryCrossingAccountDetailCxCXpoReferencesTreasuryCrossingAccountXpo", GetType(TreasuryCrossingAccountDetailCxCXpo))> _
    Public ReadOnly Property TreasuryCrossingAccountDetailCxCXpo() As XPCollection(Of TreasuryCrossingAccountDetailCxCXpo)
        Get
            Return GetCollection(Of TreasuryCrossingAccountDetailCxCXpo)("TreasuryCrossingAccountDetailCxCXpo")
        End Get
    End Property
    <Association("TreasuryCrossingAccountDetailCxPXpoReferencesTreasuryCrossingAccountXpo", GetType(TreasuryCrossingAccountDetailCxPXpo))> _
    Public ReadOnly Property TreasuryCrossingAccountDetailCxPXpo() As XPCollection(Of TreasuryCrossingAccountDetailCxPXpo)
        Get
            Return GetCollection(Of TreasuryCrossingAccountDetailCxPXpo)("TreasuryCrossingAccountDetailCxPXpo")
        End Get
    End Property
    <Association("TreasuryCrossingAccountDetailOtherConceptXpoReferencesTreasuryCrossingAccountXpo", GetType(TreasuryCrossingAccountDetailOtherConceptXpo))> _
    Public ReadOnly Property TreasuryCrossingAccountDetailOtherConceptXpo() As XPCollection(Of TreasuryCrossingAccountDetailOtherConceptXpo)
        Get
            Return GetCollection(Of TreasuryCrossingAccountDetailOtherConceptXpo)("TreasuryCrossingAccountDetailOtherConceptXpo")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
