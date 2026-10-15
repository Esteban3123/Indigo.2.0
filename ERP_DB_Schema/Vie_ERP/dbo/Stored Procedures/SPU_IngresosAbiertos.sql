CREATE PROCEDURE [dbo].[SPU_IngresosAbiertos] @fechaInicial AS DATETIME, 
                                             @fechaFinal AS   DATETIME
AS
     WITH US
          AS (SELECT UN.UserCode
              FROM Security.[User] AS UN
              WHERE UN.RollCode IN(5, 17)),
          OSs
          AS (SELECT SO.AdmissionNumber, 
                     SO.CreationUser, 
                     US.NOMUSUARI AS CreationUserName, 
                     SO.CreationDate, 
                     SO.ModificationUser, 
                     SO.ModificationDate
              FROM Billing.ServiceOrder AS SO
                   INNER JOIN dbo.SEGusuaru AS US ON SO.CreationUser = US.CODUSUARI
              WHERE SO.CreationUser IN
              (
                  SELECT US.UserCode
                  FROM US
              )
                    OR SO.ModificationUser IN
              (
                  SELECT US.UserCode
                  FROM US
              )
              --	ORDER BY SO.AdmissionNumber, SO.ModificationDate DESC, SO.CreationDate DESC
              )
          SELECT ADINGRESO.IPCODPACI, 
                 INPACIENT.IPNOMCOMP, 
                 ADINGRESO.NUMINGRES,
                 CASE
                     WHEN ADINGRESO.IESTADOIN = ' '
                     THEN 'No Facturado'
                     WHEN ADINGRESO.IESTADOIN = 'C'
                     THEN 'Cerrado'
                     WHEN ADINGRESO.IESTADOIN = 'F'
                     THEN 'Facturado'
                     WHEN ADINGRESO.IESTADOIN = 'P'
                     THEN 'Parcial'
                     WHEN ADINGRESO.IESTADOIN = 'A'
                     THEN 'Anulado'
                 END AS [ESTADO INGRESO], 
                 ADINGRESO.IFECHAING, 
                 ADINGRESO.CODUSUCRE, 
                 SEGusuaru_2.NOMUSUARI AS USUARIO_crea, 
                 ADINGRESO.UFUCODIGO, 
                 ADINGRESO.FECREGCRE, 
                 ADINGRESO.CODUSUMOD, 
                 SEGusuaru_1.NOMUSUARI, 
                 ADINGRESO.FECREGMOD, 
                 ADINGRESO.CODUSUANU, 
                 SEGusuaru.NOMUSUARI AS USUARIO2, 
                 ADINGRESO.FECREGANU, 
                 ADINGRESO.IJUSTIFIC,
                 CASE
                     WHEN ADINGRESO.IINGREPOR = '1'
                     THEN 'URGENCIAS'
                     WHEN ADINGRESO.IINGREPOR = '2'
                     THEN 'CONSULTA EXTERNA'
                     WHEN ADINGRESO.IINGREPOR = '3'
                     THEN 'NACIDO HOSPITAL'
                     WHEN ADINGRESO.IINGREPOR = '4'
                     THEN 'REMITIDO'
                     WHEN ADINGRESO.IINGREPOR = '5'
                     THEN 'HOSPITALIZACION URGENCIAS'
                 END AS [INGRESA POR], 
                 ADINGRESO.IOBSERVAC, 
                 INENTIDAD.NOMENTIDA, 
                 BS.CreationDate, 
                 bs.CreationUser, 
                 CHREGEGRE.FECEGRESO AS F_egresoEnfermeria, 
                 HCREGEGRE.FECALTPAC AS F_EgresoMedico, 
                 ADINGRESO.CODICAMHO AS Cod_cama, 
                 F.InvoiceNumber, 
                 F.InvoicedUser, 
                 COALESCE(RCD.FolioOrder, 0) AS FolioOrder, 
                 CAST(RTRIM(RCD.Observation) AS VARCHAR(4000)) AS FolioObservation, 
                 OS2.CreationUser AS CreatUserOS, 
                 OS2.CreationUserName AS CreatUserNameOS, 
                 OS2.CreationDate AS CreatDateOS, 
                 OS2.ModificationUser AS ModificUserOS, 
                 OS2.ModificationDate AS ModificDateOS
          FROM ADINGRESO
               INNER JOIN INPACIENT ON ADINGRESO.IPCODPACI = INPACIENT.IPCODPACI
               INNER JOIN INENTIDAD ON ADINGRESO.CODENTIDA = INENTIDAD.CODENTIDA
               LEFT OUTER JOIN HCREGEGRE ON ADINGRESO.NUMINGRES = HCREGEGRE.NUMINGRES
               LEFT OUTER JOIN CHREGEGRE ON ADINGRESO.NUMINGRES = CHREGEGRE.NUMINGRES
               LEFT OUTER JOIN SEGusuaru ON ADINGRESO.CODUSUANU = SEGusuaru.CODUSUARI
               LEFT OUTER JOIN SEGusuaru AS SEGusuaru_2 ON ADINGRESO.CODUSUCRE = SEGusuaru_2.CODUSUARI
               LEFT OUTER JOIN SEGusuaru AS SEGusuaru_1 ON ADINGRESO.CODUSUMOD = SEGusuaru_1.CODUSUARI
               LEFT OUTER JOIN Billing.RevenueControl AS RC ON RC.AdmissionNumber = ADINGRESO.NUMINGRES
               LEFT OUTER JOIN Billing.RevenueControlDetail AS RCD ON RCD.RevenueControlId = RC.Id
               LEFT OUTER JOIN Billing.SlipOut AS BS ON ADINGRESO.NUMINGRES = BS.AdmissionNumber
               LEFT OUTER JOIN Billing.Invoice AS F ON ADINGRESO.NUMINGRES = F.AdmissionNumber
               OUTER APPLY
          (
              SELECT TOP (1) OSs.CreationUser, 
                             OSs.CreationUserName, 
                             OSs.CreationDate, 
                             OSs.ModificationUser, 
                             OSs.ModificationDate
              FROM OSs
              WHERE OSs.AdmissionNumber = ADINGRESO.NUMINGRES
              ORDER BY OSs.AdmissionNumber, 
                       OSs.ModificationDate DESC, 
                       OSs.CreationDate DESC
          ) AS OS2
          WHERE(ADINGRESO.IFECHAING BETWEEN CAST(@fechaInicial AS DATETIME) AND CAST(@fechaFinal AS DATETIME))
               AND (ADINGRESO.IESTADOIN = ' '
                    OR ADINGRESO.IESTADOIN = 'P') 
          --and ADINGRESO.NUMINGRES = '1835217'        
          ORDER BY ADINGRESO.IFECHAING;
     RETURN;