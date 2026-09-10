CREATE TABLE [dbo].[PADCHEQUEO] (
    [ID]              INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NUMINGRES]       CHAR (10)                                                                        NOT NULL,
    [IPCODPACI]       VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [IDLISTACHEQUEO]  INT                                                                              NOT NULL,
    [IDLISTACHEQUEOD] INT                                                                              NOT NULL,
    [CHEQUEADO]       BIT                                                                              NOT NULL,
    [CAMPOTEXTO]      VARCHAR (500)                                                                    NULL,
    [CAMPOFECHA]      DATETIME                                                                         NULL,
    [URLADJUNTO]      VARCHAR (100)                                                                    NULL,
    CONSTRAINT [PK_PADCHEQUEO] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PADCHEQUEO_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_PADCHEQUEO_HCLISTACC] FOREIGN KEY ([IDLISTACHEQUEO]) REFERENCES [dbo].[HCLISTACC] ([CODCONSEC]),
    CONSTRAINT [FK_PADCHEQUEO_HCLISTACD] FOREIGN KEY ([IDLISTACHEQUEOD]) REFERENCES [dbo].[HCLISTACD] ([CODCONSEC]),
    CONSTRAINT [FK_PADCHEQUEO_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[PADCHEQUEO].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');



GO
CREATE NONCLUSTERED INDEX [IX_PADCHEQUEO_IPCODPACI]
    ON [dbo].[PADCHEQUEO]([IPCODPACI] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PADCHEQUEO_NUMINGRES]
    ON [dbo].[PADCHEQUEO]([NUMINGRES] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'URL o ruta del archivo adjunto/evidencia/documento relacionado con el resultado del chequeo, VARCHAR(100).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCHEQUEO', @level2type = N'COLUMN', @level2name = N'URLADJUNTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado campo Adjunto, URL del adjunto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCHEQUEO', @level2type = N'COLUMN', @level2name = N'URLADJUNTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCHEQUEO', @level2type = N'COLUMN', @level2name = N'URLADJUNTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del registro, diligenciamiento o validación del elemento de chequeo, tipo DATETIME.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCHEQUEO', @level2type = N'COLUMN', @level2name = N'CAMPOFECHA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado campo fecha ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCHEQUEO', @level2type = N'COLUMN', @level2name = N'CAMPOFECHA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCHEQUEO', @level2type = N'COLUMN', @level2name = N'CAMPOFECHA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo de texto libre para observaciones, comentarios o notas adicionales asociadas al ítem de chequeo, VARCHAR(500).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCHEQUEO', @level2type = N'COLUMN', @level2name = N'CAMPOTEXTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado campo observacion ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCHEQUEO', @level2type = N'COLUMN', @level2name = N'CAMPOTEXTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCHEQUEO', @level2type = N'COLUMN', @level2name = N'CAMPOTEXTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera booleana que indica si el elemento de la lista de chequeo fue marcado/validado/completado durante la atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCHEQUEO', @level2type = N'COLUMN', @level2name = N'CHEQUEADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Define si fue seleccionada la lista de chequeo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCHEQUEO', @level2type = N'COLUMN', @level2name = N'CHEQUEADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCHEQUEO', @level2type = N'COLUMN', @level2name = N'CHEQUEADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle específico de la lista de chequeo, FK a HCLISTACD, línea individual del protocolo de verificación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCHEQUEO', @level2type = N'COLUMN', @level2name = N'IDLISTACHEQUEOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del detalle de la lista de chequeo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCHEQUEO', @level2type = N'COLUMN', @level2name = N'IDLISTACHEQUEOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCHEQUEO', @level2type = N'COLUMN', @level2name = N'IDLISTACHEQUEOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la lista de chequeo padre (maestro), FK a HCLISTACC, referencia a la plantilla de verificación clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCHEQUEO', @level2type = N'COLUMN', @level2name = N'IDLISTACHEQUEO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la lista de chequeo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCHEQUEO', @level2type = N'COLUMN', @level2name = N'IDLISTACHEQUEO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCHEQUEO', @level2type = N'COLUMN', @level2name = N'IDLISTACHEQUEO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente (cédula, identificación, documento), PII ofuscado, FK a INPACIENT, equivalente a identificación única del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCHEQUEO', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCHEQUEO', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCHEQUEO', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del ingreso o atención del paciente, FK a ADINGRESO, referencia única de la hospitalización/consulta.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCHEQUEO', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCHEQUEO', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCHEQUEO', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de chequeo en la tabla PADCHEQUEO, clave primaria INT IDENTITY.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCHEQUEO', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificaión del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCHEQUEO', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCHEQUEO', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Respuestas y estado de verificación de chequeos clínicos por paciente e ingreso. Registra si cada ítem de una lista de chequeo (checklist) fue marcado como completado, junto con observaciones en texto, fecha y documentos adjuntos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCHEQUEO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCHEQUEO';
