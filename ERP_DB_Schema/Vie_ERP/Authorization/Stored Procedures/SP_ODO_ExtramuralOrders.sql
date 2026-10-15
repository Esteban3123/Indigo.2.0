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