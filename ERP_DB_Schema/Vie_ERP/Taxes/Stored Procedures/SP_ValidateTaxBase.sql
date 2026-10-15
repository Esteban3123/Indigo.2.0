
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