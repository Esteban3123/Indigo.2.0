Imports System.Runtime.Serialization
Imports Domain.Base.Entities

Partial Public Class EntityBankAccounts
    Inherits Entity(Of Domain.Entities.EntityBankAccounts)

#Region "Properties"
    <DataMember()>
    Property FullNameMainAccount As String
    <DataMember()>
    Property Name As String 'Nombre del banco relacionado a la cuenta bancaria
    <DataMember()>
    Property TransferValue As Decimal 'Valor del traslado (Comprobante de Egreso)

    <DataMember()>
    Property FinancialSourceDescription As String

    <DataMember()>
    Property CurrencyAbbreviation As String

    ''' <summary>
    ''' Bandera para saber si la entidad bancaria tiene movimientos
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Property hasMovements As Boolean

#End Region

End Class
