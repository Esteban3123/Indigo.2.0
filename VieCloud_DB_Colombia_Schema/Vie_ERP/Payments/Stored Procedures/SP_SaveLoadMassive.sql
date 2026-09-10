-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2018-06-11
-- Description:	Procedimiento que se encarga de cargar facturas masivamente
-- =============================================
CREATE PROCEDURE [Payments].[SP_SaveLoadMassive] 
	@XmlLoadMassive as Xml,
	@XmlBills as Xml,
	@XmlDetails as Xml
AS
BEGIN
	SET NOCOUNT ON;
	SET DATEFORMAT DMY
	
	/************************************* VARIABLES *************************************/

	DECLARE @Id INT, 
			@OperatingUnitId INT,
			@Code VARCHAR(20), 
			@DocumentDate DATETIME, 
			@Observations VARCHAR(200),
			@Status TINYINT,
			@CodeUser VARCHAR(20)

	DECLARE @TableBills TABLE
	(
		Id INT, 
		StatusField INT, 
		MessageField VARCHAR(MAX), 
		-- AccountPayable ---
		[HeadId] [int],
		[SupplierNit] [varchar](500),
		[SupplierId] [int],
		[ThirdPartyId] [int],
		[DistributionLineCode] [varchar](500),
		[DistributionLineId] [int],
		[SuppliersDistributionLineId] [int],
		[MainAccountNumber] [varchar](500),
		[MainAccountId] [int],
		[CostCenterCode] [varchar](500),
		[CostCenterId] [int],
		[BillNumber] [varchar](100),
		HandlesDocumentSupport BIT,
		[DocumentDate] [datetime],
		[BillDate] [datetime],
		[FilingUnitCode] [varchar](500),
		[FilingUnitId] [int],
		[SupplierTypeCode] [varchar](500),
		[SupplierTypeId] [int],
		[Term] [int],
		[InvoiceValue] [numeric](20, 4),
		[Value] [numeric](20, 4) DEFAULT(0),
		[Coments] [varchar](max),
		---------------------
		RowId INT,
		[AccountPayableCode] [varchar](20),
		[AccountPayableId] [int],
		[LoadMassiveAccountPayableId] [int]
	)

	DECLARE @TableDetails TABLE
	(
		Id INT, 
		StatusField INT DEFAULT(1), 
		MessageField VARCHAR(MAX), 
		-- AccountPayable ---
		[HeadId] [int],
		-- AccountPayableDetailConcept --
		[AccountPayableConceptCode] [varchar](500),
		[AccountPayableConceptId] [int],
		[MainAccountNumber] [varchar](500),
		[MainAccountId] [int],
		[ThirdPartyNit] [varchar](500),
		[ThirdPartyId] [int],
		[CostCenterCode] [varchar](500),
		[CostCenterId] [int],
		[Detail] [varchar](500),
		[Nature] [tinyint],
		[Value] [numeric](20, 4),
		[RetentionConceptCode] [varchar](500),
		[RetentionConceptId] [int],
		[BaseValue] [numeric](20, 4),
		[Percentage] NUMERIC(18,3),
		[TypeRounding] [tinyint]
	)
	
	DECLARE @TableResult TABLE
	(
		Id INT IDENTITY, 
		StatusField INT, 
		MessageField VARCHAR(MAX), 
		-- LoadMassive ---
		[LoadMassiveId] [int],
		[LoadMassiveCode] [varchar](20),
		-- AccountPayable ---
		[HeadId] [int],
		[SupplierNit] [varchar](500),
		[SupplierId] [int],
		[DistributionLineCode] [varchar](500),
		[DistributionLineId] [int],
		[SuppliersDistributionLineId] [int],
		[AccountPayableMainAccountNumber] [varchar](500),
		[AccountPayableMainAccountId] [int],
		[AccountPayableCostCenterCode] [varchar](500),
		[AccountPayableCostCenterId] [int],
		[BillNumber] [varchar](100),
		[DocumentDate] [datetime],
		[BillDate] [datetime],
		[FilingUnitCode] [varchar](500),
		[FilingUnitId] [int],
		[SupplierTypeCode] [varchar](500),
		[SupplierTypeId] [int],
		[Term] [int],
		[InvoiceValue] [numeric](20, 4),
		[AccountPayableValue] [numeric](20, 4),
		[Coments] [varchar](max),
		[AccountPayableCode] [varchar](20),
		[AccountPayableId] [int],
		-- AccountPayableDetailConcept --
		[DetailId] [int],
		[AccountPayableConceptCode] [varchar](500),
		[AccountPayableConceptId] [int],		
		[MainAccountNumber] [varchar](500),
		[MainAccountId] [int],
		[ThirdPartyNit] [varchar](500),
		[ThirdPartyId] [int],
		[CostCenterCode] [varchar](500),
		[CostCenterId] [int],
		[Detail] [varchar](500),
		[Nature] [tinyint],
		[Value] [numeric](20, 4),
		[RetentionConceptCode] [varchar](500),
		[RetentionConceptId] [int],		
		[BaseValue] [numeric](20, 4),
		[Percentage] NUMERIC(18,3),
		[TypeRounding] [tinyint]
	)

	--Variables para los consecutivos
	DECLARE @billsValid INT,
			@IdForm VARCHAR(5),
			@pattern VARCHAR(300),
			@NextS INT,
			@idSequenceDetail INT,
			@ConsecutiveFiling INT

	BEGIN TRY

		/************************************* CARGUE DE DATOS *************************************/

		SELECT 
			@Id = t.x.value('Id[1]','int'),
			@OperatingUnitId = t.x.value('OperatingUnitId[1]','int'),
			@Code = t.x.value('Code[1]','varchar(20)'),
			@DocumentDate = t.x.value('DocumentDate[1]','datetime'),
			@Observations = t.x.value('Observations[1]','varchar(200)'),
			@Status = t.x.value('Status[1]','tinyint'),
			@CodeUser = t.x.value('ModificationUser[1]','varchar(20)')
		FROM @XmlLoadMassive.nodes('/Data') t(x)

		INSERT INTO @TableBills
		(
			Id, StatusField, MessageField,
			HeadId, SupplierNit, DistributionLineCode, CostCenterCode, BillNumber, HandlesDocumentSupport, DocumentDate, BillDate, FilingUnitCode, SupplierTypeCode, Term, InvoiceValue, Coments
		)
		SELECT 
			t.x.value('Id[1]','int') as Id,
			t.x.value('StatusField[1]','int') as StatusField,
			t.x.value('MessageField[1]','varchar(MAX)') as MessageField,
			--------------------------------------------------------------
			t.x.value('HeadId[1]','int') as HeadId,
			t.x.value('SupplierNit[1]','varchar(20)') as SupplierNit,
			t.x.value('DistributionLineCode[1]','varchar(20)') as DistributionLineCode,
			t.x.value('AccountPayableCostCenterCode[1]','varchar(20)') as CostCenterCode,
			t.x.value('BillNumber[1]','varchar(100)') as BillNumber,
			t.x.value('HandlesDocumentSupport[1]','bit') as HandlesDocumentSupport,
			t.x.value('DocumentDate[1]','datetime') as DocumentDate,
			t.x.value('BillDate[1]','datetime') as BillDate,
			t.x.value('FilingUnitCode[1]','varchar(20)') as FilingUnitCode,
			t.x.value('SupplierTypeCode[1]','varchar(20)') as SupplierTypeCode,
			IIF(t.x.value('Term[1]','varchar(20)') = '', 0, t.x.value('Term[1]','varchar(20)')) as Term,
			IIF(t.x.value('InvoiceValue[1]','varchar(20)') = '', 0, t.x.value('InvoiceValue[1]','numeric(20,4)')) as InvoiceValue,
			t.x.value('Coments[1]','varchar(MAX)') as Coments
		FROM @XmlBills.nodes('/Data/Row') t(x)

		INSERT INTO @TableDetails
		(
			Id, HeadId, 
			AccountPayableConceptCode, ThirdPartyNit, CostCenterCode, Detail, Nature, [Value], RetentionConceptCode, BaseValue
		)
		SELECT 
			t.x.value('Id[1]','int') as Id,
			t.x.value('HeadId[1]','int') as HeadId,
			t.x.value('AccountPayableConceptCode[1]','varchar(20)') as AccountPayableConceptCode,
			t.x.value('ThirdPartyNit[1]','varchar(20)') as ThirdPartyNit,
			t.x.value('CostCenterCode[1]','varchar(20)') as CostCenterCode,
			t.x.value('Detail[1]','varchar(500)') as Detail,
			t.x.value('Nature[1]','varchar(20)') as Nature,
			t.x.value('Value[1]','varchar(20)') as [Value],
			t.x.value('RetentionConceptCode[1]','varchar(20)') as RetentionConceptCode,
			t.x.value('BaseValue[1]','varchar(20)') as BaseValue
		FROM @XmlDetails.nodes('/Data/Row') t(x)

		/************************************* VALIDACIONES MASIVAS *************************************/

		----------------------- AccountPayable -----------------------

		UPDATE tb
			SET tb.StatusField = 0,
				tb.MessageField = 'El registro ' + CAST(tb.Id AS VARCHAR(20)) + ' tiene un identificador duplicado'
		FROM @TableBills tb
		JOIN @TableBills tb2 ON tb.HeadId = tb2.HeadId AND tb.Id <> tb2.Id
		WHERE tb.StatusField = 1

		UPDATE tb
			SET tb.StatusField = IIF(s.Id IS NULL, 0, tb.StatusField),
				tb.MessageField = IIF(s.Id IS NULL, 'El registro ' + CAST(tb.Id AS VARCHAR(20)) + ' debe tener un proveedor (' + tb.SupplierNit + ') valido', tb.MessageField),
				tb.SupplierId = s.Id,
				tb.SupplierNit = CONCAT(s.Code, ' - ', s.Name),
				tb.ThirdPartyId = tp.Id
		FROM @TableBills tb
		LEFT JOIN Common.ThirdParty tp WITH (NOLOCK) ON tb.SupplierNit = tp.Nit
		LEFT JOIN Common.Supplier s WITH (NOLOCK) ON tp.Id = s.IdThirdParty
		WHERE tb.StatusField = 1

		UPDATE tb
			SET tb.StatusField = IIF(dl.Id IS NULL, 0, tb.StatusField),
				tb.MessageField = IIF(dl.Id IS NULL, 'El registro ' + CAST(tb.Id AS VARCHAR(20)) + ' debe tener una linea de distribucion (' + tb.DistributionLineCode + ') valida', tb.MessageField),
				tb.DistributionLineId = dl.Id,
				tb.DistributionLineCode = CONCAT(dl.Code, ' - ', dl.Name)
		FROM @TableBills tb
		LEFT JOIN Common.DistributionLines dl WITH (NOLOCK) ON tb.DistributionLineCode = dl.Code
		WHERE tb.StatusField = 1

		UPDATE tb
			SET tb.StatusField = IIF(sdl.Id IS NULL, 0, tb.StatusField),
				tb.MessageField = IIF(sdl.Id IS NULL, 'El registro ' + CAST(tb.Id AS VARCHAR(20)) + ' debe tener un proveedor (' + tb.SupplierNit + ') asociado a la linea de distribucion (' + tb.DistributionLineCode + ')', tb.MessageField),
				tb.SuppliersDistributionLineId = sdl.Id
		FROM @TableBills tb
		LEFT JOIN Common.SuppliersDistributionLines sdl WITH (NOLOCK) ON tb.SupplierId = sdl.IdSupplier AND tb.DistributionLineId = sdl.IdDistributionLine
		WHERE tb.StatusField = 1

		UPDATE tb
			SET tb.StatusField = IIF(cc.Id IS NULL, 0, tb.StatusField),
				tb.MessageField = IIF(ma.HandlesCostCenter = 1 AND cc.Id IS NULL, 'El registro ' + CAST(tb.Id AS VARCHAR(20)) + ' debe tener un centro de costo (' + tb.CostCenterCode + ') valido', tb.MessageField),
				tb.MainAccountId = ma.Id,
				tb.MainAccountNumber = CONCAT(ma.Number, ' - ', ma.Name),
				tb.CostCenterId = cc.Id,
				tb.CostCenterCode = CONCAT(cc.Code, ' - ', cc.Name)
		FROM @TableBills tb
		JOIN Common.DistributionLines dl WITH (NOLOCK) ON tb.DistributionLineId = dl.Id
		JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON dl.IdMainAccount = ma.Id
		LEFT JOIN Payroll.CostCenter cc WITH (NOLOCK) ON tb.CostCenterCode = cc.Code
		WHERE tb.StatusField = 1

		UPDATE tb
			SET tb.StatusField = 0,
				tb.MessageField = 'El registro ' + CAST(tb.Id AS VARCHAR(20)) + ' en el numero en la factura debe tener al menos un numero'
		FROM @TableBills tb
		WHERE tb.StatusField = 1 AND NOT tb.BillNumber LIKE '%[0-9]%'

		UPDATE tb
			SET tb.StatusField = 0,
				tb.MessageField = 'El registro ' + CAST(tb.Id AS VARCHAR(20)) + ' corresponde a una factura ' + tb.BillNumber +  ' que ya existe con el proveedor'
		FROM @TableBills tb
		JOIN Payments.AccountPayable ap WITH (NOLOCK) ON tb.BillNumber = ap.BillNumber AND ap.IdSupplier = tb.SupplierId
		WHERE tb.StatusField = 1 AND ap.Status <> 3

		UPDATE tb
			SET tb.StatusField = 0,
				tb.MessageField = 'El registro ' + CAST(tb.Id AS VARCHAR(20)) + ' corresponde a una factura ' + tb.BillNumber +  ' duplicada'
		FROM @TableBills tb
		JOIN @TableBills tb2 ON tb.BillNumber = tb2.BillNumber AND tb.SupplierId = tb2.SupplierId AND tb.Id <> tb2.Id
		WHERE tb.StatusField = 1

		UPDATE tb
			SET tb.StatusField = 0,
				tb.MessageField = 'El registro ' + CAST(tb.Id AS VARCHAR(20)) + ' debe tene una fecha de factura que no supere la fecha actual'
		FROM @TableBills tb
		WHERE tb.StatusField = 1
			AND CAST(tb.BillDate AS DATE) > [Common].[GETDATE]()

		UPDATE tb
			SET tb.StatusField = 0,
				tb.MessageField = 'El registro ' + CAST(tb.Id AS VARCHAR(20)) + ' debe tene una fecha de documento en un periodo abierto de contabilidad'
		FROM @TableBills tb
		LEFT JOIN GeneralLedger.ClosedMonth cm WITH (NOLOCK) ON YEAR(tb.DocumentDate) = cm.Year AND MONTH(tb.DocumentDate) = cm.Month AND cm.Status = 1		
		WHERE tb.StatusField = 1 AND cm.Id IS NULL

		UPDATE tb
			SET tb.StatusField = 0,
				tb.MessageField = 'El registro ' + CAST(tb.Id AS VARCHAR(20)) + ' debe tene una fecha de documento que no supere la fecha actual'
		FROM @TableBills tb
		WHERE tb.StatusField = 1
			AND CAST(tb.DocumentDate AS DATE) > [Common].[GETDATE]()

		UPDATE tb
			SET tb.StatusField = IIF(fu.Id IS NULL, 0, tb.StatusField),
				tb.MessageField = IIF(fu.Id IS NULL, 'El registro ' + CAST(tb.Id AS VARCHAR(20)) + ' debe tener una unidad de radicacion (' + tb.FilingUnitCode + ') valida', tb.MessageField),
				tb.FilingUnitId = fu.Id,
				tb.FilingUnitCode = CONCAT(fu.Code, ' - ', fu.Name)
		FROM @TableBills tb
		LEFT JOIN Payments.FilingUnit fu WITH (NOLOCK) ON tb.FilingUnitCode = fu.Code
		WHERE tb.StatusField = 1

		UPDATE tb
			SET tb.StatusField = IIF(st.Id IS NULL, 0, tb.StatusField),
				tb.MessageField = IIF(st.Id IS NULL, 'El registro ' + CAST(tb.Id AS VARCHAR(20)) + ' debe tener un tipo de proveedor (' + tb.SupplierTypeCode + ') valido', tb.MessageField),
				tb.SupplierTypeId = st.Id,
				tb.SupplierTypeCode = CONCAT(st.Code, ' - ', st.Name)
		FROM @TableBills tb
		LEFT JOIN Common.SupplierType st WITH (NOLOCK) ON tb.SupplierTypeCode = st.Code
		WHERE tb.StatusField = 1

		UPDATE tb
			SET tb.StatusField = IIF(sdt.Id IS NULL, 0, tb.StatusField),
				tb.MessageField = IIF(sdt.Id IS NULL, 'El registro ' + CAST(tb.Id AS VARCHAR(20)) + ' debe tener un proveedor (' + tb.SupplierNit + ') asociado al tipo (' + tb.SupplierTypeCode + ')', tb.MessageField)
		FROM @TableBills tb
		LEFT JOIN Common.SupplierDetailType sdt WITH (NOLOCK) ON tb.SupplierId = sdt.SupplierId AND tb.SupplierTypeId = sdt.SupplierTypeId
		WHERE tb.StatusField = 1

		UPDATE tb
			SET tb.StatusField = 0,
				tb.MessageField = 'El registro ' + CAST(tb.Id AS VARCHAR(20)) + ' debe tener un plazo mayor o igual a 0'
		FROM @TableBills tb
		WHERE tb.StatusField = 1
			AND tb.Term < 0

		UPDATE tb
			SET tb.StatusField = 0,
				tb.MessageField = 'El registro ' + CAST(tb.Id AS VARCHAR(20)) + ' debe tener un valor de factura mayor o igual a 0'
		FROM @TableBills tb
		WHERE tb.StatusField = 1
			AND NOT (tb.InvoiceValue > 0)

		----------------------- AccountPayableDetailConcept -----------------------

		UPDATE td
			SET td.StatusField = IIF(apc.Id IS NULL, 0, td.StatusField),
				td.MessageField = IIF(apc.Id IS NULL, 
									'El registro ' + CAST(td.Id AS VARCHAR(20)) + ' debe tener un concepto ' + td.AccountPayableConceptCode +  ' valido', 
									IIF(apc.IdAccount IS NULL, 'El registro ' + CAST(td.Id AS VARCHAR(20)) + ' debe tener un concepto (' + td.AccountPayableConceptCode + ') de tipo especifico', td.MessageField)),
				td.AccountPayableConceptId = apc.Id,
				td.AccountPayableConceptCode = CONCAT(apc.Code, ' - ', apc.Name),
				td.MainAccountId = apc.IdAccount,
				td.RetentionConceptId = apc.RetentionConceptId				
		FROM @TableDetails td
		LEFT JOIN Payments.AccountPayableConcepts apc WITH (NOLOCK) ON td.AccountPayableConceptCode = apc.Code
		WHERE td.StatusField = 1

		UPDATE td
			SET td.StatusField = IIF(tp.Id IS NULL, 0, td.StatusField),
				td.MessageField = IIF(tp.Id IS NULL, 'El registro ' + CAST(td.Id AS VARCHAR(20)) + ' debe tener un tercero ' + td.ThirdPartyNit +  ' valido', td.MessageField),
				td.ThirdPartyId = tp.Id,
				td.ThirdPartyNit = CONCAT(tp.Nit, ' - ', tp.Name)
		FROM @TableDetails td
		LEFT JOIN Common.ThirdParty tp WITH (NOLOCK) ON td.ThirdPartyNit = tp.Nit
		WHERE td.StatusField = 1

		UPDATE td
			SET td.StatusField = IIF(cc.Id IS NULL, 0, td.StatusField),
				td.MessageField = IIF(cc.Id IS NULL, 'El registro ' + CAST(td.Id AS VARCHAR(20)) + ' debe tener un centro de costo (' + td.CostCenterCode + ') valido', td.MessageField),
				td.CostCenterId = cc.Id,
				td.CostCenterCode = CONCAT(cc.Code, ' - ', cc.Name),
				td.MainAccountNumber = CONCAT(ma.Number, ' - ', ma.Name)
		FROM @TableDetails td
		JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON td.MainAccountId = ma.Id
		LEFT JOIN Payroll.CostCenter cc WITH (NOLOCK) ON td.CostCenterCode = cc.Code
		WHERE td.StatusField = 1 AND ma.HandlesCostCenter = 1

		UPDATE td
			SET td.StatusField = 0,
				td.MessageField = 'El registro ' + CAST(td.Id AS VARCHAR(20)) + ' debe tener una naturaleza ' + CAST(ISNULL(td.Nature, 0) AS VARCHAR(20)) +  ' valida (1. Debito, 2. Credito)'
		FROM @TableDetails td
		WHERE td.StatusField = 1
			AND ISNULL(td.Nature, 0) NOT IN (1,2)

		UPDATE td
			SET td.StatusField = 0,
				td.MessageField = 'El registro ' + CAST(td.Id AS VARCHAR(20)) + ' debe tener un valor mayor o igual a 0'
		FROM @TableDetails td
		WHERE td.StatusField = 1
			AND NOT (td.Value > 0)

		UPDATE td
			SET td.StatusField = IIF(rc.Id IS NULL, 0, td.StatusField),
				td.MessageField = IIF(rc.Id IS NULL, 
									'El registro ' + CAST(td.Id AS VARCHAR(20)) + ' debe tener un concepto de retención valido', 
									IIF(ISNULL(rc.Rate, 0) <= 0, 'El registro ' + CAST(td.Id AS VARCHAR(20)) + ' debe tener un concepto de retención con un porcentaje establecido', td.MessageField)),
				td.RetentionConceptId = rc.Id,
				td.RetentionConceptCode = CONCAT(rc.Code, ' - ', rc.Name),
				td.Percentage = rc.Rate,
				td.TypeRounding = rc.TypeRounding
		FROM @TableDetails td
		JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON td.MainAccountId = ma.Id
		LEFT JOIN GeneralLedger.RetentionConcepts rc WITH (NOLOCK) ON (td.RetentionConceptId = rc.Id) OR (td.RetentionConceptId IS NULL AND td.RetentionConceptCode = rc.Code)
		WHERE td.StatusField = 1 AND ma.RetencionType <> 0

		UPDATE td
			SET td.StatusField = 0,
				td.MessageField = 'El registro ' + CAST(td.Id AS VARCHAR(20)) + ' debe tener un valor base de retención mayor a 0'
		FROM @TableDetails td
		WHERE td.StatusField = 1 AND td.RetentionConceptId IS NOT NULL
			AND NOT (td.BaseValue >= 0)

		UPDATE td
			SET td.StatusField = 0,
				td.MessageField = 'El registro ' + CAST(td.Id AS VARCHAR(20)) + ' debe tener el siguiente valor calculado de retencion (' +
					CAST(CAST(
						(
							ROUND(
								(td.BaseValue * td.Percentage) / 100, 
								CASE td.TypeRounding
									WHEN 1 THEN 0
									WHEN 2 THEN -1
									WHEN 3 THEN -2
									WHEN 4 THEN -3
									WHEN 5 THEN 1
									WHEN 6 THEN 2
									ELSE 4
								END
							)
						) AS FLOAT) AS VARCHAR(20))
					+ ')'
		FROM @TableDetails td
		WHERE td.StatusField = 1 AND td.RetentionConceptId IS NOT NULL
			AND 
			(
				td.Value <> ROUND(
					(td.BaseValue * td.Percentage) / 100, 
					CASE td.TypeRounding
						WHEN 1 THEN 0
						WHEN 2 THEN -1
						WHEN 3 THEN -2
						WHEN 4 THEN -3
						WHEN 5 THEN 1
						WHEN 6 THEN 2
						ELSE 4
					END
				)
			)

		-- Si existen errores para alguno de los detalles de la factura, la marcamos como erronea
		UPDATE tb
			SET tb.StatusField = 0,
				tb.MessageField = 'El registro ' + CAST(td.Id AS VARCHAR(20)) + ' tiene un error en alguno de sus detalles'
		FROM @TableBills tb
		JOIN @TableDetails td ON tb.HeadId = td.HeadId
		WHERE tb.StatusField = 1 AND td.StatusField = 0

		/************************************* INSERTAR ERRORES *************************************/		
		
		INSERT INTO @TableResult 
			(
				StatusField, MessageField, HeadId, DetailId
			)
			SELECT StatusField, CONCAT('CABECERA: ', MessageField), HeadId, 0
			FROM @TableBills
			WHERE StatusField = 0

		INSERT INTO @TableResult 
			(
				StatusField, MessageField, HeadId, DetailId
			)
			SELECT StatusField, CONCAT('DETALLE: ', MessageField), HeadId, Id
			FROM @TableDetails
			WHERE StatusField = 0
		
		SELECT @billsValid = COUNT(tb.Id)
		FROM @TableBills tb
		WHERE tb.StatusField = 1

		IF @billsValid > 0
		BEGIN

			/************************************* CALCULAMOS EL VALOR DE LAS FACTURAS Y GENERAMOS CONSECUTIVO *************************************/

			UPDATE tb
				SET
					tb.Value = td.Value
			FROM @TableBills tb
			JOIN
			(
				SELECT td.HeadId, SUM(td.Value * IIF(td.Nature = 1, 1, -1)) Value
				FROM @TableDetails td
				WHERE td.StatusField = 1
				GROUP BY td.HeadId
			) td ON tb.HeadId = td.HeadId
			WHERE tb.StatusField = 1

			UPDATE tb
				SET tb.RowId = r.RowId - 1
			FROM @TableBills tb
			JOIN
			(
				SELECT tb.Id, ROW_NUMBER() OVER(ORDER BY tb.Id) RowId
				FROM @TableBills tb
				WHERE tb.StatusField = 1
			) r ON tb.Id = r.Id
			WHERE tb.StatusField = 1

			/************************************* INSERT LOAD MASSIVE HEADER DATA *************************************/

			--Si se esta insertando por primera vez se consulta la secuencia numerica
			IF @Id = 0
			BEGIN
				IF @Code = '' 
				BEGIN
					SELECT @IdForm = '2069',
						   @idSequenceDetail = NULL

					-- Consultamos la secuencia numerica del formulario
					SELECT @pattern = cs.Pattern, 
						@NextS = bsd.[Next], 
						@idSequenceDetail = bsd.Id  
					FROM Payments.PaymentsSecuenceDetail bsd 
					JOIN Payments.PaymentsSecuence bs ON bs.Id = bsd.IdSequensePaymentsC
					JOIN Common.Sequense cs on cs.Id = bsd.IdSequense
					WHERE bs.IdForm = @IdForm 
						AND 
						(
							(bs.Scope = 'O')
							OR
							(bs.Scope <> 'O' AND bsd.IdOperatingUnit = @OperatingUnitId)
						)

					SELECT @Code = dbo.GetSequence('', @pattern, @NextS)
					UPDATE Payments.PaymentsSecuenceDetail SET [Next] += 1 WHERE Id = @idSequenceDetail

					--Se inserta la cabecera
					INSERT INTO [Payments].[LoadMassive]
					(
						[OperatingUnitId],[Code],[DocumentDate],[Observations],[Status],[CreationUser],[CreationDate],[ModificationUser],[ModificationDate],[ConfirmationUser],[ConfirmationDate]
					)
					VALUES
					(
						@OperatingUnitId,@Code,@DocumentDate,@Observations,2,@CodeUser,[Common].[GETDATE](),@CodeUser,[Common].[GETDATE](),@CodeUser,[Common].[GETDATE]()
					)

					--Obtengo el id de la cabcera
					SET @Id = SCOPE_IDENTITY()
				END
			END
			ELSE --Si se esta actualizando
			BEGIN
				UPDATE [Payments].[LoadMassive]
					SET [Code] = @Code,
						[ModificationUser] = @CodeUser,
						[ModificationDate] = [Common].[GETDATE](),
						[ConfirmationUser] = @CodeUser,
						[ConfirmationDate] = [Common].[GETDATE]()
				WHERE Id = @Id
			END

			/************************************* DATOS DE CONSECUTIVO *************************************/

			SELECT @ConsecutiveFiling = cs.ConsecutiveFiling FROM GeneralLedger.CompanySettings cs
			UPDATE GeneralLedger.CompanySettings SET ConsecutiveFiling += @billsValid

			SELECT @IdForm = '730',
				   @idSequenceDetail = NULL

			-- Consultamos la secuencia numerica del formulario
			SELECT @pattern = cs.Pattern, 
				@NextS = bsd.[Next], 
				@idSequenceDetail = bsd.Id  
			FROM Payments.PaymentsSecuenceDetail bsd 
			JOIN Payments.PaymentsSecuence bs ON bs.Id = bsd.IdSequensePaymentsC
			JOIN Common.Sequense cs on cs.Id = bsd.IdSequense
			WHERE bs.IdForm = @IdForm 
				AND 
				(
					(bs.Scope = 'O')
					OR
					(bs.Scope <> 'O' AND bsd.IdOperatingUnit = @OperatingUnitId)
				)

			UPDATE Payments.PaymentsSecuenceDetail SET [Next] += @billsValid WHERE Id = @idSequenceDetail

			/************************************* INSERTAMOS LAS CUENTAS POR PAGAR *************************************/

			INSERT INTO [Payments].[AccountPayable]
	        (
				[Code],[NumberFiling],[EntityId],[EntityCode],[EntityName],[IdSupplier],[IdThirdParty],[IdAccount],[IdCostCenter],[BillNumber],HandlesDocumentSupport,[BillDate],[DocumentDate],[ServicePeriodDate],[FilingUnitId],[SupplierTypeId],[Term],
				[ExpirationDate],[Coments],[Status],[InitialBalance],[IdInitialBalance],[PreviousBudget],[Shares],[InvoiceValue],[Value],[Balance],[IdOperatingUnit],[IdSuppliersDistributionLines],[CreationUser],[CreationDate]
			)
			SELECT 
				dbo.GetSequence('', @pattern, (@NextS + tb.RowId)) Code,
				(@ConsecutiveFiling + tb.RowId) NumberFiling,
				@Id EntityId,				
				@Code EntityCode,
				'LoadMassive'  EntityName,
				tb.SupplierId,
				tb.ThirdpartyId,
				tb.MainAccountId,
				tb.CostCenterId,
				tb.BillNumber,
				tb.HandlesDocumentSupport,
				tb.BillDate,
				tb.DocumentDate,
				tb.DocumentDate ServicePeriodDate,
				tb.FilingUnitId,
				tb.SupplierTypeId,
				tb.Term,
				DATEADD(DAY, tb.Term, tb.BillDate) ExpirationDate,
				tb.Coments,
				1 Status,
				0 InitialBalance,
				NULL IdInitialBalance,
				0 PreviousBudget,
				1 Shares,
				tb.InvoiceValue,
				tb.Value,
				tb.Value,
				@OperatingUnitId,
				tb.SuppliersDistributionLineId,
				@CodeUser,
				[Common].[GETDATE]()
			FROM @TableBills tb
			WHERE tb.StatusField = 1
			ORDER BY tb.RowId

			UPDATE tb
				SET tb.AccountPayableId = ap.Id,
					tb.AccountPayableCode = ap.Code
			FROM @TableBills tb
			JOIN Payments.AccountPayable ap WITH (NOLOCK) ON tb.SupplierId = ap.IdSupplier AND tb.BillNumber = ap.BillNumber
			WHERE tb.StatusField = 1

			/************************************* INSERTAMOS LOS DETALLES *************************************/

			INSERT INTO [Payments].[AccountPayableDetailConcept]
			(
				[IdAccountPayable],[IdConceptAccountPayable],[IdAccount],[IdThirdParty],[IdCostCenter],[Nature],[BaseValue],[BillingValue],[Value],[IdRetentionConcept],[Percentage],[Detail],[DeferredCausation],[IsDirectCost]
			)
			SELECT
				tb.AccountPayableId, td.AccountPayableConceptId, td.MainAccountId, td.ThirdPartyId, td.CostCenterId, td.Nature, td.BaseValue, tb.Value, td.Value, td.RetentionConceptId, td.Percentage, td.Detail, 0, 0
			FROM @TableBills tb
			JOIN @TableDetails td ON tb.HeadId = td.HeadId
			WHERE tb.StatusField = 1
			ORDER BY tb.RowId, td.Id

			/************************************* INSERTAMOS LAS CUOTAS *************************************/

			INSERT INTO [Payments].[AccountPayableShares]
			(
				[IdAccountPayable],[Share],[DateExpires],[InitialValue],[DebitValue],[CreditValue],[ValueTransfers],[PaymentValue],[CrossingValue],[Balance]
			)
			SELECT
				tb.AccountPayableId, 1 Share, DATEADD(DAY, tb.Term, tb.BillDate) DateExpires, tb.Value, 0 DebitValue, 0 CreditValue, 0 ValueTransfers, 0 PaymentValue, 0 CrossingValue, tb.Value
			FROM @TableBills tb
			WHERE tb.StatusField = 1
			ORDER BY tb.RowId

			/************************************* TABLAS DE CONTROL ***/

			INSERT INTO [Payments].[PaymentsControl]
			(
				DocumentNumber, DocumentType, DocumentUser, DocumentDate
			)
			SELECT
				tb.AccountPayableCode, 1, @CodeUser, tb.DocumentDate
			FROM @TableBills tb
			WHERE tb.StatusField = 1
			ORDER BY tb.RowId

			/************************************* INSERT LOAD MASSIVE DETAIL DATA *************************************/

			INSERT INTO [Payments].[LoadMassiveAccountPayable]
			(
				[LoadMassiveId],[SupplierId],[DistributionLineId],[CostCenterId],[BillNumber],[DocumentDate],[BillDate],[FilingUnitId],[SupplierTypeId],[Term],[InvoiceValue],[Coments],[AccountPayableId]
			)
			SELECT @Id, tb.SupplierId, tb.DistributionLineId, tb.CostCenterId, tb.BillNumber, tb.DocumentDate, tb.BillDate, tb.FilingUnitId, tb.SupplierTypeId, tb.Term, tb.InvoiceValue, tb.Coments, tb.AccountPayableId
			FROM @TableBills tb
			WHERE tb.StatusField = 1
			ORDER BY tb.RowId

			UPDATE tb
				SET tb.LoadMassiveAccountPayableId = lmap.Id
			FROM @TableBills tb
			JOIN Payments.LoadMassiveAccountPayable lmap WITH (NOLOCK) ON @Id = lmap.LoadMassiveId AND tb.SupplierId = lmap.SupplierId AND tb.BillNumber = lmap.BillNumber
			WHERE tb.StatusField = 1

			INSERT INTO [Payments].[LoadMassiveAccountPayableDetail]
			(
				[LoadMassiveAccountPayableId],[AccountPayableConceptId],[ThirdPartyId],[CostCenterId],[Detail],[Nature],[Value],[RetentionConceptId],[BaseValue]
			)
			SELECT
				tb.LoadMassiveAccountPayableId, td.AccountPayableConceptId, td.ThirdPartyId, td.CostCenterId, td.Detail, td.Nature, tb.Value, td.RetentionConceptId, td.BaseValue
			FROM @TableBills tb
			JOIN @TableDetails td ON tb.HeadId = td.HeadId
			WHERE tb.StatusField = 1
			ORDER BY tb.RowId, td.Id

			/************************************* INSERT RESULTADOS *************************************/

			INSERT INTO @TableResult
			(
				StatusField,  MessageField, 
				-- LoadMassive ---
				[LoadMassiveId], [LoadMassiveCode],
				-- AccountPayable ---
				[AccountPayableId], [AccountPayableCode],
				[HeadId],
				[SupplierNit], [SupplierId],
				[DistributionLineCode], [DistributionLineId],
				[SuppliersDistributionLineId], [AccountPayableMainAccountNumber],
				[AccountPayableMainAccountId], [AccountPayableCostCenterCode],
				[AccountPayableCostCenterId],
				[BillNumber],
				[DocumentDate],
				[BillDate],
				[FilingUnitCode], [FilingUnitId],
				[SupplierTypeCode], [SupplierTypeId],
				[Term],				
				[InvoiceValue],
				[AccountPayableValue],
				[Coments],				
				-- AccountPayableDetailConcept --
				[DetailId],
				[AccountPayableConceptCode], [AccountPayableConceptId],
				[MainAccountNumber], [MainAccountId],
				[ThirdPartyNit], [ThirdPartyId],
				[CostCenterCode], [CostCenterId],
				[Detail],
				[Nature],
				[Value],
				[RetentionConceptCode], [RetentionConceptId],
				[BaseValue],
				[Percentage],
				[TypeRounding]
			)
			SELECT 
				tb.StatusField, tb.MessageField,
				-- LoadMassive ---
				@Id LoadMassiveId, @Code LoadMassiveCode,
				-- AccountPayable ---
				tb.AccountPayableId, tb.AccountPayableCode,
				tb.HeadId,
				tb.SupplierNit, tb.SupplierId,
				tb.DistributionLineCode, tb.DistributionLineId,
				tb.SuppliersDistributionLineId,
				tb.MainAccountNumber, tb.MainAccountId,
				tb.CostCenterCode, tb.CostCenterId,
				tb.BillNumber,
				tb.DocumentDate,
				tb.BillDate,
				tb.FilingUnitCode, tb.FilingUnitId,
				tb.SupplierTypeCode, tb.SupplierTypeId,
				tb.Term,
				tb.InvoiceValue,
				tb.Value,
				tb.Coments,
				-- AccountPayableDetailConcept --
				td.Id,
				td.AccountPayableConceptCode, td.AccountPayableConceptId,
				td.MainAccountNumber, td.MainAccountId,
				td.ThirdPartyNit, td.ThirdPartyId,
				td.CostCenterCode, td.CostCenterId,
				td.Detail,
				td.Nature,
				td.Value,
				td.RetentionConceptCode, td.RetentionConceptId,
				td.BaseValue,
				ISNULL(td.Percentage, 0),
				td.TypeRounding
			FROM @TableBills tb
			LEFT JOIN @TableDetails td ON tb.HeadId = td.HeadId
			WHERE tb.StatusField = 1
		END

	END TRY
	BEGIN CATCH
		INSERT INTO @TableResult (StatusField, MessageField)
		VALUES (0, ERROR_MESSAGE() + ' Linea: ' + cast(ERROR_LINE() as VARCHAR(5)))
	END CATCH

	--Se retorna la tabla con los resultados
	SELECT * FROM @TableResult ORDER BY HeadId, DetailId

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que permite registrar masivamente facturas de proveedores (cuentas por pagar) y sus líneas de detalle contable en el módulo de pagos. Recibe tres parámetros XML: uno con los datos generales del lote de carga masiva, otro con las facturas (proveedor, número de factura, fechas, valor, centro de costo, línea de distribución, plazo) y otro con los conceptos de detalle de cada factura (concepto contable, tercero, naturaleza débito/crédito, valor, retenciones). Valida la información de cada factura y su detalle, resuelve los identificadores internos a partir de los códigos enviados, y consolida los resultados del procesamiento en una tabla de respuesta que indica el estado de éxito o error por cada registro. Se utiliza en procesos de carga masiva de facturas de proveedores, eliminando la digitación manual una a una en el módulo de cuentas por pagar.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'PROCEDURE', @level1name = N'SP_SaveLoadMassive';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'PROCEDURE', @level1name = N'SP_SaveLoadMassive';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Carga masiva de facturas (cuentas por pagar) desde XML, validando cabeceras y detalles, y persistiendo cabecera de cargue, cuentas por pagar, sus detalles, cuotas y registros de control cuando todas las validaciones pasan.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_SaveLoadMassive';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los XML de cabecera, facturas y detalles deben respetar la estructura /Data y /Data/Row con los nodos esperados.; Debe existir configuración de secuencias en Payments.PaymentsSecuence/PaymentsSecuenceDetail para los formularios 2069 (LoadMassive) y 730 (AccountPayable).; Debe existir un registro en GeneralLedger.CompanySettings con ConsecutiveFiling.; La fecha del documento debe corresponder a un período abierto (GeneralLedger.ClosedMonth con Status=1).; Las fechas de factura y documento no deben superar la fecha actual obtenida con Common.GETDATE().', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_SaveLoadMassive';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] Payments.LoadMassive: Cuando @Id=0 y @Code='''' y existen facturas válidas (@billsValid>0), se inserta la cabecera con Status=2 y código generado por dbo.GetSequence usando el patrón del formulario 2069.; [UPDATE] Payments.LoadMassive: Cuando @Id<>0 y existen facturas válidas, se actualiza Code, ModificationUser/Date y ConfirmationUser/Date del registro existente.; [UPDATE] Payments.PaymentsSecuenceDetail: Al generar el código de LoadMassive (formulario 2069) se incrementa Next en 1; para AccountPayable (formulario 730) se incrementa Next en la cantidad de facturas válidas.; [UPDATE] GeneralLedger.CompanySettings: Se incrementa ConsecutiveFiling en la cantidad de facturas válidas para reservar números de radicación consecutivos.; [INSERT] Payments.AccountPayable: Por cada factura con StatusField=1 se inserta una cuenta por pagar con Status=1, Shares=1, InitialBalance=0, NumberFiling=@ConsecutiveFiling+RowId, ExpirationDate=BillDate+Term y Balance=Value.; [INSERT] Payments.AccountPayableDetailConcept: Por cada detalle válido asociado a una factura válida se inserta el concepto con DeferredCausation=0, IsDirectCost=0 y BillingValue=Value de la factura.; [INSERT] Payments.AccountPayableShares: Por cada factura válida se inserta una única cuota (Share=1) con DateExpires=BillDate+Term, InitialValue y Balance iguales al valor de la factura.; [INSERT] Payments.PaymentsControl: Por cada factura válida se registra un control con DocumentType=1 y el código de la cuenta por pagar generada.; [INSERT] Payments.LoadMassiveAccountPayable: Por cada factura válida se inserta el detalle de cargue masivo asociándolo a la cabecera (@Id) y a la AccountPayable creada.; [INSERT] Payments.LoadMassiveAccountPayableDetail: Por cada detalle válido se inserta el registro auxiliar de cargue masivo enlazado al LoadMassiveAccountPayable correspondiente.; [RETURN_RESULT] @TableResult: Se retornan los resultados con errores de cabecera (prefijo ''CABECERA:''), errores de detalle (prefijo ''DETALLE:''), filas exitosas y, en caso de excepción, el ERROR_MESSAGE con la línea.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_SaveLoadMassive';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @billsValid > 0 (existe al menos una factura válida tras todas las validaciones) → Se ejecuta el flujo de inserción: cabecera LoadMassive, AccountPayable, detalles, cuotas, control y registros de cargue. else Solo se devuelven los errores acumulados en @TableResult sin insertar cuentas por pagar.; si @Id = 0 y @Code = '''' (cabecera nueva sin código) → Se consulta y consume la secuencia del formulario 2069, se genera el Code y se INSERTA Payments.LoadMassive. else Se UPDATEa Payments.LoadMassive existente actualizando Code, usuarios y fechas de modificación/confirmación.; si Para detalles: la cuenta principal asociada (ma.RetencionType <> 0) → Se valida y resuelve el RetentionConcept, su tasa (>0), tipo de redondeo, BaseValue>=0 y que Value coincida con ROUND(BaseValue*Percentage/100, redondeo según TypeRounding).; si ma.HandlesCostCenter = 1 en la línea de distribución o concepto → Se exige y valida un Centro de Costo válido para la cabecera/detalle.; si Existe una factura previa en Payments.AccountPayable con mismo BillNumber e IdSupplier y Status<>3 → Se marca el registro como inválido (factura ya existe con el proveedor).; si El detalle de una factura tiene StatusField=0 → Se invalida también la cabecera correspondiente con mensaje de error en sus detalles.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_SaveLoadMassive';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_SaveLoadMassive';
-- GO
