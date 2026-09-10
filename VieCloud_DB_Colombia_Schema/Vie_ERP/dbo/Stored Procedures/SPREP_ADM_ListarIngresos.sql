-- Stored Procedure

CREATE PROCEDURE [dbo].[SPREP_ADM_ListarIngresos]
(
@NumeroIngreso Char(10),
@VersionERP int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
IF @VersionERP=1
    -- Insert statements for procedure here
SELECT A.NUMINGRES AS 'NUMERO DE INGRESO', A.IPCODPACI AS 'CODIGO DEL PACIENTE',A.CODUSUCRE AS 'USUARIO INGRESO',B.IPNOMCOMP AS 'NOMBRE DEL PACIENTE', B.IPFECNACI AS 'FECHA DE NACIMIENTO CORTA','' AS 'FECHA DE NACIMIENTO' , CASE B.IPSEXOPAC WHEN 1 THEN 'MASCULINO' ELSE 'FEMENINO' END AS SEXO, [dbo].[TypeGenderIdentity](B.IdGenderIdentity) AS 'IDENTIDAD GENERO', CASE B.IPTIPODOC WHEN 1 THEN 'CC' WHEN 2 THEN 'CE' WHEN 3 THEN 'TI' WHEN 4 THEN 'RC' WHEN 5 THEN 'PA' WHEN 6 THEN 'AS' WHEN 7 THEN 'MS' WHEN 8 THEN 'NU' WHEN 9 THEN 'NV' WHEN 9 THEN 'NV' WHEN 10 THEN 'CD' WHEN 11 THEN 'SC' WHEN 12 THEN 'PE' END 'TIPO DOCUMENTO',iif(PA.Address is null,rtrim(B.IPDIRECCI),(dbo.Patient_Address(B.IPCODPACI))) AS 'DIRECCION PACIENTE', RTRIM(B.IPTELEFON) + ' - ' + RTRIM(B.IPTELMOVI) AS TELEFONO,CASE TIPOINGRE WHEN 1 THEN 'Ambulatorio' WHEN 2 THEN 'Hospitalario' END AS 'TIPO DE INGRESO', iif(IdEntryRoutesHealthServices is null, CASE IINGREPOR WHEN 1 THEN 'Urgencias' WHEN 2 THEN 'Consulta Externa' WHEN 3 THEN 'Nacido Hospital' WHEN 4 THEN 'Remitido' WHEN 5 THEN 'Hospitalización de Urgencias' end, dbo.Enterby(IdEntryRoutesHealthServices)) AS 'INGRESO DEL PACIENTE', 
CASE ITIPORIES WHEN 1 THEN 'Enfermedad General y Maternidad' WHEN 2 THEN 'Accidente de Transito' WHEN 3 THEN 'Catastrofe' END AS 'TIPO DE RIESGO', dbo.Causeofattention(ICAUSAING) AS 'CAUSA DEL INGRESO', 
A.CODENTIDA AS 'CODIGO DE ENTIDAD DE INGRESO', C.NOMENTIDA AS 'NOMBRE DE ENTIDAD DE INGRESO',B.CODENTIDA AS 'CODIGO DE ENTIDAD DEL PACIENTE',C1.NOMENTIDA AS 'NOMBRE DE ENTIDAD DEL PACIENTE', A.CODCONTRA AS 'CODIGO DEL CONTRATO DE EAPB', D.DESCONTRA AS 'NOMBRE CONTRATO', A.CODPANATE AS 'CODIGO PLAN DE BENEFICIOS', E.DESPLANBE AS 'PLAN DE BENEFICIOS' ,IFECHAING AS 'FECHA DE INGRESO',CASE ILIQUIDAC WHEN 1 THEN 'Copago' WHEN 2 THEN 'Cuota Moderadora' WHEN 3 THEN 'No Aplica' WHEN 4 THEN 'Copago / Cuota Moderadora' END AS 'LIQUIDA PACIENTE',ICONTROLI AS 'CONTROL ADMINISTRATIVO', A.CODCENATE AS 'CODIGO CENTRO DE ATENCION', F.NOMCENATE AS 'CENTRO DE ATENCION', 
A.UFUCODIGO AS 'CODIGO UNIDAD FUNCIONAL', G.UFUDESCRI AS 'UNIDAD FUNCIONAL', IAUTORIZA AS 'NUMERO DE AUTORIZACION', CASE IESTADOIN WHEN '  ' THEN 'Sin Confirmar Hoja de Trabajo' WHEN 'F' THEN 'Confirmada Hoja de Trabajo' WHEN 'A' THEN 'Anulado' END AS 'ESTADO DEL INGRESO', IINGRESOA AS 'NUMERO INGRESO ACCIDENTE DE TRANSITO', ISOATVALO AS 'VALOR FACTURADO SOAT', A.ISALCODIG AS 'CODIGO DEL SMLV', J.ISALNOMBR AS 'NOMBRE DEL SMLV', INUMERORE AS 'NUMERO DE REMISION', IFECHAREM AS 'FECHA DE REMISION', IAUTORREM AS 'AUTORIZACION DE LA REMISION', A.DEPMUNCOD AS 'MUNICIPIO DE LA IPS', I.MUNNOMBRE AS 'NOMBRE MUNICIPIO DE LA IPS', K.DSCRIPIPS AS 'IPS DE REMISION', IOBSERVAC AS 'OBSERVACIONES', IJUSTIFIC AS 'JUSTIFICACION ANULACION',
B.CCCONTRAT AS 'CODIGO DEL CONTRATO DE EAPB DEL PACIENTE', D1.DESCONTRA AS 'NOMBRE CONTRATO DEL PACIENTE', B.CPPLANBEN AS 'CODIGO PLAN DE BENEFICIOS DEL PACIENTE', E1.DESPLANBE AS 'PLAN DE BENEFICIOS DEL PACIENTE',H.NIVDESCRI AS 'DESCRIPCION DEL NIVEL','' AS 'CAMA', '' AS 'FECHA HOSPITALIZACION', B.NUMCARPET AS 'NUMERO DE CARPETA'
,dbo.EDAD(B.IPFECNACI,GETDATE()) AS 'EDAD', DP.nomdepart AS 'DEPARTAMENTO PACIENTE', CM.Nombre AS 'NOMBRE COMUNA', RTRIM(AC.desactivi) AS 'CARGO PACIENTE', ISNULL(RTRIM(CR.CREDDESCRI),'') 'CREENCIA'
,dbo.EstadoCivilPaciente(B.IPESTADOC, B.IPSEXOPAC) AS 'ESTADO CIVIL', CG.Name AS 'NOMBRE GRUPO ATENCION', C.CODIGONIT AS 'NIT ENTIDAD INGRESO', RTRIM(C.ENTDIRECC) AS 'DIRECCION ENTIDAD INGRESO', RTRIM(C.ENTITELEF) AS 'TELEFONO ENTIDAD INGRESO'
,ISNULL(RTRIM(PS.NOMMEDICO),'') AS 'NOMBRE MEDICO', ISNULL(RTRIM(ES.DESESPECI),'') 'NOMBRE ESPECIALIDAD', ISNULL(RTRIM(DG.NOMDIAGNO),'') AS 'NOMBRE DIAGNOSTICO', b.IPFECNACI AS 'FECHA NACIMIENTO', Admissions.AdmissionModality(A.IdAdmissionModalities) AS 'MODALIDAD ATENCION'
FROM ADINGRESO A WITH(NOLOCK)
INNER JOIN INPACIENT B WITH(NOLOCK)  ON A.IPCODPACI= B.IPCODPACI
INNER JOIN INENTIDAD C WITH(NOLOCK)  ON A.CODENTIDA = C.CODENTIDA
INNER JOIN INENTIDAD C1 WITH(NOLOCK) ON B.CODENTIDA = C1.CODENTIDA
INNER JOIN COCONTRAT D WITH(NOLOCK)  ON A.CODCONTRA = D.CODCONTRA
INNER JOIN COCONTRAT D1 WITH(NOLOCK) ON B.CCCONTRAT = D1.CODCONTRA
INNER JOIN COPLANBEF E WITH(NOLOCK)  ON A.CODPANATE = E.CODPLANBE
INNER JOIN COPLANBEF E1 WITH(NOLOCK) ON B.CPPLANBEN = E1.CODPLANBE
INNER JOIN ADCENATEN F WITH(NOLOCK)  ON A.CODCENATE = F.CODCENATE
INNER JOIN INUNIFUNC G WITH(NOLOCK)  ON A.UFUCODIGO = G.UFUCODIGO
LEFT JOIN ADNIVELES H WITH(NOLOCK)  ON B.NIVCODIGO = H.NIVCODIGO
LEFT OUTER JOIN INMUNICIP I WITH(NOLOCK) ON A.DEPMUNCOD = I.DEPMUNCOD
LEFT OUTER JOIN INSALARIM J WITH(NOLOCK) ON A.ISALCODIG = J.ISALCODIG
LEFT OUTER JOIN ADCONTIPS K WITH(NOLOCK) ON A.AIPSREMIS = K.CODIGOIPS
LEFT OUTER JOIN INUBICACI U  WITH(NOLOCK) ON U.AUUBICACI = B.AUUBICACI
LEFT OUTER JOIN INMUNICIP M  WITH(NOLOCK) ON M.DEPMUNCOD = U.DEPMUNCOD
LEFT OUTER JOIN INDEPARTA DP  WITH(NOLOCK) ON DP.depcodigo = M.DEPCODIGO
LEFT OUTER JOIN ADCOMUNAS CM  WITH(NOLOCK) ON CM.Id = U.IDCOMUNA
LEFT OUTER JOIN ADACTIVID AC WITH(NOLOCK) ON AC.codactivi = B.CODACTIVI
LEFT OUTER JOIN ADCREDO CR WITH(NOLOCK) ON CR.CREDCODIGO = B.CREDCODIGO 
LEFT OUTER JOIN Contract.CareGroup CG WITH(NOLOCK) ON CG.Id = A.GENCAREGROUP
LEFT OUTER JOIN INPROFSAL PS WITH(NOLOCK) ON PS.CODPROSAL = A.CODPROING
LEFT OUTER JOIN INESPECIA ES WITH(NOLOCK) ON ES.CODESPECI = A.CODESPTRA
LEFT OUTER JOIN INDIAGNOS DG WITH(NOLOCK) ON DG.CODDIAGNO = A.CODDIAING
LEFT JOIN Admissions.PatientAddress PA With(Nolock) ON A.IPCODPACI = PA.IPCODPACI
 WHERE A.NUMINGRES=@NumeroIngreso 
 
 ELSE
 
     -- Insert statements for procedure here
SELECT cg.CareGroupType,A.NUMINGRES AS 'NUMERO DE INGRESO', A.IPCODPACI AS 'CODIGO DEL PACIENTE',A.CODUSUCRE AS 'USUARIO INGRESO',B.IPNOMCOMP AS 'NOMBRE DEL PACIENTE', B.IPFECNACI AS 'FECHA DE NACIMIENTO CORTA','' AS 'FECHA DE NACIMIENTO' , CASE B.IPSEXOPAC WHEN 1 THEN 'MASCULINO' ELSE 'FEMENINO' END AS SEXO, [dbo].[TypeGenderIdentity](B.IdGenderIdentity) AS 'IDENTIDAD GENERO', CASE B.IPTIPODOC WHEN 1 THEN 'CC' WHEN 2 THEN 'CE' WHEN 3 THEN 'TI' WHEN 4 THEN 'RC' WHEN 5 THEN 'PA' WHEN 6 THEN 'AS' WHEN 7 THEN 'MS' WHEN 8 THEN 'NU' WHEN 9 THEN 'NV' WHEN 9 THEN 'NV' WHEN 10 THEN 'CD' WHEN 11 THEN 'SC' WHEN 12 THEN 'PE' END 'TIPO DOCUMENTO', iif(PA.Address is null,Rtrim(B.IPDIRECCI),(dbo.Patient_Address(B.IPCODPACI))) AS 'DIRECCION PACIENTE', RTRIM(B.IPTELEFON) + ' - ' + RTRIM(B.IPTELMOVI) AS TELEFONO,CASE TIPOINGRE WHEN 1 THEN 'Ambulatorio' WHEN 2 THEN 'Hospitalario' END AS 'TIPO DE INGRESO', iif(IdEntryRoutesHealthServices is null, CASE IINGREPOR WHEN 1 THEN 'Urgencias' WHEN 2 THEN 'Consulta Externa' WHEN 3 THEN 'Nacido Hospital' WHEN 4 THEN 'Remitido' WHEN 5 THEN 'Hospitalización de Urgencias' end,dbo.Enterby(IdEntryRoutesHealthServices)) AS 'INGRESO DEL PACIENTE', 																																			
CASE ITIPORIES WHEN 1 THEN 'Ninguna' WHEN 2 THEN 'Accidente de Transito' WHEN 3 THEN 'Catastrofe' WHEN 4 THEN 'Enfermedad General y Maternidad' WHEN 5 THEN 'Accidente de Trabajo' WHEN 6 THEN 'Atencion Inicial de Urgencias' WHEN 7 THEN 'Atencion Inicial de Urgencias' WHEN 8 THEN 'Otro Tipo de Accidente' WHEN 9 THEN 'Lesion Por Agresion' WHEN 10 THEN 'Lesion AutoInfligida' WHEN 11 THEN 'Maltrato Fisico' WHEN 12 THEN 'Promocion y Prevencion' WHEN 13 THEN 'Otro' WHEN 14 THEN 'Accidente Rabico' WHEN 15 THEN 'Accidente Ofidico' WHEN 16 THEN 'Sopecha de Abuso Sexual' WHEN 17 THEN 'Sopecha de Violencia Sexual' WHEN 18 THEN 'Sopecha de Maltrato Emocional' WHEN 19 THEN 'Evento Terrorista' END AS 'TIPO DE RIESGO', dbo.Causeofattention(ICAUSAING) AS 'CAUSA DEL INGRESO', 
A.CODENTIDA AS 'CODIGO DE ENTIDAD DE INGRESO', HealthAdmission.Name AS 'NOMBRE DE ENTIDAD DE INGRESO',B.CODENTIDA AS 'CODIGO DE ENTIDAD DEL PACIENTE',isnull(HealthPatient.Name ,HealthAdmission.name) AS 'NOMBRE DE ENTIDAD DEL PACIENTE', A.CODCONTRA AS 'CODIGO DEL CONTRATO DE EAPB', D.DESCONTRA AS 'NOMBRE CONTRATO', A.CODPANATE AS 'CODIGO PLAN DE BENEFICIOS', E.DESPLANBE AS 'PLAN DE BENEFICIOS' ,IFECHAING AS 'FECHA DE INGRESO',CASE ILIQUIDAC WHEN 1 THEN 'Copago' WHEN 2 THEN 'Cuota Moderadora' WHEN 3 THEN 'No Aplica' WHEN 4 THEN 'Copago / Cuota Moderadora' END AS 'LIQUIDA PACIENTE',ICONTROLI AS 'CONTROL ADMINISTRATIVO', A.CODCENATE AS 'CODIGO CENTRO DE ATENCION', F.NOMCENATE AS 'CENTRO DE ATENCION', 
A.UFUCODIGO AS 'CODIGO UNIDAD FUNCIONAL', G.UFUDESCRI AS 'UNIDAD FUNCIONAL', IAUTORIZA AS 'NUMERO DE AUTORIZACION', CASE IESTADOIN WHEN '  ' THEN 'Sin Confirmar Hoja de Trabajo' WHEN 'F' THEN 'Confirmada Hoja de Trabajo' WHEN 'A' THEN 'Anulado' END AS 'ESTADO DEL INGRESO', IINGRESOA AS 'NUMERO INGRESO ACCIDENTE DE TRANSITO', ISOATVALO AS 'VALOR FACTURADO SOAT', A.ISALCODIG AS 'CODIGO DEL SMLV', J.ISALNOMBR AS 'NOMBRE DEL SMLV', INUMERORE AS 'NUMERO DE REMISION', IFECHAREM AS 'FECHA DE REMISION', IAUTORREM AS 'AUTORIZACION DE LA REMISION', A.DEPMUNCOD AS 'MUNICIPIO DE LA IPS', I.MUNNOMBRE AS 'NOMBRE MUNICIPIO DE LA IPS', K.DSCRIPIPS AS 'IPS DE REMISION', IOBSERVAC AS 'OBSERVACIONES', IJUSTIFIC AS 'JUSTIFICACION ANULACION',
B.CCCONTRAT AS 'CODIGO DEL CONTRATO DE EAPB DEL PACIENTE', D1.DESCONTRA AS 'NOMBRE CONTRATO DEL PACIENTE', B.CPPLANBEN AS 'CODIGO PLAN DE BENEFICIOS DEL PACIENTE', E1.DESPLANBE AS 'PLAN DE BENEFICIOS DEL PACIENTE',H.NIVDESCRI AS 'DESCRIPCION DEL NIVEL','' AS 'CAMA', '' AS 'FECHA HOSPITALIZACION', B.NUMCARPET AS 'NUMERO DE CARPETA'
,dbo.EDAD(B.IPFECNACI,GETDATE()) AS 'EDAD', DP.nomdepart AS 'DEPARTAMENTO PACIENTE', CM.Nombre AS 'NOMBRE COMUNA', RTRIM(AC.desactivi) AS 'CARGO PACIENTE', ISNULL(RTRIM(CR.CREDDESCRI),'') 'CREENCIA'
,dbo.EstadoCivilPaciente(B.IPESTADOC, B.IPSEXOPAC) AS 'ESTADO CIVIL', CG.Name AS 'NOMBRE GRUPO ATENCION', T.Nit AS 'NIT ENTIDAD INGRESO', AD.Addresss AS 'DIRECCION ENTIDAD INGRESO', P.Phone AS 'TELEFONO ENTIDAD INGRESO'
,ISNULL(RTRIM(PS.NOMMEDICO),'') AS 'NOMBRE MEDICO', ISNULL(RTRIM(ES.DESESPECI),'') 'NOMBRE ESPECIALIDAD', ISNULL(RTRIM(DG.NOMDIAGNO),'') AS 'NOMBRE DIAGNOSTICO', b.IPFECNACI AS 'FECHA NACIMIENTO', Admissions.AdmissionModality(A.IdAdmissionModalities) AS 'MODALIDAD ATENCION'
FROM ADINGRESO A with(nolock)
INNER JOIN INPACIENT B with(nolock) ON A.IPCODPACI= B.IPCODPACI
--INNER JOIN INENTIDAD C ON A.CODENTIDA = C.CODENTIDA
--INNER JOIN INENTIDAD C1 ON B.CODENTIDA = C1.CODENTIDA
INNER JOIN Contract.HealthAdministrator HealthAdmission with(nolock) ON HealthAdmission.Id = A.GENCONENTITY
left JOIN Contract.HealthAdministrator HealthPatient with(nolock) ON HealthPatient.Id = B.GENCONENTITY    
LEFT OUTER JOIN COCONTRAT D with(nolock) ON A.CODCONTRA = D.CODCONTRA
LEFT OUTER JOIN COCONTRAT D1 with(nolock) ON B.CCCONTRAT = D1.CODCONTRA
LEFT OUTER JOIN COPLANBEF E with(nolock) ON A.CODPANATE = E.CODPLANBE
LEFT OUTER JOIN COPLANBEF E1 with(nolock) ON B.CPPLANBEN = E1.CODPLANBE
INNER JOIN ADCENATEN F with(nolock) ON A.CODCENATE = F.CODCENATE
INNER JOIN INUNIFUNC G with(nolock) ON A.UFUCODIGO = G.UFUCODIGO
LEFT JOIN ADNIVELES H with(nolock) ON B.NIVCODIGO = H.NIVCODIGO
LEFT OUTER JOIN INMUNICIP I with(nolock) ON A.DEPMUNCOD = I.DEPMUNCOD
LEFT OUTER JOIN INSALARIM J with(nolock) ON A.ISALCODIG = J.ISALCODIG
LEFT OUTER JOIN ADCONTIPS K with(nolock) ON A.AIPSREMIS = K.CODIGOIPS 
LEFT OUTER JOIN INUBICACI U  WITH(NOLOCK) ON U.AUUBICACI = B.AUUBICACI
LEFT OUTER JOIN INMUNICIP M  WITH(NOLOCK) ON M.DEPMUNCOD = U.DEPMUNCOD
LEFT OUTER JOIN INDEPARTA DP  WITH(NOLOCK) ON DP.depcodigo = M.DEPCODIGO
LEFT OUTER JOIN ADCOMUNAS CM  WITH(NOLOCK) ON CM.Id = U.IDCOMUNA
LEFT OUTER JOIN ADACTIVID AC WITH(NOLOCK) ON AC.codactivi = B.CODACTIVI
LEFT OUTER JOIN ADCREDO CR WITH(NOLOCK) ON CR.CREDCODIGO = B.CREDCODIGO
LEFT OUTER JOIN [Contract].CareGroup CG WITH(NOLOCK) ON CG.Id = A.GENCAREGROUP
LEFT OUTER JOIN Common.ThirdParty T WITH(NOLOCK) ON T.Id = HealthAdmission.ThirdPartyId
LEFT OUTER JOIN (SELECT ROW_NUMBER() OVER (PARTITION BY AD.IdPerson order by AD.Id) ID_PARTICION, AD.IdPerson, AD.Addresss FROM Common.[Address] AD WITH(NOLOCK)) AD ON AD.ID_PARTICION = 1 AND AD.IDPERSON = T.PersonId
LEFT OUTER JOIN (SELECT ROW_NUMBER() OVER (PARTITION BY P.IdPerson order by P.Id) ID_PARTICION, P.IdPerson, P.Phone FROM Common.Phone P WITH(NOLOCK)) P ON P.ID_PARTICION = 1 AND P.IDPERSON = T.PersonId
LEFT OUTER JOIN INPROFSAL PS WITH(NOLOCK) ON PS.CODPROSAL = A.CODPROING
LEFT OUTER JOIN INESPECIA ES WITH(NOLOCK) ON ES.CODESPECI = A.CODESPTRA
LEFT OUTER JOIN INDIAGNOS DG WITH(NOLOCK) ON DG.CODDIAGNO = A.CODDIAING
LEFT JOIN Admissions.PatientAddress PA With(Nolock) ON A.IPCODPACI = PA.IPCODPACI
 WHERE A.NUMINGRES=@NumeroIngreso 
 
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que recupera el detalle completo de un ingreso o admisión de paciente a partir de su número de ingreso. Integra datos demográficos del paciente (nombre, documento o cédula, fecha de nacimiento, sexo, identidad de género, dirección, teléfono, estado civil, edad, carpeta), información del episodio de admisión (tipo de ingreso, vía de ingreso como urgencias u hospitalización, causa, tipo de riesgo, fecha de ingreso, estado del ingreso, autorización, observaciones), entidad pagadora y contrato EAPB tanto del ingreso como del paciente, plan de beneficios, centro de atención, unidad funcional, nivel socioeconómico, municipio de la IPS, IPS de remisión, médico y especialidad tratante, diagnóstico de ingreso, modalidad de atención y grupo de atención contractual. Utiliza un parámetro de versión del ERP para retornar variantes del conjunto de campos según la versión instalada, siendo la base principal las tablas ADINGRESO, INPACIENT, INENTIDAD, COCONTRAT, COPLANBEF, ADCENATEN e INUNIFUNC. Se usa principalmente en reportes de admisiones, ficha del paciente y consultas operativas de ingresos hospitalarios, de urgencias y consulta externa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_ADM_ListarIngresos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_ADM_ListarIngresos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en ADINGRESO con NUMINGRES igual al parámetro recibido; de lo contrario el resultado será vacío.; Para el flujo de versión ERP=1 deben existir registros relacionados en INENTIDAD (entidad ingreso y entidad paciente), COCONTRAT, COPLANBEF, ADCENATEN e INUNIFUNC, ya que se unen con INNER JOIN.; Para el flujo de versión ERP distinto de 1 debe existir la entidad administradora de salud en Contract.HealthAdministrator referenciada por A.GENCONENTITY, además de ADCENATEN e INUNIFUNC.; El paciente referenciado en ADINGRESO.IPCODPACI debe existir en INPACIENT (INNER JOIN).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_ADM_ListarIngresos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retorna información del ingreso cuyo NUMINGRES coincide exactamente con el parámetro recibido.; El procedimiento es de solo lectura: no realiza INSERT/UPDATE/DELETE.; El sexo se normaliza siempre como MASCULINO si IPSEXOPAC=1, en caso contrario FEMENINO (no admite valores nulos diferenciados).; El tipo de documento se traduce a códigos estándar (CC, CE, TI, RC, PA, AS, MS, NU, NV, CD, SC, PE).; El estado del ingreso se reporta como ''Sin Confirmar Hoja de Trabajo'' (espacios), ''Confirmada Hoja de Trabajo'' (F) o ''Anulado'' (A).; La dirección de la entidad y el paciente se obtienen tomando siempre el primer registro por persona (ROW_NUMBER ordenado por Id) en el flujo nuevo.; El catálogo de tipo de riesgo y la fuente de datos de la entidad varían según la versión del ERP, garantizando compatibilidad con dos modelos de datos.; Se usa NOLOCK en todas las tablas, asumiendo lecturas sucias aceptables para reportes.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_ADM_ListarIngresos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ingreso del paciente (admisión); Paciente; Tipo de documento de identidad; Identidad de género; Sexo; Tipo de ingreso (Ambulatorio/Hospitalario); Vía de ingreso (Urgencias, Consulta Externa, Remitido, etc.); Tipo de riesgo (Enfermedad General, Accidente de Tránsito, Catástrofe, Accidente de Trabajo, etc.); Causa del ingreso; Entidad responsable de pago / EAPB / EPS; Contrato EAPB; Plan de beneficios; Centro de atención; Unidad funcional; Autorización; Estado del ingreso (Sin Confirmar, Confirmada, Anulado); SOAT y valor facturado; SMLV (salario mínimo); Remisión y autorización de remisión; IPS de remisión; Municipio/Departamento/Comuna del paciente; Nivel de atención; Carpeta clínica; Edad del paciente; Estado civil; Creencia religiosa; Grupo de atención (CareGroup); Médico tratante / Especialidad / Diagnóstico de ingreso; Modalidad de atención; Liquidación de copago / cuota moderadora (+1 adicionales)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_ADM_ListarIngresos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Versión ERP igual a 1 → Consulta los datos del ingreso usando el modelo legado: entidades en INENTIDAD para entidad de ingreso y de paciente, y obtiene NIT, dirección y teléfono de la entidad directamente desde INENTIDAD. Aplica un catálogo reducido de TIPO DE RIESGO con 3 valores (Enfermedad General, Accidente de Tránsito, Catástrofe). else Consulta los datos del ingreso usando el modelo nuevo: las entidades de salud se obtienen de Contract.HealthAdministrator (vinculadas por GENCONENTITY), y el NIT, dirección y teléfono de la entidad de ingreso provienen de Common.ThirdParty, Common.Address y Common.Phone (tomando el primer registro por persona). Aplica un catálogo extendido de TIPO DE RIESGO con 19 valores y permite que entidad/contrato/plan del paciente sean opcionales (LEFT JOIN).; si PA.Address (Admissions.PatientAddress) es NULL → Usa la dirección del paciente almacenada en INPACIENT.IPDIRECCI else Usa la dirección obtenida vía dbo.Patient_Address(IPCODPACI); si IdEntryRoutesHealthServices es NULL → Mapea la vía de ingreso usando el catálogo legado IINGREPOR (Urgencias, Consulta Externa, Nacido Hospital, Remitido, Hospitalización de Urgencias) else Resuelve la vía de ingreso mediante la función dbo.Enterby(IdEntryRoutesHealthServices); si En el flujo nuevo, HealthPatient.Name (entidad del paciente) es NULL → Toma como nombre de entidad del paciente el nombre de la entidad de ingreso (HealthAdmission.Name) como fallback else Usa el nombre real de la entidad del paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_ADM_ListarIngresos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.TypeGenderIdentity; dbo.Patient_Address; dbo.Enterby; dbo.Causeofattention; dbo.EDAD; dbo.EstadoCivilPaciente; Admissions.AdmissionModality', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_ADM_ListarIngresos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO; dbo.INPACIENT; dbo.INENTIDAD; dbo.COCONTRAT; dbo.COPLANBEF; dbo.ADCENATEN; dbo.INUNIFUNC; dbo.ADNIVELES; dbo.INMUNICIP; dbo.INSALARIM; dbo.ADCONTIPS; dbo.INUBICACI; dbo.INDEPARTA; dbo.ADCOMUNAS; dbo.ADACTIVID; dbo.ADCREDO; Contract.CareGroup; dbo.INPROFSAL; dbo.INESPECIA; dbo.INDIAGNOS; Admissions.PatientAddress; Contract.HealthAdministrator; Common.ThirdParty; Common.Address; Common.Phone', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_ADM_ListarIngresos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_ADM_ListarIngresos';
-- GO
