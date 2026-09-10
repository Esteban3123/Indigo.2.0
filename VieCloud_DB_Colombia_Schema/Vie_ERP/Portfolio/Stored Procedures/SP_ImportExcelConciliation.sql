-- =============================================
-- Author:		Giovanny Plazas Lozano
-- Create date: 2021-01-25
-- Description:	Procedimiento que se encarga de importar los detalles  de la conciliacion
-- =============================================
CREATE PROCEDURE [Portfolio].[SP_ImportExcelConciliation] 
	@XmlObject AS XML
AS
BEGIN
	SET NOCOUNT ON

	--/************************************* VARIABLES ************************************/
	
	--Parametros
	DECLARE 
			@StateConciliation BIT = 1,
			@ClosingDate  DATE

	--Tabla para almacenar los items del listado que viene en el xml y los resultados a devolver
	DECLARE @TableXmlObject TABLE
	(
		Id INT IDENTITY(1,1),
		--------------------------------
		ThirdPartyNit						VARCHAR(MAX),
		InvoiceNumber						VARCHAR(50),
		DocumentValue						DECIMAL (18,2) DEFAULT(0),
		ClosingDate							Date,
		ValueGlosado						DECIMAL (18,2) DEFAULT(0),
		Balance								DECIMAL (18,2),
		PortfolioStatusConciliation			TINYINT,
		BalanceConciliation					DECIMAL (18,2),
		PortfolioDifferenceConciliation		DECIMAL (18,2) DEFAULT(0),
		ValueGlosadoConciliation			DECIMAL (18,2) DEFAULT(0),
		ConciliationConceptCode				VARCHAR(20),
		ObservationConciliation				VARCHAR(MAX),
				--------------------------------
		StatusField INT DEFAULT(0), 
		MessageField VARCHAR(MAX)
	)
	---SE EXTRAE LA FECHA DE CORTE PARA SER USADA EN LA FUNCION
	SELECT	@ClosingDate = t.x.value('Fechacorte[1]','DATE')
			FROM @XmlObject.nodes('/NewDataSet/Datos') t(x)

	BEGIN TRY
		INSERT INTO @TableXmlObject
			(
				ThirdPartyNit, 
				InvoiceNumber,
				DocumentValue ,
				ClosingDate,
				ValueGlosado,
				Balance, 
				PortfolioStatusConciliation,
				ValueGlosadoConciliation,
				BalanceConciliation,
				ConciliationConceptCode,
				ObservationConciliation
			)
			SELECT	t.x.value('Numerodocumento[1]','VARCHAR(MAX)')	as ThirdPartyNit,
					t.x.value('Numerofactura[1]','varchar(50)')		as InvoiceNumber,
					t.x.value('Valorinicial[1]','DECIMAL (18,2)')	as DocumentValue,
					t.x.value('Fechacorte[1]','DATE')				as ClosingDate,
					t.x.value('Valorglosado[1]','DECIMAL (18,2)')		as ValueGlosado,
					t.x.value('Saldo[1]','DECIMAL (18,2)')				as Balance,
					t.x.value('Estadocarteraentidad[1]','TINYINT')			as PortfolioStatusConciliation,
					t.x.value('Saldoglosa[1]','DECIMAL (18,2)')				as ValueGlosadoConciliation,
					t.x.value('Saldocartera[1]','DECIMAL (18,2)')			as BalanceConciliation,
					t.x.value('Conceptoconciliación[1]','VARCHAR(20)')		as ConciliationConceptCode,
					t.x.value('observación[1]','VARCHAR(MAX)')				as ObservationConciliation

			FROM @XmlObject.nodes('/NewDataSet/Datos') t(x)
	/**************************************************************************************************/

		--Se valida que existan detalles a procesar (El nombre de la hoja del archivo debe ser Datos)
		IF NOT EXISTS (SELECT 1 FROM @TableXmlObject) 
		BEGIN
			INSERT INTO @TableXmlObject(StatusField, MessageField)
				SELECT 999, 'No se encontraron detalles para procesar'
		END
	
			BEGIN
			UPDATE t
					SET t.PortfolioDifferenceConciliation = t.Balance - t.BalanceConciliation
				FROM @TableXmlObject t
				
			
				
			--Se valida que Exista estado de cartera puesto por el usuario exista
			UPDATE t
				SET t.StatusField = 999,
					t.MessageField = CONCAT('No existe el Estado de Cartera ', t.PortfolioStatusConciliation, ' relacionado a la factura ', t.InvoiceNumber)
			FROM @TableXmlObject t
			LEFT JOIN Portfolio.AccountReceivable gid WITH (NOLOCK) ON t.InvoiceNumber = gid.InvoiceNumber
			WHERE t.StatusField = 0 AND t.PortfolioStatusConciliation NOT IN (1,2,3,7,8,14,15,16) 

			--Se valida que Exista el codigo del concepto de conciliacion
			UPDATE t
				SET t.StatusField = 999,
					t.MessageField = CONCAT(IIF(t.ConciliationConceptCode  <> ISNULL(pcc.Code,'NULL'),' No existe',''),' El Codigo de concepto de conciliacion ', t.ConciliationConceptCode, ' relacionado a la factura ', t.InvoiceNumber, IIF(t.ConciliationConceptCode = pcc.Code,CONCAT(' esta en estado: ',IIF(pcc.Status = 1,'Activo','Inactivo')),''))
			FROM @TableXmlObject t
			LEFT JOIN Portfolio.PortfolioConciliationConcepts pcc WITH (NOLOCK) ON t.ConciliationConceptCode = pcc.Code
			WHERE t.StatusField = 0 AND   t.ConciliationConceptCode  <> ISNULL(pcc.Code,'NULL') OR pcc.Status = 0

						--Se valida que la factura no se encuentre dentro de la tabla detalle 
			UPDATE t
				SET t.StatusField = 999,
					t.MessageField = CONCAT('La factura : No.', t.InvoiceNumber, ' se encuentra ya cargada para este conciliacion : ',pc.Id)
			FROM @TableXmlObject t
			LEFT JOIN Portfolio.AccountReceivable gid WITH (NOLOCK) ON t.InvoiceNumber = gid.InvoiceNumber
			LEFT JOIN Portfolio.PortfolioConciliationDetail pcd WITH (NOLOCK) ON gid.InvoiceId = pcd.InvoiceId
			LEFT JOIN Portfolio.PortfolioConciliation pc WITH (NOLOCK) ON gid.ThirdPartyId = pc.ThirdPartyId
			WHERE t.StatusField = 0 AND gid.InvoiceId= pcd.InvoiceId  AND  pc.Id = pcd.PortfolioConciliationId AND pc.State = 1

			UPDATE t
				SET t.StatusField = 999,
					t.MessageField = CONCAT('La Fecha de corte : ', t.ClosingDate, ' No coincide, revise que la fecha de corte sea igual a la puesta en el formulario :',CAST(pcd.ClosingDate AS DATE))
			FROM @TableXmlObject t
			LEFT JOIN Portfolio.AccountReceivable gid WITH (NOLOCK) ON t.InvoiceNumber = gid.InvoiceNumber
			LEFT JOIN Portfolio.PortfolioConciliation pcd WITH (NOLOCK) ON gid.ThirdPartyId = pcd.ThirdPartyId
			WHERE t.StatusField = 0 AND  CAST (pcd.ClosingDate AS DATE) <>t.ClosingDate AND pcd.State = 1

			UPDATE t
				SET t.StatusField = 999,
					t.MessageField = CONCAT( ' El siguiente Nit: ',t.ThirdPartyNit,' cuenta mas de una  Conciliacion en estado Registrado; confirme Las anteriores y ejecute de nuevo el cargue')
					FROM @TableXmlObject t
					LEFT JOIN Common.ThirdParty th WITH (NOLOCK) ON t.ThirdPartyNit = th.Nit
					LEFT JOIN Portfolio.PortfolioConciliation pc WITH (NOLOCK) ON th.Id = pc.ThirdPartyId
					WHERE  t.StatusField = 0 AND pc.ThirdPartyId IN (SELECT p.ThirdPartyId
																	FROM Portfolio.PortfolioConciliation p WITH (NOLOCK)
																	WHERE p.State = 1
																	GROUP BY p.ThirdPartyId
																	HAVING COUNT(*)>1) 
					
			END	

			/***********************************************************************************************************/
			IF NOT EXISTS (SELECT 1 FROM @TableXmlObject WHERE StatusField <> 0) 
			BEGIN 

			INSERT INTO Portfolio.PortfolioConciliationDetail  
			(		--1
					PortfolioConciliationId,
					--2
					InvoiceId,
					--3
					AccountReceivableDate,
					--4
					RadicatedNumber,
					--5
					RadicatedDate,
					--6
					Balance,
					--7
					ValueGlosado,
					--8
					ValueEntity,
					--9
					PortfolioStatus,
					--10
					ValueAcceptedFirstInstance,
					--11
					ValueAcceptedSecondInstance,
					--12
					ValueGlosadoConciliation,
					--13
					BalanceConciliation,
					--14
					StateConciliation,
					--15
					StatePortfolioConciliation,
					--16
					Comment,
					--17
					PortfolioConciliationConceptsId,
					--18
					PortfolioDifferenceConciliation
			)

			SELECT	--1	
					pc.Id,
					--2
					i.Id,
					--3
					ar.AccountReceivableDate,
					--4
					ar.RadicatedConsecutive,
					--5
					ar.RadicatedDate,
					--6
					ar.Balance,
					--7
					ISNULL(gpg.ValueGlosado, 0) ValueGlosado,
					--8
					ar.DocumentValue,
					--9
					ar.PortfolioStatus,
					--10
					ISNULL(IIF(gpg.EvaluationDateGlosa <= @ClosingDate, gpg.ValueAcceptedFirstInstance, 0), 0) ValueAcceptedFirstInstance, 
					--11
					ISNULL(IIF(gpg.EvaluationDateReiteration <= @ClosingDate, gpg.ValueAcceptedSecondInstance, 0), 0) ValueAcceptedSecondInstance,
					--12
					t.ValueGlosadoConciliation,
					--13
					t.BalanceConciliation,
					--14
					@StateConciliation,
					--15
					t.PortfolioStatusConciliation,
					--16
					t.ObservationConciliation,
					--17
					pcc.Id,
					--18
					t.PortfolioDifferenceConciliation

			FROM @TableXmlObject t
			LEFT JOIN Common.ThirdParty th WITH(NOLOCK) ON t.ThirdPartyNit = th.Nit
			LEFT JOIN Portfolio.PortfolioConciliation pc WITH(NOLOCK) ON th.Id = pc.ThirdPartyId
			LEFT JOIN Billing.Invoice i  WITH(NOLOCK) ON t.InvoiceNumber = i.InvoiceNumber
			LEFT join Portfolio.PortfolioConciliationConcepts pcc WITH(NOLOCK) ON t.ConciliationConceptCode = pcc.Code
			LEFT JOIN Portfolio.GetAccountReceivableByAge(NULL, @ClosingDate) ar ON i.InvoiceNumber = ar.InvoiceNumber
			LEFT JOIN Glosas.GlosaPortfolioGlosada gpg WITH(NOLOCK) ON ar.InvoiceNumber = gpg.InvoiceNumber AND CAST(gpg.RadicatedDate AS DATE) <= @ClosingDate
			WHERE pc.State = 1 AND pc.ClosingDate = t.ClosingDate

			

		 --Actualizamos el valor glosado
		--BEGIN
			UPDATE t
				SET t.MessageField = 'Proceso Realizado Exitosamente'
			FROM @TableXmlObject t
		END
	END TRY
	BEGIN CATCH
		INSERT INTO @TableXmlObject(StatusField, MessageField)
			SELECT 999, CONCAT(ERROR_MESSAGE(), ' Linea: ', ERROR_LINE())
	END CATCH

	SELECT	StatusField, 
			MessageField
	FROM @TableXmlObject
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que importa desde un archivo Excel (enviado como XML) los detalles de una conciliación de cartera entre la IPS y un tercero o pagador. Lee cada fila del archivo con datos como NIT del tercero, número de factura, valor inicial, valor glosado, saldo según la entidad y saldo según el sistema, y los valida contra las tablas de cuentas por cobrar (AccountReceivable) y conceptos de conciliación (PortfolioConciliationConcepts) antes de registrarlos. Detecta errores como estados de cartera inválidos, códigos de concepto inexistentes o inactivos, facturas ya conciliadas, fechas de corte inconsistentes y terceros con más de una conciliación activa, devolviendo mensajes de error por fila. Se utiliza en el módulo de cartera para cargar masivamente el resultado de la conciliación que envía la entidad pagadora y cruzarlo con los saldos internos del sistema.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_ImportExcelConciliation';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_ImportExcelConciliation';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Importa desde un XML (originado en Excel) los detalles de una conciliación de cartera, valida cada fila contra catálogos y conciliaciones vigentes, y persiste el detalle en la conciliación correspondiente al tercero y fecha de corte.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ImportExcelConciliation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML debe contener nodos /NewDataSet/Datos con al menos un elemento; en caso contrario se reporta ''No se encontraron detalles para procesar''.; La fecha de corte (Fechacorte) se extrae del XML y debe coincidir con la ClosingDate de la PortfolioConciliation activa (State=1) del tercero.; Debe existir una Portfolio.PortfolioConciliation con State=1 para el tercero (por Nit) y con ClosingDate igual a la del XML para que la fila sea insertada en el detalle.; El tercero referenciado por ThirdPartyNit debe existir en Common.ThirdParty y la factura en Billing.Invoice (vía InvoiceNumber) para resolver los IDs del INSERT.; Cada tercero solo puede tener una conciliación en estado Registrado (State=1); si tiene más de una, se rechaza.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ImportExcelConciliation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'StateConciliation siempre se persiste con valor 1 al insertar el detalle.; La inserción al detalle es atómica respecto al lote: o pasan todas las validaciones o no se inserta nada.; Solo se concilian facturas cuya PortfolioConciliation del tercero esté activa (State=1) y con ClosingDate igual a la del XML.; Los valores aceptados en primera/segunda instancia provenientes de glosa solo se reconocen si su fecha de evaluación/reiteración es anterior o igual a la fecha de corte; de lo contrario se registran en 0.; Los estados de cartera permitidos en la conciliación se restringen al conjunto {1,2,3,7,8,14,15,16}.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ImportExcelConciliation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Conciliación de cartera; Estado de cartera; Concepto de conciliación; Factura; Glosa; Valor aceptado primera instancia; Valor aceptado segunda instancia; Fecha de corte; Tercero (NIT); Saldo de cartera', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ImportExcelConciliation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] Portfolio.PortfolioConciliationDetail: Solo si NO existen filas con StatusField<>0 en la tabla temporal (es decir, todas las validaciones pasaron) se insertan los detalles, uniendo XML con ThirdParty, PortfolioConciliation (State=1 y ClosingDate=t.ClosingDate), Invoice, PortfolioConciliationConcepts, GetAccountReceivableByAge(@ClosingDate) y GlosaPortfolioGlosada.; [INSERT] Portfolio.PortfolioConciliationDetail: ValueAcceptedFirstInstance se toma de gpg.ValueAcceptedFirstInstance solo si gpg.EvaluationDateGlosa <= @ClosingDate; en caso contrario 0. Igual lógica para ValueAcceptedSecondInstance con EvaluationDateReiteration.; [INSERT] Portfolio.PortfolioConciliationDetail: StateConciliation se inserta fijo en 1 (variable @StateConciliation) y StatePortfolioConciliation toma el PortfolioStatusConciliation provisto en el XML.; [RETURN_RESULT] @TableXmlObject: Devuelve por cada fila StatusField y MessageField; 999 indica error de validación, 0 con mensaje ''Proceso Realizado Exitosamente'' indica éxito.; [UPDATE] @TableXmlObject: Calcula PortfolioDifferenceConciliation = Balance - BalanceConciliation para cada fila importada.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ImportExcelConciliation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si NOT EXISTS filas en @TableXmlObject (XML vacío) → Inserta una fila con StatusField=999 y mensaje ''No se encontraron detalles para procesar''.; si PortfolioStatusConciliation NOT IN (1,2,3,7,8,14,15,16) → Marca la fila con StatusField=999 y mensaje ''No existe el Estado de Cartera ... relacionado a la factura ...''.; si ConciliationConceptCode no coincide con pcc.Code (no existe) o pcc.Status=0 (inactivo) → Marca la fila con StatusField=999 indicando que el código de concepto no existe o está Inactivo.; si La factura ya existe en PortfolioConciliationDetail asociada a una PortfolioConciliation con State=1 del mismo tercero → Marca StatusField=999 con mensaje ''La factura ... ya cargada para esta conciliacion: <Id>''.; si CAST(pcd.ClosingDate AS DATE) <> t.ClosingDate y pcd.State=1 → Marca StatusField=999 indicando que la fecha de corte no coincide con la del formulario.; si El tercero (ThirdPartyId) tiene más de una PortfolioConciliation con State=1 → Marca StatusField=999 pidiendo confirmar las conciliaciones anteriores antes de re-ejecutar.; si NOT EXISTS filas con StatusField<>0 (todas las validaciones OK) → Ejecuta el INSERT masivo en Portfolio.PortfolioConciliationDetail y actualiza MessageField a ''Proceso Realizado Exitosamente''. else No inserta nada y solo retorna los errores por fila.; si Excepción en TRY → Inserta una fila con StatusField=999 y MessageField = ERROR_MESSAGE() + línea.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ImportExcelConciliation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.AccountReceivable; Portfolio.PortfolioConciliationConcepts; Portfolio.PortfolioConciliationDetail; Portfolio.PortfolioConciliation; Common.ThirdParty; Billing.Invoice; Portfolio.GetAccountReceivableByAge; Glosas.GlosaPortfolioGlosada', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ImportExcelConciliation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ImportExcelConciliation';
-- GO
