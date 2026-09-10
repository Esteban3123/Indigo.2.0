
CREATE VIEW [dbo].[consulta dr roa estancias]
AS
SELECT        dbo.CHREGESTA.NUMINGRES AS INGRESO, dbo.CHCAMASHO.NUMCAMHOS AS [CAMA ACTUAL], dbo.CHREGESTA.FECINIEST AS [INICIO EN CAMA ACTUAL], 
                         dbo.ADINGRESO.FECHOSPIT AS [FECHA DE HOSPITALIZACION], dbo.INUNIFUNC.UFUDESCRI AS [UNIDAD FUNCIONAL], 
                         dbo.CHREGESTA.IPCODPACI AS IDENTIFICACION, dbo.CHREGESTA.REGESTADO, dbo.INPACIENT.IPNOMCOMP AS NOMBRE, 
                         dbo.ADINGRESO.CODENTIDA AS [COD ENTIDAD], dbo.INENTIDAD.NOMENTIDA, DATEDIFF(DAY, dbo.ADINGRESO.FECHOSPIT, GETDATE()) AS DIAS
FROM            dbo.CHREGESTA INNER JOIN
                         dbo.INPACIENT ON dbo.CHREGESTA.IPCODPACI = dbo.INPACIENT.IPCODPACI INNER JOIN
                         dbo.ADINGRESO ON dbo.CHREGESTA.NUMINGRES = dbo.ADINGRESO.NUMINGRES AND dbo.INPACIENT.IPCODPACI = dbo.ADINGRESO.IPCODPACI INNER JOIN
                         dbo.CHCAMASHO ON dbo.CHREGESTA.CODICAMAS = dbo.CHCAMASHO.CODICAMAS INNER JOIN
                         dbo.INUNIFUNC ON dbo.CHCAMASHO.UFUCODIGO = dbo.INUNIFUNC.UFUCODIGO INNER JOIN
                         dbo.INENTIDAD ON dbo.ADINGRESO.CODENTIDA = dbo.INENTIDAD.CODENTIDA
WHERE        (dbo.CHREGESTA.REGESTADO = 1)
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de consulta operativa orientada al seguimiento de estancias hospitalarias activas (estado = 1). Consolida para cada ingreso vigente: la cama actual, la unidad funcional donde se ubica, la fecha de hospitalización, el número de días transcurridos desde el ingreso hasta hoy, el paciente con su identificación y nombre, y la entidad aseguradora asociada. Sirve como herramienta de monitoreo de ocupación y permanencia para el Dr. Roa.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'consulta dr roa estancias';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'consulta dr roa estancias';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los pacientes actualmente hospitalizados mostrando su cama asignada, unidad funcional, entidad responsable de pago y los días transcurridos desde la hospitalización.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'consulta dr roa estancias';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de registros de estancia (CHREGESTA) vinculados a un ingreso (ADINGRESO), un paciente (INPACIENT), una cama (CHCAMASHO) con unidad funcional (INUNIFUNC) y entidad pagadora (INENTIDAD).; El paciente del registro de estancia debe coincidir con el paciente del ingreso (IPCODPACI igual en CHREGESTA y ADINGRESO).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'consulta dr roa estancias';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen estancias cuyo estado es 1 (activas).; Cada fila representa la cama actual del paciente en su ingreso vigente, ya que se cruza CHREGESTA.CODICAMAS con CHCAMASHO.; El cálculo de días se basa en la fecha de hospitalización del ingreso, no en la fecha de inicio de la cama actual.; Se requiere obligatoriamente entidad pagadora, unidad funcional y cama (INNER JOIN), por lo que estancias sin alguno de estos datos no aparecen.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'consulta dr roa estancias';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso/Hospitalización; Estancia hospitalaria; Cama; Unidad funcional; Entidad pagadora; Días de estancia', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'consulta dr roa estancias';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve solo estancias activas (REGESTADO = 1), calculando días de hospitalización como DATEDIFF(DAY, FECHOSPIT, GETDATE()).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'consulta dr roa estancias';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CHREGESTA.REGESTADO = 1 → Se incluye la estancia en el resultado (estancia vigente/activa) else Se excluye la estancia (no se reportan estancias cerradas o con otro estado)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'consulta dr roa estancias';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.CHREGESTA; dbo.INPACIENT; dbo.ADINGRESO; dbo.CHCAMASHO; dbo.INUNIFUNC; dbo.INENTIDAD', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'consulta dr roa estancias';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'consulta dr roa estancias';
GO
