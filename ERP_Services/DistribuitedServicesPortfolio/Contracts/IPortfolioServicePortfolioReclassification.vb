'***********************************************************************
' Assembly         : DistributedServices.Portfolio
' Author           : Juan Carlos Bermudez
' Created          : 29-07-2015
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
Public Interface IPortfolioServicePortfolioReclassification

    ''' <summary>
    ''' obtener una reclasificación de documento por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetReclassificationByCode(code As String, audit As AuditMessage) As Domain.Entities.PortfolioReclassification

End Interface
