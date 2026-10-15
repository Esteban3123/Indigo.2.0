
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