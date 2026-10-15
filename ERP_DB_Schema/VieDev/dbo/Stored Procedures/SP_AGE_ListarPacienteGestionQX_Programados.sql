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
					when 2 then CASE A.AUTOHCORDPROQ 
							         when  null then (select H.NUMINGRES  from HCORDPRON H where H.AUTO = A.AUTOHCORDPRON)
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
		WHERE CONVERT(VARCHAR(20),FECHORAIN,103) = @FechaFiltro AND GRUPO_C.CODIGO IN (SELECT Value FROM dbo.SplitString(@Grupo)) 




END