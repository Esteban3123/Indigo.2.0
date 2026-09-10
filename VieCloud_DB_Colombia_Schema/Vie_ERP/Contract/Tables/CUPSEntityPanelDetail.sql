CREATE TABLE [Contract].[CUPSEntityPanelDetail] (
    [Id]                INT          IDENTITY (1, 1) NOT NULL,
    [CUPSEntityPanelId] INT          NOT NULL,
    [CUPSEntityId]      INT          NOT NULL,
    [CreationUser]      VARCHAR (20) NOT NULL,
    [CreationDate]      DATETIME     NOT NULL,
    CONSTRAINT [PK_CUPSEntityPanelDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CUPSEntityPanel_CUPSEntityPanelId] FOREIGN KEY ([CUPSEntityPanelId]) REFERENCES [Contract].[CUPSEntity] ([Id]),
    CONSTRAINT [FK_CUPSEntityPanelDetail_CUPSEntityId] FOREIGN KEY ([CUPSEntityId]) REFERENCES [Contract].[CUPSEntity] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del detalle del panel CUPS; timestamp de auditoría para registrar cuándo se asoció la entidad al panel.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntityPanelDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la fecha de creación del detalle', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntityPanelDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntityPanelDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario o cuenta que creó el registro del detalle; identificador de 20 caracteres para auditoría y trazabilidad de cambios en configuración de panel CUPS.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntityPanelDetail', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el usuario de creación del detalle', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntityPanelDetail', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntityPanelDetail', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la entidad CUPS asociada al panel; referencia a [Contract].[CUPSEntity] para servicios, procedimientos o códigos de tarifa.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntityPanelDetail', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la entidad CUPS', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntityPanelDetail', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntityPanelDetail', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del panel CUPS padre; referencia a [Contract].[CUPSEntity] que agrupa entidades CUPS dentro de un contrato o plan de beneficios.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntityPanelDetail', @level2type = N'COLUMN', @level2name = N'CUPSEntityPanelId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del panel de entidad CUPS', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntityPanelDetail', @level2type = N'COLUMN', @level2name = N'CUPSEntityPanelId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntityPanelDetail', @level2type = N'COLUMN', @level2name = N'CUPSEntityPanelId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave primaria (PK) INT del detalle de panel CUPS; identificador único del registro que vincula entidades CUPS a paneles de configuración.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntityPanelDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro del detalle del panel de entidad CUPS', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntityPanelDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntityPanelDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de los servicios CUPS (procedimientos) asignados a un panel o portafolio de una entidad contratante. Registra qué códigos de servicio están habilitados dentro de cada panel de contrato con una EPS, aseguradora o entidad pagadora.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntityPanelDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CUPSEntityPanelDetail';
