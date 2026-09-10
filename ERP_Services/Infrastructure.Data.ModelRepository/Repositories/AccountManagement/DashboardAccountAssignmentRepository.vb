'************************************************************
' Assembly         : Domain.Entities
' Author           : Andrés Steven Rojas
' Created          : 02-05-2025
'
' Copyright        : (c) . All rights reserved.
'************************************************************
Imports System.Security.Cryptography
Imports Domain.AccountManagement.Model
Imports Domain.Entities
Imports Infrastructure.Data.Base
Imports Infrastructure.Data.CrystalRepository

Public Class DashboardAccountAssignmentRepository
    Inherits GenericRepository(Of AutomaticEntryDistribution)
    Implements IDashboardAccountAssignmentRepository

    ''' <summary>
    ''' Contexto de la entidades del modelo del EHR
    ''' </summary>
    Private _cristalContext As ICrystalModelUnitOfWork

    ''' <summary>
    ''' Contexto de la entidades del modelo del ERP
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

#Region "Builder"
    Public Sub New(crystalContext As ICrystalModelUnitOfWork, globalContext As IGlobalModelUnitOfWork)
        MyBase.New(globalContext)
        _context = globalContext
        _cristalContext = crystalContext
    End Sub

#End Region
    ''' <summary>
    ''' Obtiene los pacientes pendientes por asignar
    ''' </summary>
    ''' <param name="careCenter"></param>
    ''' <param name="entryType"></param>
    ''' <returns></returns>
    Public Function GetPendingAssignmentByCareCenterAndEntryType(careCenter As String, entryType As String, maxResults As Integer?) As List(Of VPendingAssignment) Implements IDashboardAccountAssignmentRepository.GetPendingAssignmentByCareCenterAndEntryType

        ' Primero obtenemos solo los números de ingreso no asignados (consulta ligera)
        Dim assignedAdmissionsQuery = _cristalContext.AutomaticEntryDistribution.Select(Function(aed) aed.AdmissionNumber)

        ' Construir query base sin materializar
        Dim baseQueryBuilder As IQueryable(Of VPendingAssignment)

        If entryType = "1,2" Or entryType = "2,1" Then
            baseQueryBuilder = (
         From ai In _cristalContext.ADINGRESO.AsNoTracking()
         Join p In _cristalContext.INPACIENT.AsNoTracking() On p.IPCODPACI Equals ai.IPCODPACI
         Join fu In _cristalContext.INUNIFUNC.AsNoTracking() On fu.UFUCODIGO Equals ai.UFUCODIGO
         Where ai.CODCENATE = careCenter AndAlso
               Not assignedAdmissionsQuery.Contains(ai.NUMINGRES)
         Select New VPendingAssignment With {
             .AdmissionNumber = ai.NUMINGRES,
             .PatientFullName = p.IPNOMCOMP,
             .PatientCode = p.IPCODPACI,
             .AdmissionDate = ai.IFECHAING,
             .Nit = p.CODIGONIT,
             .FunctionalUnitCode = fu.UFUCODIGO,
             .FunctionalUnitCodeName = fu.UFUCODIGO.Trim() & " - " & fu.UFUDESCRI.Trim(),
             .CareGroup = ai.GENCAREGROUP,
             .CareGroupCode = ai.CODCENATE,
             .Bed = ai.CODICAMHO,
             .Diagnosis = ai.CODDIAING,
             .Folio = ai.INUMERORE,
             .TypeIncome = ai.TIPOINGRE,
             .IncomeStatus = ai.IESTADOIN,
             .UserCreation = ai.CODUSUCRE,
             .UserModificacion = ai.CODUSUMOD
         }
     )
        Else
            baseQueryBuilder = (
         From ai In _cristalContext.ADINGRESO.AsNoTracking()
         Join p In _cristalContext.INPACIENT.AsNoTracking() On p.IPCODPACI Equals ai.IPCODPACI
         Join fu In _cristalContext.INUNIFUNC.AsNoTracking() On fu.UFUCODIGO Equals ai.UFUCODIGO
         Where ai.CODCENATE = careCenter AndAlso ai.TIPOINGRE = entryType AndAlso
               Not assignedAdmissionsQuery.Contains(ai.NUMINGRES)
         Select New VPendingAssignment With {
             .AdmissionNumber = ai.NUMINGRES,
             .PatientFullName = p.IPNOMCOMP,
             .PatientCode = p.IPCODPACI,
             .AdmissionDate = ai.IFECHAING,
             .Nit = p.CODIGONIT,
             .FunctionalUnitCode = fu.UFUCODIGO,
             .FunctionalUnitCodeName = fu.UFUCODIGO.Trim() & " - " & fu.UFUDESCRI.Trim(),
             .CareGroup = ai.GENCAREGROUP,
             .CareGroupCode = ai.CODCENATE,
             .Bed = ai.CODICAMHO,
             .Diagnosis = ai.CODDIAING,
             .Folio = ai.INUMERORE,
             .TypeIncome = ai.TIPOINGRE,
             .IncomeStatus = ai.IESTADOIN,
             .UserCreation = ai.CODUSUCRE,
             .UserModificacion = ai.CODUSUMOD
         }
     )
        End If

        'Aplicar límite si se especifica
        If maxResults.HasValue Then
            baseQueryBuilder = baseQueryBuilder.Take(maxResults.Value)
        End If

        ' Materializar los resultados base
        Dim baseQuery = baseQueryBuilder.ToList()

        ' Si no hay resultados, retornar lista vacía
        If baseQuery.Count = 0 Then
            Return baseQuery
        End If

        ' Procesar en lotes para evitar consultas con demasiados parámetros
        Const batchSize As Integer = 500
        Dim batches = baseQuery.Select(Function(x, index) New With {.Item = x, .Batch = index \ batchSize}) _
                        .GroupBy(Function(x) x.Batch) _
                        .Select(Function(g) g.Select(Function(x) x.Item).ToList()).ToList()

        ' Procesar cada lote
        For Each batch In batches
            ' Obtener IDs únicos para este lote
            Dim batchCareGroupIds = batch.Select(Function(x) CInt(x.CareGroup)).Distinct().ToList()
            Dim batchAdmissionNumbers = batch.Select(Function(x) x.AdmissionNumber).ToList()

            ' Usar diccionarios para búsquedas O(1)
            Dim careGroupDict As Dictionary(Of Integer, String) = Nothing
            Dim admissionsWithFoliosSet As HashSet(Of String) = Nothing

            ' Obtener CareGroups para este lote
            If batchCareGroupIds.Count > 0 Then
                careGroupDict = (
             From cg In _context.CareGroup.AsNoTracking()
             Where batchCareGroupIds.Contains(cg.Id)
             Select New With {
                 .Id = cg.Id,
                 .Display = cg.Code & " - " & cg.Name
             }
              ).ToDictionary(Function(x) x.Id, Function(x) x.Display)
            End If

            ' Usar EXISTS en lugar de JOIN para verificar folios
            If batchAdmissionNumbers.Count > 0 Then
                admissionsWithFoliosSet = New HashSet(Of String)(
             From rc In _context.RevenueControl.AsNoTracking()
             Where batchAdmissionNumbers.Contains(rc.AdmissionNumber) AndAlso
                   _context.RevenueControlDetail.Any(Function(rcd) rcd.RevenueControlId = rc.Id)
             Select rc.AdmissionNumber
             )
            End If

            ' Actualizar los items del lote
            For Each item In batch
                ' Actualizar CareGroup
                Dim careGroupId = CInt(item.CareGroup)
                If careGroupDict IsNot Nothing AndAlso careGroupDict.ContainsKey(careGroupId) Then
                    item.CareGroup = careGroupDict(careGroupId)
                Else
                    item.CareGroup = careGroupId.ToString()
                End If

                ' Actualizar Folio
                If admissionsWithFoliosSet IsNot Nothing AndAlso admissionsWithFoliosSet.Contains(item.AdmissionNumber) Then
                    item.Folio = "Folios asociados"
                Else
                    item.Folio = "Sin folios asociados"
                End If
            Next
        Next

        Return baseQuery
    End Function

End Class
