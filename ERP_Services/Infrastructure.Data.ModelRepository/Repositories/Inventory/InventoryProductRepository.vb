'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepositiry
' Author           : Diego Andrés Roldán Lozano
' Created          : 18-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity
Imports System.Threading.Tasks
Imports Infrastructure.CrossCutting.Exceptions

Public Class InventoryProductRepository
    Inherits GenericRepository(Of InventoryProduct)
    Implements IInventoryProductRepository

    ''' <summary>
    ''' The _context
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

#Region "Methods"

    Public Function GetCleanInventoryProductById(id As Integer, tracking As Boolean) As InventoryProduct Implements IInventoryProductRepository.GetCleanInventoryProductById
        Dim query = (From p In _context.InventoryProduct Where p.Id = id Select p)

        If Not tracking Then
            query = query.AsNoTracking()
        End If

        Return query.FirstOrDefault()
    End Function

    ''' <summary>
    ''' Obtiene un producto por codigo (OBSOLETO - Usar GetInventoryProductAsync)
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">Code</exception>
    <Obsolete("Esta función está marcada como obsoleta. Utilice GetInventoryProductAsync en su lugar para mejor rendimiento y funcionalidad asíncrona.", False)>
    Public Function GetInventoryProduct(code As String) As InventoryProduct Implements IInventoryProductRepository.GetInventoryProduct
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Code")
        End If

        Dim query As InventoryProduct = Nothing
        Dim codeSplit As String()
        If code.ToUpper().Contains("*IND*") Then
            codeSplit = code.Split("*IND*")
        Else
            codeSplit = code.Split(" - ")
        End If
        Dim productCode = codeSplit.ElementAt(0)

        query = (From ip In _context.InventoryProduct.Include("ProductHierarchy") _
                     .Include("ProductBarcode") _
                     .Include("POSPathologies") _
                     .Include("InventoryProductAttribute").Include("ProductHierarchy2.InventoryProduct1") _
                     .Include("ProductType") _
                     .Include("PhysicalInventory.Warehouse")
                 Where ip.Code.Equals(productCode) Select ip).FirstOrDefault()
        If query Is Nothing Then
            Dim productBar = (From pbc In _context.ProductBarcode.AsNoTracking() Where pbc.Barcode = productCode Select pbc).FirstOrDefault()
            If productBar Is Nothing Then
                Return New InventoryProduct
            End If
            query = (From ip In _context.InventoryProduct.Include("ProductHierarchy") _
                         .Include("ProductBarcode") _
                         .Include("POSPathologies") _
                         .Include("InventoryProductAttribute").Include("ProductHierarchy2.InventoryProduct1") _
                         .Include("ProductType") _
                         .Include("PhysicalInventory.Warehouse")
                     Where ip.Id = productBar.ProductId Select ip).FirstOrDefault()
        End If


        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From ip In _context.InventoryProduct.AsNoTracking() Where ip.Code.Equals(code) Select ip).FirstOrDefault()

            query.ProductTypeDescription = (From pt In _context.ProductType Where pt.Id = query.ProductTypeId Select String.Concat(pt.Code, " - ", pt.Name)).FirstOrDefault()

            Dim atc = (From a In _context.ATC Where a.Id = query.ATCId Select a).FirstOrDefault()
            If atc IsNot Nothing Then
                query.ATCDescription = String.Concat(atc.Code, " - ", atc.Name)
            Else
                query.ATCDescription = String.Empty
            End If

            If query.InventoryRiskLevelId Is Nothing And atc IsNot Nothing Then
                query.InventoryRiskLevelId = atc.InventoryRiskLevelId
                query.InventoryRiskLevelDescription = (From r In _context.InventoryRiskLevel Where r.Id = atc.InventoryRiskLevelId Select String.Concat(r.Code, " - ", r.Name)).FirstOrDefault()
            Else
                query.InventoryRiskLevelDescription = IIf(query.InventoryRiskLevelId IsNot Nothing, (From r In _context.InventoryRiskLevel Where r.Id = query.InventoryRiskLevelId Select String.Concat(r.Code, " - ", r.Name)).FirstOrDefault(), String.Empty)
            End If

            query.PackingUnitDescription = (From pu In _context.PackagingUnit Where pu.Id = query.PackagingUnitId Select String.Concat(pu.Code, " - ", pu.Name)).FirstOrDefault()
            query.GroupDescription = If(query?.ProductGroupId > 0, (From g In _context.ProductGroup Where g.Id = query.ProductGroupId Select String.Concat(g.Code, " - ", g.Name)).FirstOrDefault(), String.Empty)
            query.SubGroupDescription = If(query?.ProductSubGroupId > 0, (From sg In _context.ProductSubGroup Where sg.Id = query.ProductSubGroupId Select String.Concat(sg.Code, " - ", sg.Name)).FirstOrDefault(), String.Empty)
            query.ManufacturerDescription = If(query.ManufacturerId IsNot Nothing, (From m In _context.Manufacturer Where m.Id = query.ManufacturerId Select String.Concat(m.Code, " - ", m.Name)).FirstOrDefault(), String.Empty)
            query.IvaCodeDescription = If(query.IVAId IsNot Nothing, (From i In _context.GeneralLedgerIVA Where i.Id = query.IVAId Select String.Concat(i.Code, " - ", i.Name)).FirstOrDefault(), String.Empty)
            query.BillingGroupDescription = If(query.BillingGroupId IsNot Nothing, (From bg In _context.BillingGroup Where bg.Id = query.BillingGroupId Select String.Concat(bg.Code, " - ", bg.Name)).FirstOrDefault(), String.Empty)
            query.BillingGroupNoPOSDescription = If(query.BillingGroupId IsNot Nothing, (From bg In _context.BillingGroup Where bg.Id = query.BillingGroupNoPosId Select String.Concat(bg.Code, " - ", bg.Name)).FirstOrDefault(), String.Empty)
            query.SupplieDescription = If(query.SupplieId IsNot Nothing, (From s In _context.InventorySupplie Where s.Id = query.SupplieId Select String.Concat(s.Code, " - ", s.SupplieName)).FirstOrDefault(), String.Empty)
            query.MedicationTypeCodeName = If(query.MedicationTypeId IsNot Nothing, (From mt In _context.MedicationType.AsNoTracking() Where mt.Id = query.MedicationTypeId Select String.Concat(mt.Code, " - ", mt.Name)).FirstOrDefault(), String.Empty)

            If query.POSPathologies.Count > 0 Then
                For Each pp As POSPathologies In query.POSPathologies
                    Dim _diagnostic As Diagnostic = (From d In _context.Diagnostic Where d.Id = pp.DiagnosticId Select d).FirstOrDefault()
                    pp.DiagnosticCode = _diagnostic.Code
                    pp.DiagnosticName = _diagnostic.Name
                    Select Case pp.AgeMeasure
                        Case 1
                            pp.AgeMeasureName = "Años"
                        Case 2
                            pp.AgeMeasureName = "Meses"
                        Case 3
                            pp.AgeMeasureName = "Dias"
                        Case Else
                            pp.AgeMeasureName = ""
                    End Select
                Next
            End If
            query.HanddlesMovementKardex = _context.Kardex.Any(Function(a) a.ProductId = query.Id)

            Return query
        Else
            Return New InventoryProduct()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un producto por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetInventoryProductById(id As Integer, Optional tracking As Boolean = True) As InventoryProduct Implements IInventoryProductRepository.GetInventoryProductById
        If id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim product As InventoryProduct
        If tracking = True Then
            product = (From ip In _context.InventoryProduct.Include("ProductHierarchy").Include("ProductSubGroup").Include("ProductGroup").Include("ProductType") Where ip.Id = id Select ip).FirstOrDefault() '.Include("ProductHierarchy.InventoryProduct1")
        Else
            product = (From ip In _context.InventoryProduct.AsNoTracking().Include("ProductHierarchy").AsNoTracking().Include("ProductSubGroup").AsNoTracking().Include("ProductGroup").Include("ProductType").AsNoTracking() Where ip.Id = id Select ip).FirstOrDefault() '.Include("ProductHierarchy.InventoryProduct1")
        End If
        If product IsNot Nothing AndAlso product.Id > 0 Then
            'la usaremos para sacar la unidad de medida del producto 
            Dim productType As ProductType = (From pt In _context.ProductType.AsNoTracking() Where pt.Id = product.ProductTypeId Select pt).FirstOrDefault()
            Dim classProductType As Integer = productType.Class
            product.ProductTypeDescription = String.Concat(productType.Code, " - ", productType.Name)
            product.PackingUnitDescription = (From pu In _context.PackagingUnit Where pu.Id = product.PackagingUnitId Select String.Concat(pu.Code, " - ", pu.Name)).FirstOrDefault()
            product.GroupDescription = IIf(product.ProductGroupId IsNot Nothing, (From g In _context.ProductGroup Where g.Id = product.ProductGroupId Select String.Concat(g.Code, " - ", g.Name)).FirstOrDefault(), String.Empty)
            product.SubGroupDescription = IIf(product.ProductSubGroupId IsNot Nothing, String.Concat(product?.ProductSubGroup?.Code, " - ", product?.ProductSubGroup?.Name), String.Empty)
            product.HandlesBatch = IIf(product.ProductSubGroupId IsNot Nothing, product?.ProductSubGroup?.HandlesBatch, False)
            product.ManufacturerDescription = IIf(product.ManufacturerId IsNot Nothing, (From m In _context.Manufacturer Where m.Id = product.ManufacturerId Select String.Concat(m.Code, " - ", m.Name)).FirstOrDefault(), String.Empty)
            product.IvaCodeDescription = IIf(product.IVAId IsNot Nothing, (From i In _context.GeneralLedgerIVA Where i.Id = product.IVAId Select String.Concat(i.Code, " - ", i.Name)).FirstOrDefault(), String.Empty)
            product.PercentageIVA = IIf(product.IVAId IsNot Nothing, (From i In _context.GeneralLedgerIVA Where i.Id = product.IVAId Select i.Percentage).FirstOrDefault(), 0)
            product.IvaPercent = product.PercentageIVA
            product.BillingGroupDescription = IIf(product.BillingGroupId IsNot Nothing, (From bg In _context.BillingGroup Where bg.Id = product.BillingGroupId Select String.Concat(bg.Code, " - ", bg.Name)).FirstOrDefault(), String.Empty)
            product.PackingUnitAbbreviation = (From pu In _context.PackagingUnit Where pu.Id = product.PackagingUnitId Select pu.Abbreviation).FirstOrDefault()
            If classProductType <> 2 Then
                product.InventoryMeasureUnitAbreviation = IIf(product.MeasurementUnitId IsNot Nothing, (From mu In _context.InventoryMeasurementUnit.AsNoTracking() Where mu.Id = product.MeasurementUnitId Select mu.Abbreviation).FirstOrDefault(), String.Empty)
                product.MeasureUnitDescription = IIf(product.MeasurementUnitId IsNot Nothing, (From mu In _context.InventoryMeasurementUnit.AsNoTracking() Where mu.Id = product.MeasurementUnitId Select String.Concat(mu.Code, " - ", mu.Name)).FirstOrDefault(), String.Empty)
            Else
                Dim ProductAtc As ATC = IIf(product.ATCId IsNot Nothing, (From atc In _context.ATC Where atc.Id = product.ATCId Select atc).FirstOrDefault(), Nothing)
                If ProductAtc IsNot Nothing Then
                    product.ATCDescription = String.Format("{0} - {1}", ProductAtc.Code, ProductAtc.Name)
                    Dim formulationType = ProductAtc.FormulationType
                    Select Case formulationType
                        Case 1
                            product.InventoryMeasureUnitAbreviation = IIf(ProductAtc.WeightMeasureUnit IsNot Nothing, (From mu In _context.InventoryMeasurementUnit.AsNoTracking() Where mu.Id = ProductAtc.WeightMeasureUnit Select mu.Abbreviation).FirstOrDefault(), String.Empty)
                            product.MeasureUnitDescription = IIf(ProductAtc.WeightMeasureUnit IsNot Nothing, (From mu In _context.InventoryMeasurementUnit.AsNoTracking() Where mu.Id = ProductAtc.WeightMeasureUnit Select String.Concat(mu.Code, " - ", mu.Name)).FirstOrDefault(), String.Empty)
                        Case 2
                            product.InventoryMeasureUnitAbreviation = IIf(ProductAtc.VolumeMeasureUnit IsNot Nothing, (From mu In _context.InventoryMeasurementUnit.AsNoTracking() Where mu.Id = ProductAtc.VolumeMeasureUnit Select mu.Abbreviation).FirstOrDefault(), String.Empty)
                            product.MeasureUnitDescription = IIf(ProductAtc.VolumeMeasureUnit IsNot Nothing, (From mu In _context.InventoryMeasurementUnit.AsNoTracking() Where mu.Id = ProductAtc.VolumeMeasureUnit Select String.Concat(mu.Code, " - ", mu.Name)).FirstOrDefault(), String.Empty)
                        Case 3
                            product.InventoryMeasureUnitAbreviation = IIf(ProductAtc.WeightMeasureUnit IsNot Nothing, (From mu In _context.InventoryMeasurementUnit.AsNoTracking() Where mu.Id = ProductAtc.WeightMeasureUnit Select mu.Abbreviation).FirstOrDefault(), String.Empty)
                            product.MeasureUnitDescription = IIf(ProductAtc.VolumeMeasureUnit IsNot Nothing, (From mu In _context.InventoryMeasurementUnit.AsNoTracking() Where mu.Id = ProductAtc.VolumeMeasureUnit Select mu.Name).FirstOrDefault(), String.Empty) & " - " & IIf(ProductAtc.WeightMeasureUnit IsNot Nothing, (From mu In _context.InventoryMeasurementUnit.AsNoTracking() Where mu.Id = ProductAtc.WeightMeasureUnit Select mu.Name).FirstOrDefault(), String.Empty)
                        Case 4
                            product.InventoryMeasureUnitAbreviation = IIf(ProductAtc.AdministrationUnitId IsNot Nothing, (From mu In _context.InventoryMeasurementUnit.AsNoTracking() Where mu.Id = ProductAtc.AdministrationUnitId Select mu.Abbreviation).FirstOrDefault(), String.Empty)
                            product.MeasureUnitDescription = IIf(ProductAtc.AdministrationUnitId IsNot Nothing, (From mu In _context.InventoryMeasurementUnit.AsNoTracking() Where mu.Id = ProductAtc.AdministrationUnitId Select String.Concat(mu.Code, " - ", mu.Name)).FirstOrDefault(), String.Empty)
                    End Select
                End If
            End If

            If product.ProductGroup IsNot Nothing Then
                'Dim producGroup = (From pg In _context.ProductGroup.AsNoTracking() Where pg.Id = product.ProductGroupId Select pg).FirstOrDefault()
                'product.ProductGroup = producGroup

                Dim accountWithholdingSource = (From aws In _context.AccountPayableConcepts.Include("MainAccounts") Where aws.Id = product.ProductGroup.DeclarantRetentionAccountPayableConceptId Select aws).FirstOrDefault()
                If accountWithholdingSource.RetentionConceptId IsNot Nothing Then
                    product.WithholdingSourcePercernt = (From rc In _context.RetentionConcepts Where accountWithholdingSource.RetentionConceptId = rc.Id Select rc.Rate).FirstOrDefault()
                End If
                If accountWithholdingSource.MainAccounts Is Nothing Then
                    Throw New Exception("El concepto de Retencion para declarantes no tiene asociada una cuenta contable")
                End If
                product.HandlesCostCenterWithholdigSourceAccount = accountWithholdingSource.MainAccounts.HandlesCostCenter
                product.AccountWithholdingSourceId = accountWithholdingSource.IdAccount
                product.RetentionConceptsWithholdingSourceId = accountWithholdingSource.RetentionConceptId
                product.ConceptAccountPayableWithholdingSourceId = accountWithholdingSource.Id

                Dim accountInventory = (From aws In _context.AccountPayableConcepts.Include("MainAccounts") Where aws.Id = product.ProductGroup.InventoryAccountPayableConceptId Select aws).FirstOrDefault()
                If accountInventory.MainAccounts Is Nothing Then
                    Throw New Exception("El concepto de CxP de Inventarios no tiene asociada una cuenta contable")
                End If
                product.HandlesThirdPartyAccount = accountInventory.MainAccounts.HandlesThirdParty
                product.AccountInventoryCodeName = accountInventory.MainAccounts.Number + " - " + accountInventory.MainAccounts.Name
                product.AccountInventoryId = accountInventory.IdAccount
                product.AccountInventoryHandlessCostCenter = accountInventory.MainAccounts.HandlesCostCenter
                product.ConceptAccountPayableInventory = accountInventory.Id

                If product.ProductGroup.ReteFuenteConceptId IsNot Nothing Then
                    product.RetentionPercentageTax = (From r In _context.RetentionConcepts Where r.Id = product.ProductGroup.ReteFuenteConceptId Select r.Rate).FirstOrDefault()
                End If

                If product.ProductGroup.WithholdingICAConceptId IsNot Nothing Then
                    product.RetentionPercentageICA = (From r In _context.RetentionConcepts Where r.Id = product.ProductGroup.WithholdingICAConceptId Select r.Rate).FirstOrDefault()
                End If
            Else
                product.WithholdingSourcePercernt = Nothing
                product.AccountWithholdingSourceId = Nothing
                product.RetentionConceptsWithholdingSourceId = Nothing
                product.ConceptAccountPayableWithholdingSourceId = Nothing
                product.AccountInventoryId = Nothing
                product.ConceptAccountPayableInventory = Nothing
            End If
            Return product
        Else
            Return New InventoryProduct()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un producto por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetInventoryProductByIdSimple(id As Integer, Optional tracking As Boolean = True) As InventoryProduct Implements IInventoryProductRepository.GetInventoryProductByIdSimple
        If id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim product As InventoryProduct
        If tracking = True Then
            product = (From ip In _context.InventoryProduct.Include("ProductSubGroup").AsNoTracking().Include("ProductGroup").AsNoTracking().Include("ProductGroup.ProductGroupFunctionalUnit").AsNoTracking() Where ip.Id = id Select ip).FirstOrDefault() '.Include("ProductHierarchy.InventoryProduct1")
            If product IsNot Nothing AndAlso product.Id > 0 Then
                product.ManufacturerDescription = IIf(product.ManufacturerId IsNot Nothing, (From m In _context.Manufacturer Where m.Id = product.ManufacturerId Select String.Concat(m.Code, " - ", m.Name)).FirstOrDefault(), String.Empty)
                product.PackingUnitDescription = (From pu In _context.PackagingUnit Where pu.Id = product.PackagingUnitId Select String.Concat(pu.Code, " - ", pu.Name)).FirstOrDefault()
                Dim IVAPercent = (From i In _context.GeneralLedgerIVA Where i.Id = product.IVAId Select i)?.FirstOrDefault()?.Percentage
                If IVAPercent IsNot Nothing Then
                    product.IvaPercent = IVAPercent
                    product.PercentageIVA = IVAPercent
                End If
                'la usaremos para sacar la unidad de medida del producto 
                Dim productType As ProductType = (From pt In _context.ProductType.AsNoTracking() Where pt.Id = product.ProductTypeId Select pt).FirstOrDefault()
                Dim classProductType As Integer = productType.Class
                If classProductType <> 2 Then
                    product.MeasureUnitDescription = IIf(product.MeasurementUnitId IsNot Nothing, (From mu In _context.InventoryMeasurementUnit.AsNoTracking() Where mu.Id = product.MeasurementUnitId Select String.Concat(mu.Code, " - ", mu.Name)).FirstOrDefault(), String.Empty)
                Else
                    Dim ProductAtc As ATC = IIf(product.ATCId IsNot Nothing, (From atc In _context.ATC Where atc.Id = product.ATCId Select atc).FirstOrDefault(), Nothing)
                    If ProductAtc IsNot Nothing Then
                        product.ATCDescription = String.Concat(ProductAtc.Code, " - ", ProductAtc.Name)
                        Dim formulationType = ProductAtc.FormulationType
                        Select Case formulationType
                            Case 1
                                product.MeasureUnitDescription = IIf(ProductAtc.WeightMeasureUnit IsNot Nothing, (From mu In _context.InventoryMeasurementUnit.AsNoTracking() Where mu.Id = ProductAtc.WeightMeasureUnit Select String.Concat(mu.Code, " - ", mu.Name)).FirstOrDefault(), String.Empty)
                            Case 2
                                product.MeasureUnitDescription = IIf(ProductAtc.VolumeMeasureUnit IsNot Nothing, (From mu In _context.InventoryMeasurementUnit.AsNoTracking() Where mu.Id = ProductAtc.VolumeMeasureUnit Select String.Concat(mu.Code, " - ", mu.Name)).FirstOrDefault(), String.Empty)
                            Case 3
                                product.MeasureUnitDescription = IIf(ProductAtc.VolumeMeasureUnit IsNot Nothing, (From mu In _context.InventoryMeasurementUnit.AsNoTracking() Where mu.Id = ProductAtc.VolumeMeasureUnit Select mu.Name).FirstOrDefault(), String.Empty) & " - " & IIf(ProductAtc.WeightMeasureUnit IsNot Nothing, (From mu In _context.InventoryMeasurementUnit.AsNoTracking() Where mu.Id = ProductAtc.WeightMeasureUnit Select mu.Name).FirstOrDefault(), String.Empty)
                            Case 4
                                product.MeasureUnitDescription = IIf(ProductAtc.AdministrationUnitId IsNot Nothing, (From mu In _context.InventoryMeasurementUnit.AsNoTracking() Where mu.Id = ProductAtc.AdministrationUnitId Select String.Concat(mu.Code, " - ", mu.Name)).FirstOrDefault(), String.Empty)
                        End Select
                    End If
                End If
            End If
        Else
            product = (From ip In _context.InventoryProduct.AsNoTracking().Include("ProductSubGroup").AsNoTracking().Include("ProductGroup").AsNoTracking().Include("ProductGroup.ProductGroupFunctionalUnit").AsNoTracking() Where ip.Id = id Select ip).FirstOrDefault() '.Include("ProductHierarchy.InventoryProduct1")
            If product IsNot Nothing AndAlso product.Id > 0 Then
                product.ManufacturerDescription = IIf(product.ManufacturerId IsNot Nothing, (From m In _context.Manufacturer.AsNoTracking() Where m.Id = product.ManufacturerId Select String.Concat(m.Code, " - ", m.Name)).FirstOrDefault(), String.Empty)
                Dim packingUnit = (From pu In _context.PackagingUnit.AsNoTracking() Where pu.Id = product.PackagingUnitId Select pu).FirstOrDefault()
                product.PackingUnitDescription = packingUnit.Code + " - " + packingUnit.Name
                'la usaremos para sacar la unidad de medida del producto 
                Dim productType As ProductType = (From pt In _context.ProductType.AsNoTracking() Where pt.Id = product.ProductTypeId Select pt).FirstOrDefault()
                Dim classProductType As Integer = productType.Class
                If classProductType <> 2 Then
                    product.MeasureUnitDescription = IIf(product.MeasurementUnitId IsNot Nothing, (From mu In _context.InventoryMeasurementUnit.AsNoTracking() Where mu.Id = product.MeasurementUnitId Select String.Concat(mu.Code, " - ", mu.Name)).FirstOrDefault(), String.Empty)
                Else
                    Dim ProductAtc As ATC = IIf(product.ATCId IsNot Nothing, (From atc In _context.ATC Where atc.Id = product.ATCId Select atc).FirstOrDefault(), Nothing)
                    If ProductAtc IsNot Nothing Then
                        product.ATCDescription = String.Concat(ProductAtc.Code, " - ", ProductAtc.Name)
                        Dim formulationType = ProductAtc.FormulationType
                        Select Case formulationType
                            Case 1
                                product.MeasureUnitDescription = IIf(ProductAtc.WeightMeasureUnit IsNot Nothing, (From mu In _context.InventoryMeasurementUnit.AsNoTracking() Where mu.Id = ProductAtc.WeightMeasureUnit Select String.Concat(mu.Code, " - ", mu.Name)).FirstOrDefault(), String.Empty)
                            Case 2
                                product.MeasureUnitDescription = IIf(ProductAtc.VolumeMeasureUnit IsNot Nothing, (From mu In _context.InventoryMeasurementUnit.AsNoTracking() Where mu.Id = ProductAtc.VolumeMeasureUnit Select String.Concat(mu.Code, " - ", mu.Name)).FirstOrDefault(), String.Empty)
                            Case 3
                                product.MeasureUnitDescription = IIf(ProductAtc.VolumeMeasureUnit IsNot Nothing, (From mu In _context.InventoryMeasurementUnit.AsNoTracking() Where mu.Id = ProductAtc.VolumeMeasureUnit Select mu.Name).FirstOrDefault(), String.Empty) & " - " & IIf(ProductAtc.WeightMeasureUnit IsNot Nothing, (From mu In _context.InventoryMeasurementUnit.AsNoTracking() Where mu.Id = ProductAtc.WeightMeasureUnit Select mu.Name).FirstOrDefault(), String.Empty)
                            Case 4
                                product.MeasureUnitDescription = IIf(ProductAtc.AdministrationUnitId IsNot Nothing, (From mu In _context.InventoryMeasurementUnit.AsNoTracking() Where mu.Id = ProductAtc.AdministrationUnitId Select String.Concat(mu.Code, " - ", mu.Name)).FirstOrDefault(), String.Empty)
                        End Select
                    End If
                End If
            End If
        End If
        If product IsNot Nothing AndAlso product.Id > 0 Then
            product.OriginalValue = (From ip In _context.InventoryProduct.AsNoTracking() Where ip.Id = id Select ip).FirstOrDefault()
            Return product
        Else
            Return New InventoryProduct()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un producto por id sin agregados
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetInventoryProductByIdWithoutAggregates(id As Integer) As InventoryProduct Implements IInventoryProductRepository.GetInventoryProductByIdWithoutAggregates
        If id = 0 Then
            Throw New ArgumentNullException("Id")
        End If

        Dim product As InventoryProduct = (From ip In _context.InventoryProduct.AsNoTracking().Include("ProductSubGroup").AsNoTracking().Include("ProductGroup").AsNoTracking() Where ip.Id = id Select ip).FirstOrDefault()
        If product IsNot Nothing AndAlso product.Id > 0 Then
            product.HandlesBatch = (From sg In _context.ProductSubGroup.AsNoTracking() Where sg.Id = product.ProductSubGroupId Select sg.HandlesBatch).FirstOrDefault()

            If product.IVAId IsNot Nothing Then
                product.PercentageIVA = (From i In _context.GeneralLedgerIVA Where i.Id = product.IVAId Select i.Percentage).FirstOrDefault()
            End If

            Dim group = (From g In _context.ProductGroup.AsNoTracking() Where g.Id = product.ProductGroupId Select g).FirstOrDefault()
            If group.ReteFuenteConceptId IsNot Nothing Then
                Dim tax = (From r In _context.RetentionConcepts Where r.Id = group.ReteFuenteConceptId Select r).FirstOrDefault()
                product.RetentionIdTax = tax.Id
                product.RetentionPercentageTax = tax.Rate
                product.RetentionBaseTax = tax.MinBase
            End If

            If group.WithholdingICAConceptId IsNot Nothing Then
                Dim tax = (From r In _context.RetentionConcepts Where r.Id = group.WithholdingICAConceptId Select r).FirstOrDefault()
                product.RetentionIdICA = tax.Id
                product.RetentionPercentageICA = tax.Rate
                product.RetentionBaseICA = tax.MinBase
            End If

            Return product
        Else
            Return New InventoryProduct()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un producto por id sin agregados
    ''' </summary>
    ''' <param name="code">The identifier.</param>
    ''' <returns></returns>
    Public Function GetInventoryProductByCodeWithoutAggregates(code As String) As InventoryProduct Implements IInventoryProductRepository.GetInventoryProductByCodeWithoutAggregates
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Code")
        End If

        Dim query As InventoryProduct = Nothing
        Dim codeSplit As String()
        If code.ToUpper().Contains("*IND*") Then
            codeSplit = code.Split("*IND*")
        Else
            codeSplit = code.Split(" - ")
        End If
        Dim productCode = codeSplit.ElementAt(0)

        Dim product As InventoryProduct = (From ip In _context.InventoryProduct.AsNoTracking().Include("ProductSubGroup").AsNoTracking().Include("ProductGroup").AsNoTracking() Where ip.Code.Equals(productCode) Select ip).FirstOrDefault()
        If product IsNot Nothing AndAlso product.Id > 0 Then
            product.HandlesBatch = (From sg In _context.ProductSubGroup.AsNoTracking() Where sg.Id = product.ProductSubGroupId Select sg.HandlesBatch).FirstOrDefault()

            If product.IVAId IsNot Nothing Then
                product.PercentageIVA = (From i In _context.GeneralLedgerIVA Where i.Id = product.IVAId Select i.Percentage).FirstOrDefault()
            End If

            Dim group = (From g In _context.ProductGroup.AsNoTracking() Where g.Id = product.ProductGroupId Select g).FirstOrDefault()
            If group.ReteFuenteConceptId IsNot Nothing Then
                Dim tax = (From r In _context.RetentionConcepts Where r.Id = group.ReteFuenteConceptId Select r).FirstOrDefault()
                product.RetentionIdTax = tax.Id
                product.RetentionPercentageTax = tax.Rate
                product.RetentionBaseTax = tax.MinBase
            End If

            If group.WithholdingICAConceptId IsNot Nothing Then
                Dim tax = (From r In _context.RetentionConcepts Where r.Id = group.WithholdingICAConceptId Select r).FirstOrDefault()
                product.RetentionIdICA = tax.Id
                product.RetentionPercentageICA = tax.Rate
                product.RetentionBaseICA = tax.MinBase
            End If

            Return product
        Else
            Return New InventoryProduct()
        End If
    End Function

#End Region

    ''' <summary>
    ''' metodo para obtener un producto por codigo con un agrergado de subgrupo, para utilizarlo en control de inventario
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns><remarks>
    ''' no tiene original Value
    ''' </remarks>
    Public Function GetInventoryProductInventoryControl(code As String) As InventoryProduct Implements IInventoryProductRepository.GetInventoryProductInventoryControl
        Return (From ip In _context.InventoryProduct.Include("ProductSubGroup") Where ip.Code = code Select ip).FirstOrDefault()
    End Function


    ''' <summary>
    ''' Obtiene un producto por id con el grupo como agregado
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    Public Function GetInventoryProductByIdWithProducGroup(id As Integer, Optional tracking As Boolean = True) As InventoryProduct Implements IInventoryProductRepository.GetInventoryProductByIdWithProducGroup
        If tracking Then
            Return (From p In _context.InventoryProduct.Include("ProductGroup").Include("ProductGroup.ProductGroupFunctionalUnit") Where p.Id = id Select p).FirstOrDefault()
        Else
            Return (From p In _context.InventoryProduct.AsNoTracking().Include("ProductGroup").AsNoTracking().Include("ProductGroup.ProductGroupFunctionalUnit").AsNoTracking() Where p.Id = id Select p).FirstOrDefault()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un producto por codigo con el grupo como agregado
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetInventoryProductByCodeWithProducGroup(code As String, Optional tracking As Boolean = True) As InventoryProduct Implements IInventoryProductRepository.GetInventoryProductByCodeWithProducGroup
        Dim product As InventoryProduct
        If tracking = True Then
            product = (From ip In _context.InventoryProduct.Include("ProductSubGroup").AsNoTracking().Include("ProductGroup").AsNoTracking().Include("GeneralLedgerIVA").AsNoTracking() Where ip.Code = code Select ip).FirstOrDefault() '.Include("ProductHierarchy.InventoryProduct1")
        Else
            product = (From ip In _context.InventoryProduct.AsNoTracking().Include("ProductSubGroup").AsNoTracking().Include("ProductGroup").AsNoTracking().Include("GeneralLedgerIVA").AsNoTracking() Where ip.Code = code Select ip).FirstOrDefault() '.Include("ProductHierarchy.InventoryProduct1")
        End If
        If product IsNot Nothing AndAlso product.Id > 0 Then
            Dim IVAPercent = product?.GeneralLedgerIVA?.Percentage
            If IVAPercent IsNot Nothing Then
                product.PercentageIVA = IVAPercent
                product.IvaPercent = IVAPercent
            End If
            product.OriginalValue = (From ip In _context.InventoryProduct.AsNoTracking() Where ip.Code = code Select ip).FirstOrDefault()
            Return product
        Else
            Return New InventoryProduct()
        End If
    End Function

    Public Function GetInventoryProductByCodeList(listCode As List(Of String)) As List(Of InventoryProduct) Implements IInventoryProductRepository.GetInventoryProductByCodeList
        Return (From ip In _context.InventoryProduct.AsNoTracking().Include("ATC").AsNoTracking().Include("ProductType").AsNoTracking() Where listCode.Contains(ip.Code) Select ip).ToList()
    End Function

    Public Function GetFirstProductByATCCode(ATCCode As String) As InventoryProduct Implements IInventoryProductRepository.GetFirstProductByATCCode
        Return (From p In _context.InventoryProduct.AsNoTracking()
                Join a In _context.ATC.AsNoTracking() On p.ATCId Equals a.Id
                Where a.Code.Equals(ATCCode) Select p).FirstOrDefault()
    End Function

    Public Function GetFirstProductByTypeProdcutCode(ATCCode As String, TypeProduct As String) As InventoryProduct Implements IInventoryProductRepository.GetFirstProductByTypeProdcutCode
        Return (From p In _context.InventoryProduct.AsNoTracking()
                Join t In _context.ProductType.AsNoTracking() On p.ProductTypeId Equals t.Id
                Where p.Code.Equals(ATCCode) And t.Code = TypeProduct Select p).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Guarda los productos en las tablas de crystal
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    Public Function SP_SaveProductInCrystal(data As String) As SP_SaveProductInCrystal_Result Implements IInventoryProductRepository.SP_SaveProductInCrystal
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveProductInCrystal(data).SingleOrDefault
    End Function

    ''' <summary>
    ''' Obtiene el tipo de producto por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function GetClassOfProductTypeById(Id As Integer) As Byte Implements IInventoryProductRepository.GetClassOfProductTypeById
        Return (From x In _context.ProductType.AsNoTracking() Where x.Id = Id Select x.Class).FirstOrDefault()
    End Function

    Public Function GetInventoryProductByBarCode(barcode As String) As InventoryProduct Implements IInventoryProductRepository.GetInventoryProductByBarCode
        Return (From p In _context.InventoryProduct.AsNoTracking()
                Join b In _context.ProductBarcode.AsNoTracking() On p.Id Equals b.ProductId
                Where b.Barcode.Trim().Equals(barcode.Trim())
                Select p).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Guarda el producto
    ''' </summary>
    ''' <param name="Xml"></param>
    ''' <param name="UserCode"></param>
    ''' <param name="OperatingUnitId"></param>
    ''' <returns></returns>
    Public Function SP_SaveInventoryProduct(Xml As String, UserCode As String, OperatingUnitId As Integer) As SP_SaveInventoryProduct_Result Implements IInventoryProductRepository.SP_SaveInventoryProduct
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveInventoryProduct(Xml, UserCode, OperatingUnitId).SingleOrDefault()
    End Function

    Public Async Function SP_SaveInventoryProductAsync(Xml As String, UserCode As String, OperatingUnitId As Integer) As Task(Of SP_SaveInventoryProduct_Result) Implements IInventoryProductRepository.SP_SaveInventoryProductAsync
        Return Await Task.Run(Function()
                                  DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
                                  Return _context.SP_SaveInventoryProduct(Xml, UserCode, OperatingUnitId).SingleOrDefault()
                              End Function)
    End Function

#Region "Versiones Asíncronas Optimizadas"

    ''' <summary>
    ''' VERSIÓN OPTIMIZADA Y ASÍNCRONA - Obtiene un producto por código
    ''' </summary>
    ''' <param name="code">Código del producto</param>
    ''' <param name="includeRelatedData">Si debe incluir datos relacionados (descripciones, etc.)</param>
    ''' <param name="tracking">Si debe hacer tracking de cambios</param>
    ''' <returns>Producto encontrado o nuevo InventoryProduct si no existe</returns>
    Public Async Function GetInventoryProductAsync(code As String, Optional includeRelatedData As Boolean = True, Optional tracking As Boolean = True) As Task(Of InventoryProduct) Implements IInventoryProductRepository.GetInventoryProductAsync
        Try
            ' ✅ MEJORA 1: Validación temprana
            If String.IsNullOrWhiteSpace(code) Then
                Throw New ArgumentException("El código del producto no puede estar vacío", "code")
            End If

            ' ✅ MEJORA 2: Limpiar y preparar el código
            Dim cleanCode = PrepareProductCode(code)

            ' ✅ MEJORA 3: Búsqueda optimizada con una sola consulta
            Dim product = Await GetProductByCodeOptimizedAsync(cleanCode, tracking)

            ' ✅ MEJORA 4: Si no se encuentra por código, intentar por código de barras
            If (product Is Nothing OrElse product.Id = 0) Then
                product = Await GetProductByBarcodeOptimizedAsync(cleanCode, tracking)
            End If

            ' ✅ MEJORA 5: Si no se encuentra, retornar producto vacío
            If product Is Nothing OrElse product.Id = 0 Then
                Return New InventoryProduct()
            End If

            ' ✅ MEJORA 6: Cargar datos relacionados si se solicita
            If includeRelatedData Then
                Await LoadProductRelatedDataAsync(product)
            End If

            Return product

        Catch ex As ArgumentException
            ' Re-lanzar excepciones de validación - son problemas del llamador
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Throw ex
        Catch ex As System.Data.Entity.Core.EntityException
            ' Error específico de Entity Framework
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Throw New Exception($"Error de base de datos al obtener el producto con código '{code}': {ex.Message}", ex)
        Catch ex As InvalidOperationException
            ' Error de operación inválida - contexto cerrado o consulta mal formada
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Throw New Exception($"Error de operación al obtener el producto con código '{code}': {ex.Message}", ex)
        Catch ex As TimeoutException
            ' Timeout en la consulta
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Throw New Exception($"Timeout al obtener el producto con código '{code}': {ex.Message}", ex)
        Catch ex As Exception
            ' Error general - logging completo
            IndigoManagementExceptions.HandleException(New Exception($"Error inesperado al obtener el producto con código '{code}'", ex), "ApplicationPolicy")
            Throw New Exception($"Error inesperado al obtener el producto con código '{code}': {ex.Message}", ex)
        End Try
    End Function

    ''' <summary>
    ''' Versión ligera asíncrona sin datos relacionados para mejor rendimiento
    ''' </summary>
    ''' <param name="code">Código del producto</param>
    ''' <returns>Producto básico sin descripciones adicionales</returns>
    Public Async Function GetInventoryProductLightAsync(code As String) As Task(Of InventoryProduct) Implements IInventoryProductRepository.GetInventoryProductLightAsync
        Return Await GetInventoryProductAsync(code, includeRelatedData:=False, tracking:=False)
    End Function

    ''' <summary>
    ''' Prepara el código del producto eliminando prefijos especiales
    ''' </summary>
    Private Function PrepareProductCode(code As String) As String
        Try
            If String.IsNullOrWhiteSpace(code) Then
                Return String.Empty
            End If

            Dim codeSplit As String()
            If code.ToUpper().Contains("*IND*") Then
                codeSplit = code.Split({"*IND*"}, StringSplitOptions.None)
            Else
                codeSplit = code.Split({" - "}, StringSplitOptions.None)
            End If

            If codeSplit.Length > 0 Then
                Return codeSplit.ElementAt(0).Trim()
            Else
                Return code.Trim()
            End If

        Catch ex As ArgumentException
            ' Error al procesar el código - devolver código original limpio
            IndigoManagementExceptions.HandleException(New Exception($"Error de argumentos al preparar código de producto: {code}", ex), "ApplicationPolicy")
            Return If(code IsNot Nothing, code.Trim(), String.Empty)
        Catch ex As InvalidOperationException
            ' Error en ElementAt - devolver código original
            IndigoManagementExceptions.HandleException(New Exception($"Error de operación al preparar código de producto: {code}", ex), "ApplicationPolicy")
            Return If(code IsNot Nothing, code.Trim(), String.Empty)
        Catch ex As Exception
            ' Error general al preparar código
            IndigoManagementExceptions.HandleException(New Exception($"Error inesperado al preparar código de producto: {code}", ex), "ApplicationPolicy")
            Return If(code IsNot Nothing, code.Trim(), String.Empty)
        End Try
    End Function

    ''' <summary>
    ''' Obtiene el producto por código con consulta optimizada
    ''' </summary>
    Private Async Function GetProductByCodeOptimizedAsync(productCode As String, tracking As Boolean) As Task(Of InventoryProduct)
        Try
            Dim query = _context.InventoryProduct _
                .Include("ProductHierarchy") _
                .Include("ProductBarcode") _
                .Include("POSPathologies") _
                .Include("InventoryProductAttribute") _
                .Include("ProductHierarchy2.InventoryProduct1") _
                .Include("ProductType") _
                .Include("PhysicalInventory.Warehouse") _
                .Where(Function(ip) ip.Code.Equals(productCode))

            If Not tracking Then
                query = query.AsNoTracking()
            End If

            Return Await query.FirstOrDefaultAsync()

        Catch ex As System.Data.Entity.Core.EntityException
            ' Error específico de Entity Framework
            IndigoManagementExceptions.HandleException(New Exception($"Error de Entity Framework al buscar producto por código: {productCode}", ex), "ApplicationPolicy")
            Return Nothing
        Catch ex As InvalidOperationException
            ' Error de operación inválida - contexto cerrado o consulta mal formada
            IndigoManagementExceptions.HandleException(New Exception($"Error de operación al buscar producto por código: {productCode}", ex), "ApplicationPolicy")
            Return Nothing
        Catch ex As ArgumentException
            ' Error de argumentos - código inválido
            IndigoManagementExceptions.HandleException(New Exception($"Error de argumentos al buscar producto por código: {productCode}", ex), "ApplicationPolicy")
            Return Nothing
        Catch ex As Exception
            ' Error general
            IndigoManagementExceptions.HandleException(New Exception($"Error inesperado al buscar producto por código: {productCode}", ex), "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene el producto por código de barras con consulta optimizada
    ''' </summary>
    Private Async Function GetProductByBarcodeOptimizedAsync(productCode As String, tracking As Boolean) As Task(Of InventoryProduct)
        Try
            ' Primero buscar el código de barras
            Dim productBar = Await _context.ProductBarcode _
                .AsNoTracking() _
                .Where(Function(pbc) pbc.Barcode = productCode) _
                .FirstOrDefaultAsync()

            If productBar Is Nothing Then
                Return Nothing
            End If

            ' Luego obtener el producto por ID
            Dim query = _context.InventoryProduct _
                .Include("ProductHierarchy") _
                .Include("ProductBarcode") _
                .Include("POSPathologies") _
                .Include("InventoryProductAttribute") _
                .Include("ProductHierarchy2.InventoryProduct1") _
                .Include("ProductType") _
                .Include("PhysicalInventory.Warehouse") _
                .Where(Function(ip) ip.Id = productBar.ProductId)

            If Not tracking Then
                query = query.AsNoTracking()
            End If

            Return Await query.FirstOrDefaultAsync()

        Catch ex As System.Data.Entity.Core.EntityException
            ' Error específico de Entity Framework al buscar por código de barras
            IndigoManagementExceptions.HandleException(New Exception($"Error de Entity Framework al buscar producto por código de barras: {productCode}", ex), "ApplicationPolicy")
            Return Nothing
        Catch ex As InvalidOperationException
            ' Error de operación inválida - contexto cerrado o consulta mal formada
            IndigoManagementExceptions.HandleException(New Exception($"Error de operación al buscar producto por código de barras: {productCode}", ex), "ApplicationPolicy")
            Return Nothing
        Catch ex As ArgumentException
            ' Error de argumentos - código de barras inválido
            IndigoManagementExceptions.HandleException(New Exception($"Error de argumentos al buscar producto por código de barras: {productCode}", ex), "ApplicationPolicy")
            Return Nothing
        Catch ex As Exception
            ' Error general al buscar por código de barras
            IndigoManagementExceptions.HandleException(New Exception($"Error inesperado al buscar producto por código de barras: {productCode}", ex), "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Carga los datos relacionados del producto de forma optimizada
    ''' </summary>
    Private Async Function LoadProductRelatedDataAsync(product As InventoryProduct) As Task
        Try
            ' Ejecutar secuencialmente para evitar conflictos de concurrencia de Entity Framework
            ' El contexto de EF no es thread-safe y no permite operaciones paralelas

            ' Cargar descripciones básicas
            Await LoadBasicDescriptionsAsync(product)

            ' Cargar información de ATC si existe
            If product.ATCId IsNot Nothing Then
                Await LoadATCRelatedDataAsync(product)
            End If

            ' Cargar patologías POS si existen
            If product.POSPathologies?.Count > 0 Then
                Await LoadPOSPathologiesDataAsync(product)
            End If

            ' Cargar información de Kardex
            Await LoadKardexInfoAsync(product)

            ' Establecer el valor original
            product.OriginalValue = Await _context.InventoryProduct _
            .AsNoTracking() _
            .Where(Function(ip) ip.Id = product.Id) _
            .FirstOrDefaultAsync()

        Catch ex As System.Data.Entity.Core.EntityException
            ' Error específico de Entity Framework - logging detallado
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            ' Continuar con el producto básico sin fallar completamente
        Catch ex As InvalidOperationException
            ' Error de operación inválida - posiblemente contexto cerrado
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            ' Continuar con el producto básico
        Catch ex As Exception
            ' Error general - logging y continuar sin datos relacionados
            IndigoManagementExceptions.HandleException(New Exception($"Error al cargar datos relacionados para producto ID: {product.Id}", ex), "ApplicationPolicy")
            ' No re-lanzar para no fallar la operación principal
        End Try
    End Function

    ''' <summary>
    ''' Carga las descripciones básicas del producto
    ''' </summary>
    Private Async Function LoadBasicDescriptionsAsync(product As InventoryProduct) As Task
        Try
            ' Consulta optimizada para obtener todas las descripciones en una sola operación
            Dim basicDataQuery = From ip In _context.InventoryProduct.AsNoTracking()
                                 Where ip.Id = product.Id
                                 Select New With {
                                     .ProductType = (From pt In _context.ProductType Where pt.Id = ip.ProductTypeId Select pt).FirstOrDefault(),
                                     .PackagingUnit = (From pu In _context.PackagingUnit Where pu.Id = ip.PackagingUnitId Select pu).FirstOrDefault(),
                                     .ProductGroup = (From g In _context.ProductGroup Where g.Id = ip.ProductGroupId Select g).FirstOrDefault(),
                                     .ProductSubGroup = (From sg In _context.ProductSubGroup Where sg.Id = ip.ProductSubGroupId Select sg).FirstOrDefault(),
                                     .Manufacturer = If(ip.ManufacturerId IsNot Nothing, (From m In _context.Manufacturer Where m.Id = ip.ManufacturerId Select m).FirstOrDefault(), Nothing),
                                     .IVA = If(ip.IVAId IsNot Nothing, (From i In _context.GeneralLedgerIVA Where i.Id = ip.IVAId Select i).FirstOrDefault(), Nothing),
                                     .BillingGroup = If(ip.BillingGroupId IsNot Nothing, (From bg In _context.BillingGroup Where bg.Id = ip.BillingGroupId Select bg).FirstOrDefault(), Nothing),
                                     .BillingGroupNoPos = If(ip.BillingGroupNoPosId IsNot Nothing, (From bg In _context.BillingGroup Where bg.Id = ip.BillingGroupNoPosId Select bg).FirstOrDefault(), Nothing),
                                     .Supplie = If(ip.SupplieId IsNot Nothing, (From s In _context.InventorySupplie Where s.Id = ip.SupplieId Select s).FirstOrDefault(), Nothing),
                                     .MedicationType = If(ip.MedicationTypeId IsNot Nothing, (From mt In _context.MedicationType Where mt.Id = ip.MedicationTypeId Select mt).FirstOrDefault(), Nothing)
                                 }

            Dim basicData = Await basicDataQuery.FirstOrDefaultAsync()

            If basicData IsNot Nothing Then
                ' Asignar descripciones
                product.ProductTypeDescription = If(basicData.ProductType IsNot Nothing, $"{basicData.ProductType.Code} - {basicData.ProductType.Name}", String.Empty)
                product.PackingUnitDescription = If(basicData.PackagingUnit IsNot Nothing, $"{basicData.PackagingUnit.Code} - {basicData.PackagingUnit.Name}", String.Empty)
                product.GroupDescription = If(basicData.ProductGroup IsNot Nothing, $"{basicData.ProductGroup.Code} - {basicData.ProductGroup.Name}", String.Empty)
                product.SubGroupDescription = If(basicData.ProductSubGroup IsNot Nothing, $"{basicData.ProductSubGroup.Code} - {basicData.ProductSubGroup.Name}", String.Empty)
                product.ManufacturerDescription = If(basicData.Manufacturer IsNot Nothing, $"{basicData.Manufacturer.Code} - {basicData.Manufacturer.Name}", String.Empty)
                product.IvaCodeDescription = If(basicData.IVA IsNot Nothing, $"{basicData.IVA.Code} - {basicData.IVA.Name}", String.Empty)
                product.BillingGroupDescription = If(basicData.BillingGroup IsNot Nothing, $"{basicData.BillingGroup.Code} - {basicData.BillingGroup.Name}", String.Empty)
                product.BillingGroupNoPOSDescription = If(basicData.BillingGroupNoPos IsNot Nothing, $"{basicData.BillingGroupNoPos.Code} - {basicData.BillingGroupNoPos.Name}", String.Empty)
                product.SupplieDescription = If(basicData.Supplie IsNot Nothing, $"{basicData.Supplie.Code} - {basicData.Supplie.SupplieName}", String.Empty)
                product.MedicationTypeCodeName = If(basicData.MedicationType IsNot Nothing, $"{basicData.MedicationType.Code} - {basicData.MedicationType.Name}", String.Empty)
            End If

        Catch ex As System.Data.Entity.Core.EntityException
            ' Error específico de Entity Framework al cargar descripciones básicas
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            ' Mantener valores por defecto (String.Empty ya están asignados)
        Catch ex As ArgumentException
            ' Error de argumentos inválidos
            IndigoManagementExceptions.HandleException(New Exception($"Error de argumentos al cargar descripciones básicas para producto ID: {product.Id}", ex), "ApplicationPolicy")
        Catch ex As InvalidOperationException
            ' Error de operación inválida - contexto cerrado o consulta mal formada
            IndigoManagementExceptions.HandleException(New Exception($"Error de operación al cargar descripciones básicas para producto ID: {product.Id}", ex), "ApplicationPolicy")
        Catch ex As Exception
            ' Error general al cargar descripciones básicas
            IndigoManagementExceptions.HandleException(New Exception($"Error inesperado al cargar descripciones básicas para producto ID: {product.Id}", ex), "ApplicationPolicy")
        End Try
    End Function

    ''' <summary>
    ''' Carga los datos relacionados con ATC
    ''' </summary>
    Private Async Function LoadATCRelatedDataAsync(product As InventoryProduct) As Task
        Try
            Dim atc = Await _context.ATC _
                .AsNoTracking() _
                .Where(Function(a) a.Id = product.ATCId) _
                .FirstOrDefaultAsync()

            If atc IsNot Nothing Then
                product.ATCDescription = $"{atc.Code} - {atc.Name}"

                ' Configurar InventoryRiskLevel si el producto no lo tiene
                If product.InventoryRiskLevelId Is Nothing And atc.InventoryRiskLevelId > 0 Then
                    product.InventoryRiskLevelId = atc.InventoryRiskLevelId
                End If

                ' Obtener descripción del nivel de riesgo
                If product.InventoryRiskLevelId IsNot Nothing Then
                    Dim riskLevel = Await _context.InventoryRiskLevel _
                        .AsNoTracking() _
                        .Where(Function(r) r.Id = product.InventoryRiskLevelId) _
                        .FirstOrDefaultAsync()

                    product.InventoryRiskLevelDescription = If(riskLevel IsNot Nothing, $"{riskLevel.Code} - {riskLevel.Name}", String.Empty)
                End If
            End If

        Catch ex As System.Data.Entity.Core.EntityException
            ' Error específico de Entity Framework al cargar datos ATC
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            ' Mantener ATCDescription vacía
        Catch ex As InvalidOperationException
            ' Error de operación inválida
            IndigoManagementExceptions.HandleException(New Exception($"Error de operación al cargar datos ATC para producto ID: {product.Id}, ATC ID: {product.ATCId}", ex), "ApplicationPolicy")
        Catch ex As Exception
            ' Error general al cargar datos ATC
            IndigoManagementExceptions.HandleException(New Exception($"Error inesperado al cargar datos ATC para producto ID: {product.Id}, ATC ID: {product.ATCId}", ex), "ApplicationPolicy")
        End Try
    End Function

    ''' <summary>
    ''' Carga los datos de patologías POS
    ''' </summary>
    Private Async Function LoadPOSPathologiesDataAsync(product As InventoryProduct) As Task
        Try
            If product.POSPathologies Is Nothing OrElse product.POSPathologies.Count = 0 Then
                Return ' No hay patologías que procesar
            End If

            Dim diagnosticIds = product.POSPathologies.Select(Function(pp) pp.DiagnosticId).Distinct().ToList()

            If diagnosticIds.Count = 0 Then
                Return ' No hay IDs de diagnóstico válidos
            End If

            Dim diagnostics = Await _context.Diagnostic _
                .AsNoTracking() _
                .Where(Function(d) diagnosticIds.Contains(d.Id)) _
                .ToDictionaryAsync(Function(d) d.Id, Function(d) d)

            For Each pp As POSPathologies In product.POSPathologies
                If diagnostics.ContainsKey(pp.DiagnosticId) Then
                    Dim diagnostic = diagnostics(pp.DiagnosticId)
                    pp.DiagnosticCode = diagnostic.Code
                    pp.DiagnosticName = diagnostic.Name
                End If

                ' Configurar nombre de medida de edad
                Select Case pp.AgeMeasure
                    Case 1 : pp.AgeMeasureName = "Años"
                    Case 2 : pp.AgeMeasureName = "Meses"
                    Case 3 : pp.AgeMeasureName = "Dias"
                    Case Else : pp.AgeMeasureName = ""
                End Select
            Next

        Catch ex As System.Data.Entity.Core.EntityException
            ' Error específico de Entity Framework al cargar patologías POS
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
        Catch ex As InvalidOperationException
            ' Error de operación inválida
            IndigoManagementExceptions.HandleException(New Exception($"Error de operación al cargar patologías POS para producto ID: {product.Id}", ex), "ApplicationPolicy")
        Catch ex As ArgumentException
            ' Error de argumentos - posiblemente lista vacía o nula
            IndigoManagementExceptions.HandleException(New Exception($"Error de argumentos al cargar patologías POS para producto ID: {product.Id}", ex), "ApplicationPolicy")
        Catch ex As Exception
            ' Error general al cargar patologías POS
            IndigoManagementExceptions.HandleException(New Exception($"Error inesperado al cargar patologías POS para producto ID: {product.Id}", ex), "ApplicationPolicy")
        End Try
    End Function

    ''' <summary>
    ''' Carga la información de Kardex
    ''' </summary>
    Private Async Function LoadKardexInfoAsync(product As InventoryProduct) As Task
        Try
            product.HanddlesMovementKardex = Await _context.Kardex _
                .AsNoTracking() _
                .AnyAsync(Function(k) k.ProductId = product.Id)

        Catch ex As System.Data.Entity.Core.EntityException
            ' Error específico de Entity Framework al consultar Kardex
            IndigoManagementExceptions.HandleException(New Exception($"Error de Entity Framework al verificar movimientos Kardex para producto ID: {product.Id}", ex), "ApplicationPolicy")
            product.HanddlesMovementKardex = False
        Catch ex As InvalidOperationException
            ' Error de operación inválida - contexto cerrado o consulta mal formada
            IndigoManagementExceptions.HandleException(New Exception($"Error de operación al verificar movimientos Kardex para producto ID: {product.Id}", ex), "ApplicationPolicy")
            product.HanddlesMovementKardex = False
        Catch ex As Exception
            ' Error general al verificar Kardex
            IndigoManagementExceptions.HandleException(New Exception($"Error inesperado al verificar movimientos Kardex para producto ID: {product.Id}", ex), "ApplicationPolicy")
            product.HanddlesMovementKardex = False
        End Try
    End Function

#End Region

End Class