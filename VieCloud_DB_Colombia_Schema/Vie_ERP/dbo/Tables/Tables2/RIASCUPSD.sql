CREATE TABLE [dbo].[RIASCUPSD] (
    [ID]               INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDRIASCUPS]       INT     NOT NULL,
    [REGLA]            INT     NOT NULL,
    [EDADMINIMA]       INT     NULL,
    [EDADMAXIMA]       INT     NULL,
    [UNIDADRANGO]      INT     NULL,
    [FRECUENCIA]       INT     NULL,
    [UNIDADFRECUENCIA] INT     NULL,
    [REQUIEREORDENMED] BIT     NOT NULL,
    [NUMEROVECESORDEN] INT     NULL,
    [CANTIDADPERIODO]  INT     NULL,
    [SEXO]             TINYINT CONSTRAINT [DF_RIASCUPSD_SEXO] DEFAULT ((2)) NOT NULL,
    [ESTADO]           BIT     CONSTRAINT [DF_RIASCUPSD_ESTADO] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_RIASCUPSD] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT (1=Activo, 0=Inactivo). Habilita/deshabilita la regla de autorización RIAS-CUPS sin eliminar el registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSD', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Define el esatdo de la regla:  1 - Activo   0 - inactivo   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSD', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSD', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sexo aplicable de la regla: 0=Masculino, 1=Femenino, 2=Ambos. Filtro demográfico del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSD', @level2type = N'COLUMN', @level2name = N'SEXO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Determina a que sexo se aplica el rango:  0 - Masculino  1 - Femenino  2 - Ambos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSD', @level2type = N'COLUMN', @level2name = N'SEXO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSD', @level2type = N'COLUMN', @level2name = N'SEXO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número (X) para regla 3 (última realización): ''''1 cada X [año/semestre/trimestre/mes]'''' según UNIDADFRECUENCIA. Define intervalo mínimo entre servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSD', @level2type = N'COLUMN', @level2name = N'CANTIDADPERIODO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Solo aplica para la regla (3 -Frecuencia por ultima fecha de realización) y para poder especificar:  1 cada X Tiempo, Así:  Ejemplo   1 cada 3 Años  1 cada 3 Semestres  1 cada 3 Trimestres  1 cada 3 Meses', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSD', @level2type = N'COLUMN', @level2name = N'CANTIDADPERIODO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSD', @level2type = N'COLUMN', @level2name = N'CANTIDADPERIODO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad máxima de veces que un procedimiento/servicio puede repetirse dentro de una única orden médica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSD', @level2type = N'COLUMN', @level2name = N'NUMEROVECESORDEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identifica el numero de veces que se pueden solicitar en la orden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSD', @level2type = N'COLUMN', @level2name = N'NUMEROVECESORDEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSD', @level2type = N'COLUMN', @level2name = N'NUMEROVECESORDEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT (1=Sí/Requerido, 0=No) si la orden médica es obligatoria para solicitar el CUPS. PII+Governance', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSD', @level2type = N'COLUMN', @level2name = N'REQUIEREORDENMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda Requiere de orden medica Si = 1  y No = 0', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSD', @level2type = N'COLUMN', @level2name = N'REQUIEREORDENMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSD', @level2type = N'COLUMN', @level2name = N'REQUIEREORDENMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad temporal de frecuencia según REGLA (3-Última fecha: 1=Anual, 2=Semestral, 3=Trimestral, 4=Mensual; 4-Período vigente: 5=Año, 6=Semestre, 7=Trimestre, 8=Mes). Multiplica CANTIDADPERIODO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSD', @level2type = N'COLUMN', @level2name = N'UNIDADFRECUENCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Depende del campo REGLA     Unicamente cuando la Regla es  ( 3 -Frecuencia por ultima fecha de realización) se debe diligenciar este campo  para este tipo de item la frecuencia por defecto es 1 (uno)  1 - 1 Cada x año    2 - 1 Cada x semestre  3 - 1 Cada x trimestre  4 - 1 Cada x mes    la x la define el campo (CANTIDADPERIODO)    cuando la regla es  ( 4 -Frecuencia por periodo vigente)    5 - En el año vigente  6 - En el semestre vigente  7 - En el trimestre vigente  8 - En el mes vigente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSD', @level2type = N'COLUMN', @level2name = N'UNIDADFRECUENCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSD', @level2type = N'COLUMN', @level2name = N'UNIDADFRECUENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número entero (0-99) de repeticiones permitidas del procedimiento/examen/servicio dentro del período o rango definido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSD', @level2type = N'COLUMN', @level2name = N'FRECUENCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de repeticiones del evento (2 digitos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSD', @level2type = N'COLUMN', @level2name = N'FRECUENCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSD', @level2type = N'COLUMN', @level2name = N'FRECUENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad temporal del rango de edad: 1=Días, 2=Meses, 3=Años. Define la granularidad de EDADMINIMA/EDADMAXIMA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSD', @level2type = N'COLUMN', @level2name = N'UNIDADRANGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de Rango:  1 - Dias  2 - Meses  3 - Años', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSD', @level2type = N'COLUMN', @level2name = N'UNIDADRANGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSD', @level2type = N'COLUMN', @level2name = N'UNIDADRANGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad máxima del rango etario (en años) para aplicar la regla; límite superior demográfico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSD', @level2type = N'COLUMN', @level2name = N'EDADMAXIMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Rango Edad Maxima', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSD', @level2type = N'COLUMN', @level2name = N'EDADMAXIMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSD', @level2type = N'COLUMN', @level2name = N'EDADMAXIMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad mínima del rango etario (en años) para aplicar la regla; filtro demográfico del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSD', @level2type = N'COLUMN', @level2name = N'EDADMINIMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Rango Edad minima', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSD', @level2type = N'COLUMN', @level2name = N'EDADMINIMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSD', @level2type = N'COLUMN', @level2name = N'EDADMINIMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de regla de habilitación (1=Sin Regla, 2=Frecuencia por rango edad, 3=Frecuencia por última ejecución, 4=Frecuencia por período vigente). Controla qué campos de frecuencia se aplican', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSD', @level2type = N'COLUMN', @level2name = N'REGLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Sin Regla: No habilita los campos de frecuencia  2 - Frecuencia por rango de edad  3 - Frecuencia por ultima fecha de realización  4 - Frecuencia por periodo vigente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSD', @level2type = N'COLUMN', @level2name = N'REGLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSD', @level2type = N'COLUMN', @level2name = N'REGLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia a tabla RIASCUPS (FK), vincula la regla al procedimiento/servicio de salud CUPS-RIAS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSD', @level2type = N'COLUMN', @level2name = N'IDRIASCUPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la tabla RIASCUPS ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSD', @level2type = N'COLUMN', @level2name = N'IDRIASCUPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSD', @level2type = N'COLUMN', @level2name = N'IDRIASCUPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental (INT IDENTITY) de la regla RIAS-CUPS, clave primaria de la tabla RIASCUPSD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSD', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de reglas de elegibilidad y restricciones para servicios CUPS dentro de la Ruta Integral de Atención en Salud (RIAS). Define condiciones como rango de edad, sexo, frecuencia de uso y si requiere orden médica para que un servicio o procedimiento pueda ser autorizado o prestado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSD';
