CREATE PROCEDURE [dbo].[ESE_SP_Asistencial_VigilaciaEpidemiologica] @FechaIni DATETIME, 
                                                                   @FechaFin DATETIME
AS
     SELECT DISTINCT 
            ING.IFECHAING AS Fecha, 
            ING.NUMINGRES AS Ingreso,
            CASE
                WHEN ING.TIPOINGRE = 1
                THEN 'Ambulatorio'
                ELSE 'hospitalario'
            END AS 'TipodeIngreso',
            CASE
                WHEN ING.ICAUSAING = '1'
                THEN '1. Heridos en combate'
                WHEN ING.ICAUSAING = '2'
                THEN '2. Enfermedad profesional'
                WHEN ING.ICAUSAING = '3'
                THEN '3. Enfermedad general adulto'
                WHEN ING.ICAUSAING = '4'
                THEN '4. Enfermedad general pediatria'
                WHEN ING.ICAUSAING = '5'
                THEN '5. Odontología'
                WHEN ING.ICAUSAING = '6'
                THEN '6. Accidente de transito'
                WHEN ING.ICAUSAING = '7'
                THEN '7. Catastrofe/Fisalud'
                WHEN ING.ICAUSAING = '8'
                THEN '8. Quemados'
                WHEN ING.ICAUSAING = '9'
                THEN '9. Maternidad'
                WHEN ING.ICAUSAING = '10'
                THEN '10. Accidente Laboral'
                WHEN ING.ICAUSAING = '11'
                THEN '11. Cirugia Programada'
            END AS 'CausaDelIngreso', 
            ING.IPCODPACI AS 'Identificacion',
            CASE P.IPTIPODOC
                WHEN 1
                THEN 'Cédula de Ciudadanía'
                WHEN 2
                THEN 'Cédula de Extranjería '
                WHEN 3
                THEN 'Tarjeta de Identidad '
                WHEN 4
                THEN 'Registro Civil '
                WHEN 5
                THEN 'Pasporte '
                WHEN 6
                THEN 'Adulto Sin Identificación '
                WHEN 7
                THEN 'Menor Sin Identificación '
                WHEN 8
                THEN 'Número único de identificación personal'
            END AS 'TipoIdentificacion', 
            P.IPNOMCOMP AS NombrePaciente, 
            CONVERT(VARCHAR(10), P.IPFECNACI, 105) AS FechaNacimiento, 
            [dbo].[Edad](P.IPFECNACI, [Common].[GETDATE]()) AS Edad,
            CASE P.IPSEXOPAC
                WHEN 1
                THEN 'Masculino'
                WHEN 2
                THEN 'Femenino'
            END AS 'Genero', 
            ENT.CODENTIDA + ' - ' + ENt.NOMENTIDA AS 'Entidad', 
            ING.CODDIAING + ' - ' + D.NOMDIAGNO AS DiagnosticoIngreso, 
            ING.CODDIAEGR + '-' + d2.NOMDIAGNO AS DiagnosticoEgreso, 
            RTRIM(ING.CODCENATE) + ' - ' + RTRIM(CEN.NOMCENATE) AS CentroAtencion, 
            RTRIM(ING.UFUCODIGO) + ' - ' + RTRIM(UNI.UFUDESCRI) AS UnidadFuncional, 
            P.IPDIRECCI AS Direccion,
            CASE
                WHEN P.IPTELMOVI = ''
                THEN P.IPTELEFON
                ELSE P.IPTELMOVI
            END AS Telefono, 
            UBI.UBINOMBRE AS Barrio
     FROM.HCURGING1 AS HIS
         INNER JOIN.ADINGRESO AS ING ON ING.NUMINGRES = HIS.NUMINGRES
         INNER JOIN.ADCENATEN AS CEN ON ING.CODCENATE = CEN.CODCENATE
         INNER JOIN.INUNIFUNC AS UNI ON UNI.UFUCODIGO = ING.UFUCODIGO
         INNER JOIN.INPACIENT P ON P.IPCODPACI = ING.IPCODPACI
         INNER JOIN.INUBICACI AS UBI ON UBI.AUUBICACI = P.AUUBICACI
         INNER JOIN.INDIAGNOP AS DIAG ON ING.NUMINGRES = DIAG.NUMINGRES
         INNER JOIN.INDIAGNOS D ON ING.CODDIAING = D.CODDIAGNO
         INNER JOIN.INDIAGNOS D2 ON ING.CODDIAEGR = D2.CODDIAGNO
         INNER JOIN.INDIAGNOS D3 ON DIAG.CODDIAGNO = D3.CODDIAGNO
         INNER JOIN.INENTIDAD ENT ON ING.CODENTIDA = ENT.CODENTIDA
         LEFT OUTER JOIN.ADCONCOEX AS ce ON ING.NUMINGRES = ce.NUMINGRES
                                            AND ING.IPCODPACI = ce.IPCODPACI
                                            AND ce.CONESTADO = '3'
         LEFT OUTER JOIN.AGASICITA AS ci ON ce.NUMCONCIT = ci.CODAUTONU
         LEFT OUTER JOIN.AGACTIMED ACT ON ci.CODACTMED = ACT.CODACTMED
     WHERE DIAG.CODDIAPRI = 1
           AND ING.IFECHAING >= @FechaIni
           AND ING.IFECHAING <= @FechaFin;