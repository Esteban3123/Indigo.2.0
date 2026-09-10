
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que registra la remisión (referencia externa) de un examen de imagen diagnóstica desde el módulo RIS Web. Cambia el estado de la orden en RISORDENES a ''remitido'' (estado 7) y actualiza el estado del servicio en la orden de historia clínica (HCORDIMAG) o en la orden ambulatoria (AMBORDIMA) a estado 5, según si el paciente es hospitalizado (@Tipo=0) o ambulatorio (@Tipo=1). Finalmente, genera un registro en HCIMGREMI con los datos del folio, cédula del paciente, número de ingreso, centro de atención, unidad funcional, procedimiento solicitado, centro de remisión, observaciones y usuario que remite. Opera en una transacción atómica, garantizando consistencia entre las tres tablas involucradas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPNEWRIS_ReferirExamen';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPNEWRIS_ReferirExamen';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Remite un examen de imágenes diagnósticas del RIS Web a un centro externo, actualizando el estado de la orden y registrando la remisión.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPNEWRIS_ReferirExamen';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro de tipo debe ser 0 (hospitalario) o 1 (ambulatorio) para que se ejecute alguna actualización; Debe existir una orden con el identificador suministrado en la tabla correspondiente (HCORDIMAG o AMBORDIMA) y en RISORDENES', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPNEWRIS_ReferirExamen';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Toda remisión deja la orden en estado 7 en RISORDENES y estado 5 en ESTSERIPS de la tabla origen; Las modificaciones de orden y la inserción del registro de remisión se realizan dentro de una única transacción atómica; En el registro de remisión, el código del paciente se reutiliza como CODSERIPS; La fecha de remisión es generada por el sistema (Common.GETDATE), no por el cliente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPNEWRIS_ReferirExamen';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Remisión de examen de imágenes diagnósticas; RIS (Radiology Information System); Orden hospitalaria vs ambulatoria; Paciente; Centro de atención; Unidad funcional; Centro de remisión; Folio; Número de ingreso', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPNEWRIS_ReferirExamen';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] dbo.RISORDENES: Cuando el tipo es 0, marca la orden hospitalaria con ESTADO=7 (remitido), registra usuario y fecha de remisión filtrando por IDHCORDIMAG; [UPDATE] dbo.RISORDENES: Cuando el tipo es 1, marca la orden ambulatoria con ESTADO=7 (remitido), registra usuario y fecha de remisión filtrando por IDAMBORDIMA; [UPDATE] dbo.HCORDIMAG: Cuando el tipo es 0, actualiza el estado del servicio IPS (ESTSERIPS=5) en la orden hospitalaria identificada por AUTO; [UPDATE] dbo.AMBORDIMA: Cuando el tipo es 1, actualiza el estado del servicio IPS (ESTSERIPS=5) en la orden ambulatoria identificada por AUTO; [INSERT] dbo.HCIMGREMI: Siempre registra la remisión con folio, paciente, ingreso, centro de atención, unidad funcional, centro de remisión, observaciones, fecha y usuario que remite; [RAISERROR] dbo.HCIMGREMI: Ante cualquier error en la transacción se hace ROLLBACK y se retorna 0; en éxito retorna 1', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPNEWRIS_ReferirExamen';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Tipo = 0 (orden hospitalaria) → Actualiza RISORDENES por IDHCORDIMAG y HCORDIMAG por AUTO else Si Tipo = 1 (orden ambulatoria), actualiza RISORDENES por IDAMBORDIMA y AMBORDIMA por AUTO; si Ocurre excepción durante la transacción → ROLLBACK TRANSACTION y retorna 0 else COMMIT TRANSACTION y retorna 1', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPNEWRIS_ReferirExamen';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPNEWRIS_ReferirExamen';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPNEWRIS_ReferirExamen';
-- GO
