
CREATE PROCEDURE [dbo].[SPCH_PacienteServiciosMobile]
(
@Paciente Varchar(25),
@Ingreso Char(20)
)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT RTRIM(A.CODSERIPS) as Codigo,rtrim(b.DESSERIPS) as Servicio,'Laboratorios' as Tipo
	FROM HCORDLABO A INNER JOIN
	INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS
	WHERE A.IPCODPACI=@paciente and a.NUMINGRES=@ingreso
	UNION ALL
	
	SELECT RTRIM(A.CODSERIPS) as Codigo,rtrim(b.DESSERIPS) as Servicio,'Imagenes' as Tipo
	FROM HCORDIMAG A INNER JOIN
	INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS
	WHERE A.IPCODPACI=@paciente and a.NUMINGRES=@ingreso
	UNION ALL

	SELECT RTRIM(A.CODSERIPS) as Codigo,rtrim(b.DESSERIPS) as Servicio,'Procedimientos' as Tipo
	FROM HCORDPATO A INNER JOIN
	INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS
	WHERE A.IPCODPACI=@paciente and a.NUMINGRES=@ingreso
	UNION ALL

	SELECT RTRIM(A.CODSERIPS) as Codigo,rtrim(b.DESSERIPS) as Servicio,'Procedimientos' as Tipo
	FROM HCORDPROQ A INNER JOIN
	INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS
	WHERE A.IPCODPACI=@paciente and a.NUMINGRES=@ingreso

	UNION ALL
		SELECT RTRIM(A.CODSERIPS) as Codigo,rtrim(b.DESSERIPS) as Servicio,'Procedimientos' as Tipo
	FROM HCORDPRON A INNER JOIN
	INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS
	WHERE A.IPCODPACI=@paciente and a.NUMINGRES=@ingreso

		UNION ALL
		SELECT RTRIM(A.CODPRODUC) as Codigo,rtrim(b.DESPRODUC) as Servicio,'Medicamentos' as Tipo
	FROM HCPRESCRA A INNER JOIN
	IHLISTPRO B ON A.CODPRODUC=B.CODPRODUC
	WHERE A.IPCODPACI=@paciente and a.NUMINGRES=@ingreso
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento almacenado utilizado por la aplicación móvil para consultar todos los servicios y atenciones ordenadas a un paciente durante un ingreso específico. Recibe como parámetros la cédula o identificación del paciente y el número de ingreso, y devuelve un listado consolidado que agrupa exámenes de laboratorio, imágenes diagnósticas (radiología, ecografías, tomografías), procedimientos de patología, procedimientos quirúrgicos, otros procedimientos clínicos y medicamentos recetados, cada uno con su código CUPS y descripción del servicio. Integra información de las órdenes médicas de la historia clínica (HCORDLABO, HCORDIMAG, HCORDPATO, HCORDPROQ, HCORDPRON) y de las prescripciones de medicamentos (HCPRESCRA), cruzando con los catálogos de servicios CUPS (INCUPSIPS) y de productos farmacéuticos (IHLISTPRO) para obtener los nombres legibles de cada servicio. Es el punto de consulta central para que el paciente o el profesional de salud visualice desde el móvil el resumen completo de servicios ordenados durante una hospitalización o atención ambulatoria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_PacienteServiciosMobile';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_PacienteServiciosMobile';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en un único listado los servicios ordenados a un paciente durante un ingreso (laboratorios, imágenes, procedimientos y medicamentos) para consumo desde aplicación móvil.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_PacienteServiciosMobile';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se requiere identificador de paciente y número de ingreso para filtrar las órdenes.; Los códigos de servicio deben existir en el catálogo CUPS (INCUPSIPS) y los productos en el catálogo de medicamentos (IHLISTPRO) para que aparezcan con descripción.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_PacienteServiciosMobile';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los servicios se clasifican en cuatro tipos fijos: ''Laboratorios'', ''Imagenes'', ''Procedimientos'' y ''Medicamentos''.; Las órdenes de patología (HCORDPATO), quirúrgicas (HCORDPROQ) y no quirúrgicas (HCORDPRON) se reportan bajo el mismo tipo ''Procedimientos''.; Solo se incluyen órdenes/prescripciones cuyo código tenga correspondencia en el catálogo respectivo (INNER JOIN).; El resultado siempre se filtra por la combinación paciente+ingreso (no se mezclan ingresos).; Códigos y descripciones se devuelven sin espacios a la derecha (RTRIM).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_PacienteServiciosMobile';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso/Episodio asistencial; Órdenes de laboratorio; Órdenes de imágenes diagnósticas; Órdenes de procedimientos (patología, quirúrgicos y no quirúrgicos); Prescripción de medicamentos; Catálogo CUPS; Servicios IPS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_PacienteServiciosMobile';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve filas tipificadas como ''Laboratorios'' desde HCORDLABO cuando coinciden paciente e ingreso.; [RETURN_RESULT] resultset: Devuelve filas tipificadas como ''Imagenes'' desde HCORDIMAG cuando coinciden paciente e ingreso.; [RETURN_RESULT] resultset: Devuelve filas tipificadas como ''Procedimientos'' unificando HCORDPATO, HCORDPROQ y HCORDPRON cuando coinciden paciente e ingreso.; [RETURN_RESULT] resultset: Devuelve filas tipificadas como ''Medicamentos'' desde HCPRESCRA unidas al maestro de productos cuando coinciden paciente e ingreso.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_PacienteServiciosMobile';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDLABO; dbo.HCORDIMAG; dbo.HCORDPATO; dbo.HCORDPROQ; dbo.HCORDPRON; dbo.HCPRESCRA; dbo.INCUPSIPS; dbo.IHLISTPRO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_PacienteServiciosMobile';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_PacienteServiciosMobile';
-- GO
