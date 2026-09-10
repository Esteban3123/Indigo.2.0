'***********************************************************************
' Assembly         : Infrastructure.Data.MixinStationRepository
' Author           : Yoe Andres Cardenas
' Created          : 06/06/2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Text
Imports System.Data.Entity.Infrastructure
Imports Domain.Base
Imports Infrastructure.CrossCutting.Base

Public Class PackageRepository
    Inherits GenericRepository(Of Package)
    Implements IPackageRepository, Inject

    ''' <summary>
    ''' Contexto de Package
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de Package
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub
    ''' <summary>
    ''' Lista todos los paquetes
    ''' </summary>
    ''' <returns>Lista de paquetes</returns>
    ''' <remarks></remarks>
    Public Function ListAllPackage() As List(Of Package) Implements IPackageRepository.ListAllPackage
        Dim package = From e In _context.Package
                      Select e
        Return package.ToList()
    End Function

    ''' <summary>
    ''' Lista todos los paquetes duplicados
    ''' </summary>
    ''' <param name="packageId"></param>
    ''' <param name="packageDetailTmp"></param>
    ''' <returns></returns>
    Public Function ListDuplicatePackage(packageId As Integer, packageDetailTmp As List(Of Tuple(Of Byte, Integer))) As List(Of PackageDto) Implements IPackageRepository.ListDuplicatePackage
        Dim packagetmpAtc = (From p In _context.Package.Include("PackageDetail").Where(Function(pa) pa.Id <> packageId)
                             From pd In p.PackageDetail.Where(Function(pd) pd.ComponentType = 1)
                             Group Join d In packageDetailTmp.Where(Function(pdt) pdt.Item1 = 1).Select(Function(pdt) pdt.Item2) On pd.AtcId Equals d
                          Into temp = Group From r In temp.DefaultIfEmpty() Select p, r).ToList()

        Dim packagetmpSupplie = (From p In _context.Package.Include("PackageDetail").Where(Function(pa) pa.Id <> packageId)
                                 From pd In p.PackageDetail.Where(Function(pd) pd.ComponentType = 2)
                                 Group Join d In packageDetailTmp.Where(Function(pdt) pdt.Item1 = 2).Select(Function(pdt) pdt.Item2) On pd.SupplieId Equals d
                          Into temp = Group From r In temp.DefaultIfEmpty() Select p, r).ToList()

        Dim packagetmpProduct = (From p In _context.Package.Include("PackageDetail").Where(Function(pa) pa.Id <> packageId)
                                 From pd In p.PackageDetail.Where(Function(pd) pd.ComponentType = 3)
                                 Group Join d In packageDetailTmp.Where(Function(pdt) pdt.Item1 = 3).Select(Function(pdt) pdt.Item2) On pd.ProductId Equals d
                          Into temp = Group From r In temp.DefaultIfEmpty() Select p, r).ToList()

        Dim resultado As New List(Of PackageDto)


        Dim op As PackageDto = Nothing
        Dim col As List(Of Tuple(Of Package, Integer)) = New List(Of Tuple(Of Package, Integer))

        If packagetmpAtc IsNot Nothing And packagetmpAtc.Count > 0 Then
            col.AddRange(packagetmpAtc.Select(Function(p) New Tuple(Of Package, Integer)(p.p, p.r)))
        End If
        If packagetmpSupplie IsNot Nothing And packagetmpSupplie.Count > 0 Then
            col.AddRange(packagetmpSupplie.Select(Function(p) New Tuple(Of Package, Integer)(p.p, p.r)))
        End If
        If packagetmpProduct IsNot Nothing And packagetmpProduct.Count > 0 Then
            col.AddRange(packagetmpProduct.Select(Function(p) New Tuple(Of Package, Integer)(p.p, p.r)))
        End If

        For Each r1 In col.GroupBy(Function(g) g.Item1.Id)
            If r1.Count() = packageDetailTmp.Count AndAlso r1.Where(Function(r2) r2.Item2 <> 0).Count = packageDetailTmp.Count Then
                op = New PackageDto With {.Id = r1.Key, .Code = r1.FirstOrDefault.Item1.Code, .Name = r1.FirstOrDefault.Item1.Name}
                resultado.Add(op)
            End If
        Next
        Return resultado
    End Function

    ''' <summary>
    ''' Obtiene un paquete especifico
    ''' </summary>
    ''' <param name="code">Codigo del paquete</param>
    ''' <returns>Packageo</returns>
    ''' <remarks></remarks>
    Public Function GetPackage(code As String, Optional tracking As Boolean = True) As Package Implements IPackageRepository.GetPackage
        If String.IsNullOrWhiteSpace(code) Then Throw New ArgumentNullException(NameOf(code))

        Dim ObjPackage As Package = Nothing

        If tracking Then
            ObjPackage = _context.Package.Include("PackageDetail").Include("UnitDoseType") _
                        .FirstOrDefault(Function(e) e.Code = code AndAlso e.PersonalizedMasterPreparation = 0)
        Else
            ObjPackage = _context.Package.AsNoTracking.Include("PackageDetail").Include("UnitDoseType") _
                        .FirstOrDefault(Function(e) e.Code = code AndAlso e.PersonalizedMasterPreparation = 0)
        End If

        If ObjPackage Is Nothing Then Return New Package()

        ObjPackage.ConcentrationMeasurementUnitCodeName = GetMeasurementUnitName(ObjPackage.ConcentrationMeasurementUnitId)
        ObjPackage.VolumeTotalOrderMeasurementUnitCodeName = GetMeasurementUnitName(ObjPackage.VolumeTotalOrderMeasurementUnitId)
        ObjPackage.MeasurementPreparedUnitCodeName = GetMeasurementUnitName(ObjPackage.MeasurementPreparedId)
        ObjPackage.StorageTemperatureDescription = _context.StorageTemperature.AsNoTracking().Where(Function(u) u.Id = ObjPackage.Storage).Select(Function(o) o.Description).FirstOrDefault()
        ObjPackage.RiskLevelCodeName = _context.InventoryRiskLevel.AsNoTracking().Where(Function(m) m.Id = ObjPackage.RiskLevelId).Select(Function(x) String.Concat(x.Code, " - ", x.Name)).FirstOrDefault()

        If ObjPackage.UnitDoseType.MSClass = EUnitDoseTypeClass.ParenteralNutrition Then
            Dim MainDrug = _context.ATC.AsNoTracking().Where(Function(x) x.Id = ObjPackage.MainDrugId).Select(Function(x) New With {.Code = x.Code, .Name = x.Name}).FirstOrDefault()
            If MainDrug IsNot Nothing Then
                ObjPackage.MainDrugCodeName = $"{MainDrug.Code} - {MainDrug.Name}"
            End If
        End If

        If ObjPackage.ProductId IsNot Nothing Then
            Dim product = _context.InventoryProduct.AsNoTracking().Where(Function(x) x.Id = ObjPackage.ProductId).Select(Function(x) New With {.Name = x.Name}).FirstOrDefault()
            If product IsNot Nothing Then
                ObjPackage.ProductName = product.Name
            End If
        End If

        If ObjPackage.PackageDetail?.Any() Then
            If ObjPackage.UnitDoseType.MSClass = EUnitDoseTypeClass.ParenteralNutrition Then

                For Each item In ObjPackage.PackageDetail
                    ProcessCommonDetailPackageProperties(item)
                Next
            Else
                Dim MainMedicine = ObjPackage.PackageDetail.FirstOrDefault(Function(x) x.MainMedicine)

                For Each item In ObjPackage.PackageDetail
                    ProcessCommonDetailPackageProperties(item)

                    If item.Vehicle OrElse item.Thinner And MainMedicine IsNot Nothing Then
                        item.ConcentrationName = String.Concat(Math.Round(CDec(If(item.Concentration, 0)), 2), " ", MainMedicine.MeasurementUnitAbbreviation, " / ", item.MeasurementUnitAbbreviation)
                    End If

                    If {EUnitDoseTypeClass.Antibiotics, EUnitDoseTypeClass.OtherSterile, EUnitDoseTypeClass.Cytostatic}.Contains(ObjPackage.UnitDoseType.MSClass) Then
                        If item.ComponentType = 1 Then
                            If item.Thinner Then item.PreparationTypeName = "Reconstituyente"
                            If item.MainMedicine Then item.PreparationTypeName = "Medicamento ppal"
                            If item.Vehicle Then item.PreparationTypeName = "Vehiculo"
                            ' Medicamento complementario/adicional (intratecal)
                            If item.ComplementaryMedicine.GetValueOrDefault(False) AndAlso Not item.MainMedicine.GetValueOrDefault(False) Then
                                item.PreparationTypeName = "Medicamento Adicional"
                            End If
                            If item.PreparationType = 0 AndAlso String.IsNullOrEmpty(item.PreparationTypeName) Then item.PreparationTypeName = ""
                        Else
                            item.PreparationTypeName = item.ComponentTypeName
                        End If
                    End If
                Next
            End If
        End If

        Return ObjPackage
    End Function

    ''' <summary>
    ''' Obtiene la concatenación del código y nombre de la unidad de medida
    ''' </summary>
    ''' <param name="unitId">Codigo del paquete</param>
    ''' <returns>Código y nombre de la unidad de medida</returns>
    ''' <remarks></remarks>
    Private Function GetMeasurementUnitName(unitId As Integer?) As String
        If Not unitId.HasValue Then Return String.Empty
        Dim result = _context.InventoryMeasurementUnit.AsNoTracking().Where(Function(x) x.Id = unitId.Value).Select(Function(x) String.Concat(x.Code, " - ", x.Name)).FirstOrDefault()
        Return If(result, String.Empty)
    End Function

    ''' <summary>
    ''' Establece las propiedades comunes para los detalles de los paquetes
    ''' </summary>
    ''' <param name="item">PackageDetail</param>
    ''' <remarks></remarks>
    Private Sub ProcessCommonDetailPackageProperties(item As PackageDetail)
        If item.AtcId.HasValue Then
            Dim atc = _context.ATC.AsNoTracking().Where(Function(x) x.Id = item.AtcId.Value).Select(Function(x) New With {.Code = x.Code, .Name = x.Name, .DCIId = x.DCIId}).FirstOrDefault()
            item.SourceCodeName = String.Concat(atc?.Code, " - ", atc?.Name)
            item.DCIName = _context.DCI.AsNoTracking().Where(Function(m) m.Id = atc.DCIId).Select(Function(m) m.Name).FirstOrDefault()

        ElseIf item.SupplieId.HasValue Then
            Dim supplie = _context.InventorySupplie.AsNoTracking().Where(Function(x) x.Id = item.SupplieId.Value).Select(Function(x) New With {.Code = x.Code, .Name = x.SupplieName}).FirstOrDefault()
            item.SourceCodeName = String.Concat(supplie?.Code, " - ", supplie?.Name)
            item.DCIName = supplie.Name

        ElseIf item.ProductId.HasValue Then
            Dim product = _context.InventoryProduct.AsNoTracking().Where(Function(x) x.Id = item.ProductId.Value).Select(Function(x) New With {.Code = x.Code, .Name = x.Name, .AtcId = x.ATCId, .SupplieId = x.SupplieId}).FirstOrDefault()
            item.SourceCodeName = String.Concat(product?.Code, " - ", product?.Name)

            If product?.AtcId.HasValue Then
                item.DCIName = _context.ATC.AsNoTracking().Where(Function(m) m.Id = product.AtcId).Select(Function(m) m.DCI.Name).FirstOrDefault()
            ElseIf product?.SupplieId.HasValue Then
                item.DCIName = _context.InventorySupplie.AsNoTracking().Where(Function(s) s.Id = product.SupplieId).Select(Function(s) s.SupplieName).FirstOrDefault()
            End If
        End If

        If item.MeasurementUnitId.HasValue Then
            Dim measurementUnit = _context.InventoryMeasurementUnit.AsNoTracking().Where(Function(x) x.Id = item.MeasurementUnitId.Value).Select(Function(x) New With {x.Code, x.Name, x.Abbreviation}).FirstOrDefault()

            If item.ComponentType <> 4 AndAlso item.Quantity.HasValue Then
                item.QuantityMeasureunitname = String.Concat(item.Quantity, " ", measurementUnit?.Abbreviation)
            End If

            item.MeasureUnitDescription = String.Concat(measurementUnit?.Code, " - ", measurementUnit?.Name)
            item.MeasurementUnitAbbreviation = measurementUnit?.Abbreviation
            item.Dosis = String.Format("{0} {1}", item.Quantity, measurementUnit?.Name)
        End If

        If item.VolumeMeasureUnit.HasValue Then
            Dim volumeMU = _context.InventoryMeasurementUnit.AsNoTracking().Where(Function(x) x.Id = item.VolumeMeasureUnit.Value).Select(Function(x) New With {.Code = x.Code, .Name = x.Name, .Abbreviation = x.Abbreviation}).FirstOrDefault()
            item.VolumeMeasureUnitDescription = String.Concat(volumeMU?.Code, " - ", volumeMU?.Name)
            item.VolumeMeasureUnitAbbreviation = volumeMU?.Abbreviation
        End If

        Select Case item.ComponentType
            Case 1, 4
                item.ComponentTypeName = "Medicamento"
            Case 2, 5
                item.ComponentTypeName = "Insumo"
            Case 3
                item.ComponentTypeName = "Producto"
            Case Else
                item.ComponentTypeName = ""
        End Select
    End Sub

    ''' <summary>
    ''' Obtiene un paquete por el identificador
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    Public Function GetPackageById(id As String, Optional tracking As Boolean = True) As Package Implements IPackageRepository.GetPackageById
        Dim package = From e In _context.Package
                      Where e.Id = id
                      Select e
        If package.Count > 0 Then
            Dim ObjPackage = Nothing
            If tracking = False Then
                ObjPackage = (From e In _context.Package.AsNoTracking
                              Where e.Id = id
                              Select e).SingleOrDefault
            Else
                ObjPackage = package.SingleOrDefault
            End If
            Return ObjPackage
        Else
            Return New Package()
        End If
    End Function

    ''' <summary>
    ''' Guarda los paquetes
    ''' </summary>
    ''' <param name="xml"></param>
    ''' <param name="userCode"></param>
    ''' <returns></returns>
    Public Function SP_SavePackage(xml As String, userCode As String) As SP_SavePackage_Result Implements IPackageRepository.SP_SavePackage
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SavePackage(xml, userCode).SingleOrDefault
    End Function


    ''' <summary>
    ''' Obtiene un paquete asociado a un proceso de producción
    ''' </summary>
    ''' <param name="code">Codigo del paquete</param>
    ''' <returns>Packageo</returns>
    ''' <remarks></remarks>
    Public Function GetProductionPackage(code As String, Optional tracking As Boolean = True) As Boolean Implements IPackageRepository.GetProductionPackage
        Dim Validate As Boolean
        Dim Result = (From e In _context.Package.Include("RequestMixingStationDetail")
                      Where e.Code = code Select e).ToList()
        If Result(0).RequestMixingStationDetail.Any() Then
            Validate = False
        Else
            Validate = True
        End If
        Return Validate
    End Function
End Class