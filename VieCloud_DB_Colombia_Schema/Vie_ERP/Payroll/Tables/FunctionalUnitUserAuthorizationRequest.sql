CREATE TABLE [Payroll].[FunctionalUnitUserAuthorizationRequest] (
    [Id]               INT          IDENTITY (1, 1) NOT NULL,
    [FunctionalUnitId] INT          NOT NULL,
    [UserId]           INT          NOT NULL,
    [UserCode]         VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_FunctionalUnitUserAuthorizationRequest] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FunctionalUnitUserAuthorizationRequest_FunctionalUnit] FOREIGN KEY ([FunctionalUnitId]) REFERENCES [Payroll].[FunctionalUnit] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del usuario (VARCHAR 50) al que se asigna permiso de acceso a la unidad funcional; identificador legible del usuario en el sistema de nómina y gestión de personal.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FunctionalUnitUserAuthorizationRequest', @level2type = N'COLUMN', @level2name = N'UserCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del usuario al cual se le esta asignando el permiso de la unidad funcional', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FunctionalUnitUserAuthorizationRequest', @level2type = N'COLUMN', @level2name = N'UserCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FunctionalUnitUserAuthorizationRequest', @level2type = N'COLUMN', @level2name = N'UserCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico (INT) del usuario al que se autoriza acceso a la unidad funcional; clave foránea que vincula con el registro de usuario en el sistema.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FunctionalUnitUserAuthorizationRequest', @level2type = N'COLUMN', @level2name = N'UserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del usuario al cual se le esta asignando el permiso de la unidad funcional', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FunctionalUnitUserAuthorizationRequest', @level2type = N'COLUMN', @level2name = N'UserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FunctionalUnitUserAuthorizationRequest', @level2type = N'COLUMN', @level2name = N'UserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico (INT) de la unidad funcional (departamento, centro de atención, área médica) a la que se le otorga permiso de acceso; referencia a la tabla Payroll.FunctionalUnit.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FunctionalUnitUserAuthorizationRequest', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad funcional a la cual tiene permiso el usuario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FunctionalUnitUserAuthorizationRequest', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FunctionalUnitUserAuthorizationRequest', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave primaria (INT IDENTITY) que identifica unívocamente cada autorización de acceso de un usuario a una unidad funcional; registro de asignación de permisos en la estructura organizacional.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FunctionalUnitUserAuthorizationRequest', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tabla donde se especifica que usuarios tienen permisos a ciertas unidades funcionales', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FunctionalUnitUserAuthorizationRequest', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FunctionalUnitUserAuthorizationRequest', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Solicitudes de autorización de usuarios para acceder a unidades funcionales en el módulo de nómina. Registra qué usuario y con qué código tiene permiso o solicitud pendiente sobre una unidad funcional específica.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FunctionalUnitUserAuthorizationRequest';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FunctionalUnitUserAuthorizationRequest';
