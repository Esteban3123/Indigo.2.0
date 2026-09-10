#Region "Imports"

Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities

#End Region

<ServiceContract()>
Public Interface ITaxesServiceFiscalization

    <OperationContract()>
    Function SP_ValidateTaxBase(ListData As List(Of String), Year As Integer) As ActionResult(Of List(Of SP_ValidateTaxBase_Result))

End Interface
