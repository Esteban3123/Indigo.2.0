#Region "Imports"
Imports System.ServiceModel
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

#End Region




<ServiceContract()> _
Public Interface IBrandService



    <OperationContract()> _
    Function ListAllBrand(Empresa As String) As List(Of Brand)


    <OperationContract()> _
    Function DeleteBrand(Empresa As String, ByVal Brand As Brand, ByVal audit As AuditMessage) As Boolean


    <OperationContract()> _
    Function SaveBrand(Empresa As String, ByVal Brand As Brand, ByVal audit As AuditMessage) As Boolean

    <OperationContract()> _
    Function GetBrand(Empresa As String, ByVal codeBrand As String) As Brand


    Function ListBrand(Empresa As String) As List(Of Brand)
End Interface
