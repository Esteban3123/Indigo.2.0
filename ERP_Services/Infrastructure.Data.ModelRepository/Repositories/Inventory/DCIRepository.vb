'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure
Imports Infrastructure.CrossCutting.Base

Public Class DCIRepository
    Inherits GenericRepository(Of DCI)
    Implements IDCIRepository

    ''' <summary>
    ''' Contexto de payments
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de payments
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene un dci por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDCI(code As String) As DCI Implements IDCIRepository.GetDCI
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If

        Dim res = (From d In _context.DCI.Include("DCIATCEntity.ATCEntity").Include("DrugActive1.ATCEntity").Include("DrugInteraction1.ATCEntity").Include("HighRiskDrugs.DCI").Include("DrugInteraction.DCI").Include("DrugActive.DCI").Include("LethalDoseLimits").Include("DCIRiskFactors").Include("RisksDescription")
                   Where d.Code.Equals(code.Trim())
                   Select d).FirstOrDefault()

        If res IsNot Nothing Then
            If res.DCIATCEntity.Any() Then
                For Each i In res.DCIATCEntity
                    i.ATCCode = i.ATCEntity?.Code
                    i.ATCName = i.ATCEntity?.Name
                Next
            End If

            If res.DrugInteraction1.Any() Then
                For Each item In res.DrugInteraction1
                    item.isInherited = False
                    item.isInheritedName = "Interacciones en combinación"
                    item.DCICode = item.DCI?.Code
                    item.DCIName = item.DCI?.Name
                    item.ATCCode = item.ATCEntity?.Code
                    item.ATCName = item.ATCEntity?.Name
                    item.DCIParentCodeName = $"{res.Code} - {res.Name}"
                    item.ParentDCIOriginId = res.Id
                Next
            End If

            res.IHPARAMDCIs = (From d In _context.IHPARAMDCI Where d.CODDCIMEDPADRE = code Select d).ToList()

            For Each item In res.IHPARAMDCIs
                Dim dciName = (From d In _context.DCI.AsNoTracking() Where d.Code = item.CODDCIMED Select String.Concat(d.Code, " - ", d.Name)).FirstOrDefault()
                item.NameMED = dciName
                Dim _type = ""

                Select Case item.TIPO
                    Case 1
                        _type = "Suena igual"
                    Case 2
                        _type = "Se parece"
                    Case 3
                        _type = "Suena igual y se parece"
                End Select

                item.TypeSimilirityName = _type
            Next

            'Cargamos interacciones heredadas
            If res.DrugActive1.Any() Then
                For Each itemDrugActive In res.DrugActive1
                    Dim DCIInherited = (From di In _context.DrugInteraction.Include("DCI").Include("DCI1").Include("ATCEntity")
                                        Where di.ParentDCIId = itemDrugActive.DCIId
                                        Select di).ToList()

                    If DCIInherited?.Any Then
                        For Each itemInherited In DCIInherited
                            itemInherited.isInherited = True
                            itemInherited.isInheritedName = "Interacciones heredadas"
                            itemInherited.DCICode = itemInherited.DCI?.Code
                            itemInherited.DCIName = itemInherited.DCI?.Name
                            itemInherited.ATCCode = itemInherited.ATCEntity?.Code
                            itemInherited.ATCName = itemInherited.ATCEntity?.Name
                            itemInherited.DCIParentCodeName = $"{itemInherited.DCI1?.Code} - {itemInherited.DCI1?.Name}"
                            itemInherited.ParentDCIOriginId = itemInherited.ParentDCIId
                        Next

                        DCIInherited.ForEach(Sub(i) res.DrugInteraction1.Add(i))
                    End If

                    itemDrugActive.ATCCode = itemDrugActive.ATCEntity?.Code
                    itemDrugActive.ATCName = itemDrugActive.ATCEntity?.Name
                Next
            End If

            If res.HighRiskDrugs.Any() Then
                For Each item In res.HighRiskDrugs
                    Dim InventoryRiskLevel = (From rl As InventoryRiskLevel In Me._context.InventoryRiskLevel
                                              Where rl.Id = item.InventoryRiskLevelId
                                              Select rl).FirstOrDefault
                    item.RiskLevelCodeName = String.Format("{0} - {1}", InventoryRiskLevel.Code, InventoryRiskLevel.Name)
                Next
            End If

            If res.LethalDoseLimits.Any() Then
                For Each item In res.LethalDoseLimits
                    Dim startAgeUnitDescription As String = Utils.TimeUnitConvert(item.StartAgeUnit, item.StartAge)
                    Dim endAgeUnitDescription As String = Utils.TimeUnitConvert(item.EndAgeUnit, item.EndAge)

                    Dim startWeightUnitDescription As String = Utils.WeightUnitConvert(item.StartWeightUnit, item.StartWeight)
                    Dim endWeightUnitDescription As String = Utils.WeightUnitConvert(item.EndWeightUnit, item.EndWeight)

                    If item.StartAgeUnit = item.EndAgeUnit Then
                        item.AgeRange = $"{item.StartAge} a {item.EndAge} {startAgeUnitDescription}"
                    Else
                        item.AgeRange = $"{item.StartAge} {startAgeUnitDescription} a {item.EndAge} {endAgeUnitDescription}"
                    End If

                    If item.StartWeightUnit = item.EndWeightUnit Then
                        item.WeightRange = $"{item.StartWeight} a {item.EndWeight} {startWeightUnitDescription}"
                    Else
                        item.WeightRange = $"{item.StartWeight} {startWeightUnitDescription} a {item.EndWeight} {endWeightUnitDescription}"
                    End If

                    item.MaxConcPer24 = $"{item.Max24HourConcentration} {(From imu In _context.InventoryMeasurementUnit Where imu.Id = item.Max24HourConcentrationUnitId Select imu.Name).FirstOrDefault}"
                    item.MaxConcPerDose = $"{item.MaxDoseConcentration} {(From imu In _context.InventoryMeasurementUnit Where imu.Id = item.MaxDoseConcentrationUnitId Select imu.Name).FirstOrDefault}"
                    item.LethalConcPerDose = $"{item.LethalDoseConcentration} {(From imu In _context.InventoryMeasurementUnit Where imu.Id = item.LethalDoseConcentrationUnitId Select imu.Name).FirstOrDefault}"
                    item.LethalConcPer24 = $"{item.Lethal24HourConcentration} {(From imu In _context.InventoryMeasurementUnit Where imu.Id = item.Lethal24HourConcentrationUnitId Select imu.Name).FirstOrDefault}"

                Next
            End If

            If res.DCIRiskFactors.Any() Then
                For Each item In res.DCIRiskFactors

                    Dim riskfactor = (From rf In _context.RiskFactor
                                      Where rf.Id = item.RiskFactorId
                                      Select rf).FirstOrDefault()

                    If riskfactor IsNot Nothing Then
                        item.CodeRiskFactor = riskfactor.Code
                        item.NameRiskFactor = riskfactor.Description
                    End If

                Next
            End If

            Return res
        Else
            Return New DCI()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un dci por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDCIById(id As Integer) As DCI Implements IDCIRepository.GetDCIById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d In Me._context.DCI Where d.Id = id Select d).ToList
        If res.Count > 0 Then
            res.SingleOrDefault.OriginalValue = (From d As DCI In Me._context.DCI.AsNoTracking() Where d.Id = id Select d).SingleOrDefault()
            Return res.SingleOrDefault
        Else
            Return New DCI()
        End If
    End Function

    ''' <summary>
    ''' Funcion para actualizar un DCI
    ''' </summary>
    ''' <param name="DCIXml"></param>
    ''' <param name="ListDeleteMedicaments"></param>
    ''' <param name="ListDeleteDrugActive"></param>
    ''' <param name="OperatingUnitId"></param>
    ''' <param name="CodeUser"></param>
    ''' <returns></returns>
    Public Function SaveDCI(DCIXml As String, ListDeleteMedicaments As List(Of String), ListDeleteDrugActive As List(Of String), ListDeleteLethalDoseLimits As List(Of String), ListDeleteRisksDescription As List(Of String), ListDeleteDCIRiskFactors As List(Of String), OperatingUnitId As Integer, CodeUser As String) As SP_SaveDCI_Result Implements IDCIRepository.SaveDCI

        Dim TmpListDeleteMedicaments As String = String.Empty
        Dim TmpListDeleteDrugActive As String = String.Empty
        Dim TmpListDeleteLethalDoseLimits As String = String.Empty
        Dim TmpListDeleteRisksDescription As String = String.Empty
        Dim TmpListDeleteDCIRiskFactors As String = String.Empty

        If ListDeleteMedicaments IsNot Nothing AndAlso ListDeleteMedicaments.Count > 0 Then
            TmpListDeleteMedicaments = ListDeleteMedicaments(0)
        End If

        If ListDeleteDrugActive IsNot Nothing AndAlso ListDeleteDrugActive.Count > 0 Then
            TmpListDeleteDrugActive = ListDeleteDrugActive(0)
        End If

        If ListDeleteLethalDoseLimits IsNot Nothing AndAlso ListDeleteLethalDoseLimits.Count > 0 Then
            TmpListDeleteLethalDoseLimits = ListDeleteLethalDoseLimits(0)
        End If

        If ListDeleteRisksDescription IsNot Nothing AndAlso ListDeleteRisksDescription.Count > 0 Then
            TmpListDeleteRisksDescription = ListDeleteRisksDescription(0)
        End If

        If ListDeleteDCIRiskFactors IsNot Nothing AndAlso ListDeleteDCIRiskFactors.Count > 0 Then
            TmpListDeleteDCIRiskFactors = ListDeleteDCIRiskFactors(0)
        End If

        Return _context.SP_SaveDCI(DCIXml, TmpListDeleteMedicaments, TmpListDeleteDrugActive, TmpListDeleteLethalDoseLimits, TmpListDeleteRisksDescription, TmpListDeleteDCIRiskFactors, OperatingUnitId, CodeUser).FirstOrDefault()

    End Function

    ''' <summary>
    ''' Elimina por Id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function SP_DeleteDCI(Id As Integer) As SP_DeleteDCI_Result Implements IDCIRepository.SP_DeleteDCI
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_DeleteDCI(Id).SingleOrDefault
    End Function

End Class
