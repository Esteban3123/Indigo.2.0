#Region "Imports"

Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities

#End Region

<ServiceContract()>
Public Interface ILowTaxServiceTaxesLiquidation

    <OperationContract()>
    Function GetLowTaxLiquidation(consecutive As String) As LowTaxLiquidation

    <OperationContract()>
    Function SaveLowTaxLiquidation(Entity As LowTaxLiquidation, state As Integer, audit As AuditMessage) As ActionResult(Of LowTaxLiquidation)

End Interface
