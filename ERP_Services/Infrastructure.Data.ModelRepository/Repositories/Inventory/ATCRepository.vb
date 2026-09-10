'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepositiry
' Author           : Diego Andrés Roldán Lozano
' Created          : 02-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure

Public Class ATCRepository
    Inherits GenericRepository(Of ATC)
    Implements IATCRepository

    ''' <summary>
    ''' The _context
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

#Region "Methods"
    ''' <summary>
    ''' Obtiene un atc por codigo
    ''' </summary>
    Public Function GetATC(code As String) As ATC Implements IATCRepository.GetATC
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Code")
        End If
        Dim query = (From atc In _context.ATC.Include("TechnicalSheet").Include("ATCAdministrationRoute").Include("RelatedSupplieMedicine").Include("ATCConcentrationByDCI").Include("POSPathologies").Include("ATCClinicalData") Where atc.Code.Equals(code) Select atc).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From atc In _context.ATC.AsNoTracking() Where atc.Code.Equals(code) Select atc).FirstOrDefault()

            query.NullTextDCI = (From ar In _context.DCI Where ar.Id = query.DCIId Select String.Concat(ar.Code, " - ", ar.Name)).FirstOrDefault()
            'query.NullTextAdministrationRoute = (From ar In _context.AdministrationRoute Where ar.Id = query.AdministrationRouteId Select String.Concat(ar.Code, " - ", ar.Name)).FirstOrDefault()
            query.NullTextPharmacologicalGroup = (From gf In _context.PharmacologicalGroup Where gf.Id = query.PharmacologicalGroupId Select String.Concat(gf.Code, " - ", gf.Name)).FirstOrDefault()
            query.NullTextRiskLevel = (From gf In _context.InventoryRiskLevel Where gf.Id = query.InventoryRiskLevelId Select String.Concat(gf.Code, " - ", gf.Name)).FirstOrDefault()
            query.NullTextUnitMeasure = (From gf In _context.InventoryMeasurementUnit Where gf.Id = query.WeightMeasureUnit Select gf.Name).FirstOrDefault()
            query.NullTextUnitMeasureVolumen = (From gf In _context.InventoryMeasurementUnit Where gf.Id = query.VolumeMeasureUnit Select gf.Name).FirstOrDefault()
            query.NullTextUnitAdministration = (From imu In _context.InventoryMeasurementUnit Where imu.Id = query.AdministrationUnitId And imu.UnitType = 3 Select imu.Name).FirstOrDefault()
            query.NullTextUnitMeasureConcentration = (From gf In _context.InventoryMeasurementUnit Where gf.Id = query.ConcentrationMeasureUnitId Select gf.Name).FirstOrDefault()
            query.ATCMeasureAbbreviation = (From imu In _context.InventoryMeasurementUnit Where imu.Id = query.ConcentrationMeasureUnitId Select imu.Abbreviation).FirstOrDefault()
            query.BillingGroupNoPOSDescription = IIf(query.BillingGroupNoPosId IsNot Nothing, (From bg In _context.BillingGroup.AsNoTracking() Where bg.Id = query.BillingGroupNoPosId Select String.Concat(bg.Code, " - ", bg.Name)).FirstOrDefault(), String.Empty)
            query.PharmaceuticalFormDescription = IIf(query.PharmaceuticalFormId IsNot Nothing, (From ff In _context.PharmaceuticalForm.AsNoTracking() Where ff.Id = query.PharmaceuticalFormId Select String.Concat(ff.Code, " - ", ff.Name)).FirstOrDefault(), String.Empty)
            query.RequireStability = IIf(query.PharmaceuticalFormId IsNot Nothing, (From ff In _context.PharmaceuticalForm.AsNoTracking() Where ff.Id = query.PharmaceuticalFormId Select ff.RequireStability).FirstOrDefault(), False)

            If query.ATCEntityId <> Nothing AndAlso query.ATCEntityId > 0 Then
                query.NullTextATCEntity = (From gf In _context.ATCEntity.AsNoTracking() Where gf.Id = query.ATCEntityId Select String.Concat(gf.Code, " - ", gf.Name)).FirstOrDefault()
            End If

            If query.TechnicalSheet IsNot Nothing AndAlso query.TechnicalSheet.Count > 0 Then
                For Each item As TechnicalSheet In query.TechnicalSheet
                    Dim _diagnostic As Diagnostic = (From d In _context.Diagnostic Where d.Id = item.DiagnosticId Select d).FirstOrDefault()
                    item.DiagnosticCode = _diagnostic.Code
                    item.DiagnosticName = _diagnostic.Name
                Next
            End If
            For Each d In query.ATCAdministrationRoute
                d.AdministrationRoute = (From ar In _context.AdministrationRoute Where ar.Id = d.AdministrationRouteId Select ar).FirstOrDefault
                d.AdministrationRoute.ChangeTracker = New Domain.Base.Entities.ObjectChangeTracker() With {.ChangeTrackingEnabled = False}
            Next
            For Each d In query.ATCConcentrationByDCI
                d.DCICodeName = (From dci In _context.DCI Where dci.Id = d.DCIId Select String.Concat(dci.Code, " - ", dci.Name)).FirstOrDefault
                d.ConcentrationMeasureUnitCodeName = query.NullTextUnitMeasureConcentration
                d.Name = (From imu In _context.InventoryMeasurementUnit Where imu.Id = d.ConcentrationMeasureUnitId Select imu.Name).FirstOrDefault
                d.Abbreviation = (From imu In _context.InventoryMeasurementUnit Where imu.Id = d.ConcentrationMeasureUnitId Select imu.Abbreviation).FirstOrDefault
            Next
            query.AllPOSPathologies = Not query.POSPathologies?.Any()
            If query.RelatedSupplieMedicine IsNot Nothing AndAlso query.RelatedSupplieMedicine.Count > 0 Then
                Dim medicineSupplie = (From ms In query.RelatedSupplieMedicine Where ms.ItemType = 1 Select ms.SourceId).ToList()
                If medicineSupplie.Count > 0 Then 'insumos
                    Dim supplie = (From s In _context.InventorySupplie Where medicineSupplie.Contains(s.Id) Select s).ToList()
                    For Each smi In query.RelatedSupplieMedicine.Where(Function(d) d.ItemType = 1)
                        Dim s = (From su In supplie Where su.Id = smi.SourceId Select su).FirstOrDefault()
                        smi.SourceCode = s.Code
                        smi.SourceName = s.SupplieName
                        smi.NameItemType = "Insumo"
                    Next
                End If

                Dim medicine = (From m In query.RelatedSupplieMedicine Where m.ItemType = 2 Select m.SourceId).ToList()
                If medicine.Count > 0 Then 'medicamentos
                    Dim atcs = (From a In _context.ATC Where medicine.Contains(a.Id) Select a).ToList()
                    For Each smm In query.RelatedSupplieMedicine.Where(Function(d) d.ItemType = 2)
                        Dim atc = (From a In atcs Where a.Id = smm.SourceId Select a).FirstOrDefault()
                        If atc Is Nothing Then
                            Continue For
                        End If
                        smm.SourceCode = atc.Code
                        smm.SourceName = atc.Name
                        smm.NameItemType = "Medicamento"
                    Next
                End If
            End If

            Return query
        Else
            Return New ATC()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un atc por id
    ''' </summary>
    Public Function GetATCById(id As Integer) As ATC Implements IATCRepository.GetATCById
        If id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim query = (From atc In _context.ATC Where atc.Id = id Select atc).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From atc In _context.ATC.AsNoTracking() Where atc.Id = id Select atc).FirstOrDefault()
            Return query
        Else
            Return New ATC()
        End If
    End Function

    Public Function GetATCByCreateCrystalProduct(atcId As Integer) As ATC Implements IATCRepository.GetATCByCreateCrystalProduct
        Return (From a In _context.ATC.AsNoTracking().Include("DCI").AsNoTracking().Include("AdministrationRoute").AsNoTracking().Include("AdministrationRoute.PharmaceuticalForm").AsNoTracking().Include("PharmacologicalGroup").AsNoTracking().Include("InventoryRiskLevel").AsNoTracking() _
        .Include("InventoryMeasurementUnit").AsNoTracking().Include("InventoryMeasurementUnit1").AsNoTracking().Include("InventoryMeasurementUnit2").AsNoTracking()
                Where a.Id = atcId Select a).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Guarda el atc
    ''' </summary>
    ''' <param name="Xml"></param>
    ''' <param name="UserCode"></param>
    ''' <returns></returns>
    Public Function SP_SaveATC(Xml As String, UserCode As String) As SP_SaveATC_Result Implements IATCRepository.SP_SaveATC
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveATC(Xml, UserCode).SingleOrDefault
    End Function

    ''' <summary>
    ''' Elimina por Id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function SP_DeleteMedicament(Id As Integer) As SP_DeleteMedicament_Result Implements IATCRepository.SP_DeleteMedicament
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_DeleteMedicament(Id).SingleOrDefault
    End Function

#End Region

End Class