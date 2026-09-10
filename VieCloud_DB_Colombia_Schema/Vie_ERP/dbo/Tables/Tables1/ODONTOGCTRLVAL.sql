CREATE TABLE [dbo].[ODONTOGCTRLVAL] (
    [ID]        INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDCONTROL] INT     NOT NULL,
    [CEO_C]     TINYINT NOT NULL,
    [CEO_E]     TINYINT NOT NULL,
    [CEO_O]     TINYINT NOT NULL,
    [CPO_C]     TINYINT NOT NULL,
    [CPO_P]     TINYINT NOT NULL,
    [CPO_O]     TINYINT NOT NULL,
    CONSTRAINT [PK_ODONTOGCTRLVAL__ID] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ODONTOGCTRLVAL_ODONTOGCTR] FOREIGN KEY ([IDCONTROL]) REFERENCES [dbo].[ODONTOGCTR] ([ID])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Índice CPO Obturado (TINYINT): dientes permanentes obturados/restaurados por caries; componente O del índice CPO en dentición adulta.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGCTRLVAL', @level2type = N'COLUMN', @level2name = N'CPO_O';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el CPO Obturado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGCTRLVAL', @level2type = N'COLUMN', @level2name = N'CPO_O';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGCTRLVAL', @level2type = N'COLUMN', @level2name = N'CPO_O';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Índice CPO Perdido (TINYINT): dientes permanentes perdidos/ausentes por caries; componente P del índice CPO en paciente adulto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGCTRLVAL', @level2type = N'COLUMN', @level2name = N'CPO_P';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el CPOPerdido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGCTRLVAL', @level2type = N'COLUMN', @level2name = N'CPO_P';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGCTRLVAL', @level2type = N'COLUMN', @level2name = N'CPO_P';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Índice CPO Cariado (TINYINT): dientes permanentes con caries activas; componente C del índice CPO en dentición adulta.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGCTRLVAL', @level2type = N'COLUMN', @level2name = N'CPO_C';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el CPO Cariado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGCTRLVAL', @level2type = N'COLUMN', @level2name = N'CPO_C';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGCTRLVAL', @level2type = N'COLUMN', @level2name = N'CPO_C';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Índice CEO Obturado (TINYINT): dientes deciduos/temporales obturados/tratados en dentición primaria; componente O del índice CEO.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGCTRLVAL', @level2type = N'COLUMN', @level2name = N'CEO_O';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el valor  CEO_O', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGCTRLVAL', @level2type = N'COLUMN', @level2name = N'CEO_O';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGCTRLVAL', @level2type = N'COLUMN', @level2name = N'CEO_O';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Índice CEO Extraído (TINYINT): dientes deciduos/temporales extraídos por caries en dentición primaria; componente E del índice CEO.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGCTRLVAL', @level2type = N'COLUMN', @level2name = N'CEO_E';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el valor  CEO_E', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGCTRLVAL', @level2type = N'COLUMN', @level2name = N'CEO_E';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGCTRLVAL', @level2type = N'COLUMN', @level2name = N'CEO_E';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Índice CEO Cariado (TINYINT): dientes deciduos/temporales con caries en dentición primaria; componente C del índice CEO.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGCTRLVAL', @level2type = N'COLUMN', @level2name = N'CEO_C';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el valor  CEO_C', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGCTRLVAL', @level2type = N'COLUMN', @level2name = N'CEO_C';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGCTRLVAL', @level2type = N'COLUMN', @level2name = N'CEO_C';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del control odontograma (FK → ODONTOGCTR.ID); enlaza registro de valores con control odontológico del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGCTRLVAL', @level2type = N'COLUMN', @level2name = N'IDCONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el id de control odontograma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGCTRLVAL', @level2type = N'COLUMN', @level2name = N'IDCONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGCTRLVAL', @level2type = N'COLUMN', @level2name = N'IDCONTROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del registro de valores odontológicos; clave primaria de la tabla ODONTOGCTRLVAL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGCTRLVAL', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGCTRLVAL', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGCTRLVAL', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra los valores de los índices odontológicos CEO (dientes de leche: cariados, extraídos y obturados) y CPO (dientes permanentes: cariados, perdidos y obturados) asociados a un control o evaluación dental del paciente. Se usa para el seguimiento del estado de salud bucal en historia clínica odontológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGCTRLVAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGCTRLVAL';
