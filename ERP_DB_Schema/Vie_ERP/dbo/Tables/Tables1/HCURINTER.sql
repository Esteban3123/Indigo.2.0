CREATE TABLE [dbo].[HCURINTER] (
    [AUTO]       INT                                                                               IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]  VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')  NOT NULL,
    [NUMINGRES]  CHAR (10)                                                                         NOT NULL,
    [CODCENATE]  CHAR (10)                                                                         NOT NULL,
    [UFUCODIGO]  CHAR (10)                                                                         NOT NULL,
    [FECINIATE]  DATETIME                                                                          NOT NULL,
    [CODPROSAL]  CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')     NOT NULL,
    [MOTCONSUL]  VARCHAR (MAX)                                                                     NOT NULL,
    [ENFACTUAL]  VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "CurrentIllness_Ofuscado", 0)') NOT NULL,
    [REVSISTEMA] VARCHAR (MAX)                                                                     NOT NULL,
    [ANALISISP]  VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "Analysis_Ofuscado", 0)')       NOT NULL,
    [ORDENMED]   VARCHAR (MAX)                                                                     NOT NULL,
    CONSTRAINT [PK_HCURINTER] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_HCURINTER_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCURINTER_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCURINTER_HCURINTER] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCURINTER_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCURINTER_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [IX_HCURINTER] UNIQUE NONCLUSTERED ([IPCODPACI] ASC, [NUMINGRES] ASC)
);


GO
ALTER TABLE [dbo].[HCURINTER] NOCHECK CONSTRAINT [FK_HCURINTER_ADCENATEN];


GO
ALTER TABLE [dbo].[HCURINTER] NOCHECK CONSTRAINT [FK_HCURINTER_ADINGRESO];


GO
ALTER TABLE [dbo].[HCURINTER] NOCHECK CONSTRAINT [FK_HCURINTER_HCURINTER];


GO
ALTER TABLE [dbo].[HCURINTER] NOCHECK CONSTRAINT [FK_HCURINTER_INPROFSAL];


GO
ALTER TABLE [dbo].[HCURINTER] NOCHECK CONSTRAINT [FK_HCURINTER_INUNIFUNC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ordenes Medicas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURINTER', @level2type = N'COLUMN', @level2name = N'ORDENMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Analisis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURINTER', @level2type = N'COLUMN', @level2name = N'ANALISISP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Revision por Sistemas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURINTER', @level2type = N'COLUMN', @level2name = N'REVSISTEMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Enfermedad Actual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURINTER', @level2type = N'COLUMN', @level2name = N'ENFACTUAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Motivo de Consulta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURINTER', @level2type = N'COLUMN', @level2name = N'MOTCONSUL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURINTER', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Inicial de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURINTER', @level2type = N'COLUMN', @level2name = N'FECINIATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURINTER', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURINTER', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURINTER', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURINTER', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico interno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURINTER', @level2type = N'COLUMN', @level2name = N'AUTO';

