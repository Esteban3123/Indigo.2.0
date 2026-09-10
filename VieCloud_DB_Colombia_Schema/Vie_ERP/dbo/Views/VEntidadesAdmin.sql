CREATE VIEW [dbo].[VEntidadesAdmin]
AS
SELECT        CODENTADM, NOMENTADM, CODIGONIT, TIPENTADM, REGIMEN, CODSUPERINTEN
FROM            dbo.INENTADM
WHERE        (CODENTADM NOT IN
                             (SELECT        Code
                               FROM            Contract.HealthAdministrator))
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista las entidades administradoras (EPS, ARS, ARL, aseguradoras, compañías de seguros) que están registradas en el catálogo maestro pero que aún NO tienen un contrato vigente o histórico en el sistema. Combina el catálogo general de entidades administradoras (INENTADM) con el registro de administradoras con contrato (Contract.HealthAdministrator), devolviendo únicamente aquellas que no aparecen en la segunda fuente. Sirve para identificar pagadores o aseguradoras pendientes de formalizar contrato, apoyando la gestión administrativa y contractual de la institución.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'VEntidadesAdmin';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'VEntidadesAdmin';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Listar las entidades administradoras del catálogo maestro que aún no se encuentran registradas como administradoras de salud en el módulo de contratos, para permitir su alta o gestión administrativa.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VEntidadesAdmin';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el catálogo de entidades administradoras y la tabla de administradoras de salud contratadas para evaluar la exclusión.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VEntidadesAdmin';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen entidades administradoras del catálogo maestro que aún no han sido registradas como administradoras de salud en el módulo de contratos.; Excluye del listado toda entidad cuyo código ya exista en Contract.HealthAdministrator.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VEntidadesAdmin';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Entidad administradora; Administradora de salud; Régimen; NIT; Superintendencia', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VEntidadesAdmin';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.INENTADM: Devuelve los datos identificatorios (código, nombre, NIT, tipo, régimen, código superintendencia) de las entidades cuyo CODENTADM NO está presente en Contract.HealthAdministrator.Code.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VEntidadesAdmin';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INENTADM; Contract.HealthAdministrator', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VEntidadesAdmin';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VEntidadesAdmin';
GO
