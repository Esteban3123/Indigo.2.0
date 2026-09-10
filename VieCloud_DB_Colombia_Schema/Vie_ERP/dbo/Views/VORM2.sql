

CREATE VIEW [dbo].[VORM2]
AS
SELECT        A.CODCONSEC AS Numero_de_orden, 'Solicitado' AS Estado_de_la_orden, 
                         CASE D .IPTIPODOC WHEN 0 THEN 'CC' WHEN 1 THEN 'CE' WHEN 2 THEN 'TI' WHEN 3 THEN 'RC' WHEN 4 THEN 'PA' WHEN 5 THEN 'ASI' WHEN 6 THEN 'MSI' END
                          AS IPTIPODOC, CASE WHEN G.CODSERIPS IS NULL THEN 'Consulta Externa' ELSE 'Hospitalario' END AS Tipo_de_Solicitud, 
                         F.MUNNOMBRE AS Municipio_del_paciente, D.IPDIRECCI AS Direccion_del_paciente, E.UBINOMBRE AS Barrio_del_paciente, 'Colombia' AS Pais, 
                         ISNULL(C.IAUTORIZA, '') AS Expr1, 
                         CASE D .IPTIPOPAC WHEN 0 THEN 'Contributivo' WHEN 1 THEN 'Subsidiado' WHEN 2 THEN 'Vinculado' WHEN 3 THEN 'Particular' WHEN 5 THEN 'Desplazado Reg. Contributivo'
                          WHEN 6 THEN 'Desplazado Reg. Subsidiado' WHEN 7 THEN 'Desplazado no Asegurado' END AS Insurance_Plan, C.CODENTIDA AS Id_Aseguradora, 
                         A.CODCONSEC AS RISTRACAB_CODCONSEC, A.IPCODPACI AS RISTRACAB_IPCODPACI, A.NOMPACIEN AS RISTRACAB_NOMPACIEN, 
                         A.IPPRIAPEL AS RISTRACAB_IPPRIAPEL, A.IPSEGAPEL AS RISTRACAB_IPSEGAPEL, A.IPFECNACI AS RISTRACAB_IPFECNACI, 
                         CASE WHEN A.IPSEXOPAC = 1 THEN 'M' WHEN A.IPSEXOPAC = 2 THEN 'F' END AS RISTRACAB_IPSEXOPAC, A.TIPMODALI AS RISTRACAB_TIPMODALI, 
                         LTRIM(RTRIM(A.CODSERIPS)) AS RISTRACAB_CODSERIPS, A.DESSERIPS AS RISTRACAB_DESSERIPS, A.FECHTURNO AS RISTRACAB_FECHTURNO, 
                         A.HORATURNO AS RISTRACAB_HORATURNO, A.INESTADOT AS RISTRACAB_INESTADOT, A.PROCESADO AS RISTRACAB_PROCESADO, 
                         A.REPSERPAC AS RISTRACAB_REPSERPAC, ISNULL(A.OBSSERPAC, '') AS OBSERVACION_ORDEN, D.IPTELEFON AS Tel_1, D.IPTELMOVI AS Tel_2, 
                         CASE G.PRISERIPS WHEN 1 THEN '30' ELSE '10' END AS Prioridad, LTRIM(RTRIM(D.IPPRINOMB)) + ' ' + LTRIM(RTRIM(D.IPSEGNOMB)) AS NOMBRES_PACIENTE, 
                         C.CODCENATE AS Cod_Centro_Atención, SUBSTRING(LTRIM(RTRIM(dbo.INENTIDAD.NOMENTIDA)), 1, 250) AS Nom_Entidad, 
                         dbo.ADCENATEN.NOMCENATE AS Nom_Centro_Atención, ISNULL(SUBSTRING(LTRIM(RTRIM(dbo.INUNIFUNC.UFUDESCRI)), 1, 20), 'Consulta Externa') 
                         AS UNIDAD_FUNCIONAL_DE_CAMA, ISNULL(LTRIM(RTRIM(dbo.CHCAMASHO.DESCCAMAS)), 'Consulta Externa') AS CAMA_DEL_PACIENTE, 
                         LTRIM(RTRIM(A.IPPRIAPEL)) + ' ' + LTRIM(RTRIM(A.IPSEGAPEL)) AS APELLIDOS
FROM            dbo.RISTRACAB AS A INNER JOIN
                         dbo.RISTRADET AS B ON A.CODCONSEC = B.CODCONSEC INNER JOIN
                         dbo.ADINGRESO AS C ON B.NUMINGRES = C.NUMINGRES INNER JOIN
                         dbo.INPACIENT AS D ON A.IPCODPACI = D.IPCODPACI INNER JOIN
                         dbo.INUBICACI AS E ON E.AUUBICACI = D.AUUBICACI INNER JOIN
                         dbo.INMUNICIP AS F ON F.DEPMUNCOD = E.DEPMUNCOD INNER JOIN
                         dbo.INENTIDAD ON C.CODENTIDA = dbo.INENTIDAD.CODENTIDA INNER JOIN
                         dbo.ADCENATEN ON C.CODCENATE = dbo.ADCENATEN.CODCENATE LEFT OUTER JOIN
                         dbo.CHCAMASHO ON C.CODCAMACT = dbo.CHCAMASHO.CODICAMAS LEFT OUTER JOIN
                         dbo.INUNIFUNC ON C.UFUAACTHOS = dbo.INUNIFUNC.UFUCODIGO LEFT OUTER JOIN
                         dbo.HCORDIMAG AS G ON A.IPCODPACI = G.IPCODPACI AND A.CODSERIPS = G.CODSERIPS AND B.NUMEFOLIO = G.NUMEFOLIO AND 
                         B.NUMINGRES = G.NUMINGRES
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida las órdenes de agendamiento (turnos o citas) en estado ''Solicitado'', integrando datos completos del paciente, su ingreso hospitalario o de consulta externa, la aseguradora, el centro de atención, la cama y unidad funcional asignada, y el municipio y barrio de residencia. Combina la cabecera y el detalle de la agenda (RISTRACAB y RISTRADET) con el ingreso del paciente (ADINGRESO), la ficha del paciente (INPACIENT), su ubicación geográfica (INUBICACI, INMUNICIP), la entidad pagadora (INENTIDAD), el centro de atención (ADCENATEN), la cama hospitalaria (CHCAMASHO), la unidad funcional (INUNIFUNC) y las órdenes de imágenes diagnósticas (HCORDIMAG) para determinar si la solicitud es ambulatoria o hospitalaria. Se utiliza para reportería de gestión de citas, seguimiento de pacientes agendados, integración con sistemas externos (como RIPS o plataformas de aseguradoras) y control operativo de la agenda de atención, incluyendo prioridad de atención, tipo de documento, régimen de afiliación y datos de contacto del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'VORM2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'VORM2';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista que consolida la información de órdenes de imágenes diagnósticas con datos del paciente, ingreso, aseguradora, ubicación, centro de atención y cama, presentándola en formato legible para integración o reporte externo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VORM2';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La orden (RISTRACAB) debe tener detalle asociado en RISTRADET por CODCONSEC.; El detalle debe corresponder a un ingreso vigente en ADINGRESO (NUMINGRES).; El paciente debe existir en INPACIENT y tener ubicación válida en INUBICACI con municipio en INMUNICIP.; El ingreso debe estar asociado a una entidad (INENTIDAD) y un centro de atención (ADCENATEN).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VORM2';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Estado_de_la_orden siempre se reporta como ''Solicitado''.; Pais siempre se reporta como ''Colombia''.; La autorización (IAUTORIZA) y la observación de la orden se devuelven como cadena vacía cuando son NULL.; Solo se incluyen órdenes con detalle, ingreso, paciente, ubicación, municipio, entidad y centro de atención existentes (INNER JOIN).; La ausencia de cama u unidad funcional se asume como atención de Consulta Externa.; El nombre de la entidad y la unidad funcional se truncan a 250 y 20 caracteres respectivamente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VORM2';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden de imágenes diagnósticas; Paciente; Tipo de documento de identidad; Régimen de afiliación (Contributivo, Subsidiado, Vinculado, Particular, Desplazado); Aseguradora/Entidad; Autorización; Centro de atención; Unidad funcional; Cama hospitalaria; Consulta externa vs Hospitalario; Prioridad de la orden; Ubicación geográfica (municipio, barrio)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VORM2';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] VORM2: Devuelve un registro por cada combinación cabecera-detalle-ingreso de orden de imágenes con datos demográficos, administrativos y de hospitalización del paciente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VORM2';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si G.CODSERIPS IS NULL (no existe orden de imagen en HCORDIMAG vinculada) → Tipo_de_Solicitud = ''Consulta Externa'' else Tipo_de_Solicitud = ''Hospitalario''; si IPTIPODOC del paciente (0..6) → Mapea a etiqueta de tipo de documento: 0=CC, 1=CE, 2=TI, 3=RC, 4=PA, 5=ASI, 6=MSI; si IPTIPOPAC del paciente (0,1,2,3,5,6,7) → Mapea a régimen/plan: Contributivo, Subsidiado, Vinculado, Particular, Desplazado Reg. Contributivo, Desplazado Reg. Subsidiado, Desplazado no Asegurado; si IPSEXOPAC = 1 / 2 → Sexo = ''M'' / ''F''; si G.PRISERIPS = 1 → Prioridad = ''30'' else Prioridad = ''10''; si INUNIFUNC sin coincidencia (LEFT JOIN nulo) → UNIDAD_FUNCIONAL_DE_CAMA = ''Consulta Externa''; si CHCAMASHO sin coincidencia (LEFT JOIN nulo) → CAMA_DEL_PACIENTE = ''Consulta Externa''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VORM2';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.RISTRACAB; dbo.RISTRADET; dbo.ADINGRESO; dbo.INPACIENT; dbo.INUBICACI; dbo.INMUNICIP; dbo.INENTIDAD; dbo.ADCENATEN; dbo.CHCAMASHO; dbo.INUNIFUNC; dbo.HCORDIMAG', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VORM2';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VORM2';
GO
