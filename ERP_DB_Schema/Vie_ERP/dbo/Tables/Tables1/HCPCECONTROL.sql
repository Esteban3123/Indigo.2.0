CREATE TABLE [dbo].[HCPCECONTROL] (
    [ID]                    INT                                                                           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NUMINGRES]             CHAR (10)                                                                     NOT NULL,
    [FECHACREA]             DATETIME                                                                      NOT NULL,
    [CODUSUCREA]            CHAR (20)                                                                     NOT NULL,
    [CODPLANCENF]           VARCHAR (3)                                                                   NULL,
    [OBSERVACION]           VARCHAR (500)                                                                 NULL,
    [UFUCODIGO]             CHAR (10)                                                                     NOT NULL,
    [CODPROSAL]             CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [CODCENATE]             CHAR (10)                                                                     NOT NULL,
    [ESTADO]                INT                                                                           NULL,
    [CODPROSALFINALIZO]     CHAR (10)                                                                     NULL,
    [FECHAFINALIZO]         DATETIME                                                                      NULL,
    [CODPROSALSUSPENDIO]    CHAR (10)                                                                     NULL,
    [FECHASUSPENDIO]        DATETIME                                                                      NULL,
    [CODPROSALCAMBIOESTADO] CHAR (20)                                                                     NULL,
    [FECHACAMBIOESTADO]     DATETIME                                                                      NULL,
    CONSTRAINT [PK_HCPCECONTROL] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCPCECONTROL_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCPCECONTROL_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCPCECONTROL_ADINGRESO2] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCPCECONTROL_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCPCECONTROL_INPROFSAL2] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCPCECONTROL_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_HCPCECONTROL_INUNIFUNC2] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCPCECONTROL] NOCHECK CONSTRAINT [FK_HCPCECONTROL_ADCENATEN];


GO
ALTER TABLE [dbo].[HCPCECONTROL] NOCHECK CONSTRAINT [FK_HCPCECONTROL_ADINGRESO];


GO
ALTER TABLE [dbo].[HCPCECONTROL] NOCHECK CONSTRAINT [FK_HCPCECONTROL_ADINGRESO2];


GO
ALTER TABLE [dbo].[HCPCECONTROL] NOCHECK CONSTRAINT [FK_HCPCECONTROL_INPROFSAL];


GO
ALTER TABLE [dbo].[HCPCECONTROL] NOCHECK CONSTRAINT [FK_HCPCECONTROL_INPROFSAL2];


GO
ALTER TABLE [dbo].[HCPCECONTROL] NOCHECK CONSTRAINT [FK_HCPCECONTROL_INUNIFUNC];


GO
ALTER TABLE [dbo].[HCPCECONTROL] NOCHECK CONSTRAINT [FK_HCPCECONTROL_INUNIFUNC2];


GO
CREATE NONCLUSTERED INDEX [IX_HCPCECONTROL_ESTADO]
    ON [dbo].[HCPCECONTROL]([ESTADO] ASC)
    INCLUDE([CODPLANCENF], [CODPROSAL]);


GO
CREATE NONCLUSTERED INDEX [IX_HCPCECONTROL_NUMINGRES_ESTADO_CODPLANCENF]
    ON [dbo].[HCPCECONTROL]([NUMINGRES] ASC, [ESTADO] ASC, [CODPLANCENF] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la fecha cambio de estado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL', @level2type = N'COLUMN', @level2name = N'FECHACAMBIOESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código usuario profesional cambio estado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL', @level2type = N'COLUMN', @level2name = N'CODPROSALCAMBIOESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la fecha de suspención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL', @level2type = N'COLUMN', @level2name = N'FECHASUSPENDIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código usuario profesional suspendido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL', @level2type = N'COLUMN', @level2name = N'CODPROSALSUSPENDIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la fecha de finalizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL', @level2type = N'COLUMN', @level2name = N'FECHAFINALIZO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional quien finalizo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL', @level2type = N'COLUMN', @level2name = N'CODPROSALFINALIZO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Activo  2 - Suspendido  3 - Finalizado  4 - Anulado por continuidad del plan', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'CODCENATE Codigo del Centro de Atencion ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del profesional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la unidad funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones del plan de cuidado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL', @level2type = N'COLUMN', @level2name = N'OBSERVACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del plan de cuidado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL', @level2type = N'COLUMN', @level2name = N'CODPLANCENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del usuario que crea el registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL', @level2type = N'COLUMN', @level2name = N'CODUSUCREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha y hora de la creacion del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL', @level2type = N'COLUMN', @level2name = N'FECHACREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador de registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECONTROL', @level2type = N'COLUMN', @level2name = N'ID';

