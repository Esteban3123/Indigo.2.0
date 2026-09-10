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
, ine.NOMENTIDA as 'Entidad', car.Name  as 'Grupo atención', RTRIM(PROF.NOMMEDICO) AS 'ProfesionalDieta'
FROM dbo.CHREGESTA A 
INNER JOIN  dbo.INPacient B ON A.IPCODPACI=B.IPCODPACI 
INNER JOIN dbo.CHCAMASHO C ON A.CODICAMAS=C.CODICAMAS 
INNER JOIN dbo.CHPARAMET AS H ON C.CODCENATE = H.CODCENATE 
INNER JOIN dbo.ADINGRESO AS I ON A.NUMINGRES = I.NUMINGRES
INNER JOIN dbo.INENTIDAD  AS ine ON I.CODENTIDA = ine.CODENTIDA
INNER JOIN Contract.CareGroup AS car ON I.GENCAREGROUP = car.Id 
OUTER APPLY
(
SELECT TOP(1) OBSERVACI,CODPROSAL from CHREGDIET Y WHERE Y.IPCODPACI = A.IPCODPACI AND Y.NUMINGRES = A.NUMINGRES
)CHDIET
OUTER APPLY(
select STUFF((SELECT RTRIM(CHAR(10) + x.DESTIPDIE)+'.' from dbo.CHREGDIET AS DIET INNER JOIN CHTIPDIET X ON DIET.CODTIPDIE = X.CODTIPDIE WHERE DIET.IPCODPACI = a.IPCODPACI AND NUMINGRES = a.NUMINGRES AND (X.BreastMilk IS NULL OR X.BreastMilk !=1) FOR XML PATH('')),1,1,'') as tipoDieta 
)DIETA
--INNER JOIN dbo.INENTIDAD AS J ON I.CODENTIDA = J.CODENTIDA
LEFT OUTER JOIN dbo.INDIAGNOP D ON A.IPCODPACI=D.IPCODPACI AND D.CODDIAPRI = 1 AND A.NUMINGRES = D.NUMINGRES 
LEFT OUTER JOIN dbo.INDIAGNOS AS E ON D.CODDIAGNO = E.CODDIAGNO
LEFT JOIN MedicalDiet.DietControlNursing as Diet ON A.ID = Diet.IdCHREGESTA AND Diet.FoodKind = @TipoComida AND cast(Diet.CreationDate as DATE) = CAST(@fechafiltro as DATE)
LEFT JOIN INPROFSAL AS PROF ON PROF.CODPROSAL = CHDIET.CODPROSAL 
WHERE A.REGESTADO= 1 AND C.CODCENATE=@CentroAtencion AND C.UFUCODIGO=@UnidadFuncional  order by  c.CODICAMAS ASC

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista el control de dietas hospitalarias de los pacientes actualmente internados en una unidad funcional y centro de atención específicos. Para cada paciente activo en cama, consolida: identificación y nombre del paciente, número de ingreso, cama asignada, diagnóstico principal (CIE-10), tipo de dieta prescrita (excluyendo leche materna), observaciones del médico y de enfermería, entidad aseguradora (EPS/pagador), grupo de atención del contrato y profesional que indicó la dieta. Filtra por unidad funcional, centro de atención, tipo de comida y fecha, y clasifica a los pacientes en ''Con dieta asignada'' o ''Sin dieta asignada'' para facilitar el seguimiento en pantalla o impresión. Se usa en el módulo de hospitalización para que nutrición y enfermería gestionen y auditen la alimentación de los pacientes hospitalizados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_ListarControlDietas';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_ListarControlDietas';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista pacientes hospitalizados activos en una unidad funcional con su dieta asignada, observaciones médicas y de enfermería, diagnóstico principal y datos administrativos para el control diario de dietas por tipo de comida.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarControlDietas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El centro de atención y unidad funcional deben existir en CHCAMASHO; Las estancias deben estar activas (REGESTADO=1); Debe existir relación de ingreso en ADINGRESO con entidad y grupo de atención válidos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarControlDietas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran estancias activas (REGESTADO=1); Las dietas tipo leche materna (BreastMilk=1) no se incluyen en el listado consolidado de dietas del paciente; Si el paciente no tiene dietas registradas se muestra ''Sin Asignar''; El diagnóstico mostrado siempre es el marcado como principal (CODDIAPRI=1); Los días de estancia se calculan como diferencia en días entre fecha de inicio y fecha actual del sistema más uno; La observación de enfermería se filtra por tipo de comida y por la fecha exacta de creación igual a la fecha filtrada', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarControlDietas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente hospitalizado; Estancia/ingreso; Cama hospitalaria; Dieta del paciente; Tipo de comida; Diagnóstico principal (CIE-10); Observación médica; Observación de enfermería; Entidad/aseguradora; Grupo de atención; Leche materna (BreastMilk); Profesional de salud que ordena la dieta', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarControlDietas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve únicamente estancias con REGESTADO=1 en el centro de atención y unidad funcional indicados, ordenadas por código de cama ascendente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarControlDietas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @TipoUnidad = ''1'' → Se retorna la hora mínima configurada (HORMINSOL) del parámetro del centro de atención else Se retorna 0 como hora mínima; si LEN(RTRIM(DIETA.tipoDieta)) > 1 → Marca ''Imprimir Dieta'' = 1 (paciente con dieta asignada se imprime) else Marca ''Imprimir Dieta'' = 0; si DIETA.tipoDieta <> ''Sin Asignar'' → Clasifica la fila en ''1.Pacientes Asignados'' else Clasifica la fila en ''2.Pacientes No Asignados''; si En la concatenación de dietas: X.BreastMilk IS NULL OR X.BreastMilk != 1 → Incluye el tipo de dieta en la lista concatenada else Excluye dietas marcadas como leche materna; si INDIAGNOP con CODDIAPRI = 1 → Toma el diagnóstico como principal del paciente para el ingreso', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarControlDietas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.ConsultarDietas; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarControlDietas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.CHREGESTA; dbo.INPacient; dbo.CHCAMASHO; dbo.CHPARAMET; dbo.ADINGRESO; dbo.INENTIDAD; Contract.CareGroup; dbo.CHREGDIET; dbo.CHTIPDIET; dbo.INDIAGNOP; dbo.INDIAGNOS; MedicalDiet.DietControlNursing; dbo.INPROFSAL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarControlDietas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarControlDietas';
-- GO
