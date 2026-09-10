CREATE TABLE [Taxes].[TempOmissive] (
    [Id]  INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Nit] VARCHAR (50) NOT NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'NIT del registro omisivo (Número de Identificación Tributaria). Identificador fiscal único del contribuyente o entidad sujeta a obligación declarativa omitida. VARCHAR(50), clave para trazabilidad fiscal y auditoría de declaraciones incompletas.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TempOmissive', @level2type = N'COLUMN', @level2name = N'Nit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el nil del registro omisivo.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TempOmissive', @level2type = N'COLUMN', @level2name = N'Nit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TempOmissive', @level2type = N'COLUMN', @level2name = N'Nit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK) del registro temporal omisivo. INT IDENTITY, clave primaria autoincrementable para control de instancias de procesamiento de registros omisivos en reconciliación tributaria.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TempOmissive', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador único del registro.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TempOmissive', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TempOmissive', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro temporal de terceros (proveedores, pacientes o entidades) identificados como omisos o ausentes en procesos tributarios o fiscales, almacenando su NIT o documento de identificación para seguimiento.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TempOmissive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TempOmissive';
