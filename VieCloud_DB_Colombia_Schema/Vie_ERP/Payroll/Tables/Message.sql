CREATE TABLE [Payroll].[Message] (
    [Id]            INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [LiquitadionId] INT           NOT NULL,
    [PayrollDate]   DATE          NOT NULL,
    [Error]         BIT           NOT NULL,
    [Description]   VARCHAR (500) NOT NULL,
    CONSTRAINT [PK_Message] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Message_Liquidation] FOREIGN KEY ([LiquitadionId]) REFERENCES [Payroll].[Liquidation] ([Id]) ON DELETE CASCADE
);


GO
ALTER TABLE [Payroll].[Message] NOCHECK CONSTRAINT [FK_Message_Liquidation];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del mensaje de nómina; texto detallado (VARCHAR 500) que explica el contenido, resultado o detalle del procesamiento de la liquidación de nómina, ya sea error, advertencia o información informativa.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Message', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción del Mensaje', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Message', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Message', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de tipo de mensaje (BIT): 1=Error crítico en liquidación, 0=Advertencia o información. Determina si el mensaje representa una falla en el procesamiento de nómina o solo un aviso informativo.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Message', @level2type = N'COLUMN', @level2name = N'Error';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mensaje de Error: 1 - SI 0 - No (Advertencia o Información)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Message', @level2type = N'COLUMN', @level2name = N'Error';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Message', @level2type = N'COLUMN', @level2name = N'Error';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de liquidación de nómina (DATE); fecha en que se procesa o liquida la nómina asociada al mensaje. Referencia temporal del ciclo de pago.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Message', @level2type = N'COLUMN', @level2name = N'PayrollDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Liquidación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Message', @level2type = N'COLUMN', @level2name = N'PayrollDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Message', @level2type = N'COLUMN', @level2name = N'PayrollDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la liquidación de nómina (INT, FK); clave foránea que vincula el mensaje a su registro de liquidación en [Payroll].[Liquidation]. Relación obligatoria con cascada de eliminación.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Message', @level2type = N'COLUMN', @level2name = N'LiquitadionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Liquidación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Message', @level2type = N'COLUMN', @level2name = N'LiquitadionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Message', @level2type = N'COLUMN', @level2name = N'LiquitadionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (INT IDENTITY); clave primaria de la tabla Message. Generado automáticamente con incremento secuencial para cada registro de mensaje de nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Message', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Message', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Message', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mensajes y notificaciones generados durante el proceso de liquidación de nómina, indicando si ocurrió un error y el detalle del resultado para cada fecha de liquidación.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Message';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Message';
