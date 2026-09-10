CREATE TABLE [Admissions].[Cie11Mapping] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Code]        VARCHAR (7)   NOT NULL,
    [Cie11Code]   VARCHAR (7)   NOT NULL,
    [Description] VARCHAR (400) NOT NULL,
    [Status]      BIT           NOT NULL,
    [CreatedBy]   CHAR (20)     NOT NULL,
    [CreatedAt]   DATETIME      CONSTRAINT [DF_Cie11Mapping_CreatedAt] DEFAULT (getdate()) NOT NULL,
    [UpdatedBy]   CHAR (20)     NULL,
    [UpdatedAt]   DATETIME      NULL,
    CONSTRAINT [PK_Cie11Mapping] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ_Cie11Mapping_Cie11Code] UNIQUE NONCLUSTERED ([Cie11Code] ASC),
    CONSTRAINT [UQ_Cie11Mapping_Code] UNIQUE NONCLUSTERED ([Code] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de la última modificación', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Cie11Mapping', @level2type = N'COLUMN', @level2name = N'UpdatedAt';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Cie11Mapping', @level2type = N'COLUMN', @level2name = N'UpdatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Cie11Mapping', @level2type = N'COLUMN', @level2name = N'CreatedAt';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Cie11Mapping', @level2type = N'COLUMN', @level2name = N'CreatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el registro se encuentra activo (1) o inactivo (0)', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Cie11Mapping', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del diagnóstico o procedimiento', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Cie11Mapping', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CIE11 relacionado', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Cie11Mapping', @level2type = N'COLUMN', @level2name = N'Cie11Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código interno del procedimiento o diagnóstico', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Cie11Mapping', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Cie11Mapping', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de mapeo entre códigos CIE11 y CIE10 (CUPS)', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Cie11Mapping';

