Imports System.Runtime.Serialization

Imports Infrastructure.CrossCutting.Base

Partial Public Class BasicBillingDetail

#Region " Properties"

    Property RoundLevel As Integer = 2

    <DataMember>
    Property CodeName As String

    ReadOnly Property DetailTypeName As String
        Get
            Dim typeName = String.Empty
            Select Case Me.DetailType
                Case 1
                    typeName = "Producto"
                Case 2
                    typeName = "Servicio"
                Case 3
                    typeName = "Activo Fijo"
                Case 4
                    typeName = "Parte de Activo Fijo"
            End Select
            Return typeName
        End Get
    End Property

    ''' <summary>
    ''' Id del Centro de costo asociado a la unidad funcional
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Property CostCenterId As Integer?

    ''' <summary>
    ''' Unidad Funcional
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Property FunctionalUnitIdName As String

    ''' <summary>
    ''' Base de Retención en Fuente
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Property RetentionBaseTax As Decimal

    ''' <summary>
    ''' Base de Retención de ICA
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Property RetentionBaseICA As Decimal

    ''' <summary>
    ''' concatena el codigo y el nombre del campo Servicios prestados
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Public Property ServicesProvidedName As String

    ''' <summary>
    ''' concatena el codigo y el nombre del campo proveedores
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Public Property SupplierName As String

    ''' <summary>
    ''' concatena el codigo y el nombre del campo Ejecutivo de ventas
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Public Property SalesExecutiveName As String

    ''' <summary>
    ''' concatena el codigo y el nombre del almacen
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Public Property WarehouseName As String

    ''' <summary>
    ''' concatena el codigo y el nombre de la tarifa
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Property FeeName As String

    ''' <summary>
    ''' contiene el codigo alterno de los productos
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Property AlternativeCode As String

#End Region

#Region "Methods"

    Public Function CalculateValueDiscount() As Decimal
        Me.ValueDiscount = Math.Round(Me.Value * Me.PercentageDiscount / 100, Me.RoundLevel, MidpointRounding.AwayFromZero)
        Return Me.ValueDiscount
    End Function

    Public Function CalculateValueIVA() As Decimal
        Return Math.Round((Me.Value - Me.ValueDiscount) * Me.PercentageIVA / 100, Me.RoundLevel, MidpointRounding.AwayFromZero)
    End Function

    Public Function CalculateValueIVAWithOutRound() As Decimal
        Return (Me.Value - Me.ValueDiscount) * Me.PercentageIVA / 100D
    End Function

    Public Function CalculateSubTotalValue() As Decimal
        Return Me.Value - Me.ValueDiscount + Me.CalculateValueIVA()
    End Function

    Public Function CalculateWithholdingTax() As Decimal
        Me.WithholdingTax = Math.Round((Me.Value - Me.ValueDiscount) * Me.RetentionPercentageTax / 100, Me.RoundLevel, MidpointRounding.AwayFromZero)
        Return Me.WithholdingTax
    End Function

    Public Function CalculateWithholdingTax(roundLevel As Decimal) As Decimal
        Me.WithholdingTax = Utils.RoundValue((Me.Value - Me.ValueDiscount) * Me.RetentionPercentageTax / 100, roundLevel)
        Return Me.WithholdingTax
    End Function

    Public Function CalculateWithholdingICA() As Decimal
        Me.WithholdingICA = Math.Round((Me.Value - Me.ValueDiscount) * Me.RetentionPercentageICA / 100, Me.RoundLevel, MidpointRounding.AwayFromZero)
        Return Me.WithholdingICA
    End Function

    Public Function CalculateWithholdingICA(roundLevel As Decimal) As Decimal
        Me.WithholdingICA = Utils.RoundValue((Me.Value - Me.ValueDiscount) * Me.RetentionPercentageICA / 100, roundLevel)
        Return Me.WithholdingICA
    End Function

    Public Function CalculateTotalValue() As Decimal
        Return Me.CalculateSubTotalValue() - Me.WithholdingTax - Me.WithholdingICA
    End Function

#End Region

End Class
