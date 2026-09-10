

-- =====================================================
-- Author:		Johan Sebastian Cuellar Esquivel
-- Create date: 2021-04-07
-- Description:	Procedimiento que se encarga de copiar y pegar o la importacion de archivo para los detalles de banco
-- =====================================================
CREATE PROCEDURE [Payroll].[SP_SetBankDetail]
    @BankDetailXml AS XML	
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE @BankConciliationConcepts TABLE
	(
		Code varchar(20),		
		ExtractCode varchar(60),
		Detail varchar(60)
	)

	--Tabla temporal de los detalles para el return
	DECLARE @Detail TABLE
	(
		Id Int,
		BankId INT,
		BankConciliationConceptsId INT,
		Code varchar(20),
		Name varchar(100),
		ExtractCode varchar(60),
		Detail varchar(60)
	
	)

	BEGIN TRY
		--Se obtiene los detalles que vienen en el xml
			INSERT INTO @BankConciliationConcepts			
				SELECT	t.x.value('Code[1]','varchar(20)'),
						t.x.value('ExtractCode[1]','varchar(60)'),
						t.x.value('Detail[1]','varchar(60)')
				FROM @BankDetailXml.nodes('/BankDetail') t(x)

			----------------------------------------------------------------------------------------------------------------------------------------------------------------
						
			-- se inserta en la tabla temporal las relaciones y los detalles 
			INSERT INTO @Detail
			(
				[BankConciliationConceptsId],[Code],[Name],[ExtractCode],[Detail]
			)
			SELECT 
				bcc.Id, bcc.Code, bcc.Name, bc.ExtractCode, bc.Detail
			FROM Treasury.BankConciliationConcepts bcc WITH (NOLOCK) 
			JOIN @BankConciliationConcepts bc ON bc.Code = bcc.Code
			
			----------------------------------------------------------------------------------------------------------------------------------------------------------------

			IF (SELECT COUNT(BankConciliationConceptsId) FROM @Detail) = 0
			BEGIN
				SELECT 'Error, no se encuentra código del concepto de conciliación bancaria'
				RETURN
			END
						
			-- Retornar los datos
			SELECT * FROM @Detail		
			RETURN		
	END TRY
	BEGIN CATCH
		  SELECT '0' ,0 , 0, 2, ERROR_MESSAGE()+', Linea: '+CAST(ERROR_LINE() AS VARCHAR(20)) Message
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que procesa la importación o copia de detalles de banco para la conciliación bancaria en el módulo de nómina y tesorería. Recibe un XML con los detalles del banco (código, código de extracto y detalle), los cruza contra los conceptos de conciliación bancaria registrados en la tabla Treasury.BankConciliationConcepts, y retorna la información enriquecida con el identificador, nombre y datos del concepto correspondiente. Si no se encuentra ningún concepto de conciliación que coincida con los códigos enviados, devuelve un mensaje de error indicando que el código no existe.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_SetBankDetail';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_SetBankDetail';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Procesa un XML de detalles bancarios (carga manual o importación), los empareja con conceptos de conciliación bancaria por código y devuelve la relación resuelta o un error si ningún código coincide.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SetBankDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML debe seguir la estructura /BankDetail con nodos Code, ExtractCode y Detail.; Los códigos provistos en el XML deben existir en Treasury.BankConciliationConcepts para poder ser emparejados.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SetBankDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se devuelven detalles cuyo Code exista en Treasury.BankConciliationConcepts (JOIN INNER por Code).; El procedimiento nunca persiste cambios: solo lee Treasury.BankConciliationConcepts y opera sobre tablas temporales en memoria.; Los errores en tiempo de ejecución se transforman en un resultset con estructura fija en lugar de propagarse.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SetBankDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Conciliación bancaria; Conceptos de conciliación bancaria; Detalle bancario; Importación de archivo bancario; Código de extracto bancario', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SetBankDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULTSET: Cuando existen coincidencias entre los Code del XML y Treasury.BankConciliationConcepts, devuelve el conjunto de detalles resueltos (Id, Code, Name, ExtractCode, Detail).; [RETURN_RESULT] RESULTSET: Cuando COUNT(BankConciliationConceptsId)=0 en la tabla temporal de detalles, retorna un mensaje de error: ''Error, no se encuentra código del concepto de conciliación bancaria'' y termina la ejecución.; [RETURN_RESULT] RESULTSET: En caso de excepción capturada, devuelve una fila con valores por defecto y ERROR_MESSAGE()+'', Linea: ''+ERROR_LINE() como mensaje.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SetBankDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si (SELECT COUNT(BankConciliationConceptsId) FROM @Detail) = 0 → Devuelve mensaje de error indicando que no se encuentra código del concepto de conciliación bancaria y hace RETURN. else Devuelve el contenido completo de la tabla temporal @Detail con las relaciones resueltas.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SetBankDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Treasury.BankConciliationConcepts', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SetBankDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SetBankDetail';
-- GO
