

CREATE view [Report].[VIEW_REPORTE_CIRCULAR_FT025] as

	/********************************** OBTENCION DE DATOS **********************************/

	SELECT	CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY, 
            tp.Nit NIT, 
			tp.Name TERCERO,
			CASE ar.EntityType
				WHEN 1 THEN 1 -- EPS Contributivo
				WHEN 2 THEN 2 -- EPS Subsidiado
				WHEN 3 THEN 5 -- ET Vinculados Municipios
				WHEN 4 THEN 5 -- ET Vinculados Departamentos
				WHEN 5 THEN 8 -- ARL Riesgos Laborales
				WHEN 6 THEN 3 -- MP Medicina Prepagada
				WHEN 7 THEN 7 -- IPS Privada
				WHEN 8 THEN 7 -- IPS Publica
				WHEN 9 THEN 4 -- Regimen Especial
				WHEN 10 THEN 9 -- Accidentes de transito
				WHEN 11 THEN 4 -- Fosyga
				WHEN 12 THEN 4 -- Otros
				WHEN 13 THEN 7 -- Aseguradoras
				WHEN 99 THEN 3 -- Particulares
				ELSE 0
			END [TIPO ENTIDAD],
			CASE ar.LiquidationType
				WHEN 1 THEN 2 -- Pago por Servicios
				WHEN 2 THEN 1 -- Capitacion
				WHEN 3 THEN 3 -- Factura Global
				WHEN 4 THEN 1 -- Capitacion Global
				WHEN 5 THEN 3 --  Global Prospectivo - PGP
				ELSE 6
			END [TIPO LIQUIDACIO],
			IIF(ar.LiquidationType IN (1,2,3,4,5), 'NA', 'Otro Tipo de Contratación') [TIPO CONTRATO],
			SUM(ar.CollectionValue) [VALOR RECAUDADO],
			SUM(ar.InvoiceValue) [VALOR FACTURADO],
			CAST(ar.DocumentDate AS DATE) [FECHA],
			1 as 'CANTIDAD',
			CAST(ar.DocumentDate AS date) AS 'FECHA BUSQUEDA',
			YEAR(ar.DocumentDate) AS 'AÑO BUSQUEDA',
			MONTH(ar.DocumentDate) AS 'MES BUSQUEDA',
			CONCAT(FORMAT(MONTH(ar.DocumentDate), '00') ,' - ', 
			CASE MONTH(ar.DocumentDate) 
			      WHEN 1 THEN 'ENERO'
				  WHEN 2 THEN 'FEBRERO'
				  WHEN 3 THEN 'MARZO'
				  WHEN 4 THEN 'ABRIL'
				  WHEN 5 THEN 'MAYO'
				  WHEN 6 THEN 'JUNIO'
				  WHEN 7 THEN 'JULIO'
				  WHEN 8 THEN 'AGOSTO'
				  WHEN 9 THEN 'SEPTIEMBRE'
				  WHEN 10 THEN 'OCTUBRE'
				  WHEN 11 THEN 'NOVIEMBRE'
				  WHEN 12 THEN 'DICIEMBRE' END) 'MES NOMBRE BUSQUEDA',
		    CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
			
	FROM
	(
		SELECT	ar.ThirdPartyId,
				ISNULL(cg.EntityType, 0) EntityType,
				ISNULL(cg.LiquidationType, 0) LiquidationType,
				ar.Value InvoiceValue,
				0 CollectionValue,
				CAST(ri.ConfirmDate AS DATE) DocumentDate
		FROM Portfolio.RadicateInvoiceC ri
		JOIN Portfolio.RadicateInvoiceD rid ON ri.Id = rid.RadicateInvoiceCId AND rid.Devolution = 0
		JOIN Portfolio.AccountReceivable ar ON rid.InvoiceNumber = ar.InvoiceNumber AND ar.AccountReceivableType = 2
		LEFT JOIN Contract.CareGroup cg ON ar.CareGroupId = cg.Id
		WHERE ri.State IN ('2')
			--AND CAST(ri.ConfirmDate AS DATE) BETWEEN @InitialDate AND @EndDate
	UNION ALL
		SELECT	ar.ThirdPartyId,
				ISNULL(cg.EntityType, 0) EntityType,
				ISNULL(cg.LiquidationType, 0) LiquidationType,
				0 InvoiceValue,
				0 /*ISNULL(pt.TransferValue, 0) ISNULL(cr.CashReceiptValue, 0) +
				IIF(CAST(ri.ConfirmDate AS DATE) BETWEEN @InitialDate AND @EndDate, ISNULL(pt.PreviousTransferValue, 0) + ISNULL(cr.PreviousCashReceiptValue, 0), 0)*/ CollectionValue,
				CAST(cr.DocumentDate AS DATE)
		FROM Portfolio.RadicateInvoiceC ri
		JOIN Portfolio.RadicateInvoiceD rid ON ri.Id = rid.RadicateInvoiceCId AND rid.Devolution = 0
		JOIN Portfolio.AccountReceivable ar ON rid.InvoiceNumber = ar.InvoiceNumber AND ar.AccountReceivableType = 2
		LEFT JOIN Contract.CareGroup cg ON ar.CareGroupId = cg.Id
		LEFT JOIN
		(
			SELECT 
				ptd.AccountReceivableId,
				CAST(pt.RecersalDate AS DATE) DocumentDate 
				/*SUM(IIF(CAST(pt.DocumentDate AS DATE) BETWEEN @InitialDate AND @EndDate, 0, ptd.Value)) PreviousTransferValue,
				SUM(IIF(CAST(pt.DocumentDate AS DATE) BETWEEN @InitialDate AND @EndDate, ptd.Value, 0)) TransferValue*/				
			FROM Portfolio.PortfolioTransfer pt
			JOIN Portfolio.PortfolioTransferDetail ptd ON pt.Id = ptd.PortfolioTrasferId
			WHERE pt.Status IN (2, 4) 
				--AND ,
				--CAST(cr.DocumentDate AS DATE) <= @EndDate
				--AND CAST(ISNULL(pt.RecersalDate, DATEADD(DAY, 1, @EndDate)) AS DATE) > @EndDate
			GROUP BY ptd.AccountReceivableId, CAST(pt.RecersalDate AS DATE) 
		) pt ON ar.Id = pt.AccountReceivableId
		LEFT JOIN
		(
			SELECT 
				crar.AccountReceivableId,
				CAST(cr.DocumentDate AS DATE) DocumentDate
				/*SUM(IIF(CAST(cr.DocumentDate AS DATE) BETWEEN @InitialDate AND @EndDate, 0, crar.Value)) PreviousCashReceiptValue,
				SUM(IIF(CAST(cr.DocumentDate AS DATE) BETWEEN @InitialDate AND @EndDate, crar.Value, 0)) CashReceiptValue*/
			FROM Treasury.CashReceipts cr
			JOIN Treasury.CashReceiptDetails crd ON cr.Id = crd.IdCashReceipt
			JOIN Treasury.CashReceiptAccountReceivable crar ON crd.Id = crar.CashReceiptDetailId
			WHERE cr.Status IN (2 , 4)
				--AND CAST(cr.DocumentDate AS DATE) <= @EndDate
				--AND CAST(ISNULL(cr.ReversedDate, DATEADD(DAY, 1, @EndDate)) AS DATE) > @EndDate
			GROUP BY crar.AccountReceivableId, CAST(cr.DocumentDate AS DATE)
		) cr ON ar.Id = cr.AccountReceivableId
		WHERE ri.State IN ('2')
			--AND CAST(ri.ConfirmDate AS DATE) <= @EndDate
			--AND (ISNULL(pt.TransferValue, 0) + ISNULL(pt.PreviousTransferValue, 0) + ISNULL(cr.CashReceiptValue, 0) + ISNULL(cr.PreviousCashReceiptValue, 0)) > 0
	) ar
	JOIN Common.ThirdParty tp ON ar.ThirdPartyId = tp.Id
	WHERE CAST(ar.DocumentDate AS DATE) IS NOT NULL
	GROUP BY tp.Nit, tp.Name, ar.EntityType, ar.LiquidationType, CAST(ar.DocumentDate AS DATE)
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting para el reporte regulatorio Circular FT025, que consolida valores facturados y recaudados por tercero (NIT/nombre), tipo de entidad pagadora (EPS contributiva/subsidiada, ARL, medicina prepagada, IPS, régimen especial, etc.) y modalidad de contratación/liquidación, agrupados por fecha. Combina facturas radicadas confirmadas con traslados de cartera y recibos de caja aplicados, traduciendo los códigos internos de entidad y liquidación a los códigos exigidos por la circular. Incluye dimensiones temporales (año, mes, nombre de mes) y la marca de última actualización para consumo en herramientas de reporting regulatorio.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_REPORTE_CIRCULAR_FT025';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_REPORTE_CIRCULAR_FT025';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida por tercero, tipo de entidad, tipo de liquidación y fecha los valores facturados y recaudados de cartera para el reporte regulatorio Circular FT025, mapeando códigos internos a la clasificación oficial.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_REPORTE_CIRCULAR_FT025';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las facturas radicadas deben estar en estado ''2'' (confirmadas) en Portfolio.RadicateInvoiceC; El detalle de radicación no debe corresponder a devolución (RadicateInvoiceD.Devolution = 0); Solo se consideran cuentas por cobrar con AccountReceivableType = 2; Los traslados de cartera (PortfolioTransfer) deben estar en Status 2 o 4; Los recibos de caja (CashReceipts) deben estar en Status 2 o 4; La fecha del documento (ConfirmDate o DocumentDate) no puede ser nula', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_REPORTE_CIRCULAR_FT025';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Excluye radicados que no estén en estado ''2'' y detalles marcados como devolución; Solo agrega cuentas por cobrar de tipo 2 (facturas de cartera); EntityType y LiquidationType nulos en CareGroup se reemplazan por 0 (default); El campo CANTIDAD siempre es 1 por fila agrupada; La fecha de actualización se calcula en zona horaria ''Pakistan Standard Time''; El ID_COMPANY se obtiene dinámicamente vía DB_NAME() truncado a 9 caracteres; La rama UNION ALL de recaudo siempre devuelve CollectionValue = 0 (lógica de cálculo está comentada/inhabilitada)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_REPORTE_CIRCULAR_FT025';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Circular FT025; Cartera; Radicación de facturas; Cuentas por cobrar; Tipo de entidad pagadora (EPS, ARL, IPS, Medicina Prepagada, Régimen Especial, Fosyga, Aseguradoras, Particulares); Tipo de liquidación (Pago por servicios, Capitación, Factura Global, PGP); Recibos de caja; Traslados de cartera; Tercero/NIT; Valor facturado; Valor recaudado', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_REPORTE_CIRCULAR_FT025';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset Circular FT025: Devuelve, por NIT/Tercero/Tipo Entidad/Tipo Liquidación/Fecha, la suma de InvoiceValue como VALOR RECAUDADO y de InvoiceValue como VALOR FACTURADO; cantidad fija = 1; ULT_ACTUAL = GETDATE() ajustado a Pakistan Standard Time', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_REPORTE_CIRCULAR_FT025';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CareGroup.EntityType (1..13, 99) → Mapea al código oficial de TIPO ENTIDAD: 1=EPS Contributivo→1, 2=EPS Subsidiado→2, 3/4=ET Vinculados→5, 5=ARL→8, 6=Medicina Prepagada→3, 7/8=IPS Privada/Pública→7, 9=Régimen Especial→4, 10=Accidentes de Tránsito→9, 11/12=Fosyga/Otros→4, 13=Aseguradoras→7, 99=Particulares→3 else 0 cuando EntityType no coincide con ningún caso; si CareGroup.LiquidationType (1..5) → Mapea TIPO LIQUIDACIO: 1=Pago por Servicios→2, 2=Capitación→1, 3=Factura Global→3, 4=Capitación Global→1, 5=Global Prospectivo PGP→3 else 6 cuando LiquidationType no está en (1..5); si LiquidationType IN (1,2,3,4,5) → TIPO CONTRATO = ''NA'' else TIPO CONTRATO = ''Otro Tipo de Contratación''; si MONTH(DocumentDate) → Concatena número de mes con su nombre en español (ENERO..DICIEMBRE) en el campo MES NOMBRE BUSQUEDA', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_REPORTE_CIRCULAR_FT025';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.RadicateInvoiceC; Portfolio.RadicateInvoiceD; Portfolio.AccountReceivable; Contract.CareGroup; Portfolio.PortfolioTransfer; Portfolio.PortfolioTransferDetail; Treasury.CashReceipts; Treasury.CashReceiptDetails; Treasury.CashReceiptAccountReceivable; Common.ThirdParty', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_REPORTE_CIRCULAR_FT025';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_REPORTE_CIRCULAR_FT025';
GO
