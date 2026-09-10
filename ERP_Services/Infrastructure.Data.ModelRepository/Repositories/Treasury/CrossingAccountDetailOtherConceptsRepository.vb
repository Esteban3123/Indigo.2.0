'***********************************************************************
' Assembly         : Infrastructure.Data.TreasuryRepository
' Author           : Diego Andrés Roldán Lozano
' Created          : 29-09-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class CrossingAccountDetailOtherConceptsRepository
    Inherits GenericRepository(Of CrossingAccountDetailOtherConcept)
    Implements ICrossingAccountDetailOtherConceptsRepository

    'Contexto global
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

#Region "Methods"
    Public Function GetCrossingAccountDetailOtherConceptById(Id As Integer, Optional tracking As Boolean = False) As CrossingAccountDetailOtherConcept Implements ICrossingAccountDetailOtherConceptsRepository.GetCrossingAccountDetailOtherConceptById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim query As CrossingAccountDetailOtherConcept = Nothing
        If tracking Then
            query = (From cad As CrossingAccountDetailOtherConcept In _context.CrossingAccountDetailOtherConcept
                     Where cad.Id = Id Select cad).FirstOrDefault()
        Else
            query = (From cad As CrossingAccountDetailOtherConcept In _context.CrossingAccountDetailOtherConcept.AsNoTracking()
                     Where cad.Id = Id Select cad).FirstOrDefault()
        End If
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From cad As CrossingAccountDetailOtherConcept In _context.CrossingAccountDetailOtherConcept.AsNoTracking()
                     Where cad.Id = Id Select cad).FirstOrDefault()
            Return query
        Else
            Return New CrossingAccountDetailOtherConcept()
        End If
    End Function

    Public Function ListCrossingAccountDetailOtherConceptByCrossingAccountId(crossingAccountId As Integer, Optional tracking As Boolean = False) As List(Of CrossingAccountDetailOtherConcept) Implements ICrossingAccountDetailOtherConceptsRepository.ListCrossingAccountDetailOtherConceptByCrossingAccountId
        If crossingAccountId = 0 Then
            Throw New ArgumentNullException("crossingAccountId")
        End If
        Dim query As List(Of CrossingAccountDetailOtherConcept) = Nothing
        If tracking Then
            Dim listCrossingCxC As List(Of CrossingAccountDetailOtherConcept) = (From cad As CrossingAccountDetailOtherConcept In _context.CrossingAccountDetailOtherConcept
                    Where cad.CrossingAccountId = crossingAccountId
                    Select cad).ToList()
            If listCrossingCxC IsNot Nothing AndAlso listCrossingCxC.Count > 0 Then
                For Each crossingConcept As CrossingAccountDetailOtherConcept In listCrossingCxC
                    Dim _mainAccountId As Integer = crossingConcept.MainAccountId
                    crossingConcept.MainAccountCodeName = (From ma In _context.MainAccounts
                                                          Where ma.Id = _mainAccountId
                                                          Select String.Concat(ma.Number, " - ", ma.Name)).FirstOrDefault()
                    crossingConcept.ConceptCodeName = (From c In _context.NoteConcepts.AsNoTracking() Where c.Id = crossingConcept.TreasuryNoteConceptId Select String.Concat(c.Code, " - ", c.Description)).FirstOrDefault()
                    If crossingConcept.ThirdPartyId IsNot Nothing Then
                        crossingConcept.ThirdPartyNitName = (From c In _context.ThirdParty.AsNoTracking() Where c.Id = crossingConcept.ThirdPartyId Select String.Concat(c.Nit, " - ", c.Name)).FirstOrDefault()
                    End If
                    If crossingConcept.CostCenterId IsNot Nothing Then
                        crossingConcept.CostCenterCodeName = (From c In _context.CostCenter.AsNoTracking() Where c.Id = crossingConcept.CostCenterId Select String.Concat(c.Code, " - ", c.Name)).FirstOrDefault()
                    End If
                    If crossingConcept.Nature = 1 Then
                        crossingConcept.NatureName = "Debito"
                    Else
                        crossingConcept.NatureName = "Credito"
                    End If
                Next
            End If
            Return listCrossingCxC
        Else
            Return (From cad As CrossingAccountDetailOtherConcept In _context.CrossingAccountDetailOtherConcept.AsNoTracking()
                    Where cad.CrossingAccountId = crossingAccountId
                    Select cad).ToList()
        End If
    End Function
#End Region

End Class