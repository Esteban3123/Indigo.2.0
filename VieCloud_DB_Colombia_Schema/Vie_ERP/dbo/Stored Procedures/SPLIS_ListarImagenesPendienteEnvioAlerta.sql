

CREATE PROCEDURE [dbo].[SPLIS_ListarImagenesPendienteEnvioAlerta]
AS
BEGIN
	SET NOCOUNT ON;
     SELECT IMAREAEXA2 , UNITIEREA2, IMAENTRES2, UNITIERES2,FECORDMED,DESSERIPS,A.IPCODPACI,D.UFUDESCRI
     FROM  dbo.HCORDLABO AS A INNER JOIN
   dbo.HCPARALEIMA as B ON A.CODSERIPS=B.CODSERIPS INNER JOIN
   dbo.INCUPSIPS AS C ON B.CODSERIPS=C.CODSERIPS INNER JOIN
   dbo.INUNIFUNC AS D ON A.UFUCODIGO=D.UFUCODIGO
     where IMAREAEXA2 IS NOT NULL OR IMAENTRES2 IS NOT NULL
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista las órdenes médicas de imágenes diagnósticas que tienen pendiente el envío de alerta, es decir, aquellas donde ya existe un área de examen o una entidad de resultados registrada en los parámetros de imagen (HCPARALEIMA). Combina las órdenes de laboratorio/imagen del paciente (HCORDLABO) con el catálogo de servicios CUPS (INCUPSIPS) para obtener el nombre del procedimiento solicitado, y con el catálogo de unidades funcionales (INUNIFUNC) para identificar el servicio o sala de origen. Devuelve por cada orden pendiente: el área de examen, la unidad de tipo de área, la entidad de resultados, la unidad de tipo de resultado, la fecha de la orden médica, el nombre del servicio, la cédula del paciente y la descripción de la unidad funcional. Este procedimiento se usa para gestionar alertas o notificaciones de imágenes diagnósticas cuyos resultados aún no han sido enviados o procesados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPLIS_ListarImagenesPendienteEnvioAlerta';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPLIS_ListarImagenesPendienteEnvioAlerta';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las órdenes de imágenes diagnósticas que tienen tiempos de área de examen o de entrega de resultados excedidos y están pendientes para envío de alerta.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarImagenesPendienteEnvioAlerta';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las órdenes deben existir en HCORDLABO con su servicio (CUPS) parametrizado en HCPARALEIMA e INCUPSIPS; La unidad funcional asociada a la orden debe existir en INUNIFUNC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarImagenesPendienteEnvioAlerta';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran órdenes cuyo servicio (CODSERIPS) esté parametrizado tanto en HCPARALEIMA como en INCUPSIPS (INNER JOIN); Solo se consideran órdenes con unidad funcional vigente en INUNIFUNC; Se excluyen órdenes sin marcas de tiempo excedido en área de examen ni en entrega de resultados', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarImagenesPendienteEnvioAlerta';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Órdenes médicas; Imágenes diagnósticas; Tiempos de área de examen; Tiempos de entrega de resultados; Paciente; Unidad funcional; Servicio CUPS; Alertas de pendientes', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarImagenesPendienteEnvioAlerta';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCORDLABO: Cuando IMAREAEXA2 IS NOT NULL OR IMAENTRES2 IS NOT NULL, se retornan los datos de la orden junto con paciente y unidad funcional para alertar', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarImagenesPendienteEnvioAlerta';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si IMAREAEXA2 IS NOT NULL OR IMAENTRES2 IS NOT NULL → La orden se incluye en el listado de imágenes pendientes de envío de alerta else La orden se excluye del resultado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarImagenesPendienteEnvioAlerta';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDLABO; dbo.HCPARALEIMA; dbo.INCUPSIPS; dbo.INUNIFUNC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarImagenesPendienteEnvioAlerta';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarImagenesPendienteEnvioAlerta';
-- GO
