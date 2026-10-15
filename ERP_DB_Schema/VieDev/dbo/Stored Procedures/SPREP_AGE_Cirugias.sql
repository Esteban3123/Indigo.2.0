

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