CREATE TABLE [Common].[Sequense] (
    [Id]      INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Name]    VARCHAR (20) NOT NULL,
    [Pattern] VARCHAR (20) NOT NULL,
    CONSTRAINT [PK_Sequense__Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_Sequense__Name]
    ON [Common].[Sequense]([Name] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Patrón de formato que define la secuencia de numeración (ej: prefijos, sufijos, contadores). VARCHAR(20), usado en generación automática de códigos.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Sequense', @level2type = N'COLUMN', @level2name = N'Pattern';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Patrón que define la secuencia', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Sequense', @level2type = N'COLUMN', @level2name = N'Pattern';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Sequense', @level2type = N'COLUMN', @level2name = N'Pattern';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre identificador de la secuencia de numeración (ej: ''''FACT'''', ''''INGR'''', ''''RIPS''''). VARCHAR(20), clave para referenciar la secuencia en otras tablas.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Sequense', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la secuencia', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Sequense', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Sequense', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de secuencia. INT IDENTITY, clave primaria clustered.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Sequense', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Sequense', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Sequense', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de secuencias o consecutivos del sistema, donde se define el nombre y el patrón de formato para la generación automática de numeraciones (por ejemplo, número de ingreso, número de factura, número de orden médica).', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Sequense';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Sequense';
