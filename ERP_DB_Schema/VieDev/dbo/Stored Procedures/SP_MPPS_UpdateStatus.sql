-- =============================================
-- Author:		Emanuel Olaya Penagos
-- Create date: 09/01/2020
-- Description: sp para listar actualizar el estado del los procedimientos
-- =============================================
CREATE PROCEDURE [dbo].[SP_MPPS_UpdateStatus] 
	@AutoRisOrdenes varchar(16),
	@RisEstado int
AS
BEGIN

	declare @NUMINGRES varchar(10)
	declare @AUTO int
	declare @total int

	declare @len int
	declare @index int

	set @index = CHARINDEX('-', @AutoRisOrdenes)
	set @len = LEN(@AutoRisOrdenes)

	set @NUMINGRES = SUBSTRING(@AutoRisOrdenes, 0, CHARINDEX('-', @AutoRisOrdenes))
	set @AUTO = SUBSTRING(@AutoRisOrdenes, @index+1, @len-@index)

	set @total = (SELECT COUNT(NUMINGRES) FROM dbo.HCORDIMAG WHERE NUMINGRES = @NUMINGRES AND AUTO = @AUTO)
	IF @total > 0
	BEGIN
		IF @RisEstado = 3 BEGIN
			UPDATE dbo.HCORDIMAG SET ESTTRASER = '1', TIENEGRABACION = '0', ESTSERIPS = @RisEstado WHERE NUMINGRES = @NUMINGRES AND AUTO = @AUTO
		END ELSE BEGIN
			UPDATE dbo.HCORDIMAG SET ESTSERIPS = @RisEstado WHERE NUMINGRES = @NUMINGRES AND AUTO = @AUTO
		END
	END ELSE
	BEGIN
		IF @RisEstado = 3 BEGIN
			UPDATE dbo.HCORDIMAG SET ESTTRASER = '1', TIENEGRABACION = '0', ESTSERIPS = @RisEstado WHERE NUMINGRES = @NUMINGRES AND AUTO = @AUTO
		END ELSE BEGIN
			UPDATE dbo.HCORDIMAG SET ESTSERIPS = @RisEstado WHERE NUMINGRES = @NUMINGRES AND AUTO = @AUTO
		END
	END

	RETURN @@ROWCOUNT
END