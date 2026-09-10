'***********************************************************************
' Assembly         : DistributedServices.Portfolio
' Author           : Carlos Ernesto Cordoba
' Created          : 01-04-2014
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
    ''' eliminar rango de provision
    ''' </summary>
    ''' <param name="provisionRanges">The provision ranges.</param>
    ''' <returns></returns>
    Public Function DeleteProvisionRanges(provisionRanges As ProvisionRanges, audit As AuditMessage) As ActionResult Implements IPortfolioProvisionRanges.DeleteProvisionRanges
        Using service As IProvisionRangesAdminService = Container.Current.Resolve(Of IProvisionRangesAdminService)()
            Return service.DeleteProvisionRanges(provisionRanges, audit)
        End Using
        'Return Me._provisionRangesAdminService.DeleteProvisionRanges(provisionRanges, audit)
    End Function

    ''' <summary>
    ''' obtener rango de provision por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Function GetProvisionRangesByCode(code As String, audit As AuditMessage) As Object Implements IPortfolioProvisionRanges.GetProvisionRangesByCode
        Using service As IProvisionRangesAdminService = Container.Current.Resolve(Of IProvisionRangesAdminService)()
            Return service.GetProvisionRangesByCode(code, audit)
        End Using
        'Return Me._provisionRangesAdminService.GetProvisionRangesByCode(code, audit)
    End Function

    ''' <summary>
    ''' guardar rango de provision
    ''' </summary>
    ''' <param name="provisionRanges">The provision ranges.</param>
    ''' <returns></returns>
    Public Function SaveProvisionRanges(provisionRanges As ProvisionRanges, idSequense As Int64, audit As AuditMessage) As ActionResult(Of ProvisionRanges) Implements IPortfolioProvisionRanges.SaveProvisionRanges
        Using service As IProvisionRangesAdminService = Container.Current.Resolve(Of IProvisionRangesAdminService)()
            Return service.SaveProvisionRanges(provisionRanges, audit, idSequense)
        End Using
        'Return Me._provisionRangesAdminService.SaveProvisionRanges(provisionRanges, audit, idSequense)
    End Function


    ''' <summary>
    ''' Metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    Public Function ChangeStateProvisionRanges(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ProvisionRanges) Implements IPortfolioProvisionRanges.ChangeStateProvisionRanges
        Using service As IProvisionRangesAdminService = Container.Current.Resolve(Of IProvisionRangesAdminService)()
            Return service.ChangeStateProvisionRanges(code, state, audit)
        End Using
        'Return Me._provisionRangesAdminService.ChangeStateProvisionRanges(code, state, audit)
    End Function
#End Region

End Class
