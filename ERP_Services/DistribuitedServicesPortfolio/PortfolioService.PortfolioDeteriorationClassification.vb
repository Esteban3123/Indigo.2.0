'***********************************************************************
' Assembly         : DistributedServices.Portfolio
' Author           : Oscar Astudillo Reyes
' Created          : 2024-11-10
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
    ''' Obtiene la clasificación de deterioro de cartera por código.
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit">.</param>
    ''' <returns>.</returns>
    Public Function GetPortfolioDeteriorationClassificationByCode(code As String, audit As AuditMessage) As ActionResult(Of PortfolioDeteriorationClassification) Implements IPortfolioDeteriorationClassification.GetPortfolioDeteriorationClassificationByCode
        Using service As IPortfolioDeteriorationClassificationAdminService = Container.Current.Resolve(Of IPortfolioDeteriorationClassificationAdminService)()
            Return service.GetPortfolioDeteriorationClassificationByCode(code, audit)
        End Using
    End Function

    ''' <summary>
    ''' Guarda una clasificación de deterioro de cartera.
    ''' </summary>
    ''' <param name="portfolioDeteriorationClassification"></param>
    ''' <param name="idSequence"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function SavePortfolioDeteriorationClassification(portfolioDeteriorationClassification As PortfolioDeteriorationClassification, idSequence As Integer, audit As AuditMessage) As ActionResult(Of PortfolioDeteriorationClassification) Implements IPortfolioDeteriorationClassification.SavePortfolioDeteriorationClassification
        Using service As IPortfolioDeteriorationClassificationAdminService = Container.Current.Resolve(Of IPortfolioDeteriorationClassificationAdminService)()
            Return service.SavePortfolioDeteriorationClassification(portfolioDeteriorationClassification, audit, idSequence)
        End Using
    End Function

    ''' <summary>
    ''' Elimina una clasificación de deterioro de cartera.
    ''' </summary>
    ''' <param name="portfolioDeteriorationClassification"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function DeletePortfolioDeteriorationClassification(portfolioDeteriorationClassification As PortfolioDeteriorationClassification, audit As AuditMessage) As ActionResult Implements IPortfolioDeteriorationClassification.DeletePortfolioDeteriorationClassification
        Using service As IPortfolioDeteriorationClassificationAdminService = Container.Current.Resolve(Of IPortfolioDeteriorationClassificationAdminService)()
            Return service.DeletePortfolioDeteriorationClassification(portfolioDeteriorationClassification, audit)
        End Using
    End Function

    ''' <summary>
    ''' Cambia el estado de una clasificación de deterioro de cartera.
    ''' </summary>
    ''' <param name="portfolioDeteriorationClassification"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function ChangeStatePortfolioDeteriorationClassification(portfolioDeteriorationClassification As PortfolioDeteriorationClassification, state As Boolean, audit As AuditMessage) As ActionResult(Of PortfolioDeteriorationClassification) Implements IPortfolioDeteriorationClassification.ChangeStatePortfolioDeteriorationClassification
        Using service As IPortfolioDeteriorationClassificationAdminService = Container.Current.Resolve(Of IPortfolioDeteriorationClassificationAdminService)()
            Return service.ChangeStatePortfolioDeteriorationClassification(portfolioDeteriorationClassification, state, audit)
        End Using
    End Function

End Class
