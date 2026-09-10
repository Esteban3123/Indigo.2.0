CREATE TABLE [dbo].[CHTRAMEDC] (
    [CODCONCEC]  NUMERIC (18)                                                                     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FECREGTRA]  DATETIME                                                                         NOT NULL,
    [FECREGACE]  DATETIME                                                                         NULL,
    [CODPROENT]  CHAR (20)                                                                        NOT NULL,
    [CODPROACE]  CHAR (20)                                                                        NULL,
    [IPCODPACI]  VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]  CHAR (10)                                                                        NOT NULL,
    [CODCENATE]  CHAR (10)                                                                        NOT NULL,
    [UFUCODORI]  CHAR (10)                                                                        NOT NULL,
    [UFUCODDES]  CHAR (10)                                                                        NOT NULL,
    [CODICAORI]  INT                                                                              NOT NULL,
    [CODICADES]  INT                                                                              NOT NULL,
    [ORDESTADO]  CHAR (1)                                                                         NOT NULL,
    [JUSANUTRAS] CHAR (250)                                                                       NULL,
    CONSTRAINT [PK_HCFARMDEVC] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC),
    CONSTRAINT [FK_CHTRAMEDC_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_CHTRAMEDC_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_CHTRAMEDC_CHCAMASHO] FOREIGN KEY ([CODICAORI]) REFERENCES [dbo].[CHCAMASHO] ([CODICAMAS]),
    CONSTRAINT [FK_CHTRAMEDC_CHCAMASHO1] FOREIGN KEY ([CODICADES]) REFERENCES [dbo].[CHCAMASHO] ([CODICAMAS]),
    CONSTRAINT [FK_CHTRAMEDC_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_CHTRAMEDC_INPROFSAL] FOREIGN KEY ([CODPROACE]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_CHTRAMEDC_INPROFSAL1] FOREIGN KEY ([CODPROENT]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_CHTRAMEDC_INUNIFUNC] FOREIGN KEY ([UFUCODORI]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_CHTRAMEDC_INUNIFUNC1] FOREIGN KEY ([UFUCODDES]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[CHTRAMEDC].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
CREATE NONCLUSTERED INDEX [IX_CHTRAMEDC_IPCODPACI_NUMINGRES_CODICAORI]
    ON [dbo].[CHTRAMEDC]([IPCODPACI] ASC, [NUMINGRES] ASC, [CODICAORI] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación de la anulación del traslado; motivo documentado para cancelar la orden de transferencia de paciente entre camas o unidades funcionales.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTRAMEDC', @level2type = N'COLUMN', @level2name = N'JUSANUTRAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Justificacion de la anulacion del traslado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTRAMEDC', @level2type = N'COLUMN', @level2name = N'JUSANUTRAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTRAMEDC', @level2type = N'COLUMN', @level2name = N'JUSANUTRAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la Orden de Traslado: 1=Enviado, 2=Aceptado, 3=Anulado; indica el ciclo de vida de la solicitud de transferencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTRAMEDC', @level2type = N'COLUMN', @level2name = N'ORDESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la Orden:  1: Enviado  2: Aceptado  3: Anulado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTRAMEDC', @level2type = N'COLUMN', @level2name = N'ORDESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTRAMEDC', @level2type = N'COLUMN', @level2name = N'ORDESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la Cama Destino (FK CHCAMASHO); identifica la cama receptora del traslado del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTRAMEDC', @level2type = N'COLUMN', @level2name = N'CODICADES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Cama Destino', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTRAMEDC', @level2type = N'COLUMN', @level2name = N'CODICADES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTRAMEDC', @level2type = N'COLUMN', @level2name = N'CODICADES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la Cama Origen (FK CHCAMASHO); identifica la cama inicial de donde se traslada el paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTRAMEDC', @level2type = N'COLUMN', @level2name = N'CODICAORI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Cama Origen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTRAMEDC', @level2type = N'COLUMN', @level2name = N'CODICAORI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTRAMEDC', @level2type = N'COLUMN', @level2name = N'CODICAORI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la Unidad Funcional Destino (FK INUNIFUNC); unidad o departamento receptor del traslado de paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTRAMEDC', @level2type = N'COLUMN', @level2name = N'UFUCODDES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional Destino', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTRAMEDC', @level2type = N'COLUMN', @level2name = N'UFUCODDES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTRAMEDC', @level2type = N'COLUMN', @level2name = N'UFUCODDES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la Unidad Funcional Origen (FK INUNIFUNC); unidad o departamento de procedencia del traslado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTRAMEDC', @level2type = N'COLUMN', @level2name = N'UFUCODORI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional Origen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTRAMEDC', @level2type = N'COLUMN', @level2name = N'UFUCODORI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTRAMEDC', @level2type = N'COLUMN', @level2name = N'UFUCODORI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del Centro de Atención (FK ADCENATEN); identificador de la sede, hospital o establecimiento donde se registra el traslado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTRAMEDC', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTRAMEDC', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTRAMEDC', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del Ingreso (FK ADINGRESO); identificador único de la atención hospitalaria del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTRAMEDC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTRAMEDC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTRAMEDC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del Paciente (PII, FK INPACIENT); cédula, documento de identidad u identificador único del paciente (dato sensible).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTRAMEDC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTRAMEDC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTRAMEDC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del Profesional de la Salud que Acepta el Traslado (FK INPROFSAL); médico, enfermero o profesional que autoriza recibir al paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTRAMEDC', @level2type = N'COLUMN', @level2name = N'CODPROACE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud que Acepta Traslado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTRAMEDC', @level2type = N'COLUMN', @level2name = N'CODPROACE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTRAMEDC', @level2type = N'COLUMN', @level2name = N'CODPROACE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del Profesional de la Salud que Traslada (FK INPROFSAL); médico, enfermero o profesional que inicia la orden de transferencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTRAMEDC', @level2type = N'COLUMN', @level2name = N'CODPROENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud que Traslada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTRAMEDC', @level2type = N'COLUMN', @level2name = N'CODPROENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTRAMEDC', @level2type = N'COLUMN', @level2name = N'CODPROENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de Aceptación del Traslado; timestamp de cuando el profesional receptor confirma recibir al paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTRAMEDC', @level2type = N'COLUMN', @level2name = N'FECREGACE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la Aceptacion del Traslado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTRAMEDC', @level2type = N'COLUMN', @level2name = N'FECREGACE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTRAMEDC', @level2type = N'COLUMN', @level2name = N'FECREGACE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del Registro del Traslado; timestamp de creación de la orden de transferencia entre camas o unidades.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTRAMEDC', @level2type = N'COLUMN', @level2name = N'FECREGTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del Registro del Traslado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTRAMEDC', @level2type = N'COLUMN', @level2name = N'FECREGTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTRAMEDC', @level2type = N'COLUMN', @level2name = N'FECREGTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del Consecutivo (PK, IDENTITY); identificador único autonumérico del registro de traslado intrahospitalario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTRAMEDC', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTRAMEDC', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTRAMEDC', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de traslados médicos de pacientes entre unidades funcionales o centros de atención. Guarda la trazabilidad del movimiento del paciente desde un origen hasta un destino, incluyendo quién lo envió, quién lo aceptó y el estado de la transferencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTRAMEDC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTRAMEDC';
