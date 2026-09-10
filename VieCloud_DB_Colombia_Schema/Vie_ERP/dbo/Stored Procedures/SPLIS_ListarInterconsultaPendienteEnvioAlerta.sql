

CREATE PROCEDURE [dbo].[SPLIS_ListarInterconsultaPendienteEnvioAlerta]
AS
BEGIN
	SET NOCOUNT ON;
     SELECT TIEMAXATE2 , UNITIEATE2,FECORDMED,DESSERIPS,A.IPCODPACI,D.UFUDESCRI
     FROM  dbo.HCORDLABO AS A INNER JOIN
   dbo.HCPARALEINT as B ON A.CODSERIPS=B.CODSERIPS INNER JOIN
   dbo.INCUPSIPS AS C ON B.CODSERIPS=C.CODSERIPS INNER JOIN
   dbo.INUNIFUNC AS D ON A.UFUCODIGO=D.UFUCODIGO
     where TIEMAXATE2 IS NOT NULL 
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista las interconsultas (órdenes de laboratorio clínico) que están pendientes de atención y requieren envío de alerta por tiempo de espera. Cruza las órdenes médicas registradas en la historia clínica con los parámetros de tiempo máximo de atención por servicio CUPS, el catálogo de servicios y las unidades funcionales, para identificar aquellas órdenes donde ya existe configurado un segundo nivel de tiempo máximo de atención (TIEMAXATE2). Devuelve el tiempo límite configurado, la unidad de medida de ese tiempo, la fecha en que se generó la orden médica, el nombre del servicio o examen solicitado, la cédula o identificación del paciente y el área o servicio donde se debe atender. Este procedimiento es el motor de notificaciones de alerta temprana para interconsultas que están próximas a vencer o que exceden los tiempos de respuesta establecidos por la institución.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPLIS_ListarInterconsultaPendienteEnvioAlerta';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPLIS_ListarInterconsultaPendienteEnvioAlerta';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las órdenes/interconsultas de laboratorio cuyo servicio tiene definido un tiempo máximo de atención, para efectos de notificación o alerta de pendientes.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarInterconsultaPendienteEnvioAlerta';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir parametrización de tiempos máximos de atención (HCPARALEINT) para los servicios CUPS de las órdenes.; El servicio CUPS de la orden debe estar registrado en INCUPSIPS.; La unidad funcional de la orden debe existir en INUNIFUNC.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarInterconsultaPendienteEnvioAlerta';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran órdenes cuyo tiempo máximo de atención (TIEMAXATE2) está definido (no nulo).; La orden debe estar asociada a un servicio CUPS parametrizado con tiempos máximos en HCPARALEINT y existente en INCUPSIPS.; La orden debe estar asociada a una unidad funcional vigente en el catálogo INUNIFUNC.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarInterconsultaPendienteEnvioAlerta';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Interconsulta; Orden de laboratorio; Tiempo máximo de atención; Unidad funcional; Servicio CUPS; Paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarInterconsultaPendienteEnvioAlerta';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCORDLABO: Cuando TIEMAXATE2 IS NOT NULL y existe correspondencia entre orden, parámetro de tiempos, CUPS y unidad funcional, se retorna el conjunto de órdenes pendientes con su tiempo máximo, unidad de tiempo, fecha de orden, servicio, paciente y unidad funcional.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarInterconsultaPendienteEnvioAlerta';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDLABO; dbo.HCPARALEINT; dbo.INCUPSIPS; dbo.INUNIFUNC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarInterconsultaPendienteEnvioAlerta';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarInterconsultaPendienteEnvioAlerta';
-- GO
