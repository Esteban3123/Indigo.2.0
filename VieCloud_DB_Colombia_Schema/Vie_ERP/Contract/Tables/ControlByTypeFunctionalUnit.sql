CREATE TABLE [Contract].[ControlByTypeFunctionalUnit] (
    [Id]                     INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CareGroupId]            INT           NULL,
    [UnitType]               TINYINT       NULL,
    [TypeFunctionalUnit]     VARCHAR (150) NULL,
    [MandatoryAuthorization] BIT           NULL,
    CONSTRAINT [PK_ControlByTypeFunctionalUnit] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ControlByTypeFunctionalUnit_CareGroup] FOREIGN KEY ([CareGroupId]) REFERENCES [Contract].[CareGroup] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) si la autorización previa es obligatoria para acceso a la unidad: 1=Sí (requiere aprobación/glosa), 0=No (acceso directo). Controla autorizaciones en RIPS.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ControlByTypeFunctionalUnit', @level2type = N'COLUMN', @level2name = N'MandatoryAuthorization';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autorización obligatoria:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ControlByTypeFunctionalUnit', @level2type = N'COLUMN', @level2name = N'MandatoryAuthorization';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ControlByTypeFunctionalUnit', @level2type = N'COLUMN', @level2name = N'MandatoryAuthorization';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o descripción (VARCHAR 150) de la unidad funcional, centro de atención, servicio o departamento. Ej: Urgencias Adultos, UTI Pediátrica, Quirófano Central.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ControlByTypeFunctionalUnit', @level2type = N'COLUMN', @level2name = N'TypeFunctionalUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de unidad funcional', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ControlByTypeFunctionalUnit', @level2type = N'COLUMN', @level2name = N'TypeFunctionalUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ControlByTypeFunctionalUnit', @level2type = N'COLUMN', @level2name = N'TypeFunctionalUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación numérica (TINYINT) del tipo de unidad funcional: 1=Urgencias, 2=Hospitalización, 3=Quirófanos, 4=Servicios Ambulatorios. Define la línea de atención.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ControlByTypeFunctionalUnit', @level2type = N'COLUMN', @level2name = N'UnitType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo de unidad 1 - Urgencias 2 - Hospitalizacion 3 - Quirofanos 4 - Servicios Ambulatorios', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ControlByTypeFunctionalUnit', @level2type = N'COLUMN', @level2name = N'UnitType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ControlByTypeFunctionalUnit', @level2type = N'COLUMN', @level2name = N'UnitType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del grupo de atención/prestador (FK a Contract.CareGroup). Vincula la regla al contrato y prestador responsable.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ControlByTypeFunctionalUnit', @level2type = N'COLUMN', @level2name = N'CareGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del grupo de atención', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ControlByTypeFunctionalUnit', @level2type = N'COLUMN', @level2name = N'CareGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ControlByTypeFunctionalUnit', @level2type = N'COLUMN', @level2name = N'CareGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico (IDENTITY) de la regla de control por tipo de unidad funcional.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ControlByTypeFunctionalUnit', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ControlByTypeFunctionalUnit', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ControlByTypeFunctionalUnit', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Configuración contractual que define, por grupo de atención, los tipos de unidad funcional y si requieren autorización obligatoria para ser facturados o tramitados bajo un contrato.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ControlByTypeFunctionalUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ControlByTypeFunctionalUnit';
