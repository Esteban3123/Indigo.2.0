CREATE PROCEDURE [dbo].[SP_AD_GenerarReporteTriage]
(
	 @CentrosAtencion VARCHAR(1000), 
	 @Entidades VARCHAR(1000),
	 @ClasificacionTriage VARCHAR(10),
	 @FechaInicial DATETIME, 
	 @FechaFinal DATETIME, 
	 @Source VARCHAR(10) --999
)

AS
BEGIN

SELECT 
'Tipo de documento prestador' = (Select CASE INDTIPIDE WHEN 1 THEN 'CC' WHEN 2 THEN 'NIT' END from INEMPRESU WHERE INDCODEMP = @Source),  
'Número documento prestador' = (Select rtrim(INDNUMIDE) from INEMPRESU WHERE INDCODEMP = @Source), RTRIM(B.NOMENTIDA) AS 'Aseguradora', A.FECHINITR AS 'Fecha y hora de atención', RTRIM(C.NOMCENATE) AS 'Nombre prestador', RTRIM(E.NOMBRE) AS 'Tipo identificación paciente', A.IPCODPACI AS 'Número documento paciente',
RTRIM(D.IPNOMCOMP) AS 'Nombre paciente', D.IPFECNACI AS 'Fecha de nacimiento', CASE D.IPSEXOPAC WHEN 1 THEN 'Masculino' WHEN 2 THEN 'Femenino' END AS 'Género paciente', A.TRIANUMER AS 'Número triage', RTRIM(F.TRIANOMCA) AS 'Categoria dx sindromático', 
CASE A.TRIAGECLA WHEN 1 THEN 'REANIMACIÓN' WHEN 2 THEN 'EMERGENCIA' WHEN 3 THEN 'URGENCIA MÉDICA' WHEN 4 THEN 'URGENCIA DIFERIDA' WHEN 5 THEN 'NO URGENTE' END AS 'Clasificación triage', D.IPTELMOVI AS 'Teléfono contacto (celular)', D.IPTELEFON AS 'Teléfono contacto (fijo)'
FROM dbo.ADTRIAGEU A 
INNER JOIN dbo.INENTIDAD B ON A.CODENTIDA = B.CODENTIDA 
INNER JOIN dbo.ADCENATEN C ON A.CODCENATE = C.CODCENATE 
INNER JOIN dbo.INPACIENT D On A.IPCODPACI = D.IPCODPACI 
INNER JOIN dbo.ADTIPOIDENTIFICA E ON D.IPTIPODOC = E.CODIGO 
INNER JOIN dbo.ADCATTRIU F ON A.TRIACATEG = F.TRIACATEG 
WHERE A.CODCENATE IN (SELECT Value FROM dbo.SplitString(@CentrosAtencion)) AND A.CODENTIDA IN (SELECT Value FROM dbo.SplitString(@Entidades)) AND A.TRIAGECLA IN (SELECT Value FROM dbo.SplitString(@ClasificacionTriage)) AND A.FECHINITR BETWEEN @FechaInicial AND @FechaFinal

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte de triage de urgencias para un rango de fechas, centros de atención, entidades aseguradoras y clasificaciones de triage seleccionados. Consolida información del registro de triage (ADTRIAGEU) con datos del paciente como cédula, nombre, fecha de nacimiento y género (INPACIENT), el tipo de documento de identificación (ADTIPOIDENTIFICA), la aseguradora o EPS (INENTIDAD), el centro de atención o sede (ADCENATEN) y la categoría diagnóstica sindromática del triage (ADCATTRIU). Incluye también los datos del prestador de salud (NIT o CC e identificación de la institución desde INEMPRESU) y presenta la clasificación clínica del triage en lenguaje legible (Reanimación, Emergencia, Urgencia Médica, Urgencia Diferida, No Urgente). Se utiliza para reportería regulatoria, auditoría y gestión operativa del servicio de urgencias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AD_GenerarReporteTriage';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AD_GenerarReporteTriage';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte tabular de atenciones de triage de urgencias filtrado por centros de atención, aseguradoras, clasificación de triage y rango de fechas, incluyendo datos del prestador, paciente y categoría sindromática.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_GenerarReporteTriage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las listas de centros, entidades y clasificaciones se reciben como cadenas delimitadas parseables por dbo.SplitString; Debe existir un registro en INEMPRESU con código igual al @Source para obtener los datos del prestador; Las claves foráneas (entidad, centro, paciente, tipo de documento, categoría de triage) deben existir para que el registro aparezca en el resultado; @FechaInicial y @FechaFinal deben definir un rango válido sobre FECHINITR', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_GenerarReporteTriage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen registros de triage cuyo centro, entidad y clasificación estén dentro de las listas recibidas; El rango de fechas se evalúa contra la fecha/hora de inicio del triage (FECHINITR) inclusivo en ambos extremos; Los datos del prestador provienen del registro de INEMPRESU identificado por @Source; Los códigos numéricos de tipo de identificación, género y clasificación de triage siempre se traducen a etiquetas legibles; Solo se devuelven triages que tengan paciente, entidad, centro, tipo de documento y categoría de triage existentes (INNER JOIN)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_GenerarReporteTriage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Triage de urgencias; Clasificación de triage (Reanimación, Emergencia, Urgencia médica, Urgencia diferida, No urgente); Categoría diagnóstica sindromática; Prestador de salud; Aseguradora/Entidad; Centro de atención; Paciente; Tipo de identificación', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_GenerarReporteTriage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve un conjunto de resultados con datos del prestador, aseguradora, centro, paciente y triage cuando A.CODCENATE, A.CODENTIDA y A.TRIAGECLA están en las listas parametrizadas y A.FECHINITR está entre @FechaInicial y @FechaFinal', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_GenerarReporteTriage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si INDTIPIDE del prestador = 1 → Se reporta tipo documento como ''CC'' else Si = 2 se reporta ''NIT''; si IPSEXOPAC = 1 → Género se reporta como ''Masculino'' else Si = 2 se reporta ''Femenino''; si TRIAGECLA del registro (1..5) → Se traduce a etiqueta: 1=REANIMACIÓN, 2=EMERGENCIA, 3=URGENCIA MÉDICA, 4=URGENCIA DIFERIDA, 5=NO URGENTE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_GenerarReporteTriage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.SplitString', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_GenerarReporteTriage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADTRIAGEU; dbo.INENTIDAD; dbo.ADCENATEN; dbo.INPACIENT; dbo.ADTIPOIDENTIFICA; dbo.ADCATTRIU; dbo.INEMPRESU', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_GenerarReporteTriage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_GenerarReporteTriage';
-- GO
