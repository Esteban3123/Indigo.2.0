CREATE TABLE [dbo].[HCCUSTOMI] (
    [AUTO]            INT                                      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODTIPHIS]       CHAR (3)                                 NOT NULL,
    [DESTIPHIS]       CHAR (60)                                NOT NULL,
    [IDETIPHIS]       CHAR (9)                                 NOT NULL,
    [FORTIPHIS]       CHAR (1)                                 NOT NULL,
    [ANALISIS]        BIT MASKED WITH (FUNCTION = 'default()') NOT NULL,
    [SUBJETIVO]       BIT                                      NOT NULL,
    [DIAGNOSTICOS]    BIT                                      NOT NULL,
    [EXAMENFISICO]    BIT MASKED WITH (FUNCTION = 'default()') NOT NULL,
    [ORDENESMEDICAS]  BIT                                      NOT NULL,
    [ANTECEDENTES]    BIT                                      NOT NULL,
    [PANALISIS]       INT MASKED WITH (FUNCTION = 'default()') NULL,
    [PSUBJETIVO]      INT                                      NULL,
    [PDIAGNOSTICOS]   INT                                      NULL,
    [PEXAMENFISICO]   INT MASKED WITH (FUNCTION = 'default()') NULL,
    [PORDENESMEDICAS] INT                                      NULL,
    [PANTECEDENTES]   INT                                      NULL,
    CONSTRAINT [PK_HCCUSTOMI] PRIMARY KEY CLUSTERED ([AUTO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Formato Tipo de Historia Clinica  I: Ingreso  E: Evolucion  O: Otros formatos de apoyo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'FORTIPHIS';

