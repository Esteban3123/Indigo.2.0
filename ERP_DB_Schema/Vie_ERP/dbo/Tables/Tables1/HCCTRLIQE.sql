CREATE TABLE [dbo].[HCCTRLIQE] (
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [CODCENATE] CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO] CHAR (10)                                                                        NOT NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECREGIST] DATETIME                                                                         NOT NULL,
    [CODLIQELI] CHAR (4)                                                                         NOT NULL,
    [CANLIQELI] NUMERIC (6, 2)                                                                   NOT NULL,
    [CODMEDELI] CHAR (2)                                                                         NOT NULL,
    CONSTRAINT [PK_HCCTRLIQE] PRIMARY KEY CLUSTERED ([IPCODPACI] ASC, [FECREGIST] ASC, [CODLIQELI] ASC, [CODMEDELI] ASC),
    CONSTRAINT [FK_HCCTRLIQE_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCCTRLIQE_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCCTRLIQE_HCLMEDELI] FOREIGN KEY ([CODMEDELI]) REFERENCES [dbo].[HCLMEDELI] ([CODMEDELI]),
    CONSTRAINT [FK_HCCTRLIQE_HCTLIQELI] FOREIGN KEY ([CODLIQELI]) REFERENCES [dbo].[HCTLIQELI] ([CODLIQELI]),
    CONSTRAINT [FK_HCCTRLIQE_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCCTRLIQE_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCCTRLIQE_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCCTRLIQE] NOCHECK CONSTRAINT [FK_HCCTRLIQE_ADCENATEN];


GO
ALTER TABLE [dbo].[HCCTRLIQE] NOCHECK CONSTRAINT [FK_HCCTRLIQE_ADINGRESO];


GO
ALTER TABLE [dbo].[HCCTRLIQE] NOCHECK CONSTRAINT [FK_HCCTRLIQE_HCLMEDELI];


GO
ALTER TABLE [dbo].[HCCTRLIQE] NOCHECK CONSTRAINT [FK_HCCTRLIQE_HCTLIQELI];


GO
ALTER TABLE [dbo].[HCCTRLIQE] NOCHECK CONSTRAINT [FK_HCCTRLIQE_INPACIENT];


GO
ALTER TABLE [dbo].[HCCTRLIQE] NOCHECK CONSTRAINT [FK_HCCTRLIQE_INPROFSAL];


GO
ALTER TABLE [dbo].[HCCTRLIQE] NOCHECK CONSTRAINT [FK_HCCTRLIQE_INUNIFUNC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Medio de Eliminacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRLIQE', @level2type = N'COLUMN', @level2name = N'CODMEDELI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad de Liquido Eliminado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRLIQE', @level2type = N'COLUMN', @level2name = N'CANLIQELI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Liquido Eliminado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRLIQE', @level2type = N'COLUMN', @level2name = N'CODLIQELI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha y Hora del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRLIQE', @level2type = N'COLUMN', @level2name = N'FECREGIST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRLIQE', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRLIQE', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRLIQE', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRLIQE', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRLIQE', @level2type = N'COLUMN', @level2name = N'IPCODPACI';

