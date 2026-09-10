
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cancela un examen de imágenes diagnósticas (radiología, ecografía, tomografía, etc.) en el módulo RIS Web. Marca la orden como anulada (estado 8) en la tabla de órdenes RIS y actualiza el estado del servicio (estado 6) en la historia clínica del paciente hospitalizado (HCORDIMAG) o en la orden ambulatoria (AMBORDIMA), según el tipo de ingreso indicado. Registra el evento de anulación en el log de imágenes anuladas (HCIMGANUL), guardando el motivo, las observaciones, la fecha y el usuario que realizó la cancelación. Opera dentro de una transacción para garantizar consistencia: si algún paso falla, revierte todos los cambios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPNEWRIS_CancelarExamen';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPNEWRIS_CancelarExamen';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Cancela/anula un examen de imagenología en el RIS, marcando la orden y su servicio asociado como anulados y registrando la auditoría de la anulación.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPNEWRIS_CancelarExamen';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El identificador de la orden debe corresponder al tipo indicado (hospitalario=0 → IDHCORDIMAG; ambulatorio=1 → IDAMBORDIMA); Deben existir las órdenes en RISORDENES y en HCORDIMAG o AMBORDIMA según el tipo; Los códigos de paciente, ingreso, centro de atención, unidad funcional, procedimiento y motivo de anulación deben ser válidos para la inserción en HCIMGANUL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPNEWRIS_CancelarExamen';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Toda la cancelación se ejecuta dentro de una transacción: o se aplican todos los cambios o ninguno (ROLLBACK ante error); El estado final de la orden cancelada en RISORDENES siempre es 8; El estado final del servicio en HCORDIMAG/AMBORDIMA tras anulación siempre es 6; Siempre se registra una fila de auditoría de anulación en HCIMGANUL por cada cancelación exitosa; La fecha de anulación se toma del servidor (Common.GETDATE), no del cliente; El flujo distingue origen hospitalario vs. ambulatorio mediante la bandera Tipo (0/1); Devuelve 1 en éxito y 0 en error', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPNEWRIS_CancelarExamen';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cancelación/anulación de examen de imagenología; Orden de imágenes hospitalaria; Orden de imágenes ambulatoria; Motivo de anulación; Información de enfermería; RIS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPNEWRIS_CancelarExamen';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] dbo.RISORDENES: Cuando Tipo=0, se actualiza ESTADO=8, USUREMITE=usuario y FECANULADO=fecha actual donde IDHCORDIMAG coincide con el identificador recibido; [UPDATE] dbo.RISORDENES: Cuando Tipo=1, se actualiza ESTADO=8, USUREMITE=usuario y FECANULADO=fecha actual donde IDAMBORDIMA coincide con el identificador recibido; [UPDATE] dbo.HCORDIMAG: Cuando Tipo=0, se marca ESTSERIPS=6 para la orden cuyo AUTO coincide con el identificador recibido; [UPDATE] dbo.AMBORDIMA: Cuando Tipo=1, se marca ESTSERIPS=6 para la orden cuyo AUTO coincide con el identificador recibido; [INSERT] dbo.HCIMGANUL: Tras actualizar las órdenes, siempre se inserta un registro de anulación con folio, paciente, ingreso, centro, unidad funcional, procedimiento, motivo, observaciones, fecha, usuario y bandera de información de enfermería; [RETURN_RESULT] dbo.HCIMGANUL: Si todo el bloque TRY se ejecuta sin error, hace COMMIT y retorna 1; en caso de excepción hace ROLLBACK y retorna 0', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPNEWRIS_CancelarExamen';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Tipo = 0 (orden hospitalaria) → Actualiza RISORDENES por IDHCORDIMAG y marca HCORDIMAG.ESTSERIPS=6 else Si Tipo = 1 (orden ambulatoria), actualiza RISORDENES por IDAMBORDIMA y marca AMBORDIMA.ESTSERIPS=6', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPNEWRIS_CancelarExamen';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPNEWRIS_CancelarExamen';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPNEWRIS_CancelarExamen';
-- GO
