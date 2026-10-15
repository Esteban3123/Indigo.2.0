
CREATE PROCEDURE [dbo].[SPREP_HC_Generales_EsquemasInformacionCAC]
(
@CodigoPaciente Varchar(25),
@NumeroFolio nChar(10),
@NumeroIngreso Char(10)
)
AS
BEGIN
	SET NOCOUNT ON;

Select 
Rtrim(Schemes.Description) +'  -  Ciclo: ' + RTRIM(Ciclos.CICLO) + '/' + Rtrim(ORD.CICLOS) AS 'NOMBRE ESQUEMA',
CASE ORD.[48] 
WHEN 1 THEN '1=Neoadyuvancia (manejo  inicial prequirúrgico)' 
WHEN 2 THEN '2=Tratamiento inicial curativo sin cirugía sugerida' 
WHEN 3 THEN '3=Adyuvancia(manejo inicial postquirúrgico)' 
WHEN 11 THEN '11=Manejo de recaída' 
WHEN 12 THEN '12=Manejo de enfermedad metastásica' 
WHEN 13 THEN '13=Manejo paliativo (sin manejo de recaída ni enfermedad metastásica' 
END AS 'UBICACION',
CASE ORD.FASEQUIMIOTERAPIA
WHEN 1 THEN 'Prefase o citorreducción inicial' 
WHEN 2 THEN 'Inducción' 
WHEN 3 THEN 'Intensificación' 
WHEN 4 THEN 'Consolidación' 
WHEN 5 THEN 'Reinducción' 
WHEN 6 THEN 'Mantenimiento' 
WHEN 7 THEN 'Mantenimiento largo o final' 
WHEN 8 THEN 'Otra Fase' 
END AS 'FASE',
ORD.[46] AS 'CANTIDAD', Ciclos.NUMINGRES, Ciclos.NUMEFOLIO
FROM [EHR].[HCORDCICLOS] Ciclos With(Nolock)																								 
INNER JOIN [EHR].Schemes Schemes With(Nolock) ON Schemes.Id = Ciclos.SchemesId
INNER JOIN [EHR].HCORDQUIMIO ORD With(Nolock) ON ORD.Id = Ciclos.IDHCORDQUIMIO
WHERE Ciclos.IPCODPACI = @CodigoPaciente AND Ciclos.NUMINGRES= @NumeroIngreso AND Ciclos.NUMEFOLIO = @NumeroFolio
      
END