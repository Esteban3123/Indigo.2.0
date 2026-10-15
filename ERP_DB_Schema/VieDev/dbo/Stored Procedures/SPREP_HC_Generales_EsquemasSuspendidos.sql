


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