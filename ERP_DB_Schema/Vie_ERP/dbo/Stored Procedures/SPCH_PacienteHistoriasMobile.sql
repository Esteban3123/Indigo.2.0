
CREATE  PROCEDURE [dbo].[SPCH_PacienteHistoriasMobile]
(
@Paciente Varchar(25),
@Ingreso Char(20)
)
AS
BEGIN
	SET NOCOUNT ON;


	SELECT RTRIM(C.DESESPECI) as Especialidad,rtrim(b.NOMMEDICO) as Profesional,rtrim(a.CODPROSAL) as CodigoProfesional,a.FECHINIHI as FechaHistoria,CONVERT(varchar,a.FECINIATE, 0) as 'FechaHistoriaFormateada',a.ANALISISP as Analisis, 'Historia Ingreso' as Tipo
	FROM HCURGING1 A INNER JOIN
	INPROFSAL B ON A.CODPROSAL=B.CODPROSAL INNER JOIN
	INESPECIA C ON A.CODESPTRA=C.CODESPECI
	WHERE A.IPCODPACI=@paciente and a.NUMINGRES=@ingreso
	UNION ALL

		SELECT RTRIM(C.DESESPECI) as Especialidad,rtrim(b.NOMMEDICO) as Profesional,rtrim(a.CODPROSAL) as CodigoProfesional,a.FECINIATE as FechaHistoria,CONVERT(varchar,a.FECINIATE, 0) as 'FechaHistoriaFormateada',a.ANALISISP as Analisis, 'Historia Evolucion' as Tipo
	FROM HCURGEVO1 A INNER JOIN
	INPROFSAL B ON A.CODPROSAL=B.CODPROSAL INNER JOIN
	INESPECIA C ON A.CODESPTRA=C.CODESPECI
	WHERE A.IPCODPACI=@paciente and a.NUMINGRES=@ingreso

	UNION ALL

		SELECT RTRIM(C.DESESPECI) as Especialidad,rtrim(b.NOMMEDICO) as Profesional,rtrim(a.CODPROSAL) as CodigoProfesional,a.FECINIATE as FechaHistoria,CONVERT(varchar,a.FECINIATE, 0) as 'FechaHistoriaFormateada' ,a.ANALISISP as Analisis, 'Historia Nota Rapida' as Tipo
	FROM HCNOTEVO1 A INNER JOIN
	INPROFSAL B ON A.CODPROSAL=B.CODPROSAL INNER JOIN
	INESPECIA C ON A.CODESPTRA=C.CODESPECI
	WHERE A.IPCODPACI=@paciente and a.NUMINGRES=@ingreso

	
	
	
end