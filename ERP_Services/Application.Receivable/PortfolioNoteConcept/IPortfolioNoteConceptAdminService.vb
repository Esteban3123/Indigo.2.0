'***********************************************************************
' Assembly         : Application.Portfolio
' Author           : Carlos Ernesto Cordoba
' Created          : 03-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region

Public Interface IPortfolioNoteConceptAdminService
    Inherits IDisposable

#Region "Methods"
    ''' <summary>
    ''' guardar un concepto de nota
    ''' </summary>
    ''' <param name="portfolioNoteConcept">The portfolio note concept.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SavePortfolioNoteConcept(ByVal portfolioNoteConcept As PortfolioNoteConcept, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of PortfolioNoteConcept)
    ''' <summary>
    ''' eliminar un conepto de nota.
    ''' </summary>
    ''' <param name="portfolioNoteConcept">The portfolio note concept.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeletePortfolioNoteConcept(ByVal portfolioNoteConcept As PortfolioNoteConcept, ByVal audit As AuditMessage) As ActionResult
    ''' <summary>
    ''' metodo para obtener un concepto de nota
    ''' </summary>
    ''' <param name="code">codigo</param>
    ''' <returns></returns>
    Function GetPortfolioNoteConcept(ByVal code As String, ByVal audit As AuditMessage) As PortfolioNoteConcept
    ''' <summary>
    ''' metodo para obtener un concepto de nota por id
    ''' </summary>
    ''' <param name="id">id</param>
    ''' <returns></returns>
    Function GetPortfolioNoteConceptById(id As Integer) As PortfolioNoteConcept
    ''' <summary>
    ''' Metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    Function ChangeStatePortfolioNoteConcept(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of PortfolioNoteConcept)
#End Region
End Interface
