'***********************************************************************
' Assembly         : DistributedService.Billing
' Author           : Carlos Ernesto Cordoba
' Created          : 01-04-2014
'
' Last Modified By : 
' Last Modified On : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel

#End Region

<ServiceModel.ServiceContract()>
Public Interface ITaxesService
    Inherits ITaxesServiceTaxesProperty, ITaxesServiceTaxesLiquidation, ILowTaxServiceTaxesLiquidation, ITaxesServiceFiscalization

End Interface
