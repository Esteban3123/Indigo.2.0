Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Common.Customer")> _
Public Class CommonCustomerReportXpo
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
    Dim fNit As String
    '<Indexed(Name:="IX_Customer", Unique:=True)> _
    <Size(15)> _
    Public Property Nit() As String
        Get
            Return fNit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Nit", fNit, value)
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
    Dim fEPSCode As String
    <Size(20)> _
    Public Property EPSCode() As String
        Get
            Return fEPSCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EPSCode", fEPSCode, value)
        End Set
    End Property
    Dim fThirdPartyId As CommonThirdPartyXpo
    <Association("CommonCustomerReportXpoReferencesCommonThirdPartyXpo")> _
    Public Property ThirdPartyId() As CommonThirdPartyXpo
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As CommonThirdPartyXpo)
            SetPropertyValue(Of CommonThirdPartyXpo)("ThirdPartyId", fThirdPartyId, value)
        End Set
    End Property
    Dim fState As Boolean
    Public Property State() As Boolean
        Get
            Return fState
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("State", fState, value)
        End Set
    End Property
    Dim fNitName As String
    'columna que devuelve el Nit y el nombre concatenado
    <Size(50)> _
    <PersistentAlias("concat(concat(Nit,' - '),Name)")>
    Public ReadOnly Property NitName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("NitName"))
        End Get
    End Property
    'Propiedad Añadida
    Dim fSeleccionado As Boolean = False
    <NonPersistent()>
    Public Property Seleccionado() As Boolean
        Get
            Return fSeleccionado
        End Get
        Set(ByVal value As Boolean)
            Me.fSeleccionado = value
        End Set
    End Property

    <PersistentAlias("Iif(State = 1, 'Activo', 'Inactivo')")>
    Public ReadOnly Property StatusName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
    End Property

    <Association("PortfolioAdvanceReportXpoReferencesCommonCustomerReportXpo", GetType(PortfolioAdvanceReportXpo))>
    Public ReadOnly Property PortfolioAdvanceReportXpo() As XPCollection(Of PortfolioAdvanceReportXpo)
        Get
            Return GetCollection(Of PortfolioAdvanceReportXpo)("PortfolioAdvanceReportXpo")
        End Get
    End Property

    <Association("PortfolioTransferReportXpoReferencesCommonCustomerReportXpo", GetType(PortfolioTransferReportXpo))>
    Public ReadOnly Property PortfolioTransferReportXpo() As XPCollection(Of PortfolioTransferReportXpo)
        Get
            Return GetCollection(Of PortfolioTransferReportXpo)("PortfolioTransferReportXpo")
        End Get
    End Property

    <Association("PortfolioAccountReceivableReportXpoReferencesCommonCustomerReportXpo", GetType(PortfolioAccountReceivableReportXpo))>
    Public ReadOnly Property PortfolioAccountReceivableReportXpo() As XPCollection(Of PortfolioAccountReceivableReportXpo)
        Get
            Return GetCollection(Of PortfolioAccountReceivableReportXpo)("PortfolioAccountReceivableReportXpo")
        End Get
    End Property

    <Association("PortfolioInitialBalanceAccountReceivableReportXpoReferencesCommonCustomerReportXpo", GetType(PortfolioInitialBalanceAccountReceivableReportXpo))>
    Public ReadOnly Property PortfolioInitialBalanceAccountReceivableReportXpo() As XPCollection(Of PortfolioInitialBalanceAccountReceivableReportXpo)
        Get
            Return GetCollection(Of PortfolioInitialBalanceAccountReceivableReportXpo)("PortfolioInitialBalanceAccountReceivableReportXpo")
        End Get
    End Property

    <Association("PortfolioInitialBalanceAdvanceReportXpoReferencesCommonCustomerReportXpo", GetType(PortfolioInitialBalanceAdvanceReportXpo))>
    Public ReadOnly Property PortfolioInitialBalanceAdvanceReportXpo() As XPCollection(Of PortfolioInitialBalanceAdvanceReportXpo)
        Get
            Return GetCollection(Of PortfolioInitialBalanceAdvanceReportXpo)("PortfolioInitialBalanceAdvanceReportXpo")
        End Get
    End Property

    <Association("PortfolioNoteReportXpoReferencesCommonCustomerReportXpo", GetType(PortfolioNoteReportXpo))>
    Public ReadOnly Property PortfolioNoteReportXpo() As XPCollection(Of PortfolioNoteReportXpo)
        Get
            Return GetCollection(Of PortfolioNoteReportXpo)("PortfolioNoteReportXpo")
        End Get
    End Property

    <Association("PortfolioNoteDistributionReferencesCommonCustomerReportXpo", GetType(PortfolioNoteDistributionXpo))>
    Public ReadOnly Property PortfolioNoteDistributionReportXpo() As XPCollection(Of PortfolioNoteDistributionXpo)
        Get
            Return GetCollection(Of PortfolioNoteDistributionXpo)("PortfolioNoteDistributionReportXpo")
        End Get
    End Property

    <Association("PortfolioNoteDistributionOriginalReferencesCommonCustomerReportXpo", GetType(PortfolioNoteDistributionOriginalXpo))>
    Public ReadOnly Property PortfolioNoteDistributionOriginalXpo() As XPCollection(Of PortfolioNoteDistributionOriginalXpo)
        Get
            Return GetCollection(Of PortfolioNoteDistributionOriginalXpo)("PortfolioNoteDistributionOriginalXpo")
        End Get
    End Property

    <Association("PortfolioTransferReferencesCommonCustomerReportXpo", GetType(PortfolioTransferReportXpo))>
    Public ReadOnly Property PortfolioTransferCustomerReportXpo() As XPCollection(Of PortfolioTransferReportXpo)
        Get
            Return GetCollection(Of PortfolioTransferReportXpo)("PortfolioTransferCustomerReportXpo")
        End Get
    End Property

    <Association("Portfolio_AccountReceivableDocumentReferencesCommon_Customer")>
    Public ReadOnly Property PortfolioAccountReceivableDocument() As XPCollection(Of PortfolioAccountReceivableDocumentReportXpo)
        Get
            Return GetCollection(Of PortfolioAccountReceivableDocumentReportXpo)("PortfolioAccountReceivableDocument")
        End Get
    End Property

    <Association("PortfolioRadicateInvoiceCReportXpoReferencesCommonCustomerReportXpo")> _
    Public ReadOnly Property PortfolioRadicateInvoiceCReportXpo() As XPCollection(Of PortfolioRadicateInvoiceCReportXpo)
        Get
            Return GetCollection(Of PortfolioRadicateInvoiceCReportXpo)("PortfolioRadicateInvoiceCReportXpo")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
