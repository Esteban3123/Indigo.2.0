CREATE TABLE [Payroll].[PersonasFechas] (
    [NumeroIdentificacion]     VARCHAR (20) NULL,
    [FechaExpedicionDocumento] DATE         NULL,
    [FechaCumpleanos]          DATE         NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla auxiliar del módulo de nómina que almacena fechas clave asociadas a personas identificadas por su número de documento: la fecha de expedición del documento de identidad y la fecha de nacimiento. No contiene llave primaria definida ni restricciones de integridad referencial explícitas en el DDL.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'TABLE', @level1name=N'PersonasFechas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'TABLE', @level1name=N'PersonasFechas';
GO
