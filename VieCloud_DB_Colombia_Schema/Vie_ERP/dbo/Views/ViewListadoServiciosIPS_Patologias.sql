CREATE VIEW dbo.ViewListadoServiciosIPS_Patologias
AS
SELECT        RTRIM(CODSERIPS) AS Codigo, RTRIM(DESSERIPS) AS Servicio, TIPSERTER AS Terapia, RTRIM(CODSERIPS) + ' - ' + RTRIM(DESSERIPS) AS CodigoDescripcion, SERREASIT AS ServicioRealizaSitio, 
                         IPSSERIAD AS ServicioSeriado, SERIPSDASH, TIPSERIPS, OXIGENSERVICE, ISNULL(SERIPSPOS, CAST(0 AS BIT)) AS SERIPSPOS, RequiresLaterality, iif
                             ((SELECT        COUNT(*)
                                 FROM            HCPLANDOC a INNER JOIN
                                                          HCPLANDOCCUPS b ON a.CODCONSEC = b.IDHCPLANDOC
                                 WHERE        a.TIPDOCUME = 0 AND b.CODSERIPS = S.CODSERIPS) >= 1, 2, 1) AS ConsentimientoInformado
FROM            dbo.INCUPSIPS S
WHERE        (TIPSERIPS = '2') AND (SIPSESTADO = '1')
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewListadoServiciosIPS_Patologias';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
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
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewListadoServiciosIPS_Patologias';

GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Listar los servicios IPS activos clasificados como patologías, indicando atributos clínicos/administrativos y si requieren consentimiento informado según los planes documentales asociados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewListadoServiciosIPS_Patologias';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La tabla INCUPSIPS debe contener servicios marcados con TIPSERIPS=''2'' y SIPSESTADO=''1'' para retornar filas; Las tablas HCPLANDOC y HCPLANDOCCUPS deben existir y estar relacionadas por CODCONSEC=IDHCPLANDOC para evaluar el consentimiento informado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewListadoServiciosIPS_Patologias';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen servicios de tipo ''2'' (patologías) y con estado activo (''1''); Si SERIPSPOS es NULL se asume 0 (no POS); El campo CodigoDescripcion siempre concatena código y descripción separados por '' - ''; Los códigos y descripciones se entregan sin espacios en blanco a la derecha (RTRIM)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewListadoServiciosIPS_Patologias';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Servicios IPS; Patologías; Consentimiento informado; Terapia; Servicio seriado; Servicio POS; Lateralidad; Oxígeno; CUPS; Plan documental de historia clínica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewListadoServiciosIPS_Patologias';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.INCUPSIPS: Cuando TIPSERIPS=''2'' y SIPSESTADO=''1'', se retorna el servicio con sus atributos y se calcula ConsentimientoInformado en función de la existencia de planes documentales (TIPDOCUME=0) asociados al CUPS del servicio', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewListadoServiciosIPS_Patologias';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe al menos un registro en HCPLANDOC (TIPDOCUME = 0) vinculado a HCPLANDOCCUPS para el servicio actual → ConsentimientoInformado = 2 (requiere consentimiento) else ConsentimientoInformado = 1 (no requiere consentimiento)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewListadoServiciosIPS_Patologias';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INCUPSIPS; dbo.HCPLANDOC; dbo.HCPLANDOCCUPS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewListadoServiciosIPS_Patologias';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewListadoServiciosIPS_Patologias';
GO
