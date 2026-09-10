-- =============================================
-- Author:		Miguel Fonseca
-- Create date: 2020-02-24
-- Description:	SP que genera la informacion para el XML FormatoArchiveFT025
-- =============================================
CREATE PROCEDURE [GeneralLedger].[SP_ReportSingleCircularFT025]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @Year INT,
			@Month INT,
			---------------------------------------------------------------------------------------
			@InitialDate DATE,
			@EndDate DATE,
			@BusinessLine tinyint
	
	BEGIN TRY
		
		/********************************** CRITERIOS Y FILTROS **********************************/

		SELECT	@Year = t.x.value('Year[1]','int'),
				@Month = t.x.value('Month[1]','int')
		FROM @xmlCriterias.nodes('/Data') t(x)

		-------------------------------------------------------------------------------------------

		SET @InitialDate = DATEFROMPARTS(@Year, 1, 1)
		SET @EndDate = EOMONTH(DATEFROMPARTS(@Year,@Month,1))
		SET @BusinessLine = (SELECT cs.BusinessLine FROM GeneralLedger.CompanySettings cs)

		/********************************** OBTENCION DE DATOS **********************************/

		SELECT	tp.Nit ThirdPartyNit, 
				tp.Name ThirdPartyName,
				CASE ar.EntityType
					WHEN 1 THEN 1 -- EPS Contributivo
					WHEN 2 THEN 2 -- EPS Subsidiado
					WHEN 3 THEN 5 -- ET Vinculados Municipios
					WHEN 4 THEN 5 -- ET Vinculados Departamentos
					WHEN 5 THEN 8 -- ARL Riesgos Laborales
					WHEN 6 THEN 3 -- MP Medicina Prepagada
					WHEN 9 THEN 4 -- Regimen Especial
					WHEN 10 THEN 9 -- Accidentes de transito
					WHEN 11 THEN 4 -- Fosyga
					WHEN 12 THEN 4 -- Otros
					WHEN 13 THEN 7 -- Aseguradoras
					ELSE 0
				END EntityType,
				CASE ar.LiquidationType
					WHEN 1 THEN 2 -- Pago por Servicios
					WHEN 2 THEN 1 -- Capitacion
					WHEN 3 THEN 3 -- Factura Global
					WHEN 4 THEN 1 -- Capitacion Global
					WHEN 5 THEN 3 --  Global Prospectivo - PGP
					ELSE 6
				END LiquidationType,
				IIF(ar.LiquidationType IN (1,2,3,4,5), 'NA', 'Otro Tipo de Contratación') ContractType,
				SUM(ar.InvoiceValue) InvoiceValue,
				SUM(ar.CollectionValue) CollectionValue,
				@BusinessLine BusinessLine
		FROM
		(
			SELECT	ar.ThirdPartyId,
					ISNULL(cg.EntityType, 0) EntityType,
					ISNULL(cg.LiquidationType, 0) LiquidationType,
					ar.Value InvoiceValue,
					0 CollectionValue
			FROM Portfolio.RadicateInvoiceC ri
			JOIN Portfolio.RadicateInvoiceD rid WITH (NOLOCK) ON ri.Id = rid.RadicateInvoiceCId AND rid.Devolution = 0
			JOIN Portfolio.AccountReceivable ar WITH (NOLOCK) ON rid.InvoiceNumber = ar.InvoiceNumber AND ar.AccountReceivableType = 2
			LEFT JOIN Contract.CareGroup cg WITH (NOLOCK) ON ar.CareGroupId = cg.Id
			WHERE ri.State IN ('2')
				AND CAST(ri.ConfirmDate AS DATE) BETWEEN @InitialDate AND @EndDate 
		UNION ALL
			SELECT	ar.ThirdPartyId,
					ISNULL(cg.EntityType, 0) EntityType,
					ISNULL(cg.LiquidationType, 0) LiquidationType,
					0 InvoiceValue,
					ISNULL(pt.TransferValue, 0) + ISNULL(cr.CashReceiptValue, 0) +
					IIF(CAST(ri.ConfirmDate AS DATE) BETWEEN @InitialDate AND @EndDate, ISNULL(pt.PreviousTransferValue, 0) + ISNULL(cr.PreviousCashReceiptValue, 0), 0) CollectionValue
			FROM Portfolio.RadicateInvoiceC ri
			JOIN Portfolio.RadicateInvoiceD rid WITH (NOLOCK) ON ri.Id = rid.RadicateInvoiceCId AND rid.Devolution = 0
			JOIN Portfolio.AccountReceivable ar WITH (NOLOCK) ON rid.InvoiceNumber = ar.InvoiceNumber AND ar.AccountReceivableType = 2
			LEFT JOIN Contract.CareGroup cg WITH (NOLOCK) ON ar.CareGroupId = cg.Id
			LEFT JOIN
			(
				SELECT 
					ptd.AccountReceivableId,
					SUM(IIF(CAST(pt.DocumentDate AS DATE) BETWEEN @InitialDate AND @EndDate, 0, ptd.Value)) PreviousTransferValue,
					SUM(IIF(CAST(pt.DocumentDate AS DATE) BETWEEN @InitialDate AND @EndDate, ptd.Value, 0)) TransferValue				
				FROM Portfolio.PortfolioTransfer pt WITH (NOLOCK)
				JOIN Portfolio.PortfolioTransferDetail ptd WITH (NOLOCK) ON pt.Id = ptd.PortfolioTrasferId
				WHERE pt.Status IN (2, 4) 
					AND CAST(pt.DocumentDate AS DATE) <= @EndDate
					AND CAST(ISNULL(pt.RecersalDate, DATEADD(DAY, 1, @EndDate)) AS DATE) > @EndDate
				GROUP BY ptd.AccountReceivableId
			) pt ON ar.Id = pt.AccountReceivableId
			LEFT JOIN
			(
				SELECT 
					crar.AccountReceivableId,
					SUM(IIF(CAST(cr.DocumentDate AS DATE) BETWEEN @InitialDate AND @EndDate, 0, crar.Value)) PreviousCashReceiptValue,
					SUM(IIF(CAST(cr.DocumentDate AS DATE) BETWEEN @InitialDate AND @EndDate, crar.Value, 0)) CashReceiptValue
				FROM Treasury.CashReceipts cr WITH (NOLOCK)
				JOIN Treasury.CashReceiptDetails crd WITH (NOLOCK) ON cr.Id = crd.IdCashReceipt
				JOIN Treasury.CashReceiptAccountReceivable crar WITH (NOLOCK) ON crd.Id = crar.CashReceiptDetailId
				WHERE cr.Status IN (2 , 4)
					AND CAST(cr.DocumentDate AS DATE) <= @EndDate
					AND CAST(ISNULL(cr.ReversedDate, DATEADD(DAY, 1, @EndDate)) AS DATE) > @EndDate
				GROUP BY crar.AccountReceivableId
			) cr ON ar.Id = cr.AccountReceivableId
			WHERE ri.State IN ('2')
				AND CAST(ri.ConfirmDate AS DATE) <= @EndDate
				AND (ISNULL(pt.TransferValue, 0) + ISNULL(pt.PreviousTransferValue, 0) + ISNULL(cr.CashReceiptValue, 0) + ISNULL(cr.PreviousCashReceiptValue, 0)) > 0
		) ar
		JOIN Common.ThirdParty tp WITH (NOLOCK) ON ar.ThirdPartyId = tp.Id
		WHERE ar.EntityType NOT IN (7,8,99)
		GROUP BY tp.Nit, tp.Name, ar.EntityType, ar.LiquidationType
		HAVING
            SUM(ar.InvoiceValue) <> 0
            OR SUM(ar.CollectionValue) <> 0;
	END TRY
	BEGIN CATCH	
		SELECT '999' CodeResult, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) MessageResult
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte Formato FT025 (Circular Única de la Superintendencia de Salud) para un año y mes indicados, consolidando por entidad pagadora (EPS, medicina prepagada, ARL, régimen especial, etc.) los valores facturados y recaudados de cartera. Combina las facturas radicadas y confirmadas (RadicateInvoiceC/D y AccountReceivable) con los recaudos obtenidos mediante traslados de cartera (PortfolioTransfer) y recibos de caja (CashReceipts), agrupando por NIT y nombre del tercero, tipo de entidad y modalidad de contratación (capitación, pago por servicios, global). El resultado alimenta el archivo XML regulatorio FT025 que las IPS deben reportar a la Superintendencia Nacional de Salud sobre su cartera por cobrar y los recaudos del período.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ReportSingleCircularFT025';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ReportSingleCircularFT025';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera la información agregada por tercero, tipo de entidad y modalidad de contratación para el reporte XML FT025 (Circular SNS), consolidando facturación radicada y recaudos del año hasta el mes solicitado.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSingleCircularFT025';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de criterios debe contener nodos /Data con Year y Month válidos para construir fechas con DATEFROMPARTS/EOMONTH.; Debe existir un registro en GeneralLedger.CompanySettings que defina la línea de negocio.; Las facturas radicadas consideradas deben estar en estado ''2'' (radicadas/confirmadas).; Las cuentas por cobrar deben ser de tipo 2 (AccountReceivableType=2) y los detalles de radicación no deben ser devolución (Devolution=0).', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSingleCircularFT025';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El periodo siempre arranca el 1 de enero del año (@InitialDate = DATEFROMPARTS(@Year,1,1)) y termina el último día del mes solicitado (acumulado año hasta el mes).; Solo se consideran facturas radicadas no devueltas (Devolution=0) y cuentas por cobrar de tipo 2.; Las transferencias y recibos reversados antes o el mismo @EndDate se excluyen (RecersalDate/ReversedDate > @EndDate).; Los registros con EntityType 7, 8 o 99 nunca se incluyen en el resultado final.; En la rama de recaudos solo se incluyen cuentas por cobrar con saldo recaudado positivo (suma de transfer y cash receipt > 0).; La línea de negocio reportada es la única configurada en GeneralLedger.CompanySettings.; Los errores no detienen la ejecución; se capturan y devuelven como fila con código ''999''.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSingleCircularFT025';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Circular FT025 (Supersalud); Tercero / NIT; EPS Contributivo; EPS Subsidiado; Entes Territoriales (Vinculados Municipios/Departamentos); ARL - Riesgos Laborales; Medicina Prepagada; Régimen Especial; Accidentes de tránsito (SOAT); Fosyga; Aseguradoras; Modalidades de contratación: Pago por Servicios, Capitación, Factura Global, Capitación Global, PGP (Pago Global Prospectivo); Cuentas por cobrar / Cartera; Radicación de facturas; Transferencia de cartera; Recibos de caja / Recaudo; Línea de negocio', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSingleCircularFT025';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultado del SP: Devuelve un conjunto agrupado por NIT, nombre del tercero, EntityType y LiquidationType con suma de InvoiceValue y CollectionValue, excluyendo entidades con EntityType en (7,8,99).; [RETURN_RESULT] Resultado del SP: En caso de error en el TRY, devuelve fila con CodeResult=''999'' y MessageResult con ERROR_MESSAGE() + número de línea.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSingleCircularFT025';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ar.EntityType del CareGroup → Se mapea a códigos del reporte: 1→1 (EPS Contributivo), 2→2 (EPS Subsidiado), 3/4→5 (ET Vinculados), 5→8 (ARL), 6→3 (Medicina Prepagada), 9→4 (Régimen Especial), 10→9 (Accidentes de tránsito), 11/12→4 (Fosyga/Otros), 13→7 (Aseguradoras) else 0; si ar.LiquidationType del CareGroup → Se mapea: 1→2 (Pago por Servicios), 2→1 (Capitación), 3→3 (Factura Global), 4→1 (Capitación Global), 5→3 (PGP) else 6; si ar.LiquidationType IN (1,2,3,4,5) → ContractType = ''NA'' else ContractType = ''Otro Tipo de Contratación''; si Para recaudos: CAST(ri.ConfirmDate AS DATE) BETWEEN @InitialDate AND @EndDate → Se suman como recaudo del periodo los valores previos (PreviousTransferValue + PreviousCashReceiptValue) además de los del periodo else Solo se suman los valores cuyo DocumentDate está dentro del periodo; si Subconsulta de transferencias: pt.Status IN (2,4) y DocumentDate<=@EndDate y RecersalDate (o día siguiente a @EndDate) > @EndDate → Se incluye la transferencia vigente al cierre; si Subconsulta de recibos: cr.Status IN (2,4) y DocumentDate<=@EndDate y ReversedDate > @EndDate → Se incluye el recibo de caja vigente al cierre', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSingleCircularFT025';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.CompanySettings; Portfolio.RadicateInvoiceC; Portfolio.RadicateInvoiceD; Portfolio.AccountReceivable; Contract.CareGroup; Portfolio.PortfolioTransfer; Portfolio.PortfolioTransferDetail; Treasury.CashReceipts; Treasury.CashReceiptDetails; Treasury.CashReceiptAccountReceivable; Common.ThirdParty', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSingleCircularFT025';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSingleCircularFT025';
-- GO
