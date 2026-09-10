CREATE TABLE [Contract].[GroupersActivities] (
    [Id]            INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [GrouperId]     INT           NOT NULL,
    [AGACTIMEDCode] VARCHAR (5)   NOT NULL,
    [AGACTIMEDName] VARCHAR (100) NOT NULL,
    CONSTRAINT [PK_GroupersActivities] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GroupersActivities_Groupers] FOREIGN KEY ([GrouperId]) REFERENCES [Contract].[Groupers] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la actividad AGACIMED; descripción de la prestación, procedimiento o servicio de salud asociado al agrupador contractual', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersActivities', @level2type = N'COLUMN', @level2name = N'AGACTIMEDName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersActivities', @level2type = N'COLUMN', @level2name = N'AGACTIMEDName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersActivities', @level2type = N'COLUMN', @level2name = N'AGACTIMEDName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código AGACIMED; identificador de 5 caracteres de la actividad, prestación o procedimiento según nomenclatura de facturación y RIPS', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersActivities', @level2type = N'COLUMN', @level2name = N'AGACTIMEDCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersActivities', @level2type = N'COLUMN', @level2name = N'AGACTIMEDCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersActivities', @level2type = N'COLUMN', @level2name = N'AGACTIMEDCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del agrupador; clave foránea que referencia al agrupador contractual padre en Contract.Groupers', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersActivities', @level2type = N'COLUMN', @level2name = N'GrouperId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Agrupador', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersActivities', @level2type = N'COLUMN', @level2name = N'GrouperId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersActivities', @level2type = N'COLUMN', @level2name = N'GrouperId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Llave principal; identificador único secuencial de la actividad AGACIMED en el contrato', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersActivities', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Llave principal', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersActivities', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersActivities', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Agrupa las actividades médicas asociadas a cada agrupador de contrato. Relaciona códigos y nombres de actividades (procedimientos, servicios) con el agrupador tarifario o de facturación al que pertenecen.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersActivities';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersActivities';
