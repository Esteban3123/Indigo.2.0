CREATE PROCEDURE [dbo].[ESE_SP_ordenes_medicamentos] @FechaIni DATETIME, 
                                                    @FechaFin DATETIME
AS
     SELECT FM.CODCENATE, 
            CA.NOMCENATE, 
            FM.UFUCODIGO, 
            UF.UFUDESCRI, 
            FM.NUMINGRES, 
            IG.CODENTIDA, 
            ENT.Name,
            CASE ENT.EntityType
                WHEN '1'
                THEN 'Contributivo'
                WHEN '2'
                THEN 'Subsidiado'
                WHEN '3'
                THEN 'Vinculados'
                WHEN '4'
                THEN 'Vicualados'
                WHEN '5'
                THEN 'ARL'
                WHEN '6'
                THEN 'Prepagada'
                WHEN '7'
                THEN 'IPS Privada'
                WHEN '8'
                THEN 'IPS publica'
                WHEN '9'
                THEN 'Especial'
                WHEN '10'
                THEN 'Accidentes de trasito'
                WHEN '11'
                THEN 'Fosyga'
                WHEN '12'
                THEN 'otros'
            END 'Regimen', 
            PAC.GRUPCODIGO, 
            PAC.IPTIPODOC, 
            PAC.IPCODPACI, 
            PAC.IPPRINOMB, 
            PAC.IPSEGNOMB, 
            PAC.IPPRIAPEL, 
            PAC.IPSEGAPEL, 
            PAC.IPSEXO, 
            PAC.IPFECNACI, 
            FM.CODPRODUC, 
            PRO.DESPRODUC, 
            PRO.CONCENMED, 
            PRO.PRESENMED, 
            FM.CODVIAADM, 
            VIA.DESVIAADM, 
            FM.CANPEDPRO, 
            FM.DESADMINI, 
            FM.DOSISPRFN, 
            FM.CODDIAGNO, 
            DIA.NOMDIAGNO, 
            FM.CODPROSAL, 
            ME.NOMMEDICO, 
            EF.PESOPACIE, 
            EF.FECREGITE
     FROM HCPRESCRA FM
          INNER JOIN ADCENATEN CA ON FM.CODCENATE = CA.CODCENATE
          INNER JOIN INUNIFUNC UF ON FM.UFUCODIGO = UF.UFUCODIGO
          INNER JOIN ADINGRESO IG ON FM.NUMINGRES = IG.NUMINGRES
          LEFT JOIN Contract.HealthAdministrator ENT ON IG.CODENTIDA = ENT.Code
          INNER JOIN INPACIENT PAC ON FM.IPCODPACI = PAC.IPCODPACI
          INNER JOIN IHLISTPRO PRO ON FM.CODPRODUC = PRO.CODPRODUC
          INNER JOIN HCVIAADMI VIA ON FM.CODVIAADM = VIA.CODVIAADM
          INNER JOIN INDIAGNOS DIA ON FM.CODDIAGNO = DIA.CODDIAGNO
          INNER JOIN INPROFSAL ME ON FM.CODPROSAL = ME.CODPROSAL
          LEFT JOIN HCEXFISIC EF ON FM.NUMEFOLIO = EF.NUMEFOLIO
                                    AND FM.NUMINGRES = EF.NUMINGRES
                                    AND FM.IPCODPACI = EF.IPCODPACI
     WHERE EF.FECREGITE BETWEEN @FechaIni AND @FechaFin;