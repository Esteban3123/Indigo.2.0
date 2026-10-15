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