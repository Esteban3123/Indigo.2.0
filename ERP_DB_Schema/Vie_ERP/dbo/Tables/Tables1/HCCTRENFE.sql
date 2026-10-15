CREATE TABLE [dbo].[HCCTRENFE] (
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [CODCENATE] CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO] CHAR (10)                                                                        NOT NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECINIHIS] DATETIME                                                                         NOT NULL,
    [FECFINHIS] DATETIME                                                                         NULL,
    [ESTREGENF] BIT                                                                              NOT NULL,
    CONSTRAINT [PK_HCCTRENFE] PRIMARY KEY CLUSTERED ([IPCODPACI] ASC, [NUMINGRES] ASC, [FECINIHIS] ASC),
    CONSTRAINT [FK_HCCTRENFE_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCCTRENFE_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCCTRENFE_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCCTRENFE_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCCTRENFE_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCCTRENFE] NOCHECK CONSTRAINT [FK_HCCTRENFE_ADCENATEN];


GO
ALTER TABLE [dbo].[HCCTRENFE] NOCHECK CONSTRAINT [FK_HCCTRENFE_ADINGRESO];


GO
ALTER TABLE [dbo].[HCCTRENFE] NOCHECK CONSTRAINT [FK_HCCTRENFE_INPACIENT];


GO
ALTER TABLE [dbo].[HCCTRENFE] NOCHECK CONSTRAINT [FK_HCCTRENFE_INPROFSAL];


GO
ALTER TABLE [dbo].[HCCTRENFE] NOCHECK CONSTRAINT [FK_HCCTRENFE_INUNIFUNC];


GO
CREATE NONCLUSTERED INDEX [IX_HCCTRENFE]
    ON [dbo].[HCCTRENFE]([IPCODPACI] ASC, [NUMINGRES] ASC, [ESTREGENF] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Registro de Enfermeria  True: Abierto  False: Cerrado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRENFE', @level2type = N'COLUMN', @level2name = N'ESTREGENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha y Hora Final de la Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRENFE', @level2type = N'COLUMN', @level2name = N'FECFINHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha y Hora Inicial de la Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRENFE', @level2type = N'COLUMN', @level2name = N'FECINIHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRENFE', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRENFE', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRENFE', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRENFE', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRENFE', @level2type = N'COLUMN', @level2name = N'IPCODPACI';

