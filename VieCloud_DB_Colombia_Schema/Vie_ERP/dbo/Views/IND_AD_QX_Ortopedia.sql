
CREATE VIEW [dbo].[IND_AD_QX_Ortopedia]
AS
SELECT        Z.UFUDESCRI AS UNIDAD_INGRESO, RTRIM(D.NOMCENATE) + ' - ' + RTRIM(E.UFUDESCRI) AS UBICACION, RTRIM(B.IPPRINOMB) 
                         + ' ' + RTRIM(B.IPSEGNOMB) AS NOMBRES, RTRIM(B.IPPRIAPEL) + ' ' + RTRIM(B.IPSEGAPEL) AS APELLIDOS, 
                         CASE IPTIPODOC WHEN 1 THEN 'CC' WHEN 2 THEN 'CE' WHEN 3 THEN 'TI' WHEN 4 THEN 'RC' WHEN 5 THEN 'PA' WHEN 6 THEN 'AS' WHEN 7 THEN 'MS' WHEN
                          8 THEN 'NU' END AS TIPODOCUMENTO, A.IPCODPACI AS DOCUMENTO, RTRIM(LTRIM(dbo.Edad(CONVERT(varchar, B.IPFECNACI, 105), CONVERT(varchar, 
                         GETDATE(), 105)))) AS Edad, B.IPTELEFON AS TELEFONO, B.IPTELMOVI AS CELULAR, 
                         CASE IPSEXOPAC WHEN '1' THEN 'Masculino' WHEN '2' THEN 'Femenino' END AS SEXO, 
                         CASE IPTIPOPAC WHEN 1 THEN 'CONTRIBUTIVO' WHEN 2 THEN 'SUBSIDIADO' WHEN 3 THEN 'VINCULADO' WHEN 4 THEN 'PARTICULAR' WHEN 5 THEN 'OTRO'
                          WHEN 6 THEN 'DESPLAZADO REG. CONTRIBUTIVO' WHEN 7 THEN 'DESPLAZADO REG. SUBSIDIADO' WHEN 8 THEN 'DESPLAZADO NO ASEGURADO' END AS
                          TIPOPACIENTE, RTRIM(F.NOMENTIDA) AS NOMBRE_ENTIDAD, RTRIM(A.SALACIRUG) AS SALA, A.FECHORINI AS FECHA_INICIO_CX, DATEPART(HH, 
                         A.FECHORINI) AS HoraInicio, DATEPART(mi, A.FECHORINI) AS MM_Inicio, A.FECHORFIN AS FECHA_FINAL_CX, DATEPART(HH, A.FECHORFIN) AS HoraFinal, 
                         DATEPART(mi, A.FECHORFIN) AS MMFinal, DATEDIFF(MINUTE, A.FECHORINI, A.FECHORFIN) AS TIEMPO_QUIRURGICO, 
                         CASE A.URGECIRUG WHEN 1 THEN 'SI' WHEN 0 THEN 'NO' END AS URGENTE, P.DESESPECI AS ESPECIALIDAD, PR.DESSERIPS AS PROCEDIMIENTO, 
                         RTRIM(A.DESPROCED) AS DESPROCED, A.CLASIFASA AS ASA, 
                         CASE TIPHERCIR WHEN '1' THEN 'Limpia' WHEN '2' THEN 'Limpia-Contaminada' WHEN '3' THEN 'Contaminada' WHEN '4' THEN 'Sucia' END AS HERIDA, 
                         CASE TIPOANEST WHEN '1' THEN 'Local' WHEN '2' THEN 'Regional' WHEN '3' THEN 'General' WHEN '4' THEN 'Combinada' END AS ANESTESIA, 
                         RTRIM(PRE.NOMDIAGNO) AS DX_PREOPERATORIO, RTRIM(POS.NOMDIAGNO) AS DX_POSOPERATORIO, RTRIM(M.NOMMEDICO) AS CIRUJANO, 
                         RTRIM(MM.NOMMEDICO) AS ANESTESIOLOGO, RTRIM(MN.NOMMEDICO) AS AYUDANTE, 
                         CASE C.TIPOINGRE WHEN '1' THEN 'Ambulatorio' WHEN '2' THEN 'Hospitalario' END AS TIPO_ING, HS.FECHISPAC AS FECHA_FOLIO, DATEPART(hh, 
                         HS.FECHISPAC) AS HH, DATEPART(mI, HS.FECHISPAC) AS MM
FROM            dbo.HCQXINFOR AS A INNER JOIN
                         dbo.INPACIENT AS B ON A.IPCODPACI = B.IPCODPACI LEFT OUTER JOIN
                             (SELECT        IPCODPACI, NUMEFOLIO, NUMINGRES, CODPROSAL
                               FROM            dbo.HCQXEQUIP
                               WHERE        (TIPOEQUIP = '3')) AS G ON G.IPCODPACI = A.IPCODPACI AND G.NUMINGRES = A.NUMINGRES AND 
                         G.NUMEFOLIO = A.NUMEFOLIO LEFT OUTER JOIN
                         dbo.INPROFSAL AS MM ON MM.CODPROSAL = G.CODPROSAL LEFT OUTER JOIN
                             (SELECT        IPCODPACI, NUMEFOLIO, NUMINGRES, CODPROSAL
                               FROM            dbo.HCQXEQUIP AS HCQXEQUIP_2
                               WHERE        (TIPOEQUIP = '2')) AS GG ON GG.IPCODPACI = A.IPCODPACI AND GG.NUMINGRES = A.NUMINGRES AND 
                         GG.NUMEFOLIO = A.NUMEFOLIO LEFT OUTER JOIN
                         dbo.INPROFSAL AS MN ON MN.CODPROSAL = GG.CODPROSAL LEFT OUTER JOIN
                         dbo.ADINGRESO AS C ON A.NUMINGRES = C.NUMINGRES INNER JOIN
                         dbo.ADCENATEN AS D ON A.CODCENATE = D.CODCENATE INNER JOIN
                         dbo.INUNIFUNC AS E ON A.UFUCODIGO = E.UFUCODIGO INNER JOIN
                         dbo.INUNIFUNC AS Z ON C.UFUCODIGO = Z.UFUCODIGO INNER JOIN
                         dbo.INENTIDAD AS F ON C.CODENTIDA = F.CODENTIDA INNER JOIN
                         dbo.INDIAGNOS AS PRE ON A.CODDIAPRE = PRE.CODDIAGNO INNER JOIN
                         dbo.INDIAGNOS AS POS ON A.CODDIAPOS = POS.CODDIAGNO INNER JOIN
                             (SELECT        CODPROSAL, IPCODPACI, NUMINGRES, NUMEFOLIO
                               FROM            dbo.HCQXEQUIP AS HCQXEQUIP_1
                               WHERE        (QXPRINCIP = 'True') AND (TIPOEQUIP = '1')) AS ES ON ES.IPCODPACI = C.IPCODPACI AND ES.NUMINGRES = C.NUMINGRES AND 
                         ES.NUMEFOLIO = A.NUMEFOLIO INNER JOIN
                         dbo.INPROFSAL AS M ON M.CODPROSAL = ES.CODPROSAL INNER JOIN
                         dbo.INESPECIA AS P ON P.CODESPECI = M.CODESPEC1 INNER JOIN
                             (SELECT        IPCODPACI, NUMINGRES, NUMEFOLIO, CODSERIPS
                               FROM            dbo.HCQXREALI
                               WHERE        (QXPRINCIP = 'True')) AS R ON R.IPCODPACI = C.IPCODPACI AND R.NUMINGRES = C.NUMINGRES AND 
                         R.NUMEFOLIO = A.NUMEFOLIO INNER JOIN
                         dbo.INCUPSIPS AS PR ON PR.CODSERIPS = R.CODSERIPS LEFT OUTER JOIN
                         dbo.HCHISPACA AS HS ON HS.NUMINGRES = A.NUMINGRES AND HS.IPCODPACI = A.IPCODPACI AND HS.NUMEFOLIO = A.NUMEFOLIO
WHERE        (A.CODCENATE = '001') AND (C.IFECHAING >= '01/07/2015') AND (C.IFECHAING <= '31/12/2015')
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de indicadores quirúrgicos de ortopedia que consolida la información completa de cada cirugía realizada en el centro de atención ''001'' durante el segundo semestre de 2015. Integra el informe quirúrgico (HCQXINFOR) con los datos demográficos del paciente (INPACIENT), el ingreso o admisión (ADINGRESO), la sala, fechas y duración de la intervención, el tipo de herida, la clasificación ASA y el tipo de anestesia. Identifica al equipo quirúrgico por rol: cirujano principal, anestesiólogo y ayudante (HCQXEQUIP / INPROFSAL), y expone el procedimiento principal CUPS (INCUPSIPS / HCQXREALI), los diagnósticos preoperatorio y posoperatorio en CIE-10 (INDIAGNOS), la especialidad del cirujano (INESPECIA), la entidad o aseguradora (INENTIDAD) y la ubicación del paciente (ADCENATEN / INUNIFUNC). Sirve como fuente de reportería gerencial y auditoría clínica de los actos quirúrgicos de ortopedia, permitiendo analizar tiempos quirúrgicos, urgencias, tipos de paciente por régimen y trazabilidad del equipo interviniente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'IND_AD_QX_Ortopedia';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'IND_AD_QX_Ortopedia';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Construye un indicador/listado de cirugías de ortopedia del segundo semestre de 2015 en el centro de atención ''001'', exponiendo datos del paciente, ingreso, procedimiento principal, equipo quirúrgico, diagnósticos y tiempos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_QX_Ortopedia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir informe quirúrgico (HCQXINFOR) asociado al ingreso, paciente y folio; Debe existir registro en HCQXEQUIP con QXPRINCIP=''True'' y TIPOEQUIP=''1'' que identifique al cirujano principal; Debe existir registro en HCQXREALI con QXPRINCIP=''True'' que identifique el procedimiento principal; El cirujano principal debe tener especialidad registrada (CODESPEC1) en INESPECIA; Los diagnósticos preoperatorio y postoperatorio deben existir en INDIAGNOS; El ingreso debe pertenecer al centro de atención ''001''; La fecha de ingreso debe estar entre 01/07/2015 y 31/12/2015', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_QX_Ortopedia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan cirugías del centro de atención ''001''; Solo se incluyen ingresos cuya fecha de ingreso esté entre 01/07/2015 y 31/12/2015; Cada cirugía reportada tiene un único cirujano principal (QXPRINCIP=''True'' y TIPOEQUIP=''1''); Cada cirugía reportada tiene un único procedimiento principal (QXPRINCIP=''True'' en HCQXREALI); El tiempo quirúrgico se calcula en minutos como diferencia entre fecha/hora de inicio y fin de la cirugía; Anestesiólogo y ayudante son opcionales (LEFT JOIN); cirujano principal y procedimiento principal son obligatorios (INNER JOIN); Los códigos de tipo de documento, sexo, tipo de paciente, urgencia, tipo de herida, tipo de anestesia y tipo de ingreso se traducen a descripciones legibles', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_QX_Ortopedia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; ingreso/admisión; cirugía; procedimiento quirúrgico (CUPS); diagnóstico preoperatorio; diagnóstico postoperatorio; cirujano principal; anestesiólogo; ayudante quirúrgico; especialidad médica; tipo de anestesia; clasificación ASA; tipo de herida quirúrgica; urgencia quirúrgica; centro de atención; unidad funcional; entidad/aseguradora; tipo de paciente (régimen); folio de historia clínica; tiempo quirúrgico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_QX_Ortopedia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando A.CODCENATE=''001'' y C.IFECHAING entre ''01/07/2015'' y ''31/12/2015'' → retorna fila con datos de cirugía, paciente, equipo, diagnósticos y procedimiento', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_QX_Ortopedia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TIPOEQUIP = ''3'' en HCQXEQUIP → Se considera al profesional como anestesiólogo del procedimiento; si TIPOEQUIP = ''2'' en HCQXEQUIP → Se considera al profesional como ayudante quirúrgico; si QXPRINCIP = ''True'' AND TIPOEQUIP = ''1'' en HCQXEQUIP → Se considera al profesional como cirujano principal y se vincula su especialidad; si QXPRINCIP = ''True'' en HCQXREALI → Se toma el procedimiento (CUPS/CODSERIPS) como procedimiento principal de la cirugía; si CODCENATE = ''001'' AND IFECHAING entre 2015-07-01 y 2015-12-31 → Se incluye el ingreso/cirugía en el indicador de ortopedia; en caso contrario se excluye', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_QX_Ortopedia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCQXINFOR; dbo.INPACIENT; dbo.HCQXEQUIP; dbo.INPROFSAL; dbo.ADINGRESO; dbo.ADCENATEN; dbo.INUNIFUNC; dbo.INENTIDAD; dbo.INDIAGNOS; dbo.INESPECIA; dbo.INCUPSIPS; dbo.HCQXREALI; dbo.HCHISPACA; dbo.Edad', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_QX_Ortopedia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_QX_Ortopedia';
GO
