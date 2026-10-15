CREATE TABLE [dbo].[HCPRESTME] (
    [NUMCONSEC]  NUMERIC (18)                                                                     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FECREGPRE]  DATETIME                                                                         NOT NULL,
    [IPCODPACI]  VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]  CHAR (10)                                                                        NOT NULL,
    [CODPRODUC]  CHAR (20)                                                                        NOT NULL,
    [CANPRODUCT] INT                                                                              NOT NULL,
    [CANPROPEN]  INT                                                                              NOT NULL,
    [IPCODPACP]  VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGREP]  CHAR (10)                                                                        NOT NULL,
    [CODCENATE]  CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]  CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]  CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [PREANULAD]  BIT                                                                              NOT NULL,
    [FECANUPRE]  DATETIME                                                                         NULL,
    [CODPROANU]  CHAR (20)                                                                        NULL,
    CONSTRAINT [PK_HCPRESTME] PRIMARY KEY CLUSTERED ([NUMCONSEC] ASC),
    CONSTRAINT [FK_HCPRESTME_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCPRESTME_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCPRESTME_ADINGRESO1] FOREIGN KEY ([NUMINGREP]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCPRESTME_IHLISTPRO] FOREIGN KEY ([CODPRODUC]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC]),
    CONSTRAINT [FK_HCPRESTME_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCPRESTME_INPACIENT1] FOREIGN KEY ([IPCODPACP]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCPRESTME_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCPRESTME_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCPRESTME] NOCHECK CONSTRAINT [FK_HCPRESTME_ADCENATEN];


GO
ALTER TABLE [dbo].[HCPRESTME] NOCHECK CONSTRAINT [FK_HCPRESTME_ADINGRESO];


GO
ALTER TABLE [dbo].[HCPRESTME] NOCHECK CONSTRAINT [FK_HCPRESTME_ADINGRESO1];


GO
ALTER TABLE [dbo].[HCPRESTME] NOCHECK CONSTRAINT [FK_HCPRESTME_IHLISTPRO];


GO
ALTER TABLE [dbo].[HCPRESTME] NOCHECK CONSTRAINT [FK_HCPRESTME_INPACIENT];


GO
ALTER TABLE [dbo].[HCPRESTME] NOCHECK CONSTRAINT [FK_HCPRESTME_INPACIENT1];


GO
ALTER TABLE [dbo].[HCPRESTME] NOCHECK CONSTRAINT [FK_HCPRESTME_INPROFSAL];


GO
ALTER TABLE [dbo].[HCPRESTME] NOCHECK CONSTRAINT [FK_HCPRESTME_INUNIFUNC];


GO
CREATE NONCLUSTERED INDEX [IX_HCPRESTME]
    ON [dbo].[HCPRESTME]([IPCODPACI] ASC, [NUMINGRES] ASC, [CODCENATE] ASC, [UFUCODIGO] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Profesional que Anula el Prestamo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESTME', @level2type = N'COLUMN', @level2name = N'CODPROANU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Anulacion del Prestamo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESTME', @level2type = N'COLUMN', @level2name = N'FECANUPRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Prestamo Anulado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESTME', @level2type = N'COLUMN', @level2name = N'PREANULAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional de la Salud que registro el prestamo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESTME', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional del prestamo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESTME', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion del prestamo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESTME', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del Ingreso del Paciente que presta el medicamento (Origen)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESTME', @level2type = N'COLUMN', @level2name = N'NUMINGREP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente:  Paciente que presta el medicamento (Origen)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESTME', @level2type = N'COLUMN', @level2name = N'IPCODPACP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad Producto Pendiente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESTME', @level2type = N'COLUMN', @level2name = N'CANPROPEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad Producto prestado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESTME', @level2type = N'COLUMN', @level2name = N'CANPRODUCT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Interno del Producto prestado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESTME', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del Ingreso del paciente al que le prestan el medicamento (Destino)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESTME', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente:  Paciente al que le prestan el medicamento (Destino)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESTME', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Hora del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESTME', @level2type = N'COLUMN', @level2name = N'FECREGPRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero consecutivo automatico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESTME', @level2type = N'COLUMN', @level2name = N'NUMCONSEC';

