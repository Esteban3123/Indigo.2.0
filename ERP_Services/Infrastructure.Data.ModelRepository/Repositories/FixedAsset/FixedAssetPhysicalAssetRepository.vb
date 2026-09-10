'************************************************************
' Assembly         : Infrastructure.Data.FixedAssetRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 12/05/2016
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

Public Class FixedAssetPhysicalAssetRepository
    Inherits GenericRepository(Of FixedAssetPhysicalAsset)
    Implements IFixedAssetPhysicalAssetRepository

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

    Public Function GetPhysicalAssetById(Id As Integer) As FixedAssetPhysicalAsset Implements IFixedAssetPhysicalAssetRepository.GetPhysicalAssetById
        Dim res = (From e In _context.FixedAssetPhysicalAsset Where e.Id = Id Select e).FirstOrDefault
        If res IsNot Nothing Then
            Dim item = (From i In _context.FixedAssetItem.AsNoTracking().Include("FixedAssetItemCatalog").AsNoTracking() Where i.Id = res.ItemId Select i).FirstOrDefault()

            res.PercentageIVA = (From i In _context.GeneralLedgerIVA Where i.Id = item.IVAId Select i.Percentage).FirstOrDefault()

            If item.FixedAssetItemCatalog.WithholdingTaxConceptId IsNot Nothing Then
                Dim tax = (From r In _context.RetentionConcepts Where r.Id = item.FixedAssetItemCatalog.WithholdingTaxConceptId Select r).FirstOrDefault()
                res.RetentionIdTax = tax.Id
                res.RetentionPercentageTax = tax.Rate
                res.RetentionBaseTax = tax.MinBase
            End If

            If item.FixedAssetItemCatalog.WithholdingICAConceptId IsNot Nothing Then
                Dim ica = (From r In _context.RetentionConcepts Where r.Id = item.FixedAssetItemCatalog.WithholdingICAConceptId Select r).FirstOrDefault()
                res.RetentionIdICA = ica.Id
                res.RetentionPercentageICA = ica.Rate
                res.RetentionBaseICA = ica.MinBase
            End If

            Return res
        Else
            Return New FixedAssetPhysicalAsset()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un activo por placa
    ''' </summary>
    ''' <param name="Plate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFixedAssetPhysicalAssetByPlate(Plate As String) As FixedAssetPhysicalAsset Implements IFixedAssetPhysicalAssetRepository.GetFixedAssetPhysicalAssetByPlate
        If Plate Is Nothing OrElse Plate.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("Plate")
        End If
        Dim res = (From d In Me._context.FixedAssetPhysicalAsset Where d.Plate.Equals(Plate.Trim()) Select d).FirstOrDefault
        If res IsNot Nothing Then

            Dim Item = (From i In _context.FixedAssetItem.AsNoTracking Where i.Id = res.ItemId Select i).FirstOrDefault()
            res.ItemDescription = String.Concat(Item.Code, " - ", Item.Description)
            res.CodeItem = Item.Code

            res.MainAccountDescription = (From m In _context.MainAccounts.AsNoTracking Where m.Id = res.MainAccountId Select String.Concat(m.Number, " - ", m.Name)).FirstOrDefault
            res.LocationDescription = (From l In _context.FixedAssetLocation.AsNoTracking Where l.Id = res.LocationId Select String.Concat(l.Code, " - ", l.Name)).FirstOrDefault
            res.ClassLocation = (From l In _context.FixedAssetLocation.AsNoTracking Where l.Id = res.LocationId Select l.Class).FirstOrDefault
            res.ResponsibleDescription = (From r In _context.FixedAssetResponsible.AsNoTracking.Include("ThirdParty").AsNoTracking Where r.Id = res.ResponsibleId Select String.Concat(r.Code, " - ", r.ThirdParty.Name)).FirstOrDefault
            If res.SupplierId IsNot Nothing Then
                res.SupplierDescription = (From s In _context.Supplier.AsNoTracking Where s.Id = res.SupplierId Select String.Concat(s.Code, " - ", s.Name)).FirstOrDefault
            End If
            res.TrademarkDescription = (From t In _context.FixedAssetTrademark.AsNoTracking Where t.Id = res.TrademarkId Select String.Concat(t.Code, " - ", t.Name)).FirstOrDefault
            If res.PolicyId IsNot Nothing Then
                res.PolicyDescription = (From p In _context.FixedAssetPolicy.AsNoTracking Where p.Id = res.PolicyId Select String.Concat(p.Code, " - ", p.Name)).FirstOrDefault
            End If

            'Postula los datos de ingreso del activo si no se han guardado previamente
            If res.PurchaseDate Is Nothing OrElse String.IsNullOrEmpty(res.EntryNumber) Then
                Dim fixedAssetEntryItemDetail = (From r In _context.FixedAssetEntryItemDetail.AsNoTracking.Include("FixedAssetEntryItem").AsNoTracking.Include("FixedAssetEntryItem.FixedAssetEntry").AsNoTracking Where r.Plate = res.Plate Select r).FirstOrDefault
                If fixedAssetEntryItemDetail IsNot Nothing AndAlso fixedAssetEntryItemDetail.FixedAssetEntryItem IsNot Nothing AndAlso fixedAssetEntryItemDetail.FixedAssetEntryItem.FixedAssetEntry IsNot Nothing Then

                    If res.PurchaseDate Is Nothing Then
                        res.PurchaseDate = fixedAssetEntryItemDetail.FixedAssetEntryItem.FixedAssetEntry.EntryDate
                    End If

                    If String.IsNullOrEmpty(res.EntryNumber) Then
                        res.EntryNumber = fixedAssetEntryItemDetail.FixedAssetEntryItem.FixedAssetEntry.Code
                    End If

                    If String.IsNullOrEmpty(res.VoucherTransactionNumber) Then
                        Dim accountPayable = (From r In _context.AccountPayable.AsNoTracking.Include("DischargeBill").AsNoTracking.Include("DischargeBill.VoucherTransactionDetails").AsNoTracking.Include("DischargeBill.VoucherTransactionDetails.VoucherTransaction").AsNoTracking Where r.BillNumber = fixedAssetEntryItemDetail.FixedAssetEntryItem.FixedAssetEntry.InvoiceNumber Select r).FirstOrDefault

                        If accountPayable IsNot Nothing AndAlso accountPayable.DischargeBill IsNot Nothing AndAlso accountPayable.DischargeBill.Count > 0 Then
                            Dim dischargeBill = accountPayable.DischargeBill.First()

                            If dischargeBill IsNot Nothing AndAlso dischargeBill.VoucherTransactionDetails IsNot Nothing AndAlso dischargeBill.VoucherTransactionDetails.VoucherTransaction IsNot Nothing Then
                                res.VoucherTransactionNumber = dischargeBill.VoucherTransactionDetails.VoucherTransaction.Code
                            End If
                        End If
                    End If
                End If
            End If

            res.OriginalValue = (From d In Me._context.FixedAssetPhysicalAsset.AsNoTracking Where d.Plate.Equals(Plate.Trim()) Select d).SingleOrDefault()
            Return res
        Else
            Return New FixedAssetPhysicalAsset()
        End If
    End Function

    ''' <summary>
    ''' Confirma la finalización de contratos leasing
    ''' </summary>
    ''' <param name="Xml"></param>
    ''' <param name="OperatingUnitId"></param>
    ''' <param name="Year"></param>
    ''' <param name="Month"></param>
    ''' <param name="CompanyNit"></param>
    ''' <param name="CodeUser"></param>
    ''' <returns></returns>
    Public Function SP_ConfirmLeasingContractsFinalization(Xml As String, OperatingUnitId As Integer, Year As Integer, Month As Integer, CompanyNit As String, CodeUser As String) As SP_ConfirmLeasingContractsFinalization_Result Implements IFixedAssetPhysicalAssetRepository.SP_ConfirmLeasingContractsFinalization
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_ConfirmLeasingContractsFinalization(Xml, OperatingUnitId, Year, Month, CompanyNit, CodeUser).SingleOrDefault
    End Function

    ''' <summary>
    ''' Obtenemos el ultimo activo relacionado a un articulo, consultando tambien los detalles de sus libros
    ''' </summary>
    ''' <param name="ItemId"></param>
    ''' <returns></returns>
    Public Function GetLastFixedAssetPhysicalAssetByItem(ItemId As Integer) As FixedAssetPhysicalAsset Implements IFixedAssetPhysicalAssetRepository.GetLastFixedAssetPhysicalAssetByItem
        Dim physicalAsset = New FixedAssetPhysicalAsset
        physicalAsset = (From pa In _context.FixedAssetPhysicalAsset.AsNoTracking() _
                             .Include("FixedAssetPhysicalAssetDetailBook").AsNoTracking() _
                             .Include("FixedAssetPhysicalAssetDetailBook.LegalBook") Where pa.ItemId = ItemId
                         Order By pa.Id Descending Select pa).FirstOrDefault()
        Return physicalAsset
    End Function
End Class
