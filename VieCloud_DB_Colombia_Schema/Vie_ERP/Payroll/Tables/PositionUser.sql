CREATE TABLE [Payroll].[PositionUser] (
    [Id]         INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdUser]     INT          NOT NULL,
    [CodeUser]   VARCHAR (20) NOT NULL,
    [IdPosition] INT          NOT NULL,
    CONSTRAINT [PK_PositionUser] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PositionUser_Position] FOREIGN KEY ([IdPosition]) REFERENCES [Payroll].[Position] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del Cargo o Posición laboral. Clave foránea hacia tabla Position. Referencia única del puesto, función o rol asignado al usuario en nómina', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PositionUser', @level2type = N'COLUMN', @level2name = N'IdPosition';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Cargo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PositionUser', @level2type = N'COLUMN', @level2name = N'IdPosition';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PositionUser', @level2type = N'COLUMN', @level2name = N'IdPosition';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del Usuario. Identificador alfanumérico (VARCHAR 20) que distingue al profesional de salud, empleado o trabajador en el sistema', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PositionUser', @level2type = N'COLUMN', @level2name = N'CodeUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del Usuario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PositionUser', @level2type = N'COLUMN', @level2name = N'CodeUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PositionUser', @level2type = N'COLUMN', @level2name = N'CodeUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del Usuario. Clave foránea que vincula el registro del usuario/trabajador/profesional de la salud con su perfil en el sistema', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PositionUser', @level2type = N'COLUMN', @level2name = N'IdUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Usuario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PositionUser', @level2type = N'COLUMN', @level2name = N'IdUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PositionUser', @level2type = N'COLUMN', @level2name = N'IdUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico (IDENTITY) de la relación PositionUser. Clave primaria que vincula un usuario a un cargo específico en nómina', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PositionUser', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PositionUser', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PositionUser', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona los usuarios del sistema con los cargos o posiciones que desempeñan dentro de la nómina, permitiendo saber qué cargo tiene asignado cada empleado o usuario.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PositionUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PositionUser';
