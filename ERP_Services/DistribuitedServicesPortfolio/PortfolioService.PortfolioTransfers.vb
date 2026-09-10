'***********************************************************************
' Assembly         : DistributedServices.Portfolio
' Author           : Carlos Ernesto Cordoba
' Created          : 01-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Application.Portfolio
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

#End Region

Partial Class PortfolioService

    ''' <summary>
    ''' metodo para pegar en la rejilla de traslado
    ''' </summary>
    ''' <param name="data"></param>
    ''' <param name="PortfolioTransfer"></param>
    ''' <returns></returns>
    Public Function SetBillsTransfersCopyPaste(data As List(Of List(Of String)), PortfolioTransfer As Domain.Entities.PortfolioTransfer, CompanyType As Integer, OperatingUnitId As Integer) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.PortfolioTransferDetail)) Implements IPortfolioServicePortfolioTransfers.SetBillsTransfersCopyPaste
        Using service As IPortfolioTransfersAdminService = Container.Current.Resolve(Of IPortfolioTransfersAdminService)()
            Return service.SetBillsTransfersCopyPaste(data, PortfolioTransfer, CompanyType, OperatingUnitId)
        End Using
    End Function

    ''' <summary>
    ''' obtiene un traslado por id
    ''' </summary>
    ''' <param name="idPortfolioTransfer"></param>
    ''' <returns></returns>
    Public Function GetPortfolioTransferById(idPortfolioTransfer As Integer) As Domain.Entities.PortfolioTransfer Implements IPortfolioServicePortfolioTransfers.GetPortfolioTransferById
        Using service As IPortfolioTransfersAdminService = Container.Current.Resolve(Of IPortfolioTransfersAdminService)()
            Return service.GetPortfolioTransferById(idPortfolioTransfer)
        End Using
    End Function

    ''' <summary>
    ''' obtiene un traslado por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetPortfolioTransfersByCode(code As String, audit As AuditMessage) As Domain.Entities.PortfolioTransfer Implements IPortfolioServicePortfolioTransfers.GetPortfolioTransfersByCode
        Using service As IPortfolioTransfersAdminService = Container.Current.Resolve(Of IPortfolioTransfersAdminService)()
            Return service.GetPortfolioTransfersByCode(code, audit)
        End Using
    End Function

    ''' <summary>
    ''' obtiene los detalles del traslado
    ''' </summary>
    ''' <param name="idPortfolioTransfer"></param>
    ''' <returns></returns>
    Public Function GatPortfolioTransferDetailByIdPortfolioTransfer(idPortfolioTransfer As Integer) As List(Of Domain.Entities.PortfolioTransferDetail) Implements IPortfolioServicePortfolioTransfers.GatPortfolioTransferDetailByIdPortfolioTransfer
        Using service As IPortfolioTransfersAdminService = Container.Current.Resolve(Of IPortfolioTransfersAdminService)()
            Return service.GatPortfolioTransferDetailByIdPortfolioTransfer(idPortfolioTransfer)
        End Using
    End Function

    ''' <summary>
    ''' lista los otros conceptos de traslados
    ''' </summary>
    ''' <param name="idPortfolioTransfer"></param>
    ''' <returns></returns>
    Public Function GetPortfolioTransferOtherConceptByIdPortfolioTransfer(idPortfolioTransfer As Integer) As List(Of Domain.Entities.PortfolioTransferOtherConcept) Implements IPortfolioServicePortfolioTransfers.GetPortfolioTransferOtherConceptByIdPortfolioTransfer
        Using service As IPortfolioTransfersAdminService = Container.Current.Resolve(Of IPortfolioTransfersAdminService)()
            Return service.GetPortfolioTransferOtherConceptByIdPortfolioTransfer(idPortfolioTransfer)
        End Using
    End Function

    ''' <summary>
    ''' guardar un traslado
    ''' </summary>
    ''' <param name="PortfolioTransfer"></param>
    ''' <returns></returns>
    Public Function SavePortfolioTransfer(PortfolioTransfer As Domain.Entities.PortfolioTransfer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.PortfolioTransfer) Implements IPortfolioServicePortfolioTransfers.SavePortfolioTransfer
        Using service As IPortfolioTransfersAdminService = Container.Current.Resolve(Of IPortfolioTransfersAdminService)()
            Return service.SavePortfolioTransfer(PortfolioTransfer, audit)
        End Using
    End Function

End Class
