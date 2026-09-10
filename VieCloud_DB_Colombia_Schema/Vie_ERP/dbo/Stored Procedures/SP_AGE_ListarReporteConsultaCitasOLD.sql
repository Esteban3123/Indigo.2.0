
-- =============================================
-- Author:		<Yezid Garcia Medina, Desarrollador Junior>
-- Create date: <17 Septiembre de 2019>
-- Description:	<Listar Reporte Consulta Citas>
-- =============================================
CREATE PROCEDURE [dbo].[SP_AGE_ListarReporteConsultaCitasOLD]
(
	@Centro varchar(50) ,
	@FechaInicio Datetime ,
	@FechaFin Datetime , 
	@TipoFecha varchar(5)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	   	
    -- Insert statements for procedure here
	
	SELECT 

		A.CODAUTONU AS 'Autonumerico' ,
		CASE
			WHEN B.CODESPECI IS NULL THEN ' - ' 
			WHEN B.CODESPECI IS NOT NULL THEN RTRIM(B.CODESPECI) 		
		END AS 'Cod. Especialidad' , 
		CASE
			WHEN B.DESESPECI IS NULL THEN ' - ' 
			WHEN B.DESESPECI IS NOT NULL THEN RTRIM(B.DESESPECI) 	
		END AS 'Especialidad' , 
		CONCAT( RTRIM(B.CODESPECI), ' - ', RTRIM(B.DESESPECI) ) AS 'Concatenado- Especialidad' ,
		CASE
			WHEN C.CODCENATE IS NULL THEN ' - ' 
			WHEN C.CODCENATE IS NOT NULL THEN RTRIM(C.CODCENATE) 	
		END AS 'Cod. Centro Atencion', 
		CASE
			WHEN C.NOMCENATE IS NULL THEN ' - ' 
			WHEN C.NOMCENATE IS NOT NULL THEN RTRIM(C.NOMCENATE) 
		END AS 'Centro Atencion' , 		
		CONCAT(	RTRIM(C.CODCENATE), ' - ', RTRIM(C.NOMCENATE) ) AS 'Concatenado- Centro Atencion' , 
		CASE
			WHEN UF1.UFUCODIGO IS NULL AND UF2.UFUCODIGO IS NULL THEN ' - '
			WHEN UF1.UFUCODIGO IS NOT NULL THEN RTRIM(UF1.UFUCODIGO)
			WHEN UF2.UFUCODIGO IS NOT NULL THEN RTRIM(UF2.UFUCODIGO)			
		END AS 'Cod. Unidad Funcional',
		CASE
			WHEN UF1.UFUCODIGO IS NULL AND UF2.UFUCODIGO IS NULL THEN ' - '
			WHEN UF1.UFUCODIGO IS NOT NULL THEN RTRIM(UF1.UFUDESCRI)
			WHEN UF2.UFUCODIGO IS NOT NULL THEN RTRIM(UF2.UFUDESCRI)			
		END AS 'Unidad Funcional',				
		CASE
			WHEN UF1.UFUCODIGO IS NULL AND UF2.UFUCODIGO IS NULL THEN ' - '
			WHEN UF1.UFUCODIGO IS NOT NULL THEN CONCAT( RTRIM(UF1.UFUCODIGO), ' - ', RTRIM(UF1.UFUDESCRI) )
			WHEN UF2.UFUCODIGO IS NOT NULL THEN CONCAT( RTRIM(UF2.UFUCODIGO), ' - ', RTRIM(UF2.UFUDESCRI) )
		END AS 'Concatenado- Unidad Funcional',
		CASE
			WHEN D.IPCODPACI IS NULL THEN ' - ' 
			WHEN D.IPCODPACI IS NOT NULL THEN RTRIM(D.IPCODPACI)
		END AS 'Identificacion' ,
		CASE
			WHEN D.IPNOMCOMP IS NULL THEN ' - ' 
			WHEN D.IPNOMCOMP IS NOT NULL THEN RTRIM(D.IPNOMCOMP) 
		END AS 'Nombre Paciente' , 		
		CASE
			WHEN HEA.Id IS NULL AND ENT.CODENTIDA IS NULL THEN ' - '
			WHEN HEA.Id IS NOT NULL THEN RTRIM(HEA.Code)
			WHEN HEA.Id IS NULL AND ENT.CODENTIDA IS NOT NULL THEN RTRIM(ENT.CODENTIDA)			
		END AS 'Cod. Entidad',
		CASE
			WHEN HEA.Id IS NULL AND ENT.CODENTIDA IS NULL THEN ' - '
			WHEN HEA.Id IS NOT NULL THEN RTRIM(HEA.Name)
			WHEN HEA.Id IS NULL AND ENT.CODENTIDA IS NOT NULL THEN RTRIM(ENT.NOMENTIDA)			
		END AS 'Entidad',	
		CASE
			WHEN HEA.Id IS NULL AND ENT.CODENTIDA IS NULL THEN ' - '
			WHEN HEA.Id IS NOT NULL THEN CONCAT(RTRIM(HEA.Code), ' - ', RTRIM(HEA.Name))
			WHEN HEA.Id IS NULL AND ENT.CODENTIDA IS NOT NULL THEN CONCAT( RTRIM(ENT.CODENTIDA), ' - ', RTRIM(ENT.NOMENTIDA) )			
		END AS 'Concatenado- Entidad',	
		CASE
			WHEN E.CODPROSAL IS NULL THEN ' - ' 
			WHEN E.CODPROSAL IS NOT NULL THEN RTRIM(E.CODPROSAL) 
		END AS 'Cod. Profesional' ,		
		CASE
			WHEN E.CODPROSAL IS NULL THEN ' - ' 
			WHEN E.CODPROSAL IS NOT NULL THEN RTRIM(E.NOMMEDICO)
		END AS 'Profesional' ,		
		CASE
			WHEN E.CODPROSAL IS NULL THEN ' - ' 
			WHEN E.CODPROSAL IS NOT NULL THEN CONCAT( RTRIM(E.CODPROSAL), ' - ', RTRIM(E.NOMMEDICO) )
		END AS 'Concatenado- Profesional' ,
		CASE
			WHEN A.FECHORAIN IS NULL THEN NULL 
			WHEN A.FECHORAIN IS NOT NULL THEN  A.FECHORAIN
		END AS 'F. Inicial cita' ,
		CASE
			WHEN A.FECHORAFI IS NULL THEN NULL 
			WHEN A.FECHORAFI IS NOT NULL THEN A.FECHORAFI 
		END AS 'F. Final cita' ,
		CASE
			WHEN J.CODIGOCON IS NULL THEN ' - ' 
			WHEN J.CODIGOCON IS NOT NULL THEN RTRIM(J.CODIGOCON)
		END AS 'Cod. Consultorio' ,
		CASE
			WHEN J.CODIGOCON IS NULL THEN ' - ' 
			WHEN J.CODIGOCON IS NOT NULL THEN RTRIM(J.DESCRICON)
		END AS 'Consultorio' ,
		CASE
			WHEN J.CODIGOCON IS NULL THEN ' - ' 
			WHEN J.CODIGOCON IS NOT NULL THEN CONCAT( RTRIM(J.CODIGOCON) , ' - ', RTRIM(J.DESCRICON) ) 
		END AS 'Concatenado- Consultorio' ,
		CASE
			WHEN F.CODACTMED IS NULL THEN ' - ' 
			WHEN F.CODACTMED IS NOT NULL THEN RTRIM(F.CODACTMED)
		END AS 'Cod. Actividad' ,
		CASE
			WHEN F.CODACTMED IS NULL THEN ' - ' 
			WHEN F.CODACTMED IS NOT NULL THEN RTRIM(F.DESACTMED)
		END AS 'Nom. Actividad' ,
		CASE
			WHEN F.CODACTMED IS NULL THEN ' - ' 
			WHEN F.CODACTMED IS NOT NULL THEN CONCAT( RTRIM(F.CODACTMED) , ' - ', RTRIM(F.DESACTMED) )
		END AS 'Concatenado- Actividad' , 
		CASE
			WHEN A.CODTIPSOL IS NULL THEN ' - '
			WHEN A.CODTIPSOL = '0' THEN 'Presencial'
			WHEN A.CODTIPSOL = '1' THEN 'Telefónica'
		END AS 'Forma de solicitud', 
		CASE
			WHEN A.CODTIPCIT IS NULL THEN ' - '
			WHEN A.CODTIPCIT = '0' THEN 'Primera Vez'
			WHEN A.CODTIPCIT = '1' THEN 'Control'
			WHEN A.CODTIPCIT = '2' THEN 'Pos Operatorio'				
			WHEN A.CODTIPCIT = '3' THEN 'Cita Web' 
		END	AS 'Tipo cita' ,		
		CASE
			WHEN A.CODESTCIT IS NULL THEN ' - '
			WHEN A.CODESTCIT = '0' THEN 'Asignada'
			WHEN A.CODESTCIT = '1' THEN 'Cumplida'
			WHEN A.CODESTCIT = '2' THEN 'Incumplida'
			WHEN A.CODESTCIT = '3' THEN 'PreAsignada'
			WHEN A.CODESTCIT = '4' THEN 'Cancelada'
		END AS 'Estado cita' ,
		CASE
			WHEN A.CITAEXTRA IS NULL THEN ' - '
			WHEN A.CITAEXTRA = 0 THEN 'No'
			WHEN A.CITAEXTRA = 1 THEN 'Si'			
		END AS 'Cita extra' , 
		CASE
			WHEN A.OBSERVACI IS NULL OR RTRIM(A.OBSERVACI) = '' THEN ' - ' 
			WHEN A.OBSERVACI IS NOT NULL THEN RTRIM(A.OBSERVACI)
		END AS 'Observacion cita' , 
		CASE
			WHEN G.CODUSUARI IS NULL THEN ' - ' 
			WHEN G.CODUSUARI IS NOT NULL THEN RTRIM(G.CODUSUARI)
		END AS 'Cod. Usuario Registro' ,
		CASE
			WHEN G.CODUSUARI IS NULL THEN ' - ' 
			WHEN G.CODUSUARI IS NOT NULL THEN RTRIM(G.NOMUSUARI)
		END AS 'Nom. Usuario Registro' ,
		CASE
			WHEN G.CODUSUARI IS NULL THEN ' - ' 
			WHEN G.CODUSUARI IS NOT NULL THEN CONCAT( RTRIM(G.CODUSUARI), ' - ', RTRIM(G.NOMUSUARI) )
		END AS 'Concatenado- Usuario Registro' ,
		CASE
			WHEN A.FECREGSIS IS NULL THEN NULL 
			WHEN A.FECREGSIS IS NOT NULL THEN A.FECREGSIS
		END AS 'F. registro DB' , 
		CASE
			WHEN A.OBSCITPRE IS NULL OR RTRIM(A.OBSCITPRE) = '' THEN ' - ' 
			WHEN A.OBSCITPRE IS NOT NULL THEN RTRIM(A.OBSCITPRE)
		END AS 'Observacion cita preasignada' ,
		CASE
			WHEN A.FECITADES IS NULL THEN NULL 
			WHEN A.FECITADES IS NOT NULL THEN A.FECITADES
		END AS 'F. deseada cita' ,
		CASE
			WHEN A.TIPSOLICITU IS NULL THEN ' - '
			WHEN A.TIPSOLICITU = 1 THEN 'Cita Medica'
			WHEN A.TIPSOLICITU = 2 THEN 'Cita Apoyo Diagnostico'
			WHEN A.TIPSOLICITU = 3 THEN 'Cita Tratamiento Especiales'
		END	AS 'Tipo de solicitud', 
		CASE
			WHEN K.CODIGSALA IS NULL THEN ' - ' 
			WHEN K.CODIGSALA IS NOT NULL THEN RTRIM(K.CODIGSALA)
		END AS 'Cod. Sala' ,
		CASE
			WHEN K.CODIGSALA IS NULL THEN ' - ' 
			WHEN K.CODIGSALA IS NOT NULL THEN RTRIM(K.DESCRIPSAL)
		END AS 'Nombre Sala' ,
		CASE
			WHEN K.CODIGSALA IS NULL THEN ' - ' 
			WHEN K.CODIGSALA IS NOT NULL THEN CONCAT( RTRIM(K.CODIGSALA) , ' - ', RTRIM(K.DESCRIPSAL) ) 
		END AS 'Concatenado- Sala',		
		CASE
			WHEN L.CODEQUIPO IS NULL THEN ' - ' 
			WHEN L.CODEQUIPO IS NOT NULL THEN RTRIM(L.CODEQUIPO) 
		END AS 'Cod. Equipo Tratamiento' , 
		CASE
			WHEN L.CODEQUIPO IS NULL THEN ' - ' 
			WHEN L.CODEQUIPO IS NOT NULL THEN RTRIM(L.DESCREQUI) 
		END AS 'Equipo Tratamiento' ,
		CASE
			WHEN L.CODEQUIPO IS NULL THEN ' - ' 
			WHEN L.CODEQUIPO IS NOT NULL THEN CONCAT( RTRIM(L.CODEQUIPO) , ' - ', RTRIM(L.DESCREQUI) )
		END AS 'Concatenado- Equipo Tratamiento',
		CASE
			WHEN H.CODUSUARI IS NULL THEN ' - ' 
			WHEN H.CODUSUARI IS NOT NULL THEN RTRIM(H.CODUSUARI)
		END AS 'Cod. Usuario Cancela' ,
		CASE
			WHEN H.CODUSUARI IS NULL THEN ' - ' 
			WHEN H.CODUSUARI IS NOT NULL THEN RTRIM(H.NOMUSUARI) 
		END AS 'Usuario Cancela' ,
		CASE
			WHEN H.CODUSUARI IS NULL THEN ' - ' 
			WHEN H.CODUSUARI IS NOT NULL THEN CONCAT( RTRIM(H.CODUSUARI), ' - ', RTRIM(H.NOMUSUARI) ) 
		END AS 'Concatenado- Usuario Cancela' ,
		CASE
			WHEN A.FECHCANCELA IS NULL THEN NULL 
			WHEN A.FECHCANCELA IS NOT NULL THEN A.FECHCANCELA 
		END AS 'Fecha Cancelacion' , 
		CASE
			WHEN P.DESCAUCAN IS NULL THEN ' - ' 
			WHEN P.DESCAUCAN IS NOT NULL THEN RTRIM(P.DESCAUCAN)
		END AS 'Causa de Cancelacion' ,
		CASE
			WHEN A.OBSCAUCAN IS NULL OR RTRIM(A.OBSCAUCAN) = '' THEN ' - ' 
			WHEN A.OBSCAUCAN IS NOT NULL THEN RTRIM(A.OBSCAUCAN)
		END AS 'Observacion Cancelacion' ,		
		CASE
			WHEN M.CODSERIPS IS NULL THEN ' - ' 
			WHEN M.CODSERIPS IS NOT NULL THEN RTRIM(M.CODSERIPS)
		END AS 'Cod. CUPS',
		CASE
			WHEN M.CODSERIPS IS NULL THEN ' - ' 
			WHEN M.CODSERIPS IS NOT NULL THEN RTRIM(M.DESSERIPS) 
		END AS 'CUPS' ,
		CASE
			WHEN M.CODSERIPS IS NULL THEN ' - ' 
			WHEN M.CODSERIPS IS NOT NULL THEN CONCAT( RTRIM(M.CODSERIPS), ' - ', RTRIM(M.DESSERIPS) ) 
		END AS 'Concatenado- CUPS' ,		
		CASE
			WHEN T.Id IS NULL THEN ' - ' 
			WHEN T.Id IS NOT NULL THEN RTRIM(T.Code)
		END AS 'Cod. Descripcion relacionada' ,
		CASE
			WHEN T.Id IS NULL THEN ' - ' 
			WHEN T.Id IS NOT NULL THEN RTRIM(T.Name)
		END AS 'Descripcion relacionada' ,
		CASE
			WHEN T.Id IS NULL THEN ' - ' 
			WHEN T.Id IS NOT NULL THEN CONCAT( RTRIM(T.Code), ' - ', RTRIM(T.Name) ) 
		END AS 'Concatenado- Descripcion relacionada' ,
		CASE
			WHEN A.NUMINGRES IS NULL OR RTRIM(A.NUMINGRES) = '' THEN ' - ' 			
			WHEN A.NUMINGRES IS NOT NULL THEN RTRIM(A.NUMINGRES) 			
		END AS 'Ingreso' ,	
		CASE
			WHEN N.CODDIAGNO IS NULL THEN ' - ' 
			WHEN N.CODDIAGNO IS NOT NULL THEN RTRIM(N.CODDIAGNO) 
		END AS 'Cod. Diagnostico' ,
		CASE
			WHEN N.CODDIAGNO IS NULL THEN ' - ' 
			WHEN N.CODDIAGNO IS NOT NULL THEN RTRIM(N.NOMDIAGNO) 
		END AS 'Diagnostico' ,
		CASE
			WHEN N.CODDIAGNO IS NULL THEN ' - ' 
			WHEN N.CODDIAGNO IS NOT NULL THEN CONCAT( RTRIM(N.CODDIAGNO), ' - ', RTRIM(N.NOMDIAGNO) ) 
		END AS 'Concatenado- Diagnostico' ,
		CASE
			WHEN A.TIPTRATAMIENTO IS NULL THEN ' - '
			WHEN A.TIPTRATAMIENTO = 1 THEN 'Quimioterapia'
			WHEN A.TIPTRATAMIENTO = 2 THEN 'RadioTerapia'
			WHEN A.TIPTRATAMIENTO = 3 THEN 'Diálisis'	
			WHEN A.TIPTRATAMIENTO = 4 THEN 'Braquiterapia'				
		END AS 'Tipo Tratamiento' ,
		CASE
			WHEN Q.NOMBRE IS NULL THEN ' - ' 
			WHEN Q.NOMBRE IS NOT NULL THEN RTRIM(Q.NOMBRE)
		END AS 'RIAS' , 
		CASE
			WHEN A.FECHAOFERTADA IS NULL THEN NULL 
			WHEN A.FECHAOFERTADA IS NOT NULL THEN A.FECHAOFERTADA
		END AS 'Fecha Ofertada' ,
		CASE
			WHEN A.CODCAUINA IS NULL THEN ' - ' 
			WHEN A.CODCAUINA IS NOT NULL THEN RTRIM(A.CODCAUINA) 
		END AS 'Causa Inatencion' ,
		CASE
			WHEN A.OBSCAUINA IS NULL OR RTRIM(A.OBSCAUINA) = '' THEN ' - ' 
			WHEN A.OBSCAUINA IS NOT NULL THEN RTRIM(A.OBSCAUINA) 
		END AS 'Observacion Inatencion' ,
		CASE
			WHEN I.CODUSUARI IS NULL THEN ' - ' 
			WHEN I.CODUSUARI IS NOT NULL THEN RTRIM(I.CODUSUARI) 
		END AS 'Cod. Usuario Registra Inatencion' ,
		CASE
			WHEN I.CODUSUARI IS NULL THEN ' - ' 
			WHEN I.CODUSUARI IS NOT NULL THEN RTRIM(I.NOMUSUARI) 
		END AS 'Usuario Registra Inatencion' ,
		CASE
			WHEN I.CODUSUARI IS NULL THEN ' - ' 
			WHEN I.CODUSUARI IS NOT NULL THEN CONCAT( RTRIM(I.CODUSUARI), ' - ', RTRIM(I.NOMUSUARI) )
		END AS 'Concatenado- Usuario Registra Inatencion' ,
		CASE
			WHEN A.FECHAINA IS NULL THEN NULL 
			WHEN A.FECHAINA IS NOT NULL THEN A.FECHAINA 
		END AS 'Fecha Registro Inatencion' , 
		CASE
			WHEN A.CONFASIST IS NULL THEN ' - '
			WHEN A.CONFASIST = '1' THEN 'Confirmada'
			WHEN A.CONFASIST = '2' THEN 'Cancelada'
			WHEN A.CONFASIST = '3' THEN 'Sin Definir'
		END AS 'Confirmar Asistencia', 	
	    CASE
			WHEN  A.MODALIDAD IS NULL THEN ' - '
			WHEN  A.MODALIDAD = 0 THEN 'Presencial'
			WHEN  A.MODALIDAD = 1 THEN 'Teleconsulta'
		END AS 'Modalidad' ,
		CASE
			WHEN R.Id IS NULL THEN ' - ' 
			WHEN R.Id IS NOT NULL THEN CONCAT('Ciclo: ' + Rtrim(R.CICLO) +'/' + (SELECT TOP 1 RTRIM(CICLOS) FROM EHR.HCORDQUIMIO where ID = R.IDHCORDQUIMIO  )  ,' ', '  Día: ' + Rtrim(R.DIA) +'/'+ (SELECT TOP 1 RTRIM(DIA) FROM EHR.HCORDCICLOSD WHERE IDHCORDQUIMIO  = R.IDHCORDQUIMIO AND CICLO = R.CICLO AND ADMISTRADIACASA = 0 AND ESTADODIA <> 3 ORDER BY DIA DESC) ) 
		END AS 'Intervalo' 
	FROM 
	 AGASICITA AS A with (nolock) INNER JOIN ADCENATEN AS C with (nolock) ON A.CODCENATE = C.CODCENATE
		INNER JOIN INPACIENT AS D with (nolock) ON A.IPCODPACI = D.IPCODPACI
		LEFT JOIN INESPECIA AS B with (nolock)  ON A.CODESPECI = B.CODESPECI		
		LEFT JOIN INPROFSAL AS E with (nolock)
		ON A.CODPROSAL = E.CODPROSAL 
		LEFT JOIN AGACTIMED AS F with (nolock)
		ON A.CODACTMED = F.CODACTMED
		INNER JOIN SEGusuaru AS G with (nolock)
		ON A.CODUSUASI = G.CODUSUARI
		LEFT JOIN SEGusuaru AS H with (nolock)
		ON A.CANCELUSU = H.CODUSUARI
		LEFT JOIN SEGusuaru AS I with (nolock)
		ON A.CODUSUINA = I.CODUSUARI
		LEFT JOIN AGCONSULT  AS J with (nolock)
		ON A.CODIGOCON = J.CODIGOCON AND A.CODCENATE = J.CODCENATE
		LEFT JOIN AGENSALAC AS K with (nolock)
		ON A.IDSALA = K.CODCONCEC AND A.CODCENATE = K.CODCENATE
		LEFT JOIN AGEQUIPTRA AS L with (nolock)
		ON A.IDEQUIPOTRA = L.ID
		LEFT JOIN INCUPSIPS AS M with (nolock)
		ON A.CODSERIPS = M.CODSERIPS
		LEFT JOIN INDIAGNOS AS N with (nolock)
		ON A.CODDIAGNO = N.CODDIAGNO
		LEFT JOIN RIASCUPS AS O with (nolock)
		ON A.IDRIASCUPS = O.ID
		LEFT JOIN RIAS AS Q with (nolock)
		ON O.IDRIAS = Q.ID
		LEFT JOIN AGCAUCANC AS P with (nolock)
		ON A.CODCAUCAN = P.CODCAUCAN
		LEFT JOIN INUNIFUNC AS UF1 with (nolock) 
		ON J.UFUCODIGO = UF1.UFUCODIGO
		LEFT JOIN INUNIFUNC AS UF2 with (nolock)  ON K.UFUCODIGO = UF2.UFUCODIGO
		LEFT JOIN INENTIDAD AS ENT with (nolock) ON D.CODENTIDA = ENT.CODENTIDA
		LEFT JOIN Contract.HealthAdministrator AS HEA with (nolock) ON  A.GENCONENTITY = HEA.Id
		LEFT JOIN EHR.HCORDCICLOSD AS R with (nolock) ON A.IDHCORDCICLOSD = R.ID		
		LEFT JOIN Contract.CUPSEntityContractDescriptions AS S with (nolock) ON	A.IDDESCRIPCIONRELACIONADA = S.Id
		LEFT JOIN Contract.ContractDescriptions AS T with (nolock) ON S.ContractDescriptionId = T.Id
	WHERE 

		A.CODCENATE IN (SELECT Value FROM dbo.SplitString(@Centro)) 
		AND (
		CASE 
			WHEN @TipoFecha = 'False' THEN FECREGSIS 
			WHEN @TipoFecha = 'True' THEN FECHORAIN 
		END )
		BETWEEN @FechaInicio AND @FechaFin	

		ORDER BY A.CODAUTONU ASC

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte de consulta de citas médicas agendadas en un rango de fechas y por centro de atención. Integra información de la cita (AGASICITA) con datos del paciente (INPACIENT), el profesional de salud (INPROFSAL), la especialidad (INESPECIA), el tipo de actividad médica (AGACTIMED), el tipo de consultorio (AGCONSULT), la sala de atención (AGENSALAC) y los usuarios del sistema (SEGusuaru) que registraron, cancelaron o inactivaron la cita. Permite filtrar por fecha inicial o final de la cita según el tipo de fecha indicado, y muestra para cada cita su estado (asignada, cumplida, incumplida, cancelada), tipo (primera vez, control, posoperatorio, web), forma de solicitud (presencial o telefónica), entidad aseguradora del paciente, unidad funcional y observaciones, siendo útil para reportería operativa de agendamiento, control de ausentismo y seguimiento de la agenda médica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AGE_ListarReporteConsultaCitasOLD';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AGE_ListarReporteConsultaCitasOLD';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte detallado de citas agendadas filtrado por uno o varios centros de atención y un rango de fechas (de registro o de inicio de cita), enriqueciendo la información con datos de paciente, profesional, entidad, consultorio, sala, equipo, diagnóstico, CUPS, RIAS y tratamiento oncológico.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListarReporteConsultaCitasOLD';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro de centros debe ser una cadena parseable por dbo.SplitString que retorne uno o más códigos de centro de atención existentes en AGASICITA.CODCENATE.; El parámetro de tipo de fecha debe tener el valor ''True'' (filtrar por fecha/hora inicial de la cita) o ''False'' (filtrar por fecha de registro en sistema); cualquier otro valor invalida el filtro de fechas.; El rango de fechas (inicio y fin) debe estar definido para aplicar el BETWEEN.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListarReporteConsultaCitasOLD';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan citas cuyo centro de atención esté en la lista parseada del parámetro y cuya fecha (registro o inicio según parámetro) caiga dentro del rango.; Se exige existencia obligatoria de centro de atención (ADCENATEN), paciente (INPACIENT) y usuario que asignó la cita (SEGusuaru) por los INNER JOIN; las citas sin estos datos no aparecen.; Los valores nulos o cadenas vacías en campos textuales se normalizan a '' - '' para presentación.; Las fechas nulas se preservan como NULL (no se reemplazan por texto).; Para el cálculo de Intervalo solo se consideran días de ciclo no administrados en casa (ADMISTRADIACASA=0) y con estado distinto de 3.; Todas las lecturas se realizan con WITH (NOLOCK), aceptando lecturas sucias para no bloquear la operación.; La prioridad de resolución de Entidad es Contract.HealthAdministrator sobre INENTIDAD del paciente.; La prioridad de resolución de Unidad Funcional es la del consultorio sobre la de la sala.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListarReporteConsultaCitasOLD';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve un único conjunto de resultados con las citas que cumplan el filtro de centro y rango de fechas, ordenadas ascendentemente por el autonumérico de la cita.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListarReporteConsultaCitasOLD';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @TipoFecha = ''True'' → El BETWEEN se aplica sobre FECHORAIN (fecha/hora de inicio de la cita) else Si @TipoFecha = ''False'', el BETWEEN se aplica sobre FECREGSIS (fecha de registro en el sistema); si CODTIPSOL = ''0'' / ''1'' → Forma de solicitud se etiqueta como ''Presencial'' o ''Telefónica'' respectivamente; si CODTIPCIT in (''0'',''1'',''2'',''3'') → Tipo de cita se mapea a ''Primera Vez'', ''Control'', ''Pos Operatorio'' o ''Cita Web''; si CODESTCIT in (''0''..''4'') → Estado de la cita se traduce a ''Asignada'',''Cumplida'',''Incumplida'',''PreAsignada'' o ''Cancelada''; si TIPSOLICITU in (1,2,3) → Tipo de solicitud: ''Cita Medica'', ''Cita Apoyo Diagnostico'' o ''Cita Tratamiento Especiales''; si TIPTRATAMIENTO in (1..4) → Tipo de tratamiento oncológico: ''Quimioterapia'', ''RadioTerapia'', ''Diálisis'' o ''Braquiterapia''; si MODALIDAD = 0 / 1 → Modalidad se traduce a ''Presencial'' o ''Teleconsulta''; si CONFASIST in (''1'',''2'',''3'') → Confirmación de asistencia: ''Confirmada'', ''Cancelada'' o ''Sin Definir''; si CITAEXTRA = 0 / 1 → Indicador de cita extra: ''No'' o ''Si''; si HEA.Id no nulo (existe administradora de salud asociada por GENCONENTITY) → Toma código y nombre de entidad desde Contract.HealthAdministrator else Si no existe, toma CODENTIDA/NOMENTIDA desde INENTIDAD del paciente; si ambos faltan, retorna '' - ''; si UF1.UFUCODIGO no nulo (unidad funcional del consultorio) → Usa la unidad funcional del consultorio else Si UF1 es nulo y UF2 (unidad funcional de la sala) no, usa la de la sala; si ambas son nulas, retorna '' - ''; si R.Id no nulo (cita asociada a un día de ciclo de quimioterapia) → Calcula ''Intervalo'' concatenando ciclo actual/total (subconsulta TOP 1 sobre EHR.HCORDQUIMIO) y día actual/último día válido (subconsulta sobre EHR.HCORDCICLOSD con ADMISTRADIACASA=0 y ESTADODIA<>3 ordenando por DIA DESC)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListarReporteConsultaCitasOLD';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGASICITA; dbo.ADCENATEN; dbo.INPACIENT; dbo.INESPECIA; dbo.INPROFSAL; dbo.AGACTIMED; dbo.SEGusuaru; dbo.AGCONSULT; dbo.AGENSALAC; dbo.AGEQUIPTRA; dbo.INCUPSIPS; dbo.INDIAGNOS; dbo.RIASCUPS; dbo.RIAS; dbo.AGCAUCANC; dbo.INUNIFUNC; dbo.INENTIDAD; Contract.HealthAdministrator; EHR.HCORDCICLOSD; EHR.HCORDQUIMIO; Contract.CUPSEntityContractDescriptions; Contract.ContractDescriptions; dbo.SplitString', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListarReporteConsultaCitasOLD';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListarReporteConsultaCitasOLD';
-- GO
