

CREATE VIEW [Billing].[ViewRIASCups]
AS

	select r.ID RiasId, rc.ID RiasCupsId, r.CODPRO RiasCode, r.NOMBRE RiasName, r.CODPRO + ' - ' + r.NOMBRE RiasCodeName, r.ESTADO RiasStatus, rc.CODSERIPS CupsCode
	from .RIASCUPS rc
	inner join .RIAS r on r.ID = rc.IDRIAS
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que relaciona los códigos de procedimientos y servicios CUPS con las Rutas Integrales de Atención en Salud (RIAS), combinando el catálogo de RIAS con la tabla de asociación RIASCUPS para obtener, por cada código CUPS, la ruta correspondiente con su código, nombre y estado. Permite identificar a qué ruta de atención en salud pertenece cada servicio o procedimiento facturado, facilitando la clasificación, reportería RIPS-RIAS y procesos de facturación bajo el modelo de rutas integrales. Se usa para consultar qué procedimientos están vinculados a cada RIAS activa o inactiva en el sistema de salud.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewRIASCups';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewRIASCups';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone la relación entre RIAS y sus códigos CUPS asociados, entregando identificadores, código, nombre, estado de la RIAS y el código CUPS vinculado.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewRIASCups';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las tablas RIAS y RIASCUPS deben existir y estar relacionadas por RIASCUPS.IDRIAS = RIAS.ID.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewRIASCups';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo expone filas de RIASCUPS que tengan una RIAS asociada existente (INNER JOIN sobre IDRIAS = RIAS.ID).; Construye un campo concatenado de presentación con formato ''CODPRO - NOMBRE'' para identificación de la RIAS.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewRIASCups';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'RIAS (Rutas Integrales de Atención en Salud); CUPS (Clasificación Única de Procedimientos en Salud)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewRIASCups';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RIASCUPS: Devuelve un registro por cada CUPS de RIASCUPS que cuente con una RIAS existente (INNER JOIN con RIAS por IDRIAS).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewRIASCups';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'RIASCUPS; RIAS', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewRIASCups';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewRIASCups';
GO
