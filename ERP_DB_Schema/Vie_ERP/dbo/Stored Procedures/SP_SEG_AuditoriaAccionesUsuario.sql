
-- =============================================
-- Author:		Juan Montealegre
-- Create date: 01/10/2013
-- Description:	Registro de Auditoria
-- =============================================
CREATE PROCEDURE  [dbo].[SP_SEG_AuditoriaAccionesUsuario]
	-- Add the parameters for the stored procedure here
	@CodigoUsuario char(20),
	@NombreEquipo char(20),
	@DescripcionMensaje varchar(max)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO INDAUDITOR (CODUSUARI, FECREGISTRO, NOMEQUIPO, MENSAJEAUD)  VALUES ( @CodigoUsuario, getdate(), @NombreEquipo, @DescripcionMensaje)
END