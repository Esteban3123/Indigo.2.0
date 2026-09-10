Imports System.Runtime.Serialization
Partial Public Class BudgetaryValidity

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece la descripcion del representante legal
    ''' </summary>
    <DataMember()>
    Public Property LegalRepresentativeDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion del jefe de presupuesto
    ''' </summary>
    <DataMember()>
    Public Property BudgetBossDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion del jefe de financiero
    ''' </summary>
    <DataMember()>
    Public Property FinancialBossDescription As String

#End Region

End Class
