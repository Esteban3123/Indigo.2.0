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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'page antecedentes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'PANTECEDENTES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'page d eordenes medicas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'PORDENESMEDICAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'page examen fisico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'PEXAMENFISICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'page de diagnosticos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'PDIAGNOSTICOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'page subjetivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'PSUBJETIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'page de análisis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'PANALISIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'antecedentes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'ANTECEDENTES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ordenes médicas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'ORDENESMEDICAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'examen fisico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'EXAMENFISICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'diagnosticos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'DIAGNOSTICOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'subjetivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'SUBJETIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Análisis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'ANALISIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Formato Tipo de Historia Clinica  I: Ingreso  E: Evolucion  O: Otros formatos de apoyo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'FORTIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion Tipo de Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'DESTIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Tipo de Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'CODTIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'AUTO';

