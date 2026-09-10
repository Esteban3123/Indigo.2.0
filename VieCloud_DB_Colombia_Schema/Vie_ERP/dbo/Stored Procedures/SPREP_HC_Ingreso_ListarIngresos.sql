
CREATE PROCEDURE [dbo].[SPREP_HC_Ingreso_ListarIngresos]
(
@NumeroIngreso nChar(10)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
 SELECT IAUTORIZA AS 'NUMERO AUTORIZACION INGRESO',ITIPORIES AS 'TIPO DE RIESGO',ITIPORIES AS 'TIPO DE RIESGO',IFECHAING AS 'FECHA DEL INGRESO',CASE ICAUSAING WHEN 1 THEN 'Heridos en combate' WHEN 2 THEN 'Enfermedad profesional' WHEN 3 THEN 'Enfermedad general adulto' WHEN 4 THEN 'Enfermedad general pediatria' WHEN 5 THEN 'Odontología' WHEN 6 THEN 'Accidente de transito' WHEN 7 THEN 'Catastrofe/Fisalud' WHEN 8 THEN 'Quemados' WHEN 9 THEN 'Maternidad' WHEN 10 THEN 'Accidente Laboral' WHEN 11 THEN 'Cirugia Programada' END AS 'CAUSA DEL INGRESO'
                 
FROM ADINGRESO WITH(NOLOCK)

WHERE NUMINGRES = @NumeroIngreso

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que consulta y retorna los datos principales de un ingreso o admisión hospitalaria a partir de un número de ingreso específico. Recupera información como el número de autorización del ingreso, el tipo de riesgo, la fecha de ingreso y la causa del ingreso (enfermedad general, accidente de tránsito, maternidad, cirugía programada, accidente laboral, entre otras). Consulta la tabla de ingresos ADINGRESO filtrando por el número de ingreso recibido como parámetro. Se utiliza en la historia clínica para visualizar el resumen del episodio de atención del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Ingreso_ListarIngresos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Ingreso_ListarIngresos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consulta los datos básicos de un ingreso hospitalario (autorización, tipo de riesgo, fecha y causa) traduciendo el código de causa a su descripción legible.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Ingreso_ListarIngresos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en ADINGRESO cuyo NUMINGRES coincida con el número de ingreso recibido.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Ingreso_ListarIngresos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Sólo se retornan ingresos cuyo número coincide exactamente con el parámetro recibido.; El catálogo de causas de ingreso reconocido se limita a los valores 1 a 11; otros valores no obtienen descripción.; La consulta usa NOLOCK, por lo que no toma bloqueos sobre ADINGRESO y puede leer datos no confirmados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Ingreso_ListarIngresos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ingreso hospitalario; Autorización de ingreso; Tipo de riesgo; Causa de ingreso; Maternidad; Accidente de tránsito; Accidente laboral; Enfermedad profesional; Enfermedad general; Odontología; Quemados; Cirugía programada; Heridos en combate; Catástrofe/Fisalud', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Ingreso_ListarIngresos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ADINGRESO: Cuando NUMINGRES = parámetro de entrada, devuelve número de autorización, tipo de riesgo, fecha de ingreso y causa de ingreso descriptiva.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Ingreso_ListarIngresos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ICAUSAING = 1 → Causa = ''Heridos en combate''; si ICAUSAING = 2 → Causa = ''Enfermedad profesional''; si ICAUSAING = 3 → Causa = ''Enfermedad general adulto''; si ICAUSAING = 4 → Causa = ''Enfermedad general pediatria''; si ICAUSAING = 5 → Causa = ''Odontología''; si ICAUSAING = 6 → Causa = ''Accidente de transito''; si ICAUSAING = 7 → Causa = ''Catastrofe/Fisalud''; si ICAUSAING = 8 → Causa = ''Quemados''; si ICAUSAING = 9 → Causa = ''Maternidad''; si ICAUSAING = 10 → Causa = ''Accidente Laboral''; si ICAUSAING = 11 → Causa = ''Cirugia Programada'' else Causa = NULL (valor de ICAUSAING fuera del catálogo definido)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Ingreso_ListarIngresos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Ingreso_ListarIngresos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Ingreso_ListarIngresos';
-- GO
