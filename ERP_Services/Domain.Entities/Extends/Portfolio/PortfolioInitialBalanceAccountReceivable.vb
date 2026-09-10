Imports System.Runtime.Serialization
Imports Domain.Base.Entities

Partial Public Class PortfolioInitialBalanceAccountReceivable
    Inherits Entity(Of Domain.Entities.PortfolioInitialBalanceAccountReceivable)

    <DataMember>
    Property CodeNameGlosasCostCenter As String

    <DataMember>
    Property CodeNameCustomer As String

    <DataMember>
    Property CodeNameAccountWithoutRadicate As String

    <DataMember>
    Property CodeNameAccountRadicate As String

    <DataMember>
    Property CodeNameAccountObjectionRemedied As String

    <DataMember>
    Property CodeNameAccountConciliation As String

    <DataMember>
    Property CodeNameAccountLegalCollection As String

    <DataMember>
    Property CodeNameAccountDebtorOrder As String

    <DataMember>
    Property CodeNameAccountCreditorOrder As String


    ''' <summary>
    ''' Crea una copia de la entidad
    ''' </summary>
    ''' <returns>Copia de la entidad</returns>
    Public Function CloneEntity() As PortfolioInitialBalanceAccountReceivable
        Dim entity As PortfolioInitialBalanceAccountReceivable = DirectCast(MemberwiseClone(), PortfolioInitialBalanceAccountReceivable)
        Return entity
    End Function
End Class
