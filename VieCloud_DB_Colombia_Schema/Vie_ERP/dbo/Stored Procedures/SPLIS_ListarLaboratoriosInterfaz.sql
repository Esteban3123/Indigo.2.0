

CREATE PROCEDURE [dbo].[SPLIS_ListarLaboratoriosInterfaz]
AS
BEGIN
	SET NOCOUNT ON;
     SELECT IDETIPHIS, NUMEFOLIO, IPCODPACI, NUMINGRES, CODSERIPS
     FROM      dbo.HCORDLABO
     WHERE ESTSERIPS='2' AND SERREAINT='True'
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los exámenes de laboratorio clínico que están listos para ser enviados a la interfaz del sistema de laboratorio externo. Consulta las órdenes médicas de laboratorio (HCORDLABO) filtrando únicamente aquellas con estado de servicio igual a ''2'' (resultado disponible o estado específico de procesamiento) y marcadas para reenvío a la interfaz (SERREAINT=''True''). Retorna el tipo de historia, número de folio, cédula del paciente, número de ingreso y código del servicio o examen solicitado. Se usa para la integración o interfaz entre el sistema clínico y el laboratorio externo, permitiendo identificar qué órdenes de laboratorio deben ser transmitidas o sincronizadas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPLIS_ListarLaboratoriosInterfaz';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPLIS_ListarLaboratoriosInterfaz';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las órdenes de laboratorio pendientes de envío a una interfaz externa para su procesamiento/integración.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarLaboratoriosInterfaz';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las órdenes de laboratorio deben existir en HCORDLABO con estado de servicio igual a ''2'' y marca de servicio real de interfaz en ''True''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarLaboratoriosInterfaz';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se devuelven órdenes con ESTSERIPS=''2''.; Solo se devuelven órdenes marcadas para interfaz real (SERREAINT=''True'').; No modifica datos; es solo de consulta.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarLaboratoriosInterfaz';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Órdenes de laboratorio; Historia clínica; Paciente; Ingreso/Admisión; Servicio IPS; Interfaz de laboratorio (LIS)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarLaboratoriosInterfaz';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCORDLABO: Cuando ESTSERIPS=''2'' AND SERREAINT=''True'', se retornan los identificadores de tipo HIS, folio, paciente, ingreso y código de servicio IPS.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarLaboratoriosInterfaz';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDLABO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarLaboratoriosInterfaz';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarLaboratoriosInterfaz';
-- GO
