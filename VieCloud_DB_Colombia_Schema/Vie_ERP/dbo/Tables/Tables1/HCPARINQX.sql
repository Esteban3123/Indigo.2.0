CREATE TABLE [dbo].[HCPARINQX] (
    [CODCENATE]       CHAR (10) NOT NULL,
    [CODSERIPS]       CHAR (20) NOT NULL,
    [TRATAEGRE]       CHAR (1)  NOT NULL,
    [ID]              INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [USUARIOCREACION] CHAR (20) NULL,
    [FECHACREACION]   DATETIME  NULL,
    CONSTRAINT [PK_HCPARINQX] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCPARINQX_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCPARINQX_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_HCPARINQX_SEGusuaru_1] FOREIGN KEY ([USUARIOCREACION]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI]),
    CONSTRAINT [IX_HCPARINQX] UNIQUE NONCLUSTERED ([CODCENATE] ASC, [CODSERIPS] ASC)
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro en la historia clínica de procedimientos y servicios al egreso del paciente (DATETIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARINQX', @level2type = N'COLUMN', @level2name = N'FECHACREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Creación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARINQX', @level2type = N'COLUMN', @level2name = N'FECHACREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARINQX', @level2type = N'COLUMN', @level2name = N'FECHACREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario o profesional de la salud que creó el registro; vinculado a tabla SEGusuaru para auditoría (VARCHAR 20, PII)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARINQX', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARINQX', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARINQX', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (INT IDENTITY) de la asociación centro-servicio-egreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARINQX', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID o Autonumérico de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARINQX', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARINQX', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de tratamiento al egreso: 1=Intrahospitalario (internación), 2=Extramural (ambulatorio, domiciliario, remotería) (CHAR 1)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARINQX', @level2type = N'COLUMN', @level2name = N'TRATAEGRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Al Egreso Marcar como:  1. Intrahospitalario  2. Extramural', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARINQX', @level2type = N'COLUMN', @level2name = N'TRATAEGRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARINQX', @level2type = N'COLUMN', @level2name = N'TRATAEGRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del procedimiento o servicio según CUPS-RIPS (CHAR 20); referencia a catálogo de servicios de salud (FK INCUPSIPS)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARINQX', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Unico de Procedimientos y Servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARINQX', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARINQX', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro o unidad funcional de atención donde se prestó el servicio/procedimiento (CHAR 10, FK ADCENATEN)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARINQX', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARINQX', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARINQX', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parámetros de ingreso a quirófano por servicio y centro de atención: define qué servicios (CUPS/IPS) requieren tratamiento al egreso dentro del flujo quirúrgico, permitiendo configurar las reglas de admisión a cirugía según el centro de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARINQX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARINQX';
