

CREATE VIEW [Contract].[ViewListDashboardPgp]
AS

select ROW_NUMBER() over(order by gcg.DocumentDate) as Row, 
gcg.CareGroupId, 
gcg.GroupersId, 
g.Code + ' - ' + g.[Description] as GrouperName, 
g.UserNumber, 
g.UserMin, 
g.UserMax, 
g.ProjectCME, 
g.Frequence, 
g.TotalContract, 
g.UserValue,
SUM(gcg.EjectEvent) as EjectEvent, 
SUM(gcg.RealCME) as RealCME, 
SUM(gcg.TotalEject) as TotalEject, 
IIF(g.TotalContract = 0, 0, SUM(gcg.TotalEject) / g.TotalContract * 100) as Variation,
gcg.DocumentDate
from [Contract].GroupersCareGroup gcg
inner join [Contract].Groupers g on g.Id = gcg.GroupersId
group by gcg.CareGroupId, gcg.GroupersId, g.Code, g.[Description], g.UserNumber, g.UserMin, g.UserMax, g.ProjectCME, g.Frequence, 
g.TotalContract, g.UserValue, gcg.DocumentDate
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista para el tablero de control (dashboard) del modelo de Pago Global Prospectivo (PGP) por grupo de atención y agrupador de contrato. Consolida, por fecha de documento, los valores proyectados del contrato (CME proyectado, frecuencia, valor por usuario, total contratado) junto con los valores reales ejecutados (eventos eyectados, CME real y total ejecutado), calculando además la variación porcentual entre lo ejecutado y lo contratado. Combina la estructura tarifaria definida en los agrupadores de contrato con los registros de ejecución por grupo de atención, permitiendo monitorear el cumplimiento y desviación del presupuesto pactado en contratos de capitación o PGP.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'VIEW', @level1name = N'ViewListDashboardPgp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'VIEW', @level1name = N'ViewListDashboardPgp';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida indicadores de ejecución de contratos PGP por agrupador y grupo de atención (eventos, CME real, total ejecutado y variación porcentual frente al contrato) para alimentar un dashboard.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListDashboardPgp';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir correspondencia entre GroupersCareGroup.GroupersId y Groupers.Id para que la fila sea visible.; Los campos Code y Description del agrupador deben ser concatenables (no nulos) para construir el nombre.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListDashboardPgp';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La variación porcentual nunca produce división por cero: si TotalContract = 0 se devuelve 0.; Cada fila representa la agregación por combinación de CareGroup, Grouper y DocumentDate.; Solo se incluyen agrupadores existentes en Contract.Groupers (INNER JOIN excluye huérfanos en GroupersCareGroup).; El nombre del agrupador siempre se compone como ''Code - Description''.; Las métricas EjectEvent, RealCME y TotalEject se entregan como sumas agregadas por el grano definido.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListDashboardPgp';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Agrupador de contrato (Grouper); Grupo de atención (CareGroup); CME proyectado y real; Eventos de eyección; Frecuencia de uso; Variación porcentual de ejecución vs. contrato', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListDashboardPgp';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Contract.GroupersCareGroup: Devuelve un conjunto agregado por CareGroupId, GroupersId y DocumentDate con sumas de EjectEvent, RealCME y TotalEject, calculando Variation como porcentaje de ejecución sobre el contrato.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListDashboardPgp';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TotalContract del agrupador = 0 → Variation se fija en 0 para evitar división por cero else Variation = SUM(TotalEject) / TotalContract * 100', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListDashboardPgp';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Contract.GroupersCareGroup; Contract.Groupers', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListDashboardPgp';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListDashboardPgp';
GO
