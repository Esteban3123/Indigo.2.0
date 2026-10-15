CREATE TABLE [dbo].[HCINFCONI] (
    [CODCONCEC]        NUMERIC (18)                                                                     NOT NULL,
    [CODPRODUC]        CHAR (20)                                                                        NOT NULL,
    [ABRPROMEZ]        CHAR (200)                                                                       NULL,
    [CONMEDMEZ]        NUMERIC (18, 2)                                                                  NOT NULL,
    [UNIMEDMED]        VARCHAR (20)                                                                     NOT NULL,
    [CANPROCAL]        INT                                                                              NOT NULL,
    [UNIMEDVOL]        CHAR (3)                                                                         NOT NULL,
    [UNIMEDPES]        CHAR (3)                                                                         NOT NULL,
    [NUMEFOLIO]        NCHAR (10)                                                                       NOT NULL,
    [IPCODPACI]        VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]        CHAR (10)                                                                        NOT NULL,
    [CODCENATE]        CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]        CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]        CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [CONMEDMFN]        NUMERIC (18, 2)                                                                  NULL,
    [UNIMEDMFN]        CHAR (3)                                                                         NULL,
    [JUSTIFICACIONPBS] VARCHAR (2000)                                                                   NULL,
    CONSTRAINT [PK_HCINFCONI_1] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC, [CODPRODUC] ASC),
    CONSTRAINT [FK_HCINFCONI_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCINFCONI_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCINFCONI_HCINFLIQC] FOREIGN KEY ([CODCONCEC]) REFERENCES [dbo].[HCINFLICI] ([CODCONCEC]),
    CONSTRAINT [FK_HCINFCONI_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCINFCONI_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_HCINFCONI_INUNIMEDI] FOREIGN KEY ([UNIMEDMED]) REFERENCES [dbo].[INUNIMEDI] ([CODUNIMED])
);


GO
ALTER TABLE [dbo].[HCINFCONI] NOCHECK CONSTRAINT [FK_HCINFCONI_ADCENATEN];


GO
ALTER TABLE [dbo].[HCINFCONI] NOCHECK CONSTRAINT [FK_HCINFCONI_ADINGRESO];


GO
ALTER TABLE [dbo].[HCINFCONI] NOCHECK CONSTRAINT [FK_HCINFCONI_HCINFLIQC];


GO
ALTER TABLE [dbo].[HCINFCONI] NOCHECK CONSTRAINT [FK_HCINFCONI_INPACIENT];


GO
ALTER TABLE [dbo].[HCINFCONI] NOCHECK CONSTRAINT [FK_HCINFCONI_INUNIFUNC];


GO
ALTER TABLE [dbo].[HCINFCONI] NOCHECK CONSTRAINT [FK_HCINFCONI_INUNIMEDI];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Justificación clínica PBS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'JUSTIFICACIONPBS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de Medida concentracion Mezcla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'UNIMEDMFN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Concentracion medicamento para la mezcla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'CONMEDMFN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de Peso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'UNIMEDPES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de Volumen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'UNIMEDVOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad Inicial de producto calculada para la mezcla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'CANPROCAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de Medida concentracion Mezcla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'UNIMEDMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Concentracion medicamento para la mezcla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'CONMEDMEZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Abreviatura del Producto para mostrar en mezclas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'ABRPROMEZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Consecutivo de la cabecera', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONI', @level2type = N'COLUMN', @level2name = N'CODCONCEC';

