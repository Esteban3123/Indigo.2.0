
CREATE FUNCTION [dbo].[ExisteTablaEspecializada] (@Fecha as datetime)
RETURNS bit 
AS
BEGIN

	DECLARE @Mes varchar(20) = MONTH(@Fecha)
	DECLARE @nombreTablaANT varchar(20) 
	DECLARE @nombreTablaRES varchar(20)
	DECLARE @nombreTablaEXA varchar(20)
	Declare @Resultado bit

	IF LEN(@Mes) = 1 
		BEGIN
			SET @Mes = '0' + @Mes 
			SET @nombreTablaANT = CONCAT('ANTVALORES', YEAR(@Fecha) , @Mes);
			SET @nombreTablaRES = CONCAT('RSVALORES', YEAR(@Fecha) , @Mes);
			SET @nombreTablaEXA = CONCAT('EXAVALORES', YEAR(@Fecha) , @Mes);
		END
	ELSE 
		BEGIN
			SET @nombreTablaANT = CONCAT('ANTVALORES', YEAR(@Fecha) , MONTH(@Fecha));
			SET @nombreTablaRES = CONCAT('RSVALORES', YEAR(@Fecha) , MONTH(@Fecha));
			SET @nombreTablaEXA = CONCAT('EXAVALORES', YEAR(@Fecha) , MONTH(@Fecha));
		END

	IF EXISTS (SELECT * FROM sysobjects WHERE type = 'U' AND name = @nombreTablaANT) AND EXISTS (SELECT * FROM sysobjects WHERE type = 'U' AND name = @nombreTablaRES) AND EXISTS (SELECT * FROM sysobjects WHERE type = 'U' AND name = @nombreTablaEXA)
		BEGIN
			--PRINT 'Existe'
			SET	@Resultado = 1 
		END
	ELSE
		BEGIN
			--PRINT 'no existe'
			SET @Resultado = 0
		END

	RETURN @Resultado	
END