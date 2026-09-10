
-- =============================================
-- Author:		Emanuel Olaya Penagos
-- Create date: 09/01/2020
-- =============================================
CREATE PROCEDURE [dbo].[SP_MPPS_InsertarRadiacionInfo]
	@FLUTIEMP int,
	@NUMEXPO int,
	@DISTAFUEDECT varchar(16),
	@DISTAFUEPAC varchar(16),
	@DOSISENTRA int,
	@DOSISENTRAMGY varchar(16),
	@AREAEXP int,
	@AREDOSPROD varchar(16),
	@COMDOSIS varchar(1024),
	@RISORDENAUTO varchar(30)
AS
BEGIN
SET NOCOUNT ON;
	--declare @NUMINGRES varchar(10)
	--declare @AUTOTABLE int

	--declare @len int
	--declare @index int

	--set @index = CHARINDEX('-', @RISORDENAUTO)
	--set @len = LEN(@RISORDENAUTO)

	--set @NUMINGRES = SUBSTRING(@RISORDENAUTO, 0, CHARINDEX('-', @RISORDENAUTO))
	--set @AUTOTABLE = SUBSTRING(@RISORDENAUTO, @index+1, @len-@index)

	BEGIN TRY
		INSERT INTO [dbo].[RISRAD]
			   ([FLUTIEMP]
			   ,[NUMEXPO]
			   ,[DISTAFUEDECT]
			   ,[DISTAFUEPAC]
			   ,[DOSISENTRA]
			   ,[DOSISENTRAMGY]
			   ,[AREAEXP]
			   ,[AREDOSPROD]
			   ,[COMDOSIS]
			   ,[RISORDENAUTO])
		 VALUES
			   (@FLUTIEMP
			   ,@NUMEXPO
			   ,@DISTAFUEDECT
			   ,@DISTAFUEPAC
			   ,@DOSISENTRA
			   ,@DOSISENTRAMGY
			   ,@AREAEXP
			   ,@AREDOSPROD
			   ,@COMDOSIS
			   ,@RISORDENAUTO)

		SELECT CAST(SCOPE_IDENTITY() AS INT) 'RISRADAUTO'
	END TRY
	BEGIN CATCH
		SELECT CAST(0 AS INT) 'RISRADAUTO'
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra la información de radiación asociada a un estudio o procedimiento de imágenes diagnósticas (RIS), insertando en la tabla RISRAD los parámetros técnicos de exposición utilizados durante el examen. Almacena datos como el tiempo de fluoroscopía, número de exposiciones, distancia de la fuente al detector y al paciente, dosis de entrada en mGy, área de exposición, producto dosis-área y comentarios sobre la dosis recibida, vinculados al identificador automático de la orden RIS. Retorna el ID autogenerado del registro insertado, o cero en caso de error, permitiendo al sistema de radiología (MPPS) confirmar que la información dosimétrica del paciente quedó guardada correctamente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_MPPS_InsertarRadiacionInfo';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_MPPS_InsertarRadiacionInfo';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Registra la información de dosis y parámetros de radiación asociada a una orden de autorización RIS, devolviendo el identificador generado o 0 si falla.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_MPPS_InsertarRadiacionInfo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La tabla destino debe tener columna IDENTITY para que SCOPE_IDENTITY() retorne el nuevo ID.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_MPPS_InsertarRadiacionInfo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Siempre devuelve un resultado con columna RISRADAUTO: el ID generado si la inserción es exitosa, o 0 si falla.; Los errores de la inserción se silencian (no se relanzan), retornando 0 como identificador.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_MPPS_InsertarRadiacionInfo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Radiación; Dosis de entrada; Exposición radiológica; Orden de autorización RIS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_MPPS_InsertarRadiacionInfo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] dbo.RISRAD: Inserta un registro con los datos de exposición/dosis de radiación vinculados a la orden de autorización (RISORDENAUTO).; [RETURN_RESULT] dbo.RISRAD: Tras INSERT exitoso retorna SCOPE_IDENTITY() como RISRADAUTO; si ocurre excepción en TRY/CATCH retorna 0 como RISRADAUTO.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_MPPS_InsertarRadiacionInfo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_MPPS_InsertarRadiacionInfo';
-- GO
