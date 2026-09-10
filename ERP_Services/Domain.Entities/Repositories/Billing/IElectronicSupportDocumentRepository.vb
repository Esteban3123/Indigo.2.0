Imports Domain.Base
Imports Domain.Entities

Public Interface IElectronicSupportDocumentRepository
    Inherits IRepository(Of ElectronicSupportDocument)

    Function GetDocumentSupportById(supporDocumentId As Integer) As ElectronicSupportDocument

    Function GetElectronicSupportDocumentByDocumentOrigin(EntityId As Integer, EntityName As String) As ElectronicSupportDocument

    Function GetElectronicDocumentIds() As List(Of Object)

    Function GetDocumentSupportDetails(supporDocumentId As Integer) As List(Of SP_GetDocumentSupportDetailsById_Result)

    Function GetDocumentSupportPaymentMethods(supporDocumentId As Integer) As List(Of SP_GetPaymentMethodByDocumentSupportId_Result)

    Function SP_GenerateElectronicSupportDocument(AccountsPayableXml As String, Optional AuthorizationResolutionId As Integer? = Nothing) As List(Of SP_GenerateElectronicSupportDocument_Result)

End Interface
