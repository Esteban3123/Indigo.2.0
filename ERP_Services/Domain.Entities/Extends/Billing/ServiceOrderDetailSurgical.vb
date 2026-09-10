Imports System.Runtime.Serialization
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base

Partial Public Class ServiceOrderDetailSurgical
    Inherits Entity(Of ServiceOrderDetailSurgical)

    <DataMember()>
    Public Property CodeNameIpsService As String

    <DataMember()>
    Public Property ClassServiceIps As String

    <DataMember()>
    Public Property QuotationServiceOrderDetailId As Integer

    <DataMember()>
    Public Property AuthorizationOutsourcedServicesServiceOrderDetailId As Integer

    <DataMember()>
    Public Property AllowValueChange As Boolean

#Region "PropertiesMedicalFees"

    'Estas propiedades se utilizan en el modulo de MedicalFees

    <DataMember()>
    Public Property IPSServiceDescription As String

    <DataMember()>
    Public Property MedicDescription As String

    <DataMember()>
    Public Property SelectOption As Boolean

#End Region


    ''' <summary>
    ''' Crea una copia de la entidad
    ''' </summary>
    ''' <returns>Copia de la entidad</returns>
    Public Function CloneEntity() As ServiceOrderDetailSurgical
        Dim entity As ServiceOrderDetailSurgical = DirectCast(MemberwiseClone(), ServiceOrderDetailSurgical)
        Return entity
    End Function

    Public Sub New()

    End Sub

    Public Sub New(dataXml As Xml.XmlNode)

        Me.Id = dataXml.SelectSingleNode("Id").AsInt()
        Me.ServiceOrderDetailId = dataXml.SelectSingleNode("ServiceOrderDetailId").AsInt()
        Me.CodeNameIpsService = dataXml.SelectSingleNode("CodeNameIpsService").AsString()
        Me.IPSServiceId = dataXml.SelectSingleNode("IPSServiceId").AsInt()
        Me.InvoicedQuantity = dataXml.SelectSingleNode("InvoicedQuantity").AsInt()
        Me.LiquidationPercentage = dataXml.SelectSingleNode("LiquidationPercentage").AsDecimal()
        Me.RateManualSalePrice = dataXml.SelectSingleNode("RateManualSalePrice").AsDecimal()
        Me.TotalSalesPrice = dataXml.SelectSingleNode("TotalSalesPrice").AsDecimal()
        Me.PerformsHealthProfessionalCode = dataXml.SelectSingleNode("PerformsHealthProfessionalCode").AsString()
        Me.PerformsHealthProfessionalThirdPartyId = dataXml.SelectSingleNode("PerformsHealthProfessionalThirdPartyId").AsInt()
        Me.CostValue = dataXml.SelectSingleNode("CostValue").AsDecimal()
        Me.BillingConceptId = dataXml.SelectSingleNode("BillingConceptId").AsInt()
        Me.CostCenterId = dataXml.SelectSingleNode("CostCenterId").AsInt()
        Me.RateManualDetailSurgicalId = dataXml.SelectSingleNode("RateManualDetailSurgicalId").AsInt()
        Me.SurchargeApply = dataXml.SelectSingleNode("SurchargeApply").AsBoolean()
        Me.IncomeMainAccountId = dataXml.SelectSingleNode("IncomeMainAccountId").AsInt()
        Me.ClassServiceIps = dataXml.SelectSingleNode("ClassServiceIps").AsString()

    End Sub

End Class
