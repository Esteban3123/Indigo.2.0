CREATE TABLE [dbo].[HCHOJAGASTOQX] (
    [ID]                 INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CONSECUTIVO]        VARCHAR (20)                                                                     NOT NULL,
    [IPCODPACI]          VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]          CHAR (10)                                                                        NOT NULL,
    [IDAGEPROGQX]        INT                                                                              NOT NULL,
    [FECHAREGISTRO]      DATETIME                                                                         NOT NULL,
    [ESTADO]             INT                                                                              NOT NULL,
    [USUARIODESCONFIRMA] CHAR (20)                                                                        NULL,
    [FECHADESCONFIRMA]   DATETIME                                                                         NULL,
    [ConcurrencyControl] ROWVERSION                                                                       NULL,
    [UserTryingConfirm]  CHAR (20)                                                                        NULL,
    CONSTRAINT [PK_HCHOJAGASTOQX] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCHOJAGASTOQX_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCHOJAGASTOQX_AGEPROGQX] FOREIGN KEY ([IDAGEPROGQX]) REFERENCES [dbo].[AGEPROGQX] ([CODAUTONU]),
    CONSTRAINT [FK_HCHOJAGASTOQX_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HCHOJAGASTOQX] NOCHECK CONSTRAINT [FK_HCHOJAGASTOQX_ADINGRESO];


GO
ALTER TABLE [dbo].[HCHOJAGASTOQX] NOCHECK CONSTRAINT [FK_HCHOJAGASTOQX_AGEPROGQX];


GO
ALTER TABLE [dbo].[HCHOJAGASTOQX] NOCHECK CONSTRAINT [FK_HCHOJAGASTOQX_INPACIENT];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de usuario que trata de realizar la confirmacion de la Hoja gasto QX', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQX', @level2type = N'COLUMN', @level2name = N'UserTryingConfirm';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'controla la concurrencia multiple de una hoja de gasto QX', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQX', @level2type = N'COLUMN', @level2name = N'ConcurrencyControl';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de desconfirmacion Hoja de gasto QX', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQX', @level2type = N'COLUMN', @level2name = N'FECHADESCONFIRMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Usuario desconfirma Hoja de gasto QX', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQX', @level2type = N'COLUMN', @level2name = N'USUARIODESCONFIRMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1- Sin confirmar (hoja de gasto QX  sin procesar)  2- En espera aceptacion devolutivo(bloquea hoja gasto QX a la espera de proceso de devoluciones)  3 -Devolucion parcial aceptada (cuando se acepta una parte)  4 - Confirmada (cuando se acepta todo devolutivo y/o cuando se gasto todo)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQX', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha creacion registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQX', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la programacion de cirugia principal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQX', @level2type = N'COLUMN', @level2name = N'IDAGEPROGQX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQX', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cedula del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQX', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'consecutivo INCONSECU', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQX', @level2type = N'COLUMN', @level2name = N'CONSECUTIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQX', @level2type = N'COLUMN', @level2name = N'ID';

