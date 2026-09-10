'***********************************************************************
' Assembly         : Domain.Portfolio
' Author           : Carlos Ernesto Cordoba
' Created          : 03-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Base
Imports Domain.Entities
#End Region

Public Interface IPortfolioNoteConceptRepository
    Inherits IRepository(Of PortfolioNoteConcept)

    ''' <summary>
    ''' metodo para obtener un concepto de nota
    ''' </summary>
    ''' <param name="code">codigo</param>
    ''' <returns></returns>
    Function GetPortfolioNoteConcept(ByVal code As String, Optional tracking As Boolean = True) As PortfolioNoteConcept
    ''' <summary>
    ''' metodo para obtener un concepto de nota por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPortfolioNoteConceptById(id As Integer) As PortfolioNoteConcept

    ''' <summary>
    ''' metodo para obtener un concepto de nota por filtro
    ''' </summary>
    ''' <param name="noteType"></param>
    ''' <param name="idAccout"></param>
    ''' <param name="status"></param>
    ''' <returns></returns>
    Function GetPortfolioNoteConceptByFilter(noteType As Integer, idAccout As Integer, status As Integer) As PortfolioNoteConcept
End Interface