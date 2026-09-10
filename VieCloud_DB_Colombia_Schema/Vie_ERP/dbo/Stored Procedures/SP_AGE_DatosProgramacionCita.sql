

-- =============================================
-- Author:		Rafael Patiño
-- Create date: 10-10-2018
-- Description:	SP carga informacion cirugía programada
-- =============================================

CREATE PROCEDURE [dbo].[SP_AGE_DatosProgramacionCita]
(
@IdProgramacion integer 
)
AS
BEGIN
	SET NOCOUNT ON;
	 
       Select   A.CODAUTONU as 'IDQX',
				B.CODCONCEC as IDSALA,
				B.DESCRIPSAL As 'Sala',
				CONVERT(VARCHAR(5), FECHORAIN,108) AS 'Hora',
				Rtrim(A.IPCODPACI) As IPCODPACI,
				Rtrim(A.IPCODPACI) + ' - ' +Rtrim(IPNOMCOMP) As 'Paciente',
				RTRIM(C.IPTELEFON) AS TELEFONO,
				RTRIM(C.IPTELMOVI) AS CELULAR,
				Rtrim(E.CODSERIPS) +' - '+ Rtrim(DESSERIPS) As 'Procedimiento',
				CASE PRISERIPS WHEN '1' THEN 'Emergencia' WHEN '2' THEN 'Urgencia' WHEN '3' THEN 'Normal' WHEN '4' THEN 'Definir Conducta' else 'Programada'  END AS 'Prioridad',
				RTRIM(F.CODPROSAL) + ' - ' + Rtrim(F.NOMMEDICO) As 'Profesional',
				Rtrim(G.CODESPECI) + ' - ' + Rtrim(G.DESESPECI) As 'Especialidad',
				A.FECHORAIN as 'HoraInicial',
				A.FECHORAFI as 'HoraFinal',
				RTRIM(U.CODUSUARI) + ' - ' + RTRIM(U.NOMUSUARI) AS 'USUARIOASIGNO',
				A.CODESTPQX as 'ESTADO',
				CASE A.CODESTPQX WHEN 0 THEN 'Cirugia Programada' WHEN 1 THEN 'Paciente admitido' WHEN 2 THEN 'Paciente en sala de espera' WHEN 3 THEN 'Paciente en sala quirurgíca' WHEN 4 THEN 'Paciente en recuperación' WHEN 5 THEN 'Paciente con alta' WHEN 6 THEN 'Anulado'  END AS 'DescripcionEstado',
				A.PRINCIPAL,
				case A.PRINCIPAL when 1 then 'SI' else 'NO' end AS 'DescripcionPrincipal',
				J.UFUCODIGO,
				J.UFUTIPUNI, 
				Rtrim(J.UFUDESCRI) AS 'Unidad Funcional',
				I.NUMINGRES  As NUMINGRES,
				A.ORIGENQX, 
				case A.ORIGENQX when 1 then 'Ambulatoria' else 'Hospitalaria' end as 'ORIGENQXDescripcion',
				'Servicio: '+ RTRIM(E.codserips) +' - '+RTRIM(E.desserips)+ char(10)+'Profesional: ' + RTRIM(F.CODPROSAL) +' - '+RTRIM(F.NOMMEDICO)+ char(10)+'Especialidad:' + Rtrim(G.CODESPECI) + ' - ' + Rtrim(G.DESESPECI) + char(10)+'Principal: ' + case A.PRINCIPAL when 1 then 'SI' else 'NO' end + char(10)+'Origen Cirugía: ' + case A.ORIGENQX when 1 then 'Ambulatoria' else 'Hospitalaria' end  AS 'Contenido',
				A.ESTADOFARM as 'EstadoFarmacia',
				Rtrim(CE.CODCENATE) + ' - ' + Rtrim(CE.NOMCENATE ) as CentroAtencion
			FROM AGEPROGQX A with(nolock)
			INNER JOIN AGENSALAC B with(nolock) ON A.AGENSALAC = B.CODCONCEC 
			INNER JOIN INPACIENT C with(nolock) ON A.IPCODPACI = C.IPCODPACI
			INNER JOIN INCUPSIPS  E with(nolock) ON A.CODSERIPS = E.CODSERIPS
			INNER JOIN INPROFSAL   F with(nolock) ON A.CODPROSAL  = F.CODPROSAL
			INNER JOIN INESPECIA G with(nolock) ON A.CODESPECI = G.CODESPECI
			INNER JOIN SEGusuaru  U with(nolock) ON A.CODUSUASI=U.CODUSUARI   
			LEFT JOIN HCORDPROQ H with(nolock) ON A.AUTOHCORDPROQ = H.AUTO
			LEFT JOIN ADINGRESO  I with(nolock) ON H.NUMINGRES  = I.NUMINGRES
			INNER JOIN INUNIFUNC J with(nolock) ON B.UFUCODIGO = J.UFUCODIGO 
			INNER JOIN ADCENATEN CE with(nolock) ON CE.CODCENATE = A.CODCENATE 
		WHERE A.CODAUTONU = @IdProgramacion 

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento almacenado que recupera el detalle completo de una cirugía o procedimiento quirúrgico programado a partir de su identificador único de programación. Consolida en un único resultado los datos del paciente (cédula, nombre, teléfono, celular), la sala quirúrgica asignada, la hora de inicio y fin, el servicio o procedimiento CUPS, la prioridad (emergencia, urgencia, normal, programada), el profesional de la salud responsable, la especialidad, el estado actual del proceso quirúrgico (programado, admitido, en sala, recuperación, alta, anulado), la unidad funcional, el número de ingreso hospitalario vinculado, el origen de la cirugía (ambulatoria u hospitalaria), el usuario que realizó la asignación, el estado en farmacia y el centro de atención. Se utiliza para presentar en pantalla el resumen completo de una cita quirúrgica agendada, integrando datos de agendamiento, historia clínica, admisión, maestros de pacientes, profesionales, especialidades, salas, unidades funcionales y centros de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AGE_DatosProgramacionCita';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AGE_DatosProgramacionCita';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve la información detallada de una cirugía programada (sala, paciente, procedimiento, profesional, especialidad, horarios, estado, unidad funcional y centro de atención) para visualización en agenda quirúrgica.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_DatosProgramacionCita';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir una programación quirúrgica con el identificador suministrado en AGEPROGQX; Deben existir registros relacionados en AGENSALAC, INPACIENT, INCUPSIPS, INPROFSAL, INESPECIA, SEGusuaru, INUNIFUNC y ADCENATEN (joins INNER)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_DatosProgramacionCita';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La consulta solo retorna cirugías cuyo identificador coincida exactamente con el parámetro de entrada; La relación con ingreso hospitalario es opcional (LEFT JOIN sobre HCORDPROQ y ADINGRESO), permitiendo cirugías sin ingreso asociado; Toda cirugía programada debe estar asociada a una sala, paciente, procedimiento (CUPS-IPS), profesional, especialidad, usuario que asignó, unidad funcional y centro de atención', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_DatosProgramacionCita';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cirugía programada; Sala quirúrgica; Paciente; Procedimiento (CUPS-IPS); Profesional de salud; Especialidad médica; Prioridad quirúrgica; Estado de la cirugía; Unidad funcional; Ingreso hospitalario; Origen de cirugía (Ambulatoria/Hospitalaria); Centro de atención; Estado de farmacia', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_DatosProgramacionCita';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] AGEPROGQX: Cuando CODAUTONU coincide con el identificador recibido, retorna una fila con los datos consolidados de la cirugía programada', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_DatosProgramacionCita';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si PRISERIPS = ''1'' → Prioridad = ''Emergencia'' else Si ''2'' Urgencia; ''3'' Normal; ''4'' Definir Conducta; cualquier otro valor ''Programada''; si CODESTPQX in (0..6) → Mapea estado a etiqueta: 0 Cirugía Programada, 1 Paciente admitido, 2 En sala de espera, 3 En sala quirúrgica, 4 En recuperación, 5 Con alta, 6 Anulado; si PRINCIPAL = 1 → DescripcionPrincipal = ''SI'' else ''NO''; si ORIGENQX = 1 → Origen = ''Ambulatoria'' else ''Hospitalaria''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_DatosProgramacionCita';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGEPROGQX; dbo.AGENSALAC; dbo.INPACIENT; dbo.INCUPSIPS; dbo.INPROFSAL; dbo.INESPECIA; dbo.SEGusuaru; dbo.HCORDPROQ; dbo.ADINGRESO; dbo.INUNIFUNC; dbo.ADCENATEN', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_DatosProgramacionCita';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_DatosProgramacionCita';
-- GO
