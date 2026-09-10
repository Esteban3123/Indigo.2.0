CREATE TABLE [HumanTalent].[EndowmentsParameters] (
    [Id]               INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [GarmentsType]     TINYINT      NOT NULL,
    [MinSalary]        INT          NOT NULL,
    [Returnable]       TINYINT      NOT NULL,
    [Anticipated]      TINYINT      NOT NULL,
    [MinAntiquity]     INT          NOT NULL,
    [CreationUser]     VARCHAR (50) NOT NULL,
    [CreationDate]     DATETIME     NOT NULL,
    [ModificationUser] VARCHAR (50) NULL,
    [ModificationDate] DATETIME     NULL,
    [MinimumSalaryId]  INT          NULL,
    CONSTRAINT [PK_EndowmentsParameters] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EndowmentsParameters_MinimumSalary] FOREIGN KEY ([MinimumSalaryId]) REFERENCES [Payroll].[MinimumSalary] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del salario mínimo legal vigente, referencia externa a tabla Payroll.MinimumSalary (INT, FK)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'EndowmentsParameters', @level2type = N'COLUMN', @level2name = N'MinimumSalaryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación del salario mínimo', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'EndowmentsParameters', @level2type = N'COLUMN', @level2name = N'MinimumSalaryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'EndowmentsParameters', @level2type = N'COLUMN', @level2name = N'MinimumSalaryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación del registro, auditoría de cambios (DATETIME, nullable)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'EndowmentsParameters', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'EndowmentsParameters', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'EndowmentsParameters', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación del parámetro, trazabilidad de cambios (VARCHAR 50, nullable)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'EndowmentsParameters', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Modificación de Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'EndowmentsParameters', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'EndowmentsParameters', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de parámetro de dotación (DATETIME, requerido)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'EndowmentsParameters', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'EndowmentsParameters', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'EndowmentsParameters', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro de parámetro, trazabilidad de origen (VARCHAR 50, requerido)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'EndowmentsParameters', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Creación del Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'EndowmentsParameters', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'EndowmentsParameters', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antigüedad mínima requerida en meses para acceso a dotación de prendas, elegibilidad de empleado (INT, meses)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'EndowmentsParameters', @level2type = N'COLUMN', @level2name = N'MinAntiquity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para el minimo de antigüedad(meses)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'EndowmentsParameters', @level2type = N'COLUMN', @level2name = N'MinAntiquity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'EndowmentsParameters', @level2type = N'COLUMN', @level2name = N'MinAntiquity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: permite entrega anticipada de prenda (0=No permitido, 1=Permitido), anticipos de dotación (TINYINT)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'EndowmentsParameters', @level2type = N'COLUMN', @level2name = N'Anticipated';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo de selección por si la prenda se puede entregar con anticipación: 0=No, 1=Si', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'EndowmentsParameters', @level2type = N'COLUMN', @level2name = N'Anticipated';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'EndowmentsParameters', @level2type = N'COLUMN', @level2name = N'Anticipated';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: prenda es devolutiva o no (0=No devolutivo, 1=Devolutivo), clasificación de bien (TINYINT)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'EndowmentsParameters', @level2type = N'COLUMN', @level2name = N'Returnable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo de selección por si la prenda es de tipo devolutivo: 0=No, 1=Si', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'EndowmentsParameters', @level2type = N'COLUMN', @level2name = N'Returnable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'EndowmentsParameters', @level2type = N'COLUMN', @level2name = N'Returnable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Salario mínimo legal mensual vigente aplicable a cálculo de dotación, base de elegibilidad (INT)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'EndowmentsParameters', @level2type = N'COLUMN', @level2name = N'MinSalary';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Salario minimo legal mensual vigente', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'EndowmentsParameters', @level2type = N'COLUMN', @level2name = N'MinSalary';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'EndowmentsParameters', @level2type = N'COLUMN', @level2name = N'MinSalary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de prenda de dotación: 1=Vestido/ropa, 2=Calzado, clasificador de artículo (TINYINT)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'EndowmentsParameters', @level2type = N'COLUMN', @level2name = N'GarmentsType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de prenda: 1=Vestido, 2=Calzado', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'EndowmentsParameters', @level2type = N'COLUMN', @level2name = N'GarmentsType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'EndowmentsParameters', @level2type = N'COLUMN', @level2name = N'GarmentsType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del parámetro de dotación, clave primaria (INT IDENTITY, PK)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'EndowmentsParameters', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'EndowmentsParameters', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'EndowmentsParameters', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parámetros de dotación del personal: define las reglas para la entrega de prendas o elementos de dotación a los empleados según tipo de prenda, salario mínimo requerido, antigüedad mínima y condiciones de devolución o anticipo.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'EndowmentsParameters';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'EndowmentsParameters';
