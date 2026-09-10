

CREATE VIEW [dbo].[ViewAdmissionReport]
AS
SELECT DISTINCT 
	A.NUMINGRES AS 'NUMEROINGRESO', 
	A.IPCODPACI AS 'CODIGOPACIENTE',
	B.IPNOMCOMP AS 'NOMBRE DEL PACIENTE', 
	B.IPFECNACI AS 'FECHA DE NACIMIENTO CORTA',
	'' AS 'FECHA DE NACIMIENTO', 
	CASE B.IPSEXOPAC 
		WHEN 1 THEN 'MASCULINO' 
		ELSE 'FEMENINO' 
	END AS SEXO,
	CASE B.IPTIPODOC 
		WHEN 1 THEN 'CC' 
		WHEN 2 THEN 'CE' 
		WHEN 3 THEN 'TI' 
		WHEN 4 THEN 'RC' 
		WHEN 5 THEN 'PA' 
		WHEN 6 THEN 'AS' 
		WHEN 7 THEN 'MS' 
		WHEN 8 THEN 'NU' 
	END 'TIPO DOCUMENTO', 
	B.IPDIRECCI AS 'DIRECCION PACIENTE', 
	RTRIM(B.IPTELEFON) + ' - ' + RTRIM(B.IPTELMOVI) AS TELEFONO,
	CASE TIPOINGRE 
		WHEN 1 THEN 'Ambulatorio' 
		WHEN 2 THEN 'Hospitalario' 
	END AS 'TIPO DE INGRESO', 
	CASE IINGREPOR 
		WHEN 1 THEN 'Urgencias' 
		WHEN 2 THEN 'Derivado de Consulta Externa' 
		WHEN 3 THEN 'Nacido Hospital' 
		WHEN 4 THEN 'Remitido' 
		WHEN 5 THEN 'Hospitalización de Urgencias' 
	END AS 'INGRESODELPACIENTE', 
	CASE ITIPORIES 
		WHEN 1 THEN 'Ninguna' 
		WHEN 2 THEN 'Accidente de Transito' 
		WHEN 3 THEN 'Catastrofe' 
		WHEN 4 THEN 'Enfermedad General y Maternidad' 
		WHEN 5 THEN 'Accidente de Trabajo' 
		WHEN 6 THEN 'Atencion Inicial de Urgencias' 
		WHEN 7 THEN 'Atencion Inicial de Urgencias' 
		WHEN 8 THEN 'Otro Tipo de Accidente' 
		WHEN 9 THEN 'Lesion Por Agresion' 
		WHEN 10 THEN 'Lesion AutoInfligida' 
		WHEN 11 THEN 'Maltrato Fisico' 
		WHEN 12 THEN 'Promocion y Prevencion' 
		WHEN 13 THEN 'Otro' 
		WHEN 14 THEN 'Accidente Rabico' 
		WHEN 15 THEN 'Accidente Ofidico' 
		WHEN 16 THEN 'Sospecha de Abuso Sexual' 
		WHEN 17 THEN 'Sospecha de Violencia Sexual' 
		WHEN 18 THEN 'Sospecha de Maltrato Emocional' 
	END AS 'TIPODERIESGO', 
	coa.Name AS 'CAUSADELINGRESO', 
	A.CODENTIDA AS 'CODIGO DE ENTIDAD DE INGRESO',
	C.NOMENTIDA AS 'NOMBRE DE ENTIDAD DE INGRESO',
	B.CODENTIDA AS 'CODIGO DE ENTIDAD DEL PACIENTE',
	C1.NOMENTIDA AS 'NOMBRE DE ENTIDAD DEL PACIENTE',
	A.CODCONTRA AS 'CODIGO DEL CONTRATO DE EAPB',
	D.DESCONTRA AS 'NOMBRE CONTRATO',
	A.CODPANATE AS 'CODIGO PLAN DE BENEFICIOS',
	E.DESPLANBE AS 'PLAN DE BENEFICIOS' ,
	IFECHAING AS 'FECHA DE INGRESO',
	CASE ILIQUIDAC 
		WHEN 1 THEN 'Copago' 
		WHEN 2 THEN 'Cuota Moderadora' 
		WHEN 3 THEN 'No Aplica' 
		WHEN 4 THEN 'Copago / Cuota Moderadora' 
	END AS 'LIQUIDA PACIENTE',
	ICONTROLI AS 'CONTROL ADMINISTRATIVO', 
	A.CODCENATE AS 'CODIGO CENTRO DE ATENCION', 
	F.NOMCENATE AS 'CENTRO DE ATENCION', 
	A.UFUCODIGO AS 'CODIGO UNIDAD FUNCIONAL', 
	G.UFUDESCRI AS 'UNIDAD FUNCIONAL', 
	IAUTORIZA AS 'NUMERO DE AUTORIZACION', 
	CASE IESTADOIN 
		WHEN '  ' THEN 'Sin Confirmar Hoja de Trabajo' 
		WHEN 'F' THEN 'Confirmada Hoja de Trabajo' 
		WHEN 'A' THEN 'Anulado' 
	END AS 'ESTADO DEL INGRESO', 
	IINGRESOA AS 'NUMERO INGRESO ACCIDENTE DE TRANSITO', 
	ISOATVALO AS 'VALOR FACTURADO SOAT', 
	A.ISALCODIG AS 'CODIGO DEL SMLV', 
	J.ISALNOMBR AS 'NOMBRE DEL SMLV',
	INUMERORE AS 'NUMERO DE REMISION', 
	IFECHAREM AS 'FECHA DE REMISION', 
	IAUTORREM AS 'AUTORIZACION DE LA REMISION', 
	A.DEPMUNCOD AS 'MUNICIPIO DE LA IPS', 
	I.MUNNOMBRE AS 'NOMBRE MUNICIPIO DE LA IPS', 
	K.DSCRIPIPS AS 'IPS DE REMISION', 
	IOBSERVAC AS 'OBSERVACIONES', 
	IJUSTIFIC AS 'JUSTIFICACION ANULACION',
	B.CCCONTRAT AS 'CODIGO DEL CONTRATO DE EAPB DEL PACIENTE',
	D1.DESCONTRA AS 'NOMBRE CONTRATO DEL PACIENTE', 
	B.CPPLANBEN AS 'CODIGO PLAN DE BENEFICIOS DEL PACIENTE', 
	E1.DESPLANBE AS 'PLAN DE BENEFICIOS DEL PACIENTE',
	H.NIVDESCRI AS 'DESCRIPCION DEL NIVEL',
	CA.NUMCAMHOS AS 'CAMA', 
	CH.FECINIEST AS 'FECHAHOSPITALIZACION', 
	B.NUMCARPET AS 'NUMERO DE CARPETA',
	CGI.Code AS 'CODIGO GRUPO ATENCION INGRESO', 
	CGI.Name AS 'NOMBRE GRUPO ATENCION INGRESO',
	CGP.Code AS 'CODIGO GRUPO ATENCION PACIENTE', 
	CGP.Name AS 'NOMBRE GRUPO ATENCION PACIENTE', 
	RTRIM(AD.PRINOMBRE) + ' ' + RTRIM(AD.SEGNOMBRE) + ' ' + RTRIM(AD.PRIAPELLI) + ' ' + RTRIM(AD.SEGAPELLI) AS 'NOMBREACOMPANANTE',
	CASE AD.PARACOMPA 
		WHEN 1 THEN 'PADRE' 
		WHEN 2 THEN 'MADRE' 
		WHEN 3 THEN 'ESPOSO' 
		WHEN 4 THEN 'ESPOSA' 
		WHEN 5 THEN 'HIJO(A)' 
		WHEN 6 THEN 'HERMANO(A)' 
		WHEN 7 THEN 'ABUELO(A)' 
		WHEN 8 THEN 'TIO(A)' 
		WHEN 9 THEN 'PRIMO(A)' 
		WHEN 10 THEN 'SOBRINO(A)' 
		WHEN 11 THEN 'AMIGO(A)' 
		WHEN 12 THEN 'OTRO'
	END 'PARENTESCO', 
	AD.IPCODPACI AS 'CODIGOACOMPANANTE',
	A.CODUSUCRE AS 'CODIGO USARIO'
FROM ADINGRESO A 
LEFT OUTER JOIN INPACIENT B ON A.IPCODPACI= B.IPCODPACI
LEFT OUTER JOIN INENTIDAD C ON A.CODENTIDA = C.CODENTIDA
LEFT OUTER JOIN INENTIDAD C1 ON B.CODENTIDA = C1.CODENTIDA
LEFT OUTER JOIN CHREGESTA CH ON A.IPCODPACI = CH.IPCODPACI
LEFT OUTER JOIN CHCAMASHO CA ON CH.CODICAMAS = CA.CODICAMAS
LEFT OUTER JOIN ADACOMPAN AD ON B.IPCODPACI = AD.IPCODPACI
LEFT OUTER JOIN COCONTRAT D ON A.CODCONTRA = D.CODCONTRA
LEFT OUTER JOIN COCONTRAT D1 ON B.CCCONTRAT = D1.CODCONTRA
LEFT OUTER JOIN COPLANBEF E ON A.CODPANATE = E.CODPLANBE
LEFT OUTER JOIN COPLANBEF E1 ON B.CPPLANBEN = E1.CODPLANBE
INNER JOIN ADCENATEN F ON A.CODCENATE = F.CODCENATE
INNER JOIN INUNIFUNC G ON A.UFUCODIGO = G.UFUCODIGO
LEFT JOIN ADNIVELES H ON B.NIVCODIGO = H.NIVCODIGO
LEFT OUTER JOIN INMUNICIP I ON A.DEPMUNCOD = I.DEPMUNCOD
LEFT OUTER JOIN INSALARIM J ON A.ISALCODIG = J.ISALCODIG
LEFT OUTER JOIN ADCONTIPS K ON A.AIPSREMIS = K.CODIGOIPS
LEFT OUTER JOIN Contract.CareGroup CGI ON CGI.Id = A.GENCAREGROUP
LEFT OUTER JOIN Contract.CareGroup CGP ON CGP.Id = B.GENCAREGROUP
LEFT JOIN Causesofattention coa ON a.ICAUSAING = coa.Id
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de reporte completo de admisiones y ingresos de pacientes. Consolida en una sola consulta toda la información relevante de un episodio de atención: datos demográficos del paciente (nombre, documento, sexo, fecha de nacimiento, dirección, teléfono), tipo y causa del ingreso (urgencias, hospitalización, consulta externa, nacido en hospital, remitido), tipo de riesgo, entidad aseguradora o pagadora (EPS/ARS) tanto del ingreso como del paciente, contrato y plan de beneficios asociado, centro de atención y unidad funcional, cama hospitalaria asignada y fecha de hospitalización, información de acompañante y parentesco, número de autorización, estado del ingreso, datos de remisión (IPS de origen, número y fecha), nivel socioeconómico (SMLV), municipio, grupo de atención y observaciones. Integra las tablas de ingresos (ADINGRESO), pacientes (INPACIENT), entidades pagadoras (INENTIDAD), contratos (COCONTRAT), planes de beneficios (COPLANBEF), camas (CHCAMASHO), estados de estancia (CHREGESTA), acompañantes (ADACOMPAN) y maestros auxiliares de centros de atención, unidades funcionales, municipios, niveles y grupos de atención. Está diseñada para alimentar reportes administrativos y asistenciales de admisiones, cuadros de ocupación, auditoría de ingresos y conciliación con entidades pagadoras.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewAdmissionReport';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewAdmissionReport';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista consolidada de ingresos/admisiones de pacientes que decodifica catálogos y enlaza datos demográficos, contractuales, de hospitalización, remisión, acompañante y centro de atención para reportería administrativa.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existe registro de ingreso en ADINGRESO con centro de atención (ADCENATEN) y unidad funcional (INUNIFUNC) válidos, ya que estos joins son INNER.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen ingresos con centro de atención y unidad funcional existentes (INNER JOIN obligatorio).; El paciente, entidades, contratos, planes de beneficios, niveles, grupos de atención, acompañante, hospitalización, cama, municipio, SMLV e IPS de remisión son opcionales (LEFT JOIN).; El teléfono se presenta concatenando teléfono fijo y móvil separados por '' - ''.; El nombre del acompañante se construye con primer y segundo nombre y apellidos.; La entidad, contrato y plan de beneficios se reportan dos veces: el del ingreso y el del paciente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ingreso/Admisión; Paciente; Tipo de documento; Entidad EAPB; Contrato; Plan de beneficios; Tipo de ingreso (ambulatorio/hospitalario); Origen del ingreso; Tipo de riesgo (accidente de tránsito, laboral, P&P, violencia sexual, etc.); Causa de atención; Liquidación (copago/cuota moderadora); Centro de atención; Unidad funcional; Autorización; Estado de hoja de trabajo; SOAT; SMLV; Remisión a IPS; Hospitalización y cama; Grupo de atención (CareGroup); Acompañante y parentesco; Nivel del paciente; Municipio', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ViewAdmissionReport: Devuelve filas distintas combinando ADINGRESO con paciente, entidad, contrato, plan de beneficios, hospitalización, cama, acompañante, municipio, SMLV, IPS de remisión, niveles y grupos de atención.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si IPSEXOPAC = 1 → Sexo = MASCULINO else Sexo = FEMENINO; si IPTIPODOC entre 1..8 → Mapea a CC, CE, TI, RC, PA, AS, MS, NU respectivamente; si TIPOINGRE = 1 o 2 → Ambulatorio u Hospitalario; si IINGREPOR entre 1..5 → Clasifica origen del ingreso (Urgencias, Consulta Externa, Nacido Hospital, Remitido, Hospitalización de Urgencias); si ITIPORIES entre 1..18 → Clasifica tipo de riesgo (accidentes, enfermedad general, violencia, P&P, etc.); valores 6 y 7 se agrupan como ''Atención Inicial de Urgencias''; si ILIQUIDAC entre 1..4 → Define modalidad de liquidación al paciente: Copago, Cuota Moderadora, No Aplica, o ambas; si IESTADOIN = ''  '', ''F'' o ''A'' → Estado del ingreso: Sin Confirmar Hoja de Trabajo, Confirmada Hoja de Trabajo o Anulado; si PARACOMPA entre 1..12 → Asigna parentesco del acompañante (padre, madre, esposo(a), hijos, hermanos, abuelos, tíos, primos, sobrinos, amigos, otro)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO; dbo.INPACIENT; dbo.INENTIDAD; dbo.CHREGESTA; dbo.CHCAMASHO; dbo.ADACOMPAN; dbo.COCONTRAT; dbo.COPLANBEF; dbo.ADCENATEN; dbo.INUNIFUNC; dbo.ADNIVELES; dbo.INMUNICIP; dbo.INSALARIM; dbo.ADCONTIPS; Contract.CareGroup; dbo.Causesofattention', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionReport';
GO
