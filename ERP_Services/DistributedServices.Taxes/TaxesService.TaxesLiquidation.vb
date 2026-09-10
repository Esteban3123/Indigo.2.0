'***********************************************************************
' Assembly         : DistributedServices.Billing
' Author           : Carlos Ernesto Cordoba
' Created          : 13-11-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Application.Taxes
Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Microsoft.Practices.Unity

#End Region

Partial Public Class TaxesService

    Public Function SaveTaxesLiquidation(Year As Integer, CadastralIdentification As String, CadastralIdentification2 As String, Address As String, Address2 As String, OwnerId As Integer, PropertyType As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Tuple(Of Integer, String)) Implements ITaxesServiceTaxesLiquidation.SaveTaxesLiquidation
        Using service As ITaxesLiquidationAdminService = Container.Current.Resolve(Of ITaxesLiquidationAdminService)()
            Return service.SaveTaxesLiquidation(Year, CadastralIdentification, CadastralIdentification2, Address, Address2, OwnerId, PropertyType, audit)
        End Using
        'Return _taxesLiquidationAdminService.SaveTaxesLiquidation(Year, CadastralIdentification, CadastralIdentification2, Address, Address2, OwnerId, PropertyType, audit)
    End Function

    Public Function ConfirmTaxesLiquidation(Year As Integer, Ids As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Tuple(Of String, String, String, String)) Implements ITaxesServiceTaxesLiquidation.ConfirmTaxesLiquidation
        Using service As ITaxesLiquidationAdminService = Container.Current.Resolve(Of ITaxesLiquidationAdminService)()
            Return service.ConfirmTaxesLiquidation(Year, Ids, audit)
        End Using
        'Return _taxesLiquidationAdminService.ConfirmTaxesLiquidation(Year, Ids, audit)
    End Function

End Class
