'***********************************************************************
' Assembly         : DistributedServices.Portfolio
' Author           : Carlos Ernesto Cordoba
' Created          : 01-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
#End Region

<ServiceContract()> _
Public Interface IPortfolioServiceHardCollection
    <OperationContract()>
    Function GetHardCollectionByCode(code As String, audit As AuditMessage) As Domain.Entities.HardCollection
    <OperationContract()>
    Function SaveHardCollection(HardCollection As Domain.Entities.HardCollection, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.HardCollection)
    <OperationContract()> _
    Function GetHardCollectionDetailByHardCollectionId(hardCollectionId As Integer) As List(Of HardCollectionDetail)
    <OperationContract()> _
    Function SetCopyPasteOrImportFileHardCollection(dataImportFile As List(Of ImportFileRow), dataCopyPaste As List(Of List(Of String))) As ActionResult(Of List(Of HardCollectionDetail))
End Interface
