Imports System.Runtime.Serialization
Imports Infrastructure.CrossCutting.Base

Partial Public Class EntranceVoucherDevolutionOtherDeduction

    ''' <summary>
    ''' valor base
    ''' </summary>
    <DataMember()>
    Public Property ValueBase As Decimal

    ''' <summary>
    ''' porcentaje de la retencion
    ''' </summary>
    <DataMember()>
    Public Property RetentionPercentage As Decimal

    ''' <summary>
    ''' Nombre de concepto
    ''' </summary>
    <DataMember()>
    Public Property ConceptName As String

    ''' <summary>
    ''' Id de OtherOtherWithholdingDeductionId
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property OtherWithholdingDeductionId As Integer?

    ''' <summary>
    ''' Obtiene el valor a devolver
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property DevolutionValue As Decimal
End Class
