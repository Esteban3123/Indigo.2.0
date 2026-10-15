CREATE TABLE [dbo].[HCJUSTECA] (
    [CODCONCEC]                INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDETIPHIS]                CHAR (9)                                                                         NULL,
    [NUMEFOLIO]                NCHAR (10)                                                                       NULL,
    [IPCODPACI]                VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]                CHAR (10)                                                                        NOT NULL,
    [CODCENATE]                CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]                CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]                CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECORDMED]                DATETIME                                                                         NOT NULL,
    [CODSERIPS]                CHAR (20)                                                                        NOT NULL,
    [DESAYUDIA]                VARCHAR (MAX)                                                                    NOT NULL,
    [DESJUSTEC]                VARCHAR (MAX)                                                                    NOT NULL,
    [DESINCAYU]                VARCHAR (MAX)                                                                    NOT NULL,
    [CODMINSALUD]              VARCHAR (30)                                                                     NULL,
    [IDDESCRIPCIONRELACIONADA] INT                                                                              NULL,
    [PRESCRIPCIONJSON]         VARCHAR (8000)                                                                   NULL,
    [CodCUPSMipres]            VARCHAR (262)                                                                    NULL,
    [ORDUNICA]                 BIT                                                                              NULL,
    CONSTRAINT [PK_HCJUSTECA] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC),
    CONSTRAINT [FK_HCJUSTECA_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCJUSTECA_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCJUSTECA_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_HCJUSTECA_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCJUSTECA_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCJUSTECA_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCJUSTECA] NOCHECK CONSTRAINT [FK_HCJUSTECA_ADCENATEN];


GO
ALTER TABLE [dbo].[HCJUSTECA] NOCHECK CONSTRAINT [FK_HCJUSTECA_ADINGRESO];


GO
ALTER TABLE [dbo].[HCJUSTECA] NOCHECK CONSTRAINT [FK_HCJUSTECA_INCUPSIPS];


GO
ALTER TABLE [dbo].[HCJUSTECA] NOCHECK CONSTRAINT [FK_HCJUSTECA_INPACIENT];


GO
ALTER TABLE [dbo].[HCJUSTECA] NOCHECK CONSTRAINT [FK_HCJUSTECA_INPROFSAL];


GO
ALTER TABLE [dbo].[HCJUSTECA] NOCHECK CONSTRAINT [FK_HCJUSTECA_INUNIFUNC];


GO
CREATE NONCLUSTERED INDEX [IX_HCJUSTECA]
    ON [dbo].[HCJUSTECA]([IPCODPACI] ASC, [NUMINGRES] ASC, [NUMEFOLIO] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si el número de prescripción es o no unica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECA', @level2type = N'COLUMN', @level2name = N'ORDUNICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código CUPS de Mipres', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECA', @level2type = N'COLUMN', @level2name = N'CodCUPSMipres';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Prescripción en formato JSON', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECA', @level2type = N'COLUMN', @level2name = N'PRESCRIPCIONJSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECA', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de prescripción MINSALUD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECA', @level2type = N'COLUMN', @level2name = N'CODMINSALUD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion de la Incidencia de ayuda diagnostica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECA', @level2type = N'COLUMN', @level2name = N'DESINCAYU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'justificacion tecnica cientifica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECA', @level2type = N'COLUMN', @level2name = N'DESJUSTEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descirpcion de las ayudas diagnosticas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECA', @level2type = N'COLUMN', @level2name = N'DESAYUDIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Unico de Procedimientos y Servicios solicitado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECA', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Solicitud de la Orden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECA', @level2type = N'COLUMN', @level2name = N'FECORDMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECA', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECA', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECA', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECA', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECA', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECA', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Concecutivo interno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECA', @level2type = N'COLUMN', @level2name = N'CODCONCEC';

