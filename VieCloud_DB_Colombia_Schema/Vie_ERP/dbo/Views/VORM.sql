

CREATE VIEW [dbo].[VORM]
AS
SELECT     RISTRACAB.CODCONSEC AS Numero_de_orden, 'Solicitado' AS Estado_de_la_orden, 
                      CASE B.IPTIPODOC WHEN 0 THEN 'CC' WHEN 1 THEN 'CE' WHEN 2 THEN 'TI' WHEN 3 THEN 'RC' WHEN 4 THEN 'PA' WHEN 5 THEN 'ASI' WHEN 6 THEN 'MSI' END AS
                       IPTIPODOC, CASE TIPOINGRE WHEN 1 THEN 'Ambulatorio' WHEN 2 THEN 'Hospitalario' END AS Tipo_de_admision, F.MUNNOMBRE AS Municipio_del_paciente, 
                      B.IPDIRECCI AS Direccion_del_paciente, E.UBINOMBRE AS Barrio_del_paciente, 'Colombia' AS Pais, D.IAUTORIZA AS numero_de_autorización, 
                      CASE B.IPTIPOPAC WHEN 0 THEN 'Contributivo' WHEN 1 THEN 'Subsidiado' WHEN 2 THEN 'Vinculado' WHEN 3 THEN 'Particular' WHEN 5 THEN 'Desplazado Reg. Contributivo'
                       WHEN 6 THEN 'Desplazado Reg. Subsidiado' WHEN 7 THEN 'Desplazado no Asegurado' END AS Insurance_Plan, B.CODIGONIT AS Id_Aseguradora, 
                      RISTRACAB.CODCONSEC AS RISTRACAB_CODCONSEC, RISTRACAB.IPCODPACI AS RISTRACAB_IPCODPACI, RISTRACAB.NOMPACIEN AS RISTRACAB_NOMPACIEN, 
                      RISTRACAB.IPPRIAPEL AS RISTRACAB_IPPRIAPEL, RISTRACAB.IPSEGAPEL AS RISTRACAB_IPSEGAPEL, RISTRACAB.IPFECNACI AS RISTRACAB_IPFECNACI, 
                      RISTRACAB.IPSEXOPAC AS RISTRACAB_IPSEXOPAC, RISTRACAB.TIPMODALI AS RISTRACAB_TIPMODALI, RISTRACAB.CODSERIPS AS RISTRACAB_CODSERIPS, 
                      RISTRACAB.DESSERIPS AS RISTRACAB_DESSERIPS, RISTRACAB.FECHTURNO AS RISTRACAB_FECHTURNO, RISTRACAB.HORATURNO AS RISTRACAB_HORATURNO,
                       RISTRACAB.INESTADOT AS RISTRACAB_INESTADOT, RISTRACAB.PROCESADO AS RISTRACAB_PROCESADO, RISTRACAB.REPSERPAC AS RISTRACAB_REPSERPAC, 
                      RISTRACAB.OBSSERPAC AS RISTRACAB_OBSSERPAC
FROM         dbo.RISTRACAB AS RISTRACAB INNER JOIN
                      dbo.INPACIENT AS B ON RISTRACAB.IPCODPACI = B.IPCODPACI INNER JOIN
                      dbo.RISTRADET AS C ON RISTRACAB.CODCONSEC = C.CODCONSEC INNER JOIN
                      dbo.ADINGRESO AS D ON C.NUMINGRES = D.NUMINGRES INNER JOIN
                      dbo.INUBICACI AS E ON B.AUUBICACI = E.AUUBICACI INNER JOIN
                      dbo.INMUNICIP AS F ON E.DEPMUNCOD = F.DEPMUNCOD
WHERE     (RISTRACAB.PROCESADO = '1') AND (RISTRACAB.NOMSERPAC = 'Carestream')
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida las órdenes de servicios agendados procesados e integrados con el sistema Carestream (imágenes diagnósticas). Combina la cabecera y detalle de la agenda de turnos (RISTRACAB y RISTRADET) con los datos demográficos del paciente (INPACIENT), el ingreso o admisión clínica (ADINGRESO), y la información de ubicación geográfica del paciente (barrio, municipio de Colombia). Expone por cada orden: el número de orden, el estado (''Solicitado''), el tipo de documento y régimen de aseguramiento del paciente (contributivo, subsidiado, particular, etc.), el tipo de admisión (ambulatorio u hospitalario), la dirección, barrio y municipio del paciente, el número de autorización, la aseguradora, el servicio CUPS solicitado, y los datos de fecha y hora del turno. Se usa principalmente para la integración de órdenes de imágenes diagnósticas hacia Carestream, filtrando únicamente los registros ya procesados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'VORM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'VORM';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone las órdenes de imágenes diagnósticas procesadas e integradas con el sistema Carestream, enriquecidas con datos demográficos del paciente, autorización, aseguradora y ubicación, para su consumo por el RIS/PACS.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VORM';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La orden debe estar marcada como procesada (PROCESADO=''1'').; La orden debe estar dirigida al servicio/equipo ''Carestream'' (NOMSERPAC=''Carestream'').; Debe existir paciente en INPACIENT con el código IPCODPACI de la cabecera.; Debe existir al menos un detalle en RISTRADET asociado a la cabecera.; El detalle debe tener un ingreso (NUMINGRES) válido en ADINGRESO.; El paciente debe tener una ubicación (AUUBICACI) registrada en INUBICACI y un municipio (DEPMUNCOD) en INMUNICIP.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VORM';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Todas las filas expuestas tienen Estado_de_la_orden = ''Solicitado''.; Todas las filas expuestas tienen País = ''Colombia''.; Solo se exponen órdenes con PROCESADO=''1'' y servicio ''Carestream''.; Solo se incluyen pacientes con ubicación y municipio resueltos (joins INNER obligatorios).; Los códigos de tipo de documento e tipo de paciente sin equivalencia en el CASE quedan en NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VORM';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden de imagen diagnóstica; Paciente; Tipo de documento; Régimen de afiliación (Contributivo/Subsidiado/Vinculado/Particular/Desplazado); Aseguradora (NIT); Autorización; Ingreso/Admisión (Ambulatorio/Hospitalario); Ubicación geográfica (municipio, barrio); Integración RIS/PACS Carestream', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VORM';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] VORM: Devuelve órdenes con estado fijo ''Solicitado'' y país fijo ''Colombia'', filtradas por PROCESADO=''1'' y NOMSERPAC=''Carestream''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VORM';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si IPTIPODOC del paciente (0..6) → Se traduce el código numérico al código de tipo de documento: 0=CC, 1=CE, 2=TI, 3=RC, 4=PA, 5=ASI, 6=MSI.; si TIPOINGRE = 1 vs 2 → Se etiqueta el tipo de admisión como ''Ambulatorio'' (1) u ''Hospitalario'' (2).; si IPTIPOPAC del paciente → Se traduce el régimen/plan de aseguramiento: 0=Contributivo, 1=Subsidiado, 2=Vinculado, 3=Particular, 5=Desplazado Reg. Contributivo, 6=Desplazado Reg. Subsidiado, 7=Desplazado no Asegurado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VORM';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.RISTRACAB; dbo.INPACIENT; dbo.RISTRADET; dbo.ADINGRESO; dbo.INUBICACI; dbo.INMUNICIP', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VORM';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VORM';
GO
