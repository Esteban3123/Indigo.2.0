Imports System.Runtime.Serialization

Partial Public Class OtherWithholdingDeduction

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece la descripcion de la cuenta contable 
    ''' </summary>
    <DataMember()>
    Public Property MainAccountDescription As String

    ''' <summary>
    ''' Defne si el item fue seleccionado de una rejilla
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Selected As Boolean

    ''' <summary>
    ''' Obtiene o etablece el codigo y el nombre del con
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property CodeNameConcepts As String

    ''' <summary>
    ''' Obtiene o establece el porcentaje de retencion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property RetentionPercentage As Decimal

    Public Property BaseValue As Decimal


    ''' <summary>
    ''' Obtiene o establece el codigo de la cuenta por pagar
    ''' </summary>
    <DataMember()>
    Public Property AccountPayableConceptDescription As String
#End Region

End Class
