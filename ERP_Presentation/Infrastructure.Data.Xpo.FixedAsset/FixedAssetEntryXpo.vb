Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.FixedAssetEntry")> _
Public Class FixedAssetEntryXpo
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

    Dim fEntryDate As DateTime
    Public Property EntryDate() As DateTime
        Get
            Return fEntryDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("EntryDate", fEntryDate, value)
        End Set
    End Property

    Dim fEntryNumber As String
    Public Property EntryNumber() As String
        Get
            Return fEntryNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EntryNumber", fEntryNumber, value)
        End Set
    End Property

    Dim fNumberContractLeasing As String
    Public Property NumberContractLeasing() As String
        Get
            Return fNumberContractLeasing
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NumberContractLeasing", fNumberContractLeasing, value)
        End Set
    End Property

    Dim fInitialDateLeasing As Date
    Public Property InitialDateLeasing() As Date
        Get
            Return fInitialDateLeasing
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("InitialDateLeasing", fInitialDateLeasing, value)
        End Set
    End Property

    Dim fEndDateLeasing As Date
    Public Property EndDateLeasing() As Date
        Get
            Return fEndDateLeasing
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("EndDateLeasing", fEndDateLeasing, value)
        End Set
    End Property

    Dim fAdquisitionType As Byte
    Public Property AdquisitionType() As Byte
        Get
            Return fAdquisitionType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("AdquisitionType", fAdquisitionType, value)
        End Set
    End Property

    <PersistentAlias("Iif(
AdquisitionType = 1, 'Compra Directa', 
AdquisitionType = 2, 'N/A',
AdquisitionType = 3, 'Comodato',
AdquisitionType = 4, 'Donado por Particulares',
AdquisitionType = 5, 'Traspaso de Bienes',
AdquisitionType = 6, 'Otro Concepto',
AdquisitionType = 7, 'Leasing Financiero',
AdquisitionType = 8, 'Comodato Tercerizado',
AdquisitionType = 9, 'Renting Financiero',
AdquisitionType = 10, 'Renting Operativo', '')")>
    Public ReadOnly Property AdquisitionTypeName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("AdquisitionTypeName"))
        End Get
    End Property

    Dim fSupplierId As Maintenance_Supplier
    <Association("FixedAssetEntryReferencesSupplier")> _
    Public Property SupplierId() As Maintenance_Supplier
        Get
            Return fSupplierId
        End Get
        Set(ByVal value As Maintenance_Supplier)
            SetPropertyValue(Of Maintenance_Supplier)("SupplierId", fSupplierId, value)
        End Set
    End Property

    Dim fAccountPayableId As AccountPayableXpo
    <Association("FixedAssetEntryReferencesAccountPayable")> _
    Public Property AccountPayableId() As AccountPayableXpo
        Get
            Return fAccountPayableId
        End Get
        Set(ByVal value As AccountPayableXpo)
            SetPropertyValue(Of AccountPayableXpo)("AccountPayableId", fAccountPayableId, value)
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

    <PersistentAlias("Iif(Status = 1, 'Sin Confirmar', Iif(Status = 2, 'Confirmado',Iif(Status = 3, 'Anulado', '')))")>
    Public ReadOnly Property StatusName As String
        Get
            'Select Case fStatus
            '    Case 1
            '        Return "Sin Confirmar"
            '    Case 2
            '        Return "Confirmado"
            '    Case 3
            '        Return "Anulado"
            '    Case Else
            '        Return String.Empty
            'End Select
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
    End Property

    Dim fFreightValue As Decimal
    Public Property FreightValue() As Decimal
        Get
            Return fFreightValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("FreightValue", fFreightValue, value)
        End Set
    End Property

    Dim fFreightIVAPercentage As Decimal
    Public Property FreightIVAPercentage() As Decimal
        Get
            Return fFreightIVAPercentage
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("FreightIVAPercentage", fFreightIVAPercentage, value)
        End Set
    End Property

    Dim fFreightIVAValue As Decimal
    Public Property FreightIVAValue() As Decimal
        Get
            Return fFreightIVAValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("FreightIVAValue", fFreightIVAValue, value)
        End Set
    End Property

    <PersistentAlias("concat(concat(Code,' - '),SupplierId.CodeName)")>
    Public ReadOnly Property CodeSupplier() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeSupplier"))
        End Get
    End Property

    Dim fCurrencyId As CommonCurrencyXpo
    <Association("FixedAssetEntryReferencesCurrency")>
    Public Property CurrencyId() As CommonCurrencyXpo
        Get
            Return fCurrencyId
        End Get
        Set(ByVal value As CommonCurrencyXpo)
            SetPropertyValue(Of CommonCurrencyXpo)("CurrencyId", fCurrencyId, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class


