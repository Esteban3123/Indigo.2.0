#Region "Imports"

Imports DevExpress.Xpo

#End Region

<Persistent("Portfolio.PortfolioConciliationDetail")>
Public Class PortfolioConciliationDetailXpo
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

    Dim fPortfolioConciliationId As PortfolioConciliationXpo
    <Association("Portfolio_PortfolioConciliationXpo_ConciliationId")>
    Public Property PortfolioConciliationId() As PortfolioConciliationXpo
        Get
            Return fPortfolioConciliationId
        End Get
        Set(ByVal value As PortfolioConciliationXpo)
            SetPropertyValue(Of PortfolioConciliationXpo)("PortfolioConciliationId", fPortfolioConciliationId, value)
        End Set
    End Property

    Dim fInvoiceId As PortfolioInvoiceXpo
    <Association("Billing_InvoiceId_Reference_PortfolioConciliationDetail")>
    Public Property InvoiceId() As PortfolioInvoiceXpo
        Get
            Return fInvoiceId
        End Get
        Set(ByVal value As PortfolioInvoiceXpo)
            SetPropertyValue(Of PortfolioInvoiceXpo)("InvoiceId", fInvoiceId, value)
        End Set
    End Property

    Dim fAccountReceivableDate As DateTime
    Public Property AccountReceivableDate() As DateTime
        Get
            Return fAccountReceivableDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("AccountReceivableDate", fAccountReceivableDate, value)
        End Set
    End Property

    Dim fRadicatedNumber As Integer
    Public Property RadicatedNumber() As Integer
        Get
            Return fRadicatedNumber
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RadicatedNumber", fRadicatedNumber, value)
        End Set
    End Property

    Dim fRadicatedDate As DateTime
    Public Property RadicatedDate() As DateTime
        Get
            Return fRadicatedDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("RadicatedDate", fRadicatedDate, value)
        End Set
    End Property

    Dim fBalance As Decimal
    Public Property Balance() As Decimal
        Get
            Return fBalance
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Balance", fBalance, value)
        End Set
    End Property

    Dim fValueGlosado As Decimal
    Public Property ValueGlosado() As Decimal
        Get
            Return fValueGlosado
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueGlosado", fValueGlosado, value)
        End Set
    End Property

    Dim fValueEntity As Decimal
    Public Property ValueEntity() As Decimal
        Get
            Return fValueEntity
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueEntity", fValueEntity, value)
        End Set
    End Property

    Dim fPortfolioStatus As Byte
    Public Property PortfolioStatus() As Byte
        Get
            Return fPortfolioStatus
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("PortfolioStatus", fPortfolioStatus, value)
        End Set
    End Property

    Dim fValueAcceptedFirstInstance As Decimal
    Public Property ValueAcceptedFirstInstance() As Decimal
        Get
            Return fValueAcceptedFirstInstance
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueAcceptedFirstInstance", fValueAcceptedFirstInstance, value)
        End Set
    End Property

    Dim fValueAcceptedSecondInstance As Decimal
    Public Property ValueAcceptedSecondInstance() As Decimal
        Get
            Return fValueAcceptedSecondInstance
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueAcceptedSecondInstance", fValueAcceptedSecondInstance, value)
        End Set
    End Property

    Dim fValueGlosadoConciliation As Decimal
    Public Property ValueGlosadoConciliation() As Decimal
        Get
            Return fValueGlosadoConciliation
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueGlosadoConciliation", fValueGlosadoConciliation, value)
        End Set
    End Property

    Dim fBalanceConciliation As Decimal
    Public Property BalanceConciliation() As Decimal
        Get
            Return fBalanceConciliation
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("BalanceConciliation", fBalanceConciliation, value)
        End Set
    End Property

    Dim fStateConciliation As Boolean
    Public Property StateConciliation() As Boolean
        Get
            Return fStateConciliation
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("StateConciliation", fStateConciliation, value)
        End Set
    End Property

    Dim fStatePortfolioConciliation As Byte
    Public Property StatePortfolioConciliation() As Byte
        Get
            Return fStatePortfolioConciliation
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("StatePortfolioConciliation", fStatePortfolioConciliation, value)
        End Set
    End Property

    Dim fComment As String
    Public Property Comment() As String
        Get
            Return fComment
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Comment", fComment, value)
        End Set
    End Property

    Dim fPortfolioConciliationConceptsId As PortfolioConciliationConceptsXpo
    <Association("Portfolio_PortfolioConciliationXpo_ConciliationConcepts")>
    Public Property PortfolioConciliationConceptsId() As PortfolioConciliationConceptsXpo
        Get
            Return fPortfolioConciliationConceptsId
        End Get
        Set(ByVal value As PortfolioConciliationConceptsXpo)
            SetPropertyValue(Of PortfolioConciliationConceptsXpo)("PortfolioConciliationConceptsId", fPortfolioConciliationConceptsId, value)
        End Set
    End Property

    Dim fPortfolioDifferenceConciliation As Decimal
    Public Property PortfolioDifferenceConciliation() As Decimal
        Get
            Return fPortfolioDifferenceConciliation
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PortfolioDifferenceConciliation", fPortfolioDifferenceConciliation, value)
        End Set
    End Property

#End Region

#Region "Custom Members"

    <PersistentAlias("Iif(PortfolioStatus = 1, 'Sin Radicar',PortfolioStatus = 2, 'Radicada sin Confirmar',PortfolioStatus = 3,'Radicada Entidad',PortfolioStatus = 7,'Certificada Parcial',PortfolioStatus = 8,'Certificada Total',PortfolioStatus = 14,'Devolución Factura',PortfolioStatus = 15,'Cuenta de Dificil Recaudo','Cobro Jurídico')")>
    Public ReadOnly Property PortfolioStatusName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("PortfolioStatusName"))
        End Get
    End Property

    <PersistentAlias("Iif(StatePortfolioConciliation = 1, 'Sin Radicar',StatePortfolioConciliation = 2, 'Radicada sin Confirmar',StatePortfolioConciliation = 3,'Radicada Entidad',StatePortfolioConciliation = 7,'Certificada Parcial',StatePortfolioConciliation = 8,'Certificada Total',StatePortfolioConciliation = 14,'Devolución Factura',StatePortfolioConciliation = 15,'Cuenta de Dificil Recaudo','Cobro Jurídico')")>
    Public ReadOnly Property StatePortfolioConciliationName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StatePortfolioConciliationName"))
        End Get
    End Property

#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

#End Region

End Class