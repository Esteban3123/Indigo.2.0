'***********************************************************************
' Assembly         : DistributedServices.Portfolio
' Author           : Carlos Mario Arias Rubiano
' Created          : 21/10/2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Domain.Portfolio.Model
#End Region

<ServiceContract()> _
Public Interface IPortfolioServicePortfolioProvision

    ''' <summary>
    ''' metodo para pegar en la rejilla de plantilla de procedimientos
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function CopyAndPastePortfolioProvision(Data As List(Of List(Of String)), CourtDate As Date, Process As Integer, OperatingUnitId As Integer, applyDeterioration As Byte, Percentage As Decimal, Expectative As Integer) As ActionResult(Of List(Of PortfolioProvisionDetail), List(Of Tuple(Of String, Integer)))

    ''' <summary>
    ''' Obtiene el registro por código
    ''' </summary>
    ''' <param name="code">codigo</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetPortfolioProvision(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.PortfolioProvision)

    ''' <summary>
    ''' Obtiene el registro por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetPortfolioProvisionById(Id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.PortfolioProvision)

    ''' <summary>
    ''' Guarda una provision
    ''' </summary>
    ''' <param name="PortfolioProvision"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SavePortfolioProvision(PortfolioProvision As Domain.Entities.PortfolioProvision, listPortfolioProvisionDetailDelete As List(Of Integer), audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.PortfolioProvision)

    ''' <summary>
    ''' Anula una provision
    ''' </summary>
    ''' <param name="PortfolioProvision"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function AnnularPortfolioProvision(PortfolioProvision As Domain.Entities.PortfolioProvision, audit As AuditMessage) As Domain.Base.Entities.ActionResult

    ''' <summary>
    ''' Confirma la provision y deterioro
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ConfirmPortfolioProvision(PortfolioProvision As Domain.Entities.PortfolioProvision, listPortfolioProvisionDetailDelete As List(Of Integer), audit As AuditMessage, Optional operativeUnitId As Integer? = Nothing) As ActionResult(Of PortfolioProvision)

    ''' <summary>
    ''' Obtiene la información del Deterioro de Cartera por Clasificación
    ''' </summary>
    ''' <param name="closingDate"></param>
    ''' <param name="operativeUnitId"></param>
    ''' <param name="pageNumber"></param>
    ''' <param name="pageSize"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetPortfolioDeteriorationByClassification(closingDate As DateTime, operativeUnitId As Integer, Optional pageNumber As Integer = 1, Optional pageSize As Integer = 50000) As ActionResult(Of List(Of PortfolioDeteriorationByClassificationDTO))

End Interface
