

CREATE PROCEDURE [dbo].[SPLIS_ListarPatologiasInterfaz]
AS
BEGIN
	SET NOCOUNT ON;
     SELECT IDETIPHIS, NUMEFOLIO, IPCODPACI, NUMINGRES, CODSERIPS
     FROM      dbo.HCORDPATO
     WHERE ESTSERIPS='2' AND SERREAINT='True'
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista las órdenes de patología e imágenes diagnósticas que están listas para ser enviadas a una interfaz o sistema externo. Filtra únicamente los exámenes con estado ''2'' (resultado disponible o aprobado) y marcados para reintegración con sistemas externos (SERREAINT=''True''). Retorna el tipo de historia, folio, cédula del paciente, número de ingreso y código del servicio solicitado, sirviendo como punto de integración entre el módulo de patología del EHR y sistemas de laboratorio, PACS u otras plataformas diagnósticas externas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPLIS_ListarPatologiasInterfaz';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPLIS_ListarPatologiasInterfaz';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las órdenes de patología pendientes de envío a la interfaz IPS, marcadas como reales para interfaz y con estado IPS específico.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarPatologiasInterfaz';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La tabla de órdenes de patología debe contener registros con estado IPS = ''2'' y marca de envío real a interfaz en ''True''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarPatologiasInterfaz';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen órdenes de patología cuyo estado IPS sea ''2''.; Solo se exponen órdenes marcadas como envío real a interfaz (SERREAINT=''True'').; No realiza modificaciones; es de solo lectura (SET NOCOUNT ON).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarPatologiasInterfaz';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden de patología; Interfaz IPS; Folio de orden; Paciente; Ingreso hospitalario; Código de servicio IPS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarPatologiasInterfaz';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCORDPATO: Cuando ESTSERIPS=''2'' AND SERREAINT=''True'', se retorna el conjunto de identificadores de la orden (tipo HIS, folio, paciente, ingreso, código servicio IPS).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarPatologiasInterfaz';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDPATO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarPatologiasInterfaz';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarPatologiasInterfaz';
-- GO
