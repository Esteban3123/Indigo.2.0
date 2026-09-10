CREATE TABLE [dbo].[HCNUMCUARAD] (
    [ID]          INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NUMCUADRO]   VARCHAR (15)                                                                     NOT NULL,
    [IPCODPACI]   VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [FECREGISTRO] DATETIME                                                                         NOT NULL,
    [USUCREACION] CHAR (20)                                                                        NOT NULL,
    [PACIEFOTO]   VARBINARY (MAX)                                                                  NULL,
    [FECHACUADRO] DATETIME                                                                         NOT NULL,
    CONSTRAINT [PK_HCNUMCUARAD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCNUMCUARAD_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HCNUMCUARAD] NOCHECK CONSTRAINT [FK_HCNUMCUARAD_INPACIENT];


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCNUMCUARAD].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');




GO
ALTER TABLE [dbo].[HCNUMCUARAD] NOCHECK CONSTRAINT [FK_HCNUMCUARAD_INPACIENT];


GO
CREATE NONCLUSTERED INDEX [IX_HCNUMCUARAD]
    ON [dbo].[HCNUMCUARAD]([IPCODPACI] ASC, [NUMCUADRO] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de Cuadro Clínico: timestamp de la fecha en que se registra el cuadro clínico o estado del paciente (DATETIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUMCUARAD', @level2type = N'COLUMN', @level2name = N'FECHACUADRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Cuadro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUMCUARAD', @level2type = N'COLUMN', @level2name = N'FECHACUADRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUMCUARAD', @level2type = N'COLUMN', @level2name = N'FECHACUADRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Foto del Paciente: imagen binaria (VARBINARY MAX) para archivo fotográfico o evidencia visual del paciente, nullable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUMCUARAD', @level2type = N'COLUMN', @level2name = N'PACIEFOTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Foto del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUMCUARAD', @level2type = N'COLUMN', @level2name = N'PACIEFOTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUMCUARAD', @level2type = N'COLUMN', @level2name = N'PACIEFOTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de Creación: identificador del usuario que registró el cuadro clínico (CHAR 20, auditoría)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUMCUARAD', @level2type = N'COLUMN', @level2name = N'USUCREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuacrio Creacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUMCUARAD', @level2type = N'COLUMN', @level2name = N'USUCREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUMCUARAD', @level2type = N'COLUMN', @level2name = N'USUCREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de Registro: timestamp de cuándo se creó el registro en el sistema (DATETIME, auditoría)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUMCUARAD', @level2type = N'COLUMN', @level2name = N'FECREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUMCUARAD', @level2type = N'COLUMN', @level2name = N'FECREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUMCUARAD', @level2type = N'COLUMN', @level2name = N'FECREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del Paciente: identificación única del paciente (cédula, documento, identificación PII enmascarada), FK a INPACIENT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUMCUARAD', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUMCUARAD', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUMCUARAD', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de Cuadro: identificador único del cuadro clínico o consulta registrada (VARCHAR 15)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUMCUARAD', @level2type = N'COLUMN', @level2name = N'NUMCUADRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número Cuadro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUMCUARAD', @level2type = N'COLUMN', @level2name = N'NUMCUADRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUMCUARAD', @level2type = N'COLUMN', @level2name = N'NUMCUADRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo de la Tabla: identificador único incremental (INT IDENTITY, PK)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUMCUARAD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUMCUARAD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUMCUARAD', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de cuadros neurológicos o clínicos del paciente en historia clínica. Guarda el número de cuadro, la fecha en que fue elaborado, la foto del paciente asociada y el usuario que lo creó.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUMCUARAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUMCUARAD';
