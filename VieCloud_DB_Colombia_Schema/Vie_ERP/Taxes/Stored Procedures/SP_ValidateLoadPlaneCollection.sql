-- =============================================
-- Author:		Carlos Cordoba
-- Create date: 24-08-2016
-- Description:	valida el archivo plano de avaluo
-- =============================================
CREATE PROCEDURE [Taxes].[SP_ValidateLoadPlaneCollection] 
	@Data as Xml
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	declare @Records table(Id int IDENTITY PRIMARY KEY not null,record varchar(200) INDEX IX1 CLUSTERED)
	--tabla para ir almacenando los resultados
    --declare @Result as table([State] integer, [Message] varchar(max),Record varchar(300))
	

	BEGIN TRY

		--valido que este creada la configuracion de modulo
		if(select COUNT(*) from Taxes.SettingsTaxes )=0 begin
			SELECT 0 as State ,'No se encontraron parametros para el modulo' Message,'' as Record
			return
		end
		--elimino los detalles de la tabla de resultados
		delete Taxes.ResultLoadPlaneCollection
		

		INSERT into @Records
		SELECT XTags.value('.', 'varchar(200)') AS record
		FROM @Data.nodes('/Data/Record') as XTbl(XTags);	

		--inserto los mensajes de error
		insert into Taxes.ResultLoadPlaneCollection
		select 2,'Estructura Incorrecta',record  from @Records where DATALENGTH(record) <> 149
		
		declare @record as varchar(149)
		DECLARE record_cursor CURSOR

		FOR 
			select record  from @Records where DATALENGTH(record) = 149

		open record_cursor
		FETCH NEXT FROM record_cursor INTO @record
		WHILE @@FETCH_STATUS = 0 BEGIN		

			--valido la direccion
			if(SUBSTRING(@record,75,34) ='                                  ' or SUBSTRING(@record,75,34) ='0000000000000000000000000000000000') begin
				insert into Taxes.ResultLoadPlaneCollection values(3,'Dirección Incorrecta',@record)	
				FETCH NEXT FROM record_cursor INTO @record	
				continue		
			end

			if(ISNUMERIC(SUBSTRING(@record,129,12))=0 or SUBSTRING(@record,129,12) = '            ' or SUBSTRING(@record,129,12) = '000000000000') begin
				insert into Taxes.ResultLoadPlaneCollection values(3,'Avaluo Incorrecto',@record)	
				FETCH NEXT FROM record_cursor INTO @record		
				continue
			end

			declare @Nit varchar(max) = SUBSTRING(@record,63,12)
			 
			--valido si se permite crear el tercero 
			if(select AllowCreateThirdParty  from Taxes.SettingsTaxes ) = 0 begin
				IF @Nit = '000000000000' or @Nit='            ' begin
					insert into Taxes.ResultLoadPlaneCollection values(3,'Tercero vacio ',@record)
					FETCH NEXT FROM record_cursor INTO @record		
					continue
				end
			end
			

			insert into Taxes.ResultLoadPlaneCollection values(1,'Registro Correcto',@record)

		FETCH NEXT FROM record_cursor INTO @record
		END
		CLOSE record_cursor;
		DEALLOCATE record_cursor;

		
		

		select * from Taxes.ResultLoadPlaneCollection 
		delete Taxes.ResultLoadPlaneCollection
		return 
	END TRY
    BEGIN CATCH		
        SELECT 0 as State ,ERROR_MESSAGE()+', Linea: '+CAST(ERROR_LINE() AS VARCHAR(20)) Message,'' as Record
    END CATCH;

	
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valida el contenido de un archivo plano de avalúos cargado como XML para el plan de cobro (planeación de cartera o recaudo de impuestos). Recorre cada registro del archivo y verifica que tenga la longitud correcta (149 caracteres), que la dirección no esté vacía ni en ceros, que el avalúo sea un valor numérico válido, y que el NIT o identificación del tercero exista cuando la configuración del módulo no permite crear terceros automáticamente. Los resultados de cada validación (correcto, estructura incorrecta, dirección incorrecta, avalúo incorrecto, tercero vacío) se almacenan temporalmente en la tabla de resultados de carga del plan de cobro (ResultLoadPlaneCollection) y se devuelven al llamador antes de ser eliminados, permitiendo al usuario conocer qué registros del archivo plano de avalúos son válidos y cuáles tienen errores antes de confirmar la carga definitiva.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'PROCEDURE', @level1name = N'SP_ValidateLoadPlaneCollection';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'PROCEDURE', @level1name = N'SP_ValidateLoadPlaneCollection';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Valida la estructura y contenido de cada registro de un archivo plano de avalúo recibido en XML, registrando el resultado (correcto o tipo de error) por registro.', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateLoadPlaneCollection';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos un registro de configuración en Taxes.SettingsTaxes; si no, retorna estado 0 con mensaje de parámetros no encontrados sin procesar nada.; El XML de entrada debe tener nodos /Data/Record con el contenido de cada línea del archivo plano.', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateLoadPlaneCollection';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se aplican las validaciones de dirección, avalúo y tercero a registros con longitud exacta de 149 caracteres.; Cada registro genera exactamente una fila de resultado, con el primer error encontrado en el orden: estructura → dirección → avalúo → tercero.; Si AllowCreateThirdParty=1 (o distinto de 0), nunca se valida el contenido del NIT.; La tabla Taxes.ResultLoadPlaneCollection queda vacía tras la ejecución exitosa.; Estados utilizados: 0=error general/configuración, 1=correcto, 2=estructura incorrecta, 3=error de contenido del registro.', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateLoadPlaneCollection';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'avalúo; archivo plano; tercero; NIT; dirección; configuración de módulo de impuestos', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateLoadPlaneCollection';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[DELETE] Taxes.ResultLoadPlaneCollection: Al inicio del proceso (tras validar configuración) se borra el contenido previo de la tabla de resultados.; [INSERT] Taxes.ResultLoadPlaneCollection: Cuando DATALENGTH(record) <> 149, inserta estado 2 con mensaje ''Estructura Incorrecta''.; [INSERT] Taxes.ResultLoadPlaneCollection: Cuando los 34 caracteres desde la posición 75 son espacios o ceros, inserta estado 3 con mensaje ''Dirección Incorrecta''.; [INSERT] Taxes.ResultLoadPlaneCollection: Cuando los 12 caracteres desde la posición 129 no son numéricos, son espacios o son ceros, inserta estado 3 con mensaje ''Avaluo Incorrecto''.; [INSERT] Taxes.ResultLoadPlaneCollection: Cuando AllowCreateThirdParty=0 en SettingsTaxes y los 12 caracteres del NIT (posición 63) son ceros o espacios, inserta estado 3 con mensaje ''Tercero vacio''.; [INSERT] Taxes.ResultLoadPlaneCollection: Cuando el registro pasa todas las validaciones, inserta estado 1 con mensaje ''Registro Correcto''.; [RETURN_RESULT] Taxes.ResultLoadPlaneCollection: Devuelve el contenido completo de la tabla de resultados al final del proceso.; [DELETE] Taxes.ResultLoadPlaneCollection: Tras devolver los resultados al cliente, vacía la tabla de resultados.; [RETURN_RESULT] Taxes.ResultLoadPlaneCollection: Si ocurre una excepción, devuelve un único registro con estado 0, el mensaje de error y la línea del error.', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateLoadPlaneCollection';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si No existe configuración en Taxes.SettingsTaxes → Retorna estado 0 con mensaje de parámetros no encontrados y termina sin procesar.; si DATALENGTH(record) <> 149 → Marca el registro como ''Estructura Incorrecta'' (estado 2) y no lo procesa en el cursor.; si Dirección (subcadena pos 75 long 34) vacía o en ceros → Marca ''Dirección Incorrecta'' y salta al siguiente registro. else Continúa con la validación de avalúo.; si Avalúo (subcadena pos 129 long 12) no numérico, vacío o en ceros → Marca ''Avaluo Incorrecto'' y salta al siguiente registro. else Continúa con la validación del tercero.; si AllowCreateThirdParty=0 y NIT (pos 63 long 12) vacío o ceros → Marca ''Tercero vacio'' y salta al siguiente registro. else Marca ''Registro Correcto''.', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateLoadPlaneCollection';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Taxes.SettingsTaxes; Taxes.ResultLoadPlaneCollection', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateLoadPlaneCollection';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateLoadPlaneCollection';
-- GO
