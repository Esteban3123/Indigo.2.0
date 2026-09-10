'************************************************************
' Assembly         : Domain.Billing
' Author           : Miguel Angel Fonseca Castro
' Created          : 2018-10-17
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Base

#End Region

Public Interface IBillingNoteRepository
    Inherits IRepository(Of BillingNote)

    ''' <summary>
    ''' Obtiene un documento electronico usado para la facturación electronica por el id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Function GetBillingNoteById(ByVal Id As Integer, Optional tracking As Boolean = True) As BillingNote

    ''' <summary>
    ''' Obtiene un documento electronico usado para la facturación electronica por el id con agregados
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Function GetBillingNoteByIdWithAggregates(ByVal Id As Integer, Optional xmlDianGenerate As Boolean = False) As BillingNote

    ''' <summary>
    ''' Obtiene el ConceptId de PortfolioNoteAccountReceivableAdvance para el DiscrepancyResponse
    ''' </summary>
    ''' <param name="billingNoteId">Id de la BillingNote</param>
    ''' <returns>ConceptId o Nothing si no existe</returns>
    Function GetDiscrepancyConceptIdByBillingNoteId(ByVal billingNoteId As Integer) As Integer?

End Interface
