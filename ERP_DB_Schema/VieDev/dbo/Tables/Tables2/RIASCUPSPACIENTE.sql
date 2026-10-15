CREATE TABLE [dbo].[RIASCUPSPACIENTE] (
    [ID]                     INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]              VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [IDRIASCUPS]             INT                                                                              NOT NULL,
    [CODSERIPS]              CHAR (20)                                                                        NOT NULL,
    [CANTIDAD]               INT                                                                              NOT NULL,
    [ESTADO]                 INT                                                                              NOT NULL,
    [FECHAMINREALIZAR]       DATETIME                                                                         NOT NULL,
    [FECHAMAXREALIZAR]       DATETIME                                                                         NOT NULL,
    [IDRIASCUPSD]            INT                                                                              NOT NULL,
    [ORIGEN]                 INT                                                                              NOT NULL,
    [ACCION]                 INT                                                                              NOT NULL,
    [FOLIO]                  CHAR (10)                                                                        NULL,
    [NUMINGRES]              CHAR (10)                                                                        NULL,
    [FECHAREALIZACION]       DATETIME                                                                         NULL,
    [IDCITA]                 INT                                                                              NULL,
    [IDDETALLEORDENSERVICIO] INT                                                                              NULL,
    [IDFACTURA]              INT                                                                              NULL,
    [VALORCUPS]              DECIMAL (18, 2)                                                                  NULL,
    [CODPROSAL]              CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NULL,
    [CODUSUARU]              CHAR (20)                                                                        NOT NULL,
    [FECHACREACION]          DATETIME                                                                         NOT NULL,
    [CODUSUARUMOD]           CHAR (20)                                                                        NULL,
    [FECHAMODIFICACION]      DATETIME                                                                         NULL,
    CONSTRAINT [PK_RIASCUPSPACIENTE] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_RIASCUPSPACIENTE_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_RIASCUPSPACIENTE_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_RIASCUPSPACIENTE_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_RIASCUPSPACIENTE_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_RIASCUPSPACIENTE_RIASCUPS] FOREIGN KEY ([IDRIASCUPS]) REFERENCES [dbo].[RIASCUPS] ([ID]),
    CONSTRAINT [FK_RIASCUPSPACIENTE_RIASCUPSD] FOREIGN KEY ([IDRIASCUPSD]) REFERENCES [dbo].[RIASCUPSD] ([ID])
);


GO
ALTER TABLE [dbo].[RIASCUPSPACIENTE] NOCHECK CONSTRAINT [FK_RIASCUPSPACIENTE_ADINGRESO];


GO
ALTER TABLE [dbo].[RIASCUPSPACIENTE] NOCHECK CONSTRAINT [FK_RIASCUPSPACIENTE_INCUPSIPS];


GO
ALTER TABLE [dbo].[RIASCUPSPACIENTE] NOCHECK CONSTRAINT [FK_RIASCUPSPACIENTE_INPACIENT];


GO
ALTER TABLE [dbo].[RIASCUPSPACIENTE] NOCHECK CONSTRAINT [FK_RIASCUPSPACIENTE_INPROFSAL];


GO
ALTER TABLE [dbo].[RIASCUPSPACIENTE] NOCHECK CONSTRAINT [FK_RIASCUPSPACIENTE_RIASCUPS];


GO
ALTER TABLE [dbo].[RIASCUPSPACIENTE] NOCHECK CONSTRAINT [FK_RIASCUPSPACIENTE_RIASCUPSD];


GO
CREATE NONCLUSTERED INDEX [IX_RIASCUPSPACIENTE__IPCODPACI__IDRIASCUPS__CODSERIPS__ESTADO__INC__ID__CANTIDAD__FECHAREALIZACION]
    ON [dbo].[RIASCUPSPACIENTE]([IPCODPACI] ASC, [IDRIASCUPS] ASC, [CODSERIPS] ASC, [ESTADO] ASC)
    INCLUDE([CANTIDAD], [FECHAREALIZACION], [ID]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la fecha de la modificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el codigo de usuario quien modifica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'CODUSUARUMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la fecha de creacion ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'FECHACREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - si la ultima accion del registro fue una Insercion (insert)  2 - si la ultima accion fue una actualizacion (update)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'ACCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Determina el proceso de donde se genero la inscripcion   1 - Dashboard Rias  2 - Orden Medica  3 - Cita Medica  4 - Orden Servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'ORIGEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 -Sin realizar  2 - Realizado  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'ESTADO';

