Imports System.Runtime.Serialization

Partial Public Class CostDistributionDirectCost

#Region "Properties"

    <DataMember()> _
    Property FullNameGeneralExpense As String

    <DataMember()> _
    Property ThirdPartyDescription As String

    <DataMember()> _
    Property DistributionLineCodeName As String

    <DataMember()>
    Property PositionCodeName As String

    <DataMember()> _
    Property MainAccountCodeName As String

    <DataMember()> _
    Property CostCenterCodeName As String

    <DataMember()> _
    Property FilingUnitCodeName As String

    <DataMember()> _
    Property SupplierTypeCodeName As String

    <DataMember()>
    Property CurrencyAbbreviation As String

    ''' <summary>
    ''' Bandera para saber si el item esta activo
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Property Activated As Boolean

    ''' <summary>
    ''' bandera para saber que el item que voy a importar cumpla con las validaciones que se hicieron
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Property ItemInvalid As Boolean

    ''' <summary>
    ''' Estado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Property StatusName As String

#End Region

End Class
