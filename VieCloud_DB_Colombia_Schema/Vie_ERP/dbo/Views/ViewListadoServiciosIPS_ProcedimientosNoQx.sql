CREATE VIEW dbo.ViewListadoServiciosIPS_ProcedimientosNoQx
AS
SELECT        RTRIM(CODSERIPS) AS Codigo, RTRIM(DESSERIPS) AS Servicio, TIPSERTER AS Terapia, RTRIM(CODSERIPS) + ' - ' + RTRIM(DESSERIPS) AS CodigoDescripcion, SERREASIT AS ServicioRealizaSitio, 
                         IPSSERIAD AS ServicioSeriado, SERIPSDASH, TIPSERIPS, OXIGENSERVICE, ISNULL(SERIPSPOS, CAST(0 AS BIT)) AS SERIPSPOS, RequiresLaterality,
iif (( select COUNT(*) from HCPLANDOC a inner join HCPLANDOCCUPS b on a.CODCONSEC = b.IDHCPLANDOC where a.TIPDOCUME = 0 and b.CODSERIPS = S.CODSERIPS) >= 1,2,1) as ConsentimientoInformado
FROM            dbo.INCUPSIPS S
WHERE        (TIPSERIPS = '4') AND (SIPSESTADO = '1')
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewListadoServiciosIPS_ProcedimientosNoQx';


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
         Begin Table = "INCUPSIPS (dbo)"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 264
            End
            DisplayFlags = 280
            TopColumn = 0
         End
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewListadoServiciosIPS_ProcedimientosNoQx';

GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el catálogo de servicios IPS clasificados como procedimientos no quirúrgicos activos, indicando atributos clínicos/administrativos y si requieren consentimiento informado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewListadoServiciosIPS_ProcedimientosNoQx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan servicios cuyo tipo de servicio IPS es ''4'' (procedimientos no quirúrgicos); Solo se listan servicios activos (SIPSESTADO = ''1''); Si SERIPSPOS es NULL se asume 0 (false) por defecto; El campo CodigoDescripcion siempre se construye como ''código - descripción'' con espacios recortados', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewListadoServiciosIPS_ProcedimientosNoQx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Servicios IPS; Procedimientos no quirúrgicos; CUPS; Terapia; Consentimiento informado; Lateralidad; Servicio POS; Oxígeno; Servicio seriado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewListadoServiciosIPS_ProcedimientosNoQx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.INCUPSIPS: Cuando TIPSERIPS=''4'' y SIPSESTADO=''1'' se retorna el servicio con sus atributos y un indicador de consentimiento informado calculado por subconsulta', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewListadoServiciosIPS_ProcedimientosNoQx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe al menos un registro en HCPLANDOC (TIPDOCUME=0) ligado vía HCPLANDOCCUPS al código de servicio → ConsentimientoInformado = 2 (requiere consentimiento informado) else ConsentimientoInformado = 1 (no requiere)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewListadoServiciosIPS_ProcedimientosNoQx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INCUPSIPS; dbo.HCPLANDOC; dbo.HCPLANDOCCUPS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewListadoServiciosIPS_ProcedimientosNoQx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewListadoServiciosIPS_ProcedimientosNoQx';
GO
