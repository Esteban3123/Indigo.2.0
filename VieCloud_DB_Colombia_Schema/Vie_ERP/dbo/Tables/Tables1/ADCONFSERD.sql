CREATE TABLE [dbo].[ADCONFSERD] (
    [ID]          INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDADCONFSER] INT NOT NULL,
    [IDCaregroup] INT NOT NULL,
    CONSTRAINT [PK_ADCONFSERD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ADCONFSERD_ADCONFSER] FOREIGN KEY ([IDADCONFSER]) REFERENCES [dbo].[ADCONFSER] ([CODCONCEC])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del grupo de atención (Caregroup). Relación con la tabla de configuración de grupos de atención de Vie. Referencia a unidades funcionales, centros de atención o líneas de servicio. INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONFSERD', @level2type = N'COLUMN', @level2name = N'IDCaregroup';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion de la tabla de VIE de Grupos de Atencion: Caregroup', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONFSERD', @level2type = N'COLUMN', @level2name = N'IDCaregroup';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONFSERD', @level2type = N'COLUMN', @level2name = N'IDCaregroup';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la configuración de servicios susceptibles. Relación con tabla cabecera ADCONFSER (CODCONCEC). Referencia a servicios configurables para facturación, RIPS, procedimientos y atenciones. INT. FK.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONFSERD', @level2type = N'COLUMN', @level2name = N'IDADCONFSER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con la tabla cabecera de los servicios suceptibles: ADCONFSER', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONFSERD', @level2type = N'COLUMN', @level2name = N'IDADCONFSER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONFSERD', @level2type = N'COLUMN', @level2name = N'IDADCONFSER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único consecutivo (identity) del registro de detalle de configuración de servicios por grupo de atención. INT PRIMARY KEY.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONFSERD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONFSERD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONFSERD', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de configuración de servicios agrupados por grupo de atención (caregroup). Relaciona cada configuración de servicio con el grupo asistencial o línea de cuidado al que pertenece.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONFSERD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONFSERD';
