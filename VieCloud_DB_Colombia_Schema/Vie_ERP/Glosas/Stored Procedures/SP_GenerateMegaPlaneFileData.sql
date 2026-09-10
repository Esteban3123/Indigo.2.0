-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-11-10
-- Description:	Procedimiento que se encarga de el generar los datos relacionados al archivo MegaPlano
-- =============================================
CREATE PROCEDURE [Glosas].[SP_GenerateMegaPlaneFileData] 
	@XmlParameters as XML,
	@XmlInvoices AS XML
AS
BEGIN
	SET NOCOUNT ON;
	
	/***************************************** VARIABLES *****************************************/

	--Variables de control
	DECLARE	@RadicateInvoiceId INT,
			@CodificationType INT,
			@ServiceCode INT,
			---------------------------------------------------------------------------------------
			@IPSCode VARCHAR(3),
			@IPSDocumentType VARCHAR(2),
			@IPSNit VARCHAR(15),
			@IPSDigitVerification VARCHAR(1),
			@IPSName VARCHAR(50),
			@CustomerDocumentType VARCHAR(2),
			@CustomerNit VARCHAR(15),
			@CustomerDigitVerification VARCHAR(1),
			@CustomerName VARCHAR(50),
			---------------------------------------------------------------------------------------
			@FilterByInvoices BIT = 0

	--Tabla para almacenar los items del listado que viene en el xml
	DECLARE @Table_Invoices TABLE(InvoiceId INT)

	--Facturas a generar
	DECLARE @Invoices TABLE
	(
		InvoiceId INT,
		InvoiceDate DATE,
		InvoicePrefix VARCHAR(6),
		InvoiceNumber VARCHAR(10),
		DetailInvoiceId INT,
		DetailInvoiceNumber VARCHAR(10),
		DetailDiscountValue DECIMAL(18,0),
		DetailModeratingFee DECIMAL(18,0),
		DetailCopay DECIMAL(18,0),
		DetailNetValue DECIMAL(18,0)
	)

	--Tabla para devolver los resultados
	DECLARE @TableResult TABLE
	(
		IPSCode VARCHAR(3), 
		IPSDocumentType VARCHAR(2),
		IPSNit VARCHAR(15),
		IPSDigitVerification VARCHAR(1),
		IPSName VARCHAR(50),
		CustomerDocumentType VARCHAR(2),
		CustomerNit VARCHAR(15),
		CustomerDigitVerification VARCHAR(1),
		CustomerName VARCHAR(50),
		---------------------------------------------------
		InvoiceId INT, 
		InvoiceDate VARCHAR(10),
		InvoicePrefix VARCHAR(6),
		InvoiceNumber VARCHAR(10),
		InvoiceGrossValue DECIMAL(18,0),
		InvoiceModeratingFee DECIMAL(18,0),
		InvoiceCopay DECIMAL(18,0),
		InvoiceNetValue DECIMAL(18,0),
		---------------------------------------------------
		DetailId INT,
		DetailInvoiceId INT,
		DetailInvoiceNumber VARCHAR(10),
		DetailAuthorizationNumber VARCHAR(15),
		DetailDocumentType VARCHAR(2),
		DetailDocumentNumber VARCHAR(16),
		DetailLastName VARCHAR(25),
		DetailFirstName VARCHAR(15),
		DetailAdmissionDate VARCHAR(10),
		DetailOutputDate VARCHAR(10), 
		DetailAttentionType VARCHAR(2),
		DetailCode VARCHAR(12), 
		DetailDescription VARCHAR(100), 
		DetailQuantity INT, 
		DetailUnitValue DECIMAL(18,0),
		DetailTotalValue DECIMAL(18,0),
		DetailDiagnostic VARCHAR(7),
		DetailModeratingFee DECIMAL(18,0),
		DetailCopay DECIMAL(18,0)
	)

	BEGIN TRY

		/*************************************** CRITERIOS ***************************************/

		SELECT	@IPSNit = t.x.value('IPSNit[1]','varchar(15)'),
				@RadicateInvoiceId = t.x.value('RadicateInvoiceId[1]','int'),
				@CodificationType = t.x.value('CodificationType[1]','int'),
				@ServiceCode = t.x.value('ServiceCode[1]','int')
		FROM @XmlParameters.nodes('/Data') t(x)

		INSERT INTO @Table_Invoices
			SELECT DISTINCT
				t.x.value('InvoiceId[1]','int') InvoiceId
			FROM @XmlInvoices.nodes('/Data') t(x)

		IF EXISTS(SELECT 1 FROM @Table_Invoices)
		BEGIN
			SET @FilterByInvoices = 1
		END

		INSERT INTO @Invoices 
			(
				InvoiceId, InvoiceDate, InvoicePrefix, InvoiceNumber, 
				DetailInvoiceId, DetailInvoiceNumber, DetailDiscountValue, DetailModeratingFee, DetailCopay, DetailNetValue
			)
			SELECT DISTINCT 
				IIF(i.DocumentType = 4 AND cc.DocumentType = 5, i.Id, ri.Id) InvoiceId,
				IIF(i.DocumentType = 4 AND cc.DocumentType = 5, i.InvoiceDate, ri.RadicatedDate) InvoiceDate,
				REPLACE(IIF(i.DocumentType = 4 AND cc.DocumentType = 5, i.InvoiceNumber, ''), dbo.udf_GetNumeric(IIF(i.DocumentType = 4 AND cc.DocumentType = 5, i.InvoiceNumber, '')), '') InvoicePrefix,
				REPLACE(LTRIM(REPLACE(dbo.udf_GetNumeric(IIF(i.DocumentType = 4 AND cc.DocumentType = 5, i.InvoiceNumber, ri.RadicatedConsecutive)),'0',' ')),' ','0') InvoiceNumber,
				IIF(i.DocumentType = 4 AND cc.DocumentType = 5, cc.Id, i.Id) AS DetailInvoiceId,
				i.InvoiceNumber DetailInvoiceNumber,
				IIF(i.DocumentType = 4, 0, i.ThirdPartyDiscountValue) DetailDiscountValue,
				IIF(i.DocumentType = 4, 0, 0) DetailModeratingFee,
				IIF(i.DocumentType = 4, 0, ISNULL(id.DetailCopay, 0)) DetailCopay,
				IIF(i.DocumentType = 4, 0, i.ThirdPartySalesValue) DetailNetValue
			FROM Portfolio.RadicateInvoiceC ri WITH (NOLOCK)
			JOIN Portfolio.RadicateInvoiceD rid WITH (NOLOCK) ON ri.Id = rid.RadicateInvoiceCId AND	rid.State <> 4
			JOIN Billing.Invoice i WITH (NOLOCK) ON rid.InvoiceNumber = i.InvoiceNumber AND i.Status = 1
			LEFT JOIN @Table_Invoices ti ON i.Id = ti.InvoiceId
			LEFT JOIN Billing.Invoice cc WITH (NOLOCK) ON i.DocumentType = 4 AND cc.DocumentType = 5 AND cc.Status = 1
				AND i.ThirdPartyId = cc.ThirdPartyId
				AND i.CareGroupId = cc.CareGroupId
				AND i.InvoiceCategoryId = cc.InvoiceCategoryId
				AND CAST(cc.InvoiceDate AS DATE) BETWEEN i.CapitationInitialDate AND i.CapitationEndDate
			LEFT JOIN
			(
				SELECT InvoiceId, SUM(ROUND(SubTotalPatientSalesPrice / InvoicedQuantity, 0)) DetailCopay
				FROM Billing.InvoiceDetail
				GROUP BY InvoiceId
			) id ON i.Id = id.InvoiceId
			WHERE ri.Id = @RadicateInvoiceId
				AND (@FilterByInvoices = 0 OR ti.InvoiceId IS NOT NULL)

		SELECT	@IPSCode = '001',
				@IPSDocumentType = Common.GetIdentificationTypeCode(p.IdentificationType, 2),
				@IPSDigitVerification = tp.DigitVerification,
				@IPSName = SUBSTRING(tp.Name, 1, 50)
		FROM Common.ThirdParty tp WITH (NOLOCK)
		JOIN Common.Person p WITH (NOLOCK) ON tp.PersonId = p.Id
		WHERE tp.Nit = @IPSNit

		SELECT	@CustomerDocumentType = Common.GetIdentificationTypeCode(p.IdentificationType, 2),
				@CustomerNit = tp.Nit,
				@CustomerDigitVerification = tp.DigitVerification,
				@CustomerName = SUBSTRING(tp.Name, 1, 50)
		FROM Portfolio.RadicateInvoiceC ri WITH (NOLOCK)
		JOIN Common.Customer c WITH (NOLOCK) ON ri.CustomerId = c.Id
		JOIN Common.ThirdParty tp WITH (NOLOCK) ON c.ThirdPartyId = tp.Id
		JOIN Common.Person p WITH (NOLOCK) ON tp.PersonId = p.Id
		WHERE ri.Id = @RadicateInvoiceId

		/*****************************************************************************************/

		INSERT INTO @TableResult 
			 SELECT	@IPSCode IPSCode,
					@IPSDocumentType IPSDocumentType,
					@IPSNit IPSNit,
					@IPSDigitVerification IPSDigitVerification,
					@IPSName IPSName,
					@CustomerDocumentType CustomerDocumentType,
					@CustomerNit CustomerNit,
					@CustomerDigitVerification CustomerDigitVerification,
					@CustomerName CustomerName,
					---------------------------------------------------
					xi.InvoiceId,
					CONVERT(VARCHAR(10),xi.InvoiceDate,103) InvoiceDate,
					xi.InvoicePrefix,
					xi.InvoiceNumber,
					0 DetailGrossValue,
					0 DetailModeratingFee,
					0 DetailCopay,
					0 DetailNetValue,
					---------------------------------------------------
					id.Id,
					xi.DetailInvoiceId,
					xi.DetailInvoiceNumber,
					SUBSTRING(COALESCE(sod.AuthorizationNumber, admission.IAUTORIZA, ''), 1, 15) DetailAuthorizationNumber,
					Common.GetIdentificationTypeCode(pacient.IPTIPODOC, 1) DetailDocumentType,
					pacient.IPCODPACI DetailDocumentNumber,
					SUBSTRING(CONCAT(LTRIM(RTRIM(pacient.IPPRIAPEL)), ' ', LTRIM(RTRIM(pacient.IPSEGAPEL))), 1, 25) DetailLastName,
					SUBSTRING(CONCAT(LTRIM(RTRIM(pacient.IPPRINOMB)), ' ', LTRIM(RTRIM(pacient.IPSEGNOMB))), 1, 15) DetailFirstName,
					CONVERT(VARCHAR(10), IIF(admission.IFECHAING > id.ServiceDate, id.ServiceDate, admission.IFECHAING), 103) DetailAdmissionDate,
					CONVERT(VARCHAR(10), IIF(Alta.FECALTPAC > id.ServiceDate, Alta.FECALTPAC, id.ServiceDate), 103) DetailOutputDate,
					'17' DetailAttentionType,
					SUBSTRING
					(	
						CASE sod.RecordType
							WHEN 1 THEN
								CASE @ServiceCode
									WHEN 1 THEN cups.RIPSCode
									WHEN 2 THEN cups.Code
									WHEN 3 THEN ips.Code
									WHEN 4 THEN cups.Code
									WHEN 5 THEN cups.Code
								END
							WHEN 2 THEN
								CASE @ServiceCode
									WHEN 1 THEN ip.Code
									WHEN 2 THEN ip.CodeAlternative
									WHEN 3 THEN ip.CodeAlternativeTwo
									WHEN 4 THEN ip.CodeAlternative
									WHEN 5 THEN ip.CodeAlternative
								END
						END, 1, 12
					) DetailCode,
					SUBSTRING
					(
						CASE sod.RecordType
							WHEN 1 THEN
								CASE @ServiceCode
									WHEN 1 THEN ISNULL(cups.RIPSDescription, cups.Description)
									WHEN 2 THEN ISNULL(cups.RIPSDescription, cups.Description)
									WHEN 3 THEN cups.RIPSCode
									WHEN 4 THEN COALESCE(cd.Name, cups.RIPSDescription, cups.Description)
									WHEN 5 THEN CONCAT(ISNULL(cups.RIPSDescription, cups.Description), ' ' + cd.Name)
								END
							WHEN 2 THEN ip.Name
						END, 1, 100
					) DetailDescription,
					id.InvoicedQuantity DetailQuantity,
					ROUND(id.GrandTotalSalesPrice / id.InvoicedQuantity, 0) DetailUnitValue,
					id.GrandTotalSalesPrice DetailTotalValue,
					SUBSTRING(COALESCE(i.OutputDiagnosis,admission.CODDIAEGR,'Z000'), 1, 7) DetailDiagnostic,
					0 DetailModeratingFee,
					0 DetailCopay
			FROM @Invoices xi		
			JOIN Billing.Invoice i WITH (NOLOCK) ON xi.DetailInvoiceId = i.Id
			JOIN Billing.InvoiceDetail id WITH (NOLOCK) ON i.Id = id.InvoiceId
			JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON id.ServiceOrderDetailId = sod.Id
			JOIN Billing.ServiceOrder so WITH (NOLOCK) ON sod.ServiceOrderId = so.Id
			JOIN dbo.ADINGRESO admission WITH (NOLOCK) ON so.AdmissionNumber = admission.NUMINGRES
			JOIN dbo.INPACIENT pacient WITH (NOLOCK) ON i.PatientCode = pacient.IPCODPACI
			LEFT JOIN 
			(
				SELECT Alta.NUMINGRES, MAX(Alta.FECALTPAC) FECALTPAC
				FROM
				(
						SELECT NUMINGRES, MAX(FECFINEST) AS FECALTPAC 
						FROM dbo.CHREGESTA AS CHR WITH (NOLOCK) 
						JOIN dbo.CHCAMASHO AS CHC WITH (NOLOCK) ON CHR.CODICAMAS = CHC.CODICAMAS 
						WHERE CHC.CODCLACAM  = 1 
						GROUP BY NUMINGRES
					UNION ALL
						SELECT NUMINGRES, FECALTPAC
						FROM dbo.HCREGEGRE
				) Alta
				GROUP BY NUMINGRES
			) AS Alta ON admission.NUMINGRES = ALTA.NUMINGRES
			LEFT JOIN Contract.IPSService ips WITH (NOLOCK) ON sod.IPSServiceId = ips.Id
			LEFT JOIN Contract.CUPSEntity cups WITH (NOLOCK) ON sod.CUPSEntityId = cups.Id
			LEFT JOIN Contract.CUPSEntityContractDescriptions cecd WITH (NOLOCK) ON sod.CUPSEntityContractDescriptionId = cecd.Id
			LEFT JOIN Contract.ContractDescriptions cd WITH (NOLOCK) ON cecd.ContractDescriptionId = cd.Id
			LEFT JOIN Inventory.InventoryProduct IP WITH (NOLOCK) ON sod.ProductId = ip.id 
			LEFT JOIN Inventory.ProductType as PT WITH (NOLOCK) ON ip.ProductTypeId = PT.Id
			WHERE id.InvoicedQuantity > 0 AND sod.SettlementType <> 3 AND sod.IsDelete = 0
				AND
				(
					(sod.RecordType = 1 AND cups.RIPSConcept in ('06','07','08','09','11','14'))
					OR
					(sod.RecordType = 2 AND pt.Class = 3)
				)

		-- Se agrega el valor del copago y cuota moderadora al primer registro de cada factura
		UPDATE tr
			SET tr.DetailModeratingFee = xi.DetailModeratingFee,
				tr.DetailCopay = xi.DetailCopay
		FROM @Invoices xi
		JOIN @TableResult tr ON xi.DetailInvoiceId = tr.DetailInvoiceId
		JOIN
		(
			SELECT InvoiceId, DetailInvoiceId, MIN(DetailId) DetailId
			FROM @TableResult
			GROUP BY InvoiceId, DetailInvoiceId
		) trd ON tr.InvoiceId = trd.InvoiceId AND tr.DetailInvoiceId = trd.DetailInvoiceId AND tr.DetailId = trd.DetailId

		-- Se agrega mayoriza el valor del valor factura, copago y cuota moderadora, y valor neto en el registro de cabecera
		UPDATE tr
			SET tr.InvoiceGrossValue = trd.DetailNetValue + trd.DetailDiscountValue + trd.DetailModeratingFee + trd.DetailCopay,
				tr.InvoiceModeratingFee = trd.DetailModeratingFee,
				tr.InvoiceCopay = trd.DetailCopay,
				tr.InvoiceNetValue = trd.DetailNetValue
		FROM @TableResult tr
		JOIN
		(
			SELECT	tr.InvoiceId, 					
					SUM(tr.DetailModeratingFee) DetailModeratingFee, 
					SUM(tr.DetailCopay) DetailCopay,
					SUM(xi.DetailNetValue) DetailNetValue,
					SUM(xi.DetailDiscountValue) DetailDiscountValue
			FROM 
			(
				SELECT	tr.InvoiceId,
						tr.DetailInvoiceId,
						SUM(tr.DetailModeratingFee) DetailModeratingFee,
						SUM(tr.DetailCopay) DetailCopay
				FROM @TableResult tr
				GROUP BY tr.InvoiceId, tr.DetailInvoiceId
			) tr 
			JOIN @Invoices xi ON tr.InvoiceId = xi.InvoiceId AND tr.DetailInvoiceId = xi.DetailInvoiceId
			GROUP BY tr.InvoiceId
		) trd ON tr.InvoiceId = trd.InvoiceId

	END TRY
	BEGIN CATCH
		PRINT ERROR_MESSAGE() + ' Linea: ' + cast(ERROR_LINE() AS VARCHAR(20))
	END CATCH

	--Se retorna la tabla con los resultados
	SELECT * 
	FROM @TableResult
	ORDER BY InvoicePrefix, InvoiceNumber, DetailInvoiceNumber, DetailId
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera los datos estructurados para el archivo MegaPlano de glosas, utilizado en el proceso de radicación y cobro de facturas ante entidades pagadoras (EPS, aseguradoras, empresas). A partir de un radicado de cartera y un conjunto de facturas recibidos como parámetros XML, consolida la información de encabezado y detalle de cada factura: datos de la IPS prestadora, datos del pagador o cliente, valores brutos, cuotas moderadoras, copagos y valores netos. Integra las tablas de radicación de cartera (RadicateInvoiceC y RadicateInvoiceD) con las facturas de cobro (Billing.Invoice) para construir el resultado completo que alimenta el archivo plano exigido por el proceso de glosas y conciliación de cuentas.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateMegaPlaneFileData';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateMegaPlaneFileData';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el dataset estructurado (cabecera IPS/Cliente, factura y detalle de servicios/medicamentos) requerido para el archivo MegaPlano de glosas a partir de las facturas asociadas a una radicación de cartera.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateMegaPlaneFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se debe recibir un XML de parámetros con IPSNit, RadicateInvoiceId, CodificationType y ServiceCode.; La radicación (RadicateInvoiceC) debe existir y tener detalles con State distinto de 4.; Las facturas asociadas deben tener Status = 1 (activas).; El IPS identificado por IPSNit debe existir en Common.ThirdParty.; La radicación debe estar vinculada a un Customer con su ThirdParty y Person.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateMegaPlaneFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'IPSCode siempre se fija en ''001''.; DetailAttentionType siempre se fija en ''17''.; DetailDiagnostic toma ''Z000'' como valor por defecto cuando no hay diagnóstico de salida ni de egreso.; Los nombres y apellidos del paciente se truncan a 25 (apellidos) y 15 (nombres) caracteres.; Los códigos y descripciones se truncan a 12 y 100 caracteres respectivamente.; DetailUnitValue se calcula como GrandTotalSalesPrice/InvoicedQuantity redondeado a entero.; Sólo se procesan facturas activas (Status=1) y detalles de radicación con State<>4.; Las facturas de capitación (DocumentType=4) no aportan valores económicos al detalle (Discount, ModeratingFee, Copay, NetValue = 0).; Los valores de copago y cuota moderadora no se duplican: se asignan únicamente al primer detalle de cada factura.; Cualquier excepción es atrapada y solo se imprime el mensaje, no se relanza.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateMegaPlaneFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @TableResult: Se inserta una fila por cada InvoiceDetail cuya InvoicedQuantity > 0, sod.SettlementType <> 3, sod.IsDelete = 0 y que sea (RecordType=1 con CUPS.RIPSConcept en 06,07,08,09,11,14) o (RecordType=2 con ProductType.Class=3, es decir medicamentos).; [UPDATE] @TableResult: El valor de cuota moderadora y copago de la factura sólo se asigna al primer detalle (MIN(DetailId)) de cada combinación InvoiceId/DetailInvoiceId; los demás detalles quedan en 0.; [UPDATE] @TableResult: En el registro cabecera se totaliza: InvoiceGrossValue = NetValue + Discount + ModeratingFee + Copay; InvoiceNetValue = suma de DetailNetValue; InvoiceModeratingFee y InvoiceCopay se mayorizan por InvoiceId.; [RETURN_RESULT] @TableResult: Se devuelve el contenido de @TableResult ordenado por InvoicePrefix, InvoiceNumber, DetailInvoiceNumber, DetailId.; [INSERT] @Invoices: Cuando i.DocumentType=4 (factura de capitación) y existe una nota cc.DocumentType=5 con mismo ThirdParty/CareGroup/InvoiceCategory y fecha dentro del rango de capitación, se toma cc.Id como DetailInvoiceId y se anulan Discount, ModeratingFee, Copay y NetValue (quedan en 0).; [INSERT] @Invoices: Si se proporcionó la lista @Table_Invoices (FilterByInvoices=1), sólo se incluyen facturas cuyo Id esté en dicha lista; en caso contrario se incluyen todas las de la radicación.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateMegaPlaneFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existen registros en @Table_Invoices (XML de facturas no vacío) → Se activa @FilterByInvoices=1 y se filtran las facturas a las indicadas en el XML else Se procesan todas las facturas vinculadas a la radicación; si i.DocumentType = 4 AND existe cc con DocumentType = 5 dentro del rango de capitación → Se usan los datos de la factura origen (i) para cabecera y los de la nota (cc) como detalle, con valores económicos en 0 else Se usa la radicación (ri) para cabecera y la factura (i) como detalle con sus valores reales; si sod.RecordType = 1 (servicio CUPS) → DetailCode/Description se toman de CUPSEntity/IPSService según @ServiceCode (1=RIPS,2=Code,3=IPS Code,4/5=Code con descripción de contrato) else Si sod.RecordType = 2 (producto): DetailCode/Description se toman de InventoryProduct según @ServiceCode; si admission.IFECHAING > id.ServiceDate → DetailAdmissionDate se establece como id.ServiceDate else Se usa admission.IFECHAING; si Alta.FECALTPAC > id.ServiceDate → DetailOutputDate = Alta.FECALTPAC else DetailOutputDate = id.ServiceDate; si Filtro de inclusión por tipo de registro → Sólo se incluyen servicios CUPS con RIPSConcept en (06,07,08,09,11,14) o productos con ProductType.Class=3', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateMegaPlaneFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GetIdentificationTypeCode; dbo.udf_GetNumeric', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateMegaPlaneFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.RadicateInvoiceC; Portfolio.RadicateInvoiceD; Billing.Invoice; Billing.InvoiceDetail; Billing.ServiceOrder; Billing.ServiceOrderDetail; Common.ThirdParty; Common.Person; Common.Customer; dbo.ADINGRESO; dbo.INPACIENT; dbo.CHREGESTA; dbo.CHCAMASHO; dbo.HCREGEGRE; Contract.IPSService; Contract.CUPSEntity; Contract.CUPSEntityContractDescriptions; Contract.ContractDescriptions; Inventory.InventoryProduct; Inventory.ProductType', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateMegaPlaneFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateMegaPlaneFileData';
-- GO
