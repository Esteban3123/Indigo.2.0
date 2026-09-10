Imports System.Runtime.Serialization
Imports Domain.Base.Entities

Partial Public Class CashRegisters
    Inherits Entity(Of Domain.Entities.CashRegisters)

    <DataMember>
    Property MainAccountHandlesCostCenter As Boolean

    <DataMember>
    Property CurrencyName As String

    ''' <summary>
    ''' Bandera para saber si la entidad bancaria tiene movimientos
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Property hasMovements As Boolean

End Class