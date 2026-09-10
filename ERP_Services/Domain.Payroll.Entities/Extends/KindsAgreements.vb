Imports System.Runtime.Serialization

Partial Public Class KindsAgreements

    Private _CodeNameConcatenated As String

    <DataMember()>
    Public Property CodeNameConcatenated As String
        Get
            Return Me._CodeNameConcatenated
        End Get
        Set(value As String)
            Me._CodeNameConcatenated = value
        End Set
    End Property

    ''' <summary>
    ''' Codigo cuenta
    ''' </summary>
    <DataMember()>
    Public Property AccountNumber As String

    ''' <summary>
    ''' Nombre cuenta
    ''' </summary>
    <DataMember()>
    Public Property AccountName As String

    ''' <summary>
    ''' Saber si esta parametrizado el concepto de nota cxc
    ''' </summary>
    <DataMember()>
    Public Property PortfolioNoteConceptParameter As Boolean



End Class
