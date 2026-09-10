'***********************************************************************
' Assembly         : Infrastructure.Data.MixinStationRepository
' Author           : Yoe Andres Cardenas
' Created          : 15/04/2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure
Imports Domain.Base
Imports System.Data.Entity
Imports Infrastructure.Data.CrystalRepository

Public Class CMConfigRepository
    Inherits GenericRepository(Of CMConfiguration)
    Implements ICMConfigRepository, Inject

    ''' <summary>
    ''' Contexto de Configuración de Central de Mezclas
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork
    Private _crystalContext As ICrystalModelUnitOfWork
    ''' <summary>
    ''' Inicia el contexto de Configuración de Central de Mezclas
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork, ByVal crystalContext As ICrystalModelUnitOfWork)
        MyBase.New(context)
        _context = context
        _crystalContext = crystalContext
    End Sub

    ''' <summary>
    ''' Lista todos los parametros de configuración de central de Mezclas
    ''' </summary>
    ''' <returns>Lista de parametros</returns>
    ''' <remarks></remarks>
    Public Function ListAllCMConfig() As List(Of CMConfiguration) Implements ICMConfigRepository.ListAllCMConfig
        Dim cmConfig = From e In _context.CMConfiguration
                       Select e
        Return cmConfig.ToList()
    End Function
    ''' <summary>
    ''' Obtiene los parametros de cental de mezclas por Id
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetCMConfig(code As String, Optional tracking As Boolean = True) As CMConfiguration Implements ICMConfigRepository.GetCMConfig

        Dim query = (From e In _context.CMConfiguration _
                            .Include("CMCenterAttention") _
                            .Include("CMMixingProducitonLine") _
                            .Include("CMCenterLineUnit") _
                            .Include("CMExternalCareCenter") _
                            .Include("CMWarehouse") _
                            .Include("WorkingArea") _
                            .Include("CMConfigurationUsers")
                     Where e.Code = code
                     Select e)

        If Not tracking Then query = query.AsNoTracking()

        Dim cmConfig As CMConfiguration = query.FirstOrDefault()

        If Not tracking Then
            If cmConfig IsNot Nothing Then
                Dim linq = From ca In cmConfig.CMCenterAttention
                           Join pl In _context.ProductionLine.AsNoTracking
                           On ca.IdProductionLine Equals pl.Id
                           Select ca, pl
                For Each l In linq
                    l.ca.ProductionLine = l.pl
                    l.ca.CodeNameProductionLine = String.Format("{0} - {1}", l.pl.Code, l.pl.Name)
                    If l.ca.StateCA Is Nothing Then
                        l.ca.StateCA = True
                    End If
                    l.ca.StatusName = IIf(l.ca.StateCA, "Activo", "Inactivo")
                Next
                Dim linq1 = From ca In cmConfig.CMMixingProducitonLine
                            Join pl In _context.ProductionLine.AsNoTracking
                           On ca.Id_ProductionLine Equals pl.Id
                            Select ca, pl
                For Each l In linq1
                    l.ca.ProductionLine = l.pl
                    l.ca.ProductionLineCode = l.pl.Code
                    l.ca.ProductionLineName = l.pl.Name
                    If l.ca.StatePl Is Nothing Then
                        l.ca.StatePl = True
                    End If
                    l.ca.StatusName = IIf(l.ca.StatePl, "Activo", "Inactivo")
                Next
            End If
        Else
            If cmConfig IsNot Nothing Then
                If cmConfig.CMConfigurationSchedule IsNot Nothing AndAlso cmConfig.CMConfigurationSchedule.Count > 0 Then
                    Dim _DayName As String = String.Empty
                    For Each s In cmConfig.CMConfigurationSchedule
                        Select Case s.DayId
                            Case 1
                                _DayName = "Lunes"
                            Case 2
                                _DayName = "Martes"
                            Case 3
                                _DayName = "Miércoles"
                            Case 4
                                _DayName = "Jueves"
                            Case 5
                                _DayName = "Viernes"
                            Case 6
                                _DayName = "Sábado"
                            Case 7
                                _DayName = "Domingo"
                            Case 8
                                _DayName = "Festivo"
                        End Select
                        s.DayName = _DayName
                    Next
                End If

                Dim linq = From ca In cmConfig.CMCenterAttention
                           Join pl In _context.ProductionLine
                           On ca.IdProductionLine Equals pl.Id
                           Select ca, pl
                For Each l In linq
                    l.ca.ProductionLine = l.pl
                    l.ca.CodeNameProductionLine = String.Format("{0} - {1}", l.pl.Code, l.pl.Name)
                    If l.ca.StateCA Is Nothing Then
                        l.ca.StateCA = True
                    End If
                    l.ca.StatusName = IIf(l.ca.StateCA, "Activo", "Inactivo")
                Next
                Dim linq1 = From ca In cmConfig.CMMixingProducitonLine
                            Join pl In _context.ProductionLine
                           On ca.Id_ProductionLine Equals pl.Id
                            Select ca, pl
                For Each l In linq1
                    l.ca.ProductionLine = l.pl
                    l.ca.ProductionLineCode = l.pl.Code
                    l.ca.ProductionLineName = l.pl.Name
                    If l.ca.StatePl Is Nothing Then
                        l.ca.StatePl = True
                    End If
                    l.ca.StatusName = IIf(l.ca.StatePl, "Activo", "Inactivo")
                Next
            End If
        End If

        If cmConfig IsNot Nothing Then
            Dim linq1 = From ca In cmConfig.CMCenterAttention
                        Join cca In _context.SP_CM_CENTER_ATTENTION(cmConfig.Id)
                        On ca.CodeCenterAttention.Trim Equals cca.CodeCenterAttention
                        Select ca, cca
            For Each l In linq1
                l.ca.CodeNameCenterAttention = String.Format("{0} - {1}", l.cca.CodeCenterAttention.Trim, l.cca.NameCenterAttention.Trim)
                l.ca.Ubicacion = l.cca.Ubicacion
            Next

            If cmConfig.CMExternalCareCenter IsNot Nothing AndAlso cmConfig.CMExternalCareCenter.Count > 0 Then
                For Each item In cmConfig.CMExternalCareCenter
                    item.CustomerDescription = (From x In _context.Customer.AsNoTracking Where x.Id = item.CustomerId Select String.Concat(x.Nit, " - ", x.Name)).FirstOrDefault()
                    item.ExternalCareCenterDescription = (From x In _context.ExternalCareCenter.AsNoTracking Where x.Id = item.ExternalCareCenterId Select String.Concat(x.Code, " - ", x.Description)).FirstOrDefault()
                    item.ProductionLineDescription = (From x In _context.ProductionLine.AsNoTracking Where x.Id = item.ProductionLineId Select String.Concat(x.Code, " - ", x.Name)).FirstOrDefault()
                Next
            End If
            If cmConfig.CMWarehouse IsNot Nothing AndAlso cmConfig.CMWarehouse.Count > 0 Then

                For Each W In cmConfig.CMWarehouse

                    W.StatusName = IIf(W.StateWH, "Activo", "Inactivo")

                    Select Case W.WarehouseType
                        Case 1
                            W.WarehouseTypeName = "Materia Prima Stock"
                        Case 2
                            W.WarehouseTypeName = "Almacén"
                        Case 3
                            W.WarehouseTypeName = "En Proceso"
                        Case 4
                            W.WarehouseTypeName = "Terminado"
                        Case 5
                            W.WarehouseTypeName = "Control"
                        Case 6
                            W.WarehouseTypeName = "Remanente"
                    End Select

                    Dim WH = (From p In _context.Warehouse.AsNoTracking Where p.Id = W.IdWarehouse Select p).FirstOrDefault()

                    W.WareHouseCode = WH.Code
                    W.Description = WH.Name
                Next

            End If

            Return cmConfig
        Else
            Return New CMConfiguration()
        End If
    End Function

    ''' <summary>
    ''' Consulta unidades funcionales de centros de atencion de una central de mezcla
    ''' </summary>
    ''' <param name="Xml"></param>
    ''' <returns></returns>
    Public Function ListCMCenterLineUnit(Xml As String) As List(Of SP_CMCenterLineUnit_Result) Implements ICMConfigRepository.ListCMCenterLineUnit
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_CMCenterLineUnit(Xml).ToList
    End Function

    ''' <summary>
    ''' Se valida que el medicamento con el tipo de dosis unitaria no exista
    ''' </summary>
    ''' <param name="MedicinesProduction"></param>
    ''' <returns></returns>
    Public Function ValidateMedicineProduction(MedicinesProduction As MedicinesProduction) As String Implements ICMConfigRepository.ValidateMedicineProduction
        If MedicinesProduction.Id > 0 Then
            Dim result = (From mp In _context.MedicinesProduction.AsNoTracking() Where mp.Id = MedicinesProduction.Id).FirstOrDefault()
            If result Is Nothing Then
                Return "No se puede editar el item porque el medicamento el registro no fue encontrado"
            End If

            If MedicinesProduction.CMConfigurationId <> result.CMConfigurationId OrElse MedicinesProduction.ATCId <> result.ATCId OrElse MedicinesProduction.UnitDoseTypeId <> result.UnitDoseTypeId OrElse MedicinesProduction.CenterAttentionId <> result.CenterAttentionId Then
                Return "No se puede editar el item porque la información del detalle no coincide"
            End If

            If MedicinesProduction.AllowsRemnant = result.AllowsRemnant Then
                Return "El detalle no ha cambiado"
            End If
        Else
            Dim result = (From mp In _context.MedicinesProduction.AsNoTracking() Where mp.CMConfigurationId = MedicinesProduction.CMConfigurationId AndAlso mp.ATCId = MedicinesProduction.ATCId AndAlso mp.UnitDoseTypeId = MedicinesProduction.UnitDoseTypeId AndAlso mp.CenterAttentionId = MedicinesProduction.CenterAttentionId).FirstOrDefault()
            If result IsNot Nothing Then
                Dim cm = (From x In _context.ExternalCareCenter.AsNoTracking() Where x.Id = MedicinesProduction.CenterAttentionId Select x).FirstOrDefault()
                Dim ad = (From x In _crystalContext.ADCENATEN.AsNoTracking() Where x.CODCENATE = MedicinesProduction.CenterAttentionId).FirstOrDefault()
                Dim atc = (From x In _context.ATC.AsNoTracking() Where x.Id = MedicinesProduction.ATCId Select x).FirstOrDefault()
                Dim udt = (From x In _context.UnitDoseType.AsNoTracking() Where x.Id = MedicinesProduction.UnitDoseTypeId Select x).FirstOrDefault()
                Return String.Concat("No se puede agregar el item porque el medicamento ", atc.Code, " - ", atc.Name, " con tipo de dosis unitaria ", udt.Code, " - ", udt.Description, " ya se encuentra asignado a centro de atención seleccionado ", If(cm IsNot Nothing, cm.Code, ad.CODCENATE).Trim(), " - " + If(cm IsNot Nothing, cm.Description, ad.NOMCENATE))
            End If
        End If

        Return String.Empty
    End Function

    ''' <summary>
    ''' Permite la importación de los medicamentos para producción
    ''' </summary>
    ''' <param name="xmlObject"></param>
    ''' <param name="CMConfigurationId"></param>
    ''' <returns></returns>
    Public Function SP_ImportMedicinesProduction(xmlObject As String, CMConfigurationId As Integer) As List(Of SP_ImportMedicinesProduction_Result) Implements ICMConfigRepository.SP_ImportMedicinesProduction
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_ImportMedicinesProduction(xmlObject, CMConfigurationId).ToList
    End Function


    ''' <summary>
    ''' Obtengo la Central de Mezcla por ID
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Function GetCMDetailById(id As Integer) As CMConfiguration Implements ICMConfigRepository.GetCMDetailById
        Return (From ls In _context.CMConfiguration Where ls.Id = id Select ls).FirstOrDefault()
    End Function


End Class