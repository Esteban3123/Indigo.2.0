'***********************************************************************
' Assembly         : DistributedServices.Portfolio
' Author           : Carlos Ernesto Cordoba
' Created          : 04-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.IOC
Imports Application.Portfolio
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Microsoft.Practices.Unity
Imports Domain.Portfolio.Model

#End Region

Partial Class PortfolioService

    Public Function CopyAndPastePortfolioProvision(Data As List(Of List(Of String)), CourtDate As Date, Process As Integer, OperatingUnitId As Integer, applyDeterioration As Byte, Percentage As Decimal, Expectative As Integer) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.PortfolioProvisionDetail), List(Of Tuple(Of String, Integer))) Implements IPortfolioServicePortfolioProvision.CopyAndPastePortfolioProvision
        Using service As IPortfolioProvisionAdminService = Container.Current.Resolve(Of IPortfolioProvisionAdminService)()
            Return service.CopyAndPastePortfolioProvision(Data, CourtDate, Process, OperatingUnitId, applyDeterioration, Percentage, Expectative)
        End Using
    End Function

    Public Function GetPortfolioProvision(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.PortfolioProvision) Implements IPortfolioServicePortfolioProvision.GetPortfolioProvision
        Using service As IPortfolioProvisionAdminService = Container.Current.Resolve(Of IPortfolioProvisionAdminService)()
            Return service.GetPortfolioProvision(code, audit)
        End Using
    End Function

    Public Function GetPortfolioProvisionById(Id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.PortfolioProvision) Implements IPortfolioServicePortfolioProvision.GetPortfolioProvisionById
        Using service As IPortfolioProvisionAdminService = Container.Current.Resolve(Of IPortfolioProvisionAdminService)()
            Return service.GetPortfolioProvisionById(Id, audit)
        End Using
    End Function

    Public Function SavePortfolioProvision(PortfolioProvision As Domain.Entities.PortfolioProvision, listPortfolioProvisionDetailDelete As List(Of Integer), audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.PortfolioProvision) Implements IPortfolioServicePortfolioProvision.SavePortfolioProvision
        Using service As IPortfolioProvisionAdminService = Container.Current.Resolve(Of IPortfolioProvisionAdminService)()
            Return service.SavePortfolioProvision(PortfolioProvision, listPortfolioProvisionDetailDelete, audit)
        End Using
    End Function

    Public Function AnnularPortfolioProvision(PortfolioProvision As Domain.Entities.PortfolioProvision, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IPortfolioServicePortfolioProvision.AnnularPortfolioProvision
        Using service As IPortfolioProvisionAdminService = Container.Current.Resolve(Of IPortfolioProvisionAdminService)()
            Return service.AnnularPortfolioProvision(PortfolioProvision, audit)
        End Using
    End Function

    Public Function ConfirmPortfolioProvision(PortfolioProvision As Domain.Entities.PortfolioProvision, listPortfolioProvisionDetailDelete As List(Of Integer), audit As AuditMessage, Optional operativeUnitId As Integer? = Nothing) As ActionResult(Of PortfolioProvision) Implements IPortfolioServicePortfolioProvision.ConfirmPortfolioProvision
        Using service As IPortfolioProvisionAdminService = Container.Current.Resolve(Of IPortfolioProvisionAdminService)()
            Return service.SaveAndConfirmPortfolioProvision(PortfolioProvision, listPortfolioProvisionDetailDelete, audit, operativeUnitId)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene la información del Deterioro de Cartera por Clasificación
    ''' </summary>
    ''' <param name="closingDate"></param>
    ''' <param name="operativeUnitId"></param>
    ''' <param name="pageNumber"></param>
    ''' <param name="pageSize"></param>
    ''' <returns></returns>
    Public Function GetPortfolioDeteriorationByClassification(closingDate As Date, operativeUnitId As Integer, Optional pageNumber As Integer = 1, Optional pageSize As Integer = 50000) As ActionResult(Of List(Of PortfolioDeteriorationByClassificationDTO)) Implements IPortfolioServicePortfolioProvision.GetPortfolioDeteriorationByClassification
        Using service As IPortfolioProvisionAdminService = Container.Current.Resolve(Of IPortfolioProvisionAdminService)()
            Return service.GetPortfolioDeteriorationByClassification(closingDate, operativeUnitId, pageNumber, pageSize)
        End Using
    End Function

End Class
