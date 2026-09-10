Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Billing.ElectronicSupportDocumentAdjustmentNote")>
Public Class ElectronicSupportDocumentAdjustmentNoteXpo
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

    Dim fDocumentDate As DateTime
    Public Property DocumentDate() As DateTime
        Get
            Return fDocumentDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("DocumentDate", fDocumentDate, value)
        End Set
    End Property

    Dim fNoteType As Byte
    Public Property NoteType() As Byte
        Get
            Return fNoteType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("NoteType", fNoteType, value)
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

    <PersistentAlias("ElectronicSupportDocument.Id")>
    Public ReadOnly Property ElectronicSupportDocumentId() As Integer
        Get
            Return Convert.ToInt32(EvaluateAlias("ElectronicSupportDocumentId"))
        End Get
    End Property

    Dim fElectronicSupportDocument As ElectronicSupportDocumentXpo
    <Persistent("ElectronicSupportDocumentId")>
    <Association("ElectronicSupportDocumentAdjustmentNote_ElectronicSupportDocument")>
    Public Property ElectronicSupportDocument() As ElectronicSupportDocumentXpo
        Get
            Return fElectronicSupportDocument
        End Get
        Set(ByVal value As ElectronicSupportDocumentXpo)
            SetPropertyValue("ElectronicSupportDocument", fElectronicSupportDocument, value)
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

    <PersistentAlias("Iif(Status = 1, 'Registrado', 'Confirmado')")>
    Public ReadOnly Property StatusName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
    End Property

    <PersistentAlias("Iif(NoteType = 1, 'Ajuste documento soporte', 'Reversión CxP')")>
    Public ReadOnly Property NoteTypeName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("NoteTypeName"))
        End Get
    End Property

    <PersistentAlias("Iif(Nature = 1, 'Débito', 'Crédito')")>
    Public ReadOnly Property NatureName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("NatureName"))
        End Get
    End Property

#End Region

#Region "Navigation"

    <Association("ElectronicSupportDocumentAdjustmentNoteDetail_References_ElectronicSupportDocumentAdjustmentNote", GetType(ElectronicSupportDocumentAdjustmentNoteDetailXpo))>
    Public ReadOnly Property ElectronicSupportDocumentAdjustmentNoteDetail() As XPCollection(Of ElectronicSupportDocumentAdjustmentNoteDetailXpo)
        Get
            Return GetCollection(Of ElectronicSupportDocumentAdjustmentNoteDetailXpo)("ElectronicSupportDocumentAdjustmentNoteDetail")
        End Get
    End Property

#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

#End Region

End Class