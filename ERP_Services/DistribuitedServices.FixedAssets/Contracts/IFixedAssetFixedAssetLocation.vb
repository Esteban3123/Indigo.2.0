'***********************************************************************
' Assembly         : DistributedServices.FixedAsset
' Author           : Jeisson Herrera Peña
' Created          : 25/09/2015
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
Public Interface IFixedAssetFixedAssetLocation

    <OperationContract()>
    Function ListAllLocation(Empresa As String, audit As AuditMessage) As List(Of FixedAssetLocation)


    <OperationContract()> _
    Function DeleteLocation(Empresa As String, ByVal Location As FixedAssetLocation, ByVal audit As AuditMessage) As Boolean


    <OperationContract()>
    Function SaveLocation(Empresa As String, ByVal Location As List(Of FixedAssetLocation), ByVal audit As AuditMessage, idSequense As Int64) As Boolean

    <OperationContract()>
    Function GetLocation(Empresa As String, ByVal codeLocation As String, audit As AuditMessage) As Domain.Entities.FixedAssetLocation


    Function ListLocation(Empresa As String, audit As AuditMessage) As List(Of FixedAssetLocation)

End Interface
