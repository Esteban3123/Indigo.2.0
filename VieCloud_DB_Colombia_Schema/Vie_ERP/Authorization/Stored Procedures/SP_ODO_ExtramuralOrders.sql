CREATE PROCEDURE [Authorization].[SP_ODO_ExtramuralOrders] @InitialDate DATE, 
                                                          @EndDate     DATE
AS
    BEGIN
        SELECT mmo.Id ID, 
               fu.UFUDESCRI [Tipo de Consulta], 
               cc.NOMCENATE [Sede Origen], 
               cc.NOMCENATE [Sedes],
               CASE p.IPTIPODOC
                   WHEN 1
                   THEN 'CC'
                   WHEN 2
                   THEN 'CE'
                   WHEN 3
                   THEN 'TI'
                   WHEN 4
                   THEN 'RC'
                   WHEN 5
                   THEN 'PA'
                   WHEN 6
                   THEN 'AS'
                   WHEN 7
                   THEN 'MS'
                   WHEN 8
                   THEN 'NU'
                   WHEN 9
                   THEN 'CN'
                   WHEN 10
                   THEN 'CD'
                   WHEN 11
                   THEN 'SC'
                   WHEN 12
                   THEN 'PE'
                   ELSE '-'
               END [Tipo Documento], 
               p.IPCODPACI [Documento de Identidad], 
               p.IPPRINOMB [Primer Nombre], 
               p.IPSEGNOMB [Segundo Nombre], 
               p.IPPRIAPEL [Primer Apellido], 
               p.IPSEGAPEL [Segundo Apellido], 
               p.IPDIRECCI [Dirección de Residencia], 
               CONCAT(p.IPTELEFON, IIF(LEN(p.IPTELEFON) > 0
                                       AND LEN(p.IPTELMOVI) > 0, ', ', ''), p.IPTELMOVI) [Telefonos], 
               tp.Nit [DID], 
               ha.HealthEntityCode [Código Entidad], 
               ha.Name [Entidad],
               CASE p.IPSEXOPAC
                   WHEN 1
                   THEN 'MASCULINO'
                   WHEN 2
                   THEN 'FEMENINO'
               END [Genero], 
               'VIVO' [Estado Natural], 
               h.RequestDate [Fecha de Atención], 
               pre.DESESPECI [Profesional Tipo], 
               pr.NOMMEDICO [Profesional Nombre], 
               diag.CODDIAGNO [Codigo Diagnostico], 
               diag.NOMDIAGNO [Nombre Diagnostico], 
               '' [Diagnostico Ubicacion], 
               '' [Tipo Diagnostico], 
               a.desactivi [Ocupacion], 
               d.depcodigo [Codigo Departamento], 
               d.nomdepart [Departamento o Residencia], 
               m.MUNCODIGO [Codigo Municipio], 
               m.MUNNOMBRE [Municipio Residencia],
               CASE u.TIPOUBICA
                   WHEN 2
                   THEN 'RURAL'
                   ELSE 'URBANA'
               END [Zona Residencia], 
               DATEDIFF(YEAR, p.IPFECNACI, [Common].[GETDATE]()) [Edad], 
               h.ItemName [Servicio],
               CASE h.Type
                   WHEN 1
                   THEN 'SERVICIO'
                   WHEN 2
                   THEN 'PRODUCTO'
               END [Tipo Codigo], 
               h.ItemCode [Codigo], 
               h.Quantity [Cantidad], 
               '' [PROVEE], 
               '' [Medicamentos], 
               h.DescriptionCode [CONSECUTIVO], 
               h.DescriptionName [Descripcion Relacionada]
        FROM
        (  
            -- Ordenes de imagenes ambulatorias  
            SELECT 'HCORDIMAG' EntityName, 
                   h.AUTO EntityId, 
                   h.CODCENATE CareCenterCode, 
                   h.UFUCODIGO FunctionalUnitCode, 
                   h.NUMINGRES AdmissionNumber, 
                   h.NUMEFOLIO Folio, 
                   h.IPCODPACI PatientCode, 
                   h.FECORDMED RequestDate, 
                   h.CODPROSAL ProfessionalCode, 
                   h.CANSERIPS Quantity, 
                   1 Type, 
                   ce.Code ItemCode, 
                   ce.Description ItemName, 
                   cd.Code DescriptionCode, 
                   cd.Name DescriptionName
            FROM.HCORDIMAG h
                JOIN Contract.CUPSEntity ce ON h.CODSERIPS = ce.Code
                LEFT JOIN Contract.CUPSEntityContractDescriptions cecd ON ce.Id = cecd.CUPSEntityId
                                                                          AND h.IDDESCRIPCIONRELACIONADA = cecd.Id
                LEFT JOIN Contract.ContractDescriptions cd ON cecd.ContractDescriptionId = cd.Id
            WHERE h.MANEXTPRO = 1
                  AND NOT(h.ESTSERIPS IN('6'))
            UNION ALL  
            -- Ordenes de laboratorios ambulatorios  
            SELECT 'HCORDLABO' EntityName, 
                   h.AUTO EntityId, 
                   h.CODCENATE CareCenterCode, 
                   h.UFUCODIGO FunctionalUnitCode, 
                   h.NUMINGRES AdmissionNumber, 
                   h.NUMEFOLIO Folio, 
                   h.IPCODPACI PatientCode, 
                   h.FECORDMED RequestDate, 
                   h.CODPROSAL ProfessionalCode, 
                   h.CANSERIPS Quantity, 
                   1 Type, 
                   ce.Code ItemCode, 
                   ce.Description ItemName, 
                   cd.Code DescriptionCode, 
                   cd.Name DescriptionName
            FROM.HCORDLABO h
                JOIN Contract.CUPSEntity ce ON h.CODSERIPS = ce.Code
                LEFT JOIN Contract.CUPSEntityContractDescriptions cecd ON ce.Id = cecd.CUPSEntityId
                                                                          AND h.IDDESCRIPCIONRELACIONADA = cecd.Id
                LEFT JOIN Contract.ContractDescriptions cd ON cecd.ContractDescriptionId = cd.Id
            WHERE h.MANEXTPRO = 1
                  AND NOT(h.ESTSERIPS IN('6'))
            UNION ALL  
            -- Ordenes de patologias ambulatorias  
            SELECT 'HCORDPATO' EntityName, 
                   h.AUTO EntityId, 
                   h.CODCENATE CareCenterCode, 
                   h.UFUCODIGO FunctionalUnitCode, 
                   h.NUMINGRES AdmissionNumber, 
                   h.NUMEFOLIO Folio, 
                   h.IPCODPACI PatientCode, 
                   h.FECORDMED RequestDate, 
                   h.CODPROSAL ProfessionalCode, 
                   h.CANSERIPS Quantity, 
                   1 Type, 
                   ce.Code ItemCode, 
                   ce.Description ItemName, 
                   cd.Code DescriptionCode, 
                   cd.Name DescriptionName
            FROM.HCORDPATO h
                JOIN Contract.CUPSEntity ce ON h.CODSERIPS = ce.Code
                LEFT JOIN Contract.CUPSEntityContractDescriptions cecd ON ce.Id = cecd.CUPSEntityId
                                                                          AND h.IDDESCRIPCIONRELACIONADA = cecd.Id
                LEFT JOIN Contract.ContractDescriptions cd ON cecd.ContractDescriptionId = cd.Id
            WHERE h.MANEXTPRO = 1
                  AND NOT(h.ESTSERIPS IN('6'))
            UNION ALL  
            -- Ordenes de interconsultas ambulatorias  
            SELECT 'HCORDINTE' EntityName, 
                   h.AUTO EntityId, 
                   h.CODCENATE CareCenterCode, 
                   h.UFUCODIGO FunctionalUnitCode, 
                   h.NUMINGRES AdmissionNumber, 
                   h.NUMEFOLIO Folio, 
                   h.IPCODPACI PatientCode, 
                   h.FECORDMED RequestDate, 
                   h.CODPROSAL ProfessionalCode, 
                   h.CANSERIPS Quantity, 
                   1 Type, 
                   ce.Code ItemCode, 
                   ce.Description ItemName, 
                   cd.Code DescriptionCode, 
                   cd.Name DescriptionName
            FROM.HCORDINTE h
                JOIN Contract.CUPSEntity ce ON h.CODSERIPS = ce.Code
                LEFT JOIN Contract.CUPSEntityContractDescriptions cecd ON ce.Id = cecd.CUPSEntityId
                                                                          AND h.IDDESCRIPCIONRELACIONADA = cecd.Id
                LEFT JOIN Contract.ContractDescriptions cd ON cecd.ContractDescriptionId = cd.Id
            WHERE h.MANEXTPRO = 1
                  AND NOT(h.ESTSERIPS IN('5'))
            UNION ALL  
            -- Ordenes de procedimientos no Qx ambulatorias  
            SELECT 'HCORDPRON' EntityName, 
                   h.AUTO EntityId, 
                   h.CODCENATE CareCenterCode, 
                   h.UFUCODIGO FunctionalUnitCode, 
                   h.NUMINGRES AdmissionNumber, 
                   h.NUMEFOLIO Folio, 
                   h.IPCODPACI PatientCode, 
                   h.FECORDMED RequestDate, 
                   h.CODPROSAL ProfessionalCode, 
                   h.CANSERIPS Quantity, 
                   1 Type, 
                   ce.Code ItemCode, 
                   ce.Description ItemName, 
                   cd.Code DescriptionCode, 
                   cd.Name DescriptionName
            FROM.HCORDPRON h
                JOIN Contract.CUPSEntity ce ON h.CODSERIPS = ce.Code
                LEFT JOIN Contract.CUPSEntityContractDescriptions cecd ON ce.Id = cecd.CUPSEntityId
                                                                          AND h.IDDESCRIPCIONRELACIONADA = cecd.Id
                LEFT JOIN Contract.ContractDescriptions cd ON cecd.ContractDescriptionId = cd.Id
            WHERE h.MANEXTPRO = 1
                  AND NOT(h.ESTSERIPS IN('5'))
            UNION ALL  
            -- Ordenes de procedimientos Qx ambulatorias  
            SELECT 'HCORDPROQ' EntityName, 
                   h.AUTO EntityId, 
                   h.CODCENATE CareCenterCode, 
                   h.UFUCODIGO FunctionalUnitCode, 
                   h.NUMINGRES AdmissionNumber, 
                   h.NUMEFOLIO Folio, 
                   h.IPCODPACI PatientCode, 
                   h.FECORDMED RequestDate, 
                   h.CODPROSAL ProfessionalCode, 
                   h.CANSERIPS Quantity, 
                   1 Type, 
                   ce.Code ItemCode, 
                   ce.Description ItemName, 
                   cd.Code DescriptionCode, 
                   cd.Name DescriptionName
            FROM.HCORDPROQ h
                JOIN Contract.CUPSEntity ce ON h.CODSERIPS = ce.Code
                LEFT JOIN Contract.CUPSEntityContractDescriptions cecd ON ce.Id = cecd.CUPSEntityId
                                                                          AND h.IDDESCRIPCIONRELACIONADA = cecd.Id
                LEFT JOIN Contract.ContractDescriptions cd ON cecd.ContractDescriptionId = cd.Id
            WHERE h.MANEXTPRO = 1
                  AND NOT(h.ESTSERIPS IN('3'))
            UNION ALL  
            -- Hemocomponentes  
            SELECT 'HCORHEMCO' EntityName, 
                   h.ID EntityId, 
                   h.CODCENATE CareCenterCode, 
                   ing.UFUCODIGO FunctionalUnitCode, 
                   h.NUMINGRES AdmissionNumber, 
                   h.NUMEFOLIO Folio, 
                   h.IPCODPACI PatientCode, 
                   h.FECORDMED RequestDate, 
                   '' ProfessionalCode, 
                   hd.Quantity Quantity, 
                   1 Type, 
                   ce.Code ItemCode, 
                   ce.Description ItemName, 
                   cd.Code DescriptionCode, 
                   cd.Name DescriptionName
            FROM.ADINGRESO ing
                JOIN.HCORHEMCO h ON ing.NUMINGRES = h.NUMINGRES
                JOIN
            (
                SELECT hd.HCORHEMCOID, 
                       hd.CODSERIPS, 
                       hd.TraceabilityPaperworkId, 
                       hd.TraceabilityPaperworkEventsId, 
                       hd.IDDESCRIPCIONRELACIONADA, 
                       COUNT(1) Quantity
                FROM.HCORHEMSER hd
                WHERE hd.ESTADO NOT IN(3)
                GROUP BY hd.HCORHEMCOID, 
                         hd.CODSERIPS, 
                         hd.TraceabilityPaperworkId, 
                         hd.TraceabilityPaperworkEventsId, 
                         hd.IDDESCRIPCIONRELACIONADA
            ) hd ON h.ID = hd.HCORHEMCOID
                JOIN Contract.CUPSEntity ce ON hd.CODSERIPS = ce.Code
                LEFT JOIN Contract.CUPSEntityContractDescriptions cecd ON ce.Id = cecd.CUPSEntityId
                                                                          AND hd.IDDESCRIPCIONRELACIONADA = cecd.Id
                LEFT JOIN Contract.ContractDescriptions cd ON cecd.ContractDescriptionId = cd.Id
            WHERE h.MANEXTPRO = 1
            UNION ALL  
            -- Ordenes de control por la especialidad que atendió al paciente  
            SELECT 'HCDESCOEX' EntityName, 
                   h.AUTO EntityId, 
                   h.CODCENATE CareCenterCode, 
                   h.UFUCODIGO FunctionalUnitCode, 
                   h.NUMINGRES AdmissionNumber, 
                   h.NUMEFOLIO Folio, 
                   h.IPCODPACI PatientCode, 
                   hc.FECHISPAC RequestDate, 
                   hc.CODPROSAL ProfessionalCode, 
                   1 Quantity, 
                   1 Type, 
                   ce.Code ItemCode, 
                   ce.Description ItemName, 
                   NULL DescriptionCode, 
                   NULL DescriptionName
            FROM dbo.HCHISPACA hc
                 JOIN.HCDESCOEX h ON hc.NUMINGRES = h.NUMINGRES
                                     AND hc.NUMEFOLIO = h.NUMEFOLIO
                 JOIN Contract.CUPSEntity ce ON h.CODSERIPS = ce.Code
            UNION ALL  
            -- Medicamentos extramurales  
            SELECT 'HCPRESCRA' EntityName, 
                   h.CODCONCEC EntityId, 
                   h.CODCENATE CareCenterCode, 
                   h.UFUCODIGO FunctionalUnitCode, 
                   h.NUMINGRES AdmissionNumber, 
                   h.NUMEFOLIO Folio, 
                   h.IPCODPACI PatientCode, 
                   h.FECINIDOS RequestDate, 
                   h.CODPROSAL ProfessionalCode, 
                   h.CANPEDPRO Quantity, 
                   2 Type, 
                   atc.Code ItemCode, 
                   atc.Name ItemName, 
                   NULL DescriptionCode, 
                   NULL DescriptionName
            FROM.HCPRESCRA h
                JOIN Inventory.ATC atc ON h.CODPRODUC = atc.Code
            WHERE h.MANEXTPRO = 1
        ) h
        JOIN.ADCENATEN cc ON h.CareCenterCode = cc.CODCENATE
        JOIN.INUNIFUNC fu ON h.FunctionalUnitCode = fu.UFUCODIGO
        JOIN.ADINGRESO ing ON h.AdmissionNumber = ing.NUMINGRES
        JOIN Contract.HealthAdministrator ha ON ing.GENCONENTITY = ha.Id
        JOIN Common.ThirdParty tp ON ha.ThirdPartyId = tp.Id
        JOIN.INPACIENT p ON h.PatientCode = p.IPCODPACI
        JOIN dbo.ADACTIVID a ON p.CODACTIVI = a.codactivi
        JOIN dbo.INUBICACI u ON p.AUUBICACI = u.AUUBICACI
        JOIN dbo.INMUNICIP m ON u.DEPMUNCOD = m.DEPMUNCOD
        JOIN dbo.INDEPARTA d ON m.DEPCODIGO = d.depcodigo
        LEFT JOIN [Authorization].ManagementMedicalOrder mmo ON h.EntityName = mmo.EntityName
                                                                AND h.EntityId = mmo.EntityId
                                                                AND h.ItemCode = mmo.ItemCode
        LEFT JOIN dbo.INPROFSAL pr ON h.ProfessionalCode = pr.CODPROSAL
        LEFT JOIN dbo.INESPECIA pre ON pr.CODESPEC1 = pre.CODESPECI
        LEFT JOIN dbo.INDIAGNOP diagad ON ing.NUMINGRES = diagad.NUMINGRES
                                          AND diagad.CODDIAPRI = 1
        LEFT JOIN dbo.INDIAGNOS diag ON diagad.CODDIAGNO = diag.CODDIAGNO
        WHERE CAST(h.RequestDate AS DATE) BETWEEN @InitialDate AND @EndDate;
    END;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consolida y reporta las órdenes médicas extramurales (ambulatorias generadas fuera de la institución) emitidas entre dos fechas dadas, agrupando imágenes diagnósticas (HCORDIMAG), laboratorios clínicos (HCORDLABO) y patologías (HCORDPATO) en un único conjunto de resultados. Para cada orden incluye datos completos del paciente (documento de identidad, nombre, dirección, teléfonos, municipio, departamento, zona de residencia, edad, sexo), de la atención (sede, unidad funcional, tipo de consulta, fecha de atención, profesional tratante, especialidad), del servicio solicitado (código CUPS, descripción, cantidad, descripción de contrato relacionada) y del diagnóstico asociado. Utiliza el catálogo CUPS (CUPSEntity y CUPSEntityContractDescriptions) para resolver nombre y código del procedimiento, y ContractDescriptions para identificar el concepto o grupo de facturación contractual aplicable. Este procedimiento es utilizado principalmente para la generación de reportes de órdenes extramurales, seguimiento de servicios ambulatorios autorizados y control de prestaciones fuera de la institución, con aplicación en auditoría, autorización y RIPS.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'PROCEDURE', @level1name = N'SP_ODO_ExtramuralOrders';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'PROCEDURE', @level1name = N'SP_ODO_ExtramuralOrders';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reporta las órdenes médicas extramurales (imágenes, laboratorios, patología, interconsultas, procedimientos Qx/no-Qx, hemocomponentes, controles, medicamentos) generadas en un rango de fechas, con datos de paciente, sede, profesional, diagnóstico y entidad responsable.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_ODO_ExtramuralOrders';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@InitialDate y @EndDate deben permitir filtrar h.RequestDate (CAST a DATE BETWEEN @InitialDate AND @EndDate); Las órdenes deben estar marcadas como extramurales (MANEXTPRO = 1) salvo en HCDESCOEX (controles) que no aplica esa marca; El paciente debe existir en INPACIENT, el ingreso en ADINGRESO, la entidad de salud en HealthAdministrator y el tercero en ThirdParty; Los ítems CUPS deben existir en Contract.CUPSEntity y los medicamentos en Inventory.ATC', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_ODO_ExtramuralOrders';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan órdenes con marca extramural (MANEXTPRO=1), excepto controles HCDESCOEX; Las órdenes con ESTSERIPS en estado de exclusión específico por tipo (6, 5 o 3 según fuente) nunca aparecen en el resultado; Los hemocomponentes con detalle ESTADO=3 no se cuentan en la cantidad; El campo ''Estado Natural'' siempre es ''VIVO'' (literal fijo); La edad se calcula como diferencia en años entre IPFECNACI y la fecha actual del sistema (Common.GETDATE); Solo se considera el diagnóstico principal del ingreso (CODDIAPRI=1); Los teléfonos se concatenan separados por coma solo si ambos (fijo y móvil) tienen contenido', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_ODO_ExtramuralOrders';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Órdenes médicas extramurales; Imágenes diagnósticas; Laboratorios; Patología; Interconsultas; Procedimientos quirúrgicos y no quirúrgicos; Hemocomponentes; Controles por especialidad; Prescripción de medicamentos (ATC); CUPS; Diagnóstico principal (CIE-10); Ingreso/Admisión; Sede de atención y unidad funcional; Administradora de salud (EPS); Tipo de documento de identidad; Zona de residencia (urbana/rural); Especialidad médica; Autorizaciones / gestión de órdenes médicas', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_ODO_ExtramuralOrders';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULTSET: Devuelve un único conjunto consolidado de órdenes extramurales filtrado por rango de fechas sobre RequestDate', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_ODO_ExtramuralOrders';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Origen = HCORDIMAG/HCORDLABO/HCORDPATO (imágenes, laboratorios, patologías ambulatorias) → Incluye órdenes con MANEXTPRO=1 y ESTSERIPS distinto de ''6'' (excluye estado 6, presumiblemente anuladas/rechazadas); si Origen = HCORDINTE o HCORDPRON (interconsultas o procedimientos no Qx) → Incluye órdenes con MANEXTPRO=1 y ESTSERIPS distinto de ''5''; si Origen = HCORDPROQ (procedimientos Qx) → Incluye órdenes con MANEXTPRO=1 y ESTSERIPS distinto de ''3''; si Origen = HCORHEMCO (hemocomponentes) → Incluye solo si MANEXTPRO=1 y agrega cantidad como COUNT(1) de HCORHEMSER cuyo ESTADO no sea 3; si Origen = HCDESCOEX (control por especialidad) → Toma RequestDate y profesional desde HCHISPACA (historia clínica) emparejando NUMINGRES y NUMEFOLIO; sin filtro MANEXTPRO ni ESTSERIPS; si Origen = HCPRESCRA (medicamentos) → Incluye prescripciones con MANEXTPRO=1, marca Type=2 (PRODUCTO) y vincula código ATC; si p.IPTIPODOC entre 1 y 12 → Mapea a etiqueta de tipo de documento (CC, CE, TI, RC, PA, AS, MS, NU, CN, CD, SC, PE) else Devuelve ''-''; si u.TIPOUBICA = 2 → Zona Residencia = ''RURAL'' else Zona Residencia = ''URBANA''; si h.Type = 1 vs 2 → Etiqueta ''SERVICIO'' cuando Type=1, ''PRODUCTO'' cuando Type=2; si p.IPSEXOPAC = 1/2 → Género = MASCULINO/FEMENINO respectivamente; si diagad.CODDIAPRI = 1 → Solo se trae el diagnóstico marcado como principal del ingreso', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_ODO_ExtramuralOrders';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_ODO_ExtramuralOrders';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDIMAG; dbo.HCORDLABO; dbo.HCORDPATO; dbo.HCORDINTE; dbo.HCORDPRON; dbo.HCORDPROQ; dbo.HCORHEMCO; dbo.HCORHEMSER; dbo.HCDESCOEX; dbo.HCHISPACA; dbo.HCPRESCRA; dbo.ADCENATEN; dbo.INUNIFUNC; dbo.ADINGRESO; dbo.INPACIENT; dbo.ADACTIVID; dbo.INUBICACI; dbo.INMUNICIP; dbo.INDEPARTA; dbo.INPROFSAL; dbo.INESPECIA; dbo.INDIAGNOP; dbo.INDIAGNOS; Contract.CUPSEntity; Contract.CUPSEntityContractDescriptions; Contract.ContractDescriptions; Contract.HealthAdministrator; Common.ThirdParty; Inventory.ATC; Authorization.ManagementMedicalOrder', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_ODO_ExtramuralOrders';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_ODO_ExtramuralOrders';
-- GO
