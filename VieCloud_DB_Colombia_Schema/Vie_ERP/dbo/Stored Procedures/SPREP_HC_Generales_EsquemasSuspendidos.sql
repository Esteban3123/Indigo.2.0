

CREATE PROCEDURE [dbo].[SPREP_HC_Generales_EsquemasSuspendidos]
(
@CodigoPaciente Varchar(25),
@NumeroFolio nChar(10),
@NumeroIngreso Char(10)
)
AS
BEGIN
	SET NOCOUNT ON;

SELECT RTRIM(G.Description) AS 'NOMBRE ESQUEMA', CASE A.MOTIVOFINALIZAR WHEN 1 THEN 'Toxicidad de uno o más medicamentos' WHEN 2 THEN 'Otros motivos médicos' WHEN 3 THEN 'Muerte' WHEN 4 THEN 'Cambio de EAPB' WHEN 5 THEN 'Decisión del usuario' 
WHEN 6 THEN 'No hay disponibilidad de medicamentos' WHEN 7 THEN 'Otros motivos administrativos' WHEN 8 THEN 'Otras causas no contempladas' END AS 'MOTIVO DE SUSPENSION', RTRIM(A.OBSERVACIONFINALIZAR) AS 'OBSERVACION DE SUSPENSION'
FROM [EHR].[HCORDQUIMIO] A With(Nolock)																								 
INNER JOIN [EHR].Schemes G With(Nolock) ON G.Id = A.SchemesId														 
WHERE A.IPCODPACI = @CodigoPaciente AND A.NUMINGRES= @NumeroIngreso AND A.NUMFOLSUS= @NumeroFolio AND A.ESTADO='4'	
	      
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta los esquemas de quimioterapia suspendidos de un paciente específico, filtrando por cédula del paciente, número de ingreso y número de folio. Combina las órdenes de quimioterapia (HCORDQUIMIO) con el catálogo de esquemas terapéuticos (Schemes) para obtener el nombre del protocolo oncológico que fue interrumpido. Devuelve el nombre del esquema de tratamiento, el motivo de suspensión (toxicidad, muerte, cambio de EAPB, decisión del usuario, falta de medicamentos, entre otros) y las observaciones clínicas registradas al momento de finalizar el tratamiento. Se utiliza en reportes de historia clínica oncológica para documentar y auditar las interrupciones o cancelaciones de ciclos de quimioterapia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_EsquemasSuspendidos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_EsquemasSuspendidos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reporta los esquemas de quimioterapia suspendidos (finalizados) de un paciente en un ingreso y folio específicos, mostrando el motivo y la observación de suspensión.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_EsquemasSuspendidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir registros en EHR.HCORDQUIMIO que coincidan con paciente, ingreso y folio de suspensión indicados; El esquema referenciado debe existir en EHR.Schemes (INNER JOIN); La orden debe tener ESTADO=''4'' (suspendido/finalizado)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_EsquemasSuspendidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran órdenes de quimioterapia con ESTADO=''4''; El motivo de suspensión se restringe a un dominio cerrado de 8 valores (1-8); fuera de ese rango se devuelve NULL; Únicamente se incluyen órdenes con esquema válido en EHR.Schemes; Es una consulta de solo lectura (NOLOCK)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_EsquemasSuspendidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Folio de suspensión; Esquema de quimioterapia; Suspensión de tratamiento oncológico; Motivo de finalización; EAPB', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_EsquemasSuspendidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] EHR.HCORDQUIMIO: Cuando ESTADO=''4'' y coinciden IPCODPACI, NUMINGRES y NUMFOLSUS, se retorna el nombre del esquema, el motivo de suspensión decodificado y la observación', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_EsquemasSuspendidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si MOTIVOFINALIZAR = 1 → Se reporta ''Toxicidad de uno o más medicamentos''; si MOTIVOFINALIZAR = 2 → Se reporta ''Otros motivos médicos''; si MOTIVOFINALIZAR = 3 → Se reporta ''Muerte''; si MOTIVOFINALIZAR = 4 → Se reporta ''Cambio de EAPB''; si MOTIVOFINALIZAR = 5 → Se reporta ''Decisión del usuario''; si MOTIVOFINALIZAR = 6 → Se reporta ''No hay disponibilidad de medicamentos''; si MOTIVOFINALIZAR = 7 → Se reporta ''Otros motivos administrativos''; si MOTIVOFINALIZAR = 8 → Se reporta ''Otras causas no contempladas''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_EsquemasSuspendidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'EHR.HCORDQUIMIO; EHR.Schemes', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_EsquemasSuspendidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_EsquemasSuspendidos';
-- GO
