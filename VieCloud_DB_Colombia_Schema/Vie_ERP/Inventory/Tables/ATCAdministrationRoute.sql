CREATE TABLE [Inventory].[ATCAdministrationRoute] (
    [Id]                    INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ATCId]                 INT NOT NULL,
    [AdministrationRouteId] INT NOT NULL,
    CONSTRAINT [PK_ATCAdministrationRoute] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ATCAdministrationRoute_AdministrationRoute] FOREIGN KEY ([AdministrationRouteId]) REFERENCES [Inventory].[AdministrationRoute] ([Id]),
    CONSTRAINT [FK_ATCAdministrationRoute_ATC] FOREIGN KEY ([ATCId]) REFERENCES [Inventory].[ATC] ([Id])
);




GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_ATCAdministrationRoute_ATCId]
    ON [Inventory].[ATCAdministrationRoute]([ATCId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la vía de administración del medicamento (oral, intravenosa, intramuscular, tópica, etc.). FK a Inventory.AdministrationRoute.Id', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATCAdministrationRoute', @level2type = N'COLUMN', @level2name = N'AdministrationRouteId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de vía de administración', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATCAdministrationRoute', @level2type = N'COLUMN', @level2name = N'AdministrationRouteId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATCAdministrationRoute', @level2type = N'COLUMN', @level2name = N'AdministrationRouteId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del código ATC (medicamento/fármaco). FK a Inventory.ATC.Id. Referencia al producto farmacéutico catalogado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATCAdministrationRoute', @level2type = N'COLUMN', @level2name = N'ATCId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de medicamento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATCAdministrationRoute', @level2type = N'COLUMN', @level2name = N'ATCId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATCAdministrationRoute', @level2type = N'COLUMN', @level2name = N'ATCId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave primaria de la tabla de asociación ATC-vía de administración. INT IDENTITY. Identificador único del registro', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATCAdministrationRoute', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATCAdministrationRoute', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATCAdministrationRoute', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona los códigos ATC (clasificación anatómica terapéutica de medicamentos) con las vías de administración permitidas para cada uno. Permite saber por qué rutas (oral, intravenosa, tópica, etc.) se puede administrar un medicamento según su clasificación ATC.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATCAdministrationRoute';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATCAdministrationRoute';
