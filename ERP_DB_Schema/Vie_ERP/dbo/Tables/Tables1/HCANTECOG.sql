CREATE TABLE [dbo].[HCANTECOG] (
    [CONSECUTI]                             NUMERIC (18)                                                                     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDETIPHIS]                             CHAR (9)                                                                         NOT NULL,
    [NUMEFOLIO]                             NCHAR (10)                                                                       NOT NULL,
    [IPCODPACI]                             VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]                             CHAR (10)                                                                        NOT NULL,
    [CODCENATE]                             CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]                             CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]                             CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECULTECO]                             DATETIME                                                                         NOT NULL,
    [NUMSEMULT]                             NUMERIC (3, 1)                                                                   NOT NULL,
    [FECHISPAC]                             DATETIME                                                                         NOT NULL,
    [NUMSEMHIS]                             NUMERIC (3, 1)                                                                   NOT NULL,
    [UpcomingDateOfParturitionByUltrasound] DATETIME                                                                         NULL,
    [Percentile]                            NUMERIC (8, 2)                                                                   NULL,
    [Size]                                  NUMERIC (6, 2)                                                                   NULL,
    [UltrasoundObservations]                VARCHAR (100)                                                                    NULL,
    [UltrasoundForEstimatedDueDate]         BIT                                                                              NULL,
    [Weight]                                NUMERIC (7, 2)                                                                   NULL,
    [AmnioticFluidIndex]                    NUMERIC (5, 2)                                                                   NULL,
    [MaximumVerticalPocket]                 NUMERIC (5, 2)                                                                   NULL,
    CONSTRAINT [PK_HCANTECOG] PRIMARY KEY CLUSTERED ([CONSECUTI] ASC),
    CONSTRAINT [FK_HCANTECOG_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCANTECOG_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCANTECOG_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCANTECOG_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCANTECOG_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCANTECOG] NOCHECK CONSTRAINT [FK_HCANTECOG_ADCENATEN];


GO
ALTER TABLE [dbo].[HCANTECOG] NOCHECK CONSTRAINT [FK_HCANTECOG_ADINGRESO];


GO
ALTER TABLE [dbo].[HCANTECOG] NOCHECK CONSTRAINT [FK_HCANTECOG_INPACIENT];


GO
ALTER TABLE [dbo].[HCANTECOG] NOCHECK CONSTRAINT [FK_HCANTECOG_INPROFSAL];


GO
ALTER TABLE [dbo].[HCANTECOG] NOCHECK CONSTRAINT [FK_HCANTECOG_INUNIFUNC];


GO
CREATE NONCLUSTERED INDEX [IX_HCANTEPAC]
    ON [dbo].[HCANTECOG]([IPCODPACI] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Bolsillo vertical máximo (BVM) en cms', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTECOG', @level2type = N'COLUMN', @level2name = N'MaximumVerticalPocket';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo Índice de líquido amniótico (ILA) en cms', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTECOG', @level2type = N'COLUMN', @level2name = N'AmnioticFluidIndex';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo peso (gramos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTECOG', @level2type = N'COLUMN', @level2name = N'Weight';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Determina si la ecografía sirve como principal para fecha probable de parto. 1: Sí, 0: No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTECOG', @level2type = N'COLUMN', @level2name = N'UltrasoundForEstimatedDueDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones de ecografía', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTECOG', @level2type = N'COLUMN', @level2name = N'UltrasoundObservations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Talla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTECOG', @level2type = N'COLUMN', @level2name = N'Size';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Percentile', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTECOG', @level2type = N'COLUMN', @level2name = N'Percentile';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha probable de parto por ecografia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTECOG', @level2type = N'COLUMN', @level2name = N'UpcomingDateOfParturitionByUltrasound';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'numero de semanas hasta la historia clinica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTECOG', @level2type = N'COLUMN', @level2name = N'NUMSEMHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la historia clinica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTECOG', @level2type = N'COLUMN', @level2name = N'FECHISPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'numero de semanas hasta el dia de la ecografia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTECOG', @level2type = N'COLUMN', @level2name = N'NUMSEMULT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la ultima ecografia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTECOG', @level2type = N'COLUMN', @level2name = N'FECULTECO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTECOG', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTECOG', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTECOG', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTECOG', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTECOG', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTECOG', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTECOG', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo Interno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTECOG', @level2type = N'COLUMN', @level2name = N'CONSECUTI';

