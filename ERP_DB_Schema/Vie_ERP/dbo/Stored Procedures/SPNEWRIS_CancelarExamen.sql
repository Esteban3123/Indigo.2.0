
/***********************************************************************************************
Modified By: Nicolás Charry
Date: 2020.01.22
Description: Store procedure para cancelar un examen del RIS Web.
************************************************************************************************/
CREATE PROCEDURE [dbo].[SPNEWRIS_CancelarExamen] 
(
@Auto varchar(50),
@Tipo bit,
@InfoEnfermeria bit,
@Folio varchar(10),
@CodigoPaciente varchar(25),
@NumeroIngreso varchar(10),
@CentroAtencion varchar(10),
@UnidadFuncional varchar(10),
@CodigoProcedimiento varchar(2),
@MotivoAnulacion varchar(2),
@Observaciones varchar(80), 
@UsuarioAnula varchar(20)
)
AS
BEGIN TRANSACTION
    BEGIN TRY
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.

	DECLARE @FechaAnulacion DATETIMEOFFSET = [Common].[GETDATE]()

	SET NOCOUNT ON;

    -- Insert statements for procedure here

	--Update RISORDENES

	BEGIN

		IF @Tipo = 0
			UPDATE RISORDENES SET ESTADO=8, USUREMITE=@UsuarioAnula, FECANULADO=@FechaAnulacion WHERE IDHCORDIMAG=@Auto

		ELSE IF @Tipo=1

			UPDATE RISORDENES SET ESTADO=8, USUREMITE=@UsuarioAnula, FECANULADO=@FechaAnulacion WHERE IDAMBORDIMA=@Auto

	END

	-- Update HCORDIMAG/AMBORDIMA

	BEGIN

		IF @Tipo = 0
		   UPDATE HCORDIMAG SET ESTSERIPS=6 WHERE AUTO=@Auto

		ELSE IF @Tipo=1

			UPDATE AMBORDIMA SET ESTSERIPS=6 WHERE AUTO=@Auto

	END

	BEGIN
		-- Insert HCIGRREMI

		INSERT INTO HCIMGANUL (NUMEFOLIO,IPCODPACI,NUMINGRES,CODCENATE,UFUCODIGO,CODSERIPS,CODMOTANU,OBSERVACI,FECANUIMG,USUANUIMG, INFENFANU) 
		VALUES(@Folio,@CodigoPaciente,@NumeroIngreso,@CentroAtencion,@UnidadFuncional,@CodigoProcedimiento,@MotivoAnulacion,@Observaciones,cast(@FechaAnulacion as datetime),@UsuarioAnula,@InfoEnfermeria)

	END

	COMMIT TRANSACTION
		RETURN 1
    END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
    RETURN 0
	END CATCH