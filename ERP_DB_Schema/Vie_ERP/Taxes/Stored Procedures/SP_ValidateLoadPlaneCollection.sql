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