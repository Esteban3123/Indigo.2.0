
-- =============================================
-- Author:		Juan David Patiño Cabrera
-- Create date: 23-10-2017
-- Description:	Sp que me cargara las Salas Agendadas, adicionalemte se cargara los pacientes que estas para la sala.
-- =============================================

CREATE PROCEDURE [dbo].[SPHC_ListarSalasAgendadasCirugia]
(
@FechaFiltro datetime 
)
AS
BEGIN
	SET NOCOUNT ON;

/*Select Rtrim(DESCRIPSAL) As 'Nombre Sala',CONVERT(VARCHAR(5), FECHORAIN,108) AS 'Hora', Rtrim(A.IPCODPACI) As IPCODPACI,Rtrim(IPNOMCOMP) As 'Nombre Paciente',Rtrim(E.CODSERIPS) +'-'+ Rtrim(DESSERIPS) As 'Procedimiento','Programada' As 'Prioridad', Rtrim(NOMMEDICO) As 'Profesional',Rtrim(G.DESESPECI) As 'Especialidad', ' ' As 'Unidad Funcional', ' ' As NUMINGRES, '' as AUTO from [dbo].AGEPROGQX A
		INNER JOIN AGENSALAC B ON A.AGENSALAC = B.CODCONCEC 
		INNER JOIN INPACIENT C ON A.IPCODPACI = C.IPCODPACI
		INNER JOIN INCUPSIPS  E ON A.CODSERIPS = E.CODSERIPS
		INNER JOIN INPROFSAL   F ON A.CODPROSAL  = F.CODPROSAL
		INNER JOIN  INESPECIA G ON A.CODESPECI = G.CODESPECI   WHERE CONVERT(VARCHAR(20),FECHORAIN,103) = @FechaFiltro 
UNION ALL*/
   Select Rtrim(DESCRIPSAL) As 'Nombre Sala',CONVERT(VARCHAR(5), FECHORAIN,108) AS 'Hora', Rtrim(A.IPCODPACI) As IPCODPACI,Rtrim(IPNOMCOMP) As 'Nombre Paciente',Rtrim(E.CODSERIPS) +'-'+ Rtrim(DESSERIPS) As 'Procedimiento',CASE PRISERIPS WHEN '1' THEN 'Emergencia' WHEN '2' THEN 'Urgencia' WHEN '3' THEN 'Normal' WHEN '4' THEN 'Definir Conducta' else 'Porgramada'  END AS 'Prioridad', Rtrim(NOMMEDICO) As 'Profesional',Rtrim(G.DESESPECI) As 'Especialidad', Rtrim(J.UFUDESCRI) AS 'Unidad Funcional', I.NUMINGRES  As NUMINGRES,AUTO from [dbo].AGEPROGQX A
		INNER JOIN AGENSALAC B ON A.AGENSALAC = B.CODCONCEC 
		INNER JOIN INPACIENT C ON A.IPCODPACI = C.IPCODPACI
		INNER JOIN INCUPSIPS  E ON A.CODSERIPS = E.CODSERIPS
		INNER JOIN INPROFSAL   F ON A.CODPROSAL  = F.CODPROSAL
		INNER JOIN INESPECIA G ON A.CODESPECI = G.CODESPECI   
		LEFT JOIN HCORDPROQ H ON A.AUTOHCORDPROQ = H.AUTO
		LEFT JOIN ADINGRESO  I ON H.NUMINGRES  = I.NUMINGRES
		LEFT JOIN INUNIFUNC J ON I.UFUCODIGO = J.UFUCODIGO  	WHERE CONVERT(VARCHAR(20),FECHORAIN,103) = @FechaFiltro 
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista todas las salas de cirugía agendadas para una fecha específica, mostrando los pacientes programados en cada sala quirúrgica junto con su hora de inicio, nombre completo, procedimiento (código CUPS y descripción), prioridad de atención (emergencia, urgencia, normal, definir conducta o programada), profesional de la salud responsable, especialidad médica, unidad funcional de hospitalización y número de ingreso. Integra la programación quirúrgica (AGEPROGQX) con las salas disponibles (AGENSALAC), el maestro de pacientes (INPACIENT), el catálogo de servicios CUPS (INCUPSIPS), el maestro de profesionales (INPROFSAL), las especialidades médicas (INESPECIA), las órdenes de procedimientos de la historia clínica (HCORDPROQ), los ingresos o admisiones del paciente (ADINGRESO) y las unidades funcionales (INUNIFUNC). Se utiliza para la gestión y visualización del listado diario de salas de cirugía ocupadas, apoyando la operación quirúrgica y la coordinación entre admisiones, historia clínica y programación de cirugías.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarSalasAgendadasCirugia';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarSalasAgendadasCirugia';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las salas de cirugía agendadas en una fecha específica junto con los pacientes programados, su procedimiento, prioridad, profesional, especialidad y datos de ingreso.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarSalasAgendadasCirugia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe proporcionarse una fecha de filtro para comparar contra FECHORAIN en formato 103 (dd/mm/yyyy).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarSalasAgendadasCirugia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen cirugías cuya FECHORAIN coincide con la fecha filtro.; Sala, paciente, procedimiento (CUPS), profesional y especialidad son obligatorios (INNER JOIN); el ingreso y unidad funcional son opcionales (LEFT JOIN).; El procedimiento se presenta concatenando código CUPS y su descripción separados por ''-''.; La hora se muestra en formato HH:MM (24h).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarSalasAgendadasCirugia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Sala de cirugía; Agenda quirúrgica; Paciente; Procedimiento CUPS; Prioridad quirúrgica (Emergencia/Urgencia/Normal/Definir Conducta/Programada); Profesional de salud; Especialidad médica; Unidad funcional; Ingreso hospitalario; Orden de procedimiento quirúrgico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarSalasAgendadasCirugia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve las cirugías agendadas (AGEPROGQX) cuya fecha/hora de inicio (FECHORAIN) coincide con la fecha filtro, incluyendo sala, hora, paciente, procedimiento, prioridad, profesional, especialidad, unidad funcional e ingreso.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarSalasAgendadasCirugia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si PRISERIPS = ''1'' → Prioridad se reporta como ''Emergencia''; si PRISERIPS = ''2'' → Prioridad se reporta como ''Urgencia''; si PRISERIPS = ''3'' → Prioridad se reporta como ''Normal''; si PRISERIPS = ''4'' → Prioridad se reporta como ''Definir Conducta''; si PRISERIPS no coincide con 1-4 → Prioridad se reporta como ''Porgramada'' (valor por defecto)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarSalasAgendadasCirugia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGEPROGQX; dbo.AGENSALAC; dbo.INPACIENT; dbo.INCUPSIPS; dbo.INPROFSAL; dbo.INESPECIA; dbo.HCORDPROQ; dbo.ADINGRESO; dbo.INUNIFUNC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarSalasAgendadasCirugia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarSalasAgendadasCirugia';
-- GO
