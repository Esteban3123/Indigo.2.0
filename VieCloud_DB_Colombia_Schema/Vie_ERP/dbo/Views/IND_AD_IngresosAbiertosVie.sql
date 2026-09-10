CREATE VIEW [dbo].[IND_AD_IngresosAbiertosVie]
AS
SELECT DISTINCT 
                         i.NUMINGRES AS Ingreso, ga.Code AS Cód_Grupo_Atención, ga.Name AS Grupo_Atención, ea.Name AS Entidad, i.IPCODPACI AS Identificación, CAST(p.GENEXPEDITIONCITY AS varchar(20)) + ' - ' + ISNULL(ci.Name, '') 
                         AS Lugar_Expedición, p.IPNOMCOMP AS Paciente, i.IFECHAING AS Fecha_Ingreso, 
                         CASE i.IESTADOIN WHEN '  ' THEN 'Sin Confirmar Hoja de Trabajo' WHEN 'F' THEN 'Confirmada Hoja de Trabajo' WHEN 'A' THEN 'Anulado' WHEN 'C' THEN 'Cerrado' WHEN 'P' THEN 'Facturado Parcial' END AS Estado, 
                         uf.UFUDESCRI AS Unidad_Funcional, em.FECALTPAC AS Fecha_Alta_Médica, HC.CODDIAGNO AS CIE_10, CIE10.NOMDIAGNO AS Diagnóstico, i.codusucre AS CódUsuarioCrea, per.fullname AS Usuario_Crea, 
                         i.FECREGCRE AS Fecha_Creación, uu.NOMUSUARI AS UsuarioModifico, i.FECREGMOD AS Fecha_Modificación, D .UFUDESCRI AS UnidadActual, i.IOBSERVAC AS Observaciones, 
                         CASE i.TIPOINGRE WHEN 1 THEN 'Ambulatorio' WHEN 2 THEN 'Hospitalario' END AS TipoIngreso, HCU.ENFACTUAL AS Enfermedad_Actual, UBINOMBRE AS Ubicación, MUNNOMBRE AS Municipio, p.IPTELEFON AS [Tele Fijo], 
                         p.IPTELMOVI AS [Tel Movil]
FROM            dbo.ADINGRESO AS i WITH (NOLOCK) INNER JOIN
                         dbo.INUNIFUNC AS uf WITH (NOLOCK) ON uf.UFUCODIGO = i.UFUCODIGO LEFT OUTER JOIN
                         Contract.CareGroup AS ga WITH (NOLOCK) ON ga.Id = i.GENCAREGROUP LEFT OUTER JOIN
                         Security.[User] AS u ON u.UserCode = i.CODUSUCRE LEFT OUTER JOIN
                         Security.[User] AS um ON u.UserCode = i.codusumod LEFT OUTER JOIN
                         Security.Person AS per ON per.Id = u.IdPerson LEFT OUTER JOIN
                         Security.Person AS PERM ON PERM .Id = um.IdPerson LEFT OUTER JOIN
                         dbo.HCHISPACA AS HC WITH (NOLOCK) ON HC.NUMINGRES = i.NUMINGRES AND HC.IPCODPACI = HC.IPCODPACI AND HC.TIPHISPAC = 'i' LEFT OUTER JOIN
                         dbo.INPACIENT AS p WITH (NOLOCK) ON p.IPCODPACI = i.IPCODPACI LEFT OUTER JOIN
                         Contract.HealthAdministrator AS ea WITH (nolock) ON ea.Id = i.GENCONENTITY LEFT OUTER JOIN
                         Common.City AS ci WITH (NOLOCK) ON ci.Id = p.GENEXPEDITIONCITY LEFT OUTER JOIN
                         dbo.HCURGING1 AS HCU WITH (NOLOCK) ON HCU.NUMINGRES = HC.NUMINGRES AND HCU.IPCODPACI = HC.IPCODPACI AND HCU.NUMEFOLIO = HC.NUMEFOLIO LEFT OUTER JOIN
                             (SELECT        IPCODPACI, NUMINGRES, MAX(NUMEFOLIO) AS Folio
                               FROM            dbo.INDIAGNOP
                               WHERE        (CODDIAPRI = 'True')
                               GROUP BY NUMINGRES, IPCODPACI) AS DX ON DX.IPCODPACI = HCU.IPCODPACI AND DX.NUMINGRES = HCU.NUMINGRES AND DX.Folio = HC.NUMEFOLIO LEFT OUTER JOIN
                         dbo.INDIAGNOS AS CIE10 WITH (NOLOCK) ON CIE10.CODDIAGNO = HC.CODDIAGNO LEFT OUTER JOIN
                         dbo.HCURGEVO1 AS HCU1 WITH (NOLOCK) ON HCU.NUMINGRES = HC.NUMINGRES AND HCU1.IPCODPACI = HC.IPCODPACI AND HCU1.NUMEFOLIO = HC.NUMEFOLIO LEFT OUTER JOIN
                         dbo.ADINGRESO AS I2 WITH (NOLOCK) ON I2.NUMINGRES = i.NUMINGRES LEFT OUTER JOIN
                         dbo.INUNIFUNC AS D WITH (NOLOCK) ON I2.UFUAACTHOS = D .UFUCODIGO LEFT OUTER JOIN
                         dbo.HCREGEGRE AS em WITH (NOLOCK) ON em.IPCODPACI = HC.IPCODPACI AND em.NUMINGRES = HC.NUMINGRES LEFT OUTER JOIN
                         dbo.SEGusuaru AS uu WITH (NOLOCK) ON uu.CODUSUARI = i.CODUSUCRE LEFT OUTER JOIN
                         dbo.INUBICACI AS BB WITH (NOLOCK) ON BB.AUUBICACI = P.AUUBICACI LEFT OUTER JOIN
                         dbo.INMUNICIP AS EE WITH (NOLOCK) ON EE.DEPMUNCOD = BB.DEPMUNCOD
WHERE        (i.CODCENATE IN ('001', '00101','00102','00103')) AND (i.IESTADOIN <> 'F') AND (i.IESTADOIN <> 'A') AND (i.IESTADOIN <> 'C')
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida todos los ingresos activos (sin confirmar hoja de trabajo, facturados parcialmente) de los centros de atención 001 y sus subsedes, excluyendo los cerrados, confirmados y anulados. Integra datos del episodio de ingreso (número de ingreso, tipo —ambulatorio u hospitalario—, fecha, estado, observaciones, unidad funcional actual y de ingreso) con información del paciente (cédula/identificación, nombre, teléfonos, lugar de expedición del documento, municipio y ubicación), la entidad pagadora (EPS/aseguradora), el grupo de atención del contrato, el diagnóstico principal CIE-10 tomado del último folio de historia clínica de urgencias, la enfermedad actual registrada, la fecha de alta médica, y los usuarios que crearon o modificaron el registro. Sirve como panel de control operativo y de auditoría para monitorear en tiempo real los pacientes que actualmente se encuentran hospitalizados o en atención abierta dentro de la institución.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'IND_AD_IngresosAbiertosVie';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'IND_AD_IngresosAbiertosVie';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los ingresos abiertos (no cerrados, no anulados, no confirmados como hoja de trabajo final) de centros de atención específicos, con datos del paciente, diagnóstico principal, ubicación y trazabilidad de creación/modificación.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_IngresosAbiertosVie';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El ingreso debe pertenecer a uno de los centros de atención ''001'', ''00101'', ''00102'' o ''00103''.; El estado del ingreso debe ser distinto de ''F'' (Confirmada Hoja de Trabajo), ''A'' (Anulado) y ''C'' (Cerrado).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_IngresosAbiertosVie';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen ingresos en estados ''abiertos'' (vacío o ''P''); nunca ingresos cerrados, anulados o con hoja de trabajo confirmada.; Solo se incluyen ingresos de los centros de atención ''001'', ''00101'', ''00102'', ''00103''.; El diagnóstico asociado corresponde al diagnóstico principal (CODDIAPRI=''True'') del folio más reciente.; Solo se vinculan historias clínicas de tipo ingreso (TIPHISPAC=''i'').', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_IngresosAbiertosVie';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ingreso/Admisión; Paciente; Grupo de Atención; Administradora de Salud (EPS); Unidad Funcional; Diagnóstico CIE-10; Historia Clínica de Urgencias; Hoja de Trabajo; Alta Médica; Tipo de Ingreso (Ambulatorio/Hospitalario); Centro de Atención; Ubicación/Municipio del paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_IngresosAbiertosVie';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Retorna ingresos abiertos: CODCENATE IN (''001'',''00101'',''00102'',''00103'') AND IESTADOIN NOT IN (''F'',''A'',''C''), enriquecidos con datos de paciente, EPS, unidad funcional, diagnóstico CIE-10 y ubicación.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_IngresosAbiertosVie';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si i.IESTADOIN = ''  '' (dos espacios) → Estado se reporta como ''Sin Confirmar Hoja de Trabajo''; si i.IESTADOIN = ''F'' → Estado ''Confirmada Hoja de Trabajo'' (excluido por WHERE); si i.IESTADOIN = ''A'' → Estado ''Anulado'' (excluido por WHERE); si i.IESTADOIN = ''C'' → Estado ''Cerrado'' (excluido por WHERE); si i.IESTADOIN = ''P'' → Estado ''Facturado Parcial''; si i.TIPOINGRE = 1 → TipoIngreso = ''Ambulatorio'' else Si TIPOINGRE = 2 entonces ''Hospitalario''; si HC.TIPHISPAC = ''i'' → Solo se vincula la historia clínica de tipo ingreso (no otros tipos de historia); si INDIAGNOP.CODDIAPRI = ''True'' → Se considera solo el diagnóstico marcado como principal, tomando el folio máximo por ingreso/paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_IngresosAbiertosVie';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO; dbo.INUNIFUNC; Contract.CareGroup; Security.User; Security.Person; dbo.HCHISPACA; dbo.INPACIENT; Contract.HealthAdministrator; Common.City; dbo.HCURGING1; dbo.INDIAGNOP; dbo.INDIAGNOS; dbo.HCURGEVO1; dbo.HCREGEGRE; dbo.SEGusuaru; dbo.INUBICACI; dbo.INMUNICIP', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_IngresosAbiertosVie';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_IngresosAbiertosVie';
GO
