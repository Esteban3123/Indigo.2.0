Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Base

<Persistent("Treasury.TreasuryRevaluationDetail")>
Public Class TreasuryRevaluationDetailXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fId As Integer
    <Key(True)>
    <Persistent("Id")>
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property

    Dim fDocumentType As Integer
    Public Property DocumentType() As Integer
        Get
            Return fDocumentType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("DocumentType", fDocumentType, value)
        End Set
    End Property

    Dim fNature As Byte
    Public Property Nature() As Byte
        Get
            Return fNature
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Nature", fNature, value)
        End Set
    End Property

    Dim fDocumentNumber As String
    Public Property DocumentNumber() As String
        Get
            Return fDocumentNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DocumentNumber", fDocumentNumber, value)
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

    Dim fValueMovement As Decimal
    Public Property ValueMovement() As Decimal
        Get
            Return fValueMovement
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueMovement", fValueMovement, value)
        End Set
    End Property

    <PersistentAlias("CommonCurrency.Id")>
    Public ReadOnly Property CurrencyId() As Integer
        Get
            Return Convert.ToInt32(EvaluateAlias("CurrencyId"))
        End Get
    End Property

    <PersistentAlias("CommonCurrency.Abbreviation")>
    Public ReadOnly Property CurrencyAbbreviation() As String
        Get
            Return If(String.IsNullOrEmpty(Convert.ToString(EvaluateAlias("CurrencyAbbreviation"))),
                        SessionValues.Instance.CurrencyISO4217, Convert.ToString(EvaluateAlias("CurrencyAbbreviation")))
        End Get
    End Property

    Dim fCommonCurrency As CommonCurrencyXpo
    <Persistent("CurrencyId")>
    <Association("CurrencyReferenceTreasuryRevaluationDetailXpo1")>
    Public Property CommonCurrency() As CommonCurrencyXpo
        Get
            Return fCommonCurrency
        End Get
        Set(ByVal value As CommonCurrencyXpo)
            SetPropertyValue("CommonCurrency", fCommonCurrency, value)
        End Set
    End Property

    <PersistentAlias("CommonCurrencyConverted.Id")>
    Public ReadOnly Property CurrencyConvertedId() As Integer
        Get
            Return Convert.ToInt32(EvaluateAlias("CurrencyConvertedId"))
        End Get
    End Property

    <PersistentAlias("CommonCurrencyConverted.Abbreviation")>
    Public ReadOnly Property CurrencyAbbreviationConverted() As String
        Get
            Return If(String.IsNullOrEmpty(Convert.ToString(EvaluateAlias("CurrencyAbbreviation"))),
                        SessionValues.Instance.CurrencyISO4217, Convert.ToString(EvaluateAlias("CurrencyAbbreviation")))
        End Get
    End Property

    Dim fCommonCurrencyConverted As CommonCurrencyXpo
    <Persistent("CurrencyConvertedId")>
    <Association("CurrencyReferenceTreasuryRevaluationDetailXpo2")>
    Public Property CommonCurrencyConverted() As CommonCurrencyXpo
        Get
            Return fCommonCurrencyConverted
        End Get
        Set(ByVal value As CommonCurrencyXpo)
            SetPropertyValue("CommonCurrencyConverted", fCommonCurrencyConverted, value)
        End Set
    End Property

    Dim fValueCurrency As Decimal
    Public Property ValueCurrency() As Decimal
        Get
            Return fValueCurrency
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueCurrency", fValueCurrency, value)
        End Set
    End Property

    Dim fValueCurrencyReverse As Decimal
    Public Property ValueCurrencyReverse() As Decimal
        Get
            Return fValueCurrencyReverse
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueCurrencyReverse", fValueCurrencyReverse, value)
        End Set
    End Property

    Dim fActualValueCurrency As Decimal
    Public Property ActualValueCurrency() As Decimal
        Get
            Return fActualValueCurrency
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ActualValueCurrency", fActualValueCurrency, value)
        End Set
    End Property

    Dim fActualValueCurrencyReverse As Decimal
    Public Property ActualValueCurrencyReverse() As Decimal
        Get
            Return fActualValueCurrencyReverse
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ActualValueCurrencyReverse", fActualValueCurrencyReverse, value)
        End Set
    End Property

    Dim fValueMovementConverted As Decimal
    Public Property ValueMovementConverted() As Decimal
        Get
            Return fValueMovementConverted
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueMovementConverted", fValueMovementConverted, value)
        End Set
    End Property

    Dim fActualValueMovementConverted As Decimal
    Public Property ActualValueMovementConverted() As Decimal
        Get
            Return fActualValueMovementConverted
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ActualValueMovementConverted", fActualValueMovementConverted, value)
        End Set
    End Property

    Dim fProfitLostValue As Decimal
    Public Property ProfitLostValue() As Decimal
        Get
            Return fProfitLostValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ProfitLostValue", fProfitLostValue, value)
        End Set
    End Property

    <PersistentAlias("TreasuryBalance.Id")>
    Public ReadOnly Property TreasuryBalanceId() As Integer?
        Get
            Return Convert.ToInt32(EvaluateAlias("TreasuryBalanceId"))
        End Get
    End Property

    Dim fTreasuryBalance As TreasuryTreasuryBalance
    <Persistent("TreasuryBalanceId")>
    <Association("TreasuryTreasuryBalance_References_TreasuryRevaluationDetailXpo")>
    Public Property TreasuryBalance() As TreasuryTreasuryBalance
        Get
            Return fTreasuryBalance
        End Get
        Set(ByVal value As TreasuryTreasuryBalance)
            SetPropertyValue("TreasuryBalance", fTreasuryBalance, value)
        End Set
    End Property

    <PersistentAlias("TreasuryRevaluation.Id")>
    Public ReadOnly Property TreasuryRevaluationId() As Integer
        Get
            Return Convert.ToInt32(EvaluateAlias("TreasuryRevaluationId"))
        End Get
    End Property

    Dim fTreasuryRevaluation As TreasuryRevaluationXpo
    <Persistent("TreasuryRevaluationId")>
    <Association("TreasuryRevaluationXpo_References_TreasuryRevaluationDetailXpo")>
    Public Property TreasuryRevaluation() As TreasuryRevaluationXpo
        Get
            Return fTreasuryRevaluation
        End Get
        Set(ByVal value As TreasuryRevaluationXpo)
            SetPropertyValue("TreasuryRevaluation", fTreasuryRevaluation, value)
        End Set
    End Property
#End Region

#Region "PersistentAlias"
    <PersistentAlias("iif(  DocumentType=0,'Saldo Caja o Banco',
                            DocumentType=1,'Recibo de Caja',
                            DocumentType=2,'Comrpobante de Egreso',
                            DocumentType=3,'Consignaciones',
                            DocumentType=4,'Notas',
                            DocumentType=5,'Fondo de Caja Menor','')")>
    Public ReadOnly Property DocumentTypeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("DocumentTypeName"))
        End Get
    End Property

    <PersistentAlias("iif(Nature=1,'Debito','Credito')")>
    Public ReadOnly Property NatureName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("NatureName"))
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
