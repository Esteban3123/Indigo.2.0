'***********************************************************************
' Assembly         : Infrastructure.Data.MixinStationRepository
' Author           : Duván Mejia Cortes 
' Created          : 17/08/2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure
Imports Domain.Base
Imports System.Data.Entity

Public Class RequestPackageDetailStatusRepository
    Inherits GenericRepository(Of RequestPackageDetailStatus)
    Implements IRequestPackageDetailStatusRepository, Inject

    ''' <summary>
    ''' Contexto de Tipo de dosis unitaria
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de Tipo de Dosis Unitaria
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene una lista, Filtrando Por Ids
    ''' </summary>
    ''' <param name="Ids"></param>
    ''' <param name="Status"></param>
    ''' <returns></returns>
    Public Function GetListPackageDetailStatus(Ids As List(Of Integer), Status As Byte?) As List(Of RequestPackageDetailStatus) Implements IRequestPackageDetailStatusRepository.GetListPackageDetailStatus
        If Status IsNot Nothing Then
            Dim Res = (From x In _context.RequestPackageDetailStatus.AsNoTracking().Include("RequestMixingStationDetail").AsNoTracking() Where Ids.Contains(x.Id) And x.Status = Status Select x).ToList()
            If Res Is Nothing Then
                Return New List(Of RequestPackageDetailStatus)
            End If
            Return Res
        End If
        Return (From x In _context.RequestPackageDetailStatus.AsNoTracking() Where Ids.Contains(x.Id) Select x).ToList()
    End Function

    ''' <summary>
    ''' Obtiene un objeto de la entidad
    ''' </summary>
    ''' <returns></returns>
    Public Function GetRequestPackageDetailStatusById(RequestPackageDetailStatusId As Integer) As RequestPackageDetailStatus Implements IRequestPackageDetailStatusRepository.GetRequestPackageDetailStatusById
        Dim res = (From r In _context.RequestPackageDetailStatus.AsNoTracking()
                   Where r.Id = RequestPackageDetailStatusId
                   Select r).FirstOrDefault()
        If res IsNot Nothing Then
            Return res
        Else
            Return New RequestPackageDetailStatus
        End If
    End Function

    ''' <summary>
    ''' Consulta el checklist del del paquete
    ''' </summary>
    ''' <param name="requestPackageDetailStatusIds"></param>
    ''' <returns></returns>
    Public Function GetRequestPackageDetailStatusDefectClassification(requestPackageDetailStatusIds As List(Of Integer), unitDoseClass As Integer, Optional Form As Byte = 0) As List(Of DefectClassificationModel) Implements IRequestPackageDetailStatusRepository.GetRequestPackageDetailStatusDefectClassification
        Dim query As IQueryable(Of DefectClassificationModel) = Nothing

        If requestPackageDetailStatusIds.Any() AndAlso requestPackageDetailStatusIds.Count = 1 AndAlso _context.RequestPackageDetailStatusDefectClassification.Any(Function(m) requestPackageDetailStatusIds.Contains(m.RequestPackageDetailStatusId)) Then
            query = (From dcg In _context.DefectClassificationGroup
                     Join dci In _context.DefectClassificationItem On dcg.Id Equals dci.DefectClassificationGroupId
                     Join dut In _context.DefectsUnitDoseType On dci.Id Equals dut.Id_DefectsClassificationItem
                     Group Join dcd In _context.RequestPackageDetailStatusDefectClassificationDetail On dci.Id Equals dcd.DefectClassificationItemId Into gdcd = Group
                     From dcd In gdcd.DefaultIfEmpty()
                     Group Join dc In _context.RequestPackageDetailStatusDefectClassification On dcd.RequestPackageDetailStatusDefectClassificationId Equals dc.Id Into gdc = Group
                     From dc In gdc.DefaultIfEmpty()
                     Where dcg.State AndAlso dci.State AndAlso requestPackageDetailStatusIds.Contains(dc.RequestPackageDetailStatusId) AndAlso dut.UnitDoseType.MSClass = unitDoseClass
                     Order By dcg.Weight Ascending, dci.Weight Ascending
                     Select New DefectClassificationModel With {
                        .Id = dci.Id,
                        .DefectClassificationGroupId = dcg.Id,
                        .DefectClassificationGroupWeight = dcg.Weight,
                        .DefectClassificationGroupDescription = dcg.Description,
                        .DefectClassificationItemId = dci.Id,
                        .DefectClassificationItemWeight = dci.Weight,
                        .DefectClassificationItemDescription = dci.Description,
                        .RequestPackageDetailStatusDefectClassificationId = dcd.Id,
                        .Critical = dci.Critical,
                        .Less = dci.Less,
                        .TypeName = If(dci.Critical, "Crítico", "Menor"),
                        .Production = If(dcd Is Nothing OrElse dcd.Production Is Nothing, False, dcd.Production),
                        .Quality = If(dcd Is Nothing OrElse dcd.Quality Is Nothing, False, dcd.Quality),
                        .RequestPackageDetailStatusId = dc.RequestPackageDetailStatusId,
                        .Observation = dc.Observation,
                        .CreatedAt = dc.CreationDate,
                        .UnitDoseClassAllowed = dcg.UnitDoseClassAllowed,
                        .Quantity = 0
                    })
            Return query.Distinct.ToList()
        End If

        If Form = 1 Or Form = 2 Then
            query = (From dcg In _context.DefectClassificationGroup
                     Join dci In _context.DefectClassificationItem On dcg.Id Equals dci.DefectClassificationGroupId
                     Join dut In _context.DefectsUnitDoseType On dut.Id_DefectsClassificationItem Equals dci.Id
                     Where dcg.State AndAlso dci.State AndAlso dut.UnitDoseType.MSClass = unitDoseClass AndAlso If(Form = 1, dci.ProductionChemical, If(Form = 2, dci.QualityChemical, False))
                     Order By dcg.Weight Ascending, dci.Weight Ascending
                     Select New DefectClassificationModel With {
                        .Id = dci.Id,
                        .DefectClassificationGroupId = dcg.Id,
                        .DefectClassificationGroupWeight = dcg.Weight,
                        .DefectClassificationGroupDescription = dcg.Description,
                        .DefectClassificationItemId = dci.Id,
                        .DefectClassificationItemWeight = dci.Weight,
                        .DefectClassificationItemDescription = dci.Description,
                        .RequestPackageDetailStatusDefectClassificationId = Nothing,
                        .Critical = dci.Critical,
                        .Less = dci.Less,
                        .TypeName = If(dci.Critical, "Crítico", "Menor"),
                        .Production = False,
                        .Quality = False,
                        .RequestPackageDetailStatusId = Nothing,
                        .UnitDoseClassAllowed = dcg.UnitDoseClassAllowed
                    })

            Return query.Distinct.ToList()

        End If

        ' Si no se encontró ningún resultado, devolver una lista vacía
        Return New List(Of DefectClassificationModel)()
    End Function

    Public Function GetPackageDetailStatusRequestInformation(requestPackageDetailStatusId As Integer, Optional tracking As Boolean = True) As RequestPackageDetailStatus Implements IRequestPackageDetailStatusRepository.GetPackageDetailStatusRequestInformation
        Dim query = (From rpsd In _context.RequestPackageDetailStatus _
                         .Include("PackagePersonalized.Package.InventoryProduct") _
                         .Include("RequestMixingStationDetail.RequestMixingStation.CMConfiguration.CMWarehouse")
                     Where rpsd.Id = requestPackageDetailStatusId
                     Select rpsd
                     )
        If Not tracking Then query = query.AsNoTracking()

        Dim requestPackageDetailStatus As RequestPackageDetailStatus = query.FirstOrDefault()

        If requestPackageDetailStatus.RequestMixingStationDetail.RequestMixingStation.CMConfiguration.CMWarehouse IsNot Nothing Then
            requestPackageDetailStatus.CodeMixingStation = requestPackageDetailStatus.RequestMixingStationDetail.RequestMixingStation.Code
            requestPackageDetailStatus.NameMixingStation = requestPackageDetailStatus.RequestMixingStationDetail.RequestMixingStation.CMConfiguration.Name
            Dim _CMWarehouse = requestPackageDetailStatus.RequestMixingStationDetail.RequestMixingStation.CMConfiguration.CMWarehouse.ToList()
            requestPackageDetailStatus.InventoryAdjustmentWarehouseId = _CMWarehouse.Where(Function(x) x.WarehouseType = 4).LastOrDefault().IdWarehouse
        End If

        If requestPackageDetailStatus IsNot Nothing Then
            Return requestPackageDetailStatus
        Else
            Return New RequestPackageDetailStatus
        End If
    End Function
End Class