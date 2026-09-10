#Region "Imports"

Imports DevExpress.Xpo

#End Region

<Persistent("Treasury.BankReconciliationAutomatic")>
Public Class BankReconciliationAutomaticXpo
    Inherits XPLiteObject

#Region "Members"

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
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property

    Dim fEntityBankAccountId As EntityBankAccountXpo
    <Association("TreasuryBankReconciliationAutomatic_Reference_TreasuryEntityBankAccounts")>
    Public Property EntityBankAccountId() As EntityBankAccountXpo
        Get
            Return fEntityBankAccountId
        End Get
        Set(ByVal value As EntityBankAccountXpo)
            SetPropertyValue("EntityBankAccountId", fEntityBankAccountId, value)
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

    Dim fEntityBankAccountValue As Decimal
    Public Property EntityBankAccountValue() As Decimal
        Get
            Return fEntityBankAccountValue
        End Get
        Set(value As Decimal)
            SetPropertyValue(Of Decimal)("EntityBankAccountValue", fEntityBankAccountValue, value)
        End Set
    End Property

    Dim fExtractValue As Decimal
    Public Property ExtractValue As Decimal
        Get
            Return fExtractValue
        End Get
        Set(value As Decimal)
            SetPropertyValue(Of Decimal)("ExtractValue", fExtractValue, value)
        End Set
    End Property

    Dim fStatus As Byte
    Public Property Status() As Byte
        Get
            Return fStatus
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Status", fStatus, value)
        End Set
    End Property

#End Region

#Region "Extends"
    ''' <summary>
    ''' Valor de la nota de gastos
    ''' </summary>
    <NonPersistent()>
    Public Property ExpenseNoteValue As Decimal
    ''' <summary>
    ''' Sumatoria de Débitos o Créditos pendientes por conciliar de libro de bancos
    ''' </summary>
    <NonPersistent()>
    Public Property PendingBankRecordTransactions As Decimal
    ''' <summary>
    ''' Sumatoria de Débitos 
    ''' </summary>
    <NonPersistent()>
    Public Property ConsignmentsNotRecordedInExtract As Decimal
    ''' <summary>
    ''' Sumatoria de debitos o creditos de las partidas pendientes por conciliar de Extracto Bancario
    ''' Valor que suma
    ''' </summary>
    <NonPersistent()>
    Public Property PositiveDocumentsPending As Decimal
    ''' <summary>
    ''' Sumatoria de debitos o creditos de las partidas pendientes por conciliar de Extracto Bancario
    ''' Valor que resta
    ''' </summary>
    <NonPersistent()>
    Public Property NegativeDocumentsPending As Decimal

#End Region

#Region "Navigation Fields"
    <Association("TreasuryBankReconciliationAutomaticExtractDetail_Reference_TreasuryBankReconciliationAutomatic", GetType(BankReconciliationAutomaticExtractDetailXpo))>
    Public ReadOnly Property BankReconciliationAutomaticExtractDetailXpo() As XPCollection(Of BankReconciliationAutomaticExtractDetailXpo)
        Get
            Return GetCollection(Of BankReconciliationAutomaticExtractDetailXpo)("BankReconciliationAutomaticExtractDetailXpo")
        End Get
    End Property

    <Association("TreasuryBankReconciliationAutomaticDetail_Reference_TreasuryBankReconciliationAutomatic", GetType(BankReconciliationAutomaticDetailXpo))>
    Public ReadOnly Property BankReconciliationAutomaticDetailXpo() As XPCollection(Of BankReconciliationAutomaticDetailXpo)
        Get
            Return GetCollection(Of BankReconciliationAutomaticDetailXpo)("BankReconciliationAutomaticDetailXpo")
        End Get
    End Property
#End Region

#Region "Custom Fields"

    <PersistentAlias("Iif(Status = 1, 'Registrado', Status = 2, 'Confirmado', Status = 3, 'Anulado', '')")>
    Public ReadOnly Property StatusName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
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
