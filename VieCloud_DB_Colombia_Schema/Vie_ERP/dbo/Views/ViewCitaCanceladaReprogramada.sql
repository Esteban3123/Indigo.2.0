
CREATE VIEW [dbo].[ViewCitaCanceladaReprogramada]
AS
SELECT CODIGOUSUARIO = RTRIM(G.CANCELUSU)
,FECHA = CONVERT(VARCHAR(10),G.FECHCANCELA,103) + ' ' +  CONVERT(VARCHAR(8),G.FECHCANCELA,114)
,CODIGOMOTIVO = RTRIM(ISNULL(M.CODCAUCAN,''))
,MOTIVO = RTRIM(ISNULL(M.DESCAUCAN,''))
,USUARIO = RTRIM(U.NOMUSUARI)
,TIPO = CAST(1 AS TINYINT)
,G.CODAUTONU
FROM AGASICITA G WITH(NOLOCK)
LEFT OUTER JOIN AGCAUCANA M WITH(NOLOCK)
ON M.CODCAUCAN = G.CODCAUCAN 
LEFT OUTER JOIN dbo.SEGusuaru U WITH(NOLOCK)
ON U.CODUSUARI = RTRIM(G.CANCELUSU)
where G.FECHCANCELA IS NOT NULL
UNION ALL
SELECT CODIGOUSUARIO = RTRIM(G.CODUSUARIOREPRO)
,FECHA = CONVERT(VARCHAR(10),G.FECHAREPRO,103) + ' ' +  CONVERT(VARCHAR(8),G.FECHAREPRO,114)
,CODIGOMOTIVO = RTRIM(ISNULL(G.CODMOTIVOREPRO,''))
,MOTIVO = RTRIM(ISNULL(MR.DESMOTANU,''))
,USUARIO = RTRIM(UR.NOMUSUARI)
,TIPO = CAST(2 AS TINYINT)
,G.CODAUTONU
FROM AGASICITA G WITH(NOLOCK)
LEFT OUTER JOIN dbo.HCMOANULB  MR WITH(NOLOCK)
ON MR.CODMOTANU = G.CODMOTIVOREPRO
LEFT OUTER JOIN dbo.SEGusuaru UR WITH(NOLOCK)
ON UR.CODUSUARI = RTRIM(G.CODUSUARIOREPRO)
where G.FECHAREPRO IS NOT NULL
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida el historial de cancelaciones y reprogramaciones de citas médicas en el módulo de agendamiento. Combina dos conjuntos de datos mediante UNION ALL: el primero recoge las citas canceladas (TIPO=1), indicando el usuario que canceló, la fecha y hora de cancelación, y el motivo según el catálogo de causas de cancelación (AGCAUCANA); el segundo recoge las citas reprogramadas (TIPO=2), con el usuario que realizó la reprogramación, la fecha y el motivo tomado del catálogo de motivos de anulación de historia clínica (HCMOANULB). En ambos casos, resuelve el nombre completo del usuario responsable consultando el registro de usuarios del sistema (SEGusuaru). Sirve como fuente de reportería y auditoría para analizar por qué y quién canceló o reprogramó una cita, permitiendo gestión de calidad en la atención y seguimiento de agendas médicas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewCitaCanceladaReprogramada';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewCitaCanceladaReprogramada';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Unifica en un solo conjunto los eventos de cancelación y de reprogramación de citas, con usuario responsable, fecha y motivo, para reportería y auditoría de agenda médica.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewCitaCanceladaReprogramada';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La tabla AGASICITA debe registrar FECHCANCELA y CANCELUSU al cancelar una cita.; La tabla AGASICITA debe registrar FECHAREPRO y CODUSUARIOREPRO al reprogramar una cita.; Los catálogos AGCAUCANA y HCMOANULB deben contener los códigos de motivo referenciados para resolver la descripción.; SEGusuaru debe contener los códigos de usuario que cancelaron o reprogramaron para resolver el nombre.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewCitaCanceladaReprogramada';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen citas con fecha de cancelación o de reprogramación registrada (no nulas).; TIPO=1 identifica eventos de cancelación; TIPO=2 identifica eventos de reprogramación.; La fecha se entrega formateada como cadena ''dd/MM/yyyy hh:mm:ss'' (estilos 103 y 114).; Los códigos y nombres se devuelven sin espacios en blanco a la derecha (RTRIM).; Si no hay motivo asociado, el código y descripción del motivo se devuelven como cadena vacía en lugar de NULL.; Una misma cita puede aparecer dos veces si fue cancelada y además reprogramada.; Los motivos de cancelación se resuelven contra AGCAUCANA y los de reprogramación contra HCMOANULB (catálogos distintos).; El nombre del usuario responsable siempre se resuelve contra SEGusuaru, tanto para cancelación como para reprogramación.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewCitaCanceladaReprogramada';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cita médica; Cancelación de cita; Reprogramación de cita; Motivo de cancelación; Motivo de anulación de historia clínica; Usuario del sistema; Auditoría de agenda', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewCitaCanceladaReprogramada';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.AGASICITA: Cuando G.FECHCANCELA IS NOT NULL, se retorna una fila con TIPO=1 (cancelación), motivo desde AGCAUCANA y usuario desde CANCELUSU.; [RETURN_RESULT] dbo.AGASICITA: Cuando G.FECHAREPRO IS NOT NULL, se retorna una fila con TIPO=2 (reprogramación), motivo desde HCMOANULB y usuario desde CODUSUARIOREPRO.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewCitaCanceladaReprogramada';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si G.FECHCANCELA IS NOT NULL → Se incluye la cita como evento de cancelación con TIPO=1, tomando motivo del catálogo AGCAUCANA; si G.FECHAREPRO IS NOT NULL → Se incluye la cita como evento de reprogramación con TIPO=2, tomando motivo del catálogo HCMOANULB', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewCitaCanceladaReprogramada';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGASICITA; dbo.AGCAUCANA; dbo.SEGusuaru; dbo.HCMOANULB', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewCitaCanceladaReprogramada';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewCitaCanceladaReprogramada';
GO
