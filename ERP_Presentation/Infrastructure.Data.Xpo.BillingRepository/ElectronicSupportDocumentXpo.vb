Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Billing.ElectronicSupportDocument")>
Public Class ElectronicSupportDocumentXpo
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
    <Size(20)>
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
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

    Dim fRadicationDate As DateTime
    Public Property RadicationDate() As DateTime
        Get
            Return fRadicationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("RadicationDate", fRadicationDate, value)
        End Set
    End Property

    Dim fDueDate As DateTime
    Public Property DueDate() As DateTime
        Get
            Return fDueDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("DueDate", fDueDate, value)
        End Set
    End Property

    <PersistentAlias("SupplierThirdParty.Id")>
    Public ReadOnly Property SupplierThirdPartyId() As Integer
        Get
            Return Convert.ToInt32(EvaluateAlias("SupplierThirdPartyId"))
        End Get
    End Property

    Dim fSupplierThirdParty As ThirdPartyXpo
    <Persistent("SupplierThirdPartyId")>
    <Association("ElectronicSupportDocumentXpo_ThirdParty")>
    Public Property SupplierThirdParty() As ThirdPartyXpo
        Get
            Return fSupplierThirdParty
        End Get
        Set(ByVal value As ThirdPartyXpo)
            SetPropertyValue("SupplierThirdParty", fSupplierThirdParty, value)
        End Set
    End Property

    Dim fStatus As Boolean
    Public Property Status() As Boolean
        Get
            Return fStatus
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Status", fStatus, value)
        End Set
    End Property

    Dim fSubTotalValue As Decimal
    Public Property SubTotalValue() As Decimal
        Get
            Return fSubTotalValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("SubTotalValue", fSubTotalValue, value)
        End Set
    End Property

    Dim fTaxValue As Decimal
    Public Property TaxValue() As Decimal
        Get
            Return fTaxValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TaxValue", fTaxValue, value)
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

    Dim fCUDS As String
    Public Property CUDS As String
        Get
            Return fCUDS
        End Get
        Set(value As String)
            SetPropertyValue(Of String)("CUDS", fCUDS, value)
        End Set
    End Property

    Dim fEntityCode As String
    Public Property EntityCode As String
        Get
            Return fEntityCode
        End Get
        Set(value As String)
            SetPropertyValue(Of String)("EntityCode", fEntityCode, value)
        End Set
    End Property

    Dim fEntityName As String
    Public Property EntityName As String
        Get
            Return fEntityName
        End Get
        Set(value As String)
            SetPropertyValue(Of String)("EntityName", fEntityName, value)
        End Set
    End Property

    Dim fEntityId As Integer
    Public Property EntityId As Integer
        Get
            Return fEntityId
        End Get
        Set(value As Integer)
            SetPropertyValue(Of Integer)("EntityId", fEntityId, value)
        End Set
    End Property


    <PersistentAlias("Iif(Status = 0, 'Registrado','Confirmado')")>
    Public ReadOnly Property StatusName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
    End Property

    <PersistentAlias("concat('Código: ', Code, ' - Fecha documento: ', DocumentDate)")>
    Public ReadOnly Property FullName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("FullName"))
        End Get
    End Property

    <Association("ElectronicSupportDocumentAdjustmentNote_ElectronicSupportDocument", GetType(ElectronicSupportDocumentAdjustmentNoteXpo))>
    Public ReadOnly Property ElectronicSupportDocumentAdjustmentNotes() As XPCollection(Of ElectronicSupportDocumentAdjustmentNoteXpo)
        Get
            Return GetCollection(Of ElectronicSupportDocumentAdjustmentNoteXpo)("ElectronicSupportDocumentAdjustmentNotes")
        End Get
    End Property

#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

#End Region

End Class