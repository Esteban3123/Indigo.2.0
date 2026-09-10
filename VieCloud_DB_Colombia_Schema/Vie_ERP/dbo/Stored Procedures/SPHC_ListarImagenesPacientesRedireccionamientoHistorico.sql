
CREATE PROCEDURE [dbo].[SPHC_ListarImagenesPacientesRedireccionamientoHistorico] 
(
@CentroAtencion Char(500),
@SubGrupo int,
@FechaInicial datetime,
@FechaFinal datetime
)
AS
BEGIN
	SET NOCOUNT ON;
		BEGIN
			SELECT FECORDMED AS 'RequestDate',SendToInterfaceDate AS 'RoutingDate',CASE SendToInterface WHEN 1 THEN 'RIS' WHEN 2 THEN 'VieCloud' END AS 'RoutedTo'
				   ,RTRIM(NOMMEDICO) AS 'RoutingUser', A.IPCODPACI AS 'Identification',RTRIM(C.IPNOMCOMP) AS 'Patient',RTRIM(A.CODSERIPS) AS 'ServiceCode',RTRIM(D.DESSERIPS) AS 'ServiceDescription'
				   ,CASE A.LATERALIDAD WHEN 0 THEN 'No Aplica' WHEN 1 THEN 'Izquierda' WHEN 2 THEN 'Derecha' WHEN 3 THEN 'Bilateral' WHEN 4 THEN 'Multilateral' 
				   ELSE 'No Aplica' END AS 'Laterality'
				   ,RTRIM(A.NUMINGRES) AS 'AdmissionNumber',RTRIM(EV.Name) As 'Entity'
			from HCORDIMAG AS A
				INNER JOIN INPROFSAL AS B ON B.CODPROSAL = A.SendToInterfaceProfessional  
				INNER JOIN INPACIENT AS C ON C.IPCODPACI = A.IPCODPACI 
				INNER JOIN INCUPSIPS AS D ON D.CODSERIPS = A.CODSERIPS
				INNER JOIN ADINGRESO AS E ON E.NUMINGRES = A.NUMINGRES
				INNER JOIN  dbo.INCUPSSUB F with(nolock) on F.CODGRUSUB=D.CODGRUSUB 
				INNER JOIN CONTRACT.healthadministrator EV ON EV.Id = E.GENCONENTITY
			WHERE A.RoutedFrom = 2 AND A.CODCENATE IN (SELECT Value FROM dbo.splitstring(@CentroAtencion)) AND  F.IDRISGRIMAGE=@subgrupo AND A.SendToInterfaceDate BETWEEN @FechaInicial AND @FechaFinal
		END	
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista el historial de órdenes de imágenes diagnósticas (radiología, ecografías, tomografías, resonancias, entre otros) que fueron redireccionadas desde la historia clínica hacia un sistema externo, ya sea RIS o VieCloud. Para cada orden devuelve la fecha de solicitud, la fecha y destino del redireccionamiento, el usuario que realizó el envío, la cédula e identificación del paciente, el nombre completo, el código y descripción del servicio CUPS, la lateralidad del estudio, el número de ingreso o admisión y el nombre de la entidad o EPS pagadora. Filtra por centro de atención, subgrupo de imágenes diagnósticas y un rango de fechas de redireccionamiento, combinando datos de órdenes de imágenes, profesionales de la salud, pacientes, servicios CUPS, ingresos y administradoras de salud. Se utiliza para auditoría, seguimiento y reporte histórico del flujo de imágenes enviadas a interfaces externas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarImagenesPacientesRedireccionamientoHistorico';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarImagenesPacientesRedireccionamientoHistorico';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista el histórico de órdenes de imágenes diagnósticas que fueron redireccionadas desde HCE hacia interfaces externas (RIS o VieCloud), filtradas por centros de atención, subgrupo de imagen y rango de fechas de envío.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesRedireccionamientoHistorico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El listado de centros de atención debe poder ser tokenizado por dbo.splitstring; Debe existir un subgrupo de imagen (IDRISGRIMAGE) en INCUPSSUB que coincida con el parámetro; Las órdenes deben tener profesional, paciente, servicio CUPS, ingreso y entidad administradora válidos (todos los JOIN son INNER)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesRedireccionamientoHistorico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen órdenes con RoutedFrom = 2 (origen específico de redireccionamiento); Solo se reportan órdenes efectivamente enviadas a interfaz (RIS o VieCloud); El filtro temporal se aplica sobre la fecha de envío a interfaz, no sobre la fecha de la orden médica; La lateralidad siempre se traduce a una etiqueta legible, nunca queda nula', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesRedireccionamientoHistorico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden de imágenes diagnósticas; Redireccionamiento a interfaz (RIS/VieCloud); Paciente; Profesional de salud; Servicio CUPS/IPS; Lateralidad; Ingreso/Admisión; Entidad administradora de salud; Subgrupo de imagen', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesRedireccionamientoHistorico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCORDIMAG: Cuando RoutedFrom = 2, el centro de atención está en la lista, el subgrupo de imagen coincide y la fecha de envío a interfaz está en el rango, devuelve la información histórica de redireccionamiento', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesRedireccionamientoHistorico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si SendToInterface = 1 → Se reporta como redireccionado a ''RIS'' else Si SendToInterface = 2 se reporta como ''VieCloud''; si LATERALIDAD = 0 o no clasificada → Se reporta como ''No Aplica'' else 1=Izquierda, 2=Derecha, 3=Bilateral, 4=Multilateral', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesRedireccionamientoHistorico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.splitstring', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesRedireccionamientoHistorico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDIMAG; dbo.INPROFSAL; dbo.INPACIENT; dbo.INCUPSIPS; dbo.ADINGRESO; dbo.INCUPSSUB; CONTRACT.healthadministrator', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesRedireccionamientoHistorico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesRedireccionamientoHistorico';
-- GO
