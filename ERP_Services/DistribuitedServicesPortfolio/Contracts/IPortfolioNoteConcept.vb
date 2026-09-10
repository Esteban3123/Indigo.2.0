'***********************************************************************
' Assembly         : DistributedServices.Portfolio
' Author           : Carlos Ernesto Cordoba
' Created          : 03-04-2014
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
Public Interface IPortfolioNoteConcept

#Region "Methods"
    ''' <summary>
    ''' guardar un concepto de nota
    ''' </summary>
    ''' <param name="portfolioNoteConcept">The portfolio note concept.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SavePortfolioNoteConcept(portfolioNoteConcept As PortfolioNoteConcept, idSequense As Int64, audit As AuditMessage) As ActionResult(Of PortfolioNoteConcept)
    ''' <summary>
    ''' eliminar un conepto de nota.
    ''' </summary>
    ''' <param name="portfolioNoteConcept">The portfolio note concept.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function DeletePortfolioNoteConcept(portfolioNoteConcept As PortfolioNoteConcept, audit As AuditMessage) As ActionResult
    ''' <summary>
    ''' metodo para obtener un concepto de nota
    ''' </summary>
    ''' <param name="code">codigo</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetPortfolioNoteConcept(code As String, audit As AuditMessage) As PortfolioNoteConcept

    ''' <summary>
    ''' metodo para obtener un concepto de nota
    ''' </summary>
    ''' <param name="code">codigo</param>
    ''' <returns></returns>
    <OperationContract()> _
    Function GetPortfolioNoteConceptById(ByVal id As Integer) As PortfolioNoteConcept

    ''' <summary>
    ''' Metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    <OperationContract()>
    Function ChangeStatePortfolioNoteConcept(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.PortfolioNoteConcept)
#End Region

End Interface
