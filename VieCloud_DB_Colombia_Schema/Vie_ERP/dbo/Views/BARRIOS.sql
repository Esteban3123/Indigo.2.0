

CREATE VIEW [dbo].[BARRIOS]
AS
SELECT     AUUBICACI, DEPMUNCOD, UBICODIGO, UBINOMBRE, TIPOUBICA, INDAUDFOR
FROM         dbo.INUBICACI
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Barrios y sectores geográficos registrados en el sistema, filtrados desde la tabla maestra de ubicaciones (INUBICACI). Representa las unidades de ubicación de tipo barrio utilizadas para clasificar la residencia de pacientes y otras entidades geográficas. Sirve como catálogo de referencia para formularios de dirección, admisión y datos demográficos del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'BARRIOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'BARRIOS';
GO
