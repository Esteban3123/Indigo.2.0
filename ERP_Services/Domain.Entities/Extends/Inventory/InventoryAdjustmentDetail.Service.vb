Imports System.Runtime.Serialization
Imports Infrastructure.CrossCutting.Base

Partial Public Class InventoryAdjustmentDetail

    ''' <summary>
    ''' Descripcion del producto
    ''' </summary>
    <DataMember()>
    Public Property ProductCodeName As String

    ''' <summary>
    ''' unidad de medida del producto
    ''' </summary>
    <DataMember()>
    Public Property ProductUnid As String

    ''' <summary>
    ''' propiedad que contiene la unidad de consumo del producto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Property consumptionUnit As String

    ''' <summary>
    ''' Código + Nombre Concepto de ajuste
    ''' </summary>
    <DataMember()>
    Public Property CodeNameAdjustmentConcept As String

    ''' <summary>
    ''' Código + Nombre Centro de costo
    ''' </summary>
    <DataMember()>
    Public Property CodeNameCostCenter As String

End Class
