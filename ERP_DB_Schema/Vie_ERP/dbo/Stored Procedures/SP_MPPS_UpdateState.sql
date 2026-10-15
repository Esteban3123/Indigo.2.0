-- =============================================
-- Author:		Emanuel Olaya Penagos
-- Create date: 09/01/2020
-- Description: sp para listar actualizar el estado del los procedimientos
-- =============================================
CREATE PROCEDURE [dbo].[SP_MPPS_UpdateState] 
	@NUMINGRES char(10),
	@AUTO int,
	@ESTSERIPS char(1)
AS
BEGIN
	declare @total int
	set @total = (SELECT COUNT(NUMINGRES) FROM dbo.HCORDIMAG WHERE NUMINGRES = @NUMINGRES AND AUTO = @AUTO)
	IF @total > 0
	BEGIN
		UPDATE dbo.HCORDIMAG SET ESTSERIPS = @ESTSERIPS WHERE NUMINGRES = @NUMINGRES AND AUTO = @AUTO
	END ELSE
	BEGIN
		UPDATE dbo.AMBORDIMA SET ESTSERIPS = @ESTSERIPS WHERE NUMINGRES = @NUMINGRES AND AUTO = @AUTO
	END

	RETURN @@ROWCOUNT
END