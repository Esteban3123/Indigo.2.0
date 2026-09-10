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
    [NIVELREMIT]      CHAR (10)                                                                        CONSTRAINT [DF_HCREFCONT_NIVELREMIT] DEFAULT ('1') NULL,
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
ALTER TABLE [dbo].[HCREFCONT] NOCHECK CONSTRAINT [FK_HCREFCONT_INPACIENT];


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCREFCONT].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCREFCONT].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');




GO

CREATE NONCLUSTERED INDEX [IX_HCREFCONT_IDHCREFCONP_IPCODPACI_NUMINGRES_INC_CODCENATE_CODPROSAL_FECREGSIS_FECSOLICIT_SERVIDOREM_UFUCODIGO]
    ON [dbo].[HCREFCONT]([IDHCREFCONP] ASC, [IPCODPACI] ASC, [NUMINGRES] ASC)
    INCLUDE([CODCENATE], [CODPROSAL], [FECREGSIS], [FECSOLICIT], [SERVIDOREM], [UFUCODIGO]);


GO
CREATE NONCLUSTERED INDEX [IX_HCREFCONT__IPCODPACI__NUMEFOLIO]
    ON [dbo].[HCREFCONT]([IPCODPACI] ASC, [NUMEFOLIO] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de servicios CUPS ordenados en referencia/contrareferencia. INT, nuevo campo (16-09-2024).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'QuantityService';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'16-09-2024 campo nuevo:
Cantidad del servicio relacionado, codigo CUPS que el medico selecciona al ordenar una referencia o contrareferencia.
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'QuantityService';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'QuantityService';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del servicio CUPS relacionado a referencia/contrareferencia ordenado por médico. CHAR(10), nuevo campo (16-09-2024).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'16-09-2024 campo nuevo:
Codigo del servicio relacionado, codigo CUPS que el medico selecciona al ordenar una referencia o contrareferencia.
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Prioridad de la solicitud: 1=Prioritaria, 2=No prioritaria. INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'RequestPriority';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Prioritaria = 1 
No prioritaria = 2 
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'RequestPriority';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'RequestPriority';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de solicitud: 1=Referencia, 2=Contrareferencia. INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'RequestType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Referencia = 1
Contrarreferencia = 2 

', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'RequestType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'RequestType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si referencia fue marcada como extramural (fuera del centro). BIT, True/False, default 0.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'EXTRAMURAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si la Solicitud de referencia por parte del medico fue marcada como extramural.   True    False ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'EXTRAMURAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'EXTRAMURAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador FK de cabecera HCREFCONP. INT, referencia a tabla padre de referencia/contrareferencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'IDHCREFCONP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla cabecera HCREFCONP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'IDHCREFCONP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'IDHCREFCONP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones o notas adicionales del médico emisor sobre la remisión. VARCHAR(MAX).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'OBSEREMI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observación de la remisión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'OBSEREMI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'OBSEREMI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número secuencial de folio o documento de referencia/contrareferencia. NCHAR(10).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tipo de historia clínica asociada. CHAR(9).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo único del documento de referencia/contrareferencia. INT, UNIQUE.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo del documento ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de registro inicial en el sistema. DATETIME.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'FECREGSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del registro en el sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'FECREGSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'FECREGSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación del registro. DATETIME.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'FECMODREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificacion del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'FECMODREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'FECMODREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario que realizó la última modificación. CHAR(20).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'CODUSUMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Usuario que modifica el registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'CODUSUMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'CODUSUMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones clínicas, resumen de historia clínica del paciente. VARCHAR(MAX).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'OBSERVACIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observacion (resumen de la historia clinica)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'OBSERVACIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'OBSERVACIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo, razón o indicación clínica de la remisión. CHAR(30).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'MOTREMISI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Motivos de remision', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'MOTREMISI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'MOTREMISI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o identificación de profesional que solicita la remisión. CHAR(100).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'QUIENSOLRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'quien solicita la remision', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'QUIENSOLRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'QUIENSOLRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de salida o llegada de la remisión al destino. DATETIME.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'FECLLEGADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de salida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'FECLLEGADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'FECLLEGADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o identificación de quien confirma recepción de la remisión. CHAR(100).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'CONFIRMAP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Persona que confirma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'CONFIRMAP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'CONFIRMAP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de especialidad médica destino de la referencia/contrareferencia. CHAR(3).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de confirmación de recepción de la remisión. DATETIME.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'FECCONFIR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de confirmacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'FECCONFIR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'FECCONFIR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de solicitud de la referencia/contrareferencia. DATETIME.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'FECSOLICIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha solicitud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'FECSOLICIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'FECSOLICIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel de complejidad destino: primario, secundario, terciario. CHAR(10).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'NIVELREMIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nivel a que se remite', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'NIVELREMIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'NIVELREMIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de Institución Prestadora de Servicios de Salud destino. CHAR(100).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'CODIGOIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la institucion prestadora de servicios de salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'CODIGOIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'CODIGOIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Servicio o departamento destino de la remisión. CHAR(50).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'SERVIDOREM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Servicio a donde se remite', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'SERVIDOREM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'SERVIDOREM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del municipio donde se remite o destino geográfico. CHAR(5).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'MUNCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Municipio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'MUNCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'MUNCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud que ordena la remisión (médico). CHAR(20), masked, PII.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de Unidad Funcional origen de la remisión. CHAR(10).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del Centro de Atención origen de la remisión. CHAR(10).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso o atención del paciente, FK a ADINGRESO. CHAR(10).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código, cédula o identificación del paciente, FK a INPACIENT. VARCHAR(25), masked, PII.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable del registro de referencia/contrareferencia. INT IDENTITY, PK.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT', @level2type = N'COLUMN', @level2name = N'AUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de referencias y contrarreferencias de pacientes entre instituciones de salud. Guarda las solicitudes de remisión, confirmación de citas, nivel de atención, IPS destino y seguimiento del traslado de pacientes desde y hacia otros centros o especialidades.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONT';
