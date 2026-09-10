#Region "Imports"

Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities

#End Region

<ServiceContract()>
Public Interface ITaxesServiceTaxesProperty
    <OperationContract()>
    Function ValidateLoadPlaneCollection(data As List(Of String)) As ActionResult(Of List(Of Tuple(Of Integer, String, String, String)))
    <OperationContract()>
    Function SaveLoadPlaneCollection(data As List(Of String), audit As AuditMessage) As Domain.Base.Entities.ActionResult
    <OperationContract()>
    Function GetAllTaxedProperties() As List(Of TaxesProperty)

    ''' <summary>
    ''' Busca un TaxesProperty atraves de su code
    ''' </summary>
    ''' <param name="code">Code del TaxesProperty</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetTaxesPropertyByCode(code As String, session As SessionValues) As TaxesProperty
End Interface
