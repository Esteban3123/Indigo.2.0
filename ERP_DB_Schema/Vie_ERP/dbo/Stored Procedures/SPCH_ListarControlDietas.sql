CREATE PROCEDURE [dbo].[SPCH_ListarControlDietas]
(
@TipoUnidad Char(2),
@CentroAtencion Char(10),
@UnidadFuncional Char(10),
@TipoComida tinyint,
@fechafiltro date
)
AS
BEGIN
	SET NOCOUNT ON;
SELECT A.IPCODPACI AS 'Identificacion', RTRIM(B.IPNOMCOMP) AS 'Nombre Paciente', RTRIM(A.NUMINGRES) AS Ingreso, 
B.IPFECNACI AS FechaNac, RTRIM(C.DESCCAMAS) AS Cama, C.CODICAMAS as 'Codigo Cama', RTRIM(COALESCE(NULLIF(E.NOMDIAGNO,''),'')) AS 'Diagnostico Principal', E.CODDIAGNO As 'Codigo Diagnostico', 
isnull(RTRIM(DIETA.tipoDieta), 'Sin Asignar') AS 'Tipo de Dieta', isnull(CHDIET.OBSERVACI, '') as 'Obs. Medico', ISNULL(Diet.NurseObservation, '') as 'Obs. Enfermeria',
A.FECINIEST AS 'Fecha Orden', A.ID as 'IdEstancia',
DATEDIFF("d",A.FECINIEST,[Common].[GETDATE]())+1 AS Dias, CASE @TipoUnidad WHEN '1' THEN H.HORMINSOL ELSE 0 end AS 'Hora Minima', cast(A.FECINIEST as DATETIME) AS 'Fecha Hospitalizacion', 
cast(CASE WHEN LEN(RTRIM(DIETA.tipoDieta)) > 1 THEN 1 ELSE 0 END as BIT)  AS 'Imprimir Dieta', ' ' AS Bloqueado,'' AS Edad,
cast(A.FECINIEST as DATETIME) AS 'Fecha Ingreso',  dbo.ConsultarDietas(A.IPCODPACI, A.NUMINGRES, 2)as CodigoDieta, CAST(0 as BIT) as 'CambioDietas', Case WHEN DIETA.tipoDieta <> 'Sin Asignar' THEN '1.Pacientes Asignados' ELSE '2.Pacientes No Asignados' END as 'FiltroRejilla'
, ine.NOMENTIDA as 'Entidad', car.Name  as 'Grupo atención'
FROM dbo.CHREGESTA A 
INNER JOIN  dbo.INPacient B ON A.IPCODPACI=B.IPCODPACI 
INNER JOIN dbo.CHCAMASHO C ON A.CODICAMAS=C.CODICAMAS 
INNER JOIN dbo.CHPARAMET AS H ON C.CODCENATE = H.CODCENATE 
INNER JOIN dbo.ADINGRESO AS I ON A.NUMINGRES = I.NUMINGRES
INNER JOIN dbo.INENTIDAD  AS ine ON I.CODENTIDA = ine.CODENTIDA
INNER JOIN Contract.CareGroup AS car ON I.GENCAREGROUP = car.Id 
OUTER APPLY
(
SELECT TOP(1) OBSERVACI from CHREGDIET Y WHERE Y.IPCODPACI = A.IPCODPACI AND Y.NUMINGRES = A.NUMINGRES
)CHDIET
OUTER APPLY(
select STUFF((SELECT RTRIM(CHAR(10) + x.DESTIPDIE)+'.' from dbo.CHREGDIET AS DIET INNER JOIN CHTIPDIET X ON DIET.CODTIPDIE = X.CODTIPDIE WHERE DIET.IPCODPACI = a.IPCODPACI AND NUMINGRES = a.NUMINGRES AND (X.BreastMilk IS NULL OR X.BreastMilk !=1) FOR XML PATH('')),1,1,'') as tipoDieta 
)DIETA
--INNER JOIN dbo.INENTIDAD AS J ON I.CODENTIDA = J.CODENTIDA
LEFT OUTER JOIN dbo.INDIAGNOP D ON A.IPCODPACI=D.IPCODPACI AND D.CODDIAPRI = 1 AND A.NUMINGRES = D.NUMINGRES 
LEFT OUTER JOIN dbo.INDIAGNOS AS E ON D.CODDIAGNO = E.CODDIAGNO
LEFT JOIN MedicalDiet.DietControlNursing as Diet ON A.ID = Diet.IdCHREGESTA AND Diet.FoodKind = @TipoComida AND cast(Diet.CreationDate as DATE) = CAST(@fechafiltro as DATE)
WHERE A.REGESTADO= 1 AND C.CODCENATE=@CentroAtencion AND C.UFUCODIGO=@UnidadFuncional  order by  c.CODICAMAS ASC

END