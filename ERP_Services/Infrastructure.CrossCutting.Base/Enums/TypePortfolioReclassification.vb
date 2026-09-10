Imports System.Runtime.Serialization
''' <summary>
''' Enumeracion para determinar el tipo de documento de la reclasificacion 
''' </summary>
''' <remarks></remarks>
<DataContract()>
Public Enum TypePortfolioReclassification As Integer
    ''' <summary>
    ''' 1 - Radicacion - Publico y privada
    ''' </summary>
    ''' <remarks></remarks>
    Radication = 1
    ''' <summary>
    ''' 2 - Devoluciones - Privada
    ''' </summary>
    ''' <remarks></remarks>
    Devolution = 2
    ''' <summary>
    ''' 3 - Recepcion de Glosas Subsanable - Privada
    ''' </summary>
    ''' <remarks></remarks>
    ReceptionofGlosas = 3
    ''' <summary>
    ''' 4 - Conciliaciones - Privada
    ''' </summary>
    ''' <remarks></remarks>
    Conciliation = 4
    ''' <summary>
    ''' 5 Cobro Juridico
    ''' </summary>
    ''' <remarks></remarks>
    TransFerJuridical = 5
    ''' <summary>
    ''' 4 - Conciliaciones - Privada por saldo restante en reiteraciones; cuando el valor de la reiteracion es 
    ''' menor al saldo pendiente se determina que la eapb indirectamnete acepta dicho valor restante
    ''' </summary>
    ''' <remarks></remarks>
    ConciliationBalanceReiterated = 6
End Enum