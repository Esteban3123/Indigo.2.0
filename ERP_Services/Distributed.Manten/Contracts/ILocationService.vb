#Region "Imports"
Imports System.ServiceModel
Imports Domain.Maintenance.Entities
Imports Infrastructure.CrossCutting.Base

#End Region


<ServiceContract()> _
Public Interface ILocationService



    <OperationContract()> _
    Function ListAllLocation(Empresa As String) As List(Of Location)


    <OperationContract()> _
    Function DeleteLocation(Empresa As String, ByVal Location As Location, ByVal audit As AuditMessage) As Boolean


    <OperationContract()> _
    Function SaveLocation(Empresa As String, ByVal Location As List(Of Location), ByVal audit As AuditMessage) As Boolean

    <OperationContract()> _
    Function GetLocation(Empresa As String, ByVal codeLocation As String) As Location


    Function ListLocation(Empresa As String) As List(Of Location)
End Interface
