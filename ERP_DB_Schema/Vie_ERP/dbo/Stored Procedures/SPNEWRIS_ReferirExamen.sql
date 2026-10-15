
/***********************************************************************************************
Modified By: Nicolás Charry
Date: 2020.01.22
Description: Store procedure para remitir un examen del RIS Web.
************************************************************************************************/
CREATE PROCEDURE [dbo].[SPNEWRIS_ReferirExamen] 
(
@Auto varchar(50),
@Tipo bit,
@Folio varchar(10),
@CodigoPaciente varchar(25),
@NumeroIngreso varchar(10),
@CentroAtencion varchar(10),
@UnidadFuncional varchar(10),
@CodigoProcedimiento varchar(20),
@CentroRemision varchar(100),
@Observaciones varchar(80), 
@UsuarioRemite varchar(20)
)
AS
BEGIN TRANSACTION
    BEGIN TRY
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.

	DECLARE @FechaRemision DATETIMEOFFSET = [Common].[GETDATE]()

	SET NOCOUNT ON;

    -- Insert statements for procedure here

	--Update RISORDENES

	BEGIN

		IF @Tipo = 0
			UPDATE RISORDENES SET ESTADO=7, USUREMITE=@UsuarioRemite, FECREMITE=@FechaRemision WHERE IDHCORDIMAG=@Auto

		ELSE IF @Tipo=1

			UPDATE RISORDENES SET ESTADO=7, USUREMITE=@UsuarioRemite, FECREMITE=@FechaRemision WHERE IDAMBORDIMA=@Auto

	END

	-- Update HCORDIMAG/AMBORDIMA

	BEGIN

		IF @Tipo = 0
		   UPDATE HCORDIMAG SET ESTSERIPS=5 WHERE AUTO=@Auto

		ELSE IF @Tipo=1

			UPDATE AMBORDIMA SET ESTSERIPS=5 WHERE AUTO=@Auto

	END

	BEGIN
		-- Insert HCIGRREMI

		INSERT INTO HCIMGREMI (NUMEFOLIO,IPCODPACI,NUMINGRES,CODCENATE,UFUCODIGO,CODSERIPS,CODCENLAB,OBSERVACI,FECREMIMG,USUREMIMG) 
		VALUES(@Folio,@CodigoPaciente,@NumeroIngreso,@CentroAtencion,@UnidadFuncional,@CodigoPaciente,@CentroRemision,@Observaciones,cast(@FechaRemision as datetime),@UsuarioRemite)

	END

	COMMIT TRANSACTION
		RETURN 1
    END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
    RETURN 0
	END CATCH