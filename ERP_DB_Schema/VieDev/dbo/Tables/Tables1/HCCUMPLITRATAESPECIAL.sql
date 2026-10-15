CREATE TABLE [dbo].[HCCUMPLITRATAESPECIAL] (
    [ID]            INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]     VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [IDHCORDPRON]   INT                                                                              NOT NULL,
    [NUMINGRES]     CHAR (10)                                                                        NOT NULL,
    [CODCENATE]     CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]     CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]     CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [CODESPECI]     CHAR (3)                                                                         NOT NULL,
    [FECHAREGISTRO] DATETIME                                                                         NOT NULL,
    [IDCITA]        INT                                                                              NULL,
    [TIPOTRATA]     INT                                                                              CONSTRAINT [DF_HCCUMPLITRATAESPECIAL_TIPOTRATA] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_HCCUMPLIMIENTOQUIMIO] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCCUMPLIMIENTOQUIMIO_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCCUMPLIMIENTOQUIMIO_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCCUMPLIMIENTOQUIMIO_HCCUMPLIMIENTOQUIMIO] FOREIGN KEY ([IDHCORDPRON]) REFERENCES [dbo].[HCORDPRON] ([AUTO]),
    CONSTRAINT [FK_HCCUMPLIMIENTOQUIMIO_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_HCCUMPLIMIENTOQUIMIO_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCCUMPLIMIENTOQUIMIO_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_HCCUMPLITRATAESPECIAL_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HCCUMPLITRATAESPECIAL] NOCHECK CONSTRAINT [FK_HCCUMPLIMIENTOQUIMIO_ADCENATEN];


GO
ALTER TABLE [dbo].[HCCUMPLITRATAESPECIAL] NOCHECK CONSTRAINT [FK_HCCUMPLIMIENTOQUIMIO_ADINGRESO];


GO
ALTER TABLE [dbo].[HCCUMPLITRATAESPECIAL] NOCHECK CONSTRAINT [FK_HCCUMPLIMIENTOQUIMIO_HCCUMPLIMIENTOQUIMIO];


GO
ALTER TABLE [dbo].[HCCUMPLITRATAESPECIAL] NOCHECK CONSTRAINT [FK_HCCUMPLIMIENTOQUIMIO_INESPECIA];


GO
ALTER TABLE [dbo].[HCCUMPLITRATAESPECIAL] NOCHECK CONSTRAINT [FK_HCCUMPLIMIENTOQUIMIO_INPROFSAL];


GO
ALTER TABLE [dbo].[HCCUMPLITRATAESPECIAL] NOCHECK CONSTRAINT [FK_HCCUMPLIMIENTOQUIMIO_INUNIFUNC];


GO
ALTER TABLE [dbo].[HCCUMPLITRATAESPECIAL] NOCHECK CONSTRAINT [FK_HCCUMPLITRATAESPECIAL_INPACIENT];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Tratamiento Especial.   1 - Quimioterapia   2 - Radioterapia   3 - Braquiterapia  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUMPLITRATAESPECIAL', @level2type = N'COLUMN', @level2name = N'TIPOTRATA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el codigo Especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUMPLITRATAESPECIAL', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUMPLITRATAESPECIAL', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el codigo del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUMPLITRATAESPECIAL', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUMPLITRATAESPECIAL', @level2type = N'COLUMN', @level2name = N'ID';

