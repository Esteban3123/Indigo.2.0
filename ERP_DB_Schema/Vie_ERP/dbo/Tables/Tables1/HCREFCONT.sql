CREATE TABLE [dbo].[HCREFCONT] (
    [AUTO]            INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]       VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]       CHAR (10)                                                                        NOT NULL,
    [CODCENATE]       CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]       CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]       CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [MUNCODIGO]       CHAR (5)                                                                         NULL,
    [SERVIDOREM]      CHAR (50)                                                                        NOT NULL,
    [CODIGOIPS]       CHAR (100)                                                                       NULL,
    [NIVELREMIT]      CHAR (10)                                                                        NOT NULL,
    [FECSOLICIT]      DATETIME                                                                         NULL,
    [FECCONFIR]       DATETIME                                                                         NULL,
    [CODESPECI]       CHAR (3)                                                                         NULL,
    [CONFIRMAP]       CHAR (100)                                                                       NULL,
    [FECLLEGADA]      DATETIME                                                                         NULL,
    [QUIENSOLRE]      CHAR (100)                                                                       NULL,
    [MOTREMISI]       CHAR (30)                                                                        NULL,
    [OBSERVACIO]      VARCHAR (MAX)                                                                    NULL,
    [CODUSUMOD]       CHAR (20)                                                                        NULL,
    [FECMODREG]       DATETIME                                                                         NULL,
    [FECREGSIS]       DATETIME                                                                         NULL,
    [CODCONCEC]       INT                                                                              NULL,
    [IDETIPHIS]       CHAR (9)                                                                         NULL,
    [NUMEFOLIO]       NCHAR (10)                                                                       NULL,
    [OBSEREMI]        VARCHAR (MAX)                                                                    NULL,
    [IDHCREFCONP]     INT                                                                              NULL,
    [EXTRAMURAL]      BIT                                                                              CONSTRAINT [DF_HCREFCONT_EXTRAMURAL] DEFAULT ((0)) NULL,
    [RequestType]     INT                                                                              NULL,
    [RequestPriority] INT                                                                              NULL,
    [CODSERIPS]       CHAR (10)                                                                        NULL,
    [QuantityService] INT                                                                              NULL,
    CONSTRAINT [PK_HCREFCONT] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_HCREFCONT_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCREFCONT_HCREFCONP] FOREIGN KEY ([IDHCREFCONP]) REFERENCES [dbo].[HCREFCONP] ([AUTO]),
    CONSTRAINT [FK_HCREFCONT_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [IX_HCREFCONT] UNIQUE NONCLUSTERED ([CODCONCEC] ASC)
);


GO
ALTER TABLE [dbo].[HCREFCONT] NOCHECK CONSTRAINT [FK_HCREFCONT_ADINGRESO];


GO
ALTER TABLE [dbo].[HCREFCONT] NOCHECK CONSTRAINT [FK_HCREFCONT_HCREFCONP];


GO
ALTER TABLE [dbo].[HCREFCONT] NOCHECK CONSTRAINT [FK_HCREFCONT_INPACIENT];


GO
CREATE NONCLUSTERED INDEX [IX_HCREFCONT_IDHCREFCONP_IPCODPACI_NUMINGRES_INC_CODCENATE_CODPROSAL_FECREGSIS_FECSOLICIT_SERVIDOREM_UFUCODIGO]
    ON [dbo].[HCREFCONT]([IDHCREFCONP] ASC, [IPCODPACI] ASC, [NUMINGRES] ASC)
    INCLUDE([CODCENATE], [CODPROSAL], [FECREGSIS], [FECSOLICIT], [SERVIDOREM], [UFUCODIGO]);


GO
CREATE NONCLUSTERED INDEX [IX_HCREFCONT__IPCODPACI__NUMEFOLIO]
    ON [dbo].[HCREFCONT]([IPCODPACI] ASC, [NUMEFOLIO] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'16-09-2024 campo nuevo:
Cantidad del servicio relacionado, codigo CUPS que el medico selecciona al ordenar una referencia o contrareferencia.
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'QuantityService';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'16-09-2024 campo nuevo:
Codigo del servicio relacionado, codigo CUPS que el medico selecciona al ordenar una referencia o contrareferencia.
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Prioritaria = 1 
No prioritaria = 2 
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'RequestPriority';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia = 1
Contrarreferencia = 2 

', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'RequestType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si la Solicitud de referencia por parte del medico fue marcada como extramural.   True    False ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'EXTRAMURAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla cabecera HCREFCONP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'IDHCREFCONP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observación de la remisión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'OBSEREMI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo del documento ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del registro en el sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'FECREGSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificacion del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'FECMODREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Usuario que modifica el registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'CODUSUMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observacion (resumen de la historia clinica)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'OBSERVACIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Motivos de remision', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'MOTREMISI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'quien solicita la remision', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'QUIENSOLRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de salida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'FECLLEGADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Persona que confirma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'CONFIRMAP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de confirmacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'FECCONFIR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha solicitud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'FECSOLICIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nivel a que se remite', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'NIVELREMIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la institucion prestadora de servicios de salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'CODIGOIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Servicio a donde se remite', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'SERVIDOREM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Municipio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'MUNCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'AUTO';

