CREATE PROCEDURE [dbo].[SPREP_HC_Generales_EsquemasModificados]
(
@CodigoPaciente Varchar(25),
@NumeroFolio nChar(10),
@NumeroIngreso Char(10)
)
AS
BEGIN
	SET NOCOUNT ON;

SELECT 
Rtrim(Schemes.Description) +'  -  Ciclo: ' + RTRIM(Ciclos.CICLO) + '/' + Rtrim(ORD.CICLOS) AS 'NOMBRE ESQUEMA',
RTRIM(MO.DESMOTANU) AS 'MOTIVO DE MODIFICACION', 
RTRIM(Ciclos.OBSERVACIONMOD) AS 'OBSERVACION DE LA MODIFICACION'
FROM [EHR].[HCORDCICLOS] Ciclos With(Nolock)																								 
INNER JOIN [EHR].Schemes Schemes With(Nolock) ON Schemes.Id = Ciclos.SchemesId
INNER JOIN [EHR].HCORDQUIMIO ORD With(Nolock) ON ORD.Id = Ciclos.IDHCORDQUIMIO
INNER JOIN [dbo].HCMOANULB MO With(Nolock) ON MO.CODMOTANU = Ciclos.IDHCMOANULB
WHERE Ciclos.IPCODPACI = @CodigoPaciente AND Ciclos.NUMINGRES= @NumeroIngreso AND Ciclos.NUMEFOLIO = @NumeroFolio AND Ciclos.IDHCMOANULB IS NOT NULL	
      
END