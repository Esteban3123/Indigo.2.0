
-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 16/09/2016
-- Description:	Valida el archivo plano del cargue base gravable DIAN
-- =============================================
CREATE PROCEDURE [Taxes].[SP_ValidateTaxBase] 
	@Data as Xml,
	@Year as int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	--Tabla temporal para los registros del xml
	declare @Records table(Id int IDENTITY PRIMARY KEY not null, record varchar(200) INDEX IX1 CLUSTERED)

	--Nit que viene del xml
	declare @Nit varchar(50)

	--Valor que viene del xml
	declare @Value varchar(20)

	BEGIN TRY

		--Elimino los datos de la tabla tempFile
		delete from Taxes.TempFile where [Year] = @Year
		
		INSERT into @Records
		SELECT XTags.value('.', 'varchar(200)') AS record
		FROM @Data.nodes('/Data/Record') as XTbl(XTags);	

		declare @record as varchar(200)
		DECLARE record_cursor CURSOR

		FOR select record  from @Records

		open record_cursor
		FETCH NEXT FROM record_cursor INTO @record
		WHILE @@FETCH_STATUS = 0 BEGIN		

			--Obtengo el nit del xml
			select @Nit = cast(Data as varchar(20)) from dbo.Split(@record, ';') where Id = 1

			--Obtengo el valor del xml
			select @Value = cast(Data as varchar(20)) from dbo.Split(@record, ';') where Id = 2

			--Se valida que el nit exista como tercero
			if (select COUNT(*) from Common.ThirdParty where Nit = @Nit) = 0
			Begin
				insert into Taxes.TempFile values(@Nit, @Value, @Year, 0, 'Nit no existe')
				FETCH NEXT FROM record_cursor INTO @record		
				continue
			End

			--Se valida si ya existe un registro con el mismo nit en el año seleccionado
			if (select COUNT(*) from Taxes.TempFile where [Year] = @Year and Nit = @Nit) > 0
			Begin
				insert into Taxes.TempFile values(@Nit, @Value, @Year, 0, 'Nit ya existe')
				FETCH NEXT FROM record_cursor INTO @record		
				continue
			End

			--Se valida que el valor sea numérico
			if ISNUMERIC(@Value) = 0
			Begin
				insert into Taxes.TempFile values(@Nit, @Value, @Year, 0, 'Valor no es numérico')
				FETCH NEXT FROM record_cursor INTO @record		
				continue
			End

			--Si pasa todas las validaciones
			insert into Taxes.TempFile values(@Nit, @Value, @Year, 1, 'Registro Correcto')

		FETCH NEXT FROM record_cursor INTO @record
		END
		CLOSE record_cursor;
		DEALLOCATE record_cursor;
				
		select * from Taxes.TempFile where [Year] = @Year 
		delete from Taxes.TempFile where [Year] = @Year and [Status] = 0
		return 
	END TRY
    BEGIN CATCH		
        SELECT 0 as Status, ERROR_MESSAGE()+', Linea: '+CAST(ERROR_LINE() AS VARCHAR(20)) Message, '' as Record
    END CATCH;

	
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valida el archivo plano de base gravable enviado por la DIAN para un año fiscal específico. Recibe un XML con registros de NIT y valores, y por cada registro verifica tres condiciones: que el NIT exista como tercero registrado en el sistema (Common.ThirdParty), que no haya un registro duplicado para ese NIT en el mismo año fiscal, y que el valor sea numérico. Los resultados de la validación (correctos o con error) se almacenan temporalmente en Taxes.TempFile con su estado y mensaje descriptivo; al finalizar retorna todos los registros del año procesado y elimina únicamente los que fallaron la validación, dejando solo los registros correctos para su posterior carga como base gravable tributaria.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'PROCEDURE', @level1name = N'SP_ValidateTaxBase';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'PROCEDURE', @level1name = N'SP_ValidateTaxBase';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Valida los registros de un archivo plano XML de base gravable DIAN para un año fiscal, marcando cada NIT como correcto o erróneo y conservando únicamente los válidos para su posterior carga.', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateTaxBase';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML debe tener estructura /Data/Record con campos separados por '';'' donde la posición 1 es NIT y la posición 2 es el valor; Debe existir la función dbo.Split para tokenizar los registros; Los terceros válidos deben estar previamente registrados en Common.ThirdParty', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateTaxBase';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Antes de procesar el lote se borran los registros previos del mismo año en TempFile; Cada registro del XML genera siempre una fila en TempFile (válido o inválido); Solo se permite un NIT por año en el cargue (no duplicados dentro del mismo lote); Al finalizar, en TempFile solo permanecen los registros correctos (Status=1) del año procesado; Los registros marcados como inválidos (Status=0) se eliminan tras retornar el resultado; Si ocurre una excepción se retorna un resultado con Status=0 y el mensaje de error con número de línea, sin propagar el error', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateTaxBase';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Base gravable DIAN; NIT; Tercero; Año fiscal; Validación de archivo plano tributario', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateTaxBase';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[DELETE] Taxes.TempFile: Al iniciar elimina todos los registros del año procesado para reiniciar el lote; [INSERT] Taxes.TempFile: Cuando el NIT no existe en Common.ThirdParty inserta el registro con Status=0 y mensaje ''Nit no existe''; [INSERT] Taxes.TempFile: Cuando ya existe un registro con el mismo NIT y año en TempFile inserta con Status=0 y mensaje ''Nit ya existe''; [INSERT] Taxes.TempFile: Cuando el valor no es numérico inserta con Status=0 y mensaje ''Valor no es numérico''; [INSERT] Taxes.TempFile: Cuando pasa todas las validaciones inserta con Status=1 y mensaje ''Registro Correcto''; [RETURN_RESULT] Taxes.TempFile: Devuelve todos los registros (válidos e inválidos) del año procesado antes de depurar los inválidos; [DELETE] Taxes.TempFile: Después de retornar el resultado elimina los registros del año procesado con Status=0, dejando solo los correctos; [RETURN_RESULT] Taxes.TempFile: En caso de excepción retorna una fila con Status=0, el mensaje de error concatenado con el número de línea y Record vacío', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateTaxBase';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si El NIT del registro no existe en Common.ThirdParty → Inserta en TempFile con Status=0 y mensaje ''Nit no existe'' y pasa al siguiente registro; si Ya existe un registro en TempFile con el mismo NIT y año → Inserta en TempFile con Status=0 y mensaje ''Nit ya existe'' y pasa al siguiente registro; si El valor no es numérico (ISNUMERIC=0) → Inserta en TempFile con Status=0 y mensaje ''Valor no es numérico'' y pasa al siguiente registro; si Pasa todas las validaciones anteriores → Inserta en TempFile con Status=1 y mensaje ''Registro Correcto''', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateTaxBase';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Split', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateTaxBase';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Taxes.TempFile; Common.ThirdParty; dbo.Split', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateTaxBase';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateTaxBase';
-- GO
