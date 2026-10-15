CREATE TABLE [dbo].[HCINFCONC] (
    [CODCONCEC]            NUMERIC (18)                                                                     NOT NULL,
    [CODPRODUC]            CHAR (20)                                                                        NOT NULL,
    [ABRPROMEZ]            CHAR (200)                                                                       NULL,
    [CONMEDMEZ]            NUMERIC (18, 2)                                                                  NOT NULL,
    [UNIMEDMED]            VARCHAR (20)                                                                     NOT NULL,
    [CANPROCAL]            INT                                                                              NOT NULL,
    [UNIMEDVOL]            CHAR (3)                                                                         NOT NULL,
    [UNIMEDPES]            CHAR (3)                                                                         NOT NULL,
    [NUMEFOLIO]            NCHAR (10)                                                                       NOT NULL,
    [IPCODPACI]            VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]            CHAR (10)                                                                        NOT NULL,
    [CODCENATE]            CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]            CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]            CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [CONMEDMFN]            NUMERIC (18, 2)                                                                  NULL,
    [UNIMEDMFN]            CHAR (3)                                                                         NULL,
    [JUSTIFICACIONPBS]     VARCHAR (2000)                                                                   NULL,
    [CANTIDADMEZCLASMAGIS] INT                                                                              NULL,
    [NUMEROAPLICACIONES]   INT                                                                              NULL,
    CONSTRAINT [PK_HCINFCONC_1] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC, [CODPRODUC] ASC),
    CONSTRAINT [FK_HCINFCONC_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCINFCONC_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCINFCONC_HCINFLIQC] FOREIGN KEY ([CODCONCEC]) REFERENCES [dbo].[HCINFLIQC] ([CODCONCEC]),
    CONSTRAINT [FK_HCINFCONC_IHLISTPRO] FOREIGN KEY ([CODPRODUC]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC]),
    CONSTRAINT [FK_HCINFCONC_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCINFCONC_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_HCINFCONC_INUNIMEDI] FOREIGN KEY ([UNIMEDMED]) REFERENCES [dbo].[INUNIMEDI] ([CODUNIMED])
);


GO
ALTER TABLE [dbo].[HCINFCONC] NOCHECK CONSTRAINT [FK_HCINFCONC_ADCENATEN];


GO
ALTER TABLE [dbo].[HCINFCONC] NOCHECK CONSTRAINT [FK_HCINFCONC_ADINGRESO];


GO
ALTER TABLE [dbo].[HCINFCONC] NOCHECK CONSTRAINT [FK_HCINFCONC_HCINFLIQC];


GO
ALTER TABLE [dbo].[HCINFCONC] NOCHECK CONSTRAINT [FK_HCINFCONC_IHLISTPRO];


GO
ALTER TABLE [dbo].[HCINFCONC] NOCHECK CONSTRAINT [FK_HCINFCONC_INPACIENT];


GO
ALTER TABLE [dbo].[HCINFCONC] NOCHECK CONSTRAINT [FK_HCINFCONC_INUNIFUNC];


GO
ALTER TABLE [dbo].[HCINFCONC] NOCHECK CONSTRAINT [FK_HCINFCONC_INUNIMEDI];


GO
CREATE NONCLUSTERED INDEX [Productos_Pacientes_Ix]
    ON [dbo].[HCINFCONC]([CODPRODUC] ASC)
    INCLUDE([CODCONCEC]);


GO
CREATE NONCLUSTERED INDEX [IX_HCINFCONC]
    ON [dbo].[HCINFCONC]([IPCODPACI] ASC, [NUMEFOLIO] ASC);


GO
CREATE NONCLUSTERED INDEX [_dta_index_HCINFCONC_8_1474104292__K10_K11_K1_K2]
    ON [dbo].[HCINFCONC]([IPCODPACI] ASC, [NUMINGRES] ASC, [CODCONCEC] ASC, [CODPRODUC] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Aplicaciones  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'NUMEROAPLICACIONES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo que aplica solo para la Mezcla Magistral y hace referencia a la cantidad de mezclas que van a preparar    Ejemplo:    Cantidad de Acetaminofen: 1 para una mezcla  Cantidad de Diclofenaco:  1 para una mezcla  Cantidad de Mezclas: 3     Total:    Cantidad de Acetaminofen: 3   Cantidad de Diclofenaco: 3     Se multiplica la cantidad para una mezcla POR el valor de la cantidad de mezclas para dar el total para cada medicamento.      ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'CANTIDADMEZCLASMAGIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Justificación clínica PBS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'JUSTIFICACIONPBS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de Medida concentracion Mezcla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'UNIMEDMFN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Concentracion medicamento para la mezcla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'CONMEDMFN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de Peso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'UNIMEDPES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de Volumen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'UNIMEDVOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad Inicial de producto calculada para la mezcla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'CANPROCAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de Medida concentracion Mezcla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'UNIMEDMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Concentracion medicamento para la mezcla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'CONMEDMEZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Abreviatura del Producto para mostrar en mezclas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'ABRPROMEZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Consecutivo de la cabecera', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFCONC', @level2type = N'COLUMN', @level2name = N'CODCONCEC';

