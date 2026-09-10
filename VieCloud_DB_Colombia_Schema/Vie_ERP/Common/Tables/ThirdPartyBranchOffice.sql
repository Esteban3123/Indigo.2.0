CREATE TABLE [Common].[ThirdPartyBranchOffice] (
    [Id]             INT        IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ThirdPartyId]   INT        NOT NULL,
    [BranchOfficeId] INT        NOT NULL,
    [TimeStamp]      ROWVERSION NOT NULL,
    CONSTRAINT [PK_ThirdPartyBranchOffice] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ThirdPartyBranchOffice_BranchOffice] FOREIGN KEY ([BranchOfficeId]) REFERENCES [Payroll].[BranchOffice] ([Id]),
    CONSTRAINT [FK_ThirdPartyBranchOffice_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal (TIMESTAMP SQL Server) que registra el instante de creación, modificación o cambio de estado del vínculo entre tercero y sucursal. Auditoría automática de eventos.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyBranchOffice', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyBranchOffice', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyBranchOffice', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la sucursal, filial, centro de atención o punto de servicio asociado al tercero. Referencia a [Payroll].[BranchOffice].', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyBranchOffice', @level2type = N'COLUMN', @level2name = N'BranchOfficeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la sucursal', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyBranchOffice', @level2type = N'COLUMN', @level2name = N'BranchOfficeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyBranchOffice', @level2type = N'COLUMN', @level2name = N'BranchOfficeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del tercero: proveedor, contratista, entidad, persona jurídica o natural. Referencia a [Common].[ThirdParty].', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyBranchOffice', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tercero', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyBranchOffice', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyBranchOffice', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del registro de asociación entre tercero y sucursal. Clave primaria clustered.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyBranchOffice', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyBranchOffice', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyBranchOffice', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona terceros (personas naturales, jurídicas, proveedores, aseguradoras u otras entidades externas) con las sucursales u oficinas a las que pertenecen o están asociadas. Permite saber en qué sede opera cada tercero registrado en el sistema.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyBranchOffice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyBranchOffice';
