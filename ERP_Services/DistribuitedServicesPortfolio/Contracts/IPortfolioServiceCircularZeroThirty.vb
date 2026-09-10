'***********************************************************************
' Assembly         : DistributedServices.Portfolio
' Author           : Faiber Julian Mora D.
' Created          : 07-10-2016
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
Public Interface IPortfolioServiceCircularZeroThirty

    <OperationContract()>
    Function ListTrimesters() As Dictionary(Of Int32, String)
    <OperationContract()>
    Function GenerateDocument030(year As Integer, trimester As Integer, audit As AuditMessage) As ActionResult(Of List(Of GenerateDocument030_Result))

End Interface
