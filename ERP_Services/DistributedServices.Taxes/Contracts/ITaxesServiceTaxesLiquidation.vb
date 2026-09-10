#Region "Imports"

Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities

#End Region

<ServiceContract()>
Public Interface ITaxesServiceTaxesLiquidation

    <OperationContract()>
    Function SaveTaxesLiquidation(Year As Integer, CadastralIdentification As String, CadastralIdentification2 As String, Address As String, Address2 As String, OwnerId As Integer, PropertyType As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Tuple(Of Integer, String))

    <OperationContract()>
    Function ConfirmTaxesLiquidation(Year As Integer, Ids As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Tuple(Of String, String, String, String))

End Interface
