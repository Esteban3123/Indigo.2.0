
-- =============================================
-- Author:		Juan David Patiño Cabrera.
-- Create date: 29/02/2016
-- Description:	SP que lista los pacientes Que Tiene Citas Oncologia.
-- =============================================
CREATE PROCEDURE [dbo].[SPENF_ListarPacientesAgendaOncologica]
	(
@CentroAtencion Char(10),
@UnidadFuncional char(20),
@TipoUnidadFuncional int,
@TipoTratamiento as int,
@Fecha as date,
@Usuario as varchar(20)

--@CentroAtencion Char(10) = '01',
--@UnidadFuncional char(20) = '038',
--@TipoUnidadFuncional int = '13',
--@TipoTratamiento as int =  '1',
--@Fecha as date = '09/11/2016',
--@Usuario as varchar(20) = '999'
--TIPOTRATA =  1 -> Quimio 2-> Radio 
     
)
AS
BEGIN
	SET NOCOUNT ON;		 

		SELECT DISTINCT CODAUTONU,TIPOTRATA, D.CODENTIDA,Rtrim(K.NOMENTIDA) as 'Nombre Entidad',D.UFUACTPAC,Rtrim(N.UFUDESCRI) as 'Unidad Funcional','' as 'Nombre Cama',P.NUMINGRES AS 'Ingreso',D.CODTIPPAC as TipoPoblacion ,'' AS Aislamiento,CAST('' AS CHAR(50)) AS Edad,RTRIM(C.IPNOMCOMP) AS 'Nombre Completo', IPFECNACI AS 'Fecha Nacimiento' ,RTRIM(NOMDIAGNO) AS 'Nombre Diagnos',A.CODDIAGNO , B.DESCRIPSAL,  A.IPCODPACI,  isnull(D.NUMINGRES,'') as NUMINGRES,
	TIPOINGRE = CASE WHEN D.TIPOINGRE = 1 THEN 'AMBULATORIO' WHEN D.TIPOINGRE = 2 THEN 'HOSPITALARIO' END,  
	E.DESCREQUI as 'Equipo Tratamiento',  E.ID AS IDEQUIP,  CODTIPCIT = CASE WHEN A.CODTIPCIT = 0 THEN 'PRIMERA VEZ' WHEN A.CODTIPCIT = 1 THEN 'CONTROL'  WHEN A.CODTIPCIT = 2 THEN 'POS OPERATORIO' END, F.CODACTMED,  F.DESACTMED As 'Actividad',  HORINIC = FORMAT(A.FECHORAIN, 'HH:mm:ss'),  A.FECHORAIN, null AS 'Codigo Cama', 'false' AS TrasladoMedicamentos, null as  Consecutivo,  null as UFUCODIGO, A.RELCITAINGRE as 'CitaConIngreso',ISNULL(CONVERT(VARCHAR(20),CICLO),Case WHEN FASE = 1 Then 'Prefase o citoreducción inicial' WHEN FASE =  2 Then 'Inducción'  WHEN FASE = 3 Then 'Intensificación'  WHEN FASE = 4 Then 'Consolidación' WHEN FASE = 5 Then 'Reinducción' WHEN FASE = 6 Then 'Mantenimiento' WHEN FASE = 7 Then 'Mantenimiento largo o final' WHEN FASE = 8 Then 'Otra fase de quimioterapia' end) AS CAMPO  , A.OBSERVACI as 'Observacion Cita', G.ID as 'IdCabeceraProductos'
	 FROM AGASICITA A  
	 INNER JOIN AGENSALAC B ON A.IDSALA = B.CODCONCEC AND B.TIPOTRATA = @TipoTratamiento
	 INNER JOIN INPACIENT C ON A.IPCODPACI = C.IPCODPACI  
	 INNER JOIN AGEQUIPTRA E ON A.IDEQUIPOTRA = E.ID  
	 INNER JOIN AGACTIMED F ON A.CODACTMED = F.CODACTMED  
	 INNER JOIN ADACTIVID L ON C.CODACTIVI = L.codactivi  
	 LEFT  JOIN ADRELFICHAON  P ON  A.IPCODPACI = P.IPCODPACI AND P.ESTADO = 1 
	 LEFT  JOIN ADINGRESO D ON A.IPCODPACI = D.IPCODPACI  AND P.NUMINGRES = D.NUMINGRES 
	 LEFT JOIN  INDIAGNOS Z ON Z.CODDIAGNO = A.CODDIAGNO  
	 LEFT JOIN INENTIDAD K ON K.CODENTIDA = D.CODENTIDA 
	 LEFT JOIN INUNIFUNC N ON N.UFUCODIGO = D.UFUACTPAC
	 LEFT JOIN AGFARMEONCOC G ON G.IDCITA  = A.CODAUTONU 
	 WHERE A.CODESTCIT = 0 
	AND A.TIPSOLICITU = 3 
	AND (A.FECHCANCELA IS NUll OR A.FECHCANCELA = '') 
	AND A.IDSALA IN (SELECT  B.CODCONCEC FROM INAUTORIU A INNER JOIN AGENSALAC B ON A.PKVALORCO = B.CODIGSALA WHERE A.CODUSUARI = @Usuario AND A.PKTIPOCON = 8 AND B.ESTADO = 1
					UNION  select  B.CODCONCEC from INAUTORIG  A INNER JOIN AGENSALAC B ON A.PKVALORCO = B.CODIGSALA WHERE A.CODGRUPOU  in (select CODGRUPOU  from SEGusuaru where CODUSUARI = @Usuario) AND A.PKTIPOCON = 8 AND B.ESTADO = 1 ) 
	AND A.CODCENATE = @centroAtencion AND FORMAT(A.FECHORAIN, 'dd/MM/yyyy') = @Fecha
	AND A.RELCITAINGRE = 0 --no hospitalizados. no relaciona ingreso en cita. el ingreso sale de la ficha oncologica.
UNION ALL
	SELECT DISTINCT  CODAUTONU,TIPOTRATA,D.CODENTIDA,Rtrim(K.NOMENTIDA) as 'Nombre Entidad',D.UFUACTPAC,Rtrim(N.UFUDESCRI) as 'Unidad Funcional',Rtrim(O.DESCCAMAS) as 'Nombre Cama',D.NUMINGRES AS 'Ingreso',D.CODTIPPAC as TipoPoblacion ,dbo.TipoAislamiento(O.CODAISLAM) AS Aislamiento,CAST('' AS CHAR(50)) AS Edad,RTRIM(C.IPNOMCOMP) AS 'Nombre Completo', IPFECNACI AS 'Fecha Nacimiento',RTRIM(NOMDIAGNO) AS 'Nombre Diagnos',A.CODDIAGNO,B.DESCRIPSAL,  A.IPCODPACI,  isnull(D.NUMINGRES,'') as NUMINGRES,   
	TIPOINGRE = CASE WHEN D.TIPOINGRE = 1 THEN 'AMBULATORIO' WHEN D.TIPOINGRE = 2 THEN 'HOSPITALARIO' END,  
	E.DESCREQUI as 'Equipo Tratamiento',  E.ID AS IDEQUIP,  CODTIPCIT = CASE WHEN A.CODTIPCIT = 0 THEN 'PRIMERA VEZ' WHEN A.CODTIPCIT = 1 THEN 'CONTROL'  WHEN A.CODTIPCIT = 2 THEN 'POS OPERATORIO' END, F.CODACTMED,  F.DESACTMED As 'Actividad',  HORINIC = FORMAT(A.FECHORAIN, 'HH:mm:ss'),  A.FECHORAIN,  O.CODICAMAS AS 'Codigo Cama',isnull(O.CAMTRAMED,'false') AS TrasladoMedicamentos,O.CODCONCEC AS Consecutivo,  O.UFUCODIGO, A.RELCITAINGRE as 'CitaConIngreso',ISNULL(CONVERT(VARCHAR(20),CICLO),Case WHEN FASE = 1 Then 'Prefase o citoreducción inicial' WHEN FASE =  2 Then 'Inducción'  WHEN FASE = 3 Then 'Intensificación'  WHEN FASE = 4 Then 'Consolidación' WHEN FASE = 5 Then 'Reinducción' WHEN FASE = 6 Then 'Mantenimiento' WHEN FASE = 7 Then 'Mantenimiento largo o final' WHEN FASE = 8 Then 'Otra fase de quimioterapia' end) AS CAMPO , A.OBSERVACI as 'Observacion Cita',G.ID as 'IdCabeceraProductos'
	 FROM AGASICITA A  
	 INNER JOIN AGENSALAC B ON A.IDSALA = B.CODCONCEC AND B.TIPOTRATA = @TipoTratamiento 
	 INNER JOIN INPACIENT C ON A.IPCODPACI = C.IPCODPACI  
	 INNER JOIN AGEQUIPTRA E ON A.IDEQUIPOTRA = E.ID  
	 INNER JOIN AGACTIMED F ON A.CODACTMED = F.CODACTMED  
	 INNER JOIN ADACTIVID L ON C.CODACTIVI = L.codactivi  
	 LEFT JOIN  ADINGRESO D ON A.IPCODPACI = D.IPCODPACI  AND A.NUMINGRES = D.NUMINGRES 
	 INNER JOIN CHREGESTA CH on CH.NUMINGRES = A.NUMINGRES AND CH.REGESTADO = 1 
	 INNER JOIN CHCAMASHO O ON  O.CODICAMAS =CH.CODICAMAS  AND O.ESTADCAMA = '2'
	 LEFT JOIN INDIAGNOS Z ON Z.CODDIAGNO = A.CODDIAGNO
	 LEFT JOIN INENTIDAD K ON K.CODENTIDA = D.CODENTIDA 
	 LEFT JOIN INUNIFUNC N ON N.UFUCODIGO = D.UFUACTPAC
	 LEFT JOIN AGFARMEONCOC G ON G.IDCITA  = A.CODAUTONU  
	WHERE A.CODESTCIT = 0 
	AND A.TIPSOLICITU = 3 
	AND (A.FECHCANCELA IS NUll OR A.FECHCANCELA = '') 
	AND A.IDSALA IN (SELECT  B.CODCONCEC FROM INAUTORIU A INNER JOIN AGENSALAC B ON A.PKVALORCO = B.CODIGSALA WHERE A.CODUSUARI = @Usuario AND A.PKTIPOCON = 8 AND B.ESTADO = 1
					UNION  select  B.CODCONCEC from INAUTORIG  A INNER JOIN AGENSALAC B ON A.PKVALORCO = B.CODIGSALA WHERE A.CODGRUPOU  in (select CODGRUPOU  from SEGusuaru where CODUSUARI = @Usuario) AND A.PKTIPOCON = 8 AND B.ESTADO = 1 ) 
	AND A.CODCENATE = @centroAtencion AND FORMAT(A.FECHORAIN, 'dd/MM/yyyy') = @Fecha 
	AND A.RELCITAINGRE = 1  --hopitalizado y relaciona el ingreso en la cita

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes con citas oncológicas (quimioterapia o radioterapia) programadas para una fecha, centro de atención y tipo de tratamiento específicos. Combina información de citas (AGASICITA), salas oncológicas (AGENSALAC), datos del paciente (INPACIENT), equipos de tratamiento (AGEQUIPTRA), actividades médicas (AGACTIMED) e ingresos/admisiones (ADINGRESO), diferenciando entre pacientes ambulatorios (no hospitalizados, vinculados por ficha oncológica en línea ADRELFICHAON) y pacientes hospitalizados (con cama asignada en CHCAMASHO). Devuelve por cada paciente: nombre completo, diagnóstico CIE-10, entidad aseguradora, unidad funcional, número de ingreso, tipo de ingreso (ambulatorio u hospitalario), equipo de tratamiento, actividad médica, hora de inicio de la cita, ciclo o fase de quimioterapia, nombre de la cama (cuando aplica), indicador de traslado de medicamentos e identificador de cabecera de productos farmacéuticos oncológicos. Se usa en el módulo de enfermería oncológica para gestionar la agenda diaria de tratamientos oncológicos y coordinar la atención de cada paciente en la sesión correspondiente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPENF_ListarPacientesAgendaOncologica';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPENF_ListarPacientesAgendaOncologica';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las citas de pacientes en agenda oncológica (quimio/radio) para una fecha, centro de atención y tipo de tratamiento, separando ambulatorios (sin ingreso relacionado) y hospitalizados (con cama asignada), filtradas por las salas autorizadas al usuario.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPENF_ListarPacientesAgendaOncologica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El usuario debe tener autorización (individual en INAUTORIU o por grupo en INAUTORIG/SEGusuaru) sobre la sala con PKTIPOCON=8 y la sala debe estar activa (ESTADO=1).; La sala (AGENSALAC) debe corresponder al tipo de tratamiento solicitado (TIPOTRATA = @TipoTratamiento).; La cita debe estar activa: CODESTCIT=0, TIPSOLICITU=3 y sin fecha de cancelación.; Para hospitalizados, debe existir registro de estado de cama vigente (CHREGESTA.REGESTADO=1) y la cama debe estar ocupada (CHCAMASHO.ESTADCAMA=''2'').', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPENF_ListarPacientesAgendaOncologica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan citas no canceladas (CODESTCIT=0, TIPSOLICITU=3 y FECHCANCELA nula o vacía).; Solo se incluyen citas cuya sala esté autorizada al usuario (autorización individual o por grupo) con PKTIPOCON=8.; El filtro de fecha se hace sobre FECHORAIN formateada como ''dd/MM/yyyy'' contra el parámetro recibido.; Las salas consideradas siempre coinciden con el tipo de tratamiento parámetro (quimio=1, radio=2).; Para el flujo hospitalizado, la cama mostrada siempre está en estado ocupada (''2'') y vinculada al registro de estado vigente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPENF_ListarPacientesAgendaOncologica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Agenda oncológica; Quimioterapia; Radioterapia; Ciclo de tratamiento; Fases de quimioterapia (Prefase/citoreducción, Inducción, Intensificación, Consolidación, Reinducción, Mantenimiento); Ficha oncológica; Tipo de cita (Primera vez, Control, Pos operatorio); Ingreso ambulatorio vs hospitalario; Cama hospitalaria y aislamiento; Equipo de tratamiento; Autorización de usuarios y grupos sobre salas; Diagnóstico; Entidad (asegurador); Unidad funcional; Traslado de medicamentos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPENF_ListarPacientesAgendaOncologica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] AGASICITA: Devuelve dos conjuntos unidos (UNION ALL): cuando RELCITAINGRE=0 retorna pacientes ambulatorios usando el ingreso de la ficha oncológica (ADRELFICHAON.ESTADO=1); cuando RELCITAINGRE=1 retorna pacientes hospitalizados con la cama actualmente ocupada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPENF_ListarPacientesAgendaOncologica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si A.RELCITAINGRE = 0 → Trae ingreso desde ADRELFICHAON (ficha oncológica con ESTADO=1) cruzando luego con ADINGRESO; no incluye datos de cama. else Cuando RELCITAINGRE = 1, obtiene el ingreso directamente de la cita (A.NUMINGRES) y agrega información de cama vigente desde CHREGESTA/CHCAMASHO.; si D.TIPOINGRE = 1 → Etiqueta el ingreso como ''AMBULATORIO''. else Si TIPOINGRE=2 lo etiqueta como ''HOSPITALARIO''.; si A.CODTIPCIT IN (0,1,2) → Traduce el tipo de cita a ''PRIMERA VEZ'', ''CONTROL'' o ''POS OPERATORIO'' respectivamente.; si CICLO IS NULL y FASE entre 1 y 8 → Mapea la FASE a su descripción de fase de quimioterapia (Prefase, Inducción, Intensificación, Consolidación, Reinducción, Mantenimiento, Mantenimiento largo o final, Otra fase). else Si CICLO no es nulo, devuelve el número de ciclo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPENF_ListarPacientesAgendaOncologica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.TipoAislamiento', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPENF_ListarPacientesAgendaOncologica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGASICITA; dbo.AGENSALAC; dbo.INPACIENT; dbo.AGEQUIPTRA; dbo.AGACTIMED; dbo.ADACTIVID; dbo.ADRELFICHAON; dbo.ADINGRESO; dbo.INDIAGNOS; dbo.INENTIDAD; dbo.INUNIFUNC; dbo.AGFARMEONCOC; dbo.CHREGESTA; dbo.CHCAMASHO; dbo.INAUTORIU; dbo.INAUTORIG; dbo.SEGusuaru', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPENF_ListarPacientesAgendaOncologica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPENF_ListarPacientesAgendaOncologica';
-- GO
