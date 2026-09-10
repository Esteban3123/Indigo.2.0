

CREATE PROCEDURE [dbo].[SPLIS_ListarLaboriatoriosPendienteEnvioAlerta]
AS
BEGIN
	SET NOCOUNT ON;
     SELECT LABRECMUE2 , UNITIEREC2, LABENTRES2, UNITIEENT2,FECORDMED,DESSERIPS,A.IPCODPACI,D.UFUDESCRI
     FROM  dbo.HCORDLABO AS A INNER JOIN
   dbo.HCPARALELAB as B ON A.CODSERIPS=B.CODSERIPS INNER JOIN
   dbo.INCUPSIPS AS C ON B.CODSERIPS=C.CODSERIPS INNER JOIN
   dbo.INUNIFUNC AS D ON A.UFUCODIGO=D.UFUCODIGO
     where LABRECMUE2 IS NOT NULL OR LABENTRES2 IS NOT NULL
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los exámenes de laboratorio clínico pendientes de envío de alerta, combinando las órdenes médicas de laboratorio (HCORDLABO) con los parámetros de tiempo de recepción de muestra y entrega de resultados configurados por servicio (HCPARALELAB), el nombre del examen (INCUPSIPS) y la unidad funcional o área de atención donde se solicitó (INUNIFUNC). Devuelve únicamente los registros que tienen definido al menos un tiempo de referencia (recepción de muestra o entrega de resultado), junto con la cédula del paciente, la fecha de la orden médica y la descripción del examen, con el propósito de identificar laboratorios cuyas alertas de vencimiento de tiempos aún no han sido disparadas o notificadas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPLIS_ListarLaboriatoriosPendienteEnvioAlerta';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPLIS_ListarLaboriatoriosPendienteEnvioAlerta';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las órdenes de laboratorio que tienen tiempos parametrizados de recepción de muestra o entrega de resultados, para gestionar alertas de cumplimiento de plazos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarLaboriatoriosPendienteEnvioAlerta';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las órdenes de laboratorio deben tener un servicio (CODSERIPS) parametrizado en HCPARALELAB y existente en INCUPSIPS.; La orden debe tener asociada una unidad funcional válida en INUNIFUNC.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarLaboriatoriosPendienteEnvioAlerta';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se devuelven órdenes con servicio parametrizado en HCPARALELAB (INNER JOIN obligatorio).; Solo se devuelven órdenes cuya unidad funcional exista en INUNIFUNC.; Nunca se devuelven órdenes sin tiempo de recepción de muestra ni tiempo de entrega de resultados definidos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarLaboriatoriosPendienteEnvioAlerta';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden de laboratorio; Recepción de muestra; Entrega de resultados; Paciente; Unidad funcional; Servicio (CUPS/IPS); Alerta de tiempos de laboratorio', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarLaboriatoriosPendienteEnvioAlerta';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCORDLABO: Cuando LABRECMUE2 IS NOT NULL OR LABENTRES2 IS NOT NULL, retorna la orden con sus tiempos/unidades de recepción y entrega, fecha de orden, servicio, paciente y unidad funcional.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarLaboriatoriosPendienteEnvioAlerta';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si LABRECMUE2 IS NOT NULL OR LABENTRES2 IS NOT NULL → Se incluye la orden de laboratorio en el resultado de pendientes de alerta. else Se excluye la orden del listado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarLaboriatoriosPendienteEnvioAlerta';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDLABO; dbo.HCPARALELAB; dbo.INCUPSIPS; dbo.INUNIFUNC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarLaboriatoriosPendienteEnvioAlerta';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarLaboriatoriosPendienteEnvioAlerta';
-- GO
