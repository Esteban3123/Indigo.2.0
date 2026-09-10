CREATE TABLE [AccountManagement].[AssignmentManagement] (
    [Id]         INT        IDENTITY (1, 1) NOT NULL,
    [LastUserId] INT        NOT NULL,
    [RowVersion] ROWVERSION NOT NULL,
    [EntryType]  TINYINT    NOT NULL,
    CONSTRAINT [FK_AssignmentManagement_UsersAssignment] FOREIGN KEY ([LastUserId]) REFERENCES [AccountManagement].[UsersAssignment] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de ingreso/atención del paciente: 1=Ambulatorio (consulta externa, sin hospitalización), 2=Hospitalario (internación, urgencia). Clasificación para facturación y gestión de RIPS.', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AssignmentManagement', @level2type = N'COLUMN', @level2name = N'EntryType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de ingreso, 1=Ambulatorio, 2=Hospitalario', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AssignmentManagement', @level2type = N'COLUMN', @level2name = N'EntryType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AssignmentManagement', @level2type = N'COLUMN', @level2name = N'EntryType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Índice de concurrencia/timestamp para control de versiones optimista en actualizaciones simultáneas de asignaciones.', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AssignmentManagement', @level2type = N'COLUMN', @level2name = N'RowVersion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indice de concurrencia', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AssignmentManagement', @level2type = N'COLUMN', @level2name = N'RowVersion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AssignmentManagement', @level2type = N'COLUMN', @level2name = N'RowVersion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del último usuario facturador al que se asignó un ingreso/atención para facturación. Referencia FK a UsersAssignment.', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AssignmentManagement', @level2type = N'COLUMN', @level2name = N'LastUserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del último usuario (facturador) al que se le asignó un ingreso a facturar', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AssignmentManagement', @level2type = N'COLUMN', @level2name = N'LastUserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AssignmentManagement', @level2type = N'COLUMN', @level2name = N'LastUserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico (INT IDENTITY) único de registro de gestión de asignaciones de ingresos.', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AssignmentManagement', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AssignmentManagement', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AssignmentManagement', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de gestión de asignaciones en el módulo de administración de cuentas. Controla qué usuario realizó la última modificación sobre cada asignación y el tipo de entrada registrada.', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AssignmentManagement';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'AssignmentManagement';
