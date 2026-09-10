CREATE TABLE [dbo].[HCPARNUTFORD] (
    [ID]                  INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [HCPARNUTCID]         INT             NULL,
    [CODE]                CHAR (3)        NOT NULL,
    [CODNAME]             CHAR (3)        NULL,
    [NAME]                CHAR (50)       NULL,
    [STATUS]              BIT             NOT NULL,
    [OBSERVATION]         VARCHAR (MAX)   NULL,
    [FORMULATE]           VARCHAR (8000)  NULL,
    [MinValuesCentral]    NUMERIC (18, 2) NULL,
    [MaxValuesCentral]    NUMERIC (18, 2) NULL,
    [MinValuesPeripheral] NUMERIC (18, 2) NULL,
    [MaxValuesPeripheral] NUMERIC (18, 2) NULL,
    [CentralAlert]        VARCHAR (500)   NULL,
    [PeripheralAlert]     VARCHAR (500)   NULL,
    CONSTRAINT [PK__HCPARNUT__3214EC27116BAB9F] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_IDNUTCFORMULATE] FOREIGN KEY ([HCPARNUTCID]) REFERENCES [dbo].[HCPARNUTC] ([ID])
);


GO
ALTER TABLE [dbo].[HCPARNUTFORD] NOCHECK CONSTRAINT [FK_IDNUTCFORMULATE];




GO
ALTER TABLE [dbo].[HCPARNUTFORD] NOCHECK CONSTRAINT [FK_IDNUTCFORMULATE];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mensaje de alerta para acceso venoso periférico (VARCHAR 500). Notificación de valores fuera de rango en vía periférica de nutrición parenteral.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTFORD', @level2type = N'COLUMN', @level2name = N'PeripheralAlert';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mensaje de alerta de la vía periférica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTFORD', @level2type = N'COLUMN', @level2name = N'PeripheralAlert';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTFORD', @level2type = N'COLUMN', @level2name = N'PeripheralAlert';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mensaje de alerta para acceso venoso central (VARCHAR 500). Notificación de valores fuera de rango en vía central de nutrición parenteral.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTFORD', @level2type = N'COLUMN', @level2name = N'CentralAlert';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mensaje de alerta de la vía central', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTFORD', @level2type = N'COLUMN', @level2name = N'CentralAlert';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTFORD', @level2type = N'COLUMN', @level2name = N'CentralAlert';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico máximo permitido en vía periférica (NUMERIC 18,2). Límite superior de referencia para acceso venoso periférico en nutrición parenteral.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTFORD', @level2type = N'COLUMN', @level2name = N'MaxValuesPeripheral';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Peripheral máximo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTFORD', @level2type = N'COLUMN', @level2name = N'MaxValuesPeripheral';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTFORD', @level2type = N'COLUMN', @level2name = N'MaxValuesPeripheral';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico mínimo permitido en vía periférica (NUMERIC 18,2). Límite inferior de referencia para acceso venoso periférico en nutrición parenteral.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTFORD', @level2type = N'COLUMN', @level2name = N'MinValuesPeripheral';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Peripheral mínimo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTFORD', @level2type = N'COLUMN', @level2name = N'MinValuesPeripheral';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTFORD', @level2type = N'COLUMN', @level2name = N'MinValuesPeripheral';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico máximo permitido en vía central (NUMERIC 18,2). Límite superior de referencia para acceso venoso central en nutrición parenteral.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTFORD', @level2type = N'COLUMN', @level2name = N'MaxValuesCentral';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor central máximo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTFORD', @level2type = N'COLUMN', @level2name = N'MaxValuesCentral';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTFORD', @level2type = N'COLUMN', @level2name = N'MaxValuesCentral';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico mínimo permitido en vía central (NUMERIC 18,2). Límite inferior de referencia para acceso venoso central en nutrición parenteral.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTFORD', @level2type = N'COLUMN', @level2name = N'MinValuesCentral';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor central minimo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTFORD', @level2type = N'COLUMN', @level2name = N'MinValuesCentral';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTFORD', @level2type = N'COLUMN', @level2name = N'MinValuesCentral';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fórmula farmacéutica completa de nutrición parenteral (VARCHAR 8000). Composición, componentes, concentraciones e instrucciones de preparación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTFORD', @level2type = N'COLUMN', @level2name = N'FORMULATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'formula farmaceutica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTFORD', @level2type = N'COLUMN', @level2name = N'FORMULATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTFORD', @level2type = N'COLUMN', @level2name = N'FORMULATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones clínicas sobre la fórmula farmacéutica de nutrición parenteral (VARCHAR MAX). Notas de validación, contraindicaciones, indicaciones especiales.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTFORD', @level2type = N'COLUMN', @level2name = N'OBSERVATION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observacion de la formula farmaceutica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTFORD', @level2type = N'COLUMN', @level2name = N'OBSERVATION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTFORD', @level2type = N'COLUMN', @level2name = N'OBSERVATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado activo/inactivo de la fórmula farmacéutica (BIT). Indica si la fórmula de nutrición parenteral está vigente en uso clínico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTFORD', @level2type = N'COLUMN', @level2name = N'STATUS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la formula farmaceutica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTFORD', @level2type = N'COLUMN', @level2name = N'STATUS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTFORD', @level2type = N'COLUMN', @level2name = N'STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo de la fórmula farmacéutica de nutrición parenteral (CHAR 50). Identificación legible de la composición nutricional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTFORD', @level2type = N'COLUMN', @level2name = N'NAME';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la formula farmaceutica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTFORD', @level2type = N'COLUMN', @level2name = N'NAME';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTFORD', @level2type = N'COLUMN', @level2name = N'NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código alfanumérico del nombre de la fórmula (CHAR 3). Abreviatura de identificación rápida de la composición.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTFORD', @level2type = N'COLUMN', @level2name = N'CODNAME';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del nombre de la formula farmaceutica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTFORD', @level2type = N'COLUMN', @level2name = N'CODNAME';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTFORD', @level2type = N'COLUMN', @level2name = N'CODNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único de la fórmula farmacéutica de nutrición parenteral (CHAR 3). Identificador estándar para clasificación y referencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTFORD', @level2type = N'COLUMN', @level2name = N'CODE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la formula farmaceutica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTFORD', @level2type = N'COLUMN', @level2name = N'CODE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTFORD', @level2type = N'COLUMN', @level2name = N'CODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cabecera de nutrición parenteral (INT, FK→HCPARNUTC). Clave foránea que vincula el detalle de la fórmula con el registro maestro de nutrición parenteral.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTFORD', @level2type = N'COLUMN', @level2name = N'HCPARNUTCID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la cabecera de nutricion parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTFORD', @level2type = N'COLUMN', @level2name = N'HCPARNUTCID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTFORD', @level2type = N'COLUMN', @level2name = N'HCPARNUTCID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro (INT IDENTITY). Clave primaria que identifica unívocamente cada fórmula de nutrición parenteral.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTFORD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTFORD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTFORD', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parámetros y órdenes de fórmulas de nutrición parenteral: define cada componente o nutriente con sus valores de referencia (mínimos y máximos) según la vía de administración central o periférica, junto con alertas clínicas cuando los valores están fuera de rango.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTFORD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTFORD';
