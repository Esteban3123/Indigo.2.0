'************************************************************
' Assembly         : Infrastructure.Data.Billing
' Author           : Miguel Angel Fonseca Castro
' Created          : 2018-10-17
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities

#End Region

Public Class BillingNoteRepository
    Inherits GenericRepository(Of BillingNote)
    Implements IBillingNoteRepository

    ' contexto del repositorio de ciudades
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''  Contructor del repositorio coidades el cual instancia una nueva clase
    ''' </summary>
    ''' <param name="context">contexto del repositorio</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene un documento electronico usado para la facturación electronica por el id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetBillingNoteById(Id As Integer, Optional tracking As Boolean = True) As BillingNote Implements IBillingNoteRepository.GetBillingNoteById
        Dim res As BillingNote
        If tracking = True Then
            res = (From a As BillingNote _
                   In Me._context.BillingNote
                   Where a.Id = Id
                   Select a).FirstOrDefault()
        Else
            res = (From a As BillingNote _
                   In Me._context.BillingNote.AsNoTracking()
                   Where a.Id = Id
                   Select a).FirstOrDefault()
        End If
        If res IsNot Nothing AndAlso res.Id > 0 Then
            res.OriginalValue = (From b In _context.BillingNote.AsNoTracking() Where b.Id = Id Select b).FirstOrDefault()
            Return res
        Else
            Return New BillingNote
        End If
    End Function

	''' <summary>
	''' Obtiene un documento electronico usado para la facturación electronica por el id con agregados
	''' </summary>
	''' <param name="Id">The identifier.</param>
	''' <returns></returns>
	Public Function GetBillingNoteByIdWithAggregates(Id As Integer, Optional xmlDianGenerate As Boolean = False) As BillingNote Implements IBillingNoteRepository.GetBillingNoteByIdWithAggregates
		Dim res As BillingNote = (From a As BillingNote _
			In Me._context.BillingNote.
									  Include("BillingNoteDetail").
									  Include("BillingNoteDetail.Invoice").
									  Include("BillingNoteDetail.BillingNoteDetailTax")
								  Where a.Id = Id
								  Select a).FirstOrDefault()
		If res IsNot Nothing AndAlso res.Id > 0 Then

			Dim noteTypeDetailQuery = Me.GetPortfolioNoteTypeDetailByBillingNoteId(Id, xmlDianGenerate)
			For Each detail In res.BillingNoteDetail
				detail.NoteTypeDetails = noteTypeDetailQuery?.FindAll(Function(x) x.InvoiceId = detail.InvoiceId)
				Dim electronicDocument = (From ed In _context.ElectronicDocument.AsNoTracking() Where ed.EntityId = detail.InvoiceId And ed.EntityName = "Invoice").FirstOrDefault()
				If electronicDocument Is Nothing Or detail.ConceptId = 7 Then
					detail.DianVersion = 0
				Else
					detail.DianVersion = electronicDocument.DianVersion
					detail.Status = electronicDocument.Status
				End If
			Next
			Return res
		Else
			Return New BillingNote
		End If
	End Function

	''' <summary>
	''' 
	''' </summary>
	''' <param name="billingNoteId"></param>
	''' <returns></returns>
	Public Function GetPortfolioNoteTypeDetailByBillingNoteId(billingNoteId As Integer, Optional xmlDianGenerate As Boolean = False) As List(Of NoteTypeDetail)

		If billingNoteId = 0 Then
			Return Nothing
		End If
		Dim parmas As List(Of (String, Object)) = New List(Of (String, Object)) From {("@BillingNoteId", billingNoteId)}

		Dim sql As String = "	
	                SELECT 
						id.Id,
						id.Id as InvoiceDetailId,
						i.Id as InvoiceId,
						bnd.Id as BillingNoteDetailId, 
						p.Code, 
						p.Name, 
						p.CodeAlternative,
						p.CodeAlternativeTwo,
						bg.Code + ' - ' + bg.Name BillingGroup,
						1 AS InvoicedQuantity, 
						pnarad.BaseValue AS UnitValue, 
						pnarad.BaseValue, 
						pnarad.Value TotalAdjustmentValue,
						COALESCE(pnarad.TaxValue,0) TaxValue, 
						COALESCE(pnarad.TaxPercentage,0) TaxPercentage, 
						tax.Code as TaxCode,
						NULL NameAlternative,
						NULL NameAlternativeTwo,
						tax.TaxClassificationType
					FROM Billing.BillingNote bn WITH (NOLOCK)
						JOIN Billing.BillingNoteDetail bnd WITH (NOLOCK) ON bn.Id = bnd.BillingNoteId
						JOIN Portfolio.PortfolioNote pn WITH (NOLOCK) ON pn.Id = bn.EntityId AND 'PortfolioNote' = bn.EntityName
						JOIN Portfolio.PortfolioNoteAccountReceivableAdvance pnara WITH(NOLOCK) on pn.Id = pnara.PortfolioNoteId
						JOIN Portfolio.PortfolioNoteAccountReceivableDetail pnarad WITH(NOLOCK) ON pnara.Id = pnarad.PortfolioNoteAccountReceivableId
						JOIN Billing.InvoiceDetail id WITH(NOLOCK) on pnarad.EntityName ='InvoiceDetail' and pnarad.EntityId = id.Id
						JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) on sod.Id=id.ServiceOrderDetailId
						JOIN Inventory.InventoryProduct p WITH(NOLOCK) ON sod.ProductId = p.Id
						JOIN Billing.Invoice i WITH(NOLOCK) on i.Id =id.InvoiceId
						LEFT JOIN Billing.ProductServiceDetail psd WITH(NOLOCK) ON sod.Id = psd.ServiceOrderDetailId
						LEFT JOIN Contract.CUPSEntity cups WITH(NOLOCK) ON psd.CUPSEntityId=cups.Id
						LEFT JOIN Billing.BillingGroup bg WITH (NOLOCK) ON bg.Id = IIF(psd.CUPSEntityId IS NULL, p.BillingGroupId, cups.BillingGroupId)
						LEFT JOIN GeneralLedger.GeneralLedgerIVA tax WITH(NOLOCK) ON tax.Id = pnarad.TaxId
					WHERE (pn.NoteType = 6 or ISNULL(pn.EntityName,'')='Glosas') and sod.RecordType =2 and bn.Id =@BillingNoteId

					UNION ALL

					SELECT 
						id.Id,
						id.Id as InvoiceDetailId,
						i.Id as InvoiceId,
						bnd.Id as BillingNoteDetailId,  
						ips.Code, 
						ips.Name, 
						ce.Code CodeAlternative,
						ce.RIPSCode CodeAlternativeTwo,
						bg.Code + ' - ' + bg.Name BillingGroup,
						1 AS InvoicedQuantity,
						pnarad.BaseValue AS UnitValue, 
						pnarad.BaseValue, 
						pnarad.Value TotalAdjustmentValue,
						COALESCE(pnarad.TaxValue,0) TaxValue, 
						COALESCE(pnarad.TaxPercentage,0) TaxPercentage, 
						tax.Code TaxCode,
						ce.Description NameAlternative,
						ce.RIPSDescription NameAlternativeTwo,
						tax.TaxClassificationType
					FROM Billing.BillingNote bn WITH (NOLOCK)
						JOIN Billing.BillingNoteDetail bnd WITH (NOLOCK) ON bn.Id = bnd.BillingNoteId
						JOIN Portfolio.PortfolioNote pn WITH (NOLOCK) ON pn.Id = bn.EntityId AND 'PortfolioNote' = bn.EntityName
						JOIN Portfolio.PortfolioNoteAccountReceivableAdvance pnara WITH(NOLOCK) on pn.Id = pnara.PortfolioNoteId
						JOIN Portfolio.PortfolioNoteAccountReceivableDetail pnarad WITH(NOLOCK) ON pnara.Id = pnarad.PortfolioNoteAccountReceivableId
						JOIN Billing.InvoiceDetail id WITH(NOLOCK) on pnarad.EntityName ='InvoiceDetail' and pnarad.EntityId = id.Id
						JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) on sod.Id=id.ServiceOrderDetailId
						JOIN Billing.Invoice i WITH(NOLOCK) on i.Id =id.InvoiceId
						JOIN Contract.IPSService ips WITH(NOLOCK) ON ips.Id = sod.IPSServiceId
						JOIN Contract.CUPSEntity ce WITH(NOLOCK) ON ce.Id = sod.CUPSEntityId 
						JOIN Billing.BillingGroup bg WITH (NOLOCK) ON bg.Id = ce.BillingGroupId
						LEFT JOIN GeneralLedger.GeneralLedgerIVA tax WITH(NOLOCK) ON tax.Id = pnarad.TaxId
					WHERE (pn.NoteType = 6 or ISNULL(pn.EntityName,'')='Glosas') and sod.RecordType =1 and  bn.Id =@BillingNoteId

                    UNION ALL
                    
                   SELECT 
						ids.Id,
						id.Id as InvoiceDetailId,
						i.Id as InvoiceId,
						bnd.Id as BillingNoteDetailId,  
						ips.Code, 
						ips.Name, 
						NULL CodeAlternative,
						NULL CodeAlternativeTwo,
						NULL BillingGroup,
						1 AS InvoicedQuantity,
						pnarad.BaseValue AS UnitValue, 
						pnarad.BaseValue, 
						pnarad.Value TotalAdjustmentValue,
						COALESCE(pnarad.TaxValue,0) TaxValue, 
						COALESCE(pnarad.TaxPercentage,0) TaxPercentage, 
						tax.Code TaxCode,
						NULL NameAlternative,
						NULL NameAlternativeTwo,
						tax.TaxClassificationType
					FROM Billing.BillingNote bn WITH (NOLOCK)
						JOIN Billing.BillingNoteDetail bnd WITH (NOLOCK) ON bn.Id = bnd.BillingNoteId
						JOIN Portfolio.PortfolioNote pn WITH (NOLOCK) ON pn.Id = bn.EntityId AND 'PortfolioNote' = bn.EntityName
						JOIN Portfolio.PortfolioNoteAccountReceivableAdvance pnara WITH(NOLOCK) on pn.Id = pnara.PortfolioNoteId
						JOIN Portfolio.PortfolioNoteAccountReceivableDetail pnarad WITH(NOLOCK) ON pnara.Id = pnarad.PortfolioNoteAccountReceivableId
						JOIN Billing.InvoiceDetailSurgical ids WITH(NOLOCK) on pnarad.EntityName ='InvoiceDetailSurgical' and pnarad.EntityId = ids.Id
						JOIN Billing.InvoiceDetail id WITH(NOLOCK) on ids.InvoiceDetailId = id.Id
						JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) on id.ServiceOrderDetailId=sod.Id
						JOIN Billing.Invoice i WITH(NOLOCK) on i.Id =id.InvoiceId
						JOIN Contract.IPSService ips WITH(NOLOCK) ON ips.Id = ids.IPSServiceId
						LEFT JOIN GeneralLedger.GeneralLedgerIVA tax WITH(NOLOCK) ON tax.Id = pnarad.TaxId
					WHERE pn.NoteType = 6 and sod.RecordType =1 and bn.Id =@BillingNoteId
            
                    UNION ALL

					SELECT 
						ids.Id,
						id.Id as InvoiceDetailId,
						i.Id as InvoiceId,
						bnd.Id as BillingNoteDetailId,  
						ips.Code, 
						ips.Name, 
						NULL CodeAlternative,
						NULL CodeAlternativeTwo,
						NULL BillingGroup,
						1 AS InvoicedQuantity,
						pnarad.BaseValue AS UnitValue, 
						pnarad.BaseValue, 
						pnarad.Value TotalAdjustmentValue,
						COALESCE(pnarad.TaxValue,0) TaxValue, 
						COALESCE(pnarad.TaxPercentage,0) TaxPercentage, 
						tax.Code TaxCode,
						NULL NameAlternative,
						NULL NameAlternativeTwo,
						tax.TaxClassificationType
					FROM Billing.BillingNote bn WITH (NOLOCK)
						JOIN Billing.BillingNoteDetail bnd WITH (NOLOCK) ON bn.Id = bnd.BillingNoteId
						JOIN Portfolio.PortfolioNote pn WITH (NOLOCK) ON pn.Id = bn.EntityId AND 'PortfolioNote' = bn.EntityName
						JOIN Portfolio.PortfolioNoteAccountReceivableAdvance pnara WITH(NOLOCK) on pn.Id = pnara.PortfolioNoteId
						JOIN Portfolio.PortfolioNoteAccountReceivableDetail pnarad WITH(NOLOCK) ON pnara.Id = pnarad.PortfolioNoteAccountReceivableId
						JOIN Billing.ServiceOrderDetailSurgical ids WITH(NOLOCK) on pnarad.EntityName ='ServiceOrderDetailSurgical' and pnarad.EntityId = ids.Id
						JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) on ids.ServiceOrderDetailId=sod.Id
						JOIN Portfolio.AccountReceivable ar WITH(NOLOCK) on ar.Id = pnara.AccountReceivableId
						JOIN Billing.Invoice i WITH(NOLOCK) on i.Id =ar.InvoiceId
						JOIN Billing.InvoiceDetail id WITH(NOLOCK) on i.Id=id.InvoiceId and id.ServiceOrderDetailId=sod.Id
						JOIN Contract.IPSService ips WITH(NOLOCK) ON ips.Id = ids.IPSServiceId
						LEFT JOIN GeneralLedger.GeneralLedgerIVA tax WITH(NOLOCK) ON tax.Id = pnarad.TaxId
					WHERE (pn.NoteType = 6 or ISNULL(pn.EntityName,'')='Glosas') and sod.RecordType =1 and bn.Id =@BillingNoteId

                    UNION ALL

					SELECT
                        ibid.Id,
                        ibid.Id AS InvoiceDetailId,
                        i.Id AS InvoiceId,
                        bnd.Id AS BillingNoteDetailId,
                        ibid.ServiceCode AS Code,
                        COALESCE(ce.Description, p.Name, ibid.ServiceCode) AS Name,
                        ce.Code AS CodeAlternative,
                        ce.RIPSCode AS CodeAlternativeTwo,
                        NULL AS BillingGroup,
                        ibid.Quantity AS InvoicedQuantity,
                        ibid.UnitValue,
                        pnarad.BaseValue,
                        pnarad.Value AS TotalAdjustmentValue,
                        COALESCE(pnarad.TaxValue, 0) AS TaxValue,
                        COALESCE(pnarad.TaxPercentage, 0) AS TaxPercentage,
                        tax.Code AS TaxCode,
                        ce.Description AS NameAlternative,
                        ce.RIPSDescription AS NameAlternativeTwo, 
						tax.TaxClassificationType
                    FROM Billing.BillingNote bn WITH (NOLOCK)
                        JOIN Billing.BillingNoteDetail bnd WITH (NOLOCK) ON bn.Id = bnd.BillingNoteId
                        JOIN Portfolio.PortfolioNote pn WITH (NOLOCK) ON pn.Id = bn.EntityId AND bn.EntityName = 'PortfolioNote'
                        JOIN Portfolio.PortfolioNoteAccountReceivableAdvance pnara WITH (NOLOCK) ON pn.Id = pnara.PortfolioNoteId
                        JOIN Portfolio.PortfolioNoteAccountReceivableDetail pnarad WITH (NOLOCK) ON pnara.Id = pnarad.PortfolioNoteAccountReceivableId
                        JOIN Portfolio.InitialBalanceInvoiceDetail ibid WITH (NOLOCK)
                            ON pnarad.EntityName = 'InitialBalanceInvoiceDetail' AND pnarad.EntityId = ibid.Id
                        JOIN Portfolio.InitialBalanceInvoice ibi WITH (NOLOCK) ON ibid.InitialBalanceInvoiceId = ibi.Id
                        JOIN Billing.Invoice i WITH (NOLOCK) ON i.Id = ibi.InvoiceId
                        LEFT JOIN Contract.CUPSEntity ce WITH (NOLOCK) ON ce.Code = ibid.ServiceCode
                        LEFT JOIN Inventory.InventoryProduct p WITH (NOLOCK) ON p.Code = ibid.ServiceCode
                        LEFT JOIN GeneralLedger.GeneralLedgerIVA tax WITH (NOLOCK) ON tax.Id = pnarad.TaxId
                    WHERE pn.NoteType = 6 AND ISNULL(pn.EntityName, '') = 'InitialBalance' AND bn.Id = @BillingNoteId

            "

		If xmlDianGenerate Then
			sql &= "
					UNION ALL

					select
						bbd.Id Id,
						bbd.Id  InvoiceDetailId,
						BB.InvoiceId InvoiceId,
						bnd.Id  BillingNoteDetailId,
						isnull(ip.Code, isnull(bc.Code, fai.Code)) Code,
						isnull(ip.Name, isnull(bc.Name, fai.Description)) Name,
						NULL CodeAlternative,
						NULL CodeAlternativeTwo,
						NULL BillingGroup,
						bbd.Quantity InvoicedQuantity,
						bbd.Price As UnitValue,
						bbd.value As BaseValue,
						((bbd.Value) + (IIF(bbd.PercentageIVA > 0, bbd.Value * bbd.PercentageIVA / 100, 0))) TotalAdjustmentValue,
						IIF(bbd.PercentageIVA > 0, bbd.Value * bbd.PercentageIVA / 100, 0) As TaxValue,
						bbd.PercentageIVA As TaxPercentage,
						IVA.Code TaxCode,
						NULL NameAlternative,
						NULL NameAlternativeTwo,
						IVA.TaxClassificationType
					FROM Billing.BillingNote bn
					INNER JOIN Billing.BillingNoteDetail bnd on bnd.BillingNoteId = bn.id
					INNER JOIN Billing.BasicBilling BB ON BB.InvoiceId = bnd.InvoiceId
					INNER JOIN Billing.BasicBillingDetail bbd on bbd.BasicBillingId = bb.id
					INNER JOIN Portfolio.PortfolioNote pn on pn.Id = bn.EntityId and bn.EntityName = 'PortfolioNote'
					LEFT JOIN Inventory.InventoryProduct ip on ip.Id = bbd.ProductId
					LEFT JOIN Billing.BillingConcept bc on bc.Id = bbd.BillingConceptId
					LEFT JOIN FixedAsset.FixedAssetPhysicalAsset fapa on fapa.Id = bbd.PhysicalAssetId
					LEFT JOIN FixedAsset.FixedAssetItem fai on fai.Id = fapa.ItemId
					LEFT JOIN GeneralLedger.GeneralLedgerIVA IVA on IVA.Id in (ip.IVAId, bc.IVAId, fai.IVAId)
					WHERE bn.Id =@BillingNoteId AND pn.EntityName = 'ReverseBasicBilling'
            "
		End If

		Dim query = Me.ExecuteQueryDR(Of NoteTypeDetail)(sql, parmas)?.ToList()

		If query?.Any() Then
			Return query
		Else
			Return Nothing
		End If
	End Function

	''' <summary>
	''' Obtiene el ConceptId de PortfolioNoteAccountReceivableAdvance para el DiscrepancyResponse
	''' </summary>
	''' <param name="billingNoteId">Id de la BillingNote</param>
	''' <returns>ConceptId o Nothing si no existe</returns>
	Public Function GetDiscrepancyConceptIdByBillingNoteId(billingNoteId As Integer) As Integer? Implements IBillingNoteRepository.GetDiscrepancyConceptIdByBillingNoteId
		' 1. BillingNote asociada a PortfolioNote
		Dim portfolioNote = (From bn In _context.BillingNote.AsNoTracking()
							 Join pn In _context.PortfolioNote.AsNoTracking() On bn.EntityId Equals pn.Id
							 Join pnara In _context.PortfolioNoteAccountReceivableAdvance.AsNoTracking() On pn.Id Equals pnara.PortfolioNoteId
							 Where bn.Id = billingNoteId AndAlso bn.EntityName = "PortfolioNote"
							 Select New With {.ConceptId = pnara.ConceptId, .EntityName = pn.EntityName}).FirstOrDefault()

		If portfolioNote IsNot Nothing Then
			If portfolioNote.EntityName = "ReverseBasicBilling" Then Return 2
			If portfolioNote.ConceptId IsNot Nothing Then Return portfolioNote.ConceptId
		End If

		' 2. BillingNote asociada a Invoice anulada (Status = 2)
		Dim hasAnnulledInvoice = (From bn In _context.BillingNote.AsNoTracking()
								  Join i In _context.Invoice.AsNoTracking() On bn.EntityId Equals i.Id
								  Where bn.Id = billingNoteId AndAlso bn.EntityName = "Invoice" AndAlso i.Status = 2).Any()
		If hasAnnulledInvoice Then Return 2

		' 3. BillingNote asociada a DocumentInvoiceProductSales cancelado (Status = 3)
		Dim hasCancelledDoc = (From bn In _context.BillingNote.AsNoTracking()
							   Join i In _context.DocumentInvoiceProductSales.AsNoTracking() On bn.EntityId Equals i.Id
							   Where bn.Id = billingNoteId AndAlso bn.EntityName = "DocumentInvoiceProductSales" AndAlso i.Status = 3).Any()
		If hasCancelledDoc Then Return 2

		Return Nothing
	End Function

End Class
