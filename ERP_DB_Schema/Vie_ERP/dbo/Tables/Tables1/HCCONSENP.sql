CREATE TABLE [dbo].[HCCONSENP] (
    [CODCONCEC] CHAR (10)                                                                        NOT NULL,
    [IDETIPHIS] CHAR (9)                                                                         NULL,
    [NUMEFOLIO] CHAR (10)                                                                        NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NULL,
    [CODCENATE] CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO] CHAR (10)                                                                        NOT NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECCONINF] DATETIME                                                                         NOT NULL,
    [AUTCONINF] CHAR (80)                                                                        NULL,
    [CODSERIPS] CHAR (20)                                                                        NOT NULL,
    [OTRSERIPS] CHAR (100)                                                                       NULL,
    [TIPPROIPS] CHAR (1)                                                                         NOT NULL,
    [TIPANEPRO] CHAR (1)                                                                         NOT NULL,
    [ASPPROIPS] VARCHAR (4000)                                                                   NULL,
    [RIEPROIPS] VARCHAR (4000)                                                                   NULL,
    [DETPROIPS] VARCHAR (4000)                                                                   NULL,
    CONSTRAINT [PK_HCCONSENP] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC),
    CONSTRAINT [FK_HCCONSENP_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCCONSENP_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCCONSENP_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_HCCONSENP_INPacient] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCCONSENP_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCCONSENP_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCCONSENP] NOCHECK CONSTRAINT [FK_HCCONSENP_ADcenaten];


GO
ALTER TABLE [dbo].[HCCONSENP] NOCHECK CONSTRAINT [FK_HCCONSENP_ADINGRESO];


GO
ALTER TABLE [dbo].[HCCONSENP] NOCHECK CONSTRAINT [FK_HCCONSENP_INCUPSIPS];


GO
ALTER TABLE [dbo].[HCCONSENP] NOCHECK CONSTRAINT [FK_HCCONSENP_INPacient];


GO
ALTER TABLE [dbo].[HCCONSENP] NOCHECK CONSTRAINT [FK_HCCONSENP_INPROFSAL];


GO
ALTER TABLE [dbo].[HCCONSENP] NOCHECK CONSTRAINT [FK_HCCONSENP_INUNIFUNC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Detalle Adicional del Procedimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONSENP', @level2type = N'COLUMN', @level2name = N'DETPROIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Riesgos del Procedimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONSENP', @level2type = N'COLUMN', @level2name = N'RIEPROIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Aspectos del Procedimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONSENP', @level2type = N'COLUMN', @level2name = N'ASPPROIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Anestesia utilizada en el procedimiento  1: General  2: Local  3: Regional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONSENP', @level2type = N'COLUMN', @level2name = N'TIPANEPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Procedimiento IPS  1: Procedimiento Especial  2: Procedimiento Quirurgico  3: Otro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONSENP', @level2type = N'COLUMN', @level2name = N'TIPPROIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del Procedimiento a Realizar en caso de que no este codigicado el evento o procedimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONSENP', @level2type = N'COLUMN', @level2name = N'OTRSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Servicio IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONSENP', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la Persona que autoriza el Consentimiento Informado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONSENP', @level2type = N'COLUMN', @level2name = N'AUTCONINF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de elaboracion del Documento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONSENP', @level2type = N'COLUMN', @level2name = N'FECCONINF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONSENP', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONSENP', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONSENP', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONSENP', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONSENP', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONSENP', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONSENP', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Concecutivo de Consentimiento Informado Nota: Codigo 00000019', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONSENP', @level2type = N'COLUMN', @level2name = N'CODCONCEC';

