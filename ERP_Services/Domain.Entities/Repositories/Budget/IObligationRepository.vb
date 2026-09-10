'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Carlos Ernesto Cordoba
' Created          : 19-08-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports Domain.Base
Imports Domain.Entities
Imports Domain.Base.Entities

#End Region

Public Interface IObligationRepository
    Inherits IRepository(Of Obligation)

    ''' <summary>
    ''' obtiene una obligacion por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetObligationByCode(code As String, BudgetaryValidityId As Integer) As Obligation

    ''' <summary>
    ''' obtiene una obligacion por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetObligationById(id As Integer, Optional flagTracking As Boolean = True) As Obligation

    ''' <summary>
    ''' Guarda la obligacion
    ''' </summary>
    ''' <param name="ObligationXml"></param>
    ''' <param name="ObligationDetailForDeleteXml"></param>
    ''' <param name="CodeUser"></param>
    ''' <returns></returns>
    Function SP_SaveObligation(ObligationXml As String, ObligationDetailForDeleteXml As String, CodeUser As String) As SP_SaveObligation_Result

End Interface
