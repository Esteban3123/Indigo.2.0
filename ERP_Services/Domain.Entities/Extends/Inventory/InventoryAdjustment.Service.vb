Imports System.Runtime.Serialization
Imports Infrastructure.CrossCutting.Base

Partial Public Class InventoryAdjustment

    ''' <summary>
    ''' Descripcion del almacen
    ''' </summary>
    <DataMember()>
    Public Property DescriptionWarehouse As String

    ''' <summary>
    ''' Descripcion del almacen
    ''' </summary>
    <DataMember()>
    Public Property CodeNameAdjustmentConcept As String

    ''' <summary>
    ''' Descripcion del almacen
    ''' </summary>
    <DataMember()>
    Public Property CodeNameInventoryControl As String


    ''' <summary>
    ''' Descripcion del almacen
    ''' </summary>
    <DataMember()>
    Public Property NameThirdParty As String

    <DataMember()>
    Public Property Prefix As String

    <DataMember()>
    Public Property DocumentDateInventoryControl As Date

End Class
