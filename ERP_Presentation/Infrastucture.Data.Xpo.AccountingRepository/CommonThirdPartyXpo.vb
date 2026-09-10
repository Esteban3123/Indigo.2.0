Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Common.ThirdParty")> _
Public Class CommonThirdPartyXpo
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
    Dim fPersonId As CommonPersonXpo
    <Association("CommonThirdPartyXpoReferencesCommonPersonXpo")> _
    Public Property PersonId() As CommonPersonXpo
        Get
            Return fPersonId
        End Get
        Set(ByVal value As CommonPersonXpo)
            SetPropertyValue(Of CommonPersonXpo)("PersonId", fPersonId, value)
        End Set
    End Property
    Dim fNit As String
    <Size(15)> _
    Public Property Nit() As String
        Get
            Return fNit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Nit", fNit, value)
        End Set
    End Property
    Dim fDigitVerification As String
    <Size(1)> _
    Public Property DigitVerification() As String
        Get
            Return fDigitVerification
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DigitVerification", fDigitVerification, value)
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
    Dim fPersonType As Byte
    Public Property PersonType() As Byte
        Get
            Return fPersonType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("PersonType", fPersonType, value)
        End Set
    End Property
    <PersistentAlias("Iif(State = 0, 'Inactivo', Iif(State = 1, 'Activo', ''))")>
    Public ReadOnly Property StateName() As String
        Get
            'Select Case fState
            '    Case 0
            '        Return "Inactivo"
            '    Case 1
            '        Return "Activo"
            '    Case Else
            '        Return String.Empty
            'End Select
            Return Convert.ToString(Me.EvaluateAlias("StateName"))
        End Get
    End Property
    <PersistentAlias("Iif(PersonType = 1, 'Natural', Iif(PersonType = 2, 'Jurídico', ''))")>
    Public ReadOnly Property PersonTypeName() As String
        Get
            'Select Case fPersonType
            '    Case 1
            '        Return "Natural"
            '    Case 2
            '        Return "Jurídico"
            '    Case Else
            '        Return String.Empty
            'End Select
            Return Convert.ToString(Me.EvaluateAlias("PersonTypeName"))
        End Get
    End Property
    <PersistentAlias("Iif(ContributionType = 0, 'No responsable de Iva',
                    ContributionType = 1, ' Responsables de Iva', 
                    ContributionType = 2, 'Empresa estatal',
                    ContributionType = 3, 'Gran Contribuyente',
                    ContributionType = 4, 'Régimen simple', '')")>
    Public ReadOnly Property ContributionTypeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("ContributionTypeName"))
        End Get
    End Property
    <PersistentAlias("Iif(RetentionType = 0, 'Ninguna',Iif(RetentionType = 1, 'Exento de Retencion', Iif(RetentionType = 2, 'Hace Retencion', Iif(RetentionType = 3, 'Autoretenedor',''))))")>
    Public ReadOnly Property RetentionTypeName() As String
        Get
            'Select Case fRetentionType
            '    Case 0
            '        Return "Ninguna"
            '    Case 1
            '        Return "Exento de Retencion"
            '    Case 2
            '        Return "Hace Retencion"
            '    Case 3
            '        Return "Autoretenedor"
            '    Case Else
            '        Return String.Empty
            'End Select
            Return Convert.ToString(Me.EvaluateAlias("RetentionTypeName"))
        End Get
    End Property
    Dim fRetentionType As Byte
    Public Property RetentionType() As Byte
        Get
            Return fRetentionType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("RetentionType", fRetentionType, value)
        End Set
    End Property
    Dim fContributionType As Byte
    Public Property ContributionType() As Byte
        Get
            Return fContributionType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("ContributionType", fContributionType, value)
        End Set
    End Property
    Dim fIca As Boolean
    Public Property Ica() As Boolean
        Get
            Return fIca
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Ica", fIca, value)
        End Set
    End Property
    Dim fIcaPercentage As Decimal
    Public Property IcaPercentage() As Decimal
        Get
            Return fIcaPercentage
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("IcaPercentage", fIcaPercentage, value)
        End Set
    End Property
    Dim fIcaTop As Boolean
    Public Property IcaTop() As Boolean
        Get
            Return fIcaTop
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("IcaTop", fIcaTop, value)
        End Set
    End Property
    Dim fIcaTopValue As Decimal
    Public Property IcaTopValue() As Decimal
        Get
            Return fIcaTopValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("IcaTopValue", fIcaTopValue, value)
        End Set
    End Property
    Dim fState As Byte
    Public Property State() As Byte
        Get
            Return fState
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("State", fState, value)
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
    Dim fUserId As Integer
    Public Property UserId() As Integer
        Get
            Return fUserId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("UserId", fUserId, value)
        End Set
    End Property

    'columna que devuelve el codigo y el nombre concatenado
    <Size(120)> _
    <PersistentAlias("concat(concat(Nit,' - '),Name)")>
    Public Property NitName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("NitName"))
        End Get
        Set(value As String)
            If value IsNot Nothing Then
                Dim thirdSplit = value.Split("-")
                fNit = thirdSplit(0).Trim()
                fName = thirdSplit(1).Trim()
            End If
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
    <Association("GeneralLedger_JournalVoucherDetailsReferencesCommon_ThirdParty", GetType(JournalVoucherDetailsXpo))> _
    Public ReadOnly Property GeneralLedger_JournalVoucherDetailsCollection() As XPCollection(Of JournalVoucherDetailsXpo)
        Get
            Return GetCollection(Of JournalVoucherDetailsXpo)("GeneralLedger_JournalVoucherDetailsCollection")
        End Get
    End Property
    <Association("GeneralLedger_MainAccountsReferencesCommon_ThirdParty", GetType(MainAccountsXpo))> _
    Public ReadOnly Property GeneralLedger_MainAccountsCollection() As XPCollection(Of MainAccountsXpo)
        Get
            Return GetCollection(Of MainAccountsXpo)("GeneralLedger_MainAccountsCollection")
        End Get
    End Property
    <Association("GeneralLedger_GeneralLedgerBalanceReferencesCommon_ThirdParty", GetType(GeneralLedgerBalanceXpo))>
    Public ReadOnly Property GeneralLedger_GeneralLedgerBalanceCollection() As XPCollection(Of GeneralLedgerBalanceXpo)
        Get
            Return GetCollection(Of GeneralLedgerBalanceXpo)("GeneralLedger_GeneralLedgerBalanceCollection")
        End Get
    End Property

    <Association("GeneralLedger_MainAccountRestrictionsReferencesCommon_ThirdParty", GetType(MainAccountRestrictionsXpo))>
    Public ReadOnly Property GeneralLedger_MainAccountRestrictionsCollection() As XPCollection(Of MainAccountRestrictionsXpo)
        Get
            Return GetCollection(Of MainAccountRestrictionsXpo)("GeneralLedger_MainAccountRestrictionsCollection")
        End Get
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
