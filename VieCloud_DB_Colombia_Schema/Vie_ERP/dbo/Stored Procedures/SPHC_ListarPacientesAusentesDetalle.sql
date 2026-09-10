
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SPHC_ListarPacientesAusentesDetalle] 
@UnidadFuncional char(10),
@FechaInicial datetime,
@FechaFinal datetime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

SELECT DISTINCT A.IPCODPACI+CAST(FECAUSENT AS CHAR) AS Llave,A.CONESTADO AS Estado,A.FECAUSENT AS FechaAusente,
rtrim(A.PROPRILLA)+' - '+rtrim(C.NOMMEDICO) AS ProfesionalPrimerLlamadoT,
rtrim(A.PROSEGLLA)+' - '+rtrim(D.NOMMEDICO) AS ProfesionalSegundoLlamadoT,
rtrim(A.PROTERLLA)+' - '+rtrim(E.NOMMEDICO) AS ProfesionalTercerLlamadoT,
rtrim(B.PROPRILLA)+' - '+rtrim(F.NOMMEDICO) AS ProfesionalPrimerLlamadoA,
rtrim(B.PROSEGLLA)+' - '+rtrim(G.NOMMEDICO) AS ProfesionalSegundoLlamadoA,
rtrim(B.PROTERLLA)+' - '+rtrim(H.NOMMEDICO) AS ProfesionalTercerLlamadoA,
cast('' AS char(150)) AS PriLlamado,cast('' AS char(150)) AS SegLlamado,cast('' AS char(150)) AS TerLlamado
FROM ADCONTURG A 
LEFT OUTER JOIN ADTRIAGEU B ON A.CODCONCEC =B.CODCONCEC
LEFT OUTER JOIN INPROFSAL C ON A.PROPRILLA= C.CODPROSAL
LEFT OUTER JOIN INPROFSAL D ON A.PROSEGLLA = D.CODPROSAL  
LEFT OUTER JOIN INPROFSAL E ON A.PROTERLLA = E.CODPROSAL 
LEFT OUTER JOIN INPROFSAL F ON B.PROPRILLA= F.CODPROSAL
LEFT OUTER JOIN INPROFSAL G ON B.PROSEGLLA = G.CODPROSAL  
LEFT OUTER JOIN INPROFSAL H ON B.PROTERLLA = H.CODPROSAL
WHERE 
A.CONESTADO IN ('2','6') AND A.UFUCODIGO= @UnidadFuncional AND A.FECAUSENT BETWEEN @FechaInicial AND @FechaFinal
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista el detalle de pacientes ausentes en urgencias para una unidad funcional y rango de fechas dado. Combina el registro de contactos y llamados (ADCONTURG) con el triage de urgencias (ADTRIAGEU) para mostrar, por cada paciente que no se presentó o fue dado por ausente, los tres intentos de llamado realizados tanto desde el control de contacto como desde el triage, incluyendo el código y nombre completo de cada profesional que efectuó el llamado (obtenido del maestro INPROFSAL). Filtra únicamente los registros con estado de ausencia activa (estados 2 y 6) y se usa para reportería y auditoría del proceso de convocatoria a pacientes que esperan atención en urgencias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarPacientesAusentesDetalle';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarPacientesAusentesDetalle';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista el detalle de pacientes marcados como ausentes en urgencias dentro de una unidad funcional y rango de fechas, mostrando los profesionales asignados a los tres llamados tanto en la consulta como en el triage.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAusentesDetalle';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe recibirse una unidad funcional válida y un rango de fechas (inicial y final) para acotar las ausencias.; Las tablas ADCONTURG, ADTRIAGEU e INPROFSAL deben estar disponibles y vinculadas por los códigos de consulta y profesional.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAusentesDetalle';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran consultas de urgencias cuyo estado sea ''2'' o ''6'' (interpretado como estados de ausencia).; El detalle se filtra siempre por una unidad funcional específica y un rango de fechas de ausencia.; Cada fila combina los profesionales de primer/segundo/tercer llamado tanto del registro de urgencias como del triage asociado.; Se construye una llave compuesta uniendo el código de paciente con la fecha de ausencia.; Las relaciones con profesionales y triage se hacen mediante LEFT JOIN, por lo que la ausencia de triage o de algún profesional no excluye el registro.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAusentesDetalle';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente ausente; urgencias; triage; profesional de salud; llamados (primero/segundo/tercer llamado); unidad funcional', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAusentesDetalle';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.ADCONTURG: Cuando A.CONESTADO IN (''2'',''6'') AND UFUCODIGO = @UnidadFuncional AND FECAUSENT BETWEEN @FechaInicial AND @FechaFinal, devuelve el detalle de los pacientes ausentes con sus profesionales de primer, segundo y tercer llamado (de urgencias y de triage).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAusentesDetalle';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADCONTURG; dbo.ADTRIAGEU; dbo.INPROFSAL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAusentesDetalle';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAusentesDetalle';
-- GO
