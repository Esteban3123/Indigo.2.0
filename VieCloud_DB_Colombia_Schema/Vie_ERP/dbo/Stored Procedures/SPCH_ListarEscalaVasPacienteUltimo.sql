
CREATE PROCEDURE [dbo].[SPCH_ListarEscalaVasPacienteUltimo]
(
@Paciente Varchar(25)
)
AS
BEGIN
	SET NOCOUNT ON;
	
	DECLARE @Consecutivo as int
	SELECT  @Consecutivo = CODCONSEC FROM HCESCVASC WHERE IPCODPACI = @Paciente

	SELECT 
			--CODTIPDOL AS TipoDolor,
			--CODPARCUE AS ParteCuerpo,
			--PTSESTREP AS PuntosReposo,
			--PTSESTMOV AS PuntosMovimiento,
			--POSIMGCUX AS PosicionX,
			--POSIMGCUY AS PosicionY
			CODTIPDOL,
			CODPARCUE,
			PTSESTREP,
			PTSESTMOV,
			POSIMGCUX,
			POSIMGCUY
		FROM
			HCESCVASD
		WHERE
			CODCONSEC =@Consecutivo

end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obtiene la última evaluación de dolor registrada en la escala VAS (Escala Visual Análoga del Dolor) para un paciente específico, identificado por su cédula o código de paciente. Primero busca el consecutivo de la sesión de valoración en la tabla de encabezado de escala VAS (HCESCVASC) y luego recupera el detalle del dolor desde HCESCVASD, incluyendo el tipo de dolor, la parte del cuerpo afectada, los puntos de dolor en reposo y en movimiento, y la posición marcada por el paciente en el mapa corporal (coordenadas X e Y). Se usa en la historia clínica para consultar la valoración de dolor más reciente del paciente, apoyando el seguimiento del manejo del dolor en hospitalización, urgencias o consulta.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_ListarEscalaVasPacienteUltimo';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_ListarEscalaVasPacienteUltimo';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recuperar los puntos de dolor (tipo, parte del cuerpo, intensidad en reposo y movimiento, y posición en imagen corporal) del último registro de escala visual analógica (VAS) del paciente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarEscalaVasPacienteUltimo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en la cabecera de escala VAS para el paciente indicado; de lo contrario el consecutivo queda nulo y el detalle no retorna filas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarEscalaVasPacienteUltimo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La cabecera de la escala VAS se identifica por un único consecutivo asociado al paciente.; El detalle de la escala VAS se vincula a la cabecera mediante el consecutivo.; Solo se devuelven los registros de detalle correspondientes al consecutivo encontrado para el paciente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarEscalaVasPacienteUltimo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Escala visual analógica del dolor (EVA/VAS); Paciente; Tipo de dolor; Parte del cuerpo; Dolor en reposo; Dolor en movimiento', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarEscalaVasPacienteUltimo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCESCVASD: Cuando existe cabecera de escala VAS para el paciente, se retorna el detalle (tipo de dolor, parte del cuerpo, puntaje en reposo y movimiento, coordenadas X/Y) filtrado por el consecutivo de esa cabecera.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarEscalaVasPacienteUltimo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCESCVASC; dbo.HCESCVASD', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarEscalaVasPacienteUltimo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarEscalaVasPacienteUltimo';
-- GO
