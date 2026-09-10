CREATE TABLE [dbo].[HCRADDOSISTIPOSBRAQUI] (
    [ID]                 INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCRADDOSISBRAQUI] INT NOT NULL,
    [IDHCRADMAESTIP]     INT NOT NULL,
    [TIPO]               INT NOT NULL,
    CONSTRAINT [PK_HCRADDOSISTIPOSBRAQUI] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCRADDOSISTIPOSBRAQUI_HCRADDOSISBRAQUI] FOREIGN KEY ([IDHCRADDOSISBRAQUI]) REFERENCES [dbo].[HCRADDOSISBRAQUI] ([ID]),
    CONSTRAINT [FK_HCRADDOSISTIPOSBRAQUI_HCRADMAESTIP] FOREIGN KEY ([IDHCRADMAESTIP]) REFERENCES [dbo].[HCRADMAESTIP] ([ID])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación del tipo de dosis en braquiterapia: 1=Factores asociados al tratamiento, 2=Sistema. Indicador numérico (INT) que categoriza la naturaleza o origen del registro de dosificación en procedimientos de radioterapia intracavitaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISTIPOSBRAQUI', @level2type = N'COLUMN', @level2name = N'TIPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 -  Factores Asociados al Tratamiento   2 - Sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISTIPOSBRAQUI', @level2type = N'COLUMN', @level2name = N'TIPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISTIPOSBRAQUI', @level2type = N'COLUMN', @level2name = N'TIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK INT) que vincula con tabla maestro HCRADMAESTIP. Referencia a tipos maestros de datos de radioterapia/braquiterapia para clasificación estándar de tratamientos y factores clínicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISTIPOSBRAQUI', @level2type = N'COLUMN', @level2name = N'IDHCRADMAESTIP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con la tabla maestro de: HCRADMAESTIP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISTIPOSBRAQUI', @level2type = N'COLUMN', @level2name = N'IDHCRADMAESTIP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISTIPOSBRAQUI', @level2type = N'COLUMN', @level2name = N'IDHCRADMAESTIP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK INT) que referencia la tabla principal HCRADDOSISBRAQUI. Agrupa tipos de dosis dentro de un registro de dosificación específico de braquiterapia (radioterapia intracavitaria).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISTIPOSBRAQUI', @level2type = N'COLUMN', @level2name = N'IDHCRADDOSISBRAQUI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con la tabla HCRADDOSISBRAQUI', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISTIPOSBRAQUI', @level2type = N'COLUMN', @level2name = N'IDHCRADDOSISBRAQUI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISTIPOSBRAQUI', @level2type = N'COLUMN', @level2name = N'IDHCRADDOSISBRAQUI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY). Clave primaria consecutiva de la tabla de detalle de tipos de dosis en braquiterapia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISTIPOSBRAQUI', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISTIPOSBRAQUI', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISTIPOSBRAQUI', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra los tipos de dosis asociados a cada sesión de braquiterapia, relacionando cada dosis con su clasificación o tipo específico dentro del tratamiento de radioterapia interna.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISTIPOSBRAQUI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISTIPOSBRAQUI';
