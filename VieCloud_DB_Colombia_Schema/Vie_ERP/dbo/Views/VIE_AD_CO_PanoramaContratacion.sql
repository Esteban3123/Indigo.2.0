

CREATE VIEW [dbo].[VIE_AD_CO_PanoramaContratacion]
AS
SELECT        TOP (200) Id, ContractEntityId, HealthAdministratorId, Code, ContractName, ContractNumber, ContractValue, ExecuteValue, InitialDate, EndDate, Legalized, 
                         DateLegalization, Observations, ContractObject, PrintingMode, TerminationControl, NotificationValueType, PercentageNotification, NotificationValue, 
                         NotificationTimeType, NotificationDays, Status, CreationUser, CreationDate, ModificationUser, ModificationDate, InForceUser, InForceDate, SuspendedUser, 
                         SuspendedDate, FinishedUser, FinishedDate
FROM            Contract.Contract
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista panorámica de los contratos vigentes y históricos suscritos con entidades pagadoras o administradoras de salud (EPS, aseguradoras, etc.). Muestra los primeros 200 registros del maestro de contratos con información clave de cada contrato: código, nombre, número, valor pactado, valor ejecutado, fechas de inicio y fin, estado (legalizado, vigente, suspendido, terminado), observaciones y objeto contractual. Incluye además datos de auditoría como usuario y fecha de creación, modificación, legalización, vigencia, suspensión y finalización del contrato. Útil para reportería de gestión contractual, seguimiento de contratación con pagadores y control de vencimientos o alertas de ejecución presupuestal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'VIE_AD_CO_PanoramaContratacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'VIE_AD_CO_PanoramaContratacion';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los primeros 200 contratos suscritos con entidades pagadoras/administradoras de salud, incluyendo datos económicos, vigencias, estado y trazabilidad de su ciclo de vida.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_CO_PanoramaContratacion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El resultado nunca supera 200 registros por la cláusula TOP (200).; No aplica filtros por estado (Status) ni vigencia: incluye contratos en cualquier estado del ciclo de vida (legalizados, suspendidos, finalizados, etc.).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_CO_PanoramaContratacion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Contrato; Entidad contratante; Administradora de salud; Valor del contrato; Valor ejecutado; Vigencia del contrato; Legalización; Control de terminación; Notificación de vencimiento; Estado del contrato (vigente/suspendido/finalizado)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_CO_PanoramaContratacion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Contract.Contract: Devuelve hasta 200 filas (TOP 200) sin ORDER BY ni filtros, retornando los atributos de cabecera del contrato.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_CO_PanoramaContratacion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Contract.Contract', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_CO_PanoramaContratacion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_CO_PanoramaContratacion';
GO
