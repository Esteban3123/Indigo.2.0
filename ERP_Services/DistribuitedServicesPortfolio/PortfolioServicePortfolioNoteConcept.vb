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
    ''' eliminar un conepto de nota.
    ''' </summary>
    ''' <param name="portfolioNoteConcept">The portfolio note concept.</param>
    ''' <returns></returns>
    Public Function DeletePortfolioNoteConcept(portfolioNoteConcept As PortfolioNoteConcept, audit As AuditMessage) As ActionResult Implements IPortfolioNoteConcept.DeletePortfolioNoteConcept
        Using service As IPortfolioNoteConceptAdminService = Container.Current.Resolve(Of IPortfolioNoteConceptAdminService)()
            Return service.DeletePortfolioNoteConcept(portfolioNoteConcept, audit)
        End Using
        'Return Me._portfolioNoteConceptAdminService.DeletePortfolioNoteConcept(portfolioNoteConcept, audit)
    End Function

    ''' <summary>
    ''' metodo para obtener un concepto de nota
    ''' </summary>
    ''' <param name="code">codigo</param>
    ''' <returns></returns>
    Public Function GetPortfolioNoteConcept(code As String, audit As AuditMessage) As PortfolioNoteConcept Implements IPortfolioNoteConcept.GetPortfolioNoteConcept
        Using service As IPortfolioNoteConceptAdminService = Container.Current.Resolve(Of IPortfolioNoteConceptAdminService)()
            Return service.GetPortfolioNoteConcept(code, audit)
        End Using
        'Return Me._portfolioNoteConceptAdminService.GetPortfolioNoteConcept(code, audit)
    End Function

    ''' <summary>
    ''' metodo para obtener un concepto de nota
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetPortfolioNoteConceptById(id As Integer) As Domain.Entities.PortfolioNoteConcept Implements IPortfolioNoteConcept.GetPortfolioNoteConceptById
        Using service As IPortfolioNoteConceptAdminService = Container.Current.Resolve(Of IPortfolioNoteConceptAdminService)()
            Return service.GetPortfolioNoteConceptById(id)
        End Using
        'Return _portfolioNoteConceptAdminService.GetPortfolioNoteConceptById(id)
    End Function
    ''' <summary>
    ''' guardar un concepto de nota
    ''' </summary>
    ''' <param name="portfolioNoteConcept">The portfolio note concept.</param>
    ''' <returns></returns>
    Public Function SavePortfolioNoteConcept(portfolioNoteConcept As PortfolioNoteConcept, idSequense As Int64, audit As AuditMessage) As ActionResult(Of PortfolioNoteConcept) Implements IPortfolioNoteConcept.SavePortfolioNoteConcept
        Using service As IPortfolioNoteConceptAdminService = Container.Current.Resolve(Of IPortfolioNoteConceptAdminService)()
            Return service.SavePortfolioNoteConcept(portfolioNoteConcept, audit, idSequense)
        End Using
        'Return Me._portfolioNoteConceptAdminService.SavePortfolioNoteConcept(portfolioNoteConcept, audit, idSequense)
    End Function

    ''' <summary>
    ''' Metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    Public Function ChangeStatePortfolioNoteConcept(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.PortfolioNoteConcept) Implements IPortfolioNoteConcept.ChangeStatePortfolioNoteConcept
        Using service As IPortfolioNoteConceptAdminService = Container.Current.Resolve(Of IPortfolioNoteConceptAdminService)()
            Return service.ChangeStatePortfolioNoteConcept(code, state, audit)
        End Using
        'Return Me._portfolioNoteConceptAdminService.ChangeStatePortfolioNoteConcept(code, state, audit)
    End Function
#End Region

End Class
