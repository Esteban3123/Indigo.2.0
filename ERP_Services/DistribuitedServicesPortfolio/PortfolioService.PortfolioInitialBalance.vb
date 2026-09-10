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
#End Region

Partial Class PortfolioService
    ''' <summary>
    ''' confirmar el saldo incial
    ''' </summary>
    ''' <param name="idPortfolioInitialBalance"></param>
    ''' <returns></returns>
    Public Function ConfirmPortfolioInitialBalance(idPortfolioInitialBalance As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of String) Implements IPortfolioServicePortfolioInitialBalance.ConfirmPortfolioInitialBalance
        Using service As IPortfolioInitialBalanceAdminService = Container.Current.Resolve(Of IPortfolioInitialBalanceAdminService)()
            Return service.ConfirmPortfolioInitialBalance(idPortfolioInitialBalance, audit)
        End Using
        'Return _portfolioInitialBalanceAdminService.ConfirmPortfolioInitialBalance(idPortfolioInitialBalance, audit)
    End Function

    ''' <summary>
    ''' metodo para obtener las facturas del saldo inicial por id del saldo inicial
    ''' </summary>
    ''' <param name="idPortfolioInitialBalance"></param>
    ''' <returns></returns>
    Public Function GetPortfolioInitialBalanceAccountReceivableByIdPortfolioInitialBalance(idPortfolioInitialBalance As Integer) As List(Of Domain.Entities.PortfolioInitialBalanceAccountReceivable) Implements IPortfolioServicePortfolioInitialBalance.GetPortfolioInitialBalanceAccountReceivableByIdPortfolioInitialBalance
        Using service As IPortfolioInitialBalanceAdminService = Container.Current.Resolve(Of IPortfolioInitialBalanceAdminService)()
            Return service.GetPortfolioInitialBalanceAccountReceivableByIdPortfolioInitialBalance(idPortfolioInitialBalance)
        End Using
        'Return _portfolioInitialBalanceAdminService.GetPortfolioInitialBalanceAccountReceivableByIdPortfolioInitialBalance(idPortfolioInitialBalance)
    End Function

    ''' <summary>
    ''' metodo para obtener los anticipos del saldo inicial por id del saldo inicial
    ''' </summary>
    ''' <param name="idPortfolioInitialBalance"></param>
    ''' <returns></returns>
    Public Function GetPortfolioInitialBalanceAdvanceByIdPortfolioInitialBalance(idPortfolioInitialBalance As Integer) As List(Of Domain.Entities.PortfolioInitialBalanceAdvance) Implements IPortfolioServicePortfolioInitialBalance.GetPortfolioInitialBalanceAdvanceByIdPortfolioInitialBalance
        Using service As IPortfolioInitialBalanceAdminService = Container.Current.Resolve(Of IPortfolioInitialBalanceAdminService)()
            Return service.GetPortfolioInitialBalanceAdvanceByIdPortfolioInitialBalance(idPortfolioInitialBalance)
        End Using
        'Return _portfolioInitialBalanceAdminService.GetPortfolioInitialBalanceAdvanceByIdPortfolioInitialBalance(idPortfolioInitialBalance)
    End Function

    ''' <summary>
    ''' metodo para obtener el saldo inicial por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetPortfolioInitialBalanceByCode(code As String, audit As AuditMessage) As Domain.Entities.PortfolioInitialBalance Implements IPortfolioServicePortfolioInitialBalance.GetPortfolioInitialBalanceByCode
        Using service As IPortfolioInitialBalanceAdminService = Container.Current.Resolve(Of IPortfolioInitialBalanceAdminService)()
            Return service.GetPortfolioInitialBalanceByCode(code, audit)
        End Using
        'Return _portfolioInitialBalanceAdminService.GetPortfolioInitialBalanceByCode(code, audit)
    End Function

    ''' <summary>
    ''' metodo para obtener el saldo inicial por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetPortfolioInitialBalanceById(id As Integer) As Domain.Entities.PortfolioInitialBalance Implements IPortfolioServicePortfolioInitialBalance.GetPortfolioInitialBalanceById
        Using service As IPortfolioInitialBalanceAdminService = Container.Current.Resolve(Of IPortfolioInitialBalanceAdminService)()
            Return service.GetPortfolioInitialBalanceById(id)
        End Using
        'Return _portfolioInitialBalanceAdminService.GetPortfolioInitialBalanceById(id)
    End Function

    ''' <summary>
    ''' metodo para guardar y confirmar el saldo inicial
    ''' </summary>
    ''' <param name="PortfolioInitialBalance"></param>
    ''' <returns></returns>
    Public Function SaveAndConfirmPortfolioInitialBalance(PortfolioInitialBalance As Domain.Entities.PortfolioInitialBalance, idSequence As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of String) Implements IPortfolioServicePortfolioInitialBalance.SaveAndConfirmPortfolioInitialBalance
        Using service As IPortfolioInitialBalanceAdminService = Container.Current.Resolve(Of IPortfolioInitialBalanceAdminService)()
            Return service.SaveAndConfirmPortfolioInitialBalance(PortfolioInitialBalance, audit, idSequence)
        End Using
        'Return _portfolioInitialBalanceAdminService.SaveAndConfirmPortfolioInitialBalance(PortfolioInitialBalance, audit, idSequence)
    End Function

    ''' <summary>
    ''' guardar saldo inicial
    ''' </summary>
    ''' <param name="PortfolioInitialBalance"></param>
    ''' <returns></returns>
    Public Function SavePortfolioInitialBalance(PortfolioInitialBalance As Domain.Entities.PortfolioInitialBalance, idSequence As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.PortfolioInitialBalance) Implements IPortfolioServicePortfolioInitialBalance.SavePortfolioInitialBalance
        Using service As IPortfolioInitialBalanceAdminService = Container.Current.Resolve(Of IPortfolioInitialBalanceAdminService)()
            Return service.SavePortfolioInitialBalance(PortfolioInitialBalance, audit, idSequence)
        End Using
        'Return _portfolioInitialBalanceAdminService.SavePortfolioInitialBalance(PortfolioInitialBalance, audit, idSequence)
    End Function

    ''' <summary>
    ''' metodo para establecer los anticipos en la rejilla
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    Public Function SetAdvanceCopyPaste(data As List(Of List(Of String))) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.PortfolioInitialBalanceAdvance)) Implements IPortfolioServicePortfolioInitialBalance.SetAdvanceCopyPaste
        Using service As IPortfolioInitialBalanceAdminService = Container.Current.Resolve(Of IPortfolioInitialBalanceAdminService)()
            Return service.SetAdvanceCopyPaste(data)
        End Using
        'Return _portfolioInitialBalanceAdminService.SetAdvanceCopyPaste(data)
    End Function

    ''' <summary>
    ''' metodo para establecer las facturas que se pegaron en la rejilla
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    Public Function SetBillsCopyPaste(data As List(Of List(Of String)), companyType As Integer) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.PortfolioInitialBalanceAccountReceivable)) Implements IPortfolioServicePortfolioInitialBalance.SetBillsCopyPaste
        Using service As IPortfolioInitialBalanceAdminService = Container.Current.Resolve(Of IPortfolioInitialBalanceAdminService)()
            Return service.SetBillsCopyPaste(data, companyType)
        End Using
        'Return _portfolioInitialBalanceAdminService.SetBillsCopyPaste(data, companyType)
    End Function

    ''' <summary>
    ''' metodo para validar la carga del archivo para los saldo iniciales
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    Public Function ValidateFileBillsInitialBalance(data As List(Of ImportFileRow), companyType As Integer) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.PortfolioInitialBalanceAccountReceivable)) Implements IPortfolioServicePortfolioInitialBalance.ValidateFileBillsInitialBalance
        Using service As IPortfolioInitialBalanceAdminService = Container.Current.Resolve(Of IPortfolioInitialBalanceAdminService)()
            Return service.ValidateFileBillsInitialBalance(data, companyType)
        End Using
        'Return _portfolioInitialBalanceAdminService.ValidateFileBillsInitialBalance(data, companyType)
    End Function
End Class
