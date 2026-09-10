'***********************************************************************
' Assembly         : DistributedServices.PortfolioService
' Author           : Hector Rodriguez Rubiano
' Created          : 09-08-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Portfolio
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

Partial Public Class PortfolioService

    ''' <summary>
    ''' Funcion para eliminar un estado de demanda
    ''' </summary>
    ''' <param name="DemandStatus"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function DeleteDemandStatus(DemandStatus As Domain.Entities.DemandStatus, audit As AuditMessage) As ActionResult Implements IPortfolioServiceDemandStatus.DeleteDemandStatus
        Using service As IPortfolioDemandStatusAdminService = Container.Current.Resolve(Of IPortfolioDemandStatusAdminService)()
            Return service.DeleteDemandStatus(DemandStatus, audit)
        End Using
        'Return _DemandStatusAdminService.DeleteDemandStatus(DemandStatus, audit)
    End Function


    ''' <summary>
    ''' Función que obtiene un estado de demanda por Código
    ''' </summary>
    ''' <param name="Code">Código del estado de demanda</param>
    ''' <returns>Trademark</returns>
    ''' <remarks></remarks>
    Public Function GetDemandStatusByCode(Code As String) As Domain.Entities.DemandStatus Implements IPortfolioServiceDemandStatus.GetDemandStatusByCode
        Using service As IPortfolioDemandStatusAdminService = Container.Current.Resolve(Of IPortfolioDemandStatusAdminService)()
            Return service.GetDemandStatus(Code)
        End Using
    End Function

    ''' <summary>
    ''' Función que obtiene todos los estados de demanda
    ''' </summary>
    ''' <returns>Lista de Marcas</returns>
    ''' <remarks></remarks>
    Public Function ListAllDemandStatus(audit As AuditMessage) As List(Of Domain.Entities.DemandStatus) Implements IPortfolioServiceDemandStatus.ListAllDemandStatus
        Using service As IPortfolioDemandStatusAdminService = Container.Current.Resolve(Of IPortfolioDemandStatusAdminService)()
            Return service.ListAllDemandStatus()
        End Using
    End Function

    ''' <summary>
    ''' Función para almacenar un estado de demanda
    ''' </summary>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveDemandStatus(DemandStatus As Domain.Entities.DemandStatus, idSequense As Int64, audit As AuditMessage) As ActionResult(Of DemandStatus) Implements IPortfolioServiceDemandStatus.SaveDemandStatus
        Using service As IPortfolioDemandStatusAdminService = Container.Current.Resolve(Of IPortfolioDemandStatusAdminService)()
            Return service.SaveDemandStatus(DemandStatus, audit, idSequense)
        End Using
    End Function
    ''' <summary>
    ''' Funcion para actualizar el estado de un estado de demanda
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <param name="State"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function ChangeDemandStatus(Code As String, State As Boolean, audit As AuditMessage) As ActionResult(Of DemandStatus) Implements IPortfolioServiceDemandStatus.ChangeDemandStatus
        Using service As IPortfolioDemandStatusAdminService = Container.Current.Resolve(Of IPortfolioDemandStatusAdminService)()
            Return service.DemandStatusChangeState(Code, State, audit)
        End Using
    End Function

End Class
