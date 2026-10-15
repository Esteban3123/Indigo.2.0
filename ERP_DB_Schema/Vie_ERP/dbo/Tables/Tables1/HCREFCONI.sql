CREATE TABLE [dbo].[HCREFCONI] (
    [AUTO]       INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]  VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]  CHAR (10)                                                                        NOT NULL,
    [CODCENATE]  CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]  CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]  CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [MUNCODIGO]  CHAR (5)                                                                         NULL,
    [SERVIDOREM] CHAR (50)                                                                        NOT NULL,
    [CODIGOIPS]  CHAR (100)                                                                       NULL,
    [NIVELREMIT] CHAR (10)                                                                        NOT NULL,
    [FECSOLICIT] DATETIME                                                                         NULL,
    [FECCONFIR]  DATETIME                                                                         NULL,
    [CODESPECI]  CHAR (3)                                                                         NULL,
    [CONFIRMAP]  CHAR (50)                                                                        NOT NULL,
    [FECLLEGADA] DATETIME                                                                         NULL,
    [QUIENSOLRE] CHAR (50)                                                                        NULL,
    [MOTREMISI]  CHAR (30)                                                                        NULL,
    [OBSERVACIO] VARCHAR (MAX)                                                                    NULL,
    [CODUSUMOD]  CHAR (20)                                                                        NULL,
    [FECMODREG]  DATETIME                                                                         NULL,
    [FECREGSIS]  DATETIME                                                                         NULL,
    CONSTRAINT [PK_HCREFCONI] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_HCREFCONI_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCREFCONI_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HCREFCONI] NOCHECK CONSTRAINT [FK_HCREFCONI_ADINGRESO];


GO
ALTER TABLE [dbo].[HCREFCONI] NOCHECK CONSTRAINT [FK_HCREFCONI_INPACIENT];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del registro en el sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONI', @level2type = N'COLUMN', @level2name = N'FECREGSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificacion del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONI', @level2type = N'COLUMN', @level2name = N'FECMODREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Usuario que modifica el registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONI', @level2type = N'COLUMN', @level2name = N'CODUSUMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observacion (resumen de la historia clinica)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONI', @level2type = N'COLUMN', @level2name = N'OBSERVACIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Motivos de remision', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONI', @level2type = N'COLUMN', @level2name = N'MOTREMISI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'quien solicita la remision', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONI', @level2type = N'COLUMN', @level2name = N'QUIENSOLRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de salida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONI', @level2type = N'COLUMN', @level2name = N'FECLLEGADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Persona que confirma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONI', @level2type = N'COLUMN', @level2name = N'CONFIRMAP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONI', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de confirmacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONI', @level2type = N'COLUMN', @level2name = N'FECCONFIR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha solicitud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONI', @level2type = N'COLUMN', @level2name = N'FECSOLICIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nivel a que se remite', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONI', @level2type = N'COLUMN', @level2name = N'NIVELREMIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la institucion prestadora de servicios de salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONI', @level2type = N'COLUMN', @level2name = N'CODIGOIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Servicio a donde se remite', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONI', @level2type = N'COLUMN', @level2name = N'SERVIDOREM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Municipio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONI', @level2type = N'COLUMN', @level2name = N'MUNCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONI', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONI', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONI', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONI', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONI', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONI', @level2type = N'COLUMN', @level2name = N'AUTO';

