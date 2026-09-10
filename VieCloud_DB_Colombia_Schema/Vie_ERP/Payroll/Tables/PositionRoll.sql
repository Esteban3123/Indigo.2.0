CREATE TABLE [Payroll].[PositionRoll] (
    [Id]         INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdRol]      INT          NOT NULL,
    [CodeRol]    VARCHAR (20) NOT NULL,
    [IdPosition] INT          NOT NULL,
    CONSTRAINT [PK_PositionRoll] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PositionRoll_Position] FOREIGN KEY ([IdPosition]) REFERENCES [Payroll].[Position] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del Cargo o Posición laboral (FK a Payroll.Position). Referencia única que vincula el rol a su puesto de trabajo asociado en la estructura organizacional.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PositionRoll', @level2type = N'COLUMN', @level2name = N'IdPosition';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Cargo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PositionRoll', @level2type = N'COLUMN', @level2name = N'IdPosition';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PositionRoll', @level2type = N'COLUMN', @level2name = N'IdPosition';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código alfanumérico del Rol o Perfil laboral (VARCHAR 20). Código único que identifica el tipo de rol, función o perfil asignado al cargo para control de permisos y responsabilidades.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PositionRoll', @level2type = N'COLUMN', @level2name = N'CodeRol';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del Rol', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PositionRoll', @level2type = N'COLUMN', @level2name = N'CodeRol';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PositionRoll', @level2type = N'COLUMN', @level2name = N'CodeRol';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico del Rol o Perfil (INT). Clave única que referencia el rol o función laboral asociado a la posición en la estructura de nómina y seguridad.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PositionRoll', @level2type = N'COLUMN', @level2name = N'IdRol';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Rol', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PositionRoll', @level2type = N'COLUMN', @level2name = N'IdRol';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PositionRoll', @level2type = N'COLUMN', @level2name = N'IdRol';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico de la relación Cargo-Rol (INT IDENTITY). Clave primaria que identifica cada asignación de rol a cargo en la tabla de asociaciones de nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PositionRoll', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PositionRoll', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PositionRoll', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona los roles de nómina con los cargos o posiciones de la organización. Permite saber qué rol tiene asignado cada cargo dentro del esquema de liquidación de nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PositionRoll';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PositionRoll';
