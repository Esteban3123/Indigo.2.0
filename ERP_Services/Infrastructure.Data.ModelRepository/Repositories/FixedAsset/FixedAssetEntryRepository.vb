'************************************************************
' Assembly         : Infrastructure.Data.FixedAssetRepository
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 17-02-2016
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Importar"
Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Domain.Base
Imports System.Data.Entity.Infrastructure

#End Region

Public Class FixedAssetEntryRepository
    Inherits GenericRepository(Of FixedAssetEntry)
    Implements IFixedAssetEntryRepository

    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''inicializa la neva instancia d clase.
    ''' </summary>
    ''' <param name="contex">el contexto.</param>
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

    Public Function GetFixedAssetEntry(code As String) As FixedAssetEntry Implements IFixedAssetEntryRepository.GetFixedAssetEntry
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As FixedAssetEntry In Me._context.FixedAssetEntry.Include("FixedAssetEntryCommitment").Include("FixedAssetEntryItem").Include("FixedAssetEntryItem.FixedAssetEntryItemDetail").Include("FixedAssetEntryItem.FixedAssetEntryItemDetail.FixedAssetEntryItemDetailBook").Include("FixedAssetEntryItem.FixedAssetEntryItemDetail.FixedAssetEntryItemDetailPart").Include("FixedAssetEntryItem.FixedAssetEntryItemDetail.FixedAssetEntryItemDetailPart.FixedAssetEntryItemDetailPartBook").Include("Currency") Where d.Code.Equals(code.Trim()) Select d).FirstOrDefault
        If res IsNot Nothing Then

            'Proveedor
            Dim supplierDistributionLine = (From sdl In _context.SuppliersDistributionLines.AsNoTracking() Where res.SupplierDistributionLineId = sdl.Id Select sdl).FirstOrDefault
            Dim supplier = (From s In _context.Supplier.AsNoTracking Where supplierDistributionLine.IdSupplier = s.Id Select s).FirstOrDefault
            Dim distributionLine = (From dl In _context.DistributionLines.AsNoTracking Where supplierDistributionLine.IdDistributionLine = dl.Id Select dl).FirstOrDefault
            res.SupplierCodeName = supplier.Code + " - " + supplier.Name + " - " + distributionLine.Code + " - " + distributionLine.Name
            'Responsable
            If res.ResponsibleId IsNot Nothing Then
                Dim responsible = (From r In _context.FixedAssetResponsible.AsNoTracking.Include("ThirdParty").AsNoTracking Where r.Id = res.ResponsibleId Select r).FirstOrDefault
                res.ResponsibleCodeName = responsible.Code + " - " + responsible.ThirdParty.Nit + " - " + responsible.ThirdParty.Name
            End If
            'Centro costo
            If res.CostCenterId IsNot Nothing Then
                Dim costCenter = (From c In _context.CostCenter.AsNoTracking Where c.Id = res.CostCenterId Select c).FirstOrDefault
                res.CostCenterCodeName = costCenter.Code + " - " + costCenter.Name
            End If

			If res.DocumentSupportId IsNot Nothing Then
				'Codigo y nombre de la resolucion de Documento Soporte
				Dim DocumentSupport = (From ds In _context.DocumentSupport.AsNoTracking Where res.DocumentSupportId = ds.Id Select ds).FirstOrDefault
				If DocumentSupport IsNot Nothing Then
					res.DescriptionDocumentSupport = $"{DocumentSupport.Code} - {DocumentSupport.Name}"
				Else
					Dim BillingAuthorization = (From ds In _context.BillingAuthorization.AsNoTracking Where res.DocumentSupportId = ds.Id Select ds).FirstOrDefault
					If BillingAuthorization IsNot Nothing Then
						res.DescriptionDocumentSupport = $"{BillingAuthorization.Code} - {BillingAuthorization.Name}"
					End If
				End If
			End If

			If res.FixedAssetEntryCommitment IsNot Nothing AndAlso res.FixedAssetEntryCommitment.Any() Then
                For Each apc In res.FixedAssetEntryCommitment
                    Dim CommitmentDetail = (From c In _context.CommitmentDetail.AsNoTracking.Include("Commitment").AsNoTracking.Include("Category").AsNoTracking.Include("Category.FinancialSource").AsNoTracking.Include("RevenueType").AsNoTracking
                                            Where c.Id = apc.CommitmentDetailId Select c).FirstOrDefault

                    apc.CommitmentCode = CommitmentDetail.Commitment.Code
                    apc.CommitmentDocument = CommitmentDetail.Commitment.Document
                    apc.CategoryCodeName = String.Format("{0} - {1}", CommitmentDetail.Category.Code, CommitmentDetail.Category.Name)
                    apc.FinancialSourceCodeName = String.Format("{0} - {1}", CommitmentDetail.Category.FinancialSource.Code, CommitmentDetail.Category.FinancialSource.Name)
                    apc.RevenueTypeCodeName = String.Format("{0} - {1}", CommitmentDetail.RevenueType.Code, CommitmentDetail.RevenueType.Name)
                    apc.Balance = CommitmentDetail.Balance
                Next
            End If

            If res.FixedAssetEntryItem.Count > 0 Then
                For Each entryItem In res.FixedAssetEntryItem
                    'Articulo
                    Dim item = (From i In _context.FixedAssetItem.AsNoTracking Where i.Id = entryItem.ItemId Select i).FirstOrDefault
                    entryItem.ItemCodeName = item.Code + " - " + item.Description
                    'IVA
                    If entryItem.IVAId IsNot Nothing Then
                        Dim iva = (From i In _context.GeneralLedgerIVA.AsNoTracking Where i.Id = entryItem.IVAId Select i).FirstOrDefault
                        entryItem.IVACodeName = iva.Code + " - " + iva.Name
                    End If
                    'Marca
                    Dim trademark = (From t In _context.FixedAssetTrademark.AsNoTracking Where t.Id = entryItem.TrademarkId Select t).FirstOrDefault
                    entryItem.TrademarkCodeName = trademark.Code + " - " + trademark.Name
                    'Poliza
                    Dim policy = (From p In _context.FixedAssetPolicy.AsNoTracking Where p.Id = entryItem.PolicyId Select p).FirstOrDefault
                    entryItem.PolicyCodeName = policy.Code + " - " + policy.Name

                    Dim FixedAssetItem = (From f In _context.FixedAssetItem.AsNoTracking.Include("FixedAssetItemCatalog").AsNoTracking Where f.Id = entryItem.ItemId Select f).FirstOrDefault
                    Dim AccountPayableConceptId As Integer = IIf(supplier.Declarant, FixedAssetItem.FixedAssetItemCatalog.DeclarantRetentionAccountPayableConceptId, FixedAssetItem.FixedAssetItemCatalog.NotDeclarantRetentionAccountPayableConceptId)
                    Dim RetentionConcept = (From r In _context.RetentionConcepts.AsNoTracking()
                                            Join a In _context.AccountPayableConcepts.AsNoTracking() On r.Id Equals a.RetentionConceptId
                                            Where a.Id = AccountPayableConceptId
                                            Select r).FirstOrDefault

                    If RetentionConcept IsNot Nothing Then
                        entryItem.MinBase = RetentionConcept.MinBase
                        entryItem.Rate = RetentionConcept.Rate
                        entryItem.TypeRounding = RetentionConcept.TypeRounding
                    End If

                    If entryItem.FixedAssetEntryItemDetail IsNot Nothing AndAlso entryItem.FixedAssetEntryItemDetail.Count > 0 Then
                        For Each entryItemDetail In entryItem.FixedAssetEntryItemDetail
                            'Responsable
                            Dim responsible = (From r In _context.FixedAssetResponsible.AsNoTracking.Include("ThirdParty").AsNoTracking Where r.Id = entryItemDetail.ReponsibleId Select r).FirstOrDefault
                            entryItemDetail.ResponsibleCodeName = responsible.Code + " - " + responsible.ThirdParty.Nit + " - " + responsible.ThirdParty.Name
                            'Estado del Activo
                            Dim statusAsset = (From s In _context.FixedAssetStatusAsset.AsNoTracking Where s.Id = entryItemDetail.StatusAssetId Select s).FirstOrDefault
                            entryItemDetail.StatusAssetCodeName = statusAsset.Code + " - " + statusAsset.Name

                            If entryItemDetail.FixedAssetEntryItemDetailBook IsNot Nothing AndAlso entryItemDetail.FixedAssetEntryItemDetailBook.Count > 0 Then
                                For Each entryItemDetailBook In entryItemDetail.FixedAssetEntryItemDetailBook
                                    'Libro oficial
                                    Dim legalBook = (From l In _context.LegalBook.AsNoTracking Where l.Id = entryItemDetailBook.LegalBookId Select l).FirstOrDefault
                                    entryItemDetailBook.LegalBookCodeName = legalBook.Code + " - " + legalBook.Name
                                Next
                            End If

                            If entryItemDetail.FixedAssetEntryItemDetailPart IsNot Nothing AndAlso entryItemDetail.FixedAssetEntryItemDetailPart.Count > 0 Then
                                For Each entryItemDetailPart In entryItemDetail.FixedAssetEntryItemDetailPart
                                    'Parte
                                    Dim part = (From p In _context.FixedAssetPartsAccesoriesConsumables.AsNoTracking Where p.Id = entryItemDetailPart.PartAccesoriesConsumiblesId Select p).FirstOrDefault
                                    entryItemDetailPart.PartAccesoriesConsumablesCodeName = part.Code + " - " + part.Name

                                    If entryItemDetailPart.FixedAssetEntryItemDetailPartBook IsNot Nothing AndAlso entryItemDetailPart.FixedAssetEntryItemDetailPartBook.Count > 0 Then
                                        For Each entryItemDetailPartBook In entryItemDetailPart.FixedAssetEntryItemDetailPartBook
                                            'Libro oficial
                                            Dim legalBook = (From l In _context.LegalBook.AsNoTracking Where l.Id = entryItemDetailPartBook.LegalBookId Select l).FirstOrDefault
                                            entryItemDetailPartBook.LegalBookCodeName = legalBook.Code + " - " + legalBook.Name
                                        Next
                                    End If

                                Next
                            End If

                        Next
                    End If

                Next
            End If

            res.OriginalValue = (From d As FixedAssetEntry In Me._context.FixedAssetEntry.AsNoTracking() Where d.Code.Equals(code.Trim()) Select d).SingleOrDefault()
            Return res
        Else
            Return New FixedAssetEntry()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un ingreso por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFixedAssetEntryById(Id As Integer) As FixedAssetEntry Implements IFixedAssetEntryRepository.GetFixedAssetEntryById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim res = (From d As FixedAssetEntry In Me._context.FixedAssetEntry.Include("FixedAssetEntryItem").Include("FixedAssetEntryItem.FixedAssetEntryItemDetail").Include("FixedAssetEntryItem.FixedAssetEntryPartsAccesoriesConsumibles") Where d.Id = Id Select d).FirstOrDefault
        If res IsNot Nothing Then

            Dim IdSuplier = res.SupplierDistributionLineId

            Dim ObjSuplier = (From a In _context.SuppliersDistributionLines.Include("Supplier").Include("Supplier.ThirdParty").AsNoTracking Where a.Id = IdSuplier Select a).FirstOrDefault()

            res.NameSuplier = ObjSuplier.Supplier.ThirdParty.Name
            'res.NameIVA = ObjFreightIVA.Name

            If res.FixedAssetEntryItem.Count > 0 Then
                For Each ObjFixedAssetRemissionEntranceEquipment As FixedAssetEntryItem In res.FixedAssetEntryItem
                    Dim IdEquipment = ObjFixedAssetRemissionEntranceEquipment.ItemId
                    Dim IdIVA = ObjFixedAssetRemissionEntranceEquipment.IVAId
                    Dim IdTrademark = ObjFixedAssetRemissionEntranceEquipment.TrademarkId
                    Dim IdPoliza = ObjFixedAssetRemissionEntranceEquipment.PolicyId

                    Dim ObjEquipment = (From c In _context.FixedAssetItem.AsNoTracking Where c.Id = IdEquipment Select c).FirstOrDefault()
                    Dim ObjIVA = (From d In _context.GeneralLedgerIVA.AsNoTracking Where d.Id = IdIVA Select d).FirstOrDefault()
                    Dim ObjTrademark = (From e In _context.FixedAssetTrademark.AsNoTracking Where e.Id = IdTrademark Select e).FirstOrDefault()
                    Dim ObjPoliza = (From f In _context.FixedAssetPolicy.AsNoTracking Where f.Id = IdPoliza Select f).FirstOrDefault()

                    ObjFixedAssetRemissionEntranceEquipment.NameEquipment = ObjEquipment.Description
                    ObjFixedAssetRemissionEntranceEquipment.NameIva = ObjIVA.Name
                    ObjFixedAssetRemissionEntranceEquipment.NameTrademark = ObjTrademark.Name
                    ObjFixedAssetRemissionEntranceEquipment.NamePoliza = ObjPoliza.Name


                    If ObjFixedAssetRemissionEntranceEquipment.FixedAssetEntryItemDetail IsNot Nothing Then
                        For Each ObjFixedAssetEntryItemDetail As FixedAssetEntryItemDetail In ObjFixedAssetRemissionEntranceEquipment.FixedAssetEntryItemDetail
                            Dim IdResponsible = ObjFixedAssetEntryItemDetail.ReponsibleId
                            Dim IdLocation = ObjFixedAssetEntryItemDetail.LocationId

                            Dim ObjResponsible = (From a In _context.FixedAssetResponsible.Include("ThirdParty").AsNoTracking Where a.Id = IdResponsible Select a).FirstOrDefault()
                            Dim ObjLocation = (From c In _context.FixedAssetLocation.AsNoTracking Where c.Id = IdLocation Select c).FirstOrDefault()

                            ObjFixedAssetEntryItemDetail.NameResponsible = ObjResponsible.ThirdParty.Name
                            ObjFixedAssetEntryItemDetail.NameLocation = ObjLocation.Name

                        Next
                    End If

                    'If ObjFixedAssetRemissionEntranceEquipment.FixedAssetEntryPa IsNot Nothing Then
                    '    For Each ObjParts As FixedAssetEntryPartsAccesoriesConsumibles In ObjFixedAssetRemissionEntranceEquipment.FixedAssetEntryPartsAccesoriesConsumibles
                    '        Dim IdPartsAccesories = ObjParts.IdPartsAccesoriesConsumibles
                    '        Dim ObjPartsAccesories = (From a In _context.FixedAssetPartsAccesoriesConsumables.AsNoTracking Where a.Id = IdPartsAccesories Select a).FirstOrDefault()

                    '        ObjParts.NamePartsAccesoriesConsumibles = ObjPartsAccesories.Name

                    '    Next
                    'End If

                Next
            End If

            res.OriginalValue = (From d As FixedAssetEntry In Me._context.FixedAssetEntry.AsNoTracking() Where d.Id = Id Select d).SingleOrDefault()
            Return res
        Else
            Return New FixedAssetEntry()
        End If
    End Function

    ''' <summary>
    ''' Guarda un ingreso de activos
    ''' </summary>
    ''' <param name="FixedAssetEntryXml"></param>
    ''' <param name="ListDeleteString"></param>
    ''' <param name="codeUser"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SP_SaveFixedAssetEntry(FixedAssetEntryXml As String, ListDeleteString As List(Of String), codeUser As String) As SP_SaveFixedAssetEntry_Result Implements IFixedAssetEntryRepository.SP_SaveFixedAssetEntry
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveFixedAssetEntry(FixedAssetEntryXml, ListDeleteString(0), ListDeleteString(1), ListDeleteString(2), ListDeleteString(3), ListDeleteString(4), codeUser).SingleOrDefault
    End Function

    ''' <summary>
    ''' Obtiene los parametros de activo fijo por unidad operativa
    ''' </summary>
    ''' <param name="OperatingUnitId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSettingFixedAssetByOperatingUnidId(OperatingUnitId As Integer) As SettingFixedAsset Implements IFixedAssetEntryRepository.GetSettingFixedAssetByOperatingUnidId
        If OperatingUnitId = 0 Then
            Throw New ArgumentNullException("OperatingUnitId")
        End If

        Dim settings = (From s In _context.SettingFixedAsset.AsNoTracking _
                            .Include("SettingFixedAssetByLegalBook.LegalBook").AsNoTracking
                        Where s.OperatingUnitId = OperatingUnitId
                        Select s).FirstOrDefault

        If settings IsNot Nothing Then
            'Porcentaje de los fletes
            Dim concept = (From apc In _context.AccountPayableConcepts.AsNoTracking _
                               .Include("RetentionConcepts").AsNoTracking()
                           Where apc.Id = settings.IVAFreightAccountPayableConceptId
                           Select apc).FirstOrDefault()

            If concept IsNot Nothing AndAlso concept.RetentionConcepts IsNot Nothing Then
                settings.FreightIVAPercentage = concept.RetentionConcepts.Rate
            End If

        End If
        Return settings
    End Function

    ''' <summary>
    ''' Obtiene concepto de pago por id
    ''' </summary>
    ''' <param name="AccountPayableConceptId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountPayableConceptById(AccountPayableConceptId As Integer) As AccountPayableConcepts Implements IFixedAssetEntryRepository.GetAccountPayableConceptById
        If AccountPayableConceptId = 0 Then
            Throw New ArgumentNullException("AccountPayableConceptId")
        End If
        Return (From a In _context.AccountPayableConcepts.AsNoTracking.Include("RetentionConcepts").AsNoTracking Where a.Id = AccountPayableConceptId Select a).FirstOrDefault
    End Function

    ''' <summary>
    ''' Obtiene el tercero con el id de la relacion de linea de distribucion y proveedor
    ''' </summary>
    ''' <param name="SupplierDistributionLineId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountPayableConceptBySupplierDistributionLineId(SupplierDistributionLineId As Integer) As AccountPayableConcepts Implements IFixedAssetEntryRepository.GetAccountPayableConceptBySupplierDistributionLineId
        If SupplierDistributionLineId = 0 Then
            Throw New ArgumentNullException("SupplierDistributionLineId")
        End If
        Dim supplierDistributionLine = (From s In _context.SuppliersDistributionLines.AsNoTracking.Include("Supplier").AsNoTracking.Include("Supplier.ThirdParty").AsNoTracking Where s.Id = SupplierDistributionLineId Select s).FirstOrDefault
        Return (From a In _context.AccountPayableConcepts.AsNoTracking.Include("RetentionConcepts").AsNoTracking.Include("MainAccounts").AsNoTracking
                Where a.Id = supplierDistributionLine.Supplier.ThirdParty.IVARetentionAccountPayableConceptId Select a).FirstOrDefault
    End Function

    ''' <summary>
    ''' Obtiene la linea de distribucion por id de la relacion de proveedor y linea de distribucion
    ''' </summary>
    ''' <param name="SupplierDistributionLineId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDistributionLineBySupplierDistributionLineId(SupplierDistributionLineId As Integer) As DistributionLines Implements IFixedAssetEntryRepository.GetDistributionLineBySupplierDistributionLineId
        If SupplierDistributionLineId = 0 Then
            Throw New ArgumentNullException("SupplierDistributionLineId")
        End If
        Dim supplierDistributionLine = (From s In _context.SuppliersDistributionLines.AsNoTracking Where s.Id = SupplierDistributionLineId Select s).FirstOrDefault
        Return (From d In _context.DistributionLines.AsNoTracking.Include("DistributionLinesICARetention").AsNoTracking.Include("DistributionLinesDetail").AsNoTracking() Where d.Id = supplierDistributionLine.IdDistributionLine Select d).FirstOrDefault
    End Function

    ''' <summary>
    ''' Obtiene el tercero por la relacion de linea distribucion y proveedor
    ''' </summary>
    ''' <param name="SupplierDistributionLineId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetThirdPartyBySupplierDistributionLineId(SupplierDistributionLineId As Integer) As ThirdParty Implements IFixedAssetEntryRepository.GetThirdPartyBySupplierDistributionLineId
        If SupplierDistributionLineId = 0 Then
            Throw New ArgumentNullException("SupplierDistributionLineId")
        End If
        Dim supplierDistributionLine = (From s In _context.SuppliersDistributionLines.AsNoTracking.Include("Supplier").AsNoTracking.Include("Supplier.ThirdParty").AsNoTracking
                                        Where s.Id = SupplierDistributionLineId Select s).FirstOrDefault
        Return supplierDistributionLine.Supplier.ThirdParty
    End Function

    ''' <summary>
    ''' Obtiene el catalogo que tiene relacionado el articulo
    ''' </summary>
    ''' <param name="ItemId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFixedAssetItemCatalogByItemId(ItemId As Integer) As FixedAssetItemCatalog Implements IFixedAssetEntryRepository.GetFixedAssetItemCatalogByItemId
        If ItemId = 0 Then
            Throw New ArgumentNullException("ItemId")
        End If
        Dim item = (From s In _context.FixedAssetItem.AsNoTracking.Include("FixedAssetItemCatalog").AsNoTracking.Include("FixedAssetItemCatalog.AccountPayableConcepts").AsNoTracking.Include("FixedAssetItemCatalog.AccountPayableConcepts.RetentionConcepts").AsNoTracking.Include("FixedAssetItemCatalog.AccountPayableConcepts1").AsNoTracking.Include("FixedAssetItemCatalog.AccountPayableConcepts1.RetentionConcepts").AsNoTracking
                                        Where s.Id = ItemId Select s).FirstOrDefault
        Return item.FixedAssetItemCatalog
    End Function

    ''' <summary>
    ''' Obtiene el proveedor por la relacion del proveedor y linea de distribucion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSupplierBySupplierDistributionLineId(SupplierDistributionLineId As Integer) As Supplier Implements IFixedAssetEntryRepository.GetSupplierBySupplierDistributionLineId
        If SupplierDistributionLineId = 0 Then
            Throw New ArgumentNullException("SupplierDistributionLineId")
        End If
        Dim supplierDistributionLine = (From s In _context.SuppliersDistributionLines.AsNoTracking.Include("Supplier").AsNoTracking
                                        Where s.Id = SupplierDistributionLineId Select s).FirstOrDefault
        Return supplierDistributionLine.Supplier
    End Function

    ''' <summary>
    ''' Obtiene el articulo por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFixedAssetItemById(Id As Integer) As FixedAssetItem Implements IFixedAssetEntryRepository.GetFixedAssetItemById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Return (From i In _context.FixedAssetItem.AsNoTracking Where i.Id = Id Select i).FirstOrDefault
    End Function

    ''' <summary>
    ''' Obtiene un catalogo de articulos por id 
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFixedAssetItemCatalogById(Id As Integer) As FixedAssetItemCatalog Implements IFixedAssetEntryRepository.GetFixedAssetItemCatalogById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Return (From i In _context.FixedAssetItemCatalog.AsNoTracking.Include("FixedAssetItemCatalogAdquisitionType").AsNoTracking.Include("FixedAssetItemCatalogAdquisitionType.LegalBook").AsNoTracking Where i.Id = Id Select i).FirstOrDefault
    End Function

    ''' <summary>
    ''' Obtiene el tipo de comprobante 
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetJournalVoucherType(Id As Integer) As JournalVoucherTypes Implements IFixedAssetEntryRepository.GetJournalVoucherType
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Return (From i In _context.JournalVoucherTypes.AsNoTracking Where i.Id = Id Select i).FirstOrDefault
    End Function

    ''' <summary>
    ''' Obtiene una localizacion por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFixedAssetLocationById(Id As Integer) As FixedAssetLocation Implements IFixedAssetEntryRepository.GetFixedAssetLocationById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Return (From l In _context.FixedAssetLocation.Include("FunctionalUnit").AsNoTracking Where l.Id = Id Select l).FirstOrDefault
    End Function

    ''' <summary>
    ''' Obtiene un ingreso de activos por id sin agregado
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function GetFixedAssetEntrySimpleById(Id As Integer) As FixedAssetEntry Implements IFixedAssetEntryRepository.GetFixedAssetEntrySimpleById
        Return (From x In _context.FixedAssetEntry.Include("FixedAssetEntryCommitment").AsNoTracking.Include("FixedAssetEntryItem").AsNoTracking.Include("FixedAssetEntryItem.FixedAssetEntryItemDetail").AsNoTracking Where x.Id = Id Select x).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Obtiene el registro del libro oficial de los libros parametrizados en el catalogo
    ''' </summary>
    ''' <param name="FixedAssetItemCatalogId"></param>
    ''' <returns></returns>
    Public Function GetOfficialBookInCatalogBooks(FixedAssetItemCatalogId As Integer) As FixedAssetItemCatalogAdquisitionType Implements IFixedAssetEntryRepository.GetOfficialBookInCatalogBooks
        Return (From catalog In _context.FixedAssetItemCatalog.AsNoTracking
                Join catalogBooks In _context.FixedAssetItemCatalogAdquisitionType.AsNoTracking On catalogBooks.ItemCatalogId Equals catalog.Id
                Join legalBook In _context.LegalBook.AsNoTracking On legalBook.Id Equals catalogBooks.LegalBookId
                Where catalog.Id = FixedAssetItemCatalogId AndAlso legalBook.OfficialBook = True
                Select catalogBooks).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Metodo que obtiene todos los catalogos de los articulos
    ''' </summary>
    ''' <param name="ListItemId"></param>
    ''' <returns></returns>
    Public Function GetItemByItemIds(ListItemId As List(Of Integer)) As List(Of FixedAssetItem) Implements IFixedAssetEntryRepository.GetItemByItemIds
        Return (From x In _context.FixedAssetItem.AsNoTracking.Include("FixedAssetItemCatalog").AsNoTracking.Include("FixedAssetItemCatalog.FixedAssetItemCatalogAdquisitionType").AsNoTracking
                Where ListItemId.Contains(x.Id)
                Select x).OrderByDescending(Function(y) y.FixedAssetItemCatalog.FixedAssetItemCatalogAdquisitionType.Count()).ToList()
    End Function

    ''' <summary>
    ''' Valida que la cantidad de los productos que tengan presupuesto asociado al grupo sea igual que la sumatoria de los compromisos
    ''' </summary>
    ''' <returns></returns>
    Public Function ValidateItemsAndCommitments(listFixedAssetEntryItem As List(Of FixedAssetEntryItem), listFixedAssetEntryCommitment As List(Of FixedAssetEntryCommitment)) As Boolean Implements IFixedAssetEntryRepository.ValidateItemsAndCommitments
        Dim sumFixedAssetEntryItem As Decimal = 0
        Dim sumFixedAssetEntryCommitment As Decimal = 0

        Dim listDetails = (From evd In listFixedAssetEntryItem
                           Join p In _context.FixedAssetItem.AsNoTracking() On p.Id Equals evd.ItemId
                           Join g In _context.FixedAssetItemCatalog.AsNoTracking() On g.Id Equals p.ItemCatalogId
                           Where evd.ChangeTracker.State <> Domain.Base.Entities.ObjectState.Deleted AndAlso g.BudgetId IsNot Nothing
                           Select New With {.BudgetId = g.BudgetId, .Value = evd.TotalValue}).ToList()

        If listDetails IsNot Nothing AndAlso listDetails.Count > 0 Then 'Si se esta restringiendo el rubro
            Dim listBudgets = (From faec In listFixedAssetEntryCommitment
                               Join cd In _context.CommitmentDetail.AsNoTracking() On faec.CommitmentDetailId Equals cd.Id
                               Join b In _context.Budget.AsNoTracking() On cd.CategoryId Equals b.CategoryId And cd.RevenueTypeId Equals b.RevenueTypeId
                               Select New With {.BudgetId = b.Id, .Value = faec.Value}).ToList()

            For Each detail In listDetails.GroupBy(Function(d) d.BudgetId) 'Se valida que se hayan agregado al menos los detalles con valores restringidos
                sumFixedAssetEntryItem = detail.Sum(Function(d) d.Value)
                sumFixedAssetEntryCommitment = 0

                If listBudgets IsNot Nothing AndAlso listBudgets.Any(Function(d) d.BudgetId = detail.Key) Then
                    sumFixedAssetEntryCommitment = listBudgets.Where(Function(d) d.BudgetId = detail.Key).Sum(Function(d) d.Value)
                End If

                If Math.Round(sumFixedAssetEntryCommitment, 0) < Math.Round(sumFixedAssetEntryItem, 0) Then
                    Return False
                End If
            Next
        End If

        sumFixedAssetEntryItem = listFixedAssetEntryItem.Sum(Function(d) d.TotalValue)
        sumFixedAssetEntryCommitment = listFixedAssetEntryCommitment.Sum(Function(d) d.Value)

        If Math.Round(sumFixedAssetEntryCommitment, 0) <> Math.Round(sumFixedAssetEntryItem, 0) Then
            Return False
        End If

        Return True
    End Function

End Class
