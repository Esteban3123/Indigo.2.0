CREATE TABLE [dbo].[SOLCOTANU] (
    [AUTOANULA] INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FECHANULA] DATETIME      NOT NULL,
    [USUAANULA] CHAR (20)     NOT NULL,
    [COTIANULA] INT           NOT NULL,
    [OBSEANULA] VARCHAR (500) NULL,
    CONSTRAINT [PK_SOLCOTANU] PRIMARY KEY CLUSTERED ([AUTOANULA] ASC),
    CONSTRAINT [FK_SOLCOTANU_SOLCOTI] FOREIGN KEY ([COTIANULA]) REFERENCES [dbo].[SOLCOTI] ([AUTO])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones y notas justificativas de la anulación de cotización. Texto libre (VARCHAR 500) para documentar motivo, autorización o comentarios asociados a la cancelación de la cotización.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTANU', @level2type = N'COLUMN', @level2name = N'OBSEANULA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene las observaciones de la anulacion de la cotizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTANU', @level2type = N'COLUMN', @level2name = N'OBSEANULA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTANU', @level2type = N'COLUMN', @level2name = N'OBSEANULA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (autonumérico) de la cotización anulada. Referencia a SOLCOTI.AUTO; vincula el registro de anulación con la cotización original que se cancela.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTANU', @level2type = N'COLUMN', @level2name = N'COTIANULA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el autonumerico de la cotizacion que anula', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTANU', @level2type = N'COLUMN', @level2name = N'COTIANULA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTANU', @level2type = N'COLUMN', @level2name = N'COTIANULA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario o profesional que realiza la anulación de la cotización. Identificación del operador responsable (CHAR 20) en el sistema de auditoría.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTANU', @level2type = N'COLUMN', @level2name = N'USUAANULA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el usuario que anula', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTANU', @level2type = N'COLUMN', @level2name = N'USUAANULA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTANU', @level2type = N'COLUMN', @level2name = N'USUAANULA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora exacta de la anulación de la cotización. Timestamp (DATETIME) que registra cuándo se ejecutó la cancelación del documento cotizado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTANU', @level2type = N'COLUMN', @level2name = N'FECHANULA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene la fecha de anulacion de la cotizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTANU', @level2type = N'COLUMN', @level2name = N'FECHANULA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTANU', @level2type = N'COLUMN', @level2name = N'FECHANULA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (autonumérico identity) del registro de anulación en SOLCOTANU. Clave primaria que identifica cada acción de anulación de cotización.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTANU', @level2type = N'COLUMN', @level2name = N'AUTOANULA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTANU', @level2type = N'COLUMN', @level2name = N'AUTOANULA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTANU', @level2type = N'COLUMN', @level2name = N'AUTOANULA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de anulaciones de cotizaciones. Guarda el historial de cada cotización anulada, indicando quién la anuló, cuándo y por qué motivo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTANU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTANU';
