CREATE TABLE [Payroll].[AuthorizationConceptEmployee] (
    [Id]                     INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AuthorizationConceptId] INT NOT NULL,
    [EmployeeId]             INT NOT NULL,
    [State]                  BIT NOT NULL,
    CONSTRAINT [PK_AuthorizationConceptEmployee] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AuthorizationConceptEmployee_AuthorizationConcept] FOREIGN KEY ([AuthorizationConceptId]) REFERENCES [Payroll].[AuthorizationConcept] ([Id]),
    CONSTRAINT [FK_AuthorizationConceptEmployee_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Payroll].[Employee] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de autorización: 1 = Activo/Autorizado, 0 = Inactivo/Revocado. Indica si el empleado tiene permiso para usar este concepto de nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AuthorizationConceptEmployee', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado: 1 - Activo, 0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AuthorizationConceptEmployee', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AuthorizationConceptEmployee', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK - Identificador del empleado (relación con tabla Employee). Referencia única al profesional de la salud o personal vinculado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AuthorizationConceptEmployee', @level2type = N'COLUMN', @level2name = N'EmployeeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'FK Id Empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AuthorizationConceptEmployee', @level2type = N'COLUMN', @level2name = N'EmployeeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AuthorizationConceptEmployee', @level2type = N'COLUMN', @level2name = N'EmployeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK - Identificador de concepto de autorización de nómina (relación con tabla AuthorizationConcept). Define qué conceptos salariales o deducciones están permitidos.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AuthorizationConceptEmployee', @level2type = N'COLUMN', @level2name = N'AuthorizationConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'FK Id Autorizacion de Conceptos', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AuthorizationConceptEmployee', @level2type = N'COLUMN', @level2name = N'AuthorizationConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AuthorizationConceptEmployee', @level2type = N'COLUMN', @level2name = N'AuthorizationConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK) del registro de asignación de concepto a empleado. Clave principal para autorización de conceptos por empleado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AuthorizationConceptEmployee', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Conceptos x Empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AuthorizationConceptEmployee', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AuthorizationConceptEmployee', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra la autorización de conceptos de nómina asignados a empleados específicos, indicando qué conceptos (como deducciones, bonificaciones o devengados) están habilitados o deshabilitados para cada trabajador.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AuthorizationConceptEmployee';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AuthorizationConceptEmployee';
