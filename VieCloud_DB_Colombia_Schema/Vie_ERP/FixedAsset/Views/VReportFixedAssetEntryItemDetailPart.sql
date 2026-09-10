

CREATE VIEW [FixedAsset].[VReportFixedAssetEntryItemDetailPart]
AS
select cast(CONCAT(id.Id,ISNULL(idp.Id,0)) as Integer) as Id,fe.Id as IdEntry,Iif(fe.[Status] = 1, 'Registrado' , Iif(fe.[Status] = 2, 'Confirmado' , 'Anulado')) as [Status],fe.Code as AdmisionNumber,fe.EntryDate,
cs.Name as NameSupplier,cs.Code as CodeSupplier,(select top 1 Addresss from Common.[Address] as cca where cca.IdPerson = cp.Id) as [Address],
fe.InvoiceDate,ap.Code as CodeCxP,fe.InvoiceNumber,cc.Name as CityName,fe.GetLocationResponsible,
(select top 1 Phone from Common.Phone as ccp where ccp.IdPerson = cp.Id) as Phone, fr.Code as CodeResponsible,fct.Name as NameResponsible,
lo.Code as CodeLocation,lo.Name as NameLocation,ai.Code as CodeItem,ai.[Description] as DescriptionItem,flo.code as CodeLocationDetail, flo.Name as NameLocationDetail,
id.Plate, ei.IvaPercentage,ei.UnitValue,fe.Value,fe.ValueDiscount,(fe.FreightIVAValue + fe.ValueTax) as Iva,fe.WithholdingTax,fe.RetentionOther,fe.DeductionOther,fe.WithholdingICA,
fe.RetentionSource,fe.FreightValue,fe.FreightIVAValue,(fe.TotalValue-fe.WithholdingTax-fe.WithholdingICA-fe.RetentionSource) as Total,fe.[Description],
pac.code as CodePartAccesorieConsumible,pac.Name as NamePartAccesorieConsumible,idp.DepreciatePart,idp.Value as ValuePart,su.UserCode,sp.Fullname
from FixedAsset.FixedAssetEntryItemDetail as id 
inner join FixedAsset.FixedAssetLocation as flo on flo.Id = id.LocationId
inner join FixedAsset.FixedAssetEntryItem as ei on ei.Id = id.FixedAssetEntryItemId
inner join FixedAsset.FixedAssetItem as ai on ai.Id = ei.ItemId
inner join FixedAsset.FixedAssetEntry as fe on fe.Id = ei.FixedAssetEntryId
inner join Common.Supplier as cs on cs.Id = fe.SupplierId
inner join Common.City as cc on cc.Id = cs.IdCity
inner join Common.ThirdParty as ct on ct.Id = cs.IdThirdParty
inner join Common.Person as cp on cp.Id = ct.PersonId
left join Payments.AccountPayable as ap on ap.BillNumber = fe.InvoiceNumber and ap.IdSupplier = fe.SupplierId
left join FixedAsset.FixedAssetResponsible as fr on fr.Id = fe.ResponsibleId
left join Common.ThirdParty as fct on fct.Id = fr.ThirdPartyId
left join FixedAsset.FixedAssetLocation as lo on lo.id = fe.LocationId
left join FixedAsset.FixedAssetEntryItemDetailPart as idp on idp.FixedAssetEntryItemDetailId = id.Id
left join FixedAsset.FixedAssetPartsAccesoriesConsumables as pac on pac.Id = idp.PartAccesoriesConsumiblesId
left join [Security].[User] as su on su.UserCode = fe.CreationUser
left join [Security].Person as sp on sp.Id = su.IdPerson
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de reporte detallado de ítems en entradas de activos fijos, incluyendo sus partes, accesorios y consumibles. Consolida información de cada bien adquirido (placa, serie, ubicación física, descripción) junto con los datos de la entrada o compra (número de admisión, fecha, estado: Registrado/Confirmado/Anulado), el proveedor (nombre, código, ciudad, dirección, teléfono), la factura asociada y su cuenta por pagar (CxP). También incorpora el responsable del activo, la ubicación del ítem y de la entrada, los valores financieros (valor unitario, IVA, descuento, retenciones, flete, total neto) y, cuando aplica, las partes o accesorios asociados al ítem detallado junto con su valor y si se deprecian. Está diseñada para alimentar reportes de inventario y control de activos fijos, auditoría de adquisiciones y seguimiento contable-financiero de bienes de la organización.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'VIEW', @level1name = N'VReportFixedAssetEntryItemDetailPart';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'VIEW', @level1name = N'VReportFixedAssetEntryItemDetailPart';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de reporte que consolida el detalle de entradas de activos fijos con sus ítems, partes/accesorios, proveedor, responsable, ubicación y valores tributarios para impresión o análisis.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'VReportFixedAssetEntryItemDetailPart';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el detalle de ítem de entrada con su ubicación, ítem maestro y entrada asociada (relaciones obligatorias).; Debe existir el proveedor con tercero, persona y ciudad asociados.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'VReportFixedAssetEntryItemDetailPart';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada fila representa la combinación de un detalle de ítem de entrada con una de sus partes/accesorios/consumibles (o sin parte si no existe).; Los registros sólo aparecen si tienen ubicación de detalle, ítem de entrada, ítem maestro, entrada, proveedor con ciudad, tercero y persona válidos.; Responsable, ubicación general de la entrada, CxP, parte/accesorio y usuario creador son opcionales (LEFT JOIN).; El IVA reportado consolida impuesto del ítem y del flete en un único valor.; El Total neto siempre descuenta retención en la fuente, ICA y otra retención (WithholdingTax) del valor total bruto.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'VReportFixedAssetEntryItemDetailPart';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Activo fijo; Entrada/ingreso de activos; Placa de activo; Proveedor; Cuenta por pagar; Factura; Retención en la fuente; Retención ICA; IVA; Flete; Descuento; Responsable de activo; Ubicación/sede; Partes, accesorios y consumibles; Depreciación de partes', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'VReportFixedAssetEntryItemDetailPart';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] FixedAsset.VReportFixedAssetEntryItemDetailPart: Devuelve un Id sintético construido como CONCAT(IdDetalle, ISNULL(IdParte,0)) casteado a entero, garantizando unicidad por combinación detalle-parte.; [RETURN_RESULT] FixedAsset.VReportFixedAssetEntryItemDetailPart: Calcula Total = TotalValue - WithholdingTax - WithholdingICA - RetentionSource (descuenta retenciones del valor total de la entrada).; [RETURN_RESULT] FixedAsset.VReportFixedAssetEntryItemDetailPart: Calcula Iva = FreightIVAValue + ValueTax (suma IVA del flete con el impuesto del valor).; [RETURN_RESULT] FixedAsset.VReportFixedAssetEntryItemDetailPart: Traduce Status: 1=''Registrado'', 2=''Confirmado'', cualquier otro valor=''Anulado''.; [RETURN_RESULT] FixedAsset.VReportFixedAssetEntryItemDetailPart: Para dirección y teléfono toma sólo el primer registro (TOP 1) asociado a la persona del proveedor.; [RETURN_RESULT] FixedAsset.VReportFixedAssetEntryItemDetailPart: Cruza la cuenta por pagar mediante coincidencia de número de factura y proveedor (LEFT JOIN), permitiendo entradas sin CxP asociada.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'VReportFixedAssetEntryItemDetailPart';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si fe.Status = 1 → Etiqueta como ''Registrado'' else Si Status=2 ''Confirmado''; en otro caso ''Anulado''', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'VReportFixedAssetEntryItemDetailPart';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'FixedAsset.FixedAssetEntryItemDetail; FixedAsset.FixedAssetLocation; FixedAsset.FixedAssetEntryItem; FixedAsset.FixedAssetItem; FixedAsset.FixedAssetEntry; Common.Supplier; Common.City; Common.ThirdParty; Common.Person; Common.Address; Common.Phone; Payments.AccountPayable; FixedAsset.FixedAssetResponsible; FixedAsset.FixedAssetEntryItemDetailPart; FixedAsset.FixedAssetPartsAccesoriesConsumables; Security.User; Security.Person', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'VReportFixedAssetEntryItemDetailPart';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'VIEW', @level1name=N'VReportFixedAssetEntryItemDetailPart';
GO
