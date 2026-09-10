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
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Microsoft.Practices.Unity

#End Region

Partial Public Class TaxesService

    Public Function GetLowTaxLiquidation(consecutive As String) As LowTaxLiquidation Implements ILowTaxServiceTaxesLiquidation.GetLowTaxLiquidation
        Using service As ILowTaxLiquidationAdminService = Container.Current.Resolve(Of ILowTaxLiquidationAdminService)()
            Return service.GetLowTaxLiquidation(consecutive, Nothing)
        End Using
        'Return _lowTaxesLiquidationAdminService.GetLowTaxLiquidation(consecutive, Nothing)
    End Function


    Public Function SaveLowTaxLiquidation(Entity As LowTaxLiquidation, state As Integer, audit As AuditMessage) As ActionResult(Of LowTaxLiquidation) Implements ILowTaxServiceTaxesLiquidation.SaveLowTaxLiquidation
        Using service As ILowTaxLiquidationAdminService = Container.Current.Resolve(Of ILowTaxLiquidationAdminService)()
            Return service.SaveLowTaxLiquidation(Entity, state, audit)
        End Using
        'Return _lowTaxesLiquidationAdminService.SaveLowTaxLiquidation(Entity, state, audit)
    End Function

End Class
