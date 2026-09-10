Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Payroll.Foreclousure")>
Partial Public Class Payroll_Foreclousure
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
        Dim fCode As String
        <Size(20)>
        Public Property Code() As String
            Get
                Return fCode
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("Code", fCode, value)
            End Set
        End Property
        Dim fIdEmployee As Integer
        Public Property IdEmployee() As Integer
            Get
                Return fIdEmployee
            End Get
            Set(ByVal value As Integer)
                SetPropertyValue(Of Integer)("IdEmployee", fIdEmployee, value)
            End Set
        End Property
        Dim fInitialContract As Integer
        Public Property InitialContract() As Integer
            Get
                Return fInitialContract
            End Get
            Set(ByVal value As Integer)
                SetPropertyValue(Of Integer)("InitialContract", fInitialContract, value)
            End Set
        End Property
        Dim fIdContract As Integer
        Public Property IdContract() As Integer
            Get
                Return fIdContract
            End Get
            Set(ByVal value As Integer)
                SetPropertyValue(Of Integer)("IdContract", fIdContract, value)
            End Set
        End Property
        Dim fForeclousureType As Byte
        Public Property ForeclousureType() As Byte
            Get
                Return fForeclousureType
            End Get
            Set(ByVal value As Byte)
                SetPropertyValue(Of Byte)("ForeclousureType", fForeclousureType, value)
            End Set
        End Property
        Dim fComment As String
        <Size(500)>
        Public Property Comment() As String
            Get
                Return fComment
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("Comment", fComment, value)
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
        Dim fIdApplicant As Common_ThirdParty
        <Association("Payroll_ForeclousureReferencesCommon_ThirdParty")>
        Public Property IdApplicant() As Common_ThirdParty
            Get
                Return fIdApplicant
            End Get
            Set(ByVal value As Common_ThirdParty)
                SetPropertyValue(Of Common_ThirdParty)("IdApplicant", fIdApplicant, value)
            End Set
        End Property
        Dim fDiscountType As Byte
        Public Property DiscountType() As Byte
            Get
                Return fDiscountType
            End Get
            Set(ByVal value As Byte)
                SetPropertyValue(Of Byte)("DiscountType", fDiscountType, value)
            End Set
        End Property
        Dim fDiscountClass As Byte
        Public Property DiscountClass() As Byte
            Get
                Return fDiscountClass
            End Get
            Set(ByVal value As Byte)
                SetPropertyValue(Of Byte)("DiscountClass", fDiscountClass, value)
            End Set
        End Property
        Dim fQuoteValue As Decimal
        Public Property QuoteValue() As Decimal
            Get
                Return fQuoteValue
            End Get
            Set(ByVal value As Decimal)
                SetPropertyValue(Of Decimal)("QuoteValue", fQuoteValue, value)
            End Set
        End Property
        Dim fPercentage As Decimal
        Public Property Percentage() As Decimal
            Get
                Return fPercentage
            End Get
            Set(ByVal value As Decimal)
                SetPropertyValue(Of Decimal)("Percentage", fPercentage, value)
            End Set
        End Property
        Dim fTotalValue As Decimal
        Public Property TotalValue() As Decimal
            Get
                Return fTotalValue
            End Get
            Set(ByVal value As Decimal)
                SetPropertyValue(Of Decimal)("TotalValue", fTotalValue, value)
            End Set
        End Property
        Dim fIdConcept As Integer
        Public Property IdConcept() As Integer
            Get
                Return fIdConcept
            End Get
            Set(ByVal value As Integer)
                SetPropertyValue(Of Integer)("IdConcept", fIdConcept, value)
            End Set
        End Property
        Dim fInitialDate As DateTime
        Public Property InitialDate() As DateTime
            Get
                Return fInitialDate
            End Get
            Set(ByVal value As DateTime)
                SetPropertyValue(Of DateTime)("InitialDate", fInitialDate, value)
            End Set
        End Property
        Dim fAffectVacation As Boolean
        Public Property AffectVacation() As Boolean
            Get
                Return fAffectVacation
            End Get
            Set(ByVal value As Boolean)
                SetPropertyValue(Of Boolean)("AffectVacation", fAffectVacation, value)
            End Set
        End Property
        Dim fAffect1erIncentivePayment As Boolean
        Public Property Affect1erIncentivePayment() As Boolean
            Get
                Return fAffect1erIncentivePayment
            End Get
            Set(ByVal value As Boolean)
                SetPropertyValue(Of Boolean)("Affect1erIncentivePayment", fAffect1erIncentivePayment, value)
            End Set
        End Property
        Dim fAffect2doIncentivePayment As Boolean
        Public Property Affect2doIncentivePayment() As Boolean
            Get
                Return fAffect2doIncentivePayment
            End Get
            Set(ByVal value As Boolean)
                SetPropertyValue(Of Boolean)("Affect2doIncentivePayment", fAffect2doIncentivePayment, value)
            End Set
        End Property
        Dim fAffectUnemploymentValue As Boolean
        Public Property AffectUnemploymentValue() As Boolean
            Get
                Return fAffectUnemploymentValue
            End Get
            Set(ByVal value As Boolean)
                SetPropertyValue(Of Boolean)("AffectUnemploymentValue", fAffectUnemploymentValue, value)
            End Set
        End Property
        Dim fIdJudgment As Integer
        Public Property IdJudgment() As Integer
            Get
                Return fIdJudgment
            End Get
            Set(ByVal value As Integer)
                SetPropertyValue(Of Integer)("IdJudgment", fIdJudgment, value)
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
        Dim fTradeNumber As String
        Public Property TradeNumber() As String
            Get
                Return fTradeNumber
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("TradeNumber", fTradeNumber, value)
            End Set
        End Property
        Dim fTradeDate As DateTime
        Public Property TradeDate() As DateTime
            Get
                Return fTradeDate
            End Get
            Set(ByVal value As DateTime)
                SetPropertyValue(Of DateTime)("TradeDate", fTradeDate, value)
            End Set
        End Property
        Dim fIdBeneficiary As Common_ThirdParty
        <Association("Payroll_ForeclousureReferencesCommon_ThirdParty1")>
        Public Property IdBeneficiary() As Common_ThirdParty
            Get
                Return fIdBeneficiary
            End Get
            Set(ByVal value As Common_ThirdParty)
                SetPropertyValue(Of Common_ThirdParty)("IdBeneficiary", fIdBeneficiary, value)
            End Set
        End Property
        Dim fProcessNumber As String
        Public Property ProcessNumber() As String
            Get
                Return fProcessNumber
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("ProcessNumber", fProcessNumber, value)
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
        Dim fCreationUser As String
        <Size(20)>
        Public Property CreationUser() As String
            Get
                Return fCreationUser
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("CreationUser", fCreationUser, value)
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
        Dim fConfirmationUser As String
        <Size(20)>
        Public Property ConfirmationUser() As String
            Get
                Return fConfirmationUser
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("ConfirmationUser", fConfirmationUser, value)
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
        Dim fAnnulmentUser As String
        <Size(20)>
        Public Property AnnulmentUser() As String
            Get
                Return fAnnulmentUser
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("AnnulmentUser", fAnnulmentUser, value)
            End Set
        End Property
        Dim fSuspendDate As DateTime
        Public Property SuspendDate() As DateTime
            Get
                Return fSuspendDate
            End Get
            Set(ByVal value As DateTime)
                SetPropertyValue(Of DateTime)("SuspendDate", fSuspendDate, value)
            End Set
        End Property
        Dim fSuspendUser As String
        <Size(20)>
        Public Property SuspendUser() As String
            Get
                Return fSuspendUser
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("SuspendUser", fSuspendUser, value)
            End Set
        End Property
        Dim fAnnulmentComment As String
        Public Property AnnulmentComment() As String
            Get
                Return fAnnulmentComment
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("AnnulmentComment", fAnnulmentComment, value)
            End Set
        End Property
        Dim fSupendComment As String
        Public Property SupendComment() As String
            Get
                Return fSupendComment
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("SupendComment", fSupendComment, value)
            End Set
        End Property
        Dim fCodeDestinationOffice As String
        <Size(5)>
        Public Property CodeDestinationOffice() As String
            Get
                Return fCodeDestinationOffice
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("CodeDestinationOffice", fCodeDestinationOffice, value)
            End Set
        End Property
        Dim fJudicialAccount As String
        <Size(20)>
        Public Property JudicialAccount() As String
            Get
                Return fJudicialAccount
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("JudicialAccount", fJudicialAccount, value)
            End Set
        End Property
End Class