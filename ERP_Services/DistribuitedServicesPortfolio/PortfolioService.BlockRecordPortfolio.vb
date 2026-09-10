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

#Region "Methods"
    ''' <summary>
    ''' Elimina una registro bloqueado
    ''' </summary>
    ''' <param name="blockRecordPortfolio"></param>
    ''' <returns>
    ''' ActionResult
    ''' </returns>
    Public Function DeleteBlockRecordPortfolio(blockRecordPortfolio As BlockRecordPortfolio) As ActionResult Implements IPortfolioBlockRecordPortfolio.DeleteBlockRecordPortfolio
        Using service As IBlockRecordPortfolioAdminService = Container.Current.Resolve(Of IBlockRecordPortfolioAdminService)()
            Return service.DeleteBlockRecordPortfolio(blockRecordPortfolio)
        End Using
        'Return Me._blockRecordPortfolioAdminService.DeleteBlockRecordPortfolio(blockRecordPortfolio)
    End Function

    ''' <summary>
    ''' Gets the block record portfolio by idform and identifier record.
    ''' </summary>
    ''' <param name="IdForm">The identifier form.</param>
    ''' <param name="IdRecord">The identifier record.</param>
    ''' <returns></returns>
    Public Function GetBlockRecordPortfolioByIdformAndIdRecord(IdForm As String, IdRecord As String) As BlockRecordPortfolio Implements IPortfolioBlockRecordPortfolio.GetBlockRecordPortfolioByIdformAndIdRecord
        Using service As IBlockRecordPortfolioAdminService = Container.Current.Resolve(Of IBlockRecordPortfolioAdminService)()
            Return service.GetBlockRecordPortfolioByIdformAndIdRecord(IdForm, IdRecord)
        End Using
        'Return Me._blockRecordPortfolioAdminService.GetBlockRecordPortfolioByIdformAndIdRecord(IdForm, IdRecord)
    End Function

    ''' <summary>
    ''' Almacena o Actualiza registro bloqueado
    ''' </summary>
    ''' <param name="blockRecordPortfolio"></param>
    ''' <returns>
    ''' ActionResult
    ''' </returns>
    Public Function SaveBlockRecordPortfolio(blockRecordPortfolio As BlockRecordPortfolio) As ActionResult(Of BlockRecordPortfolio) Implements IPortfolioBlockRecordPortfolio.SaveBlockRecordPortfolio
        Using service As IBlockRecordPortfolioAdminService = Container.Current.Resolve(Of IBlockRecordPortfolioAdminService)()
            Return service.SaveBlockRecordPortfolio(blockRecordPortfolio)
        End Using
        'Return Me._blockRecordPortfolioAdminService.SaveBlockRecordPortfolio(blockRecordPortfolio)
    End Function
#End Region

End Class
