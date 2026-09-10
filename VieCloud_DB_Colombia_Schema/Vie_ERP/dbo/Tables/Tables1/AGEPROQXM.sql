CREATE TABLE [dbo].[AGEPROQXM] (
    [AUTONUMER] INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [CODCENATE] CHAR (10)                                                                        NOT NULL,
    [FECHORAIN] DATETIME                                                                         NOT NULL,
    [FECHORAFI] DATETIME                                                                         NOT NULL,
    CONSTRAINT [PK_AGEPROQXM] PRIMARY KEY CLUSTERED ([AUTONUMER] ASC),
    CONSTRAINT [FK_AGEPROQXM_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_AGEPROQXM_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[AGEPROQXM].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora final de la cirugía programada; timestamp de término de procedimiento quirúrgico; tipo DATETIME', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROQXM', @level2type = N'COLUMN', @level2name = N'FECHORAFI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'FECHA Y HORA FINAL DE LA CIRUGIA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROQXM', @level2type = N'COLUMN', @level2name = N'FECHORAFI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROQXM', @level2type = N'COLUMN', @level2name = N'FECHORAFI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora inicial de la cirugía programada; timestamp de inicio de procedimiento quirúrgico; tipo DATETIME', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROQXM', @level2type = N'COLUMN', @level2name = N'FECHORAIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'FECHA Y HORA INICIAL DE LA CIRUGIA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROQXM', @level2type = N'COLUMN', @level2name = N'FECHORAIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROQXM', @level2type = N'COLUMN', @level2name = N'FECHORAIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención (FK a ADCENATEN); identificador de unidad funcional, clínica o hospital donde se ejecuta la cirugía; tipo CHAR(10)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROQXM', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROQXM', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROQXM', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente (PII - identificación ofuscada); cédula, documento o equivalente de identificación del paciente; FK a INPACIENT; tipo VARCHAR(25)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROQXM', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROQXM', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROQXM', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementado (INT IDENTITY) del registro de cirugía programada múltiple; clave primaria de la tabla AGEPROQXM', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROQXM', @level2type = N'COLUMN', @level2name = N'AUTONUMER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'autonumerico CIRUGIA PROGRAMADA MULTIPLE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROQXM', @level2type = N'COLUMN', @level2name = N'AUTONUMER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROQXM', @level2type = N'COLUMN', @level2name = N'AUTONUMER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de programación de procedimientos quirúrgicos: guarda los bloques de tiempo asignados a cada paciente en un centro de atención para la realización de cirugías o intervenciones quirúrgicas, incluyendo la franja horaria de inicio y fin.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROQXM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPROQXM';
