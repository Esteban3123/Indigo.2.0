CREATE PROCEDURE [dbo].[SP_AGE_ListarPacienteGestionQX_Programados]
(
	@FechaFiltro datetime,
	@Grupo varchar(max)
)
AS
BEGIN
	SET NOCOUNT ON;
	 
	  -- QX programados
       Select   A.CODAUTONU as 'IDQX',
				B.CODCONCEC as IDSALA,
				B.DESCRIPSAL As 'Sala',
				CONVERT(VARCHAR(5), FECHORAIN,108) AS 'Hora',
				Rtrim(A.IPCODPACI) As IPCODPACI,
				Rtrim(A.IPCODPACI) + ' - ' +Rtrim(IPNOMCOMP) As 'Paciente',
				RTRIM(C.IPTELEFON) AS TELEFONO,
				RTRIM(C.IPTELMOVI) AS CELULAR,
				Rtrim(E.CODSERIPS) +' - '+ Rtrim(DESSERIPS) + '. ' + isnull(CD.name,'') As 'Procedimiento',
		        RTRIM(F.CODPROSAL) + ' - ' + Rtrim(F.NOMMEDICO) As 'Profesional',
				Rtrim(G.CODESPECI) + ' - ' + Rtrim(G.DESESPECI) As 'Especialidad',
				A.FECHORAIN as 'HoraInicial',
				A.FECHORAFI as 'HoraFinal',
				RTRIM(U.CODUSUARI) + ' - ' + RTRIM(U.NOMUSUARI) AS 'USUARIOASIGNO',
				A.CODESTPQX as 'ESTADO',
				CASE A.CODESTPQX WHEN 0 THEN 'Cirugia Programada' WHEN 1 THEN 'Paciente admitido' WHEN 2 THEN 'Paciente en sala de espera' WHEN 3 THEN 'Paciente en sala quirurgíca' WHEN 4 THEN 'Paciente en recuperación' WHEN 5 THEN 'Paciente con alta' WHEN 6 THEN 'Anulado'  END AS 'DescripcionEstado',
				A.PRINCIPAL,
				case A.PRINCIPAL when 1 then 'QX Principal - SI' else 'QX - Principal NO' end AS 'DescripcionPrincipal',
				J.UFUCODIGO,
				J.UFUTIPUNI, 
				Rtrim(J.UFUDESCRI) AS 'Unidad Funcional',
				case A.ORIGENQX 
					when 1 then A.NUMINGRES 
					when 2 then CASE WHEN A.AUTOHCORDPROQ
							         IS NULL THEN (select H.NUMINGRES  from HCORDPRON H where H.AUTO = A.AUTOHCORDPRON)
								                else (select H.NUMINGRES  from HCORDPROQ H where H.AUTO = A.AUTOHCORDPROQ) END END As NUMINGRES,
				A.ORIGENQX, 
				case A.ORIGENQX when 1 then 'Ambulatoria' else 'Hospitalaria' end as 'ORIGENQXDescripcion',
				'Servicio: '+ RTRIM(E.codserips) +' - '+RTRIM(E.desserips) + '. ' + isnull(CD.name,'') + ' ' + char(10)+'Profesional: ' + RTRIM(F.CODPROSAL) +' - '+RTRIM(F.NOMMEDICO)+ char(10)+'Especialidad:' + Rtrim(isnull(G.CODESPECI,'')) + ' - ' + Rtrim(Isnull(G.DESESPECI,'')) + char(10)+'Principal: ' + case A.PRINCIPAL when 1 then 'SI' else 'NO' end + char(10)+'Origen Cirugía: ' + case A.ORIGENQX when 1 then 'Ambulatoria' else 'Hospitalaria' end + CASE WHEN A.OBSERVACION IS NOT NULL THEN char(10) + 'Observación: ' + RTRIM(A.OBSERVACION) ELSE '' END AS 'Contenido',
				A.ESTADOFARM as 'EstadoFarmacia',
				Rtrim(CE.CODCENATE) + ' - ' + Rtrim(CE.NOMCENATE ) as CentroAtencion,
				rtrim(F.CODPROSAL) as CODPROSAL,
				rtrim(A.CODSERIPS) as CODSERIPS,
				case A.TIPOANESTESIA when 1 then 'Local' when 2 then 'Regional' when 3 then 'General' when 4 then 'Combinada' when 5 then 'No aplica' end TipoAnestesia,
				CASE A.AUTOHCORDPROQ 
							         when null then (select CASE H.PRISERIPS WHEN '1' THEN 'Emergencia' WHEN '2' THEN 'Urgencia' WHEN '3' THEN 'Normal' WHEN '4' THEN 'Definir Conducta' else 'Programada'  END  Prioridad  from HCORDPRON H where H.AUTO = A.AUTOHCORDPRON)
								                else (select CASE H.PRISERIPS WHEN '1' THEN 'Emergencia' WHEN '2' THEN 'Urgencia' WHEN '3' THEN 'Normal' WHEN '4' THEN 'Definir Conducta' else 'Programada'  END  Prioridad  from HCORDPROQ H where H.AUTO = A.AUTOHCORDPROQ) END Prioridad,
				case A.PRINCIPAL when 1 then 'SI' else 'NO' end as Principal, 
				case A.ORIGENQX when 1 then 'Ambulatoria' else 'Hospitalaria' end  Origen ,
				RTRIM(GRUPO_C.DESCRIPCION) AS 'Grupo',ISNULL(A.IDDESCRIPCIONRELACIONADA,0) as IDDESCRIPCIONRELACIONADA, ISNUll(CD.Code,'') + ' - ' + isnull(CD.Name,'') as DescripcionRelacionada, C.GENCAREGROUP
				,C.IPFECNACI as FechaNacimiento
		FROM AGEPROGQX A with(nolock)
			INNER JOIN AGENSALAC B with(nolock) ON A.AGENSALAC = B.CODCONCEC 
			INNER JOIN INPACIENT C with(nolock) ON A.IPCODPACI = C.IPCODPACI
			INNER JOIN INCUPSIPS  E with(nolock) ON A.CODSERIPS = E.CODSERIPS
			INNER JOIN INPROFSAL F with(nolock) ON A.CODPROSAL  = F.CODPROSAL
			LEFT JOIN INESPECIA G with(nolock) ON A.CODESPECI = G.CODESPECI
			INNER JOIN SEGusuaru  U with(nolock) ON A.CODUSUASI=U.CODUSUARI   
			INNER JOIN INUNIFUNC J with(nolock) ON B.UFUCODIGO = J.UFUCODIGO  	
			INNER JOIN ADCENATEN CE with(nolock) ON CE.CODCENATE = A.CODCENATE
			INNER JOIN HCGRUPINVD GRUPO_D with(nolock) ON E.CODSERIPS = GRUPO_D.CODSERIPS AND ISNULL(GRUPO_D.IDDESCRIPCIONRELACIONADA, 0) = ISNULL(A.IDDESCRIPCIONRELACIONADA, 0)
			INNER JOIN HCGRUPINVC GRUPO_C with(nolock) ON GRUPO_D.IDHCGRUPINVC = GRUPO_C.ID
			LEFT JOIN contract.CUPSEntityContractDescriptions CDD on CDD.Id = A.IDDESCRIPCIONRELACIONADA 
			LEFT JOIN contract.ContractDescriptions  CD on CD.Id = CDD.ContractDescriptionId
		WHERE CONVERT(VARCHAR(20),FECHORAIN,103) = @FechaFiltro AND GRUPO_C.CODIGO IN (SELECT Value FROM dbo.SplitString(@Grupo)) --AND A.CODESTPQX <> 6

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes con cirugías programadas para una fecha y grupo de procedimientos específicos, integrando datos de la programación quirúrgica, sala asignada, paciente (nombre, teléfono, celular, fecha de nacimiento), procedimiento CUPS con su descripción relacionada de contrato, profesional de la salud, especialidad, unidad funcional y centro de atención. Devuelve el estado actual del paciente en el flujo quirúrgico (programado, admitido, en sala, en recuperación, con alta o anulado), el origen de la cirugía (ambulatoria u hospitalaria), el tipo de anestesia, la prioridad de la orden médica y el usuario que realizó la asignación. Se utiliza en el módulo de gestión de salas de cirugía para el control operativo del pabellón quirúrgico durante el día.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AGE_ListarPacienteGestionQX_Programados';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AGE_ListarPacienteGestionQX_Programados';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las cirugías programadas en una fecha dada y para uno o varios grupos quirúrgicos, devolviendo datos del paciente, sala, profesional, especialidad, procedimiento, prioridad, estado y origen.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListarPacienteGestionQX_Programados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La fecha de filtro debe poder compararse en formato 103 (dd/mm/yyyy) contra FECHORAIN; El parámetro de grupo debe ser una cadena parseable por dbo.SplitString; Deben existir relaciones consistentes entre AGEPROGQX y catálogos de sala, paciente, CUPS, profesional, unidad funcional, centro de atención y grupo de inversión (HCGRUPINVD/HCGRUPINVC)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListarPacienteGestionQX_Programados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se devuelven cirugías cuyo grupo de invasión (HCGRUPINVC.CODIGO) esté dentro del listado recibido; El procedimiento descrito requiere coincidencia obligatoria entre AGEPROGQX y HCGRUPINVD por CODSERIPS e IDDESCRIPCIONRELACIONADA (con ISNULL a 0); El estado 6 (Anulado) NO se excluye (el filtro está comentado), por lo que también puede aparecer en el resultado; La especialidad puede ser nula (LEFT JOIN), el resto de catálogos son obligatorios; La descripción relacionada del contrato puede estar vacía si no existe en CUPSEntityContractDescriptions/ContractDescriptions', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListarPacienteGestionQX_Programados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cirugía programada; Sala quirúrgica; Paciente; Profesional/Médico; Especialidad; Procedimiento (CUPS); Unidad funcional; Centro de atención; Estado de cirugía (programada, admitido, sala de espera, sala quirúrgica, recuperación, alta, anulado); Tipo de anestesia (Local, Regional, General, Combinada, No aplica); Prioridad clínica (Emergencia, Urgencia, Normal, Definir Conducta, Programada); Origen de cirugía (Ambulatoria/Hospitalaria); Cirugía principal; Orden de procedimiento (HCORDPRON/HCORDPROQ); Grupo de inversión/quirúrgico; Número de ingreso; Estado de farmacia; Descripción contractual del procedimiento', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListarPacienteGestionQX_Programados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] AGEPROGQX: Cuando CONVERT(VARCHAR(20),FECHORAIN,103) = @FechaFiltro y GRUPO_C.CODIGO está en la lista del parámetro @Grupo, se retorna el conjunto de cirugías programadas con sus datos asociados', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListarPacienteGestionQX_Programados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si A.CODESTPQX (0..6) → Traduce a etiqueta: 0=Cirugia Programada, 1=Paciente admitido, 2=Sala de espera, 3=Sala quirúrgica, 4=Recuperación, 5=Alta, 6=Anulado; si A.PRINCIPAL = 1 → Marca la cirugía como ''QX Principal - SI'' / ''SI'' else Marca como ''QX - Principal NO'' / ''NO''; si A.ORIGENQX = 1 → Origen ''Ambulatoria'' y NUMINGRES se toma directamente de AGEPROGQX.NUMINGRES else Origen ''Hospitalaria''; si ORIGENQX=2 y AUTOHCORDPROQ IS NULL se obtiene NUMINGRES desde HCORDPRON, en caso contrario desde HCORDPROQ; si A.AUTOHCORDPROQ → Determina si la prioridad se lee de HCORDPROQ o de HCORDPRON para mapear PRISERIPS a Emergencia/Urgencia/Normal/Definir Conducta/Programada; si A.TIPOANESTESIA (1..5) → Traduce a Local/Regional/General/Combinada/No aplica; si A.OBSERVACION IS NOT NULL → Agrega línea ''Observación: ...'' al campo Contenido else No agrega observación al Contenido', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListarPacienteGestionQX_Programados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.SplitString', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListarPacienteGestionQX_Programados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGEPROGQX; dbo.AGENSALAC; dbo.INPACIENT; dbo.INCUPSIPS; dbo.INPROFSAL; dbo.INESPECIA; dbo.SEGusuaru; dbo.INUNIFUNC; dbo.ADCENATEN; dbo.HCGRUPINVD; dbo.HCGRUPINVC; contract.CUPSEntityContractDescriptions; contract.ContractDescriptions; dbo.HCORDPRON; dbo.HCORDPROQ', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListarPacienteGestionQX_Programados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListarPacienteGestionQX_Programados';
-- GO
