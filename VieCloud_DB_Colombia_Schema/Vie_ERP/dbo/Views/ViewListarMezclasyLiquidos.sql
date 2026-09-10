CREATE VIEW dbo.ViewListarMezclasyLiquidos
AS
SELECT        CASE WHEN NSD.OBSERVACI IS NULL THEN 0 ELSE 1 END AS 'AtencionFarmaceutica', NSD.AppropriateTreatment, NSD.OBSERVACI AS 'Observaciones', NSD.ConfirmAppropriateTreatment, 
                         NSD.ObservationConfirm AS 'ObservacionesConfirmacion', NS.OBSERVACI AS 'ObservacionesGenerales', NSD.ID AS 'IDHCNOSERFD', NSD.CODPRODUC, NSD.MedicalObservation, NSD.State, A.IPCODPACI, A.NUMINGRES, 
                         FECHAINIC AS 'Fecha Inicio', DATEDIFF(day, FECHAINIC, GETDATE()) AS DiasTranscurridos, RTRIM(MEZLIQPAC) AS 'Mezcla / Liquido', RTRIM(B.NOMDIAGNO) AS 'Dx / Motivo', RTRIM(ADMMEZLIQ) AS Administracion, 
                         RTRIM(INDAPLMED) AS Indicaciones, PREESTADO AS Estado, RTRIM(C.NOMMEDICO) AS Medico, RTRIM(D .UFUDESCRI) AS Unidad, '0' AS Nuevo, '0' Modificado, 'Folio: ' + RTRIM(A.NUMEFOLIO) 
                         + ' - ' + (CASE WHEN A.ReasonDiscontinuationOfDrug = '1' THEN 'Riesgos y reacciones adversas de medicamentos: ' + (CASE WHEN A.PatientRiskLevel = '1' THEN 'Reacción alérgica.' WHEN A.PatientRiskLevel = '2' THEN 'Reacción adversa.'
                          WHEN A.PatientRiskLevel = '3' THEN 'Intolerancia.' ELSE 'No definido.' END) + (IIF(RTRIM(A.PatientRiskLevelObservations) IS NOT NULL, ' ' + RTRIM(A.PatientRiskLevelObservations), '')) 
                         WHEN A.ReasonDiscontinuationOfDrug = '2' THEN 'Otra razón o motivo' + (IIF(RTRIM(A.MOTSUSMED) IS NOT NULL, ': ' + RTRIM(A.MOTSUSMED), '.')) ELSE 'Otra razón o motivo.' + (IIF(RTRIM(A.MOTSUSMED) IS NOT NULL, 
                         ' ' + RTRIM(A.MOTSUSMED), '')) END) AS 'Motivo Suspension', A.NUMEFOLIO AS Folio, RTRIM(A.CODCONCEC) AS ConsecutivoCabecera, CONSECUTI AS ConsecutivoTabla, RTRIM(N .DESESPECI) AS Especialidad, CONVERT(int, 
                         A.TIPMEZLIQ) AS TIPMEZLIQ, 2 AS CENTRALMEZCLAS, CONVERT(BIT, 0) AS Reformulacion
FROM            dbo.HCINFLIQA A INNER JOIN
                         dbo.INDIAGNOS B ON A.CODDIAGNO = B.CODDIAGNO INNER JOIN
                         dbo.INPROFSAL C ON A.CODPROSAL = C.CODPROSAL INNER JOIN
                         dbo.INUNIFUNC D ON A.UFUCODIGO = D .UFUCODIGO INNER JOIN
                         dbo.HCHISPACA J ON A.NUMEFOLIO = J.NUMEFOLIO AND A.IPCODPACI = J.IPCODPACI LEFT OUTER JOIN
                         dbo.INESPECIA N ON J.CODESPTRA = N .CODESPECI LEFT JOIN
                         dbo.HCNOSERFD AS NSD WITH (nolock) ON NSD.IdSourceTable = A.CONSECUTI AND NSD.CurrentMedication = 1 LEFT JOIN
                         dbo.HCNOSERFA AS NS WITH (nolock) ON NS.[AUTO] = NSD.CODCONCEC
UNION ALL
/* Consultamos si el paciente tiene nutriciones parenterales*/ SELECT 0 AS 'AtencionFarmaceutica', NSD.AppropriateTreatment, NSD.OBSERVACI, NSD.ConfirmAppropriateTreatment, 
                         NSD.ObservationConfirm AS 'ObservacionesConfirmacion', NS.OBSERVACI AS 'ObservacionesGenerales', NSD.ID AS 'IDHCNOSERFD', NSD.CODPRODUC, NSD.MedicalObservation, NSD.State, A.IPCODPACI, A.NUMINGRES, 
                         A.FECHAORDEN AS 'Fecha Inicio', DATEDIFF(day, A.FECHAORDEN, GETDATE()) AS DiasTranscurridos, RTRIM(B.NAME) AS 'Mezcla / Liquido', RTRIM(C.NOMDIAGNO) AS 'Dx / Motivo', RTRIM(A.INDICACIONADM) 
                         AS 'Administracion', RTRIM(A.INDICACIONADI) AS 'Indicaciones', CASE A.STATUS WHEN 4 THEN 1 WHEN 2 THEN 4 WHEN 5 THEN 2 ELSE A.STATUS END AS 'Estado', RTRIM(D .NOMMEDICO) AS 'Medico', 'No Aplica' AS 'Unidad', '0' AS Nuevo, '0' Modificado, 'No aplica' AS 'Motivo Suspension', 
                         A.FOLIORDEN AS Folio, Rtrim(A.IDHCPARNUTC) AS ConsecutivoCabecera, A.ID AS ConsecutivoTabla, 'No Aplica', 5 AS TIPMEZLIQ, 1 AS CENTRALMEZCLAS, CONVERT(BIT, 0) AS Reformulacion
FROM            dbo.HCNUTPAREC A INNER JOIN
                         HCPARNUTC B WITH (Nolock) ON A.IDHCPARNUTC = B.ID INNER JOIN
                         dbo.INDIAGNOS C ON A.CODDIAGNO = C.CODDIAGNO INNER JOIN
                         dbo.INPROFSAL D ON A.CODPROSAL = D .CODPROSAL LEFT JOIN
                         dbo.HCNOSERFD AS NSD WITH (nolock) ON NSD.IdSourceTable = A.ID AND NSD.CurrentMedication = 1 LEFT JOIN
                         dbo.HCNOSERFA AS NS WITH (nolock) ON NS.[AUTO] = NSD.CODCONCEC
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewListarMezclasyLiquidos';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[12] 4[26] 2[43] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewListarMezclasyLiquidos';

GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en un único listado las mezclas y líquidos endovenosos junto con las nutriciones parenterales prescritas a un paciente, incluyendo seguimiento farmacéutico, motivo de suspensión y estado actual.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewListarMezclasyLiquidos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada registro de HCINFLIQA debe tener diagnóstico (INDIAGNOS), profesional (INPROFSAL), unidad funcional (INUNIFUNC) e historia clínica (HCHISPACA) asociados (INNER JOIN).; Cada registro de HCNUTPAREC debe tener cabecera de nutrición parenteral (HCPARNUTC), diagnóstico (INDIAGNOS) y profesional (INPROFSAL) asociados (INNER JOIN).; Las notas farmacéuticas (HCNOSERFD) consideradas como vigentes requieren CurrentMedication = 1.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewListarMezclasyLiquidos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los registros provenientes de nutrición parenteral siempre tienen TIPMEZLIQ=5, CENTRALMEZCLAS=1, Unidad=''No Aplica'', Especialidad=''No Aplica'' y MotivoSuspension=''No aplica''.; Los registros de mezclas/líquidos siempre tienen CENTRALMEZCLAS=2.; El campo Reformulacion siempre se devuelve como 0 (BIT).; Los campos Nuevo y Modificado siempre se devuelven como ''0''.; AtencionFarmaceutica para nutriciones parenterales siempre es 0, sin importar si existen observaciones farmacéuticas.; DiasTranscurridos se calcula como diferencia en días entre la fecha de inicio/orden y la fecha actual (GETDATE).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewListarMezclasyLiquidos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Mezclas y líquidos endovenosos; Nutrición parenteral; Atención farmacéutica; Reacción alérgica / adversa / intolerancia; Motivo de suspensión de medicamento; Diagnóstico clínico; Especialidad médica; Unidad funcional; Historia clínica / folio; Central de mezclas; Confirmación de tratamiento apropiado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewListarMezclasyLiquidos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Devuelve la unión de mezclas/líquidos (HCINFLIQA) y nutriciones parenterales (HCNUTPAREC) del paciente con datos clínicos, farmacéuticos y de seguimiento.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewListarMezclasyLiquidos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si NSD.OBSERVACI IS NULL en HCINFLIQA → AtencionFarmaceutica = 0 (no hay seguimiento farmacéutico) else AtencionFarmaceutica = 1 (existe observación farmacéutica); si A.ReasonDiscontinuationOfDrug = ''1'' (mezcla/líquido) → Motivo de suspensión = ''Riesgos y reacciones adversas de medicamentos'' clasificado por PatientRiskLevel: 1=Reacción alérgica, 2=Reacción adversa, 3=Intolerancia, otro=No definido, anexando observaciones si existen; si A.ReasonDiscontinuationOfDrug = ''2'' → Motivo de suspensión = ''Otra razón o motivo'' concatenando MOTSUSMED si existe else Motivo de suspensión = ''Otra razón o motivo.'' (con MOTSUSMED si existe); si Estado de nutrición parenteral A.STATUS → Se remapea: 4→1, 2→4, 5→2; cualquier otro valor se conserva tal cual; si Origen del registro → Si proviene de HCINFLIQA → CENTRALMEZCLAS=2 y TIPMEZLIQ=valor original; si proviene de HCNUTPAREC → CENTRALMEZCLAS=1 y TIPMEZLIQ=5', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewListarMezclasyLiquidos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCINFLIQA; dbo.INDIAGNOS; dbo.INPROFSAL; dbo.INUNIFUNC; dbo.HCHISPACA; dbo.INESPECIA; dbo.HCNOSERFD; dbo.HCNOSERFA; dbo.HCNUTPAREC; dbo.HCPARNUTC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewListarMezclasyLiquidos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewListarMezclasyLiquidos';
GO
