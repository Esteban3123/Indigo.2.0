Imports System.Runtime.Serialization
Partial Public Class ProductMixingStation
    <DataMember()>
    Property ItemId As Integer?
    <DataMember()>
    Property ProductCode As String
    <DataMember()>
    Property OnlyNameProduct As String
    <DataMember()>
    Property ProductName As String
    <DataMember()>
    Property ProductShortName As String
    <DataMember()>
    Property QuantityMaterialRaw As Decimal
    <DataMember()>
    Property Dosis As Decimal
    <DataMember()>
    Property RequiredDosis As Decimal
    <DataMember()>
    Property ItemType As Byte
    <DataMember()>
    Property GroupName As String
    <DataMember()>
    Property TypeProduct As eTypeProduct
    <DataMember()>
    Property NameTypeProduct As String
    <DataMember()>
    Property BatchCode As String
    <DataMember()>
    Public Property BatchData As List(Of BatchData)
    <DataMember()>
    Public Property MeasureUnitAbbreviation As String
    <DataMember()>
    Public Property TotalToUseQuantity As Decimal
    <DataMember()>
    Public Property DosisRequeridaPaquete As Decimal
    <DataMember()>
    Public Property Concentration As Decimal
    <DataMember()>
    Public Property Vehicle As Byte
    <DataMember()>
    Public Property Thinner As Byte
    <DataMember()>
    Public Property FormulationType As Byte?
    <DataMember()>
    Public Property NPTItemOrder As Byte?
    <DataMember()>
    Public Property RequestMixingStationDetailId As Integer?
    <DataMember()>
    Public Property PackageId As Integer?
    <DataMember()>
    Public Property MSClass As Integer
    <DataMember()>
    Property VerifiedFor As String
    <DataMember()>
    Property ConfirmedFor As String
    <DataMember()>
    Property IsPackagePersonalized As Boolean  ' Indica si viene de un paquete personalizado

    Enum eTypeProduct
        Atc = 1
        Supply = 2
        Product = 3
    End Enum
End Class

Public Class BatchData
    Public Property BatchCode As String
    Public Property Quantity As Decimal
    Public Property MeasurementUnit As String
End Class

Public Class ProductCalculateMixingStation
    Public Property TypeProduct As Integer
    Public Property NameTypeProduct As String
    Public Property ItemId As Integer
    Public Property Code As String
    Public Property Name As String
    Public Property ItemType As Byte
    Public Property GroupName As String
    Public Property CantidadDosis As Decimal
    Public Property CodigoUnidadPeso As String
    Public Property CodigoUnidadVolumen As String
    Public Property FormulationType As Byte?
    Public Property ATCEntityId As Integer?
    Public Property PharmaceuticalFormId As Integer?
    Public Property Concentration As Decimal
    Public Property DosisRequerida As Decimal
    Public Property QuantityPackage As Decimal
    Public Property BatchCode As String
    Public Property BatchCodes As List(Of String)
    Public Property MeasureUnitAbbreviation As String
    Public Property Thinner As Byte
    Public Property QuantityBlister As Decimal
    Public Property PackageId As Integer?
    Public Property Vehicle As Byte
    Public Property NPTItemOrder As Byte?
    Public Property RequestMixingStationDetailId As Integer?
    Public Property VerifiedFor As String
    Public Property ConfirmedFor As String
    Public Property IsPackagePersonalized As Boolean  ' Indica si viene de un paquete personalizado
End Class

Public Class ProductCalculationCampaignDetail
    Public Property Id As Integer
    Public Property ProductCode As String
    Public Property ProductName As String
    Public Property ProductAbbreviationName As String
    Public Property Concentration As Decimal
    Public Property BatchCode As String
End Class