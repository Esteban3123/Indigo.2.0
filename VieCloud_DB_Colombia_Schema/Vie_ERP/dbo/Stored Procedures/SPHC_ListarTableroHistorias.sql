
CREATE PROCEDURE [dbo].[SPHC_ListarTableroHistorias]
(
@Paciente varchar(25),
@Ingreso Char(10)
)
AS
BEGIN
	SET NOCOUNT ON;
SELECT TIPHISPAC,UFUCODIGO 
FROM dbo.HCHISPACA with(nolock)
WHERE IPCODPACI=@paciente AND NUMINGRES=@ingreso and TIPHISPAC in ('I','T')
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta el tablero de historias clínicas de un paciente en un ingreso específico, filtrando únicamente los tipos de historia ''I'' (ingreso) y ''T'' (turno u otro tipo relacionado). Recibe como parámetros la cédula o código del paciente y el número de ingreso, y retorna el tipo de historia clínica y la unidad funcional donde fue generada. Se utiliza para mostrar en un panel o tablero los folios clínicos activos o relevantes asociados a una atención hospitalaria particular.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarTableroHistorias';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarTableroHistorias';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los tipos de historia clínica y unidad funcional asociados a un paciente y a un ingreso específico, restringido a historias de tipo ''I'' o ''T'', para alimentar el tablero de historias.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarTableroHistorias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se debe proporcionar el identificador del paciente y el número de ingreso para filtrar las historias.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarTableroHistorias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se devuelven historias clínicas cuyo tipo sea ''I'' (Ingreso/Internación) o ''T''.; La consulta se realiza con NOLOCK (lectura sin bloqueo, admite lecturas sucias).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarTableroHistorias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Historia clínica; Paciente; Ingreso/atención; Unidad funcional', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarTableroHistorias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCHISPACA: Cuando IPCODPACI=@paciente AND NUMINGRES=@ingreso AND TIPHISPAC IN (''I'',''T''), retorna las columnas TIPHISPAC y UFUCODIGO de HCHISPACA.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarTableroHistorias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHISPACA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarTableroHistorias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarTableroHistorias';
-- GO
