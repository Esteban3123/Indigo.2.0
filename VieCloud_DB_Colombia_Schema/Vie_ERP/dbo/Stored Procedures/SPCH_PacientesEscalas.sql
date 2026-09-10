
CREATE PROCEDURE [dbo].[SPCH_PacientesEscalas]
(
@Paciente Varchar(25),
@Ingreso Char(20)
)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT TOP 1 '001' AS Codigo , 'Escala Bieri' AS Escala
	from HCESCABIE
	where IPCODPACI=@Paciente and NUMINGRES=@Ingreso
	UNION ALL
	select TOP 1 '002' as Codigo, 'Escala Apache' as Escala 
	from HCESCAPAC
	where IPCODPACI=@Paciente and NUMINGRES=@Ingreso
	UNION ALL
	SELECT TOP 1 '003' AS Codigo , 'Escala DownTon' as Escala
	from HCESCDOWN
	where IPCODPACI=@Paciente and NUMINGRES=@Ingreso
	UNION ALL
	SELECT TOP 1 '004' AS Codigo , 'Escala NorTon' as Escala
	from HCESCNTON
	where IPCODPACI=@Paciente and NUMINGRES=@Ingreso
	UNION ALL
	SELECT TOP 1 '005' AS Codigo, 'Escala RASS' as Escala
	FROM HCESCRASS
	 where IPCODPACI=@Paciente and NUMINGRES=@Ingreso
	 SELECT TOP 1 '006' AS Codigo, 'Escala VAS' as Escala
	FROM HCESCRASS
	 where IPCODPACI=@Paciente and NUMINGRES=@Ingreso

--SELECT case   when dbo.PuntajeEscalaDownTon(J.NUMINGRES, J.IPCODPACI)>=1 then 'El Paciente Presenta Riesgo de Caidas, Puntaje '  + RTRIM(cast( dbo.PuntajeEscalaDownTon(J.NUMINGRES, J.IPCODPACI) as char)) end as   PUNTAJEDOWN,
--case  dbo.PuntajeEscalaRass(J.NUMINGRES, J.IPCODPACI)  when 0 then 'Sedacion : El paciente esta alerta, tranquilo' when 1 then 'Sedacion : El paciente esta inquieto o agitado' WHEN 2 THEN  'Sedacion : El paciente esta inquieto o agitado' WHEN 3 THEN  'Sedacion : El paciente esta inquieto o agitado' WHEN 4  THEN  'Sedacion : El paciente esta inquieto o agitado' WHEN -1 THEN 'Sedacion : El paciente se despierta y  abre los ojos, manteniendo contacto visual durante mas de 10 segundos' WHEN -2 THEN 'Sedacion : El paciente se despierta y  abre los ojos, manteniendo contacto visual durante menos de 10 segundos' WHEN -3 THEN 'Sedacion : El paciente se mueve a la llamada pero sin abrir los ojos' WHEN -4 THEN 'Sedacion : El paciente se mueve ante el estimulo fisico' WHEN -5 THEN 'Sedacion : El paciente no se mueve ante ningun estimulo' END AS   EscalaRASS
--FROM  dbo.ADINGRESO J
--WHERE J.IPCODPACI=@Paciente AND j.NUMINGRES = @Ingreso 
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que consulta qué escalas clínicas de valoración han sido aplicadas a un paciente en un ingreso hospitalario específico, identificados por cédula del paciente y número de ingreso. Retorna una lista de las escalas diligenciadas, indicando cuáles de las siguientes están registradas: Escala Bieri (escabiosis/sarna), Escala Apache (gravedad en cuidados intensivos), Escala Downton (riesgo de caídas), Escala Norton (valoración funcional y riesgo de úlceras), Escala RASS (nivel de sedación y agitación) y Escala VAS (dolor). Sirve para que la historia clínica o el equipo asistencial verifique rápidamente cuáles instrumentos de valoración clínica ya fueron completados para un paciente durante su hospitalización o atención, evitando duplicar evaluaciones y facilitando el seguimiento del estado clínico del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_PacientesEscalas';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_PacientesEscalas';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las escalas clínicas (Bieri, Apache, DownTon, NorTon, RASS, VAS) que han sido registradas para un paciente en un ingreso específico.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_PacientesEscalas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente y el ingreso (admisión) deben existir y coincidir en las tablas de escalas para que se devuelvan filas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_PacientesEscalas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada escala se reporta como máximo una vez (TOP 1) por paciente/ingreso.; Solo se devuelven las escalas para las que existe al menos un registro asociado al paciente e ingreso indicados.; Cada escala devuelta se identifica con un código fijo: 001 Bieri, 002 Apache, 003 DownTon, 004 NorTon, 005 RASS, 006 VAS.; Las escalas RASS (005) y VAS (006) se obtienen de la misma tabla HCESCRASS, por lo que VAS no tiene almacenamiento propio diferenciado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_PacientesEscalas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso; Escala Bieri; Escala Apache; Escala DownTon; Escala NorTon; Escala RASS; Escala VAS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_PacientesEscalas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCESCABIE: Si existe registro en HCESCABIE para el paciente e ingreso, se devuelve fila con código ''001'' y ''Escala Bieri''.; [RETURN_RESULT] dbo.HCESCAPAC: Si existe registro en HCESCAPAC para el paciente e ingreso, se devuelve fila con código ''002'' y ''Escala Apache''.; [RETURN_RESULT] dbo.HCESCDOWN: Si existe registro en HCESCDOWN para el paciente e ingreso, se devuelve fila con código ''003'' y ''Escala DownTon''.; [RETURN_RESULT] dbo.HCESCNTON: Si existe registro en HCESCNTON para el paciente e ingreso, se devuelve fila con código ''004'' y ''Escala NorTon''.; [RETURN_RESULT] dbo.HCESCRASS: Si existe registro en HCESCRASS para el paciente e ingreso, se devuelve fila con código ''005'' y ''Escala RASS'' (en el primer SELECT) y, en un segundo result set independiente, fila con código ''006'' y ''Escala VAS'' a partir de la misma tabla.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_PacientesEscalas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCESCABIE; dbo.HCESCAPAC; dbo.HCESCDOWN; dbo.HCESCNTON; dbo.HCESCRASS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_PacientesEscalas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_PacientesEscalas';
-- GO
