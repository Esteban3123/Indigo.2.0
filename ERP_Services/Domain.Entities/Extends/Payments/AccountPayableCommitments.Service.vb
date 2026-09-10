#Region "Imports"

Imports System.Runtime.Serialization

#End Region

Partial Public Class AccountPayableCommitments

#Region "Properties"

    ''' <summary>
    ''' Código del compromiso
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property CommitmentCode As String

    ''' <summary>
    ''' Documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property CommitmentDocument As String

    ''' <summary>
    ''' Rubro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property CategoryCodeName As String

    ''' <summary>
    ''' Recurso
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property FinancialSourceCodeName As String

    ''' <summary>
    ''' Tipo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property RevenueTypeCodeName As String

    ''' <summary>
    ''' Saldo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property Balance As Decimal

#End Region

End Class
