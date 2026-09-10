CREATE TABLE [Payroll].[FunctionalUnitUser] (
    [Id]               INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FunctionalUnitId] INT          NOT NULL,
    [UserId]           INT          NOT NULL,
    [UserCode]         VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_FunctionalUnitUser] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FunctionalUnitUser_FunctionalUnit] FOREIGN KEY ([FunctionalUnitId]) REFERENCES [Payroll].[FunctionalUnit] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del usuario (VARCHAR 50) asignado a la unidad funcional para acceso y permisos en centro de atención, consulta o área médica', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FunctionalUnitUser', @level2type = N'COLUMN', @level2name = N'UserCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del usuario al cual se le esta asignando el permiso de la unidad funcional', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FunctionalUnitUser', @level2type = N'COLUMN', @level2name = N'UserCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FunctionalUnitUser', @level2type = N'COLUMN', @level2name = N'UserCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico (INT) del usuario, profesional de salud o administrativo, vinculado a permisos en unidad funcional del sistema de nómina', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FunctionalUnitUser', @level2type = N'COLUMN', @level2name = N'UserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del usuario al cual se le esta asignando el permiso de la unidad funcional', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FunctionalUnitUser', @level2type = N'COLUMN', @level2name = N'UserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FunctionalUnitUser', @level2type = N'COLUMN', @level2name = N'UserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de la unidad funcional, área de atención o centro (FK → Payroll.FunctionalUnit), con la cual el usuario tiene autorización activa', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FunctionalUnitUser', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad funcional a la cual tiene permiso el usuario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FunctionalUnitUser', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FunctionalUnitUser', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autoincrementable (INT IDENTITY) y clave primaria de la relación usuario-unidad funcional en tabla de asignación de permisos', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FunctionalUnitUser', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FunctionalUnitUser', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FunctionalUnitUser', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona los usuarios del sistema con las unidades funcionales a las que pertenecen en el módulo de nómina, permitiendo controlar qué unidad funcional tiene asignado cada usuario.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FunctionalUnitUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FunctionalUnitUser';
