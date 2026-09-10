Imports DevExpress.Xpo

Public Class CustomTRMXpo
    Inherits XPLiteObject

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

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

    Dim fInitialMeasurementDate As DateTime
    Public Property InitialMeasurementDate() As DateTime
        Get
            Return fInitialMeasurementDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("InitialMeasurementDate", fInitialMeasurementDate, value)
        End Set
    End Property

    Dim fFinalMeasurementDate As DateTime
    Public Property FinalMeasurementDate() As DateTime
        Get
            Return fFinalMeasurementDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("FinalMeasurementDate", fFinalMeasurementDate, value)
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

    Dim fOfficialCurrencyId As Integer
    Public Property OfficialCurrencyId() As Integer
        Get
            Return fOfficialCurrencyId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("OfficialCurrencyId", fOfficialCurrencyId, value)
        End Set
    End Property

    Dim fCurrency As CommonCurrencyXpo
    <Persistent("CurrencyId")>
    <Association("CustomTRM_References_Common_Currency")>
    Public Property Currency() As CommonCurrencyXpo
        Get
            Return fCurrency
        End Get
        Set(ByVal value As CommonCurrencyXpo)
            SetPropertyValue("Currency", fCurrency, value)
        End Set
    End Property

    <PersistentAlias("Currency.Id")>
    Public ReadOnly Property CurrencyId() As Integer
        Get
            Return Convert.ToInt32(EvaluateAlias("CurrencyId"))
        End Get
    End Property

    <PersistentAlias("Currency.Abbreviation")>
    Public ReadOnly Property CurrencyAbbreviation() As Integer
        Get
            Return Convert.ToInt32(EvaluateAlias("CurrencyAbbreviation"))
        End Get
    End Property

    Dim fValue As Decimal
    Public Property Value() As Decimal
        Get
            Return fValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Value", fValue, value)
        End Set
    End Property
End Class
