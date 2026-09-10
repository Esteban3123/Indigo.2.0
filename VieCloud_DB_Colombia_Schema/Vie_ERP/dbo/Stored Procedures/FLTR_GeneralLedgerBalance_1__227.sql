CREATE PROCEDURE [dbo].[FLTR_GeneralLedgerBalance_1__227] AS if exists (select * from [GeneralLedger].[GeneralLedgerBalance] where [Year] >2017) return 1 else return 0
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de verificación que comprueba si existen registros en el libro mayor general con año superior a 2017. Evalúa la tabla de saldos del libro mayor (GeneralLedgerBalance) y retorna 1 si encuentra movimientos contables posteriores a ese año, o 0 en caso contrario. Se utiliza como filtro de validación o control de prerrequisito antes de ejecutar procesos contables o migraciones, para confirmar que hay datos de saldos contables vigentes disponibles en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'FLTR_GeneralLedgerBalance_1__227';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'FLTR_GeneralLedgerBalance_1__227';
-- GO
