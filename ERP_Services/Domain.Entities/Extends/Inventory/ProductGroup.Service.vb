Imports System.Runtime.Serialization

Partial Public Class ProductGroup

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el numero y el nombre de la cuenta contable
    ''' </summary>
    <DataMember()>
    Public Property IncomeAccountDescription As String

    ''' <summary>
    ''' Obtiene o establece el numero y el nombre de la cuenta contable
    ''' </summary>
    <DataMember()>
    Public Property IncomeRecognitionMainAccountDescription As String

    ''' <summary>
    ''' Obtiene o establece el codigo y nombre del concepto de cxp
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property InventoryAccountPayableConceptDescription As String

    ''' <summary>
    ''' Obtiene o establece el codigo y nombre del centro de costo
    ''' </summary>
    <DataMember()>
    Public Property ExternalIncomeDescription As String

    ''' <summary>
    ''' Obtiene o establece el codigo del anticipo
    ''' </summary>
    <DataMember()>
    Public Property InventoryExpensesDescription As String

    ''' <summary>
    ''' Obtiene o establece el codigo de la cxp
    ''' </summary>
    <DataMember()>
    Public Property CostSaleDescription As String


    ''' <summary>
    ''' Obtiene o establece el saldo del anticipo
    ''' </summary>
    <DataMember()>
    Public Property DeclarantRetentionAccountPayableConceptDescription As String

    ''' <summary>
    ''' Obtiene o establece el saldo del anticipo
    ''' </summary>
    <DataMember()>
    Public Property NotDeclarantRetentionAccountPayableConceptDescription As String

    ''' <summary>
    ''' Obtiene o establece el saldo del anticipo
    ''' </summary>
    <DataMember()>
    Public Property CostCenterDescription As String

    ''' <summary>
    ''' Obtiene o establece el saldo del anticipo
    ''' </summary>
    <DataMember()>
    Public Property RemissionInputDebitDescription As String

    ''' <summary>
    ''' Obtiene o establece el saldo del anticipo
    ''' </summary>
    <DataMember()>
    Public Property RemissionInputCreditDescription As String

    ''' <summary>
    ''' Obtiene o establece el saldo del anticipo
    ''' </summary>
    <DataMember()>
    Public Property RemissionOutputDebitDescription As String

    ''' <summary>
    ''' Obtiene o establece el saldo del anticipo
    ''' </summary>
    <DataMember()>
    Public Property RemissionOutputCreditDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de la cuenta debito usada para contabilizar el inventario en consignación
    ''' </summary>
    <DataMember()>
    Public Property ConsignmentMerchandiseDebitDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de la cuenta crédito usada para contabilizar el inventario en consignación
    ''' </summary>
    <DataMember()>
    Public Property ConsignmentMerchandiseCreditDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de la cuenta contrapartida del costo usada para contabilizar las dispensaciones, devoluciones del inventario en consignación
    ''' </summary>
    <DataMember()>
    Public Property CounterpartCostConsignedInventoryDescription As String

    ''' <summary>
    ''' Obtiene el numero y el nombre de la cuenta contable del costo inventario
    ''' </summary>
    <DataMember()>
    Public Property InventoryCostMainAccountDescription As String

    ''' <summary>
    ''' Obtiene el codigo y el nombre del concepto de retencion
    ''' </summary>
    <DataMember()>
    Public Property ReteFuenteConceptDescription As String

    <DataMember>
    Property CodeNameIVAAccount As String

    <DataMember>
    Property CodeNameWithholdingTaxAccount As String

    <DataMember>
    Property CodeNameWithholdingICAAccount As String

    <DataMember>
    Property CodeNameWithholdingICAConcept As String

    <DataMember>
    Property CodeNameAccountingPackageMainAccount As String

    <DataMember>
    Property CodeNameFavorableDeviationMainAccount As String

    <DataMember>
    Property CodeNameVariationPVMainAccount As String

#Region "Contabilidad"

    ''' <summary>
    ''' Porcentaje de rentencion en la fuente
    ''' </summary>
    <DataMember()>
    Public Property WithholdingSourcePercernt As Decimal?

    ''' <summary>
    ''' Id de la cuenta donde se asignara la cuenta por pagar de la retencion en la fuente
    ''' </summary>
    <DataMember()>
    Public Property AccountWithholdingSourceId As Integer?
    ''' <summary>
    ''' id del concecpto de retencion en la fuente
    ''' </summary>
    <DataMember()>
    Public Property ConceptAccountPayableWithholdingSourceId As Integer?
    ''' <summary>
    ''' id del concecpto de retencion en la fuente
    ''' </summary>
    <DataMember()>
    Public Property RetentionConceptsWithholdingSourceId As Integer?

    ''' <summary>
    ''' id del concecpto de retencion en la fuente
    ''' </summary>
    <DataMember()>
    Public Property AccountInventoryId As Integer?


    ''' <summary>
    ''' id del concecpto de retencion en la fuente
    ''' </summary>
    <DataMember()>
    Public Property AccountInventoryHandlessCostCenter As Boolean

    ''' <summary>
    ''' id del concecpto de retencion en la fuente
    ''' </summary>
    <DataMember()>
    Public Property ConceptAccountPayableInventory As Integer?

    ''' <summary>
    ''' indica si la cuenta asociada maneja tercero
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property HandlesThirdPartyAccount As Boolean

    <DataMember()>
    Public Property AccountInventoryCodeName As String

    <DataMember()>
    Public Property HandlesCostCenterWithholdigSourceAccount As Boolean


    ''' <summary>
    '''  Identifica si maneja centro de costo por contra parte inventario consignacion
    ''' </summary>
    <DataMember()>
    Public Property CounterpartCostConsignedHandlessCostCenter As Boolean
#End Region

#Region "Budget Interface"

    <DataMember()>
    Public Property BudgetaryEntityId As Integer?

    <DataMember()>
    Public Property BudgetaryEntityDescription As String

    <DataMember()>
    Public Property BudgetaryValidityId As Integer?

    <DataMember()>
    Public Property BudgetaryValidityDescription As String

    <DataMember()>
    Public Property BudgetDescription As String

#End Region

#End Region

End Class
