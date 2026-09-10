#Region "Imports"

Imports System.Runtime.Serialization

#End Region


Partial Public Class FixedAssetTransactionDetailBook

    ''' <summary>
    ''' Obtiene o establece el código y nombre de la Ubicación concatenado
    ''' </summary>
    <DataMember()>
    Public Property NameLegalBook As String

    ''' <summary>
    ''' Obtiene o establece la abreviacion de la moneda del libro contable
    ''' </summary>
    <DataMember()>
    Public Property CurrencyAbbreviationLegalBook As String

End Class
