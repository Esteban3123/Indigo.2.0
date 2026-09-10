

-- =============================================
-- Author:		Rafael Patiño
-- Create date: 07-07-2019
-- Description:	SP carga info de las cirugías programadas
-- =============================================
CREATE PROCEDURE [dbo].[SPREP_AGE_Cirugias]
(
@Id Integer
)
AS
BEGIN
	SET NOCOUNT ON;
	 
       Select   
			A.IPCODPACI as 'Codigo',A.NUMINGRES as 'Ingreso', 
			RTRIM(IPNOMCOMP) as Nombre,C.IPFECNACI AS 'Fecha Nacimiento','' as Edad ,C.IPDIRECCI AS Direccion,C.IPTELEFON AS Telefono,
			CASE IPSEXOPAC WHEN 1 THEN 'Masculino' WHEN 2 THEN 'Femenino' END AS Sexo, 
			[dbo].[TypeGenderIdentity](C.IdGenderIdentity) AS IdentidadGenero,
			CASE when IPTIPODOC = 1 then 'Cédula de Ciudadanía' 
				when IPTIPODOC = 2 then 'Cedula Extranjera' 
				when IPTIPODOC = 3 then 'Tarjeta Identidad' 
				when IPTIPODOC = 4 then 'Registro Civil' 
				when IPTIPODOC = 5 then 'Pasaporte' 
				when IPTIPODOC = 6 THEN 'Adulto sin Identificación (solo para el Régimen Subsidiado)' 
				When IPTIPODOC = 7 then 'Menor sin Identificación (solo para el Régimen Subsidiado)'
				when IPTIPODOC= 9 then 'Carnet Diplomático' END AS TipoIdentificacion,
			CASE when IPTIPOPAC = 0 then 'Contributivo' 
				when IPTIPOPAC = 1 then 'Subsidiado'  
				when IPTIPOPAC = 2 then 'Vinculado'
				when IPTIPOPAC = 3 then 'Particular' 
				when IPTIPOPAC = 5 then 'Desplazado Reg. Contributivo'  
				when IPTIPOPAC = 6 then 'Desplazado Reg. Subsidiado' 
				when IPTIPOPAC = 7 then 'Desplazado no Asegurado' END AS TipoUsuario,
				rtrim(ce.CODCENATE) + ' - ' + ce.NOMCENATE as 'CentroAtencion',
				rtrim(J.UFUCODIGO) + ' - ' + J.UFUDESCRI as 'UnidadFuncional',
				rtrim(F.CODPROSAL) + ' - ' + F.NOMMEDICO as 'Profesional',
				rtrim(G.CODESPECI) +' - ' + G.DESESPECI as 'Especialidad',
				rtrim(B.CODIGSALA) +' - ' + B.DESCRIPSAL as 'Sala',
				A.CODAUTONU as 'IDQX',
				B.CODCONCEC as IDSALA,
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
				case A.PRINCIPAL when 1 then 'SI' else 'NO' end AS 'Principal',
				A.PRINCIPAL as QXPRINCIPAL,
				J.UFUCODIGO,
				J.UFUTIPUNI, 
				Rtrim(J.UFUDESCRI) AS 'Unidad Funcional',
				case A.ORIGENQX when 1 then A.NUMINGRES else I.NUMINGRES END As NUMINGRES,
				A.ORIGENQX, 
				case A.ORIGENQX when 1 then 'Ambulatoria' else 'Hospitalaria' end as 'ORIGENQXDescripcion',
				'Servicio: '+ RTRIM(E.codserips) +' - '+RTRIM(E.desserips)+ char(10)+'Profesional: ' + RTRIM(F.CODPROSAL) +' - '+RTRIM(F.NOMMEDICO)+ char(10)+'Especialidad:' + Rtrim(G.CODESPECI) + ' - ' + Rtrim(G.DESESPECI) + char(10)+'Principal: ' + case A.PRINCIPAL when 1 then 'SI' else 'NO' end + char(10)+'Origen Cirugía: ' + case A.ORIGENQX when 1 then 'Ambulatoria' else 'Hospitalaria' end  AS 'Contenido',
				A.ESTADOFARM as 'EstadoFarmacia',
				Rtrim(CE.CODCENATE) + ' - ' + Rtrim(CE.NOMCENATE ) as CentroAtencion,
				rtrim(F.CODPROSAL) as CODPROSAL,
				rtrim(A.CODSERIPS) as CODSERIPS,
				case A.NUMAUTORI when null then '--No Aplica--' when '' then '--No Aplica--' else Rtrim(A.NUMAUTORI) end as 'NUMAUTORI',
				rtrim(A.OTROSMATERIALES) as 'OTROSMATERIALES',
				EN.NOMENTIDA AS 'Entidad', A.OBSERVACION AS 'Observaciones', 
				A.OTROSMATERIALES AS 'Otros materiales', 
				A.OTROSAYUDANTES AS 'Otros ayudantes'
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
			INNER JOIN INENTIDAD EN with(nolock) ON EN.CODENTIDA = C.CODENTIDA
		WHERE A.CODAUTONU = @id or A.IDPADRE = @id

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento almacenado que recupera el detalle completo de una cirugía programada (o su grupo de procedimientos relacionados) a partir de un identificador único de programación quirúrgica. Consolida información del paciente (cédula, nombre, sexo, fecha de nacimiento, tipo de documento, régimen), la sala quirúrgica asignada, el profesional y especialidad responsable, el procedimiento CUPS, la prioridad, el estado del proceso quirúrgico (programada, en sala, en recuperación, anulada, etc.), el origen de la cirugía (ambulatoria u hospitalaria), el número de ingreso hospitalario cuando aplica, la unidad funcional, el centro de atención, la entidad aseguradora, autorización, materiales adicionales, ayudantes y el usuario que realizó la asignación. Se utiliza para alimentar reportes, órdenes quirúrgicas y pantallas de gestión de salas de cirugía en el módulo de agendamiento quirúrgico de Indigo Vie Cloud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_AGE_Cirugias';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_AGE_Cirugias';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consulta consolidada de las cirugías programadas (y sus hijas por jerarquía) con datos del paciente, profesional, sala, procedimiento, prioridad, estado y origen, para alimentar reportes de agendamiento quirúrgico.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_AGE_Cirugias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El identificador recibido debe corresponder a una cirugía existente (CODAUTONU) o al padre de un grupo de cirugías (IDPADRE) en AGEPROGQX; Las tablas maestras (sala, paciente, procedimiento, profesional, especialidad, usuario, unidad funcional, centro atención y entidad) deben tener los códigos referenciados, dado el uso de INNER JOIN; El paciente debe tener una entidad asociada (CODENTIDA) válida en INENTIDAD', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_AGE_Cirugias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se devuelven cirugías cuyo identificador coincide con el filtro o que pertenecen a la misma jerarquía padre (CODAUTONU = @id o IDPADRE = @id); Toda cirugía retornada debe tener paciente, sala, procedimiento, profesional, especialidad, usuario que asignó, unidad funcional, centro de atención y entidad existentes (INNER JOIN); El vínculo con un ingreso hospitalario es opcional (LEFT JOIN con HCORDPROQ y ADINGRESO); El sexo solo se reporta como Masculino/Femenino si IPSEXOPAC ∈ {1,2}; otros valores quedan sin descripción; Los tipos de identificación válidos son 1-7 y 9 (no contempla 8); Los tipos de usuario válidos son 0,1,2,3,5,6,7 (no contempla 4)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_AGE_Cirugias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cirugía programada; Paciente; Ingreso hospitalario; Autorización; Procedimiento (CUPS/IPS); Profesional de la salud; Especialidad; Sala quirúrgica; Centro de atención; Unidad funcional; Entidad (aseguradora); Identidad de género; Tipo de usuario (régimen); Tipo de identificación; Prioridad quirúrgica; Estado farmacia; Origen de cirugía (ambulatoria/hospitalaria)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_AGE_Cirugias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando A.CODAUTONU = @Id o A.IDPADRE = @Id, retorna las cirugías programadas con descripciones traducidas (sexo, tipo identificación, tipo usuario, prioridad, estado, origen) y datos relacionados de paciente, sala, profesional, especialidad, unidad funcional, centro de atención, entidad e ingreso', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_AGE_Cirugias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ORIGENQX = 1 → Se considera cirugía Ambulatoria y se usa el NUMINGRES del registro de la cirugía else Se considera Hospitalaria y se usa el NUMINGRES proveniente del ingreso vinculado a la orden quirúrgica (HCORDPROQ→ADINGRESO); si PRINCIPAL = 1 → Se marca la cirugía como principal (SI) else Se marca como no principal (NO); si NUMAUTORI es NULL o cadena vacía → Se reporta ''--No Aplica--'' en el número de autorización else Se reporta el número de autorización tal cual; si CODESTPQX entre 0 y 6 → Se traduce a estado clínico-quirúrgico (Programada, Admitido, Sala de espera, Sala quirúrgica, Recuperación, Alta, Anulado); si PRISERIPS entre 1 y 4 → Se traduce a Emergencia/Urgencia/Normal/Definir Conducta else Se asume ''Programada''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_AGE_Cirugias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.TypeGenderIdentity', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_AGE_Cirugias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGEPROGQX; dbo.AGENSALAC; dbo.INPACIENT; dbo.INCUPSIPS; dbo.INPROFSAL; dbo.INESPECIA; dbo.SEGusuaru; dbo.HCORDPROQ; dbo.ADINGRESO; dbo.INUNIFUNC; dbo.ADCENATEN; dbo.INENTIDAD', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_AGE_Cirugias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_AGE_Cirugias';
-- GO
