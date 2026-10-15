CREATE PROCEDURE [dbo].[SPU_Agendamiento_Facturacion_Detalle] @FechaInicial DATE, 
                                                             @FechaFinal   DATE, 
                                                             @Especialidad CHAR(3)
AS
     WITH AG
          AS (SELECT CI.CODAUTONU, 
                     CI.CODESPECI, 
                     ES.DESESPECI AS EspecialidadAgendada, 
                     CI.IPCODPACI, 
                     CI.CODPROSAL, 
                     PS.NOMMEDICO AS NombreMedico, 
                     CI.FECHORAIN, 
                     AM.DESACTMED AS ActividadAgendada,
                     CASE CI.CODESTCIT
                         WHEN 0
                         THEN 'Asignada'
                         WHEN 1
                         THEN 'Cumplida'
                         WHEN 2
                         THEN 'Incumplida'
                         WHEN 3
                         THEN 'PreAsignada'
                         WHEN 4
                         THEN 'Cancelada'
                     END AS EstadoAgendamiento
              FROM DBO.AGASICITA AS CI
                   INNER JOIN DBO.INESPECIA AS ES ON CI.CODESPECI = ES.CODESPECI
                   INNER JOIN DBO.AGACTIMED AS AM ON CI.CODACTMED = AM.CODACTMED
                   INNER JOIN dbo.INPROFSAL AS PS ON PS.CODPROSAL = CI.CODPROSAL
              WHERE CI.FECHORAIN >= @FechaInicial
                    AND CI.FECHORAIN < @FechaFinal
                    AND CI.CODESPECI = @Especialidad
                    AND CI.CODESTCIT <> 4)
          SELECT BID.InvoiceId, 
                 BID.ServiceOrderDetailId, 
                 BID.ServiceDate, 
                 BID.GrandTotalSalesPrice, 
                 BID.ThirdPartySalesPrice, 
                 BID.SubTotalPatientSalesPrice, 
                 BI.Id, 
                 BI.InvoiceNumber, 
                 BI.AdmissionNumber
          FROM Billing.Invoice AS BI
               INNER JOIN AG ON AG.IPCODPACI = BI.PatientCode
                                AND CAST(AG.FECHORAIN AS DATE) = CAST(BI.InvoiceDate AS DATE)
               INNER JOIN Billing.InvoiceDetail AS BID ON BID.InvoiceId = BI.Id
          ORDER BY BI.InvoiceDate;
     RETURN 0;