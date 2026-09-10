
CREATE PROCEDURE [dbo].[SP_AD_ListarIngresosRelacionados]
(
@Paciente varchar(25),
@NumerHoras int
)
AS
BEGIN
	SET NOCOUNT ON;
SELECT 'de Ingreso' as Reingreso, CAST(0 AS BIT) AS Sel, CONVERT(VARCHAR(30),FECHISPAC) AS FechaHistoria,RTRIM(NOMDIAGNO) AS Diagnostico,RTRIM(NOMMEDICO) AS Profesional,RTRIM(DESESPECI) AS Especialidad,A.NUMINGRES AS Ingreso,A.NUMEFOLIO AS Folio, null as NumeroTriage
FROM dbo.HCHISPACA A 
INNER JOIN dbo.INDIAGNOS B ON A.CODDIAGNO=B.CODDIAGNO 
INNER JOIN dbo.INPROFSAL C ON A.CODPROSAL=C.CODPROSAL 
INNER JOIN dbo.INESPECIA D ON C.CODESPEC1=D.CODESPECI 
INNER JOIN dbo.HCREGEGRE E ON E.NUMINGRES = A.NUMINGRES AND E.NUMEFOLIO = A.NUMEFOLIO
INNER JOIN dbo.ADINGRESO I ON I.NUMINGRES = A.NUMINGRES 
INNER JOIN dbo.ADTRIAGEU M ON M.NUMINGRES = A.NUMINGRES and UFUINGMED  is not null ---Saber si el Inbgreso Previo tubo Relación con Triage y ademas si Folio de historia clínica
WHERE A.IPCODPACI=@Paciente AND DATEDIFF(hour,FECALTPAC,[Common].[GETDATE]())<=@NumerHoras  
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los ingresos o episodios de atención previos relacionados a un paciente, filtrando solo aquellos ocurridos dentro de un número de horas configurado antes del alta. Combina la historia clínica del paciente con el diagnóstico CIE-10, el profesional de salud tratante y su especialidad, el registro de egreso hospitalario y el triage de urgencias, devolviendo únicamente los ingresos que tuvieron triage asociado y unidad funcional médica registrada. Se utiliza para identificar reingresos o atenciones anteriores recientes de un paciente (por cédula o código de paciente), apoyando decisiones clínicas y administrativas como detección de reingresos tempranos o continuidad de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AD_ListarIngresosRelacionados';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AD_ListarIngresosRelacionados';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los ingresos previos de un paciente, con egreso registrado y vínculo con triage de urgencias, ocurridos dentro de una ventana de horas hacia atrás desde la fecha actual, para evaluar posibles reingresos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarIngresosRelacionados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el paciente identificado y tener registros en HCHISPACA con diagnóstico, profesional y especialidad válidos en sus catálogos.; Los ingresos relacionados deben tener registro de egreso en HCREGEGRE y registro de triage en ADTRIAGEU con UFUINGMED no nulo.; La función [Common].[GETDATE]() debe estar disponible para calcular la ventana temporal.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarIngresosRelacionados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan ingresos previos del paciente que ya cuenten con egreso registrado (HCREGEGRE).; Solo se incluyen ingresos cuya historia clínica esté asociada a un triage de urgencias con UFUINGMED no nulo (es decir, triage que derivó en ingreso médico).; Solo se consideran ingresos cuya fecha de alta esté dentro de la ventana de horas indicada respecto a la fecha/hora actual del sistema.; El campo Reingreso siempre se retorna con el literal ''de Ingreso'' y Sel siempre se inicializa en 0 (no seleccionado).; NumeroTriage se retorna siempre nulo en este listado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarIngresosRelacionados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Reingreso; Ingreso; Folio de historia clínica; Diagnóstico; Profesional de salud; Especialidad; Egreso; Triage de urgencias; Paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarIngresosRelacionados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCHISPACA: Cuando IPCODPACI coincide con el paciente y DATEDIFF(hour, FECALTPAC, GETDATE()) ≤ horas dadas, y existen egreso (HCREGEGRE), ingreso (ADINGRESO) y triage con UFUINGMED no nulo (ADTRIAGEU), entonces se retorna la fila con datos de historia, diagnóstico, profesional, especialidad, ingreso y folio.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarIngresosRelacionados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHISPACA; dbo.INDIAGNOS; dbo.INPROFSAL; dbo.INESPECIA; dbo.HCREGEGRE; dbo.ADINGRESO; dbo.ADTRIAGEU', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarIngresosRelacionados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarIngresosRelacionados';
-- GO
