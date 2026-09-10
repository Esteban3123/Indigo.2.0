
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_ProceTriage] 
	-- Add the parameters for the stored procedure here
	@fecha_ini date,
	@fecha_fin date
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT        
T.TRIANUMER AS 'CONSECUTIVO DE REGISTRO(NUMEO DE TRIAGE)',
'TIPO DE IDENTIFICACION' = CASE INP.IPTIPODOC
WHEN '1' THEN 'Cédula de Ciudadanía'
WHEN '2' THEN 'Cédula de Extranjería'
WHEN '3' THEN 'Tarjeta de Identidad'
WHEN '4' THEN 'Registro Civil'
WHEN '5' THEN 'Pasporte'
WHEN '6' THEN 'Adulto Sin Identificación'
ELSE 'Menor Sin Identificación'
END,
T.IPCODPACI AS 'NUMERO DE IDENTIFICACION',
CAST(INP.IPFECNACI AS date) AS 'FECHA DE NACIMINETO',
'SEXO PACIENTE'= CASE INP.IPSEXOPAC
         WHEN '1' THEN 'HOMBRE'
		 WHEN '2' THEN 'MUJER'
         ELSE 'INDEFINIDO'
		 end,
INP.IPPRIAPEL AS 'PRIMER APELLIDO',
INP.IPSEGAPEL as 'SEGUNDO APELLIDO',
INP.IPPRINOMB as 'PRIMER NOMBRE',
INP.IPSEGNOMB as 'SEGUNDO NOMBRE',
T.CODENTIDA AS 'CODIGO DE LA EAPB',
INTE.NOMENTIDA AS 'NOMBRE DE LA EAPB',
CAST(T.TRIAFECHA AS DATE) AS 'FECHA DE CLASIFICACION TRIAGE',
CAST(T.TRIAFECHA AS time) AS 'HORA DE CLASIFICACION TRIAGE',
CAST(T.TRIFECCON AS DATE) AS 'FECHA DE ATENCION EN CONSULTA URGENCIAS',
CAST(T.TRIFECCON AS time) AS 'HORA DE ATENCION EN CONSULTA URGENCIAS'
                       
FROM            ADTRIAGEU AS T INNER JOIN
                         INPROFSAL AS P ON T.CODPROSAL = P.CODPROSAL LEFT OUTER JOIN
                         ADCONTURG AS C ON T.CODCONCEC = C.CODCONCEC INNER JOIN
                         INUNIFUNC AS UF ON C.UFUCODIGO = UF.UFUCODIGO INNER JOIN
                         INPACIENT AS INP ON T.IPCODPACI = INP.IPCODPACI INNER JOIN
						 INENTIDAD AS INTE ON INTE.CODENTIDA = T.CODENTIDA
WHERE        (T.TRIAFECHA BETWEEN @fecha_ini AND @fecha_fin) 
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera el reporte de triage de urgencias para un rango de fechas determinado. Consolida información del paciente (identificación, tipo de documento, fecha de nacimiento, sexo y nombre completo), la clasificación de triage (número consecutivo, fecha y hora de clasificación), la atención en consulta de urgencias (fecha y hora de atención) y la entidad aseguradora o EAPB a la que pertenece el paciente. Combina los registros de triage (ADTRIAGEU) con el maestro de pacientes (INPACIENT), el directorio de entidades (INENTIDAD), el maestro de profesionales (INPROFSAL), el control de llamados de urgencias (ADCONTURG) y el catálogo de unidades funcionales (INUNIFUNC). Se utiliza principalmente para auditoría, seguimiento de tiempos de atención en urgencias y reportería operativa del servicio de triage.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ProceTriage';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ProceTriage';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte de clasificaciones de triage de urgencias en un rango de fechas, incluyendo datos demográficos del paciente, EAPB y tiempos de atención.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ProceTriage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El rango de fechas (inicio y fin) debe estar definido para filtrar TRIAFECHA; Cada triage debe tener paciente, profesional de salud y entidad (EAPB) existentes para satisfacer los INNER JOIN', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ProceTriage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen registros de triage cuya fecha esté en el rango parametrizado; Solo se reportan triages que tengan paciente, profesional de salud y entidad (EAPB) válidos por el INNER JOIN; La unidad funcional y la consulta de urgencias se incluyen sólo si existen (LEFT JOIN sobre ADCONTURG); La fecha y hora de clasificación y atención se separan a partir de un mismo timestamp (TRIAFECHA y TRIFECCON); Los códigos de tipo de documento y sexo se traducen siempre a etiquetas legibles, con valor por defecto si el código no es reconocido', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ProceTriage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Triage de urgencias; Paciente; Tipo de identificación; Sexo del paciente; EAPB (entidad aseguradora); Profesional de salud; Unidad funcional; Consulta de urgencias; Clasificación de triage', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ProceTriage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultado SELECT: Cuando TRIAFECHA está entre la fecha inicial y final, se retorna el listado de triages con datos del paciente, EAPB y tiempos de clasificación/atención', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ProceTriage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si IPTIPODOC = ''1'' → Se etiqueta tipo de identificación como ''Cédula de Ciudadanía''; si IPTIPODOC = ''2'' → Se etiqueta como ''Cédula de Extranjería''; si IPTIPODOC = ''3'' → Se etiqueta como ''Tarjeta de Identidad''; si IPTIPODOC = ''4'' → Se etiqueta como ''Registro Civil''; si IPTIPODOC = ''5'' → Se etiqueta como ''Pasporte'' (Pasaporte); si IPTIPODOC = ''6'' → Se etiqueta como ''Adulto Sin Identificación'' else Se etiqueta como ''Menor Sin Identificación'' para cualquier otro valor; si IPSEXOPAC = ''1'' → Sexo del paciente se reporta como ''HOMBRE''; si IPSEXOPAC = ''2'' → Sexo del paciente se reporta como ''MUJER'' else Sexo se reporta como ''INDEFINIDO'' para cualquier otro valor', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ProceTriage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADTRIAGEU; dbo.INPROFSAL; dbo.ADCONTURG; dbo.INUNIFUNC; dbo.INPACIENT; dbo.INENTIDAD', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ProceTriage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ProceTriage';
-- GO
