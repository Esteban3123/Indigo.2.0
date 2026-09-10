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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que lista los ingresos (admisiones) de pacientes que permanecen abiertos o en estado parcial dentro de un rango de fechas dado, útil para seguimiento de facturación pendiente y auditoría de gestión de camas. Combina datos del ingreso (ADINGRESO) con información del paciente (INPACIENT), la entidad pagadora o aseguradora (INENTIDAD), el estado de facturación (No Facturado, Parcial), el egreso médico y de enfermería (HCREGEGRE, CHREGEGRE), la factura asociada (Billing.Invoice), el control de ingresos (Billing.RevenueControl), la orden de servicio más reciente gestionada por usuarios con roles 5 o 17 (Billing.ServiceOrder), y el slip de salida (Billing.SlipOut). Para cada ingreso abierto muestra: cédula y nombre del paciente, número de ingreso, vía de ingreso (urgencias, consulta externa, hospitalización, remitido, nacido en hospital), usuarios que crearon, modificaron o anularon el registro, fechas clave, cama asignada, folio de control de cartera y datos de la última orden de servicio gestionada por el equipo de facturación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPU_IngresosAbiertos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPU_IngresosAbiertos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Listar los ingresos hospitalarios aún abiertos (no facturados o parcialmente facturados) en un rango de fechas, junto con datos del paciente, entidad, egresos, factura, control de ingresos y la última orden de servicio creada/modificada por usuarios de roles específicos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_IngresosAbiertos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las fechas inicial y final deben estar definidas y delimitan el rango de IFECHAING a consultar.; Deben existir usuarios con RollCode 5 o 17 en Security.User para que se obtenga información de órdenes de servicio asociadas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_IngresosAbiertos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan ingresos con estado abierto: No Facturado ('' '') o Parcial (''P'').; Para cada ingreso se asocia a lo sumo una orden de servicio: la más reciente por ModificationDate y luego CreationDate.; Las órdenes de servicio consideradas provienen exclusivamente de usuarios con RollCode 5 o 17.; Cuando no existe folio en RevenueControlDetail, se devuelve FolioOrder = 0 (COALESCE).; Los ingresos se ordenan ascendentemente por fecha de ingreso (IFECHAING).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_IngresosAbiertos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ingreso hospitalario; Estado de facturación del ingreso (No Facturado, Parcial, Cerrado, Facturado, Anulado); Origen del ingreso (Urgencias, Consulta Externa, Nacido Hospital, Remitido, Hospitalización Urgencias); Paciente; Entidad responsable de pago; Egreso de enfermería y egreso médico; Cama hospitalaria; Factura; Control de ingresos y folios de facturación; Comprobante de salida (Slip Out); Orden de servicio; Usuario y rol de seguridad', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_IngresosAbiertos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve únicamente ingresos cuyo IESTADOIN sea '' '' (No Facturado) o ''P'' (Parcial) dentro del rango [@fechaInicial, @fechaFinal] sobre IFECHAING.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_IngresosAbiertos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ADINGRESO.IESTADOIN = '' '' → Se etiqueta el estado como ''No Facturado'' y se incluye en el resultado; si ADINGRESO.IESTADOIN = ''P'' → Se etiqueta el estado como ''Parcial'' y se incluye en el resultado; si ADINGRESO.IESTADOIN IN (''C'',''F'',''A'') → Se traduce a ''Cerrado''/''Facturado''/''Anulado'' pero se EXCLUYE del resultado por el WHERE; si ADINGRESO.IINGREPOR entre ''1'' y ''5'' → Se traduce el código de origen del ingreso a su descripción (URGENCIAS, CONSULTA EXTERNA, NACIDO HOSPITAL, REMITIDO, HOSPITALIZACION URGENCIAS); si Security.User.RollCode IN (5,17) → Sus órdenes de servicio (creadas o modificadas por ellos) son consideradas para asociar la última orden a cada ingreso', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_IngresosAbiertos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Security.User; Billing.ServiceOrder; dbo.SEGusuaru; Billing.ServiceOrder; dbo.ADINGRESO; dbo.INPACIENT; dbo.INENTIDAD; dbo.HCREGEGRE; dbo.CHREGEGRE; Billing.RevenueControl; Billing.RevenueControlDetail; Billing.SlipOut; Billing.Invoice', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_IngresosAbiertos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPU_IngresosAbiertos';
-- GO
