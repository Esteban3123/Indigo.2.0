CREATE VIEW [dbo].[IND_AS_HC_EgresosAltaMedica]
AS
     SELECT A.IPCODPACI AS Identificacion, 
            H.NUMINGRES AS Ingreso,
            CASE
                WHEN F.IPTIPODOC = '1'
                THEN 'Cedula de Ciudadania'
                WHEN F.IPTIPODOC = '2'
                THEN 'Cedula de Extranjeria'
                WHEN F.IPTIPODOC = '3'
                THEN 'Tarjeta de Identidad'
                WHEN F.IPTIPODOC = '4'
                THEN 'Registro Civil'
                WHEN F.IPTIPODOC = '5'
                THEN 'Pasaporte'
                WHEN F.IPTIPODOC = '6'
                THEN 'Adulto sin Identificacion'
                WHEN F.IPTIPODOC = '7'
                THEN 'Menor sin Identificacion'
            END AS Tipo_Documento, 
            F.IPEXPEDIC AS Lugar_expedicion, 
            A.NUMINGRES, 
            F.IPNOMCOMP AS Paciente, 
            RTRIM(LTRIM(dbo.Edad(CONVERT(VARCHAR, F.IPFECNACI, 105), CONVERT(VARCHAR, GETDATE(), 105)))) AS Edad,
            CASE F.IPESTADOC
                WHEN '1'
                THEN 'Soltero'
                WHEN '2'
                THEN 'Casado'
                WHEN '3'
                THEN 'Viudo'
                WHEN '4'
                THEN 'Union libre'
                WHEN '5'
                THEN 'Separado_Div'
            END AS EstadoCivil,
            CASE
                WHEN F.IPSEXOPAC = '1'
                THEN 'Masculino'
                WHEN F.IPSEXOPAC = '2'
                THEN 'Femenino'
            END AS Sexo, 
            F.IPDIRECCI AS Direccion, 
            F.IPTELEFON AS Telefono, 
            F.IPTELMOVI AS Movil, 
            E.UFUDESCRI AS [Unidad Funcional], 
            H.IFECHAING AS [Fecha Ingreso], 
            A.FECALTPAC AS [Fecha Alta Medica], 
            MAX(EST.FECFINEST) AS Fecha_Egreso_Cama, 
            DATEDIFF(minute, A.FECALTPAC, MAX(EST.FECFINEST)) AS Diferencia_Minutos, 
            K.NOMENTIDA AS Entidad,
            CASE F.IPTIPOPAC
                WHEN '1'
                THEN 'Contributivo'
                WHEN '2'
                THEN 'Subsidiado'
                WHEN '3'
                THEN 'Vinculado'
                WHEN '4'
                THEN 'Particular'
                WHEN '5'
                THEN 'Desplazado Reg. Contributivo'
                WHEN '6'
                THEN 'Desplazado Reg. Subsidiado'
                WHEN '7'
                THEN 'Desplazado no Asegurado'
            END AS TipoPaciente, 
            J.CODDIAGNO AS CodDiag, 
            J.NOMDIAGNO AS DiagnosticoPpal, 
            H.IAUTORIZA AS [Nro. Autorizacion], 
            H.IOBSERVAC AS Observaciones,
            CASE ESTPACEGR
                WHEN 1
                THEN 'Mejor'
                WHEN 2
                THEN 'Igual o Peor'
                WHEN 3
                THEN 'Fallecido'
                WHEN 4
                THEN 'Remitido'
                WHEN 5
                THEN 'Hospitalizacion en Casa'
            END AS EstadoEgreso,
            CASE HC.INDICAPAC
                WHEN '1'
                THEN 'Trasladar a Urgencias Solo desde consulta externa'
                WHEN '2'
                THEN 'Trasladar a Observacion Urgencias  solo desde Urgencias'
                WHEN '3'
                THEN 'Trasladar a Hospitalizacion Dif Misma Unidad'
                WHEN '4'
                THEN 'Trasladar a  UCI Adulto Dif Misma Unidad'
                WHEN '5'
                THEN 'Trasladar a UCI Pediatrica Dif Misma Unidad'
                WHEN '6'
                THEN 'Trasladar a UCI Neonatal Dif Misma Unidad'
                WHEN '7'
                THEN 'Trasladar a Consulta Externa Dif Misma 
Unidad'
                WHEN '8'
                THEN 'Trasladar a  Cirugia Dif Misma Unidad'
                WHEN '9'
                THEN 'Hospitalizacion en Casa'
                WHEN '10'
                THEN 'Referencia'
                WHEN '11'
                THEN 'Morgue'
                WHEN '12'
                THEN 'Salida'
                WHEN '13'
                THEN 'Continua en la Unidad'
                WHEN '15'
                THEN 'Retiro Voluntario'
                WHEN '16'
                THEN 'Fuga'
            END AS DestinoPaciente, 
            SUBSTRING(F.AUUBICACI, 1, 5) AS CodMunicipio, 
            M.MUNNOMBRE AS Municipio, 
            SUBSTRING(F.AUUBICACI, 1, 2) AS CodDepto, 
            D.nomdepart AS Departamento
     FROM dbo.HCREGEGRE AS A WITH(NOLOCK)
          INNER JOIN dbo.HCHISPACA AS HC WITH(NOLOCK) ON HC.IPCODPACI = A.IPCODPACI
                                                         AND HC.NUMINGRES = A.NUMINGRES
                                                         AND HC.NUMEFOLIO = A.NUMEFOLIO
                                                         AND A.FECALTPAC BETWEEN '01/01/2019 00:00:00' AND '31/12/2019 23:59:59'
          INNER JOIN dbo.INUNIFUNC AS E WITH(NOLOCK) ON E.UFUCODIGO = A.UFUCODIGO
          INNER JOIN dbo.INPACIENT AS F WITH(NOLOCK) ON F.IPCODPACI = A.IPCODPACI
          INNER JOIN dbo.INMUNICIP AS M WITH(NOLOCK) ON M.DEPMUNCOD = SUBSTRING(F.AUUBICACI, 1, 5)
          INNER JOIN dbo.INDEPARTA AS D WITH(NOLOCK) ON D.depcodigo = SUBSTRING(F.AUUBICACI, 1, 2)
          INNER JOIN dbo.ADINGRESO AS H WITH(NOLOCK) ON H.IPCODPACI = F.IPCODPACI
                                                        AND A.NUMINGRES = H.NUMINGRES
          LEFT OUTER JOIN dbo.INENTIDAD AS AE WITH(NOLOCK) ON AE.CODENTIDA = H.CODENTIDA
          INNER JOIN dbo.INDIAGNOS AS J WITH(NOLOCK) ON J.CODDIAGNO = H.CODDIAING
          INNER JOIN dbo.INENTIDAD AS K WITH(NOLOCK) ON K.CODENTIDA = AE.CODENTIDA
          LEFT OUTER JOIN dbo.CHREGESTA AS EST WITH(NOLOCK) ON EST.NUMINGRES = H.NUMINGRES
     WHERE(A.FECALTPAC BETWEEN '01/01/2019 00:00:00' AND '31/12/2019 23:59:59')
     GROUP BY A.IPCODPACI, 
              H.NUMINGRES, 
              F.IPTIPODOC, 
              F.IPEXPEDIC, 
              A.NUMINGRES, 
              F.IPNOMCOMP, 
              F.IPFECNACI, 
              F.IPESTADOC, 
              F.IPSEXOPAC, 
              F.IPDIRECCI, 
              F.IPTELEFON, 
              F.IPTELMOVI, 
              E.UFUDESCRI, 
              H.IFECHAING, 
              HC.FECHISPAC, 
              A.FECALTPAC, 
              K.NOMENTIDA, 
              F.IPTIPOPAC, 
              J.CODDIAGNO, 
              J.NOMDIAGNO, 
              H.IOBSERVAC, 
              A.ESTPACEGR, 
              HC.INDICAPAC, 
              F.AUUBICACI, 
              M.MUNNOMBRE, 
              F.AUUBICACI, 
              D.nomdepart, 
              H.IAUTORIZA;