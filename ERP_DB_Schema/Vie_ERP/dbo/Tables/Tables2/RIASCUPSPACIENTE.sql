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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Usuario que genero la orden de servicio VIE ERP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'CODUSUARU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Profesional que se coloco en la orden  de servicio generada de VIE ERP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del cups', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'VALORCUPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la factura de VIE ERP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'IDFACTURA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID detalle de la orden de servicios de vie ERP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'IDDETALLEORDENSERVICIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cita (AGASICITA)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'IDCITA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha en que se realizo CUPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'FECHAREALIZACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ingreso con el que se genero la orden de servicio VIE ERP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Folio de la HC que solicito, solo cuando el CUPS requiere orden medica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'FOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - si la ultima accion del registro fue una Insercion (insert)  2 - si la ultima accion fue una actualizacion (update)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'ACCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Determina el proceso de donde se genero la inscripcion   1 - Dashboard Rias  2 - Orden Medica  3 - Cita Medica  4 - Orden Servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'ORIGEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de relacion con el rango que me genero el registro (RIASCUPSD)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'IDRIASCUPSD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha maxima para realizar el cups', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'FECHAMAXREALIZAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha minima para realizar el cups', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'FECHAMINREALIZAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 -Sin realizar  2 - Realizado  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'cantidad solicitada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'CANTIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de CUPS ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la RIAS (Tabla RIAS)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'IDRIASCUPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificacion del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSPACIENTE', @level2type = N'COLUMN', @level2name = N'ID';

