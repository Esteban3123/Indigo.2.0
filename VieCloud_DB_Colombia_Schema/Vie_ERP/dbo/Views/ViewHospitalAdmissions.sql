

CREATE VIEW [dbo].[ViewHospitalAdmissions]
AS
SELECT DISTINCT 
    I.CODCENATE AS CodCentro, 
	cen.NOMCENATE AS CentroAtencion, 
	i.NUMINGRES AS Ingreso, 
	ga.Code AS Cod_Grupo_Atencion, 
	ga.Name AS Grupo_Atencion, 
	ea.Name AS Entidad, 
	i.IPCODPACI AS Identificacion, 
	CAST(p.GENEXPEDITIONCITY AS varchar(20)) + ' - ' + ISNULL(ci.Name, '') AS Lugar_Expedicion, 
	p.IPNOMCOMP AS Paciente, 
	i.IFECHAING AS Fecha_Ingreso, 
	CASE i.IESTADOIN 
		WHEN '  ' THEN 'Sin Confirmar Hoja de Trabajo' 
		WHEN 'F' THEN 'Confirmada Hoja de Trabajo' 
		WHEN 'A' THEN 'Anulado' 
		WHEN 'C' THEN 'Cerrado' 
		WHEN 'P' THEN 'Facturado Parcial' 
	END AS Estado, 
	uf.UFUDESCRI AS Unidad_Funcional, 
	em.FECALTPAC AS Fecha_Alta_Médica, 
	HC.CODDIAGNO AS CIE_10, 
	CIE10.NOMDIAGNO AS Diagnostico, 
	i.codusucre AS CodUsuarioCrea, 
	per.fullname AS Usuario_Crea, 
	i.FECREGCRE AS Fecha_Creacion, 
	uu.NOMUSUARI AS UsuarioModifico, 
	i.FECREGMOD AS Fecha_Modificacion, 
	D .UFUDESCRI AS UnidadActual, 
	i.IOBSERVAC AS Observaciones, 
	CASE i.TIPOINGRE 
		WHEN 1 THEN 'Ambulatorio' 
		WHEN 2 THEN 'Hospitalario' 
	END AS TipoIngreso, 
	HCU.ENFACTUAL AS Enfermedad_Actual, 
	UBINOMBRE AS Ubicacion, 
	MUNNOMBRE AS Municipio
FROM .ADINGRESO AS i WITH (NOLOCK) 
INNER JOIN .INUNIFUNC AS uf WITH (NOLOCK) ON uf.UFUCODIGO = i.UFUCODIGO 
INNER JOIN .ADCENATEN AS Cen ON Cen.CODCENATE = i.CODCENATE 
LEFT OUTER JOIN Contract.CareGroup AS ga WITH (NOLOCK) ON ga.Id = i.GENCAREGROUP 
LEFT OUTER JOIN Security.[User] AS u ON u.UserCode = i.CODUSUCRE 
LEFT OUTER JOIN Security.[User] AS um ON u.UserCode = i.codusumod 
LEFT OUTER JOIN Security.Person AS per ON per.Id = u.IdPerson 
LEFT OUTER JOIN Security.Person AS PERM ON PERM .Id = um.IdPerson 
LEFT OUTER JOIN .HCHISPACA AS HC WITH (NOLOCK) ON HC.NUMINGRES = i.NUMINGRES AND HC.IPCODPACI = HC.IPCODPACI AND HC.TIPHISPAC = 'i' 
LEFT OUTER JOIN .INPACIENT AS p WITH (NOLOCK) ON p.IPCODPACI = i.IPCODPACI 
LEFT OUTER JOIN Contract.HealthAdministrator AS ea WITH (nolock) ON ea.Id = i.GENCONENTITY 
LEFT OUTER JOIN Common.City AS ci WITH (NOLOCK) ON ci.Id = p.GENEXPEDITIONCITY 
LEFT OUTER JOIN .HCURGING1 AS HCU WITH (NOLOCK) ON HCU.NUMINGRES = HC.NUMINGRES AND HCU.IPCODPACI = HC.IPCODPACI AND HCU.NUMEFOLIO = HC.NUMEFOLIO 
LEFT OUTER JOIN 
(
	SELECT 
		IPCODPACI, 
		NUMINGRES, 
		MAX(NUMEFOLIO) AS Folio
	FROM .INDIAGNOP
	WHERE (CODDIAPRI = 'True')
	GROUP BY NUMINGRES, IPCODPACI
) AS DX ON DX.IPCODPACI = HCU.IPCODPACI AND DX.NUMINGRES = HCU.NUMINGRES AND DX.Folio = HC.NUMEFOLIO 
LEFT OUTER JOIN .INDIAGNOS AS CIE10 WITH (NOLOCK) ON CIE10.CODDIAGNO = HC.CODDIAGNO 
LEFT OUTER JOIN .HCURGEVO1 AS HCU1 WITH (NOLOCK) ON HCU.NUMINGRES = HC.NUMINGRES AND HCU1.IPCODPACI = HC.IPCODPACI AND HCU1.NUMEFOLIO = HC.NUMEFOLIO 
LEFT OUTER JOIN .ADINGRESO AS I2 WITH (NOLOCK) ON I2.NUMINGRES = i.NUMINGRES 
LEFT OUTER JOIN .INUNIFUNC AS D WITH (NOLOCK) ON I2.UFUAACTHOS = D .UFUCODIGO 
LEFT OUTER JOIN .HCREGEGRE AS em WITH (NOLOCK) ON em.IPCODPACI = HC.IPCODPACI AND em.NUMINGRES = HC.NUMINGRES 
LEFT OUTER JOIN .SEGusuaru AS uu WITH (NOLOCK) ON uu.CODUSUARI = i.CODUSUCRE 
LEFT OUTER JOIN .INUBICACI AS BB ON BB.AUUBICACI = P.AUUBICACI 
LEFT OUTER JOIN .INMUNICIP AS EE ON EE.DEPMUNCOD = BB.DEPMUNCOD
WHERE (i.IESTADOIN <> 'F') AND (i.IESTADOIN <> 'A') AND (i.IESTADOIN <> 'C')
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida los ingresos u hospitalizaciones activas de pacientes (excluye los estados Confirmado, Anulado y Cerrado), integrando datos del episodio de admisión con información del paciente, centro de atención, unidad funcional actual, grupo de atención, entidad pagadora (EPS/aseguradora), diagnóstico principal CIE-10, enfermedad actual, fecha de alta médica, ubicación geográfica, municipio y datos de auditoría de creación y modificación. Combina los registros de admisión (ADINGRESO) con la historia clínica (HCHISPACA), la nota de urgencias (HCURGEVO1, HCURGING1), el registro de egreso (HCREGEGRE), el catálogo de pacientes (INPACIENT), unidades funcionales (INUNIFUNC), centros de atención (ADCENATEN), grupos de atención contractuales (CareGroup) y administradoras de salud (HealthAdministrator). Sirve como fuente principal para reportería operativa y gerencial de censo hospitalario, seguimiento de ingresos en curso, auditoría de admisiones y consulta de estado de pacientes hospitalizados, en urgencias o en atención ambulatoria pendiente de cierre.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewHospitalAdmissions';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewHospitalAdmissions';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista que consolida los ingresos hospitalarios/ambulatorios activos (no facturados, no anulados, no cerrados) con datos del paciente, unidad funcional, diagnóstico principal, entidad responsable y trazabilidad de creación/modificación.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewHospitalAdmissions';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El ingreso debe existir en la tabla de ingresos administrativos; El ingreso debe estar asociado a una unidad funcional y a un centro de atención válidos; El estado del ingreso no debe ser ''F'' (Confirmada Hoja de Trabajo), ''A'' (Anulado) ni ''C'' (Cerrado)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewHospitalAdmissions';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo expone ingresos activos (estado distinto de F, A, C); La vista usa NOLOCK en la mayoría de joins, por lo que admite lecturas sucias; Por cada ingreso se toma como diagnóstico principal el de mayor NUMEFOLIO con CODDIAPRI=''True''; Solo considera historias clínicas de tipo ingreso (''i''); El estado del ingreso se traduce a una etiqueta legible mediante el CASE definido', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewHospitalAdmissions';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ingreso hospitalario; Ingreso ambulatorio; Paciente; Unidad funcional; Centro de atención; Grupo de atención; Entidad administradora de salud; Historia clínica; Diagnóstico principal; CIE-10; Egreso/Alta médica; Hoja de trabajo; Facturación parcial; Lugar de expedición del documento; Enfermedad actual; Ubicación/Municipio del paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewHospitalAdmissions';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ViewHospitalAdmissions: Devuelve filas DISTINCT de ingresos cuyo IESTADOIN no esté en (''F'',''A'',''C''), es decir, solo ingresos sin confirmar hoja de trabajo o facturados parcial.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewHospitalAdmissions';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si i.IESTADOIN = ''  '' → Estado se reporta como ''Sin Confirmar Hoja de Trabajo''; si i.IESTADOIN = ''F'' → Estado ''Confirmada Hoja de Trabajo'' (excluido por WHERE) else No aparece en la vista; si i.IESTADOIN = ''A'' → Estado ''Anulado'' (excluido por WHERE) else No aparece en la vista; si i.IESTADOIN = ''C'' → Estado ''Cerrado'' (excluido por WHERE) else No aparece en la vista; si i.IESTADOIN = ''P'' → Estado se reporta como ''Facturado Parcial''; si i.TIPOINGRE = 1 → TipoIngreso = ''Ambulatorio'' else Si TIPOINGRE = 2 entonces ''Hospitalario''; si HC.TIPHISPAC = ''i'' → Solo se enlaza la historia clínica de tipo ''i'' (ingreso/internación); si INDIAGNOP.CODDIAPRI = ''True'' → Se selecciona el folio máximo del diagnóstico marcado como principal por paciente e ingreso', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewHospitalAdmissions';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO; dbo.INUNIFUNC; dbo.ADCENATEN; Contract.CareGroup; Security.User; Security.Person; dbo.HCHISPACA; dbo.INPACIENT; Contract.HealthAdministrator; Common.City; dbo.HCURGING1; dbo.INDIAGNOP; dbo.INDIAGNOS; dbo.HCURGEVO1; dbo.HCREGEGRE; dbo.SEGusuaru; dbo.INUBICACI; dbo.INMUNICIP', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewHospitalAdmissions';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewHospitalAdmissions';
GO
